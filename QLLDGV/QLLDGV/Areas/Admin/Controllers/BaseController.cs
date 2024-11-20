using Database.Framework;
using QLLDGV.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLLDGV.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        private UserDbContext db = new UserDbContext();
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                var result = db.QuanTris.Count(x => x.MaAdmin == session.UserID);
                if (result == 0)
                {
                    filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "HomeGV", action = "Index", Area = "" }));
                }
            }
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Login", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}