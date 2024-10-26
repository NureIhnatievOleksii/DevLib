using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevLib.Api.Controllers
{
    [Route("api/post")]
    public class PostController(IMediator mediator) : ControllerBase
    {

    }

}
