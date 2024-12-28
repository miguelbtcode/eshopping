namespace Catalog.Core.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Product : BaseEntity
{
    [BsonElement("Name")]
    public required string Name { get; set; }
    public required string Summary { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageFile { get; set; } = string.Empty;
    public ProductBrand? Brand { get; set; }
    public ProductType? Type { get; set; }
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
}