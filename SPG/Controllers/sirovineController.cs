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
    public class sirovineController : Controller
    {
        private Entities db = new Entities();

        // GET: sirovine
        public ActionResult Index()
        {
            var sirovine = db.sirovine.Include(s => s.zivotinje);
            return View(sirovine.ToList());
        }

        // GET: sirovine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sirovine sirovine = db.sirovine.Find(id);
            if (sirovine == null)
            {
                return HttpNotFound();
            }
            return View(sirovine);
        }

        // GET: sirovine/Create
        public ActionResult Create()
        {
            ViewBag.id_zivotinje = new SelectList(db.zivotinje, "id", "vrsta");
            return View();
        }

        // POST: sirovine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_zivotinje,naziv,datum,kolicina")] sirovine sirovine)
        {
            if (ModelState.IsValid)
            {
                db.sirovine.Add(sirovine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_zivotinje = new SelectList(db.zivotinje, "id", "vrsta", sirovine.id_zivotinje);
            return View(sirovine);
        }

        // GET: sirovine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sirovine sirovine = db.sirovine.Find(id);
            if (sirovine == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_zivotinje = new SelectList(db.zivotinje, "id", "vrsta", sirovine.id_zivotinje);
            return View(sirovine);
        }

        // POST: sirovine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_zivotinje,naziv,datum,kolicina")] sirovine sirovine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sirovine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_zivotinje = new SelectList(db.zivotinje, "id", "vrsta", sirovine.id_zivotinje);
            return View(sirovine);
        }

        // GET: sirovine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sirovine sirovine = db.sirovine.Find(id);
            if (sirovine == null)
            {
                return HttpNotFound();
            }
            return View(sirovine);
        }

        // POST: sirovine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sirovine sirovine = db.sirovine.Find(id);
            db.sirovine.Remove(sirovine);
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
