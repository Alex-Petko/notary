using AccessControl.Api;
using AccessControl.Application;
using AutoFixture.Xunit2;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Tests;

namespace AccessControl.Tests;

public class UsersControllerTests
{
    [Theory, AutoData]
    public async Task Create_Ok_Ok(CreateUserRequest request, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<CreateUserRequest, IActionResult>(new OkResult());

        // Act
        var result = await controller.Create(request, cancellationToken);

        // Assert
        Assert.IsType<OkResult>(result);
        mediatorVerify();
    }

    private static (UsersController, Action) Sut<TRequest, TResponse>(TResponse response)
       where TRequest : IRequest<TResponse>
        => TestHelper.Sut<UsersController, TRequest, TResponse>(x => new(x), response);
}