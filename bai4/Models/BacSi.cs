namespace bai4.Models
{
    public partial class BacSi
    {
        public int MaBS { get; set; }
        public string HoTen { get; set; } = null!;
        public string? ChuyenKhoa { get; set; }
        public string? SDT { get; set; }

        public virtual ICollection<LichKham> LichKhams { get; set; } = new List<LichKham>();
    }
}