using AccessControl.Domain;
using AccessControl.Infrastructure;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Application;

internal sealed class CreateTokenHandler : IRequestHandler<CreateTokenRequest, IActionResult>
{
    private readonly ITokenManager _tokenManager;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTokenHandler(
        ITokenManager tokenManager,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _tokenManager = tokenManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Handle(CreateTokenRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        if (!await _unitOfWork.Users.ContainsAsync(user))
            return new NotFoundResult();

        await _tokenManager.UpdateAsync(user.Login);

        return new OkResult();
    }
}