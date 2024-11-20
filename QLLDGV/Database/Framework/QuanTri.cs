namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuanTri")]
    public partial class QuanTri
    {
        [Key]
        public Guid MaAdmin { get; set; }

        public string TenAdmin { get; set; }

        public string SDT { get; set; }

        public string Email { get; set; }

        public string MatKhau { get; set; }
    }
}
