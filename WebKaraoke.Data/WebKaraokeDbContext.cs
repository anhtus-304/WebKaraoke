using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data.Entities;

namespace WebKaraoke.Data
{
    public class WebKaraokeDbContext : DbContext
    {
        public WebKaraokeDbContext(DbContextOptions<WebKaraokeDbContext> options) : base(options)
        {
        }

        // ADD THIS LINE
        public DbSet<Phong> Phongs { get; set; }

        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<DatPhong> DatPhongs { get; set; }
        public DbSet<MonAnNuocUong> MonAnNuocUongs { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHoaDon>()
                .HasKey(ct => new { ct.HoaDonID, ct.MonID });
        }
    }
}