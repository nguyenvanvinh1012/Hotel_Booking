namespace Hotel_Booking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachSan")]
    public partial class KhachSan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachSan()
        {
            Phongs = new HashSet<Phong>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Ten { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        [StringLength(10)]
        public string SoDienThoai { get; set; }

        public int? CachTrungTam { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        public bool? GiapBien { get; set; }

        public int? DanhGia { get; set; }

        public int? BuaAn { get; set; }

        [StringLength(200)]
        public string UrlHinhAnh1 { get; set; }

        [StringLength(200)]
        public string UrlHinhAnh2 { get; set; }

        [StringLength(200)]
        public string UrlHinhAnh3 { get; set; }

        [StringLength(200)]
        public string UrlHinhAnh4 { get; set; }

        public int? IdThanhPho { get; set; }

        public int? IdLoaiKhachSan { get; set; }

        public virtual LoaiKhachSan LoaiKhachSan { get; set; }

        public virtual ThanhPho ThanhPho { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phong> Phongs { get; set; }
    }
}
