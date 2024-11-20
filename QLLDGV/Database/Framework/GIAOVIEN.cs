namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GIAOVIEN")]
    public partial class GIAOVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GIAOVIEN()
        {
            LichDays = new HashSet<LichDay>();
            Messages = new HashSet<Message>();
            SuKiens = new HashSet<SuKien>();
        }

        [Key]
        public Guid MaGV { get; set; }

        [Required]
        public string HoTenGV { get; set; }

        [Required]
        [StringLength(8)]
        public string BoMonPhuTrach { get; set; }

        [Required]
        [StringLength(15)]
        public string SodtGV { get; set; }

        [Required]
        public string EmailGV { get; set; }

        public string QueQuan { get; set; }

        [Required]
        public string MatKhauGV { get; set; }

        public virtual MonHoc MonHoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichDay> LichDays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuKien> SuKiens { get; set; }
    }
}
