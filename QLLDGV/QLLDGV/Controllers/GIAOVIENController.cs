using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Database.Framework;

namespace QLLDGV.Controllers
{
    public class GIAOVIENController : BaseGVController
    {
        private UserDbContext db = new UserDbContext();

        // GET: GIAOVIEN
        public ActionResult Index(Guid? id)
        {
            var session = (UserLogin)Session[QLLDGV.Common.CommonConstants.USER_SESSION];
            id = session.UserID;
            GIAOVIEN gIAOVIEN = db.GIAOVIENs.Find(id);
            if (gIAOVIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIAOVIEN);
        }

        // GET: GIAOVIEN/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var session = (UserLogin)Session[QLLDGV.Common.CommonConstants.USER_SESSION];
            id = session.UserID;
            GIAOVIEN gIAOVIEN = db.GIAOVIENs.Find(id);
            if (gIAOVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoMonPhuTrach = new SelectList(db.MonHocs, "MaMH", "TenMH", gIAOVIEN.BoMonPhuTrach);
            return View(gIAOVIEN);
        }

        // POST: GIAOVIEN/Edit/5
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
    }
}
