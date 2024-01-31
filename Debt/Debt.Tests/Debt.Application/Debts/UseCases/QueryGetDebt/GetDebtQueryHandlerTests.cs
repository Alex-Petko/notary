using AutoFixture.Xunit2;
using AutoMapper;
using DebtManager.Application;
using DebtManager.Domain;
using Moq;

namespace Application.Handlers;

public class GetDebtQueryHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_Ok_Ok(GetDebtQuery query, CancellationToken cancellationToken, Debt debt, GetDebtQueryResult getDebtQueryResult)
    {
        // Arrange
        var queryProvider = CreateQueryProvider();
        var mapper = CreateMapper();

        var handler = CreateHander(queryProvider, mapper);

        queryProvider.Setup(x => x.Debts.FindAsync(query.DebtId)).ReturnsAsync(debt);
        mapper.Setup(x => x.Map<GetDebtQueryResult>(debt)).Returns(getDebtQueryResult);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(getDebtQueryResult, result);

        queryProvider.Verify(x => x.Debts.FindAsync(query.DebtId), Times.Once);
        queryProvider.VerifyNoOtherCalls();

        mapper.Verify(x => x.Map<GetDebtQueryResult>(debt), Times.Once);
        mapper.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    public async Task Debt_NotFound_Null(GetDebtQuery query, CancellationToken cancellationToken)
    {
        // Arrange
        var queryProvider = CreateQueryProvider();
        var mapper = CreateMapper();

        var handler = CreateHander(queryProvider, mapper);

        queryProvider.Setup(x => x.Debts.FindAsync(query.DebtId)).ReturnsAsync((Debt?)null);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.Null(result);

        queryProvider.Verify(x => x.Debts.FindAsync(query.DebtId), Times.Once);
        queryProvider.VerifyNoOtherCalls();

        mapper.VerifyNoOtherCalls();
    }

    private GetDebtQueryHandler CreateHander(
        Mock<DebtManager.Application.IQueryProvider> queryProvider, 
        Mock<IMapper> mapper)
    {
        return new GetDebtQueryHandler(queryProvider.Object, mapper.Object);
    }

    private static Mock<DebtManager.Application.IQueryProvider> CreateQueryProvider() 
        => new Mock<DebtManager.Application.IQueryProvider>();

    private static Mock<IMapper> CreateMapper()
        => new Mock<IMapper>();
}
