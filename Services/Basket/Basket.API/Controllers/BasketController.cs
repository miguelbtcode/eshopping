namespace Basket.API.Controllers;

using System.Net;
using Application.Commands;
using Application.Queries;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

public class BasketController : ApiController
{
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
}