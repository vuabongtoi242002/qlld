using System;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using Database.Framework;

namespace QLLDGV.Areas.Admin.Controllers
{
    public class ForgetPasswordAdminController : Controller
    {
        private UserDbContext db = new UserDbContext();

        // GET: Admin/ForgetPasswordAdmin
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(MailModel _objModelMail)
        {
            if (string.IsNullOrEmpty(_objModelMail.To))
            {
                // Thông báo khi người dùng không nhập email
                ViewBag.ThongBao = "Vui lòng nhập email.";
                return View("ForgotPassword");
            }

            string passwordCharacters = "abcdefghijklmnopqrstuvwxyz1234567890";
            Random random = new Random();
            string newPassword = new string(Enumerable.Repeat(passwordCharacters, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            MailMessage mail = new MailMessage();
            mail.To.Add(_objModelMail.To);
            mail.From = new MailAddress("testaspmvc123@gmail.com", "Admin");
            mail.Subject = "Nhận lại mật khẩu";
            string body = "<h4><b>Mật khẩu mới của bạn là: " + newPassword + "</b></h4>";
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("testaspmvc123@gmail.com", "qkovstsfttgohtgl");
            smtp.EnableSsl = true;

            var userExists = db.QuanTris.Any(x => x.Email == _objModelMail.To);
            if (userExists)
            {
                QuanTri user = db.QuanTris.Single(x => x.Email == _objModelMail.To);
                user.MatKhau = newPassword;
                db.SaveChanges();
                smtp.Send(mail);
                ViewBag.ThongBao = "Gửi thành công, vui lòng kiểm tra Email!";
            }
            else
            {
                ViewBag.ThongBao = "Email không tồn tại";
            }

            return View("ForgotPassword");
        }
    }
}
