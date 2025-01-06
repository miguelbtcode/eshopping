namespace Ordering.Infrastructure.Data;

using Core.Entities;
using Microsoft.Extensions.Logging;

public class OrderContextSeed
{
    public async static Task SeedAsync(OrderContext orderContext, 
                                       ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Ordering Database: {DbContextName} seeded!!!", nameof(OrderContext));
        }
    }

    private static List<Order> GetOrders()
    {
        return
        [
            new Order
            {
                UserName = "miguelbtcode",
                FirstName = "Miguel",
                LastName = "Barreto",
                EmailAddress = "mabt2206@gmail.com",
                AddressLine = "Lima",
                Country = "Peru",
                TotalPrice = 750,
                State = "KA",
                ZipCode = "560001",
                CardName = "Visa",
                CardNumber = "1234567890123456",
                CreatedBy = "miguelbtcode",
                Expiration = "12/25",
                Cvv = "123",
                PaymentMethod = 1,
                LastModifiedBy = "miguelbtcode",
                LastModifiedDate = new DateTime(),
            }
        ];
    }
}