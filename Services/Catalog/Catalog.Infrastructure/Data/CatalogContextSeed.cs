namespace Catalog.Infrastructure.Data;

using System.Text.Json;
using Core.Entities;
using MongoDB.Driver;

public static class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        var checkProducts = productCollection.Find(b => true).Any();
        var path = Path.Combine("Data", "SeedData", "products.json");

        if (checkProducts)
            return;

        var productsData = File.ReadAllText(path);
        var products = JsonSerializer.Deserialize<List<Product>>(productsData);

        if (products == null)
            return;

        foreach (var product in products)
        {
            productCollection.InsertOneAsync(product);
        }
    }
}