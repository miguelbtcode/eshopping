namespace Catalog.API.Controllers;

using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    protected readonly IMediator mediator;

    protected ApiController(IMediator mediator)
    {
        this.mediator = mediator;
    }
}