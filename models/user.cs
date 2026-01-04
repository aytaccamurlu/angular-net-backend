using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.models;

public class User // Baş harfi büyük yaparak CS8981 hatasını giderdik
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? id { get; set; }

    [BsonElement("email")]
    public string email { get; set; } = null!;

    [BsonElement("password")]
    public string password { get; set; } = null!;

    [BsonElement("full_name")]
    public string full_name { get; set; } = null!;

    [BsonElement("role")]
    public string role { get; set; } = "dealer";
}