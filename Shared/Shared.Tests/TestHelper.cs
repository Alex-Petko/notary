using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Shared.Tests;

public static class TestHelper
{
    public static string String(int length = 128) => new string('x', length);

    public static (T, Action) Sut<T, TRequest, TResponse>(
        Func<IMediator, T> controllerCtr,
        TResponse response)
        where T : ControllerBase
        where TRequest : IRequest<TResponse>
    {
        var mediator = MediatorMock<TRequest, TResponse>(response);

        var controller = controllerCtr(mediator.Object);
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        return (controller, () => mediator.Verify(x => x.Send(It.IsAny<TRequest>(), It.IsAny<CancellationToken>()), Times.Once));
    }

    public static Mock<IMediator> MediatorMock<TRequest, TResponse>(TResponse response)
        where TRequest : IRequest<TResponse>
    {
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(x => x.Send(
                It.IsAny<TRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        return mediator;
    }
}