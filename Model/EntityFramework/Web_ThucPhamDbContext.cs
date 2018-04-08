namespace Model.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Web_ThucPhamDbContext : DbContext
    {
        public Web_ThucPhamDbContext()
            : base("name=Web_ThucPhamDbContext")
        {
        }

        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<DanhmucSanPham> DanhmucSanPhams { get; set; }
        public virtual DbSet<DanhmucTinTuc> DanhmucTinTucs { get; set; }
        public virtual DbSet<DatHang> DatHangs { get; set; }
        public virtual DbSet<LienHe> LienHes { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<QuangCao> QuangCaos { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }
        public virtual DbSet<ChiTietDatHang> ChiTietDatHangs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanhmucSanPham>()
                .HasMany(e => e.DanhmucSanPham1)
                .WithOptional(e => e.DanhmucSanPham2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<DanhmucSanPham>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.DanhmucSanPham)
                .HasForeignKey(e => e.IdDanhMucSanPham);

            modelBuilder.Entity<DanhmucTinTuc>()
                .HasMany(e => e.DanhmucTinTuc1)
                .WithOptional(e => e.DanhmucTinTuc2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<DanhmucTinTuc>()
                .HasMany(e => e.TinTucs)
                .WithOptional(e => e.DanhmucTinTuc)
                .HasForeignKey(e => e.IdDanhMucTinTuc);

            modelBuilder.Entity<DatHang>()
                .HasMany(e => e.ChiTietDatHangs)
                .WithRequired(e => e.DatHang)
                .HasForeignKey(e => e.IdDatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.GiaSanPham)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.GiaKhuyenMai)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.BinhLuans)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.IdSanPham);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDatHangs)
                .WithRequired(e => e.SanPham)
                .HasForeignKey(e => e.IdSanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.BinhLuans)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.IdNguoiBluan);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.QuangCaos)
                .WithRequired(e => e.TaiKhoan)
                .HasForeignKey(e => e.IdNguoiTao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.IdNguoiTao);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.Slides)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.IdNguoiTao);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.TinTucs)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.IdNguoiTao);

            modelBuilder.Entity<TinTuc>()
                .HasMany(e => e.BinhLuans)
                .WithOptional(e => e.TinTuc)
                .HasForeignKey(e => e.IdTinTuc);

            modelBuilder.Entity<ChiTietDatHang>()
                .Property(e => e.GiaSanPham)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietDatHang>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);
        }
    }
}
