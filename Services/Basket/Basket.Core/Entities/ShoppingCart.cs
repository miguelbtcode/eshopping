namespace Basket.Core.Entities;

public class ShoppingCart
{
    public ShoppingCart()
    {
            
    }
    
    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
    
    public required string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = [];
}