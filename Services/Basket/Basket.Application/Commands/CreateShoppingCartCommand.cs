namespace Basket.Application.Commands;

using Core.Entities;
using MediatR;
using Responses;

public sealed record CreateShoppingCartCommand(
    string UserName, 
    List<ShoppingCartItem> Items) : IRequest<ShoppingCartResponse>;