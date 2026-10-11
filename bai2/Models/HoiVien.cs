namespace bai2.Models
{
    public partial class HoiVien
    {
        public int MaHV { get; set; }
        public string HoTen { get; set; } = null!;
        public bool GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string? SDT { get; set; }
        public string? Email { get; set; }
        public string HangThanhVien { get; set; } = null!;
        public DateTime NgayDangKy { get; set; }
        public bool TrangThai { get; set; }
    }
}