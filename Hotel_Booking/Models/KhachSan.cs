namespace Hotel_Booking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("KhachSan")]
    public partial class KhachSan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachSan()
        {
            Phongs = new HashSet<Phong>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Tên khách sạn không được để trống!")]

        [StringLength(100)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Địa chỉ khách sạn không được để trống!")]
        [StringLength(100)]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "SĐT không được để trống!")]
        [StringLength(10)]
        public string SoDienThoai { get; set; }

        [Range(1, 999999, ErrorMessage = "Giá trị phải lớn hơn 0")]
        public double? CachTrungTam { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống!")]
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

        public bool? Active { get; set; }

        public virtual LoaiKhachSan LoaiKhachSan { get; set; }

        public virtual ThanhPho ThanhPho { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phong> Phongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile1 { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile2 { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile3 { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile4 { get; set; }
    }
}
