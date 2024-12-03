using DevLib.Application.CQRS.Queries.Admins;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevLib.Api.Controllers
{
    [Route("api/moderation")]
    public class ModerationController(IMediator mediator) : ControllerBase
    {
        
    }
}
