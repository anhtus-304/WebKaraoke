namespace WebKaraoke.Data.Entities
{
    public class DatPhong
    {
        public int DatPhongID { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime GioKetThuc { get; set; }
        public int SoLuongNguoi { get; set; }
        public string TrangThai { get; set; }
        // Note: You will need to add foreign keys here later for KhachHangID and PhongID.
    }
}