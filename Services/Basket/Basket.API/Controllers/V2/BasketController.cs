namespace Basket.API.Controllers.V2;

using System.Net;
using Application.Commands;
using Application.Mappers;
using Application.Queries;
using Asp.Versioning;
using Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiVersion("2")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IPublishEndpoint publishEndpoint;
    private readonly ILogger<BasketController> logger;
    
    public BasketController(
        IMediator mediator,
        IPublishEndpoint publishEndpoint, 
        ILogger<BasketController> logger)
    {
        this.mediator = mediator;
        this.publishEndpoint = publishEndpoint;
        this.logger = logger;
    }
    
    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckoutV2 basketCheckout)
    {
        // Get the existing basket with username
        var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
        var basket = await mediator.Send(query);
        
        if (basket is null)
            return BadRequest();

        var eventMessage = BasketMapper.Mapper.Map<BasketCheckoutEventV2>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;
        // Publish the event
        await publishEndpoint.Publish(eventMessage);
        logger.LogInformation("Basket Published for {userName} with V2 endpoint", basketCheckout.UserName);
        // Remove the basket
        var deleteBasketCmd = new DeleteBasketByUserNameCommand(basketCheckout.UserName);
        await mediator!.Send(deleteBasketCmd);
        return Accepted();
    }
}