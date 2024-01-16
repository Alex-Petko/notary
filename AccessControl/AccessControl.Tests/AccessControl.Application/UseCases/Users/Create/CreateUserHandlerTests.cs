using AccessControl.Application;
using AccessControl.Domain;
using AccessControl.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Tests;

namespace Application.UseCases;

public class CreateUserHandlerTests
{
    [Theory, CustomAutoData]
    public async Task Handle_FailCreateResult_ConflictResult(CreateUserRequest request, CancellationToken cancellationToken, User user)
    {
        // Arrange
        var (handler, transactions, mapper) = Sut();
        transactions.Setup(x => x.TryCreateAsync(user)).ReturnsAsync(false);
        mapper.Setup(x => x.Map<User>(request)).Returns(user);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.IsType<ConflictResult>(result);

        transactions.Verify(x => x.TryCreateAsync(user), Times.Once());
        transactions.VerifyNoOtherCalls();

        mapper.Verify(x => x.Map<User>(request), Times.Once());
        mapper.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Handle_Ok_OkResult(CreateUserRequest request, CancellationToken cancellationToken, User user)
    {
        // Arrange
        var (handler, transactions, mapper) = Sut();
        transactions.Setup(x => x.TryCreateAsync(user)).ReturnsAsync(true);
        mapper.Setup(x => x.Map<User>(request)).Returns(user);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.IsType<OkResult>(result);

        transactions.Verify(x => x.TryCreateAsync(user), Times.Once());
        transactions.VerifyNoOtherCalls();

        mapper.Verify(x => x.Map<User>(request), Times.Once());
        mapper.VerifyNoOtherCalls();
    }

    private (CreateUserHandler, Mock<ITransactions>, Mock<IMapper>) Sut()
    {
        var transactions = new Mock<ITransactions>();
        var mapper = new Mock<IMapper>();

        return (
            new CreateUserHandler(transactions.Object, mapper.Object),
            transactions,
            mapper
        );
    }
}