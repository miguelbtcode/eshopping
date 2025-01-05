namespace Ordering.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    public OrderContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
        optionsBuilder.UseSqlServer("Data Source=OrderDb");
        return new OrderContext(optionsBuilder.Options);
    }
}