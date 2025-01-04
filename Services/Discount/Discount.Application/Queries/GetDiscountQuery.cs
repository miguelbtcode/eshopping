namespace Discount.Application.Queries;

using Grpc.Protos;
using MediatR;

public sealed record GetDiscountQuery(string ProductName) : IRequest<CouponModel>;