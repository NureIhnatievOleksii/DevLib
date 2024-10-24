using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PostController(IMediator mediator) : ControllerBase
    {

    }

}
