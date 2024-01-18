using AccessControl.Application;
using AccessControl.Domain;
using AccessControl.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Tests;

namespace Application.UseCases;

public class CreateUserHandlerTests
{
    [Theory, CustomAutoData]
    public async Task Handle_FailCreateResult_ConflictResult(
        CreateUserRequest request, 
        CancellationToken cancellationToken, 
        User user)
    {
        // Arrange
        var (handler, unitOfWork, mapper, passwordHasher) = Sut();
        unitOfWork.Setup(x => x.Users.FindAsync(request.Login)).ReturnsAsync(user);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.IsType<ConflictResult>(result);

        unitOfWork.Verify(x => x.Users.FindAsync(request.Login), Times.Once);
        unitOfWork.VerifyNoOtherCalls();

        mapper.VerifyNoOtherCalls();

        passwordHasher.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Handle_Ok_OkResult(
        CreateUserRequest request, 
        CancellationToken cancellationToken, 
        User user, 
        string passwordHash)
    {
        // Arrange
        var (handler, unitOfWork, mapper, passwordHasher) = Sut();
        unitOfWork.Setup(x => x.Users.FindAsync(request.Login)).ReturnsAsync((User)null!);
        mapper.Setup(x => x.Map<User>(request)).Returns(user);
        passwordHasher.Setup(x => x.HashPassword(user, request.Password)).Returns(passwordHash);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.Equal(passwordHash, user.PasswordHash);
        Assert.IsType<OkResult>(result);

        unitOfWork.Verify(x => x.Users.FindAsync(request.Login), Times.Once);
        unitOfWork.Verify(x => x.Users.Add(user), Times.Once);
        unitOfWork.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
        unitOfWork.VerifyNoOtherCalls();

        mapper.Verify(x => x.Map<User>(request), Times.Once);
        mapper.VerifyNoOtherCalls();

        passwordHasher.Verify(x => x.HashPassword(user, request.Password), Times.Once);
        passwordHasher.VerifyNoOtherCalls();
    }

    private (CreateUserHandler, Mock<IUnitOfWork>, Mock<IMapper>, Mock<IPasswordHasher>) Sut()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var mapper = new Mock<IMapper>();
        var passwordHasher = new Mock<IPasswordHasher>();

        return (
            new CreateUserHandler(unitOfWork.Object, mapper.Object, passwordHasher.Object),
            unitOfWork,
            mapper,
            passwordHasher
        );
    }
}