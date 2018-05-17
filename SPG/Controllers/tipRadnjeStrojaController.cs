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
    public class tipRadnjeStrojaController : Controller
    {
        private Entities db = new Entities();

        // GET: tipRadnjeStroja
        public ActionResult Index()
        {
            return View(db.tip_radnje_stroja.ToList());
        }

        // GET: tipRadnjeStroja/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tip_radnje_stroja tip_radnje_stroja = db.tip_radnje_stroja.Find(id);
            if (tip_radnje_stroja == null)
            {
                return HttpNotFound();
            }
            return View(tip_radnje_stroja);
        }

        // GET: tipRadnjeStroja/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tipRadnjeStroja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,naziv")] tip_radnje_stroja tip_radnje_stroja)
        {
            if (ModelState.IsValid)
            {
                db.tip_radnje_stroja.Add(tip_radnje_stroja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tip_radnje_stroja);
        }

        // GET: tipRadnjeStroja/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tip_radnje_stroja tip_radnje_stroja = db.tip_radnje_stroja.Find(id);
            if (tip_radnje_stroja == null)
            {
                return HttpNotFound();
            }
            return View(tip_radnje_stroja);
        }

        // POST: tipRadnjeStroja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,naziv")] tip_radnje_stroja tip_radnje_stroja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tip_radnje_stroja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tip_radnje_stroja);
        }

        // GET: tipRadnjeStroja/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tip_radnje_stroja tip_radnje_stroja = db.tip_radnje_stroja.Find(id);
            if (tip_radnje_stroja == null)
            {
                return HttpNotFound();
            }
            return View(tip_radnje_stroja);
        }

        // POST: tipRadnjeStroja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tip_radnje_stroja tip_radnje_stroja = db.tip_radnje_stroja.Find(id);
            db.tip_radnje_stroja.Remove(tip_radnje_stroja);
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
