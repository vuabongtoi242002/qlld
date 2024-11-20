namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TietHoc")]
    public partial class TietHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TietHoc()
        {
            LichDays = new HashSet<LichDay>();
            LichDays1 = new HashSet<LichDay>();
        }

        [Key]
        [StringLength(3)]
        public string MaTiet { get; set; }

        public int Tiet { get; set; }

        [Required]
        public string TGianBD { get; set; }

        [Required]
        public string TGianKT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichDay> LichDays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichDay> LichDays1 { get; set; }
    }
}
