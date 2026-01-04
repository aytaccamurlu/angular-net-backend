using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.models;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? id { get; set; }
    public string name { get; set; } = string.Empty;
    public string category { get; set; } = string.Empty;
    public decimal price { get; set; }
    public int stock { get; set; }
    public string description { get; set; } = string.Empty;
}