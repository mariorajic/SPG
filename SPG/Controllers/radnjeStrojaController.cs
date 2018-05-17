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
    public class radnjeStrojaController : Controller
    {
        private Entities db = new Entities();

        // GET: radnjeStroja
        public ActionResult Index()
        {
            var radnje_stroja = db.radnje_stroja.Include(r => r.strojevi).Include(r => r.tip_radnje_stroja1);
            return View(radnje_stroja.ToList());
        }

        // GET: radnjeStroja/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            radnje_stroja radnje_stroja = db.radnje_stroja.Find(id);
            if (radnje_stroja == null)
            {
                return HttpNotFound();
            }
            return View(radnje_stroja);
        }

        // GET: radnjeStroja/Create
        public ActionResult Create()
        {
            ViewBag.id_stroja = new SelectList(db.strojevi, "id", "naziv");
            ViewBag.tip_radnje_stroja = new SelectList(db.tip_radnje_stroja, "id", "naziv");
            return View();
        }

        // POST: radnjeStroja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,naziv,datum,trosak,profit,id_stroja,tip_radnje_stroja")] radnje_stroja radnje_stroja)
        {
            if (ModelState.IsValid)
            {
                db.radnje_stroja.Add(radnje_stroja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_stroja = new SelectList(db.strojevi, "id", "naziv", radnje_stroja.id_stroja);
            ViewBag.tip_radnje_stroja = new SelectList(db.tip_radnje_stroja, "id", "naziv", radnje_stroja.tip_radnje_stroja);
            return View(radnje_stroja);
        }

        // GET: radnjeStroja/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            radnje_stroja radnje_stroja = db.radnje_stroja.Find(id);
            if (radnje_stroja == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_stroja = new SelectList(db.strojevi, "id", "naziv", radnje_stroja.id_stroja);
            ViewBag.tip_radnje_stroja = new SelectList(db.tip_radnje_stroja, "id", "naziv", radnje_stroja.tip_radnje_stroja);
            return View(radnje_stroja);
        }

        // POST: radnjeStroja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,naziv,datum,trosak,profit,id_stroja,tip_radnje_stroja")] radnje_stroja radnje_stroja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(radnje_stroja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_stroja = new SelectList(db.strojevi, "id", "naziv", radnje_stroja.id_stroja);
            ViewBag.tip_radnje_stroja = new SelectList(db.tip_radnje_stroja, "id", "naziv", radnje_stroja.tip_radnje_stroja);
            return View(radnje_stroja);
        }

        // GET: radnjeStroja/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            radnje_stroja radnje_stroja = db.radnje_stroja.Find(id);
            if (radnje_stroja == null)
            {
                return HttpNotFound();
            }
            return View(radnje_stroja);
        }

        // POST: radnjeStroja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            radnje_stroja radnje_stroja = db.radnje_stroja.Find(id);
            db.radnje_stroja.Remove(radnje_stroja);
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
