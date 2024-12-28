namespace Catalog.Application.Commands;

using Core.Entities;
using MediatR;
using Responses;

public sealed record CreateProductCommand
(
    string Name,
    string Summary,
    string Description,
    string ImageFile,
    decimal Price,
    ProductBrand Brand,
    ProductType Type
) : IRequest<ProductResponse>;