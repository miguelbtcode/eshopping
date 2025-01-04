namespace Discount.API.Services;

using Application.Commands;
using Application.Queries;
using global::Grpc.Core;
using Grpc.Protos;
using MediatR;

public sealed class DiscountService(
    IMediator mediator, 
    ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var query = new GetDiscountQuery(request.ProductName);
        var result = await mediator.Send(query);
        logger.LogInformation("Discount is retrieved for the Product Name: {ProductName} and Amount: {Amount}", request.ProductName, result.Amount);
        return result;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var command = new CreateDiscountCommand
        (
            ProductName: request.Coupon.ProductName,
            Description: request.Coupon.Description,
            Amount: request.Coupon.Amount
        );

        var result = await mediator.Send(command);
        logger.LogInformation("Discount is successfully created for the Product Name: {ProductName}", result.ProductName);
        return result;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var command = new UpdateDiscountCommand
        (
            Id: request.Coupon.Id,
            ProductName: request.Coupon.ProductName,
            Description: request.Coupon.Description,
            Amount: request.Coupon.Amount
        );

        var result = await mediator.Send(command);
        logger.LogInformation("Discount is successfully updated for the Product Name: {ProductName}", result.ProductName);
        return result;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var command = new DeleteDiscountCommand(request.ProductName);
        var deleted = await mediator.Send(command);
        return new DeleteDiscountResponse
        {
            Success = deleted
        };
    }
}