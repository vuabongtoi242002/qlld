using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLLDGV.Areas.Admin.Controllers;
using Database.Framework;

namespace QLLDGV.Areas.Admin.Controllers
{
    public class CalendarController : BaseController
    {
        private UserDbContext db = new UserDbContext();
        // GET: Admin/Calendar
        public ActionResult Calendar(string SearchString)
        {
            var links = new List<LichDay>();
            if (!string.IsNullOrEmpty(SearchString))
            {
                links = db.LichDays.Where(n => n.GIAOVIEN.HoTenGV.Contains(SearchString) ||
                                            n.MonHoc.TenMH.Contains(SearchString) ||
                                            n.HocPhan.TenKhoa.Contains(SearchString) ||
                                            n.THU.TenThu.Contains(SearchString)).ToList();
            }
            else
            {
                links = db.LichDays.Include(l => l.GIAOVIEN).Include(l => l.MonHoc).Include(l => l.TietHoc).Include(l => l.TietHoc1).Include(l => l.PHONG).Include(l => l.THU).ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            return View(links);
        }

        public ActionResult SuKien(string SearchSuKien)
        {
            var links = new List<SuKien>();
            if (!string.IsNullOrEmpty(SearchSuKien))
            {
                links = db.SuKiens.Where(n => n.GIAOVIEN1.HoTenGV.Contains(SearchSuKien) ||
                                            n.MonHoc.TenMH.Contains(SearchSuKien) ||
                                            n.HocPhan.TenKhoa.Contains(SearchSuKien) ||
                                            n.TenSuKien.Contains(SearchSuKien)).ToList();
            }
            else
            {
                links = db.SuKiens.ToList();
            }
            ViewBag.CurrentFilter = SearchSuKien;
            return View(links);
        }
    }
}