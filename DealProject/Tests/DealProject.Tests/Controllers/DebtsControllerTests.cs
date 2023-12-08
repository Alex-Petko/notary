using DealProject;
using DealProject.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DealProjectTest;

public class DebtsControllerTests
{
    [Fact]
    public void GetAll_AllData()
    {
        // Arrange
        var controller = new DebtsController();

        // Act
        var actionResult = controller.Get();

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void GetById_OneItem(int id)
    {
        // Arrange
        var controller = new DebtsController();

        // Act
        var actionResult = controller.Get(id);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
    }

    [Fact]
    public void GiverSend_InitNewDebt_Status201Created()
    {
        // Arrange
        var controller = new DebtsController();
        var debt = new InitDebtDto(
            DealSourceType.Giver,
            GiverId:1,
            ReceiverId:2,
            Sum:1,
            FakeBeginDateTime());

        // Act
        var actionResult = controller.Init(debt);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
    }

    [Fact]
    public void ReceiverSend_InitNewDebt_Status201Created()
    {
        // Arrange
        var controller = new DebtsController();
        var debt = new InitDebtDto(
            DealSourceType.Receiver,
            GiverId: 1,
            ReceiverId: 2,
            Sum: 1,
            FakeBeginDateTime());

        // Act
        var actionResult = controller.Init(debt);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
    }

    [Fact]
    public void GiverSend_CloseDebt_Status201Created()
    {
        // Arrange
        var controller = new DebtsController();
        var debt = new InitDebtDto(
            DealSourceType.Receiver,
            GiverId: 1,
            ReceiverId: 2,
            Sum: 1,
            FakeBeginDateTime());

        // Act
        var actionResult = controller.Init(debt);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
    }

    [Fact]
    public void ReceiverSend_CloseDebt_Status201Created()
    {
        // Arrange
        var controller = new DebtsController();
        var debt = new InitDebtDto(
            DealSourceType.Receiver,
            GiverId: 1,
            ReceiverId: 2,
            Sum: 1,
            FakeBeginDateTime());

        // Act
        var actionResult = controller.Init(debt);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
    }

    [Fact]
    public void GetUsersDebtsAll_Ok()
    {
        // Arrange
        var controller = new DebtsController();
        var debt = new InitDebtDto(
            DealSourceType.Receiver,
            GiverId: 1,
            ReceiverId: 2,
            Sum: 1,
            FakeBeginDateTime());

        // Act
        var actionResult = controller.Init(debt);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
    }

    private DateTime FakeBeginDateTime() => new DateTime(2023, 1, 1);
}