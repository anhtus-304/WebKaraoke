namespace WebKaraoke.Data.Entities
{
    public class HoaDon
    {
        public int HoaDonID { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        // Note: Identifier_1 seems like a placeholder in your diagram.
        // You might need to add foreign keys here later, like NhanVienID or DatPhongID.
    }
}