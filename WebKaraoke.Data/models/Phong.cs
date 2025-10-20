namespace WebKaraoke.Data.Models
{
    public class Phong
    {
        public int PhongID { get; set; }
        public string TenPhong { get; set; } = string.Empty;
        public string LoaiPhong { get; set; } = string.Empty;
        public decimal GiaGio { get; set; }
        public string TrangThai { get; set; } = string.Empty;
    }
}