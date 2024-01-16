using AccessControl.Application;
using AccessControl.Domain;
using AccessControl.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Shared.Tests;

namespace Application.Services;

public class TokenManagerTests
{
    [Theory, CustomAutoData]
    public async Task UpdateAsync_RTExist_Ok(string login, RefreshToken refreshToken, JwtOptions options)
    {
        // Arrange
        var (manager, unitOfWork, dateTimeProvider, httpContextAccessor, jwtOptions, cookie, httpContext) = Sut();

        unitOfWork.Setup(x => x.RefreshTokens.FindAsync(login)).ReturnsAsync(refreshToken);
        jwtOptions.Setup(x => x.Value).Returns(options);

        // Act
        await manager.UpdateAsync(login);

        // Assert;
        unitOfWork.Verify(x => x.RefreshTokens.FindAsync(login), Times.Once);
        unitOfWork.Verify(x => x.SaveChangesAsync(default), Times.Once);
        unitOfWork.Verify(x => x.RefreshTokens.Add(It.IsAny<RefreshToken>()), Times.Never);
        unitOfWork.VerifyNoOtherCalls();

        httpContextAccessor.Verify(x => x.HttpContext, Times.Exactly(2));

        httpContext.Verify(x => x.Response, Times.Exactly(2));

        cookie.Verify(x => x.Append("JwtBearer", It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once);
        cookie.Verify(x => x.Append("RT", It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once);

        cookie.VerifyNoOtherCalls();
        httpContext.VerifyNoOtherCalls();
        httpContextAccessor.VerifyNoOtherCalls();

        dateTimeProvider.Verify(x => x.UtcNow, Times.AtLeastOnce);
        dateTimeProvider.VerifyNoOtherCalls();

        jwtOptions.Verify(x => x.Value, Times.AtLeastOnce);
        jwtOptions.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task UpdateAsync_RTNotExist_Ok(string login, JwtOptions options)
    {
        // Arrange
        var (manager, unitOfWork, dateTimeProvider, httpContextAccessor, jwtOptions, cookie, httpContext) = Sut();

        unitOfWork.Setup(x => x.RefreshTokens.FindAsync(login)).ReturnsAsync((RefreshToken?)null);
        jwtOptions.Setup(x => x.Value).Returns(options);

        // Act
        await manager.UpdateAsync(login);

        // Assert;
        unitOfWork.Verify(x => x.RefreshTokens.FindAsync(login), Times.Once);
        unitOfWork.Verify(x => x.SaveChangesAsync(default), Times.Once);
        unitOfWork.Verify(x => x.RefreshTokens.Add(It.IsAny<RefreshToken>()), Times.Once);
        unitOfWork.VerifyNoOtherCalls();

        httpContextAccessor.Verify(x => x.HttpContext, Times.Exactly(2));

        httpContext.Verify(x => x.Response, Times.Exactly(2));

        cookie.Verify(x => x.Append("JwtBearer", It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once);
        cookie.Verify(x => x.Append("RT", It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once);

        cookie.VerifyNoOtherCalls();
        httpContext.VerifyNoOtherCalls();
        httpContextAccessor.VerifyNoOtherCalls();

        dateTimeProvider.Verify(x => x.UtcNow, Times.AtLeastOnce);
        dateTimeProvider.VerifyNoOtherCalls();

        jwtOptions.Verify(x => x.Value, Times.AtLeastOnce);
        jwtOptions.VerifyNoOtherCalls();
    }

    private (
        TokenManager,
        Mock<IUnitOfWork>,
        Mock<IDateTimeProvider>,
        Mock<IHttpContextAccessor>,
        Mock<IOptionsSnapshot<JwtOptions>>,
        Mock<IResponseCookies>,
        Mock<HttpContext>
        )
            Sut()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        var jwtOptions = new Mock<IOptionsSnapshot<JwtOptions>>();

        dateTimeProvider.Setup(x => x.UtcNow).Returns(DateTime.UtcNow);

        var httpContext = new Mock<HttpContext>();
        var cookie = new Mock<IResponseCookies>();
        httpContext.Setup(x => x.Response.Cookies).Returns(cookie.Object);
        httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext.Object);

        return (
            new TokenManager(unitOfWork.Object, dateTimeProvider.Object, httpContextAccessor.Object, jwtOptions.Object),
            unitOfWork,
            dateTimeProvider,
            httpContextAccessor,
            jwtOptions,
            cookie,
            httpContext);
    }
}