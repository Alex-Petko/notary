namespace AuthService.Application;

public interface IUserCreator
{
    Task<bool> ExecuteAsync(CreateUserDto dto);
}