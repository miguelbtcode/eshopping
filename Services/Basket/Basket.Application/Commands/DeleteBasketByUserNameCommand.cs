namespace Basket.Application.Commands;

using MediatR;

public sealed record DeleteBasketByUserNameCommand(string UserName) : IRequest<Unit>;