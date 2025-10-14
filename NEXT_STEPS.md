# Các Bước Tiếp Theo (Next Steps) 🎯

## ✅ Đã Hoàn Thành

- ✅ Đã sửa phiên bản .NET framework (từ net10.0 sang net9.0)
- ✅ Đã cài đặt Entity Framework Core packages
- ✅ Đã thêm connection string vào `appsettings.json`
- ✅ Dự án đã build thành công!

## 🎯 Các Bước Tiếp Theo

### 1. Tạo Database Models (Entity Classes)

Tạo các entity classes trong thư mục `WebKaraoke.Data`:

```csharp
// WebKaraoke.Data/Entities/Room.cs
namespace WebKaraoke.Data.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;  // VIP, Thường
    public int Capacity { get; set; }
    public decimal PricePerHour { get; set; }
    public string Status { get; set; } = "Available";  // Available, Occupied, Maintenance
}

// WebKaraoke.Data/Entities/Booking.cs
namespace WebKaraoke.Data.Entities;

public class Booking
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public DateTime BookingTime { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Status { get; set; } = "Pending";  // Pending, Confirmed, Completed, Cancelled
    
    public Room? Room { get; set; }
}

// WebKaraoke.Data/Entities/MenuItem.cs
namespace WebKaraoke.Data.Entities;

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;  // Đồ uống, Đồ ăn
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; } = true;
}

// WebKaraoke.Data/Entities/Order.cs
namespace WebKaraoke.Data.Entities;

public class Order
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public DateTime OrderTime { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";  // Pending, Preparing, Served, Paid
    
    public Booking? Booking { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
}

// WebKaraoke.Data/Entities/OrderItem.cs
namespace WebKaraoke.Data.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    
    public Order? Order { get; set; }
    public MenuItem? MenuItem { get; set; }
}
```

### 2. Tạo DbContext

Tạo file `WebKaraoke.Data/ApplicationDbContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data.Entities;

namespace WebKaraoke.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships and constraints
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Room)
            .WithMany()
            .HasForeignKey(b => b.RoomId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Booking)
            .WithMany()
            .HasForeignKey(o => o.BookingId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.MenuItem)
            .WithMany()
            .HasForeignKey(oi => oi.MenuItemId);

        // Seed initial data
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, Name = "Phòng VIP 1", Type = "VIP", Capacity = 10, PricePerHour = 200000, Status = "Available" },
            new Room { Id = 2, Name = "Phòng VIP 2", Type = "VIP", Capacity = 15, PricePerHour = 250000, Status = "Available" },
            new Room { Id = 3, Name = "Phòng Thường 1", Type = "Thường", Capacity = 6, PricePerHour = 100000, Status = "Available" },
            new Room { Id = 4, Name = "Phòng Thường 2", Type = "Thường", Capacity = 8, PricePerHour = 120000, Status = "Available" }
        );

        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem { Id = 1, Name = "Coca Cola", Category = "Đồ uống", Price = 15000, IsAvailable = true },
            new MenuItem { Id = 2, Name = "Pepsi", Category = "Đồ uống", Price = 15000, IsAvailable = true },
            new MenuItem { Id = 3, Name = "Bia Heineken", Category = "Đồ uống", Price = 25000, IsAvailable = true },
            new MenuItem { Id = 4, Name = "Mực chiên", Category = "Đồ ăn", Price = 50000, IsAvailable = true },
            new MenuItem { Id = 5, Name = "Khoai tây chiên", Category = "Đồ ăn", Price = 30000, IsAvailable = true }
        );
    }
}
```

### 3. Cấu hình Dependency Injection trong Program.cs

Thêm vào file `WebKaraoke.API/Program.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors();

// Add your API endpoints here

app.Run();
```

### 4. Tạo và Chạy Database Migrations

```bash
# Di chuyển vào thư mục API
cd WebKaraoke.API

# Tạo migration đầu tiên
dotnet ef migrations add InitialCreate --project ../WebKaraoke.Data

# Cập nhật database
dotnet ef database update
```

### 5. Tạo Repositories và Services

#### Repository Pattern (trong WebKaraoke.Data)

```csharp
// WebKaraoke.Data/Repositories/IRepository.cs
namespace WebKaraoke.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

// WebKaraoke.Data/Repositories/Repository.cs
using Microsoft.EntityFrameworkCore;

namespace WebKaraoke.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
```

#### Business Services (trong WebKaraoke.Business)

```csharp
// WebKaraoke.Business/Services/IRoomService.cs
using WebKaraoke.Data.Entities;

namespace WebKaraoke.Business.Services;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByIdAsync(int id);
    Task<Room> CreateRoomAsync(Room room);
    Task UpdateRoomAsync(Room room);
    Task DeleteRoomAsync(int id);
    Task<IEnumerable<Room>> GetAvailableRoomsAsync();
}

// WebKaraoke.Business/Services/RoomService.cs
using WebKaraoke.Data.Entities;
using WebKaraoke.Data.Repositories;

namespace WebKaraoke.Business.Services;

public class RoomService : IRoomService
{
    private readonly IRepository<Room> _roomRepository;

    public RoomService(IRepository<Room> roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return await _roomRepository.GetAllAsync();
    }

    public async Task<Room?> GetRoomByIdAsync(int id)
    {
        return await _roomRepository.GetByIdAsync(id);
    }

    public async Task<Room> CreateRoomAsync(Room room)
    {
        return await _roomRepository.AddAsync(room);
    }

    public async Task UpdateRoomAsync(Room room)
    {
        await _roomRepository.UpdateAsync(room);
    }

    public async Task DeleteRoomAsync(int id)
    {
        await _roomRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync()
    {
        var rooms = await _roomRepository.GetAllAsync();
        return rooms.Where(r => r.Status == "Available");
    }
}
```

### 6. Tạo API Controllers

Xóa code mẫu WeatherForecast trong `Program.cs` và tạo các controllers:

```csharp
// WebKaraoke.API/Controllers/RoomsController.cs
using Microsoft.AspNetCore.Mvc;
using WebKaraoke.Business.Services;
using WebKaraoke.Data.Entities;

namespace WebKaraoke.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
    {
        var rooms = await _roomService.GetAllRoomsAsync();
        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> GetRoom(int id)
    {
        var room = await _roomService.GetRoomByIdAsync(id);
        if (room == null)
            return NotFound();
        
        return Ok(room);
    }

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<Room>>> GetAvailableRooms()
    {
        var rooms = await _roomService.GetAvailableRoomsAsync();
        return Ok(rooms);
    }

    [HttpPost]
    public async Task<ActionResult<Room>> CreateRoom(Room room)
    {
        var createdRoom = await _roomService.CreateRoomAsync(room);
        return CreatedAtAction(nameof(GetRoom), new { id = createdRoom.Id }, createdRoom);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(int id, Room room)
    {
        if (id != room.Id)
            return BadRequest();

        await _roomService.UpdateRoomAsync(room);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        await _roomService.DeleteRoomAsync(id);
        return NoContent();
    }
}
```

### 7. Cập nhật Program.cs để thêm Controllers

```csharp
// Thêm vào Program.cs
builder.Services.AddControllers();

// Đăng ký repositories và services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRoomService, RoomService>();

// Trong phần configure
app.MapControllers();
```

### 8. Tạo Frontend HTML

Tạo file `WebKaraoke.Client/index.html` để test API:

```html
<!DOCTYPE html>
<html lang="vi">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>WebKaraoke - Quản lý phòng karaoke</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f5f5f5;
        }
        h1 {
            color: #333;
            text-align: center;
        }
        .rooms-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
            gap: 20px;
            margin-top: 20px;
        }
        .room-card {
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .room-card h3 {
            margin-top: 0;
            color: #2c3e50;
        }
        .room-status {
            display: inline-block;
            padding: 4px 12px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: bold;
        }
        .available {
            background-color: #2ecc71;
            color: white;
        }
        .occupied {
            background-color: #e74c3c;
            color: white;
        }
    </style>
</head>
<body>
    <h1>🎤 WebKaraoke Management System</h1>
    <div id="rooms-container" class="rooms-grid"></div>

    <script>
        const API_URL = 'https://localhost:7000/api';  // Thay đổi port theo cấu hình

        async function loadRooms() {
            try {
                const response = await fetch(`${API_URL}/rooms`);
                const rooms = await response.json();
                
                const container = document.getElementById('rooms-container');
                container.innerHTML = rooms.map(room => `
                    <div class="room-card">
                        <h3>${room.name}</h3>
                        <p><strong>Loại:</strong> ${room.type}</p>
                        <p><strong>Sức chứa:</strong> ${room.capacity} người</p>
                        <p><strong>Giá:</strong> ${room.pricePerHour.toLocaleString('vi-VN')} VNĐ/giờ</p>
                        <p><span class="room-status ${room.status.toLowerCase()}">${room.status}</span></p>
                    </div>
                `).join('');
            } catch (error) {
                console.error('Error loading rooms:', error);
            }
        }

        loadRooms();
    </script>
</body>
</html>
```

### 9. Chạy và Test Application

```bash
# Chạy API
cd WebKaraoke.API
dotnet run

# API sẽ chạy trên: https://localhost:xxxx (xem trong output)
# Test API endpoints:
# - GET https://localhost:xxxx/api/rooms
# - GET https://localhost:xxxx/api/rooms/available
# - GET https://localhost:xxxx/api/rooms/1
```

### 10. Các Tính Năng Nên Thêm Tiếp

- [ ] Authentication & Authorization (JWT)
- [ ] Validation cho các models
- [ ] Error handling middleware
- [ ] Logging
- [ ] Unit tests
- [ ] API documentation với Swagger
- [ ] Booking management APIs
- [ ] Order management APIs
- [ ] Payment integration
- [ ] Real-time updates với SignalR

## 📚 Tài Liệu Tham Khảo

- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/)
- [Repository Pattern](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)

## 💡 Tips

1. Luôn kiểm tra connection string trong `appsettings.json` phù hợp với SQL Server của bạn
2. Sử dụng `dotnet watch run` để tự động reload khi code thay đổi
3. Kiểm tra database bằng SQL Server Management Studio hoặc Azure Data Studio
4. Sử dụng Postman hoặc Thunder Client để test API endpoints

Chúc bạn code vui vẻ! 🚀
