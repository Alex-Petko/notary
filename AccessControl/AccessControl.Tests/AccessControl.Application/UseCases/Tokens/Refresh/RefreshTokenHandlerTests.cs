using AccessControl.Application;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Tests;

namespace Application.UseCases;

public class RefreshTokenHandlerTests
{

    [Theory, CustomAutoData]
    public async Task Handle_Ok_OkResult(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        // Arrange
        var (handler, tokenManager) = Sut();

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.IsType<OkResult>(result);

        tokenManager.Verify(x => x.UpdateAsync(request.Login), Times.Once());
        tokenManager.VerifyNoOtherCalls();
    }

    private (RefreshTokenHandler, Mock<ITokenManager>) Sut()
    {
        var tokenManager = new Mock<ITokenManager>();

        return (
            new RefreshTokenHandler(tokenManager.Object),
            tokenManager
        );
    }
}
