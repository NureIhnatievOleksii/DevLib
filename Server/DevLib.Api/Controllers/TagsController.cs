﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevLib.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TagsController(IMediator mediator) : ControllerBase
    {

    }

}
