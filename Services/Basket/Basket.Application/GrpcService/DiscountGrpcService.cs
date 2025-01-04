namespace Basket.Application.GrpcService;

using Discount.Grpc.Protos;

public class DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
{
    public async Task<CouponModel> GetDiscountAsync(string productName)
    {
        var discountRequest = new GetDiscountRequest { ProductName = productName };
        return await discountProtoServiceClient.GetDiscountAsync(discountRequest);
    }
}