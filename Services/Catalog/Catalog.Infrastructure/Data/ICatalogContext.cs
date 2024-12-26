namespace Catalog.Infrastructure.Data;

using Core.Entities;
using MongoDB.Driver;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
    IMongoCollection<ProductBrand> Brands { get; }
    IMongoCollection<ProductType> Types { get; }
}