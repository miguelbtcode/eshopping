namespace Basket.Application.Queries;

using MediatR;
using Responses;

public sealed record GetBasketByUserNameQuery(string UserName) : IRequest<ShoppingCartResponse>;