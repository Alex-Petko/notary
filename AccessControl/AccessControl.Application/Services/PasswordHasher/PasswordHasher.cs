using AccessControl.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AccessControl.Application;

internal sealed class PasswordHasher : PasswordHasher<User>, IPasswordHasher
{
    private const string StaticSalt = "UXucgn0gL4Vt";

    public PasswordHasher(IOptions<PasswordHasherOptions>? optionsAccessor = null) : base(optionsAccessor)
    {
    }

    public override string HashPassword(User user, string password)
    {
        return base.HashPassword(user,  password + StaticSalt);
    }

    public override PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
    {
        return base.VerifyHashedPassword(user, hashedPassword, providedPassword + StaticSalt);
    }
}
