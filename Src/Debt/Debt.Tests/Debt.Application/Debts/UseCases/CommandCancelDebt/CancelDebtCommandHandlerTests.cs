using AutoFixture.Xunit2;
using DebtManager.Application;
using DebtManager.Domain;
using Moq;

namespace Application.Handlers;

public class CancelDebtCommandHandlerTests
{
    [Theory, AutoData]
    [InlineAutoData(DealStatusType.Opened)]
    [InlineAutoData(DealStatusType.BorrowerCanceled)]
    [InlineAutoData(DealStatusType.Canceled)]
    [InlineAutoData(DealStatusType.LenderClosed)]
    [InlineAutoData(DealStatusType.BorrowerClosed)]
    [InlineAutoData(DealStatusType.Closed)]
    public async Task Borrower_IncorrectDebtStatus_NotChanges(DealStatusType status, CancelDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        if (
            status == DealStatusType.LenderApproved ||
            status == DealStatusType.BorrowerApproved ||
            status == DealStatusType.LenderCanceled
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
    public async Task Borrower_LenderApproved_BorrowerCanceled(CancelDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.BorrowerLogin = command.Login;
        debt.Status = DealStatusType.LenderApproved;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(DealStatusType.BorrowerCanceled, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    public async Task Borrower_BorrowerApproved_BorrowerCanceled(CancelDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.BorrowerLogin = command.Login;
        debt.Status = DealStatusType.BorrowerApproved;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(DealStatusType.BorrowerCanceled, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    public async Task Borrower_LenderCanceled_BorrowerCanceled(CancelDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.BorrowerLogin = command.Login;
        debt.Status = DealStatusType.LenderCanceled;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(DealStatusType.Canceled, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    [InlineAutoData(DealStatusType.Opened)]
    [InlineAutoData(DealStatusType.LenderCanceled)]
    [InlineAutoData(DealStatusType.Canceled)]
    [InlineAutoData(DealStatusType.LenderClosed)]
    [InlineAutoData(DealStatusType.BorrowerClosed)]
    [InlineAutoData(DealStatusType.Closed)]
    public async Task Lender_IncorrectDebtStatus_NotChanges(DealStatusType status, CancelDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        if (
            status == DealStatusType.LenderApproved ||
            status == DealStatusType.BorrowerApproved ||
            status == DealStatusType.BorrowerCanceled
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
    public async Task Lender_LenderApproved_LenderCanceled(CancelDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.LenderLogin = command.Login;
        debt.Status = DealStatusType.LenderApproved;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(DealStatusType.LenderCanceled, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    public async Task Lender_BorrowerApproved_LenderCanceled(CancelDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.LenderLogin = command.Login;
        debt.Status = DealStatusType.BorrowerApproved;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(DealStatusType.LenderCanceled, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    public async Task Lender_BorrowerCanceled_Canceled(CancelDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        // Arrange
        var commandProvider = CreateCommandProvider();

        var handler = CreateHander(commandProvider);

        commandProvider.Setup(x => x.Debts.FindAsync(command.DebtId)).ReturnsAsync(debt);

        debt.LenderLogin = command.Login;
        debt.Status = DealStatusType.BorrowerCanceled;

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(debt.Status, result);
        Assert.Equal(DealStatusType.Canceled, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    private CancelDebtCommandHandler CreateHander(
        Mock<ICommandProvider> commandProvider)
    {
        return new CancelDebtCommandHandler(commandProvider.Object);
    }

    private static Mock<ICommandProvider> CreateCommandProvider()
        => new Mock<ICommandProvider>();
}
