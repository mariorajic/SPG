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
    public class farmeController : Controller
    {
        private Entities db = new Entities();

        // GET: farme
        public ActionResult Index()
        {
            var farme = db.farme.Include(f => f.parcele);
            return View(farme.ToList());
        }

        // GET: farme/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farme farme = db.farme.Find(id);
            if (farme == null)
            {
                return HttpNotFound();
            }
            return View(farme);
        }

        // GET: farme/Create
        public ActionResult Create()
        {
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate");
            return View();
        }

        // POST: farme/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_parcele,naziv")] farme farme)
        {
            if (ModelState.IsValid)
            {
                db.farme.Add(farme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", farme.id_parcele);
            return View(farme);
        }

        // GET: farme/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farme farme = db.farme.Find(id);
            if (farme == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", farme.id_parcele);
            return View(farme);
        }

        // POST: farme/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_parcele,naziv")] farme farme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "koordinate", farme.id_parcele);
            return View(farme);
        }

        // GET: farme/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farme farme = db.farme.Find(id);
            if (farme == null)
            {
                return HttpNotFound();
            }
            return View(farme);
        }

        // POST: farme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            farme farme = db.farme.Find(id);
            db.farme.Remove(farme);
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
