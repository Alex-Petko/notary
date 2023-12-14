using AuthService.Application;
using AuthService.Domain;
using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace AuthService.Tests;

public class TokenGeneratorTests
{
    [Fact]
    public async Task ExecuteAsync_JwtToken()
    {
        // Arrange
        var dto = new CreateTokenDto("1", "2");
        var user = new User()
        {
            Login = dto.Login,
            PasswordHash = dto.PasswordHash
        };

        var transactions = new Mock<ITransactions>();
        transactions.Setup(x => x.ContainsAsync(user)).ReturnsAsync(true);

        var options = new Mock<IOptionsSnapshot<JwtOptions>>();
        options.SetupGet(x => x.Value.ExpiresMinutes).Returns(1);
        options.Setup(x => x.Value.Key).Returns(new string('x', 16));

        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<User>(dto)).Returns(user);

        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(x => x.UtcNow).Returns(DateTime.UnixEpoch);

        var tokenGenerator = new TokenGenerator(
            transactions.Object,
            options.Object,
            mapper.Object,
            dateTimeProvider.Object);

        var validTo = dateTimeProvider.Object.UtcNow.AddMinutes(1);

        // Act
        var token = await tokenGenerator.ExecuteAsync(dto);

        // Assert

        var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        Assert.NotNull(securityToken);

        var subClaim = securityToken.Claims.Where(x => x.Type == "sub").SingleOrDefault();
        Assert.NotNull(subClaim);
        Assert.Equal("1", subClaim.Value);

        var expClaim = securityToken.Claims.Where(x => x.Type == "exp").SingleOrDefault();
        Assert.NotNull(expClaim);
        Assert.Equal((1 * 60).ToString(), expClaim.Value);

        Assert.Equal(validTo, securityToken.ValidTo);
    }

    [Fact]
    public async Task ExecuteAsync_Null()
    {
        // Arrange
        var dto = new CreateTokenDto("1", "2");

        var mapper = new Mock<IMapper>();
        var options = new Mock<IOptionsSnapshot<JwtOptions>>();
        var dateTimeProvider = new Mock<IDateTimeProvider>();

        var transactions = new Mock<ITransactions>();
        transactions.Setup(x => x.ContainsAsync(It.IsAny<User>())).ReturnsAsync(false);

        var tokenGenerator = new TokenGenerator(
            transactions.Object,
            options.Object,
            mapper.Object,
            dateTimeProvider.Object);

        // Act
        var token = await tokenGenerator.ExecuteAsync(dto);

        // Assert
        Assert.Null(token);
    }
}