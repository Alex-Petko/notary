namespace AuthService.Application;

public interface ITokenGenerator
{
    Task<string?> ExecuteAsync(CreateTokenDto dto, string key, int expiresMinutes);
}