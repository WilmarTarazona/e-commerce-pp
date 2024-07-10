using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace e_commerce_pp.Models;
public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public required string Item { get; set; }
    public string? Description { get; set; }
    public required int Price { get; set; }
} 