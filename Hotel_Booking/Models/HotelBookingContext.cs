using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Hotel_Booking.Models
{
    public partial class HotelBookingContext : DbContext
    {
        public HotelBookingContext()
            : base("name=HotelBookingContext")
        {
        }

        public virtual DbSet<DatPhong> DatPhongs { get; set; }
        public virtual DbSet<KhachSan> KhachSans { get; set; }
        public virtual DbSet<LoaiKhachSan> LoaiKhachSans { get; set; }
        public virtual DbSet<Phong> Phongs { get; set; }
        public virtual DbSet<QuyenTruyCap> QuyenTruyCaps { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ThanhPho> ThanhPhoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatPhong>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<KhachSan>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<KhachSan>()
                .Property(e => e.UrlHinhAnh1)
                .IsUnicode(false);

            modelBuilder.Entity<KhachSan>()
                .Property(e => e.UrlHinhAnh2)
                .IsUnicode(false);

            modelBuilder.Entity<KhachSan>()
                .Property(e => e.UrlHinhAnh3)
                .IsUnicode(false);

            modelBuilder.Entity<KhachSan>()
                .Property(e => e.UrlHinhAnh4)
                .IsUnicode(false);

            modelBuilder.Entity<KhachSan>()
                .HasMany(e => e.Phongs)
                .WithOptional(e => e.KhachSan)
                .HasForeignKey(e => e.IdKhachSan);

            modelBuilder.Entity<LoaiKhachSan>()
                .Property(e => e.UrlHinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiKhachSan>()
                .HasMany(e => e.KhachSans)
                .WithOptional(e => e.LoaiKhachSan)
                .HasForeignKey(e => e.IdLoaiKhachSan);

            modelBuilder.Entity<Phong>()
                .HasMany(e => e.DatPhongs)
                .WithOptional(e => e.Phong)
                .HasForeignKey(e => e.IdPhong);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TenTaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.DatPhongs)
                .WithOptional(e => e.TaiKhoan1)
                .HasForeignKey(e => e.TaiKhoan);

            modelBuilder.Entity<ThanhPho>()
                .Property(e => e.UrlHinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhPho>()
                .HasMany(e => e.KhachSans)
                .WithOptional(e => e.ThanhPho)
                .HasForeignKey(e => e.IdThanhPho);
        }
    }
}
