using AuthService.Domain;

namespace AuthService.Application;
public interface ITokenGenerator
{
    Task<string?> ExecuteAsync(CreateTokenDto dto);
}