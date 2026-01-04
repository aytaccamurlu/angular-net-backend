using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.models;

public class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? id { get; set; }
    public string companyName { get; set; } = string.Empty;
    public string contactName { get; set; } = string.Empty;
    public string city { get; set; } = string.Empty;
}