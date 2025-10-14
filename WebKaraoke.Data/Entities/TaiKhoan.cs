namespace WebKaraoke.Data.Entities
{
    public class TaiKhoan
    {
        public int TaiKhoanID { get; set; }
        public string Username { get; set; }
        public string MatKhauHash { get; set; } // Storing the hashed password
        public string Role { get; set; }
        // Note: You will need to add foreign keys here for KhachHangID or NhanVienID.
    }
}