namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuKien")]
    public partial class SuKien
    {
        [Key]
        public Guid MaSuKien { get; set; }

        [Required]
        [StringLength(8)]
        public string TenHP { get; set; }
        [Required]
        public Guid Nhom { get; set; }
        public Guid? GiaoVien { get; set; }

        public string TenSuKien { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? StartDate { get; set; }

        public virtual GIAOVIEN GIAOVIEN1 { get; set; }

        public virtual Khoa HocPhan { get; set; }

        public virtual MonHoc MonHoc { get; set; }
    }
}
