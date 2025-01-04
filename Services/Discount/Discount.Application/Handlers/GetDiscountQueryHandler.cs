namespace Discount.Application.Handlers;

using Core.Repositories;
using global::Grpc.Core;
using Grpc.Protos;
using MediatR;
using Queries;

public sealed class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel> 
{
    private readonly IDiscountRepository discountRepository;

    public GetDiscountQueryHandler(IDiscountRepository discountRepository)
    {
        this.discountRepository = discountRepository;
    }
    
    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var coupon = await discountRepository.GetDiscountAsync(request.ProductName);
        return coupon is null
            ? throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name = {request.ProductName} not found"))
            : new CouponModel
            {
                Id = coupon.Id, Amount = coupon.Amount, Description = coupon.Description, ProductName = coupon.ProductName
            };
    }
}