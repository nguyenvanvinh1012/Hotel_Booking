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

        [Required(ErrorMessage ="Vui lòng nhập tên quyền truy cập!")]
        [StringLength(50)]
        public string TenQuyenTryCap { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập mô tả!")]
        [StringLength(50)]
        public string Mota { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
