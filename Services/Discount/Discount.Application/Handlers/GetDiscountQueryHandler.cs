namespace Discount.Application.Handlers;

using Core.Repositories;
using global::Grpc.Core;
using Grpc.Protos;
using MediatR;
using Microsoft.Extensions.Logging;
using Queries;

public sealed class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel> 
{
    private readonly IDiscountRepository discountRepository;
    private readonly ILogger<GetDiscountQueryHandler> logger;

    public GetDiscountQueryHandler(
        IDiscountRepository discountRepository,
        ILogger<GetDiscountQueryHandler> logger)
    {
        this.discountRepository = discountRepository;
        this.logger = logger;
    }
    
    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var coupon = await discountRepository.GetDiscountAsync(request.ProductName);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name = {request.ProductName} not found"));
        
        var couponModel = new CouponModel
        {
            Id = coupon.Id, 
            Amount = coupon.Amount, 
            Description = coupon.Description, 
            ProductName = coupon.ProductName
        };
        logger.LogInformation("Coupon for the {productName} is fetched", request.ProductName);
        return couponModel;
    }
}