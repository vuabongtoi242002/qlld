﻿using QLLDGV.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLLDGV.Controllers
{
    public class BaseGVController : Controller
    {
        // GET: BaseGV
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "LoginGV", action = "LoginGV", Area = "" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}