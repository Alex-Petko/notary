using AuthService.Domain;
using AuthService.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public UsersController(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost("[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);

        var userCreated = await _repository.Users.TryCreateUser(user);
        if (userCreated)
            return Ok();

        return BadRequest();
    }
}