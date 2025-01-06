namespace Ordering.Application.Queries;

using MediatR;
using Responses;

public sealed record GetOrderListQuery(string UserName) : IRequest<List<OrderResponse>>;