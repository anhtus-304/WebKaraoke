using System;
using System.Collections.Generic;

namespace WebKaraoke.Data.Models
{
    public class DatPhong
    {
        public int DatPhongID { get; set; }
        public int KhachHangID { get; set; }
        public int PhongID { get; set; }
        public DateTime ThoiGianDat { get; set; }
        public DateTime GioBatDau { get; set; }
        public DateTime GioKetThuc { get; set; }
        public int SoLuongNguoi { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        // ✅ Một đặt phòng có thể có nhiều hóa đơn
        public ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

        // ✅ Khóa ngoại
        public KhachHang KhachHang { get; set; } = null!;
        public Phong Phong { get; set; } = null!;
    }
}
