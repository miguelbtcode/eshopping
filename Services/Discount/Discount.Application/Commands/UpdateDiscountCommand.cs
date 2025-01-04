namespace Discount.Application.Commands;

using Grpc.Protos;
using MediatR;

public sealed record UpdateDiscountCommand(
    int Id,
    string ProductName,
    string Description,
    int Amount
) : IRequest<CouponModel>;