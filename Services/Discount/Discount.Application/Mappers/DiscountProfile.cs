namespace Discount.Application.Mappers;

using AutoMapper;
using Core.Entities;
using Grpc.Protos;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<Coupon, CouponModel>()
            .ReverseMap();
    }
}