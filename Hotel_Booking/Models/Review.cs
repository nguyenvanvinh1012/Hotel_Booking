namespace Hotel_Booking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        [Key]
        public int review_id { get; set; }

        public int? KhachSan_id { get; set; }

        [StringLength(50)]
        public string TenTaiKhoan { get; set; }

        public int? rating { get; set; }

        [StringLength(500)]
        public string comment { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_rv { get; set; }

        public virtual KhachSan KhachSan { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
