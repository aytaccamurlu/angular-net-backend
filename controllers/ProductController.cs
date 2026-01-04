using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using backend.models;

namespace backend.controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IMongoCollection<Product> _products;

    public ProductController(IMongoDatabase database)
    {
        _products = database.GetCollection<Product>("products");
    }

    // 1. Tüm Ürünleri Getir
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _products.Find(_ => true).ToListAsync();
        return Ok(list);
    }

    // 2. Yeni Ürün Ekle
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        await _products.InsertOneAsync(product);
        return Ok(product);
    }

    // 3. Ürünü Güncelle
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Product updatedProduct)
    {
        // MongoDB'deki string id ile eşleştirip güncelleme yapıyoruz
        var result = await _products.ReplaceOneAsync(p => p.id == id, updatedProduct);
        
        if (result.MatchedCount == 0) return NotFound("Ürün bulunamadı.");
        
        return Ok(updatedProduct);
    }

    // 4. Ürünü Sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _products.DeleteOneAsync(p => p.id == id);
        
        if (result.DeletedCount == 0) return NotFound("Ürün bulunamadı.");
        
        return Ok(new { message = "Ürün başarıyla silindi." });
    }
}