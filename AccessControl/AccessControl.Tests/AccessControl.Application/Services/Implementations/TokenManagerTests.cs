using AccessControl.Application;
using AccessControl.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Shared.Tests;

namespace Application.Services;

public class TokenManagerTests
{
    [Theory, CustomAutoData]
    internal async Task UpdateAsync_RTExist_Ok(TokenManagerDto dto, JwtOptions options, CancellationToken cancellationToken)
    {
        // Arrange
        var (manager, commandProvider, nowGetService, httpContextAccessor, jwtOptions, cookie, httpContext) = Sut();

        jwtOptions.Setup(x => x.Value).Returns(options);

        // Act
        await manager.UpdateAsync(dto, cancellationToken);

        // Assert;
        commandProvider.Verify(x => x.UpdateRefreshToken(dto.Login, It.IsAny<string>(), nowGetService.Object.Now, cancellationToken), Times.Once);
        commandProvider.VerifyNoOtherCalls();

        httpContextAccessor.Verify(x => x.HttpContext, Times.Exactly(2));

        httpContext.Verify(x => x.Response, Times.Exactly(2));

        cookie.Verify(x => x.Append("JwtBearer", It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once);
        cookie.Verify(x => x.Append("RT", It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once);

        cookie.VerifyNoOtherCalls();
        httpContext.VerifyNoOtherCalls();
        httpContextAccessor.VerifyNoOtherCalls();

        nowGetService.Verify(x => x.Now, Times.AtLeastOnce);
        nowGetService.VerifyNoOtherCalls();

        jwtOptions.Verify(x => x.Value, Times.AtLeastOnce);
        jwtOptions.VerifyNoOtherCalls();
    }

    private (
        TokenManager,
        Mock<ICommandProvider>,
        Mock<INowGetService>,
        Mock<IHttpContextAccessor>,
        Mock<IOptionsSnapshot<JwtOptions>>,
        Mock<IResponseCookies>,
        Mock<HttpContext>
        )
            Sut()
    {
        var commandProvider = new Mock<ICommandProvider>();
        var dateTimeProvider = new Mock<INowGetService>();
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        var jwtOptions = new Mock<IOptionsSnapshot<JwtOptions>>();

        dateTimeProvider.Setup(x => x.Now).Returns(DateTime.UtcNow);

        var httpContext = new Mock<HttpContext>();
        var cookie = new Mock<IResponseCookies>();
        httpContext.Setup(x => x.Response.Cookies).Returns(cookie.Object);
        httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext.Object);

        return (
            new TokenManager(commandProvider.Object, dateTimeProvider.Object, httpContextAccessor.Object, jwtOptions.Object),
            commandProvider,
            dateTimeProvider,
            httpContextAccessor,
            jwtOptions,
            cookie,
            httpContext);
    }
}