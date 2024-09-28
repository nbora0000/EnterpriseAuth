using Microsoft.EntityFrameworkCore;
using AuthenticationApi.Domain;
namespace AuthenticationApi.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<KeySecret> KeySecret { get; set; }


}

