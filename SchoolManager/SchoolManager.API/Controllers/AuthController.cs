using Microsoft.AspNetCore.Mvc;
using MediatR;
using SchoolManager.Application.Features.Auth.Commands;

namespace SchoolManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}