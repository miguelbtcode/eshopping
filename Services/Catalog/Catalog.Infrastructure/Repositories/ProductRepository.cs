namespace Catalog.Infrastructure.Repositories;

using Core.Entities;
using Core.Repositories;
using Core.Specs;
using Data;
using MongoDB.Driver;

public class ProductRepository : IProductRepository, IProductBrandRepository, IProductTypeRepository
{
    private ICatalogContext context { get; }
    
    public ProductRepository(ICatalogContext context)
    {
        this.context = context;
    }

    public async Task<Pagination<Product>> GetAllProducts(CatalogSpecParams catalogSpecParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;

        if (!string.IsNullOrEmpty(catalogSpecParams.Search))
        {
            filter &= builder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
        }

        if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
        {
            var brandFilter = builder.Eq(p => p.Brand!.Id, catalogSpecParams.BrandId);
            filter &= brandFilter;
        }
        
        if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
        {
            var typeFilter = builder.Eq(p => p.Type!.Id, catalogSpecParams.TypeId);
            filter &= typeFilter;
        }

        var totalItems = await context.Products.CountDocumentsAsync(filter);
        var data = await DataFilter(catalogSpecParams, filter);

        return new Pagination<Product>(
            catalogSpecParams.PageIndex,
            catalogSpecParams.PageSize,
            totalItems,
            data
        );
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
    
    //* Aux. methods
    private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
    {
        var builder = Builders<Product>.Sort;
        var sortDefinition = builder.Ascending("Name"); // Default

        if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
        {
            sortDefinition = catalogSpecParams.Sort switch
            {
                "priceAsc" => builder.Ascending(p => p.Price),
                "priceDesc" => builder.Descending(p => p.Price),
                _ => builder.Ascending(p => p.Name)
            };
        }

        return await context
            .Products
            .Find(filter)
            .Sort(sortDefinition)
            .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
            .Limit(catalogSpecParams.PageSize)
            .ToListAsync();
    }
}