using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DealProject.Infrastructure;

[ExcludeFromCodeCoverage]
internal class DealContext : DbContext
{
    public DealContext(DbContextOptions<DealContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DebtEntityTypeConfiguration());
    }
}