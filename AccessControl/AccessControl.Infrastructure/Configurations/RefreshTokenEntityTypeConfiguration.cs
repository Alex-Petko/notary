using AccessControl.Domain;
using Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder
            .HasKey(x => x.Login);

        builder
            .Property(x => x.Login)
            .HasMaxLength(Constraints.Login.MaxLength);

        builder
            .HasIndex(x => x.Data)
            .IsUnique();

        builder
           .Property(x => x.Data)
           .HasMaxLength(256);

        builder
            .HasOne(refreshToken => refreshToken.User)
            .WithOne(user => user.RefreshToken)
            .HasForeignKey<RefreshToken>(refreshToken => refreshToken.Login);
 
    }
}