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
    public class gradoviController : Controller
    {
        private Entities db = new Entities();

        // GET: gradovi
        public ActionResult Index()
        {
            return View(db.gradovi.ToList());
        }

        // GET: gradovi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gradovi gradovi = db.gradovi.Find(id);
            if (gradovi == null)
            {
                return HttpNotFound();
            }
            return View(gradovi);
        }

        // GET: gradovi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: gradovi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ime")] gradovi gradovi)
        {
            if (ModelState.IsValid)
            {
                db.gradovi.Add(gradovi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gradovi);
        }

        // GET: gradovi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gradovi gradovi = db.gradovi.Find(id);
            if (gradovi == null)
            {
                return HttpNotFound();
            }
            return View(gradovi);
        }

        // POST: gradovi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ime")] gradovi gradovi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gradovi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gradovi);
        }

        // GET: gradovi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gradovi gradovi = db.gradovi.Find(id);
            if (gradovi == null)
            {
                return HttpNotFound();
            }
            return View(gradovi);
        }

        // POST: gradovi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gradovi gradovi = db.gradovi.Find(id);
            db.gradovi.Remove(gradovi);
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
