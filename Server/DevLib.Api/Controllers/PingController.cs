using Microsoft.AspNetCore.Mvc;

namespace DevLib.Api.Controllers;

[ApiController]
[Route("api/ping")]
public class PingController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Ping()
    {
        IActionResult response = Ok();

        return Task.FromResult(response);
    }
}