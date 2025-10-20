namespace WebKaraoke.Data.Models
{
    public class MonAnNuocUong
    {
        public int MonID { get; set; }
        public string TenMon { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public string DanhMuc { get; set; } = string.Empty;
    }
}