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
using PagedList;

namespace QLLDGV.Controllers
{
    public class SuKienGVController : BaseGVController
    {
        private UserDbContext db = new UserDbContext();

        // GET: SuKienGV
        public ActionResult Index(int? page, string SearchString, string currentFilter)
        {
            var session = (UserLogin)Session[QLLDGV.Common.CommonConstants.USER_SESSION];
            var links = new List<SuKien>();
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
                links = db.SuKiens.Where(n => n.GiaoVien == session.UserID &&
                                            n.TenSuKien.Contains(SearchString) ||
                                            n.MonHoc.TenMH.Contains(SearchString) ||
                                            n.HocPhan.TenKhoa.Contains(SearchString)).ToList();
            }
            else
            {
                links = db.SuKiens.Where(n => n.GiaoVien == session.UserID).ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            links = links.OrderByDescending(n => n.StartDate).ToList();
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: SuKienGV/Create
        public ActionResult Create()
        {
            ViewBag.Nhom = new SelectList(db.Khoas, "MaKhoa", "TenKhoa");
            ViewBag.TenHP = new SelectList(db.MonHocs, "MaMH", "TenMH");
            return View();
        }
        public JsonResult GetNhomHPList(string MaHP)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Khoa> NhomHPList = db.Khoas.Where(x => x.MonHoc == MaHP).ToList();
            return Json(NhomHPList, JsonRequestBehavior.AllowGet);
        }

        // POST: SuKienGV/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSuKien,TenHP,Nhom,GiaoVien,TenSuKien,StartDate")] SuKien suKien)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[QLLDGV.Common.CommonConstants.USER_SESSION];
                suKien.MaSuKien = Guid.NewGuid();
                suKien.GiaoVien = session.UserID;
                db.SuKiens.Add(suKien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GiaoVien = new SelectList(db.GIAOVIENs, "MaGV", "HoTenGV", suKien.GiaoVien);
            ViewBag.Nhom = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", suKien.Nhom);
            ViewBag.TenHP = new SelectList(db.MonHocs, "MaMH", "TenMH", suKien.TenHP);
            return View(suKien);
        }

        // GET: SuKienGV/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuKien suKien = db.SuKiens.Find(id);
            if (suKien == null)
            {
                return HttpNotFound();
            }
            ViewBag.GiaoVien = new SelectList(db.GIAOVIENs, "MaGV", "HoTenGV", suKien.GiaoVien);
            ViewBag.Nhom = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", suKien.Nhom);
            ViewBag.TenHP = new SelectList(db.MonHocs, "MaMH", "TenMH", suKien.TenHP);
            return View(suKien);
        }

        // POST: SuKienGV/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSuKien,TenHP,Nhom,GiaoVien,TenSuKien,StartDate")] SuKien suKien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suKien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GiaoVien = new SelectList(db.GIAOVIENs, "MaGV", "HoTenGV", suKien.GiaoVien);
            ViewBag.Nhom = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", suKien.Nhom);
            ViewBag.TenHP = new SelectList(db.MonHocs, "MaMH", "TenMH", suKien.TenHP);
            return View(suKien);
        }

        // GET: SuKienGV/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuKien suKien = db.SuKiens.Find(id);
            if (suKien == null)
            {
                return HttpNotFound();
            }
            return View(suKien);
        }

        // POST: SuKienGV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SuKien suKien = db.SuKiens.Find(id);
            db.SuKiens.Remove(suKien);
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
