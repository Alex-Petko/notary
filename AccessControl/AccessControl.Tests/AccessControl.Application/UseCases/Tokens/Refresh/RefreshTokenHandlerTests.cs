using AccessControl.Application;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Tests;

namespace Application.UseCases;

public class RefreshTokenHandlerTests
{
    [Theory, CustomAutoData]
    internal async Task Handle_Ok_OkResult(RefreshTokenCommand command, CancellationToken cancellationToken, TokenManagerDto tokenManagerDto)
    {
        // Arrange
        var (handler, tokenManager, mapper) = Sut();
        mapper.Setup(x => x.Map<TokenManagerDto>(command)).Returns(tokenManagerDto);

        // Act
        await handler.Handle(command, cancellationToken);

        // Assert
        mapper.Verify(x => x.Map<TokenManagerDto>(command), Times.Once);
        mapper.VerifyNoOtherCalls();

        tokenManager.Verify(x => x.UpdateAsync(tokenManagerDto, cancellationToken), Times.Once);
        tokenManager.VerifyNoOtherCalls();
    }

    private static (RefreshTokenCommandHandler, Mock<ITokenManager>, Mock<IMapper>) Sut()
    {
        var tokenManager = new Mock<ITokenManager>();
        var mapper = new Mock<IMapper>();

        return (
            new RefreshTokenCommandHandler(tokenManager.Object, mapper.Object),
            tokenManager,
            mapper
        );
    }
}