using Database.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLLDGV.Controllers
{
    public class HomeGVController : BaseGVController
    {
        private UserDbContext db = new UserDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var session = (UserLogin)Session[QLLDGV.Common.CommonConstants.USER_SESSION];
            var mess = db.Messages.Where(n => n.NguoiGui == session.UserID).ToList();
            return View(mess);
        }
        public ActionResult Details(Guid? id)
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
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMail,NguoiGui,NoiDung,NgayGui,TinhTrang,PhanHoi")] Message mess)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[QLLDGV.Common.CommonConstants.USER_SESSION];
                mess.MaMail = Guid.NewGuid();
                mess.NguoiGui = session.UserID;
                mess.NgayGui = DateTime.Now;
                mess.Tinhtrang = "Đã gửi";
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