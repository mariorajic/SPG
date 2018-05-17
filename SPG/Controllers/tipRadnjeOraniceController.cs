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
    public class tipRadnjeOraniceController : Controller
    {
        private Entities db = new Entities();

        // GET: tipRadnjeOranice
        public ActionResult Index()
        {
            return View(db.tip_radnje_oranice.ToList());
        }

        // GET: tipRadnjeOranice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tip_radnje_oranice tip_radnje_oranice = db.tip_radnje_oranice.Find(id);
            if (tip_radnje_oranice == null)
            {
                return HttpNotFound();
            }
            return View(tip_radnje_oranice);
        }

        // GET: tipRadnjeOranice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tipRadnjeOranice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,naziv")] tip_radnje_oranice tip_radnje_oranice)
        {
            if (ModelState.IsValid)
            {
                db.tip_radnje_oranice.Add(tip_radnje_oranice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tip_radnje_oranice);
        }

        // GET: tipRadnjeOranice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tip_radnje_oranice tip_radnje_oranice = db.tip_radnje_oranice.Find(id);
            if (tip_radnje_oranice == null)
            {
                return HttpNotFound();
            }
            return View(tip_radnje_oranice);
        }

        // POST: tipRadnjeOranice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,naziv")] tip_radnje_oranice tip_radnje_oranice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tip_radnje_oranice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tip_radnje_oranice);
        }

        // GET: tipRadnjeOranice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tip_radnje_oranice tip_radnje_oranice = db.tip_radnje_oranice.Find(id);
            if (tip_radnje_oranice == null)
            {
                return HttpNotFound();
            }
            return View(tip_radnje_oranice);
        }

        // POST: tipRadnjeOranice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tip_radnje_oranice tip_radnje_oranice = db.tip_radnje_oranice.Find(id);
            db.tip_radnje_oranice.Remove(tip_radnje_oranice);
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
