using AutoFixture.Xunit2;
using DebtManager.Application;
using Moq;

namespace Application.Handlers;

public class GetAllDebtsQueryHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_Ok_Ok(GetAllDebtsQuery query, CancellationToken cancellationToken, IEnumerable<GetDebtQueryResult> debts)
    {
        // Arrange
        var queryProvider = CreateQueryProvider();

        var handler = CreateHander(queryProvider);

        queryProvider.Setup(x => x.Debts.GetAllAsync<GetDebtQueryResult>(cancellationToken)).ReturnsAsync(debts);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(debts, result);

        queryProvider.Verify(x => x.Debts.GetAllAsync<GetDebtQueryResult>(cancellationToken), Times.Once);
        queryProvider.VerifyNoOtherCalls();
    }

    private GetAllDebtsQueryHandler CreateHander(
        Mock<DebtManager.Application.IQueryProvider> queryProvider)
    {
        return new GetAllDebtsQueryHandler(queryProvider.Object);
    }

    private static Mock<DebtManager.Application.IQueryProvider> CreateQueryProvider()
        => new Mock<DebtManager.Application.IQueryProvider>();
}
