using AutoFixture.Xunit2;
using DebtManager.Api;
using DebtManager.Application;
using DebtManager.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Tests;

namespace DebtManager.Tests;

public class DebtsControllerTests
{
    [Theory, AutoData]
    public async Task GetAll_OkObjectResult(List<GetDebtDto> result)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<GetAllDebtsRequest, IEnumerable<GetDebtDto>>(result);

        // Act
        var actionResult = await controller.GetAll();

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(result, okObjectResult.Value);
        mediatorVerify();
    }

    [Theory, AutoData]
    public async Task Get_OkObjectResult(GetDebtRequest request, GetDebtDto response)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<GetDebtRequest, GetDebtDto?>(response);

        // Act
        var actionResult = await controller.Get(request);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultDto = Assert.IsType<GetDebtDto>(okObjectResult.Value);
        Assert.Equal(response, resultDto);
        mediatorVerify();
    }

    [Theory, AutoData]
    public async Task Get_NotFoundResult(GetDebtRequest request)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<GetDebtRequest, GetDebtDto?>(null);

        // Act
        var actionResult = await controller.Get(request);

        //Assert
        Assert.IsType<NotFoundResult>(actionResult);
        mediatorVerify();
    }

    [Theory, AutoData]
    public async Task Lend_CreatedResult(LendDebtRequest request, Guid response)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<LendDebtRequest, Guid>(response);

        // Act
        var actionResult = await controller.Lend(request);

        //Assert
        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        var createdGuid = Assert.IsType<Guid>(createdResult.Value);
        Assert.Equal(response, createdGuid);
        mediatorVerify();
    }

    [Theory, AutoData]
    public async Task Borrow_CreatedResult(BorrowDebtRequest request, Guid response)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<BorrowDebtRequest, Guid>(response);

        // Act
        var actionResult = await controller.Borrow(request);

        //Assert
        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        var createdGuid = Assert.IsType<Guid>(createdResult.Value);
        Assert.Equal(response, createdGuid);
        mediatorVerify();
    }

    [Theory, AutoData]
    public async Task Accept_OkObjectResult(AcceptDebtRequest request, DealStatusType response)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<AcceptDebtRequest, DealStatusType?>(response);

        // Act
        var actionResult = await controller.Accept(request);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultStatus = Assert.IsType<DealStatusType>(okObjectResult.Value);
        Assert.Equal(response, resultStatus);
        mediatorVerify();
    }

    [Theory, AutoData]
    public async Task Accept_BadRequestResult(AcceptDebtRequest request)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<AcceptDebtRequest, DealStatusType?>(null);

        // Act
        var actionResult = await controller.Accept(request);

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);
        mediatorVerify();
    }

    [Theory, AutoData]
    public async Task Cancel_OkObjectResult(CancelDebtRequest request, DealStatusType response)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<CancelDebtRequest, DealStatusType?>(response);

        // Act
        var actionResult = await controller.Cancel(request);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultStatus = Assert.IsType<DealStatusType>(okObjectResult.Value);
        Assert.Equal(response, resultStatus);
        mediatorVerify();
    }

    [Theory, AutoData]
    public async Task Cancel_BadRequestResult(CancelDebtRequest request)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<CancelDebtRequest, DealStatusType?>(null);

        // Act
        var actionResult = await controller.Cancel(request);

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);
        mediatorVerify();
    }

    [Theory, AutoData]
    public async Task Close_OkObjectResult(CloseDebtRequest request, DealStatusType response)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<CloseDebtRequest, DealStatusType?>(response);

        // Act
        var actionResult = await controller.Close(request);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var resultStatus = Assert.IsType<DealStatusType>(okObjectResult.Value);
        Assert.Equal(response, resultStatus);
        mediatorVerify();
    }

    [Theory, AutoData]
    public async Task Close_BadRequestResult(CloseDebtRequest request)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<CloseDebtRequest, DealStatusType?>(null);

        // Act
        var actionResult = await controller.Close(request);

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);
        mediatorVerify();
    }

    private static (DebtsController, Action) Sut<TRequest, TResponse>(TResponse response)
       where TRequest : IRequest<TResponse>
        => TestHelper.Sut<DebtsController, TRequest, TResponse>(x => new(x), response);
}