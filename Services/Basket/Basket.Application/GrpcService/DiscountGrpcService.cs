namespace Basket.Application.GrpcService;

using Discount.Grpc.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;

public class DiscountGrpcService(
    DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient,
    ILogger<DiscountGrpcService> logger)
{
    public async Task<CouponModel?> GetDiscountAsync(string productName)
    {
        try
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await discountProtoServiceClient.GetDiscountAsync(discountRequest);
        }
        catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            logger.LogInformation("Product {productName} not found:", ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            logger.LogError("Unexpected error: {message}", ex.Message);
            throw;
        }
    }
}