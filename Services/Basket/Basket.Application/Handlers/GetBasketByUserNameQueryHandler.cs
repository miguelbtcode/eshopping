namespace Basket.Application.Handlers;

using Core.Repositories;
using Mappers;
using MediatR;
using Queries;
using Responses;

public sealed class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse?>
{
    private readonly IBasketRepository basketRepository;

    public GetBasketByUserNameQueryHandler(IBasketRepository basketRepository)
    {
        this.basketRepository = basketRepository;
    }
    
    public async Task<ShoppingCartResponse?> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await basketRepository.GetBasketAsync(request.UserName);

        if (shoppingCart is null)
            return null;
        
        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;
    }
}