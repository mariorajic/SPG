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
    public class troskoviController : Controller
    {
        private Entities db = new Entities();

        // GET: troskovi
        public ActionResult Index()
        {
            var troskovi = db.troskovi.Include(t => t.parcele);
            return View(troskovi.ToList());
        }

        // GET: troskovi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            troskovi troskovi = db.troskovi.Find(id);
            if (troskovi == null)
            {
                return HttpNotFound();
            }
            return View(troskovi);
        }

        // GET: troskovi/Create
        public ActionResult Create()
        {
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate");
            return View();
        }

        // POST: troskovi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,naziv,datum,trosak,id_parcele")] troskovi troskovi)
        {
            if (ModelState.IsValid)
            {
                db.troskovi.Add(troskovi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", troskovi.id_parcele);
            return View(troskovi);
        }

        // GET: troskovi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            troskovi troskovi = db.troskovi.Find(id);
            if (troskovi == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", troskovi.id_parcele);
            return View(troskovi);
        }

        // POST: troskovi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,naziv,datum,trosak,id_parcele")] troskovi troskovi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(troskovi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", troskovi.id_parcele);
            return View(troskovi);
        }

        // GET: troskovi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            troskovi troskovi = db.troskovi.Find(id);
            if (troskovi == null)
            {
                return HttpNotFound();
            }
            return View(troskovi);
        }

        // POST: troskovi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            troskovi troskovi = db.troskovi.Find(id);
            db.troskovi.Remove(troskovi);
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
