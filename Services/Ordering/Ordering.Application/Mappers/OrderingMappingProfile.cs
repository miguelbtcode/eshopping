namespace Ordering.Application.Mappers;

using AutoMapper;
using Commands;
using Core.Entities;
using Responses;

public class OrderingMappingProfile : Profile
{
    public OrderingMappingProfile()
    {
        CreateMap<Order, OrderResponse>()
            .ReverseMap();

        CreateMap<Order, CheckoutOrderCommand>()
            .ReverseMap();

        CreateMap<Order, UpdateOrderCommand>()
            .ReverseMap();
    }
}