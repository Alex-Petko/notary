using AccessControl.Api;
using AccessControl.Application;
using AutoFixture.Xunit2;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Tests;

namespace Api.Controllers;

public class TokensControllerTests
{
    [Theory, AutoData]
    public async Task Create_Ok_Ok(CreateTokenRequest request, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediatorVerify) = Sut<CreateTokenRequest, IActionResult>(new OkResult());

        // Act
        var result = await controller.Create(request, cancellationToken);

        // Assert
        Assert.IsType<OkResult>(result);
        mediatorVerify();
    }

    private static (TokensController, Action) Sut<TRequest, TResponse>(TResponse response)
       where TRequest : IRequest<TResponse>
        => TestHelper.Sut<TokensController, TRequest, TResponse>(x => new(x), response);
}