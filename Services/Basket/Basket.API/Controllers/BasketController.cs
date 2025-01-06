namespace Basket.API.Controllers;

using System.Net;
using Application.Commands;
using Application.Mappers;
using Application.Queries;
using Application.Responses;
using Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

public class BasketController : ApiController
{
    private readonly IPublishEndpoint publishEndpoint;
    
    public BasketController(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    [Route("[action]/{userName}", Name = "GetBasketByUserName")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName) 
    {
        var query = new GetBasketByUserNameQuery(userName);
        var basket = await mediator!.Send(query);
        return Ok(basket);
    }
    
    [HttpPost("CreateBasket")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> CreateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
    {
        var basket = await mediator!.Send(createShoppingCartCommand);
        return Ok(basket);
    }

    [HttpDelete]
    [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> DeleteBasket(string userName) 
    {
        var cmd = new DeleteBasketByUserNameCommand(userName);
        return Ok(await mediator!.Send(cmd));
    }

    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        // Get the existing basket with username
        var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
        var basket = await mediator!.Send(query);
        
        if (basket is null)
            return BadRequest();

        var eventMessage = BasketMapper.Mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;
        // Publish the event
        await publishEndpoint.Publish(eventMessage);
        // Remove the basket
        var deleteBasketCmd = new DeleteBasketByUserNameCommand(basketCheckout.UserName);
        await mediator!.Send(deleteBasketCmd);
        return Accepted();
    }
}