﻿using AutoFixture.Xunit2;
using AutoMapper;
using DebtManager.Application;
using DebtManager.Domain;
using Moq;

namespace Application.Handlers;

public class BorrowDebtCommandHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_Ok_Ok(BorrowDebtCommand command, CancellationToken cancellationToken, Debt debt)
    {
        // Arrange
        var commandProvider = CreateCommandProvider();
        var mapper = CreateMapper();

        var handler = CreateHander(commandProvider, mapper);

        mapper.Setup(x => x.Map<Debt>(command)).Returns(debt);

        // Act
        _ = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(command.Login, debt.BorrowerLogin);
        Assert.Equal(command.Body.Login, debt.LenderLogin);
        Assert.Equal(DealStatusType.BorrowerApproved, debt.Status);

        commandProvider.Verify(x => x.AddDebtAsync(debt), Times.Once);
        commandProvider.VerifyNoOtherCalls();

        mapper.Verify(x => x.Map<Debt>(command), Times.Once);
        mapper.VerifyNoOtherCalls();
    }


    private BorrowDebtCommandHandler CreateHander(
        Mock<ICommandProvider>? commandProvider = null,
        Mock<IMapper>? mapper = null)
    {
        commandProvider ??= CreateCommandProvider();
        mapper ??= CreateMapper();
        return new BorrowDebtCommandHandler(commandProvider.Object, mapper.Object);
    }

    private static Mock<ICommandProvider> CreateCommandProvider()
        => new Mock<ICommandProvider>();

    private static Mock<IMapper> CreateMapper()
        => new Mock<IMapper>();
}
