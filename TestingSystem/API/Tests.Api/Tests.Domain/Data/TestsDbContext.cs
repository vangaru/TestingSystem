using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tests.Domain.Models;

namespace Tests.Domain.Data;

public class TestsDbContext : IdentityDbContext<TestsUser>
{
    public DbSet<Question>? Questions { get; set; }
    public DbSet<QuestionAnswer>? QuestionAnswers { get; set; }
    public DbSet<Test>? Tests { get; set; }
    public DbSet<TestResult>? TestResults { get; set; }
    
    public TestsDbContext(DbContextOptions<TestsDbContext> options) : base(options)
    {
    }
}