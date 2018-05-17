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
    public class oraniceController : Controller
    {
        private Entities db = new Entities();

        // GET: oranice
        public ActionResult Index()
        {
            var oranice = db.oranice.Include(o => o.stanje_tla1).Include(o => o.parcele).Include(o => o.vrste_tla);
            return View(oranice.ToList());
        }

        // GET: oranice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oranice oranice = db.oranice.Find(id);
            if (oranice == null)
            {
                return HttpNotFound();
            }
            return View(oranice);
        }

        // GET: oranice/Create
        public ActionResult Create()
        {
            ViewBag.stanje_tla = new SelectList(db.stanje_tla, "id", "stanje");
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate");
            ViewBag.vrsta_tla = new SelectList(db.vrste_tla, "id", "vrsta");
            return View();
        }

        // POST: oranice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_parcele,naziv,dimenzije,stanje_tla,vrsta_tla")] oranice oranice)
        {
            if (ModelState.IsValid)
            {
                db.oranice.Add(oranice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.stanje_tla = new SelectList(db.stanje_tla, "id", "stanje", oranice.stanje_tla);
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", oranice.id_parcele);
            ViewBag.vrsta_tla = new SelectList(db.vrste_tla, "id", "vrsta", oranice.vrsta_tla);
            return View(oranice);
        }

        // GET: oranice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oranice oranice = db.oranice.Find(id);
            if (oranice == null)
            {
                return HttpNotFound();
            }
            ViewBag.stanje_tla = new SelectList(db.stanje_tla, "id", "stanje", oranice.stanje_tla);
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", oranice.id_parcele);
            ViewBag.vrsta_tla = new SelectList(db.vrste_tla, "id", "vrsta", oranice.vrsta_tla);
            return View(oranice);
        }

        // POST: oranice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_parcele,naziv,dimenzije,stanje_tla,vrsta_tla")] oranice oranice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oranice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.stanje_tla = new SelectList(db.stanje_tla, "id", "stanje", oranice.stanje_tla);
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", oranice.id_parcele);
            ViewBag.vrsta_tla = new SelectList(db.vrste_tla, "id", "vrsta", oranice.vrsta_tla);
            return View(oranice);
        }

        // GET: oranice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oranice oranice = db.oranice.Find(id);
            if (oranice == null)
            {
                return HttpNotFound();
            }
            return View(oranice);
        }

        // POST: oranice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oranice oranice = db.oranice.Find(id);
            db.oranice.Remove(oranice);
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
