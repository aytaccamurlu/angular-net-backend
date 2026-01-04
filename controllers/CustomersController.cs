using backend.models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace backend.controllers;

[ApiController]
[Route("api/customers")] // Angular servisindeki /api/customers rotasıyla eşleşir
public class CustomersController : ControllerBase
{
    private readonly IMongoCollection<Customer> _customers;

    // HATA BURADAYDI: Burası IMongoDatabase olmalı!
    public CustomersController(IMongoDatabase database)
    {
        _customers = database.GetCollection<Customer>("customers");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _customers.Find(_ => true).ToListAsync();
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Customer customer)
    {
        await _customers.InsertOneAsync(customer);
        return Ok(customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Customer updatedCustomer)
    {
        var result = await _customers.ReplaceOneAsync(c => c.id == id, updatedCustomer);
        if (result.MatchedCount == 0) return NotFound();
        return Ok(updatedCustomer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _customers.DeleteOneAsync(c => c.id == id);
        if (result.DeletedCount == 0) return NotFound();
        return Ok(new { message = "Silindi" });
    }
}