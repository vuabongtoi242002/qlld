using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Database.Framework;

namespace QLLDGV.Areas.Admin.Controllers
{
    public class QuanTrisController : BaseController
    {
        private UserDbContext db = new UserDbContext();

        // GET: Admin/QuanTris
        public ActionResult Index()
        {
            return View(db.QuanTris.ToList());
        }
        public JsonResult CheckEmailQT(string emailQT)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = db.QuanTris.Where(x => x.Email == emailQT).SingleOrDefault();
            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        // GET: Admin/QuanTris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QuanTris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaAdmin,TenAdmin,SDT,Email,MatKhau")] QuanTri quanTri)
        {
            if (ModelState.IsValid)
            {
                quanTri.MaAdmin = Guid.NewGuid();
                db.QuanTris.Add(quanTri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quanTri);
        }

        // GET: Admin/QuanTris/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTri quanTri = db.QuanTris.Find(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }

        // POST: Admin/QuanTris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaAdmin,TenAdmin,SDT,Email,MatKhau")] QuanTri quanTri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quanTri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quanTri);
        }

        // GET: Admin/QuanTris/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTri quanTri = db.QuanTris.Find(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }

        // POST: Admin/QuanTris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            QuanTri quanTri = db.QuanTris.Find(id);
            db.QuanTris.Remove(quanTri);
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
