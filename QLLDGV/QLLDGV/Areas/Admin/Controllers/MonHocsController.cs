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
    public class MonHocsController : BaseController
    {
        private UserDbContext db = new UserDbContext();

        // GET: Admin/MonHocs
        public ActionResult Index(int? page, string SearchString, string currentFilter)
        {
            var links = new List<MonHoc>();
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
                links = db.MonHocs.Where(n => n.MaMH.Contains(SearchString) ||
                                            n.TenMH.Contains(SearchString) ||
                                            n.Nganh1.TenNganh.Contains(SearchString)).ToList();
            }
            else
            {
                links = db.MonHocs.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            links = links.OrderByDescending(n => n.TinChi).ToList();
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/MonHocs/Create
        public ActionResult Create()
        {
            ViewBag.Nganh = new SelectList(db.Nganhs, "MaNganh", "TenNganh");
            return View();
        }
        public JsonResult CheckMaMon(string maMH)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = db.MonHocs.Where(x => x.MaMH == maMH).SingleOrDefault();
            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        // POST: Admin/MonHocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMH,TenMH,Nganh,TinChi")] MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                db.MonHocs.Add(monHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Nganh = new SelectList(db.Nganhs, "MaNganh", "TenNganh", monHoc.Nganh);
            return View(monHoc);
        }

        // GET: Admin/MonHocs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonHoc monHoc = db.MonHocs.Find(id);
            if (monHoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.Nganh = new SelectList(db.Nganhs, "MaNganh", "TenNganh", monHoc.Nganh);
            return View(monHoc);
        }

        // POST: Admin/MonHocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMH,TenMH,Nganh,TinChi")] MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Nganh = new SelectList(db.Nganhs, "MaNganh", "TenNganh", monHoc.Nganh);
            return View(monHoc);
        }

        // GET: Admin/MonHocs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonHoc monHoc = db.MonHocs.Find(id);
            if (monHoc == null)
            {
                return HttpNotFound();
            }
            return View(monHoc);
        }

        // POST: Admin/MonHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MonHoc monHoc = db.MonHocs.Find(id);
            db.MonHocs.Remove(monHoc);
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
