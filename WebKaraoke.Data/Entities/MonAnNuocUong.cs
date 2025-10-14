using System.ComponentModel.DataAnnotations; // 1. Add this line

namespace WebKaraoke.Data.Entities
{
    public class MonAnNuocUong
    {
        [Key] // 2. Add this attribute to mark the primary key
        public int MonID { get; set; }
        public string? TenMon { get; set; }
        public decimal DonGia { get; set; }
        public string? DanhMuc { get; set; }
    }
}