using System;
using System.Collections.Generic;

namespace WebKaraoke.Data.Models
{
    public class HoaDon
    {
        public int HoaDonID { get; set; }
        public int DatPhongID { get; set; }
        public int NhanVienID { get; set; }
        public int? KM_ID { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }

        // ✅ Một hóa đơn có thể có nhiều chi tiết
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

        // ✅ Khóa ngoại
        public DatPhong DatPhong { get; set; } = null!;
        public NhanVien NhanVien { get; set; } = null!;
        public KhuyenMai? KhuyenMai { get; set; }
    }
}
