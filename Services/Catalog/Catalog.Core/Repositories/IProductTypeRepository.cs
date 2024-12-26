namespace Catalog.Core.Repositories;

using Entities;

public interface IProductTypeRepository
{
    Task<IEnumerable<ProductType>> GetAllProductTypesAsync();
}