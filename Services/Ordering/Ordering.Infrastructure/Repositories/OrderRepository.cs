namespace Ordering.Infrastructure.Repositories;

using Core.Entities;
using Core.Repositories;
using Data;
using Microsoft.EntityFrameworkCore;

public class OrderRepository(OrderContext dbContext) : RepositoryBase<Order>(dbContext), IOrderRepository
{
    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        var orderList = await dbContext.Orders
            .Where(o => o.UserName == userName)
            .ToListAsync();
        return orderList;
    }
}