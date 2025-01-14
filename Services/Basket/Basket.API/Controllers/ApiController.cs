namespace Basket.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator? mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}