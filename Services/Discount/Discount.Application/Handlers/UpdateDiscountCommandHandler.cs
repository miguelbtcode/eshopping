namespace Discount.Application.Handlers;

using AutoMapper;
using Commands;
using Core.Entities;
using Core.Repositories;
using Grpc.Protos;
using MediatR;

public sealed class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
{
    private readonly IDiscountRepository discountRepository;
    private readonly IMapper mapper;
    
    public UpdateDiscountCommandHandler(
        IDiscountRepository discountRepository,
        IMapper mapper)
    {
        this.discountRepository = discountRepository;
        this.mapper = mapper;
    }

    public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request);
        await discountRepository.UpdateDiscountAsync(coupon);
        var couponModel = mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
}