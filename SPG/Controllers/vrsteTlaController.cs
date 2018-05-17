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
    public class vrsteTlaController : Controller
    {
        private Entities db = new Entities();

        // GET: vrsteTla
        public ActionResult Index()
        {
            return View(db.vrste_tla.ToList());
        }

        // GET: vrsteTla/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vrste_tla vrste_tla = db.vrste_tla.Find(id);
            if (vrste_tla == null)
            {
                return HttpNotFound();
            }
            return View(vrste_tla);
        }

        // GET: vrsteTla/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: vrsteTla/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,vrsta")] vrste_tla vrste_tla)
        {
            if (ModelState.IsValid)
            {
                db.vrste_tla.Add(vrste_tla);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vrste_tla);
        }

        // GET: vrsteTla/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vrste_tla vrste_tla = db.vrste_tla.Find(id);
            if (vrste_tla == null)
            {
                return HttpNotFound();
            }
            return View(vrste_tla);
        }

        // POST: vrsteTla/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,vrsta")] vrste_tla vrste_tla)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vrste_tla).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vrste_tla);
        }

        // GET: vrsteTla/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vrste_tla vrste_tla = db.vrste_tla.Find(id);
            if (vrste_tla == null)
            {
                return HttpNotFound();
            }
            return View(vrste_tla);
        }

        // POST: vrsteTla/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vrste_tla vrste_tla = db.vrste_tla.Find(id);
            db.vrste_tla.Remove(vrste_tla);
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
