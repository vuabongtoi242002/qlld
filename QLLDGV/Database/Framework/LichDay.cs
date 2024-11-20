namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichDay")]
    public partial class LichDay
    {
        [Key]
        public Guid MaLich { get; set; }

        [Required]
        [StringLength(8)]
        public string TenMH { get; set; }

        public Guid NhomHP { get; set; }

        [StringLength(5)]
        public string PhongHoc { get; set; }

        public Guid? GVDay { get; set; }

        [StringLength(2)]
        public string ThuNgay { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? NgayBatDau { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? NgayKetThuc { get; set; }

        [StringLength(3)]
        public string GioBatDau { get; set; }

        [StringLength(3)]
        public string GioKetThuc { get; set; }

        public virtual GIAOVIEN GIAOVIEN { get; set; }

        public virtual Khoa HocPhan { get; set; }

        public virtual TietHoc TietHoc { get; set; }

        public virtual TietHoc TietHoc1 { get; set; }

        public virtual PHONG PHONG { get; set; }

        public virtual MonHoc MonHoc { get; set; }

        public virtual THU THU { get; set; }
    }
}
