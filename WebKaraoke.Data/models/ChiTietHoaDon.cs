namespace WebKaraoke.Data.Models
{
    public class ChiTietHoaDon
    {
        public int CTHD_ID { get; set; }
        public int HoaDonID { get; set; }
        public int MonID { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        
        public HoaDon HoaDon { get; set; } = null!;
        public MonAnNuocUong MonAnNuocUong { get; set; } = null!;
    }
}