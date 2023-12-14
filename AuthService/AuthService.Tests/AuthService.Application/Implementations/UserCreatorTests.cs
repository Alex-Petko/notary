using AuthService.Application;
using AuthService.Domain;
using AutoMapper;
using Moq;

namespace AuthService.Tests;

public class UserCreatorTests
{
    [Fact]
    public async Task ExecuteAsync_True()
    {
        // Arrange
        var dto = new CreateUserDto("1", "2");
        var user = new User()
        {
            Login = dto.Login,
            PasswordHash = dto.PasswordHash
        };

        var transactions = new Mock<ITransactions>();
        transactions.Setup(x => x.TryCreateAsync(user)).ReturnsAsync(true);

        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<User>(dto)).Returns(user);

        var userCreator = new UserCreator(
            transactions.Object,
            mapper.Object);

        // Act
        var result = await userCreator.ExecuteAsync(dto);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExecuteAsync_False()
    {
        // Arrange
        var dto = new CreateUserDto("1", "2");
        var user = new User()
        {
            Login = dto.Login,
            PasswordHash = dto.PasswordHash
        };

        var transactions = new Mock<ITransactions>();
        transactions.Setup(x => x.TryCreateAsync(user)).ReturnsAsync(false);

        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<User>(dto)).Returns(user);

        var userCreator = new UserCreator(
            transactions.Object,
            mapper.Object);

        // Act
        var result = await userCreator.ExecuteAsync(dto);

        // Assert
        Assert.False(result);
    }
}
