using Microsoft.EntityFrameworkCore;
using WebKaraoke.Data.Models;

namespace WebKaraoke.Data
{
    public class WebKaraokeDbContext : DbContext
    {
        public WebKaraokeDbContext(DbContextOptions<WebKaraokeDbContext> options) 
            : base(options)
        {
        }

        // 🔥 QUAN TRỌNG: THÊM TẤT CẢ CÁC DBSET PROPERTIES
        public DbSet<KhachHang> KhachHang { get; set; } = null!;
        public DbSet<Phong> Phongs { get; set; } = null!;
        public DbSet<DatPhong> DatPhong { get; set; } = null!;
        public DbSet<HoaDon> HoaDon { get; set; } = null!;
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; } = null!;
        public DbSet<MonAnNuocUong> MonAnNuocUong { get; set; } = null!;
        public DbSet<KhuyenMai> KhuyenMai { get; set; } = null!;
        public DbSet<NhanVien> NhanVien { get; set; } = null!;
        public DbSet<TaiKhoan> TaiKhoan { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔥 CẤU HÌNH PRIMARY KEYS
            modelBuilder.Entity<KhachHang>().HasKey(k => k.KhachHangID);
            modelBuilder.Entity<Phong>().HasKey(p => p.PhongID);
            modelBuilder.Entity<DatPhong>().HasKey(d => d.DatPhongID);
            modelBuilder.Entity<HoaDon>().HasKey(h => h.HoaDonID);
            modelBuilder.Entity<ChiTietHoaDon>().HasKey(c => c.CTHD_ID);
            modelBuilder.Entity<MonAnNuocUong>().HasKey(m => m.MonID);
            modelBuilder.Entity<KhuyenMai>().HasKey(k => k.KM_ID);
            modelBuilder.Entity<NhanVien>().HasKey(n => n.NhanVienID);
            modelBuilder.Entity<TaiKhoan>().HasKey(t => t.TaiKhoanID);

            // Cấu hình table names
            modelBuilder.Entity<KhachHang>().ToTable("KhachHang");
            modelBuilder.Entity<Phong>().ToTable("Phong");
            modelBuilder.Entity<DatPhong>().ToTable("DatPhong");
            modelBuilder.Entity<HoaDon>().ToTable("HoaDon");
            modelBuilder.Entity<ChiTietHoaDon>().ToTable("ChiTietHoaDon");
            modelBuilder.Entity<MonAnNuocUong>().ToTable("MonAnNuocUong");
            modelBuilder.Entity<KhuyenMai>().ToTable("KhuyenMai");
            modelBuilder.Entity<NhanVien>().ToTable("NhanVien");
            modelBuilder.Entity<TaiKhoan>().ToTable("TaiKhoan");
        }
    }
}
