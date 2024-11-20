namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        [Key]
        public Guid MaMail { get; set; }

        public Guid NguoiGui { get; set; }

        public string NoiDung { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? NgayGui { get; set; }

        public string Tinhtrang { get; set; }
        public string PhanHoi {  get; set; }
        public string PhanHoiTuAdmin { get; set; }

        public virtual GIAOVIEN GIAOVIEN { get; set; }
    }

}
