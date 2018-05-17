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
    public class strojeviController : Controller
    {
        private Entities db = new Entities();

        // GET: strojevi
        public ActionResult Index()
        {
            var strojevi = db.strojevi.Include(s => s.parcele);
            return View(strojevi.ToList());
        }

        // GET: strojevi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            strojevi strojevi = db.strojevi.Find(id);
            if (strojevi == null)
            {
                return HttpNotFound();
            }
            return View(strojevi);
        }

        // GET: strojevi/Create
        public ActionResult Create()
        {
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate");
            return View();
        }

        // POST: strojevi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,naziv,datum_kupovine,trosak,vrijednost,id_parcele")] strojevi strojevi)
        {
            if (ModelState.IsValid)
            {
                db.strojevi.Add(strojevi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", strojevi.id_parcele);
            return View(strojevi);
        }

        // GET: strojevi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            strojevi strojevi = db.strojevi.Find(id);
            if (strojevi == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", strojevi.id_parcele);
            return View(strojevi);
        }

        // POST: strojevi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,naziv,datum_kupovine,trosak,vrijednost,id_parcele")] strojevi strojevi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(strojevi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", strojevi.id_parcele);
            return View(strojevi);
        }

        // GET: strojevi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            strojevi strojevi = db.strojevi.Find(id);
            if (strojevi == null)
            {
                return HttpNotFound();
            }
            return View(strojevi);
        }

        // POST: strojevi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            strojevi strojevi = db.strojevi.Find(id);
            db.strojevi.Remove(strojevi);
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
