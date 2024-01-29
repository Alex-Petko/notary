using DebtManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Infrastructure;

public sealed class Context : DbContext
{
    public DbSet<Debt> Debts { get; set; } = null!;

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DebtEntityTypeConfiguration());
    }
}