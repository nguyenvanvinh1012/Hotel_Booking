namespace Hotel_Booking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("ThanhPho")]
    public partial class ThanhPho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThanhPho()
        {
            KhachSans = new HashSet<KhachSan>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Ten { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }

        [StringLength(200)]
        public string UrlHinhAnh { get; set; }

        public bool? Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachSan> KhachSans { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
