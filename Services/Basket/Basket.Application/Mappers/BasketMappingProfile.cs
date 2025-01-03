namespace Basket.Application.Mappers;

using AutoMapper;
using Core.Entities;
using Responses;

public class BasketMappingProfile : Profile
{
    public BasketMappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartResponse>()
            .ReverseMap();
        
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>()
            .ReverseMap();
    }
}