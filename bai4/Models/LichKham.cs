namespace bai4.Models
{
    public partial class LichKham
    {
        public int MaLich { get; set; }
        public string TenBenhNhan { get; set; } = null!;
        public string? SDT { get; set; }
        public DateTime NgayKham { get; set; }
        public string GioKham { get; set; } = null!;
        public int MaBS { get; set; }
        public string TrangThai { get; set; } = null!;

        public virtual BacSi MaBSNavigation { get; set; } = null!;
    }
}