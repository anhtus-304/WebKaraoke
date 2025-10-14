namespace WebKaraoke.Data.Entities
{
    public class ChiTietHoaDon
    {
        public int HoaDonID { get; set; } // Renamed from CTHD_ID
        public int MonID { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }
}