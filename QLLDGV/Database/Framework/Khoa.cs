namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Khoa")]
    public partial class Khoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Khoa()
        {
            LichDays = new HashSet<LichDay>();
            SuKiens = new HashSet<SuKien>();
        }

        [Key]
        public Guid MaKhoa { get; set; }

        [Required]
        [StringLength(8)]
        public string MonHoc { get; set; }

        [Required]
        public string TenKhoa { get; set; }

        public virtual MonHoc MonHoc1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichDay> LichDays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuKien> SuKiens { get; set; }
    }
}
