using Database.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace QLLDGV.Controllers
{
    public class ForgetPasswordGVController : Controller
    {

        private UserDbContext db = new UserDbContext();
        // GET: ForgetPasswordGV
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(Database.Framework.MailModel _objModelMail)
        {
            if (string.IsNullOrEmpty(_objModelMail.To))
            {
                // Thông báo khi người dùng không nhập email
                ViewBag.ThongBao = "Vui lòng nhập email.";
                return View("ForgotPassword");
            }

            String pass = "abcdefghijklmnopqrstuvwxyz1234567890";
            Random r = new Random();
            char[] mypass = new char[5];
            for (int i = 0; i < 5; i++)
            {
                mypass[i] = pass[(int)(35 * r.NextDouble())];
            }
            string mkm = new string(mypass);
            MailMessage mail = new MailMessage();
            mail.To.Add(_objModelMail.To);
            mail.From = new MailAddress("testaspmvc123@gmail.com", "Admin");
            mail.Subject = "Nhận lại mật khẩu";
            string Body = "<h4><b>Mật khẩu mới của bạn là: " + mkm + "</b></h4>";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("testaspmvc123@gmail.com", "qkovstsfttgohtgl"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            var checkmail = db.GIAOVIENs.Count(x => x.EmailGV == _objModelMail.To);
            if (checkmail > 0)
            {
                GIAOVIEN gv = db.GIAOVIENs.SingleOrDefault(x => x.EmailGV == _objModelMail.To);
                gv.MatKhauGV = mkm;
                db.SaveChanges();
                smtp.Send(mail);
                @ViewBag.ThongBao = "Gửi thành công vui lòng check Mail!";
            }
            else
            {
                @ViewBag.ThongBao = "Email không tồn tại";
            }
            return View("ForgotPassword");
        }
    }
}