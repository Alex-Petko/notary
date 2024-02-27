using AutoFixture.Xunit2;
using DebtManager.Application;
using DebtManager.Domain;
using Moq;

namespace Application.Handlers;

public class AcceptDebtCommandHandlerTests
{
    [Theory, AutoData]
    [InlineAutoData(DealStatusType.BorrowerApproved)]
    [InlineAutoData(DealStatusType.Opened)]
    [InlineAutoData(DealStatusType.LenderCanceled)]
    [InlineAutoData(DealStatusType.BorrowerCanceled)]
    [InlineAutoData(DealStatusType.Canceled)]
    [InlineAutoData(DealStatusType.LenderClosed)]
    [InlineAutoData(DealStatusType.BorrowerClosed)]
    [InlineAutoData(DealStatusType.Closed)]
    public async Task Borrower_IncorrectDebtStatus_NotChanges(DealStatusType status, AcceptDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        if (status == DealStatusType.LenderApproved)
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
    public async Task Borrower_LenderApproved_Opened(AcceptDebtCommand command, CancellationToken cancellationToken, Debt debt)
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
        Assert.Equal(DealStatusType.Opened, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    [InlineAutoData(DealStatusType.LenderApproved)]
    [InlineAutoData(DealStatusType.Opened)]
    [InlineAutoData(DealStatusType.LenderCanceled)]
    [InlineAutoData(DealStatusType.BorrowerCanceled)]
    [InlineAutoData(DealStatusType.Canceled)]
    [InlineAutoData(DealStatusType.LenderClosed)]
    [InlineAutoData(DealStatusType.BorrowerClosed)]
    [InlineAutoData(DealStatusType.Closed)]
    public async Task Lender_IncorrectDebtStatus_NotChanges(DealStatusType status, AcceptDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        if (status == DealStatusType.BorrowerApproved)
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
    public async Task Lender_BorrowerApproved_Opened(AcceptDebtCommand command, CancellationToken cancellationToken, Debt debt)
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
        Assert.Equal(DealStatusType.Opened, result);

        commandProvider.Verify(x => x.Debts.FindAsync(command.DebtId), Times.Once);
        commandProvider.Verify(x => x.SaveChangesAsync(), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    private AcceptDebtCommandHandler CreateHander(
        Mock<ICommandProvider>? commandProvider = null)
    {
        commandProvider ??= CreateCommandProvider();
        return new AcceptDebtCommandHandler(commandProvider.Object);
    }

    private static Mock<ICommandProvider> CreateCommandProvider()
        => new Mock<ICommandProvider>();
}
