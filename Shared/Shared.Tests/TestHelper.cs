using Global;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Shared.Tests;

public static class TestHelper
{
    public static string String(int length) => new string('x', length);

    public static (T, Mock<IMediator>) Sut<T>(
        Func<IMediator, T> controllerCtr)
        where T : ControllerBase
    {
        var mediator = new Mock<IMediator>();

        var controller = controllerCtr(mediator.Object);
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        return (controller, mediator);
    }
}