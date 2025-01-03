namespace Basket.Core.Repositories;

using Entities;

public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasketAsync(string userName);
    Task<ShoppingCart> UpdateBasketAsync(ShoppingCart shoppingCart);
    Task DeleteBasketAsync(string userName);
}