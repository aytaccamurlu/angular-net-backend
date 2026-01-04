using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.models;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? id { get; set; }

    public string? customerId { get; set; }
    public string? customerName { get; set; }
    
    // HATA BURADAYDI: Bu alanları ekledik
    public string? orderNumber { get; set; } 
    public DateTime orderDate { get; set; }

    public List<OrderItem> items { get; set; } = new();
    public decimal totalAmount { get; set; }
}

public class OrderItem
{
    public string? productId { get; set; }
    public string? productName { get; set; }
    public int quantity { get; set; }
    public decimal price { get; set; }
}