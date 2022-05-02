using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tests.Api.Interfaces;
using Tests.Api.Models;
using Tests.Domain.Models;

namespace Tests.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<TestsUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenProvider _tokenProvider;
    
    public AccountController(
        UserManager<TestsUser> userManager, 
        RoleManager<IdentityRole> roleManager,
        ITokenProvider tokenProvider)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenProvider = tokenProvider;
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

        JwtSecurityToken jwtToken = _tokenProvider.CreateToken(authClaims);
        string refreshToken = _tokenProvider.GenerateRefreshToken();

        int refreshTokenValidityInDays = _tokenProvider.GetRefreshTokenValidityInDays();

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

        await AddToRole(user, registerModel.UserRole!);

        var successResponse = new Response {Status = "Success", Message = "User created successfully!"};
        return Ok(successResponse);
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(Token token)
    {
        string? accessToken = token.AccessToken;
        string? refreshToken = token.RefreshToken;

        ClaimsPrincipal? principal = _tokenProvider.GetPrincipalFromExpiredToken(accessToken);

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

        JwtSecurityToken newAccessToken = _tokenProvider.CreateToken(principal.Claims.ToList());
        string newRefreshToken = _tokenProvider.GenerateRefreshToken();

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

    [HttpGet]
    [Route("{userName}/in-role/{role}")]
    public async Task<IActionResult> IsInRole(string userName, string role)
    {
        TestsUser user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return BadRequest("Invalid user name");
        }
        
        IList<string> userRoles = await _userManager.GetRolesAsync(user);
        bool isInRole = userRoles.Contains(role);
        return Ok(isInRole);
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        IQueryable<TestsUser> users = _userManager.Users;
        
        if (users == null)
        {
            return BadRequest("Failed to get users");
        }

        IEnumerable<UserViewModel> viewUsers = users.Select(u => new UserViewModel
        {
            Id = u.Id,
            Email = u.Email,
            UserName = u.UserName,
            Role = _userManager.GetRolesAsync(u).Result.Count > 0 ? _userManager.GetRolesAsync(u).Result[0] : ""
        });

        return Ok(viewUsers);
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

    private async Task AddToRole(TestsUser user, string userRole)
    {
        if (!await _roleManager.RoleExistsAsync(userRole))
        {
            var role = new IdentityRole(userRole);
            await _roleManager.CreateAsync(role);
        }
        
        await _userManager.AddToRoleAsync(user, userRole);
    }

    #endregion
}