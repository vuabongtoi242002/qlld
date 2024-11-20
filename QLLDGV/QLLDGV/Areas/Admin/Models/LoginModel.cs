using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLLDGV.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string Password { get; set; }
    }
}