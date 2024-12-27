namespace Catalog.Application.Responses;

using Core.Entities;

public sealed record ProductResponse
(
    string Id,
    string Name,
    string Summary,
    string Description,
    string ImageFile,
    ProductBrand Brands,
    ProductType Types,
    decimal Price
);