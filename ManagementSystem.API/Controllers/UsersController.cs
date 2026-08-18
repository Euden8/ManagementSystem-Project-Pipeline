using ManagementSystem.Application.Users.Commands.Login;
using ManagementSystem.Application.Users.Commands.RegisterUser;
using ManagementSystem.Application.Users.Commands.UpdateUser;
using ManagementSystem.Application.Users.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)  
    {
        _sender = sender;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetAllUsersQuery());
        return Ok(result);
    }


    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateUserCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }
}