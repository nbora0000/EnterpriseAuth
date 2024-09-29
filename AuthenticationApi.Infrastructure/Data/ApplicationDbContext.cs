using Microsoft.EntityFrameworkCore;
using AuthenticationApi.Domain.Data;
namespace AuthenticationApi.Infrastructure.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<KeySecret> KeySecret { get; set; }
}

