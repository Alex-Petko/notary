using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rent.Domain;

namespace Rent.Infrastructure;

internal sealed class FileDescriptionConfiguration : IEntityTypeConfiguration<FileDescription>
{
    public void Configure(EntityTypeBuilder<FileDescription> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Path)
            .IsRequired()
            .HasMaxLength(50 + "/TemplateDescriptions/".Length);
    }
}