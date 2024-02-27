using MediatR;

namespace Rent.Application;

public sealed record AddTemplateCommand(IFile FileStream) : IRequest<bool>;
