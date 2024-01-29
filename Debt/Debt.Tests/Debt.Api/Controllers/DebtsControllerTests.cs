using DebtManager.Api;
using DebtManager.Application;
using DebtManager.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Tests;

namespace DebtManager.Tests;

public class DebtsControllerTests
{
    [Theory, CustomAutoData]
    public async Task GetAll_OkObjectResult(List<GetDebtDto> result, GetAllDebtsRequest query, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(query, cancellationToken)).ReturnsAsync(result);

        // Act
        var actionResult = await controller.GetAll(cancellationToken);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(result, okObjectResult.Value);

        mediator.Verify(x => x.Send(query, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Get_OkObjectResult(GetDebtDto response, GetDebtRequest query, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(query, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.Get(query, cancellationToken);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultDto = Assert.IsType<GetDebtDto>(okObjectResult.Value);

        Assert.Equal(response, resultDto);

        mediator.Verify(x => x.Send(query, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Get_NotFoundResult(GetDebtRequest query, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(query, cancellationToken)).ReturnsAsync((GetDebtDto?)null);
        // Act
        var actionResult = await controller.Get(query, cancellationToken);

        //Assert
        Assert.IsType<NotFoundResult>(actionResult);

        mediator.Verify(x => x.Send(query, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Lend_CreatedResult(LendDebtRequest command, CancellationToken cancellationToken, Guid response)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.Lend(command, cancellationToken);

        //Assert
        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        var createdGuid = Assert.IsType<Guid>(createdResult.Value);
        Assert.Equal(response, createdGuid);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Borrow_CreatedResult(BorrowDebtRequest command, CancellationToken cancellationToken, Guid response)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.Borrow(command, cancellationToken);

        //Assert
        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        var createdGuid = Assert.IsType<Guid>(createdResult.Value);

        Assert.Equal(response, createdGuid);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Accept_OkObjectResult(AcceptDebtRequest command, CancellationToken cancellationToken, DealStatusType response)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.Accept(command, cancellationToken);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultStatus = Assert.IsType<DealStatusType>(okObjectResult.Value);

        Assert.Equal(response, resultStatus);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Accept_BadRequestResult(AcceptDebtRequest command, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync((DealStatusType?)null);

        // Act
        var actionResult = await controller.Accept(command, cancellationToken);

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Cancel_OkObjectResult(CancelDebtRequest command, CancellationToken cancellationToken, DealStatusType response)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.Cancel(command, cancellationToken);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultStatus = Assert.IsType<DealStatusType>(okObjectResult.Value);

        Assert.Equal(response, resultStatus);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Cancel_BadRequestResult(CancelDebtRequest command, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync((DealStatusType?)null);

        // Act
        var actionResult = await controller.Cancel(command, cancellationToken);

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Close_OkObjectResult(CloseDebtRequest command, CancellationToken cancellationToken, DealStatusType response)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(response);

        // Act
        var actionResult = await controller.Close(command, cancellationToken);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultStatus = Assert.IsType<DealStatusType>(okObjectResult.Value);

        Assert.Equal(response, resultStatus);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Close_BadRequestResult(CloseDebtRequest command, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync((DealStatusType?)null);

        // Act
        var actionResult = await controller.Close(command, cancellationToken);

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    private static (DebtsController, Mock<IMediator>) Sut()
        => TestHelper.Sut<DebtsController>(x => new(x));
}