using AutoFixture.Xunit2;
using DebtManager.Application;
using DebtManager.Domain;
using Moq;

namespace Application.Handlers;

public class CloseDebtCommandHandlerTests
{
    [Theory, AutoData]
    [InlineAutoData(DealStatusType.LenderApproved)]
    [InlineAutoData(DealStatusType.BorrowerApproved)]
    [InlineAutoData(DealStatusType.LenderCanceled)]
    [InlineAutoData(DealStatusType.BorrowerCanceled)]
    [InlineAutoData(DealStatusType.Canceled)]
    [InlineAutoData(DealStatusType.BorrowerClosed)]
    [InlineAutoData(DealStatusType.Closed)]
    public async Task Borrower_IncorrectDebtStatus_NotChanges(DealStatusType status, CloseDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        if (
            status == DealStatusType.Opened ||
            status == DealStatusType.LenderClosed
            )
            return;

        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.BorrowerLogin = command.Login;
        debt.Status = status;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(status, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    public async Task Borrower_Opened_BorrowerClosed(CloseDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.BorrowerLogin = command.Login;
        debt.Status = DealStatusType.Opened;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(DealStatusType.BorrowerClosed, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    public async Task Borrower_LenderClosed_Closed(CloseDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.BorrowerLogin = command.Login;
        debt.Status = DealStatusType.LenderClosed;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(DealStatusType.Closed, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    [InlineAutoData(DealStatusType.LenderApproved)]
    [InlineAutoData(DealStatusType.BorrowerApproved)]
    [InlineAutoData(DealStatusType.LenderCanceled)]
    [InlineAutoData(DealStatusType.BorrowerCanceled)]
    [InlineAutoData(DealStatusType.Canceled)]
    [InlineAutoData(DealStatusType.LenderClosed)]
    [InlineAutoData(DealStatusType.Closed)]
    public async Task Lender_IncorrectDebtStatus_NotChanges(DealStatusType status, CloseDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        if (
            status == DealStatusType.Opened ||
            status == DealStatusType.BorrowerClosed
            )
            return;

        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.LenderLogin = command.Login;
        debt.Status = status;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(status, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    public async Task Lender_Opened_LenderClosed(CloseDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.LenderLogin = command.Login;
        debt.Status = DealStatusType.Opened;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(DealStatusType.LenderClosed, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    public async Task Lender_BorrowerClosed_Closed(CloseDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.LenderLogin = command.Login;
        debt.Status = DealStatusType.BorrowerClosed;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(DealStatusType.Closed, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    private CloseDebtCommandHandler CreateHander(
        Mock<ICommandProvider> commandProvider)
    {
        return new CloseDebtCommandHandler(commandProvider.Object);
    }

    private static Mock<ICommandProvider> CreateCommandProvider()
        => new Mock<ICommandProvider>();
}
