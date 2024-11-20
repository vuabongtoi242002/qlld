namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHONG")]
    public partial class PHONG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHONG()
        {
            LichDays = new HashSet<LichDay>();
        }

        [Key]
        [StringLength(5)]
        public string MaPhong { get; set; }

        [Required]
        public string TenPhong { get; set; }

        public string ThongTinPhong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichDay> LichDays { get; set; }
    }
}
