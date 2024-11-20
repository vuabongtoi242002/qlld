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
    public class KhoaController : BaseController
    {
        private UserDbContext db = new UserDbContext();

        // GET: Admin/Khoa
        public ActionResult Index(int? page, string SearchString, string currentFilter)
        {
            var links = new List<Khoa>();
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
                links = db.Khoas.Where(n => n.MonHoc1.TenMH.Contains(SearchString) ||
                                            n.MonHoc.Contains(SearchString)).ToList();
            }
            else
            {
                links = db.Khoas.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            links = links.OrderByDescending(n => n.MonHoc1.TenMH).ToList();
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Khoa/Create
        public ActionResult Create()
        {
            ViewBag.MonHoc = new SelectList(db.MonHocs, "MaMH", "TenMH");
            return View();
        }

        // POST: Admin/Khoa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKhoa,MonHoc,TenKhoa")] Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                khoa.MaKhoa = Guid.NewGuid();
                db.Khoas.Add(khoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MonHoc = new SelectList(db.MonHocs, "MaMH", "TenMH", khoa.MonHoc);
            return View(khoa);
        }

        // GET: Admin/Khoa/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khoa khoa = db.Khoas.Find(id);
            if (khoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.MonHoc = new SelectList(db.MonHocs, "MaMH", "TenMH", khoa.MonHoc);
            return View(khoa);
        }

        // POST: Admin/HocPhans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKhoa,MonHoc,TenKhoa")] Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MonHoc = new SelectList(db.MonHocs, "MaMH", "TenMH", khoa.MonHoc);
            return View(khoa);
        }

        // GET: Admin/Khoa/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khoa khoa = db.Khoas.Find(id);
            if (khoa == null)
            {
                return HttpNotFound();
            }
            return View(khoa);
        }

        // POST: Admin/HocPhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Khoa khoa = db.Khoas.Find(id);
            db.Khoas.Remove(khoa);
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
