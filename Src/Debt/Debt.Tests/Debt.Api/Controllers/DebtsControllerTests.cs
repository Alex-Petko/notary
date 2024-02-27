using DebtManager.Api;
using DebtManager.Application;
using DebtManager.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Tests;

namespace Api.Controllers;

public class DebtsControllerTests
{
    [Theory, CustomAutoData]
    public async Task GetAll_OkObjectResult(List<GetDebtQueryResult> result, GetAllDebtsQuery query, CancellationToken cancellationToken)
    {
        // Arrange
        var mediator = CreateMediator();
        var controller = CreateController(mediator);

        mediator.Setup(x => x.Send(query, cancellationToken)).ReturnsAsync(result);

        // Act
        var actionResult = await controller.GetAllDebtsQuery(cancellationToken);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(result, okObjectResult.Value);

        mediator.Verify(x => x.Send(query, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Get_OkObjectResult(GetDebtQueryResult response, GetDebtQuery query, CancellationToken cancellationToken)
    {
        // Arrange
        var mediator = CreateMediator();
        var controller = CreateController(mediator);

        mediator.Setup(x => x.Send(query, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.GetDebtQuery(query, cancellationToken);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultDto = Assert.IsType<GetDebtQueryResult>(okObjectResult.Value);

        Assert.Equal(response, resultDto);

        mediator.Verify(x => x.Send(query, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Get_NotFoundResult(GetDebtQuery query, CancellationToken cancellationToken)
    {
        // Arrange
        var mediator = CreateMediator();
        var controller = CreateController(mediator);

        mediator.Setup(x => x.Send(query, cancellationToken)).ReturnsAsync((GetDebtQueryResult?)null);
        // Act
        var actionResult = await controller.GetDebtQuery(query, cancellationToken);

        //Assert
        Assert.IsType<NotFoundResult>(actionResult);

        mediator.Verify(x => x.Send(query, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Lend_CreatedResult(LendDebtCommand command, CancellationToken cancellationToken, Guid response)
    {
        // Arrange
        var mediator = CreateMediator();
        var controller = CreateController(mediator);

        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.LendDebtCommand(command, cancellationToken);

        //Assert
        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        var createdGuid = Assert.IsType<Guid>(createdResult.Value);
        Assert.Equal(response, createdGuid);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Borrow_CreatedResult(BorrowDebtCommand command, CancellationToken cancellationToken, Guid response)
    {
        // Arrange
        var mediator = CreateMediator();
        var controller = CreateController(mediator);

        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.BorrowDebtCommand(command, cancellationToken);

        //Assert
        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        var createdGuid = Assert.IsType<Guid>(createdResult.Value);

        Assert.Equal(response, createdGuid);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Accept_OkObjectResult(AcceptDebtCommand command, CancellationToken cancellationToken, DealStatusType response)
    {
        // Arrange
        var mediator = CreateMediator();
        var controller = CreateController(mediator);

        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.AcceptDebtCommand(command, cancellationToken);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultStatus = Assert.IsType<DealStatusType>(okObjectResult.Value);

        Assert.Equal(response, resultStatus);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Accept_BadRequestResult(AcceptDebtCommand command, CancellationToken cancellationToken)
    {
        // Arrange
        var mediator = CreateMediator();
        var controller = CreateController(mediator);

        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync((DealStatusType?)null);

        // Act
        var actionResult = await controller.AcceptDebtCommand(command, cancellationToken);

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Cancel_OkObjectResult(CancelDebtCommand command, CancellationToken cancellationToken, DealStatusType response)
    {
        // Arrange
        var mediator = CreateMediator();
        var controller = CreateController(mediator);

        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.CancelDebtCommand(command, cancellationToken);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultStatus = Assert.IsType<DealStatusType>(okObjectResult.Value);

        Assert.Equal(response, resultStatus);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Cancel_BadRequestResult(CancelDebtCommand command, CancellationToken cancellationToken)
    {
        // Arrange
        var mediator = CreateMediator();
        var controller = CreateController(mediator);

        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync((DealStatusType?)null);

        // Act
        var actionResult = await controller.CancelDebtCommand(command, cancellationToken);

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Close_OkObjectResult(CloseDebtCommand command, CancellationToken cancellationToken, DealStatusType response)
    {
        // Arrange
        var mediator = CreateMediator();
        var controller = CreateController(mediator);

        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.CloseDebtCommand(command, cancellationToken);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultStatus = Assert.IsType<DealStatusType>(okObjectResult.Value);

        Assert.Equal(response, resultStatus);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Close_BadRequestResult(CloseDebtCommand command, CancellationToken cancellationToken)
    {
        // Arrange
        var mediator = CreateMediator();
        var controller = CreateController(mediator);

        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync((DealStatusType?)null);

        // Act
        var actionResult = await controller.CloseDebtCommand(command, cancellationToken);

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    private static DebtsController CreateController(Mock<IMediator> mediator)
    {
        var controller = new DebtsController(mediator.Object);
        controller.ControllerContext.HttpContext = new DefaultHttpContext();
        return controller;
    }

    private static Mock<IMediator> CreateMediator()
        => new Mock<IMediator>();
}