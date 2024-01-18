using AccessControl.Domain;
using AccessControl.Infrastructure;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Application;

internal sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, IActionResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<IActionResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var dbUser = await _unitOfWork.Users.FindAsync(request.Login);
        if (dbUser is not null)
            return new ConflictResult();

        var user = _mapper.Map<User>(request);

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        _unitOfWork.Users.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OkResult();
    }
}