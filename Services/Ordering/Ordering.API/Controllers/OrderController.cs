namespace Ordering.API.Controllers;

using System.Net;
using Application.Commands;
using Application.Queries;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

public class OrderController : ApiController
{
    [HttpGet("{userName}", Name = "GetOrdersByUserName")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName)
    {
        var query = new GetOrderListQuery(userName);
        var orders = await mediator!.Send(query);
        return Ok(orders);
    }
    
    //Just for testing 
    [HttpPost(Name = "CheckoutOrder")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
    {
        var result = await mediator!.Send(command);
        return Ok(result);
    }
    
    [HttpPut(Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
        await mediator!.Send(command);
        return NoContent();
    }
    
    [HttpDelete("{id:int}", Name = "DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var cmd = new DeleteOrderCommand(id);
        await mediator!.Send(cmd);
        return NoContent();
    }
}