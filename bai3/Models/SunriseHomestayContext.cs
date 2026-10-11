using Microsoft.EntityFrameworkCore;

namespace bai3.Models
{
    public partial class SunriseHomestayContext : DbContext
    {
        public SunriseHomestayContext()
        {
        }

        public SunriseHomestayContext(DbContextOptions<SunriseHomestayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; } = null!;
        public virtual DbSet<Phong> Phongs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-8M2Q8KB\\SQLEXPRESS;Database=SunriseHomestay;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoaiPhong>(entity =>
            {
                entity.ToTable("LoaiPhong");
                entity.HasKey(e => e.MaLoai);
                entity.Property(e => e.TenLoai).HasMaxLength(100);
                entity.Property(e => e.GiaMoiDem).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.MoTa).HasMaxLength(255);
            });

            modelBuilder.Entity<Phong>(entity =>
            {
                entity.ToTable("Phong");
                entity.HasKey(e => e.MaPhong);
                entity.Property(e => e.SoPhong).HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.TinhTrang).HasMaxLength(20);
                entity.Property(e => e.HinhAnh).HasMaxLength(255);

                entity.HasOne(p => p.MaLoaiNavigation)
                      .WithMany(l => l.Phongs)
                      .HasForeignKey(p => p.MaLoai)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}