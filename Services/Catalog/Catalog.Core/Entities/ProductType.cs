namespace Catalog.Core.Entities;

using MongoDB.Bson.Serialization.Attributes;

public class ProductType : BaseEntity
{
    [BsonElement("Name")]
    public required string Name { get; set; }
}