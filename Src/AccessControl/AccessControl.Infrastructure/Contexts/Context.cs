using AccessControl.Domain;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure;

public sealed class Context : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());
    }
}