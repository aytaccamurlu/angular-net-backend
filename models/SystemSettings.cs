using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization; // JsonPropertyName için bu şart!

namespace backend.models;

public class SystemSettings
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? id { get; set; }

    [BsonElement("companyName")]
    public string companyName { get; set; } = string.Empty;

    [BsonElement("taxInfo")]
    public string? taxInfo { get; set; }

    [BsonElement("currency")]
    public string currency { get; set; } = "TRY";
}