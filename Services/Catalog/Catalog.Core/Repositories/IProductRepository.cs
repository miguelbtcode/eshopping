namespace Catalog.Core.Repositories;

using Entities;
using Specs;

public interface IProductRepository
{
    Task<Pagination<Product>> GetAllProducts(CatalogSpecParams catalogSpecParams);
    Task<Product> GetProductByIdAsync(string id);
    Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    Task<IEnumerable<Product>> GetProductsByBrandNameAsync(string brandName);
    Task<Product> CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(string id);
}