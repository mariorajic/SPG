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
    public class servisiMljekomataController : Controller
    {
        private Entities db = new Entities();

        // GET: servisiMljekomata
        public ActionResult Index()
        {
            var servisi_mljekomata = db.servisi_mljekomata.Include(s => s.mljekomati);
            return View(servisi_mljekomata.ToList());
        }

        // GET: servisiMljekomata/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            servisi_mljekomata servisi_mljekomata = db.servisi_mljekomata.Find(id);
            if (servisi_mljekomata == null)
            {
                return HttpNotFound();
            }
            return View(servisi_mljekomata);
        }

        // GET: servisiMljekomata/Create
        public ActionResult Create()
        {
            ViewBag.id_mljekomata = new SelectList(db.mljekomati, "id", "lokacija");
            return View();
        }

        // POST: servisiMljekomata/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,datum,troskovi,id_mljekomata")] servisi_mljekomata servisi_mljekomata)
        {
            if (ModelState.IsValid)
            {
                db.servisi_mljekomata.Add(servisi_mljekomata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_mljekomata = new SelectList(db.mljekomati, "id", "lokacija", servisi_mljekomata.id_mljekomata);
            return View(servisi_mljekomata);
        }

        // GET: servisiMljekomata/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            servisi_mljekomata servisi_mljekomata = db.servisi_mljekomata.Find(id);
            if (servisi_mljekomata == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_mljekomata = new SelectList(db.mljekomati, "id", "lokacija", servisi_mljekomata.id_mljekomata);
            return View(servisi_mljekomata);
        }

        // POST: servisiMljekomata/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,datum,troskovi,id_mljekomata")] servisi_mljekomata servisi_mljekomata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servisi_mljekomata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_mljekomata = new SelectList(db.mljekomati, "id", "lokacija", servisi_mljekomata.id_mljekomata);
            return View(servisi_mljekomata);
        }

        // GET: servisiMljekomata/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            servisi_mljekomata servisi_mljekomata = db.servisi_mljekomata.Find(id);
            if (servisi_mljekomata == null)
            {
                return HttpNotFound();
            }
            return View(servisi_mljekomata);
        }

        // POST: servisiMljekomata/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            servisi_mljekomata servisi_mljekomata = db.servisi_mljekomata.Find(id);
            db.servisi_mljekomata.Remove(servisi_mljekomata);
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
