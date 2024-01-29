using MediatR;

namespace Rent.Application;

public sealed record GetTemplateQuery(Guid Id) : IRequest<Stream?>;
