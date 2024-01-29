using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebtManager.Infrastructure;

internal sealed class DebtEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Debt>
{
    public void Configure(EntityTypeBuilder<Domain.Debt> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.BorrowerLogin)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .Property(x => x.LenderLogin)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .Property(x => x.Sum)
            .IsRequired();

        builder
            .Property(x => x.Begin)
            .IsRequired();

        builder
            .Property(x => x.End)
            .IsRequired(false);
    }
}