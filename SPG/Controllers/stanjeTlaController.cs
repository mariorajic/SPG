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
    public class stanjeTlaController : Controller
    {
        private Entities db = new Entities();

        // GET: stanjeTla
        public ActionResult Index()
        {
            return View(db.stanje_tla.ToList());
        }

        // GET: stanjeTla/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stanje_tla stanje_tla = db.stanje_tla.Find(id);
            if (stanje_tla == null)
            {
                return HttpNotFound();
            }
            return View(stanje_tla);
        }

        // GET: stanjeTla/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: stanjeTla/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,stanje")] stanje_tla stanje_tla)
        {
            if (ModelState.IsValid)
            {
                db.stanje_tla.Add(stanje_tla);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stanje_tla);
        }

        // GET: stanjeTla/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stanje_tla stanje_tla = db.stanje_tla.Find(id);
            if (stanje_tla == null)
            {
                return HttpNotFound();
            }
            return View(stanje_tla);
        }

        // POST: stanjeTla/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,stanje")] stanje_tla stanje_tla)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stanje_tla).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stanje_tla);
        }

        // GET: stanjeTla/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stanje_tla stanje_tla = db.stanje_tla.Find(id);
            if (stanje_tla == null)
            {
                return HttpNotFound();
            }
            return View(stanje_tla);
        }

        // POST: stanjeTla/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            stanje_tla stanje_tla = db.stanje_tla.Find(id);
            db.stanje_tla.Remove(stanje_tla);
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
