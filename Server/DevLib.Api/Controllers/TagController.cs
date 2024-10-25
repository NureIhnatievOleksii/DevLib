using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevLib.Api.Controllers
{
    [Route("api/tag")]
    public class TagController(IMediator mediator) : ControllerBase
    {

    }

}
