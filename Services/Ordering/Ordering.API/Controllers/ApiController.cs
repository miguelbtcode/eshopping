namespace Ordering.API.Controllers;

using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator? mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}