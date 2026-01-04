using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using backend.models;

namespace backend.controllers;

[ApiController]
[Route("api/auth")] // Rotayı elle sabitledik
public class AuthController : ControllerBase // İsmi 'AuthController' olarak düzelttik
{
    private readonly IMongoCollection<User> _users;

    public AuthController(IMongoDatabase database)
    {
        _users = database.GetCollection<User>("users");
    }

    [HttpGet("check")]
    public IActionResult check()
    {
        return Ok("Backend sonunda cevap verdi!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> login([FromBody] dynamic login_data)
{
    string email = login_data.GetProperty("email").GetString();
    string password = login_data.GetProperty("password").GetString();

    var user = await _users.Find(u => u.email == email && u.password == password).FirstOrDefaultAsync();
    
    if (user == null) return Unauthorized("Hatalı giriş");
    
    return Ok(user);
}
    [HttpPost("register")]
    public async Task<IActionResult> register([FromBody] User new_user)
    {
        // 1. Aynı e-posta ile başka bir kullanıcı var mı kontrol et
        var existingUser = await _users.Find(u => u.email == new_user.email).FirstOrDefaultAsync();
        if (existingUser != null) return BadRequest("Bu e-posta adresi zaten kayıtlı!");

        // 2. Kullanıcıyı MongoDB'ye kaydet
        await _users.InsertOneAsync(new_user);
        
        return Ok(new { message = "Kayıt başarılı", user = new_user });
    }
}