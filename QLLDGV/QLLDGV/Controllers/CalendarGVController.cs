using Database.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLLDGV.Controllers
{
    public class CalendarGVController : BaseGVController
    {
        private UserDbContext db = new UserDbContext();
        // GET: CalendarGV
        public ActionResult Calendar(string SearchString, string currentFilter)
        {
            var session = (UserLogin)Session[QLLDGV.Common.CommonConstants.USER_SESSION];
            var ld = db.LichDays.Where(n => n.GVDay == session.UserID).ToList();
            return View(ld);
        }

        public ActionResult SuKien()
        {
            var session = (UserLogin)Session[QLLDGV.Common.CommonConstants.USER_SESSION];
            var sk = db.SuKiens.Where(n => n.GiaoVien == session.UserID).ToList();
            return View(sk);
        }
    }
}