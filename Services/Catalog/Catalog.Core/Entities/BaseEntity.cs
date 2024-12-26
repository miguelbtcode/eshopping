namespace Catalog.Core.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }
}