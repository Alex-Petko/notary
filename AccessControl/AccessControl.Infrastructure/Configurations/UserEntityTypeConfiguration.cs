using AccessControl.Domain;
using Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.Infrastructure;

internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    private const int PasswordHashLength = 84;

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
            .HasMaxLength(PasswordHashLength);
    }
}