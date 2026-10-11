using Microsoft.EntityFrameworkCore;

namespace bai2.Models
{
    public partial class FitZoneContext : DbContext
    {
        public FitZoneContext()
        {
        }

        public FitZoneContext(DbContextOptions<FitZoneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HoiVien> HoiViens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-8M2Q8KB\\SQLEXPRESS;Database=FitZone;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoiVien>(entity =>
            {
                entity.ToTable("HoiVien");
                entity.HasKey(e => e.MaHV);

                entity.Property(e => e.HoTen).HasMaxLength(100);
                entity.Property(e => e.NgaySinh).HasColumnType("date");
                entity.Property(e => e.SDT).HasMaxLength(15).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.HangThanhVien).HasMaxLength(20);
                entity.Property(e => e.NgayDangKy)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("(getdate())");
\            });
        }
    }
}