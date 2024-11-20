using Database;
using Database.Dao;
using QLLDGV.Areas.Admin.Models;
using QLLDGV.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace QLLDGV.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Email, model.Password);
                if (result)
                {
                    var user = dao.GetByEmail(model.Email);
                    var userSession = new UserLogin();
                   
                    userSession.UserName = user.TenAdmin;
                    userSession.UserID = user.MaAdmin;

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                   
                }
               
                else
                {
                    ViewBag.ThongBao = "Đăng nhập không thành công";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(model.Email))
                {
                    ViewBag.ThongBao = "Vui lòng nhập email.";
                }
                else if (string.IsNullOrEmpty(model.Password))
                {
                    ViewBag.ThongBao = "Vui lòng nhập mật khẩu.";
                }
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Login", "Login");

        }
    }
}
