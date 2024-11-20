using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Database.Framework;
using PagedList;

namespace QLLDGV.Areas.Admin.Controllers
{
    public class LichDaysController : BaseController
    {
        private UserDbContext db = new UserDbContext();

        // GET: Admin/LichDays
        public ActionResult Index(int? page, string SearchString, string currentFilter)
        {
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
                links = db.LichDays.Where(n => n.GIAOVIEN.HoTenGV.Contains(SearchString) ||
                                            n.MonHoc.TenMH.Contains(SearchString) ||
                                            n.HocPhan.TenKhoa.Contains(SearchString) ||
                                            n.THU.TenThu.Contains(SearchString) ||
                                            n.PHONG.TenPhong.Contains(SearchString)).ToList();
            }
            else
            {
                links = db.LichDays.Include(l => l.GIAOVIEN).Include(l => l.MonHoc).Include(l => l.TietHoc).Include(l => l.TietHoc1).Include(l => l.PHONG).Include(l => l.THU).ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            links = links.OrderByDescending(n => n.MonHoc.TenMH).ToList();
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/LichDays/Details/5
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

        // GET: Admin/LichDays/Create
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
                db.Entry(lichDay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GVDay = new SelectList(db.GIAOVIENs, "MaGV", "HoTenGV", lichDay.GVDay);
            ViewBag.NhomHP = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", lichDay.NhomHP);
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
            ViewBag.NhomHP = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", lichDay.NhomHP);
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
            ViewBag.NhomHP = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", lichDay.NhomHP);
            ViewBag.TenMH = new SelectList(db.MonHocs, "MaMH", "TenMH", lichDay.TenMH);
            ViewBag.PhongHoc = new SelectList(db.PHONGs, "MaPhong", "TenPhong", lichDay.PhongHoc);
            ViewBag.ThuNgay = new SelectList(db.THUs, "MaThu", "TenThu", lichDay.ThuNgay);
            ViewBag.GioBatDau = new SelectList(db.TietHocs, "MaTiet", "Tiet", lichDay.GioBatDau);
            ViewBag.GioKetThuc = new SelectList(db.TietHocs, "MaTiet", "Tiet", lichDay.GioKetThuc);
            return View(lichDay);
        }

        // GET: Admin/LichDays/Delete/5
        public ActionResult Delete(Guid? id)
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

        // POST: Admin/LichDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            LichDay lichDay = db.LichDays.Find(id);
            db.LichDays.Remove(lichDay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
