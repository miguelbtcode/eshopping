namespace Discount.Application.Handlers;

using AutoMapper;
using Commands;
using Core.Entities;
using Core.Repositories;
using Grpc.Protos;
using MediatR;

public sealed class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
{
    private readonly IDiscountRepository discountRepository;
    private readonly IMapper mapper;
    
    public CreateDiscountCommandHandler(
        IDiscountRepository discountRepository,
        IMapper mapper)
    {
        this.discountRepository = discountRepository;
        this.mapper = mapper;
    }

    public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request);
        await discountRepository.CreateDiscountAsync(coupon);
        var couponModel = mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
}