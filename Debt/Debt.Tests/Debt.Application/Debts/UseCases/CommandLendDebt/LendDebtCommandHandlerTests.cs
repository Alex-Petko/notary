using AutoFixture.Xunit2;
using AutoMapper;
using DebtManager.Application;
using DebtManager.Domain;
using Moq;

namespace Application.Handlers;

public class LendDebtCommandHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_Ok_Ok(LendDebtCommand command, CancellationToken cancellationToken, Debt debt, User user)
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
        Assert.Equal(command.Body.Login, debt.BorrowerLogin);
        Assert.Equal(command.Login, debt.LenderLogin);
        Assert.Equal(DealStatusType.LenderApproved, debt.Status);

        commandProvider.Verify(x => x.AddDebtAsync(debt), Times.Once);
        commandProvider.VerifyNoOtherCalls();

        mapper.Verify(x => x.Map<Debt>(command), Times.Once);
        mapper.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    public async Task User_Null_Null(LendDebtCommand command, CancellationToken cancellationToken)
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

    private LendDebtCommandHandler CreateHander(
        Mock<DebtManager.Application.IQueryProvider> queryProvider,
        Mock<ICommandProvider> commandProvider,
        Mock<IMapper> mapper)
    {
        return new LendDebtCommandHandler(queryProvider.Object, commandProvider.Object, mapper.Object);
    }

    private static Mock<DebtManager.Application.IQueryProvider> CreateQueryProvider()
        => new Mock<DebtManager.Application.IQueryProvider>();

    private static Mock<ICommandProvider> CreateCommandProvider()
        => new Mock<ICommandProvider>();

    private static Mock<IMapper> CreateMapper()
        => new Mock<IMapper>();
}
