using backend.models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace backend.controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMongoCollection<User> _users;

    public UsersController(IMongoDatabase database)
    {
        _users = database.GetCollection<User>("users");
    }

    // Tüm kullanıcıları listele
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _users.Find(_ => true).ToListAsync();
        return Ok(users);
    }

    // Rol güncelleme
    [HttpPut("{id}/role")]
    public async Task<IActionResult> UpdateRole(string id, [FromBody] string newRole)
    {
        var update = Builders<User>.Update.Set(u => u.role, newRole);
        var result = await _users.UpdateOneAsync(u => u.id == id, update);
        
        if (result.MatchedCount == 0) return NotFound();
        return Ok(new { message = "Yetki başarıyla güncellendi." });
    }
    // controllers/UsersController.cs içine ekleyin

// 1. Kullanıcı Silme
[HttpDelete("{id}")]
public async Task<IActionResult> Delete(string id)
{
    var result = await _users.DeleteOneAsync(u => u.id == id);
    if (result.DeletedCount == 0) return NotFound();
    return Ok(new { message = "Kullanıcı başarıyla silindi." });
}

// 2. Kullanıcı Bilgilerini Güncelleme (Ad, E-posta vs.)
[HttpPut("{id}")]
public async Task<IActionResult> Update(string id, [FromBody] User updatedUser)
{
    var result = await _users.ReplaceOneAsync(u => u.id == id, updatedUser);
    if (result.MatchedCount == 0) return NotFound();
    return Ok(updatedUser);
}

// 3. Yeni Kullanıcı Ekleme
[HttpPost]
public async Task<IActionResult> Create([FromBody] User newUser)
{
    await _users.InsertOneAsync(newUser);
    return Ok(newUser);
}
}