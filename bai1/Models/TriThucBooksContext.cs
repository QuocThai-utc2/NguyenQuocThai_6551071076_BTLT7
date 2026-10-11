using Microsoft.EntityFrameworkCore;

namespace bai1.Models
{
    public partial class TriThucBooksContext : DbContext
    {
        public TriThucBooksContext()
        {
        }

        public TriThucBooksContext(DbContextOptions<TriThucBooksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TheLoaiSach> TheLoaiSaches { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-8M2Q8KB\\SQLEXPRESS;Database=TriThucBooks;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TheLoaiSach>(entity =>
            {
                entity.ToTable("TheLoaiSach");
                entity.HasKey(e => e.MaTL);

                entity.Property(e => e.MaTL).HasColumnName("MaTL");
                entity.Property(e => e.TenTheLoai).HasMaxLength(100);
                entity.Property(e => e.MoTa).HasMaxLength(255);
                entity.Property(e => e.SoLuongSach).HasDefaultValue(0);
                entity.Property(e => e.NgayTao)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("(getdate())");
            });
        }
    }
}