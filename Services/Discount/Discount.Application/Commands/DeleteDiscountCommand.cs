namespace Discount.Application.Commands;

using MediatR;

public sealed record DeleteDiscountCommand(string ProductName) : IRequest<bool>;