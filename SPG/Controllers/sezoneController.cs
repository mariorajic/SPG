using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SPG;

namespace SPG.Controllers
{
    public class sezoneController : Controller
    {
        private Entities db = new Entities();

        // GET: sezone
        public ActionResult Index()
        {
            return View(db.sezone.ToList());
        }

        // GET: sezone/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sezone sezone = db.sezone.Find(id);
            if (sezone == null)
            {
                return HttpNotFound();
            }
            return View(sezone);
        }

        // GET: sezone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sezone/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,sezona")] sezone sezone)
        {
            if (ModelState.IsValid)
            {
                db.sezone.Add(sezone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sezone);
        }

        // GET: sezone/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sezone sezone = db.sezone.Find(id);
            if (sezone == null)
            {
                return HttpNotFound();
            }
            return View(sezone);
        }

        // POST: sezone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,sezona")] sezone sezone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sezone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sezone);
        }

        // GET: sezone/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sezone sezone = db.sezone.Find(id);
            if (sezone == null)
            {
                return HttpNotFound();
            }
            return View(sezone);
        }

        // POST: sezone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sezone sezone = db.sezone.Find(id);
            db.sezone.Remove(sezone);
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
