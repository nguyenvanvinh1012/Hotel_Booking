namespace Hotel_Booking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phong")]
    public partial class Phong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phong()
        {
            DatPhongs = new HashSet<DatPhong>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Ten { get; set; }

        public int? DienTich { get; set; }

        public int? GiaThue { get; set; }

        [StringLength(200)]
        public string TienNghi { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        public int? LoaiGiuong { get; set; }

        public int? IdKhachSan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatPhong> DatPhongs { get; set; }

        public virtual KhachSan KhachSan { get; set; }
    }
}
