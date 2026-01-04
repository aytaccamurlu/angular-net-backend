using backend.models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace backend.controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IMongoCollection<Order> _orders;

    // Program.cs'deki db nesnesini buraya enjekte ediyoruz
    public OrdersController(IMongoDatabase database)
    {
        // Koleksiyon adı 'orders' olmalı
        _orders = database.GetCollection<Order>("orders");
    }

    // 1. Siparişleri Listele
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var list = await _orders.Find(_ => true).ToListAsync();
        return Ok(list);
    }

    // 2. Sipariş Kaydet
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Order order)
    {
        // Sunucu tarafında tarih damgası vuruyoruz
        order.orderNumber = "ORD-" + DateTime.Now.Ticks.ToString().Substring(10);
        order.orderDate = DateTime.Now;

        await _orders.InsertOneAsync(order);
        return Ok(new { message = "Sipariş başarıyla kaydedildi!", orderNumber = order.orderNumber });
    }
}