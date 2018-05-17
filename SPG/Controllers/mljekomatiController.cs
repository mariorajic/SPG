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
    public class mljekomatiController : Controller
    {
        private Entities db = new Entities();

        // GET: mljekomati
        public ActionResult Index()
        {
            var mljekomati = db.mljekomati.Include(m => m.gospodarstva);
            return View(mljekomati.ToList());
        }

        // GET: mljekomati/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mljekomati mljekomati = db.mljekomati.Find(id);
            if (mljekomati == null)
            {
                return HttpNotFound();
            }
            return View(mljekomati);
        }

        // GET: mljekomati/Create
        public ActionResult Create()
        {
            ViewBag.id_korisnika = new SelectList(db.gospodarstva, "id", "ime");
            return View();
        }

        // POST: mljekomati/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_korisnika,lokacija,kapacitet")] mljekomati mljekomati)
        {
            if (ModelState.IsValid)
            {
                db.mljekomati.Add(mljekomati);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_korisnika = new SelectList(db.gospodarstva, "id", "ime", mljekomati.id_korisnika);
            return View(mljekomati);
        }

        // GET: mljekomati/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mljekomati mljekomati = db.mljekomati.Find(id);
            if (mljekomati == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_korisnika = new SelectList(db.gospodarstva, "id", "ime", mljekomati.id_korisnika);
            return View(mljekomati);
        }

        // POST: mljekomati/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_korisnika,lokacija,kapacitet")] mljekomati mljekomati)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mljekomati).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_korisnika = new SelectList(db.gospodarstva, "id", "ime", mljekomati.id_korisnika);
            return View(mljekomati);
        }

        // GET: mljekomati/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mljekomati mljekomati = db.mljekomati.Find(id);
            if (mljekomati == null)
            {
                return HttpNotFound();
            }
            return View(mljekomati);
        }

        // POST: mljekomati/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mljekomati mljekomati = db.mljekomati.Find(id);
            db.mljekomati.Remove(mljekomati);
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
