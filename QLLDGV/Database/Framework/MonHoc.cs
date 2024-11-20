namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MonHoc")]
    public partial class MonHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MonHoc()
        {
            GIAOVIENs = new HashSet<GIAOVIEN>();
            HocPhans = new HashSet<Khoa>();
            LichDays = new HashSet<LichDay>();
            SuKiens = new HashSet<SuKien>();
        }

        [Key]
        [StringLength(8)]
        public string MaMH { get; set; }

        [Required]
        public string TenMH { get; set; }

        [Required]
        [StringLength(6)]
        public string Nganh { get; set; }

        [Range(1, Int32.MaxValue)]
        public int? TinChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIAOVIEN> GIAOVIENs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Khoa> HocPhans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichDay> LichDays { get; set; }

        public virtual Nganh Nganh1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuKien> SuKiens { get; set; }
    }
}
