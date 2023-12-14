using AuthService.Application;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserCreator _userCreator;

    public UsersController(IUserCreator userCreator)
    {
        _userCreator = userCreator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        return await _userCreator.ExecuteAsync(dto) ? Ok() : BadRequest();
    }
}