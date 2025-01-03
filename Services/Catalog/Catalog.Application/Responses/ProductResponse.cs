namespace Catalog.Application.Responses;

using Core.Entities;

public sealed class ProductResponse(
    string id,
    string name,
    string summary,
    string description,
    string imageFile,
    ProductBrand brand,
    ProductType type,
    decimal price
)
{
    public string Id { get; init; } = id;
    public string Name { get; init; } = name;
    public string Summary { get; init; } = summary;
    public string Description { get; init; } = description;
    public string ImageFile { get; init; } = imageFile;
    public ProductBrand Brand { get; init; } = brand;
    public ProductType Type { get; init; } = type;
    public decimal Price { get; init; } = price;
}