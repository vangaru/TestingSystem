using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tests.Domain.Models;

namespace Tests.Domain.Data;

public class TestsDbContext : IdentityDbContext<TestsUser>
{
    public TestsDbContext(DbContextOptions<TestsDbContext> options) : base(options)
    {
    }
}