namespace Basket.Application.Handlers;

using Commands;
using Core.Entities;
using Core.Repositories;
using Mappers;
using MediatR;
using Responses;

public sealed class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository basketRepository;

    public CreateShoppingCartCommandHandler(IBasketRepository basketRepository)
    {
        this.basketRepository = basketRepository;
    }
    
    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        //TODO: Will be integrating Discount Service
        var shoppingCart = await basketRepository.UpdateBasketAsync(new ShoppingCart
        {
            UserName = request.UserName,
            Items = request.Items
        });
        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;
    }
}