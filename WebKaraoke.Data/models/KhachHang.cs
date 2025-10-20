namespace WebKaraoke.Data.Models
{
    public class KhachHang
    {
        public int KhachHangID { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
    }
}