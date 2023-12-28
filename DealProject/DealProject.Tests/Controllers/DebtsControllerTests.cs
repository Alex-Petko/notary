using DealProject;
using DealProject.Application;
using DealProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DealProjectTest;

public class DebtsControllerTests
{
    [Fact]
    public async Task GetAll_OkObjectResult()
    {
        // Arrange
        var list = new List<GetDebtDto>() { new GetDebtDto(), new GetDebtDto() };

        var debtService = new Mock<IDebtService>();
        debtService.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
        var controller = new DebtsController(debtService.Object);

        // Act
        var actionResult = await controller.GetAll();

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(list, okObjectResult.Value);
    }

    [Fact]
    public async Task Get_OkObjectResult()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var debtService = new Mock<IDebtService>();
        debtService.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(new GetDebtDto() { Id = guid });

        var controller = new DebtsController(debtService.Object);

        // Act
        var actionResult = await controller.Get(guid);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var dto = Assert.IsType<GetDebtDto>(okObjectResult.Value);
        Assert.Equal(guid, dto.Id);
    }

    [Fact]
    public async Task Get_NotFoundResult()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var debtService = new Mock<IDebtService>();
        debtService.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(value: null);

        var controller = new DebtsController(debtService.Object);

        // Act
        var actionResult = await controller.Get(guid);

        //Assert
        Assert.IsType<NotFoundResult>(actionResult);
    }

    [Fact]
    public async Task Lend_CreatedResult()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var debtService = new Mock<IDebtService>();
        debtService
            .Setup(x => x.LendAsync(It.IsAny<string>(), It.IsAny<LendDebtDto>()))
            .ReturnsAsync(guid);

        var controller = new DebtsController(debtService.Object);

        // Act
        var actionResult = await controller.Lend(new LendDebtDto("1", 1, DateTime.UtcNow), "login");

        //Assert
        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        var createdGuid = Assert.IsType<Guid>(createdResult.Value);
        Assert.Equal(guid, createdGuid);
    }

    [Fact]
    public async Task Borrow_CreatedResult()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var debtService = new Mock<IDebtService>();
        debtService
            .Setup(x => x.BorrowAsync(It.IsAny<string>(), It.IsAny<BorrowDebtDto>()))
            .ReturnsAsync(guid);

        var controller = new DebtsController(debtService.Object);

        // Act
        var actionResult = await controller.Borrow(new BorrowDebtDto("1", 1, DateTime.UtcNow), "login");

        //Assert
        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        var createdGuid = Assert.IsType<Guid>(createdResult.Value);
        Assert.Equal(guid, createdGuid);
    }

    [Fact]
    public async Task Accept_OkObjectResult()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var debtService = new Mock<IDebtService>();
        debtService
            .Setup(x => x.AcceptAsync(It.IsAny<string>(), It.IsAny<AcceptDebtDto>()))
            .ReturnsAsync(DealStatusType.LenderApproved);

        var controller = new DebtsController(debtService.Object);

        // Act
        var actionResult = await controller.Accept(new AcceptDebtDto(guid), "login");

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var status = Assert.IsType<DealStatusType>(okObjectResult.Value);
        Assert.Equal(DealStatusType.LenderApproved, status);
    }

    [Fact]
    public async Task Accept_BadRequestResult()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var debtService = new Mock<IDebtService>();
        debtService
            .Setup(x => x.AcceptAsync(It.IsAny<string>(), It.IsAny<AcceptDebtDto>()))
            .ReturnsAsync(value: null);

        var controller = new DebtsController(debtService.Object);

        // Act
        var actionResult = await controller.Accept(new AcceptDebtDto(guid), "login");

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);
    }

    [Fact]
    public async Task Cancel_OkObjectResult()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var debtService = new Mock<IDebtService>();
        debtService
            .Setup(x => x.CancelAsync(It.IsAny<string>(), It.IsAny<CancelDebtDto>()))
            .ReturnsAsync(DealStatusType.LenderApproved);

        var controller = new DebtsController(debtService.Object);

        // Act
        var actionResult = await controller.Cancel(new CancelDebtDto(guid), "login");

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var status = Assert.IsType<DealStatusType>(okObjectResult.Value);
        Assert.Equal(DealStatusType.LenderApproved, status);
    }

    [Fact]
    public async Task Cancel_BadRequestResult()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var debtService = new Mock<IDebtService>();
        debtService
            .Setup(x => x.CancelAsync(It.IsAny<string>(), It.IsAny<CancelDebtDto>()))
            .ReturnsAsync(value: null);

        var controller = new DebtsController(debtService.Object);

        // Act
        var actionResult = await controller.Cancel(new CancelDebtDto(guid), "login");

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);
    }

    [Fact]
    public async Task Close_OkObjectResult()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var debtService = new Mock<IDebtService>();
        debtService
            .Setup(x => x.CloseAsync(It.IsAny<string>(), It.IsAny<CloseDebtDto>()))
            .ReturnsAsync(DealStatusType.LenderApproved);

        var controller = new DebtsController(debtService.Object);

        // Act
        var actionResult = await controller.Close(new CloseDebtDto(guid), "login");

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        var status = Assert.IsType<DealStatusType>(okObjectResult.Value);
        Assert.Equal(DealStatusType.LenderApproved, status);
    }

    [Fact]
    public async Task Close_BadRequestResult()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var debtService = new Mock<IDebtService>();
        debtService
            .Setup(x => x.CloseAsync(It.IsAny<string>(), It.IsAny<CloseDebtDto>()))
            .ReturnsAsync(value: null);

        var controller = new DebtsController(debtService.Object);

        // Act
        var actionResult = await controller.Close(new CloseDebtDto(guid), "login");

        //Assert
        Assert.IsType<BadRequestResult>(actionResult);
    }
}