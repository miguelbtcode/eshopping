namespace Catalog.Infrastructure.Repositories;

using Core.Entities;
using Core.Repositories;
using Data;
using MongoDB.Driver;

public class ProductRepository : IProductRepository, IProductBrandRepository, IProductTypeRepository
{
    public ICatalogContext context { get; }
    
    public ProductRepository(ICatalogContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await context.Products
            .Find(p => true)
            .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await context.Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        return await context.Products
            .Find(p => p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandNameAsync(string brandName)
    {
        return await context.Products
            .Find(p => p.Brand!.Name.Equals(brandName, StringComparison.CurrentCultureIgnoreCase))
            .ToListAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        await context.Products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var updatedProduct = await context.Products
            .ReplaceOneAsync(p => p.Id == product.Id, product);
        return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var deletedProduct = await context.Products
            .DeleteOneAsync(p => p.Id == id);
        return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
    }

    public async Task<IEnumerable<ProductBrand>> GetAllProductBrandsAsync()
    {
        return await context.Brands
            .Find(brand => true)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductType>> GetAllProductTypesAsync()
    {
        return await context.Types
            .Find(type => true)
            .ToListAsync();
    }
}