namespace Catalog.Core.Repositories;

using Entities;

public interface IProductBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetAllProductBrandsAsync();
}