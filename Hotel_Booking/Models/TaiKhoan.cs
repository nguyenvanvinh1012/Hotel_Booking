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
        }

        [Key]
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản!")]
        [StringLength(50)]
        public string TenTaiKhoan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên!")]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
        [StringLength(15)]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email!")]
        [StringLength(50)]
        public string Email { get; set; }

        public int? MaQuyenTryCap { get; set; }

        public bool? Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatPhong> DatPhongs { get; set; }

        public virtual QuyenTruyCap QuyenTruyCap { get; set; }
    }
}
