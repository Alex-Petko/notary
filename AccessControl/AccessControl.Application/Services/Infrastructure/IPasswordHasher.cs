using AccessControl.Domain;
using Microsoft.AspNetCore.Identity;

namespace AccessControl.Application;

internal interface IPasswordHasher
{
    public string HashPassword(User user, string password);

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword);
}