using Microsoft.EntityFrameworkCore;

namespace bai4.Models
{
    public partial class AnKhangClinicContext : DbContext
    {
        public AnKhangClinicContext()
        {
        }

        public AnKhangClinicContext(DbContextOptions<AnKhangClinicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BacSi> BacSis { get; set; } = null!;
        public virtual DbSet<LichKham> LichKhams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-8M2Q8KB\\SQLEXPRESS;Database=AnKhangClinic;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BacSi>(entity =>
            {
                entity.ToTable("BacSi");
                entity.HasKey(e => e.MaBS);
                entity.Property(e => e.HoTen).HasMaxLength(100);
                entity.Property(e => e.ChuyenKhoa).HasMaxLength(100);
                entity.Property(e => e.SDT).HasMaxLength(15).IsUnicode(false);
            });

            modelBuilder.Entity<LichKham>(entity =>
            {
                entity.ToTable("LichKham");
                entity.HasKey(e => e.MaLich);
                entity.Property(e => e.TenBenhNhan).HasMaxLength(100);
                entity.Property(e => e.SDT).HasMaxLength(15).IsUnicode(false);
                entity.Property(e => e.NgayKham).HasColumnType("date");
                entity.Property(e => e.GioKham).HasMaxLength(10);
                entity.Property(e => e.TrangThai).HasMaxLength(20);

                entity.HasOne(l => l.MaBSNavigation)
                      .WithMany(b => b.LichKhams)
                      .HasForeignKey(l => l.MaBS)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}