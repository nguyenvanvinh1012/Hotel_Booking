namespace Hotel_Booking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Phong")]
    public partial class Phong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phong()
        {
            DatPhongs = new HashSet<DatPhong>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên phòng!")]
        [StringLength(100)]
        public string Ten { get; set; }

        [Range(10, 1000, ErrorMessage ="Diện tích phải lớn 10")]
        public int? DienTich { get; set; }

        [StringLength(200)]
        public string UrlHinhAnh { get; set; }


        [Range(100000, 10000000, ErrorMessage =" Giá thuê phải lớn hơn 100000")]
        public int? GiaThue { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiện nghi!")]
        [StringLength(200)]
        public string TienNghi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả!")]
        [StringLength(1000)]
        public string MoTa { get; set; }

        public int? LoaiGiuong { get; set; }

        public int? IdKhachSan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatPhong> DatPhongs { get; set; }

        public virtual KhachSan KhachSan { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
