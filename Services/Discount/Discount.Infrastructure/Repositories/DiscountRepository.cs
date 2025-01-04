namespace Discount.Infrastructure.Repositories;

using Core.Entities;
using Core.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<Coupon?> GetDiscountAsync(string productName)
    {
        await using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        return await connection.QueryFirstOrDefaultAsync<Coupon>(
            "SELECT * FROM Coupon WHERE ProductName = @ProductName", 
            new
            {
                ProductName = productName
            });
    }

    public async Task<bool> CreateDiscountAsync(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var affected = await connection.ExecuteAsync(
            "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
            new {
                coupon.ProductName, 
                coupon.Description, 
                coupon.Amount 
            });

        return affected != 0;
    }

    public async Task<bool> UpdateDiscountAsync(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var affected = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
            new {
                coupon.ProductName, 
                coupon.Description, 
                coupon.Amount, 
                coupon.Id 
            });
        
        return affected != 0;
    }

    public async Task<bool> DeleteDiscountAsync(string productName)
    {
        await using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var affected = await connection.ExecuteAsync(
            "DELETE FROM Coupon WHERE ProductName = @ProductName",
            new
            {
                ProductName = productName
            });

        return affected != 0;
    }
}