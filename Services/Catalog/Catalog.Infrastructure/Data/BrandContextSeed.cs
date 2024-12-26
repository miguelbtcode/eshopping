namespace Catalog.Infrastructure.Data;

using System.Text.Json;
using Core.Entities;
using MongoDB.Driver;

public static class BrandContextSeed
{
    public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
    {
        var checkBrands = brandCollection.Find(b => true).Any();
        var path = Path.Combine("Data", "SeedData", "brands.json");

        if (checkBrands)
            return;

        var brandsData = File.ReadAllText(path);
        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

        if (brands == null)
            return;

        foreach (var brand in brands)
        {
            brandCollection.InsertOneAsync(brand);
        }
    }
}