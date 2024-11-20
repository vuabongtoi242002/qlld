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
    public class NganhsController : BaseController
    {
        private UserDbContext db = new UserDbContext();

        // GET: Admin/Nganhs
        public ActionResult Index(int? page, string SearchString, string currentFilter)
        {
            var links = new List<Nganh>();
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
                links = db.Nganhs.Where(n => n.MaNganh.Contains(SearchString) ||
                                        n.TenNganh.Contains(SearchString)).ToList();
            }
            else
            {
                links = db.Nganhs.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            links = links.OrderByDescending(n => n.TenNganh).ToList();
            return View(links.ToPagedList(pageNumber, pageSize));
        }
        public JsonResult CheckMaNganh(string maNganh)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = db.Nganhs.Where(x => x.MaNganh == maNganh).SingleOrDefault();
            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        // GET: Admin/Nganhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Nganhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNganh,TenNganh")] Nganh nganh)
        {
            if (ModelState.IsValid)
            {
                db.Nganhs.Add(nganh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nganh);
        }

        // GET: Admin/Nganhs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nganh nganh = db.Nganhs.Find(id);
            if (nganh == null)
            {
                return HttpNotFound();
            }
            return View(nganh);
        }

        // POST: Admin/Nganhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNganh,TenNganh")] Nganh nganh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nganh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nganh);
        }

        // GET: Admin/Nganhs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nganh nganh = db.Nganhs.Find(id);
            if (nganh == null)
            {
                return HttpNotFound();
            }
            return View(nganh);
        }

        // POST: Admin/Nganhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Nganh nganh = db.Nganhs.Find(id);
            db.Nganhs.Remove(nganh);
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
