namespace Hotel_Booking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            DatPhongs = new HashSet<DatPhong>();
            Reviews = new HashSet<Review>();
        }

        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản!")]
        public string TenTaiKhoan { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        public string MatKhau { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập họ tên!")]
        public string HoTen { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
        public string SoDienThoai { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập Email!")]
        public string Email { get; set; }

        public int? MaQuyenTryCap { get; set; }

        public bool? Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatPhong> DatPhongs { get; set; }

        public virtual QuyenTruyCap QuyenTruyCap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
