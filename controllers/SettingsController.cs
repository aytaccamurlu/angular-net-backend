using backend.models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace backend.controllers;

[ApiController]
[Route("api/settings")]
public class SettingsController : ControllerBase
{
    private readonly IMongoCollection<SystemSettings> _settings;

    public SettingsController(IMongoDatabase database)
    {
        _settings = database.GetCollection<SystemSettings>("settings");
    }

    // Mevcut ayarları getir (Kullanıcı listesi gibi tümünü döner)
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var allSettings = await _settings.Find(_ => true).ToListAsync();
    return Ok(allSettings);

        // Liste doluysa tüm listeyi döndür
        
    }

    // Ayarları kaydet veya güncelle
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] SystemSettings settings)
    {
        if (string.IsNullOrEmpty(settings.id))
        {
            // Yeni kayıt için ID'yi null yapıyoruz (MongoDB otomatik atasın)
            settings.id = null;
            await _settings.InsertOneAsync(settings);
        }
        else
        {
            // Mevcut ID varsa güncelle
            await _settings.ReplaceOneAsync(s => s.id == settings.id, settings);
        }
        
        return Ok(settings);
    }
}