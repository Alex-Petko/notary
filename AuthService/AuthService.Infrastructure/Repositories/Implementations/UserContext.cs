using AuthService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure;

internal class UserContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        
    }
}
