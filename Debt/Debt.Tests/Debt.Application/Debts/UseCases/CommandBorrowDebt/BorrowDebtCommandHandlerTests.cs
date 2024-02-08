using AutoFixture.Xunit2;
using AutoMapper;
using DebtManager.Application;
using DebtManager.Domain;
using Moq;
using System;

namespace Application.Handlers;

public class BorrowDebtCommandHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_Ok_Ok(BorrowDebtCommand command, CancellationToken cancellationToken, Debt debt, User user)
    {
        // Arrange
        var queryProvider = CreateQueryProvider();
        var commandProvider = CreateCommandProvider();
        var mapper = CreateMapper();

        var handler = CreateHander(queryProvider, commandProvider, mapper);

        queryProvider.Setup(x => x.Users.FindAsync(command.Body.Login, cancellationToken)).ReturnsAsync(user);
        mapper.Setup(x => x.Map<Debt>(command)).Returns(debt);

        // Act
        var guid = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotNull(guid);
        Assert.Equal(command.Login, debt.BorrowerLogin);
        Assert.Equal(command.Body.Login, debt.LenderLogin);
        Assert.Equal(DealStatusType.BorrowerApproved, debt.Status);

        queryProvider.Verify(x => x.Users.FindAsync(command.Body.Login, cancellationToken), Times.Once);
        queryProvider.VerifyNoOtherCalls();

        commandProvider.Verify(x => x.AddDebtAsync(debt), Times.Once);
        commandProvider.VerifyNoOtherCalls();

        mapper.Verify(x => x.Map<Debt>(command), Times.Once);
        mapper.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    public async Task User_Null_Null(BorrowDebtCommand command, CancellationToken cancellationToken)
    {
        // Arrange
        var queryProvider = CreateQueryProvider();
        var commandProvider = CreateCommandProvider();
        var mapper = CreateMapper();

        var handler = CreateHander(queryProvider, commandProvider, mapper);

        queryProvider.Setup(x => x.Users.FindAsync(command.Body.Login, cancellationToken)).ReturnsAsync((User?)null);

        // Act
        var guid = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Null(guid);

        queryProvider.Verify(x => x.Users.FindAsync(command.Body.Login, cancellationToken), Times.Once);
        queryProvider.VerifyNoOtherCalls();

        commandProvider.VerifyNoOtherCalls();

        mapper.VerifyNoOtherCalls();
    }

    private BorrowDebtCommandHandler CreateHander(
        Mock<DebtManager.Application.IQueryProvider> queryProvider,
        Mock<ICommandProvider> commandProvider,
        Mock<IMapper> mapper)
    {
        return new BorrowDebtCommandHandler(queryProvider.Object, commandProvider.Object, mapper.Object);
    }

    private static Mock<DebtManager.Application.IQueryProvider> CreateQueryProvider()
        => new Mock<DebtManager.Application.IQueryProvider>();

    private static Mock<ICommandProvider> CreateCommandProvider()
        => new Mock<ICommandProvider>();

    private static Mock<IMapper> CreateMapper()
        => new Mock<IMapper>();
}
