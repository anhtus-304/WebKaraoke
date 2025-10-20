using System;

namespace WebKaraoke.Data.Models
{
    public class KhuyenMai
    {
        public int KM_ID { get; set; }
        public string MaKM { get; set; } = string.Empty;
        public decimal TyLeGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }
}