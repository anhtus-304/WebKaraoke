using Microsoft.EntityFrameworkCore;
using WebKaraoke.Business.Services;
using WebKaraoke.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebKaraokeDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Register Your Business Services
builder.Services.AddScoped<PhongService>();

// 3. Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Configure the HTTP request pipeline (Swagger UI)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();