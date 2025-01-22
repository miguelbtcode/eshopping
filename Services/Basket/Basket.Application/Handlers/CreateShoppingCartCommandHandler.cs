namespace Basket.Application.Handlers;

using Commands;
using Core.Entities;
using Core.Repositories;
using GrpcService;
using Mappers;
using MediatR;
using Responses;

public sealed class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository basketRepository;
    private readonly DiscountGrpcService discountGrpcService;

    public CreateShoppingCartCommandHandler(
        IBasketRepository basketRepository, 
        DiscountGrpcService discountGrpcService)
    {
        this.basketRepository = basketRepository;
        this.discountGrpcService = discountGrpcService;
    }
    
    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        //* Apply discount
        foreach (var item in request.Items)
        {
            var coupon = await discountGrpcService.GetDiscountAsync(item.ProductName);
            if (coupon != null)
                item.Price -= coupon.Amount;
        }
        
        var shoppingCart = await basketRepository.UpdateBasketAsync(new ShoppingCart
        {
            UserName = request.UserName,
            Items = request.Items
        });
        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;
    }
}