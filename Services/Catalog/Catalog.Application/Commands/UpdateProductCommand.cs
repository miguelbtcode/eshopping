namespace Catalog.Application.Commands;

using Core.Entities;
using MediatR;

public sealed record UpdateProductCommand
(
    string Id,
    string Name,
    string Summary,
    string Description,
    string ImageFile,
    decimal Price,
    ProductBrand Brand,
    ProductType Type
) : IRequest<bool>;