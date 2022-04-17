using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Tests.Api.Models;
using Tests.Domain.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Tests.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private const string ValidIssuerParameter = "JWT:ValidIssuer";
    private const string ValidAudienceParameter = "JWT:ValidAudience";
    private const string SecretParameter = "JWT:Secret";
    private const string TokenValidityInMinutesParameter = "JWT:TokenValidityInMinutes";
    private const string RefreshTokenValidityInDaysParameter = "JWT:RefreshTokenValidityInDays";

    private readonly UserManager<TestsUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    
    public AccountController(
        UserManager<TestsUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        TestsUser user = await _userManager.FindByNameAsync(loginModel.UserName);

        if (!await UserPasswordValid(user, loginModel.Password))
        {
            return Unauthorized();
        }

        IList<string> userRoles = await _userManager.GetRolesAsync(user);
        List<Claim> authClaims = GetAuthClaims(user);

        foreach (string userRole in userRoles)
        {
            var authClaim = new Claim(ClaimTypes.Role, userRole);
            authClaims.Add(authClaim);
        }

        JwtSecurityToken jwtToken = CreateToken(authClaims);
        string refreshToken = GenerateRefreshToken();

        _ = int.TryParse(_configuration[RefreshTokenValidityInDaysParameter], out int refreshTokenValidityInDays);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            RefreshToken = refreshToken,
            Expiration = jwtToken.ValidTo
        });
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (await UserExists(registerModel.UserName))
        {
            var userExistsResponse = new Response
            {
                Status = StatusCodes.Status400BadRequest.ToString(),
                Message = "User already exists!"
            };
            return StatusCode(StatusCodes.Status400BadRequest, userExistsResponse);
        }

        var user = new TestsUser
        {
            Email = registerModel.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerModel.UserName
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerModel.Password);
        if (!result.Succeeded)
        {
            var registerFailedResponse = new Response
            {
                Status = "Error", 
                Message = "User creation failed! Please check user details and try again."
            };
            return StatusCode(StatusCodes.Status500InternalServerError, registerFailedResponse);
        }

        var successResponse = new Response {Status = "Success", Message = "User created successfully!"};
        return Ok(successResponse);
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(Token token)
    {
        string? accessToken = token.AccessToken;
        string? refreshToken = token.RefreshToken;

        ClaimsPrincipal? principal = GetPrincipalFromExpiredToken(accessToken);

        if (principal == null)
        {
            return BadRequest("Invalid access token or refresh token");
        }
        
#pragma warning disable CS8600 
#pragma warning disable CS8602
        string userName = principal.Identity.Name;
#pragma warning restore CS8602
#pragma warning restore CS8600

        TestsUser user = await _userManager.FindByNameAsync(userName);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        JwtSecurityToken newAccessToken = CreateToken(principal.Claims.ToList());
        string newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    [Authorize]
    [HttpPost]
    [Route("revoke/{userName}")]
    public async Task<IActionResult> Revoke(string userName)
    {
        TestsUser user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return BadRequest("Invalid user name");
        }

        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
        
        return NoContent();
    }

    [Authorize]
    [HttpPost]
    [Route("revoke-all")]
    public async Task<IActionResult> RevokeAll()
    {
        List<TestsUser> users = _userManager.Users.ToList();
        foreach (TestsUser user in users)
        {
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        return NoContent();
    }
 
    #region Helpers

    private async Task<bool> UserPasswordValid(TestsUser? user, string? password)
    {
        return user != null && await _userManager.CheckPasswordAsync(user, password);
    }

    private async Task<bool> UserExists(string? userName)
    {
        TestsUser user = await _userManager.FindByNameAsync(userName);
        return user != null;
    }

    private List<Claim> GetAuthClaims(TestsUser user)
    {
        var authClaims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return authClaims;
    }

    private JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[SecretParameter]));
        var signingCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);
        _ = int.TryParse(_configuration[TokenValidityInMinutesParameter], out int tokenValidityInMinutes);

        var token = new JwtSecurityToken(
            issuer: _configuration[ValidIssuerParameter],
            audience: _configuration[ValidAudienceParameter],
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: signingCredentials
        );

        return token;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        string refreshToken = Convert.ToBase64String(randomNumber);
        return refreshToken;
    }

    private async Task AddToRole(TestsUser user, UserRoles userRole)
    {
        if (!await _roleManager.RoleExistsAsync(userRole.ToString()))
        {
            var role = new IdentityRole(userRole.ToString());
            await _roleManager.CreateAsync(role);
        }

        await _userManager.AddToRoleAsync(user, userRole.ToString());
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[SecretParameter])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (!TokenValid(securityToken))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private bool TokenValid(SecurityToken securityToken)
    {
        return !(securityToken is not JwtSecurityToken jwtSecurityToken ||
                 !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                     StringComparison.InvariantCultureIgnoreCase));
    }

    #endregion
}