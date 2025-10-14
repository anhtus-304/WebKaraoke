using System.ComponentModel.DataAnnotations; // 1. Add this line

namespace WebKaraoke.Data.Entities
{
    public class KhuyenMai
    {
        [Key] // 2. Add this attribute
        public int KM_ID { get; set; }
        public string? MaKM { get; set; }
        public decimal TyLeGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }
}