using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Database.Framework;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace QLLDGV.Controllers
{
    public class LichDayGVController : BaseGVController
    {
        private UserDbContext db = new UserDbContext();

        // GET: LichDayGV
        public ActionResult Index(int? page, string SearchString, string currentFilter)
        {
            var session = (UserLogin)Session[QLLDGV.Common.CommonConstants.USER_SESSION];
            var links = new List<LichDay>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                links = db.LichDays.Where(n => n.GVDay == session.UserID &&
                                            n.MonHoc.TenMH.Contains(SearchString) ||
                                            n.HocPhan.TenKhoa.Contains(SearchString) ||
                                            n.THU.TenThu.Contains(SearchString) ||
                                            n.PHONG.TenPhong.Contains(SearchString)).ToList();
            }
            else
            {
                links = db.LichDays.Where(n => n.GVDay == session.UserID).ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            links = links.OrderByDescending(n => n.MonHoc.TenMH).ToList();
            return View(links.ToPagedList(pageNumber, pageSize));
        }
        public JsonResult GetEvents()
        {
            using (UserDbContext dc = new UserDbContext())
            {
                var session = (UserLogin)Session[QLLDGV.Common.CommonConstants.USER_SESSION];
                var events = dc.LichDays.Where(n => n.GVDay == session.UserID).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // GET: LichDayGV/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichDay lichDay = db.LichDays.Find(id);
            if (lichDay == null)
            {
                return HttpNotFound();
            }
            return View(lichDay);
        }

        public ActionResult Create()
        {
            ViewBag.GVDay = new SelectList(db.GIAOVIENs, "MaGV", "HoTenGV");
            ViewBag.NhomHP = new SelectList(db.Khoas, "MaKhoa", "TenKhoa");
            ViewBag.TenMH = new SelectList(db.MonHocs, "MaMH", "TenMH");
            ViewBag.PhongHoc = new SelectList(db.PHONGs, "MaPhong", "TenPhong");
            ViewBag.ThuNgay = new SelectList(db.THUs, "MaThu", "TenThu");
            ViewBag.GioBatDau = new SelectList(db.TietHocs, "MaTiet", "Tiet");
            ViewBag.GioKetThuc = new SelectList(db.TietHocs, "MaTiet", "Tiet");
            return View();
        }
        public JsonResult GetNhomHPList(string MaMH)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Khoa> NhomHPList = db.Khoas.Where(x => x.MonHoc1.MaMH == MaMH).ToList();
            return Json(NhomHPList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGVList(string MaMH)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<GIAOVIEN> GiaoVienList = db.GIAOVIENs.Where(x => x.BoMonPhuTrach == MaMH).ToList();
            return Json(GiaoVienList, JsonRequestBehavior.AllowGet);
        }

        // POST: Admin/LichDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLich,TenMH,PhongHoc,GVDay,ThuNgay,NgayBatDau,NgayKetThuc,GioBatDau,GioKetThuc")] LichDay lichDay)
        {
            if (ModelState.IsValid)
            {
                lichDay.MaLich = Guid.NewGuid();
                db.LichDays.Add(lichDay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GVDay = new SelectList(db.GIAOVIENs, "MaGV", "HoTenGV", lichDay.GVDay);
            //ViewBag.NhomHP = new SelectList(db.Khoas, "MaNhom", "TenNhom", lichDay.NhomHP);
            ViewBag.TenMH = new SelectList(db.MonHocs, "MaMH", "TenMH", lichDay.TenMH);
            ViewBag.PhongHoc = new SelectList(db.PHONGs, "MaPhong", "TenPhong", lichDay.PhongHoc);
            ViewBag.ThuNgay = new SelectList(db.THUs, "MaThu", "TenThu", lichDay.ThuNgay);
            ViewBag.GioBatDau = new SelectList(db.TietHocs, "MaTiet", "Tiet", lichDay.GioBatDau);
            ViewBag.GioKetThuc = new SelectList(db.TietHocs, "MaTiet", "Tiet", lichDay.GioKetThuc);
            return View(lichDay);
        }

        // GET: Admin/LichDays/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichDay lichDay = db.LichDays.Find(id);
            if (lichDay == null)
            {
                return HttpNotFound();
            }
            ViewBag.GVDay = new SelectList(db.GIAOVIENs, "MaGV", "HoTenGV", lichDay.GVDay);
            //ViewBag.NhomHP = new SelectList(db.Khoas, "MaKhoa", "TenNhom", lichDay.NhomHP);
            ViewBag.TenMH = new SelectList(db.MonHocs, "MaMH", "TenMH", lichDay.TenMH);
            ViewBag.PhongHoc = new SelectList(db.PHONGs, "MaPhong", "TenPhong", lichDay.PhongHoc);
            ViewBag.ThuNgay = new SelectList(db.THUs, "MaThu", "TenThu", lichDay.ThuNgay);
            ViewBag.GioBatDau = new SelectList(db.TietHocs, "MaTiet", "Tiet", lichDay.GioBatDau);
            ViewBag.GioKetThuc = new SelectList(db.TietHocs, "MaTiet", "Tiet", lichDay.GioKetThuc);
            return View(lichDay);
        }

        // POST: Admin/LichDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLich,TenMH,PhongHoc,GVDay,ThuNgay,NgayBatDau,NgayKetThuc,GioBatDau,GioKetThuc")] LichDay lichDay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lichDay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GVDay = new SelectList(db.GIAOVIENs, "MaGV", "HoTenGV", lichDay.GVDay);
            //ViewBag.NhomHP = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", lichDay.NhomHP);
            ViewBag.TenMH = new SelectList(db.MonHocs, "MaMH", "TenMH", lichDay.TenMH);
            ViewBag.PhongHoc = new SelectList(db.PHONGs, "MaPhong", "TenPhong", lichDay.PhongHoc);
            ViewBag.ThuNgay = new SelectList(db.THUs, "MaThu", "TenThu", lichDay.ThuNgay);
            ViewBag.GioBatDau = new SelectList(db.TietHocs, "MaTiet", "Tiet", lichDay.GioBatDau);
            ViewBag.GioKetThuc = new SelectList(db.TietHocs, "MaTiet", "Tiet", lichDay.GioKetThuc);
            return View(lichDay);
        }
    }
}
