using Microsoft.EntityFrameworkCore;
using Rent.Domain;

namespace Rent.Infrastructure;

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
