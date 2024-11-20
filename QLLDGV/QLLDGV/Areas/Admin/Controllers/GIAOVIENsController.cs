 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Database.Framework;
using PagedList.Mvc;
using PagedList;
using System.Web.UI;

namespace QLLDGV.Areas.Admin.Controllers
{
    public class GIAOVIENsController : BaseController
    {
        private UserDbContext db = new UserDbContext();

        // GET: Admin/GIAOVIENs
        public ActionResult Index(int? page, string SearchString, string currentFilter)
        {
            var links = new List<GIAOVIEN>();
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
                links = db.GIAOVIENs.Where(n => n.HoTenGV.Contains(SearchString) ||
                                            n.MonHoc.TenMH.Contains(SearchString)).ToList();
            }
            else
            {
                links = db.GIAOVIENs.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            links = links.OrderByDescending(n => n.BoMonPhuTrach).ToList();
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/GIAOVIENs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAOVIEN gIAOVIEN = db.GIAOVIENs.Find(id);
            if (gIAOVIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIAOVIEN);
        }

        // GET: Admin/GIAOVIENs/Create
        public ActionResult Create()
        {
            ViewBag.BoMonPhuTrach = new SelectList(db.MonHocs, "MaMH", "TenMH");
            return View();
        }
        public JsonResult CheckEmailGV(string emailGV)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = db.GIAOVIENs.Where(x => x.EmailGV == emailGV).SingleOrDefault();
            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        // POST: Admin/GIAOVIENs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGV,HoTenGV,BoMonPhuTrach,SodtGV,EmailGV,QueQuan,MatKhauGV")] GIAOVIEN gIAOVIEN)
        {
            if (ModelState.IsValid)
            {
                gIAOVIEN.MaGV = Guid.NewGuid();
                db.GIAOVIENs.Add(gIAOVIEN);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Thêm giáo viên thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.BoMonPhuTrach = new SelectList(db.MonHocs, "MaMH", "TenMH", gIAOVIEN.BoMonPhuTrach);
            return View(gIAOVIEN);
        }

        // GET: Admin/GIAOVIENs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAOVIEN gIAOVIEN = db.GIAOVIENs.Find(id);
            if (gIAOVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoMonPhuTrach = new SelectList(db.MonHocs, "MaMH", "TenMH", gIAOVIEN.BoMonPhuTrach);
            return View(gIAOVIEN);
        }

        // POST: Admin/GIAOVIENs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGV,HoTenGV,BoMonPhuTrach,SodtGV,EmailGV,QueQuan,MatKhauGV")] GIAOVIEN gIAOVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIAOVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoMonPhuTrach = new SelectList(db.MonHocs, "MaMH", "TenMH", gIAOVIEN.BoMonPhuTrach);
            return View(gIAOVIEN);
        }

        // GET: Admin/GIAOVIENs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAOVIEN gIAOVIEN = db.GIAOVIENs.Find(id);
            if (gIAOVIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIAOVIEN);
        }

        // POST: Admin/GIAOVIENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            GIAOVIEN gIAOVIEN = db.GIAOVIENs.Find(id);
            db.GIAOVIENs.Remove(gIAOVIEN);
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
