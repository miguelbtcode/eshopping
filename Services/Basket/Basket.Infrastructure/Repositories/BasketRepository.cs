namespace Basket.Infrastructure.Repositories;

using Core.Entities;
using Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache redisCache;
    
    public BasketRepository(IDistributedCache redisCache)
    {
        this.redisCache = redisCache;
    }

    public async Task<ShoppingCart?> GetBasketAsync(string userName)
    {
        var basket = await redisCache.GetStringAsync(userName);
        return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart shoppingCart)
    {
        await redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));
        return (await GetBasketAsync(shoppingCart.UserName))!;
    }

    public async Task DeleteBasketAsync(string userName)
    {
        await redisCache.RemoveAsync(userName);
    }
}