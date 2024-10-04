using MediatR;
using Microsoft.AspNetCore.Mvc;
using DevLib.Application.CQRS.Commands.Auth.Login;
using DevLib.Application.CQRS.Commands.Auth.Logout;
using DevLib.Application.CQRS.Commands.Auth.Register;

namespace DevLib.Api.Controllers;

[Route("api/[controller]/[action]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { Message = "User registered successfully." });
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> LoginWithGoogle([FromBody] LoginWithGoogleCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { Message = "Login information added successfully." });
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { result.Token, Message = "User logged in successfully." });
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { Message = "Logout successful." });
        }

        return BadRequest(result.ErrorMessage);
    }
}
