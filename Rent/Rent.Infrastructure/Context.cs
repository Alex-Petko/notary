using Microsoft.EntityFrameworkCore;
using Rent.Domain;
using System.Diagnostics.CodeAnalysis;

namespace Rent.Infrastructure;

[ExcludeFromCodeCoverage]
public sealed class Context : DbContext
{
    public DbSet<FileDescription> TemplateDescriptions { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FileDescriptionConfiguration());
    }
}
