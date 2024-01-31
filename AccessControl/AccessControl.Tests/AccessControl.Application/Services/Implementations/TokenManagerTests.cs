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
    internal async Task UpdateAsync_Ok_Ok(TokenManagerDto dto, JwtOptions options, CancellationToken cancellationToken, string oldRt)
    {
        // Arrange
        var context = new Context();

        context.JwtOptions.Setup(x => x.Value).Returns(options);
        context.HttpContext.Setup(x => x.Request.Cookies["RT"]).Returns(oldRt);
        context.CommandProvider.Setup(x => x.TryRefreshRT(dto.Login, It.IsAny<string>(), oldRt, context.NowGetService.Object.Now, cancellationToken))
            .ReturnsAsync(RefreshRTResult.Ok);

        // Act
        var result = await context.Manager.RefreshAsync(dto, cancellationToken);

        // Assert;
        Assert.Equal(RefreshResult.Ok, result);

        context.CommandProvider.Verify(x => x.TryRefreshRT(dto.Login, It.IsAny<string>(), oldRt, context.NowGetService.Object.Now, cancellationToken), Times.Once);
        context.CommandProvider.VerifyNoOtherCalls();

        context.HttpContextAccessor.Verify(x => x.HttpContext, Times.Exactly(3));

        context.HttpContext.Verify(x => x.Response, Times.Exactly(2));
        context.HttpContext.Verify(x => x.Request, Times.Once);

        context.RequestCookieCollection.Verify(x => x["RT"], Times.Once);

        context.ResponseCookies.Verify(x => x.Append("JwtBearer", It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once);
        context.ResponseCookies.Verify(x => x.Append("RT", It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once);

        context.ResponseCookies.VerifyNoOtherCalls();
        context.RequestCookieCollection.VerifyNoOtherCalls();

        context.HttpContext.VerifyNoOtherCalls();
        context.HttpContextAccessor.VerifyNoOtherCalls();

        context.NowGetService.Verify(x => x.Now, Times.AtLeastOnce);
        context.NowGetService.VerifyNoOtherCalls();

        context.JwtOptions.Verify(x => x.Value, Times.AtLeastOnce);
        context.JwtOptions.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    internal async Task UpdateAsync_TokenInvalid_TokenInvalid(TokenManagerDto dto, JwtOptions options, CancellationToken cancellationToken, string oldRt)
    {
        // Arrange
        var context = new Context();

        context.JwtOptions.Setup(x => x.Value).Returns(options);
        context.HttpContext.Setup(x => x.Request.Cookies["RT"]).Returns(oldRt);
        context.CommandProvider.Setup(x => x.TryRefreshRT(dto.Login, It.IsAny<string>(), oldRt, context.NowGetService.Object.Now, cancellationToken))
            .ReturnsAsync(RefreshRTResult.TokenInvalid);

        // Act
        var result = await context.Manager.RefreshAsync(dto, cancellationToken);

        // Assert;
        Assert.Equal(RefreshResult.TokenInvalid, result);

        context.CommandProvider.Verify(x => x.TryRefreshRT(dto.Login, It.IsAny<string>(), oldRt, context.NowGetService.Object.Now, cancellationToken), Times.Once);
        context.CommandProvider.VerifyNoOtherCalls();

        context.HttpContextAccessor.Verify(x => x.HttpContext, Times.Once);

        context.HttpContext.Verify(x => x.Request, Times.Once);

        context.RequestCookieCollection.Verify(x => x["RT"], Times.Once);

        context.ResponseCookies.VerifyNoOtherCalls();
        context.RequestCookieCollection.VerifyNoOtherCalls();
        context.HttpContext.VerifyNoOtherCalls();
        context.HttpContextAccessor.VerifyNoOtherCalls();

        context.NowGetService.Verify(x => x.Now, Times.AtLeastOnce);
        context.NowGetService.VerifyNoOtherCalls();

        context.JwtOptions.Verify(x => x.Value, Times.AtLeastOnce);
        context.JwtOptions.VerifyNoOtherCalls();
    }

    private class Context
    {
        private TokenManager? _manager;
        private Mock<ICommandProvider>? _commandProvider;
        private Mock<INowGetService>? _nowGetService;
        private Mock<IHttpContextAccessor>? _httpContextAccessor;
        private Mock<IOptionsSnapshot<JwtOptions>>? _jwtOptions;
        private Mock<IResponseCookies>? _responseCookies;
        private Mock<IRequestCookieCollection>? _requestCookieCollection;
        private Mock<HttpContext>? _httpContext;

        public Mock<ICommandProvider> CommandProvider => _commandProvider ??= new Mock<ICommandProvider>();

        public Mock<INowGetService> NowGetService
        {
            get
            {
                if (_nowGetService == null)
                {
                    _nowGetService = new Mock<INowGetService>();
                    _nowGetService.Setup(x => x.Now).Returns(DateTime.UtcNow);
                }

                return _nowGetService;
            }
        }

        public Mock<IHttpContextAccessor> HttpContextAccessor
        {
            get
            {
                if (_httpContextAccessor == null)
                {
                    _httpContextAccessor = new Mock<IHttpContextAccessor>();
                    _httpContextAccessor.Setup(x => x.HttpContext).Returns(HttpContext.Object);
                }

                return _httpContextAccessor;
            }
        }

        public Mock<IOptionsSnapshot<JwtOptions>> JwtOptions => _jwtOptions ??= new Mock<IOptionsSnapshot<JwtOptions>>();

        public Mock<IResponseCookies> ResponseCookies => _responseCookies ??= new Mock<IResponseCookies>();
        public Mock<IRequestCookieCollection> RequestCookieCollection => _requestCookieCollection ??= new Mock<IRequestCookieCollection>();

        public Mock<HttpContext> HttpContext
        {
            get
            {
                if (_httpContext == null)
                {
                    _httpContext = new Mock<HttpContext>();
                    _httpContext.Setup(x => x.Response.Cookies).Returns(ResponseCookies.Object);
                    _httpContext.Setup(x => x.Request.Cookies).Returns(RequestCookieCollection.Object);
                }

                return _httpContext;
            }
        }

        public TokenManager Manager => _manager ??= new TokenManager(
            CommandProvider.Object,
            NowGetService.Object,
            HttpContextAccessor.Object,
            JwtOptions.Object);
    }

}