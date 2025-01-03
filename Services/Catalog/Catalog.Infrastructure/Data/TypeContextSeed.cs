namespace Catalog.Infrastructure.Data;

using System.Text.Json;
using Core.Entities;
using MongoDB.Driver;

public static class TypeContextSeed
{
    public static void SeedData(IMongoCollection<ProductType> productTypeCollection)
    {
        var checkTypes = productTypeCollection.Find(b => true).Any();
        var path = Path.Combine("Data", "SeedData", "types.json");

        if (checkTypes)
            return;

        var productTypesData = File.ReadAllText(path);
        // var productTypesData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/types.json");
        var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);

        if (productTypes == null)
            return;

        foreach (var productType in productTypes)
        {
            productTypeCollection.InsertOneAsync(productType);
        }
    }
}