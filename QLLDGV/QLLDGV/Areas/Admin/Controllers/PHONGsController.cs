using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Database.Framework;

namespace QLLDGV.Areas.Admin.Controllers
{
    public class PHONGsController : BaseController
    {
        private UserDbContext db = new UserDbContext();

        // GET: Admin/PHONGs
        public ActionResult Index(int? page, string SearchString, string currentFilter)
        {
            var links = new List<PHONG>();
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
                links = db.PHONGs.Where(n => n.MaPhong.Contains(SearchString) ||
                                            n.TenPhong.Contains(SearchString)).ToList();
            }
            else
            {
                links = db.PHONGs.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            links = links.OrderByDescending(n => n.TenPhong).ToList();
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        public JsonResult CheckMaPhong(string maPhong)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = db.PHONGs.Where(x => x.MaPhong == maPhong).SingleOrDefault();
            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        // GET: Admin/PHONGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONG pHONG = db.PHONGs.Find(id);
            if (pHONG == null)
            {
                return HttpNotFound();
            }
            return View(pHONG);
        }

        // GET: Admin/PHONGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PHONGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhong,TenPhong,ThongTinPhong")] PHONG pHONG)
        {
            if (ModelState.IsValid)
            {
                db.PHONGs.Add(pHONG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pHONG);
        }

        // GET: Admin/PHONGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONG pHONG = db.PHONGs.Find(id);
            if (pHONG == null)
            {
                return HttpNotFound();
            }
            return View(pHONG);
        }

        // POST: Admin/PHONGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhong,TenPhong,ThongTinPhong")] PHONG pHONG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHONG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pHONG);
        }

        // GET: Admin/PHONGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONG pHONG = db.PHONGs.Find(id);
            if (pHONG == null)
            {
                return HttpNotFound();
            }
            return View(pHONG);
        }

        // POST: Admin/PHONGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PHONG pHONG = db.PHONGs.Find(id);
            db.PHONGs.Remove(pHONG);
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
