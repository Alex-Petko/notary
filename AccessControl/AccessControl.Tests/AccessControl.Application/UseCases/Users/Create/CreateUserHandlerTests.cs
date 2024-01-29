using AccessControl.Application;
using AccessControl.Domain;
using AutoMapper;
using Moq;
using Shared.Tests;

namespace Application.UseCases;

public class CreateUserHandlerTests
{
    [Theory, CustomAutoData]
    public async Task Handle_UserCreated_Ok(
        CreateUserCommand command,
        CancellationToken cancellationToken,
        User user,
        string hash)
    {
        // Arrange
        var (handler, commandProvider, mapper, passwordHasher) = Sut();
        mapper.Setup(x => x.Map<User>(command)).Returns(user);
        passwordHasher.Setup(x => x.HashPassword(user, command.Password)).Returns(hash);
        commandProvider.Setup(x => x.TryCreateUserAsync(user, cancellationToken)).ReturnsAsync(true);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        var createUserCommandResult = Assert.IsType<CreateUserCommandResult>(result);
        Assert.Equal(CreateUserCommandResult.Ok, createUserCommandResult);

        mapper.Verify(x => x.Map<User>(command), Times.Once);
        mapper.VerifyNoOtherCalls();

        passwordHasher.Verify(x => x.HashPassword(user, command.Password), Times.Once);
        passwordHasher.VerifyNoOtherCalls();


        commandProvider.Verify(x => x.TryCreateUserAsync(user, cancellationToken), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Handle_UserNotCreated_CreateUserFail(
        CreateUserCommand command,
        CancellationToken cancellationToken,
        User user,
        string hash)
    {
        // Arrange
        var (handler, commandProvider, mapper, passwordHasher) = Sut();
        mapper.Setup(x => x.Map<User>(command)).Returns(user);
        passwordHasher.Setup(x => x.HashPassword(user, command.Password)).Returns(hash);
        commandProvider.Setup(x => x.TryCreateUserAsync(user, cancellationToken)).ReturnsAsync(false);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        var createUserCommandResult = Assert.IsType<CreateUserCommandResult>(result);
        Assert.Equal(CreateUserCommandResult.CreateUserFail, createUserCommandResult);

        mapper.Verify(x => x.Map<User>(command), Times.Once);
        mapper.VerifyNoOtherCalls();

        passwordHasher.Verify(x => x.HashPassword(user, command.Password), Times.Once);
        passwordHasher.VerifyNoOtherCalls();


        commandProvider.Verify(x => x.TryCreateUserAsync(user, cancellationToken), Times.Once);
        commandProvider.VerifyNoOtherCalls();
    }

    private static (CreateUserCommandHandler, Mock<ICommandProvider>, Mock<IMapper>, Mock<IPasswordHasher>) Sut()
    {
        var commandProvider = new Mock<ICommandProvider>();
        var mapper = new Mock<IMapper>();
        var passwordHasher = new Mock<IPasswordHasher>();

        return (
            new CreateUserCommandHandler(commandProvider.Object, mapper.Object, passwordHasher.Object),
            commandProvider,
            mapper,
            passwordHasher
        );
    }
}