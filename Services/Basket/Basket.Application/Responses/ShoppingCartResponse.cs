namespace Basket.Application.Responses;

public class ShoppingCartResponse
{
    public ShoppingCartResponse()
    {
            
    }
    public ShoppingCartResponse(string username)
    {
        UserName = username;
    }
    
    public required string UserName { get; set; }
    public List<ShoppingCartItemResponse> Items { get; set; } = [];
    
    public decimal TotalPrice
    {
        get
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }
    }
}