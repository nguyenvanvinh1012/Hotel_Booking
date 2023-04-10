namespace Hotel_Booking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DatPhong")]
    public partial class DatPhong
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string TaiKhoan { get; set; }

        public int? IdPhong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTra { get; set; }

        [StringLength(200)]
        public string DichVu { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }

        public int? ThanhTien { get; set; }

        public bool? isPaid { get; set; }

        public virtual Phong Phong { get; set; }

        public virtual TaiKhoan TaiKhoan1 { get; set; }
    }
}
