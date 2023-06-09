namespace Hotel_Booking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuyenTruyCap")]
    public partial class QuyenTruyCap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuyenTruyCap()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        [Key]
        public int MaQuyenTryCap { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tên quyền truy cập!")]
        public string TenQuyenTryCap { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập mô tả!")]
        public string Mota { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
