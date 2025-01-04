namespace Discount.Application.Commands;

using Grpc.Protos;
using MediatR;

public sealed record CreateDiscountCommand(
    string ProductName,
    string Description,
    int Amount
) : IRequest<CouponModel>;