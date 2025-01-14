namespace Ordering.Application.Mappers;

using AutoMapper;
using Commands;
using Core.Entities;
using EventBus.Messages.Events;
using Responses;

public class OrderingMappingProfile : Profile
{
    public OrderingMappingProfile()
    {
        CreateMap<Order, OrderResponse>()
            .ReverseMap();

        CreateMap<Order, CheckoutOrderCommand>()
            .ReverseMap();
        
        CreateMap<Order, CheckoutOrderCommandV2>()
            .ReverseMap();

        CreateMap<Order, UpdateOrderCommand>()
            .ReverseMap();

        CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>()
            .ReverseMap();
        
        CreateMap<CheckoutOrderCommandV2, BasketCheckoutEventV2>()
            .ReverseMap();
    }
}