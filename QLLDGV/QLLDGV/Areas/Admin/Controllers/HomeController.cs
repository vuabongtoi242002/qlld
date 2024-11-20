using Database.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLLDGV.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private UserDbContext db = new UserDbContext();
        // GET: Admin/Home
        public ActionResult Index(int? page, string SearchString, string currentFilter)
        {
            var links = new List<Message>();
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
                links = db.Messages.Where(n => n.GIAOVIEN.HoTenGV.Contains(SearchString)).ToList();
            }
            else
            {
                links = db.Messages.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            links = links.OrderByDescending(n => n.NgayGui).ToList();
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhanHoi")] Message mess)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(mess);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mess);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message mess = db.Messages.Find(id);
            if (mess == null)
            {
                return HttpNotFound();
            }
            return View(mess);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Message mess = db.Messages.Find(id);
            db.Messages.Remove(mess);
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