using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tests.Api.Implementations;
using Tests.Api.Interfaces;
using Tests.Application.Implementations;
using Tests.Application.Interfaces;
using Tests.Domain.Data;
using Tests.Domain.Implementations;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<TestsDbContext>(options =>
    options.UseSqlite(configuration.GetConnectionString("TestsConnectionString"), 
        b => b.MigrationsAssembly("Tests.Api")));

builder.Services.AddIdentity<TestsUser, IdentityRole>()
    .AddEntityFrameworkStores<TestsDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(o =>
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ITokenProvider, TokenProvider>();
builder.Services.AddTransient<IQuestionsRepository, QuestionsesRepository>();
builder.Services.AddTransient<IQuestionAnswerRepository, QuestionAnswerRepository>();
builder.Services.AddTransient<ITestsRepository, TestsRepository>();
builder.Services.AddTransient<ITestResultRepository, TestResultRepository>();
builder.Services.AddTransient<ITestsService, TestsService>();
builder.Services.AddTransient<IQuestionsService, QuestionsService>();
builder.Services.AddTransient<ITestResultService, TestResultService>();
builder.Services.AddTransient<ITestsInfoProvider, TestsInfoProvider>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IUsersInfoProvider, UsersInfoProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(b => b.AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();