using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Lấy chuỗi kết nối từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2️⃣ Đăng ký DbContext với SQL Server
builder.Services.AddDbContext<WebKaraokeDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// 3️⃣ Đăng ký các service khác
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4️⃣ Swagger và pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 5️⃣ Endpoint kiểm tra
app.MapGet("/", () => "🚀 WebKaraoke API is running!");
app.MapGet("/test", () => new
{
    message = "✅ API is working!",
    timestamp = DateTime.Now
});

app.Run();
