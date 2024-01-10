using AccessControl.Domain;
using Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Login);

        builder
            .Property(x => x.Login)
            .HasMaxLength(Constraints.Login.MaxLength);

        builder
            .Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(Constraints.Password.MaxLength);
    }
}