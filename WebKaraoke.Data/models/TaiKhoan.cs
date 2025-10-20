namespace WebKaraoke.Data.Models
{
    public class TaiKhoan
    {
        public int TaiKhoanID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string MatKhauHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? KhachHangID { get; set; }
        public int? NhanVienID { get; set; }
        
        public KhachHang? KhachHang { get; set; }
        public NhanVien? NhanVien { get; set; }
    }
}