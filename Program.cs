using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Bağlantısı
var uri = "mongodb+srv://aytaccamurlu26_db_user:3HwWLyyOSY1Stvaj@cluster0.vg96nxd.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
var client = new MongoClient(uri);
var db = client.GetDatabase("b2b_erp_db");

builder.Services.AddSingleton(db);

// Controller'ları ve JSON ayarlarını ekle
builder.Services.AddControllers();

var app = builder.Build();

// Rota haritasını çıkar
app.UseRouting();

// CORS Ayarı (Frontend için şart)
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers(); // Rotaları bağla

app.Run();