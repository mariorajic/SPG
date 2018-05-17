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
    public class prodajeBiljakaController : Controller
    {
        private Entities db = new Entities();

        // GET: prodajeBiljaka
        public ActionResult Index()
        {
            var prodaje_biljaka = db.prodaje_biljaka.Include(p => p.biljke);
            return View(prodaje_biljaka.ToList());
        }

        // GET: prodajeBiljaka/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prodaje_biljaka prodaje_biljaka = db.prodaje_biljaka.Find(id);
            if (prodaje_biljaka == null)
            {
                return HttpNotFound();
            }
            return View(prodaje_biljaka);
        }

        // GET: prodajeBiljaka/Create
        public ActionResult Create()
        {
            ViewBag.id_biljke = new SelectList(db.biljke, "id", "naziv");
            return View();
        }

        // POST: prodajeBiljaka/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_biljke,kolicina,profit")] prodaje_biljaka prodaje_biljaka)
        {
            if (ModelState.IsValid)
            {
                db.prodaje_biljaka.Add(prodaje_biljaka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_biljke = new SelectList(db.biljke, "id", "naziv", prodaje_biljaka.id_biljke);
            return View(prodaje_biljaka);
        }

        // GET: prodajeBiljaka/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prodaje_biljaka prodaje_biljaka = db.prodaje_biljaka.Find(id);
            if (prodaje_biljaka == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_biljke = new SelectList(db.biljke, "id", "naziv", prodaje_biljaka.id_biljke);
            return View(prodaje_biljaka);
        }

        // POST: prodajeBiljaka/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_biljke,kolicina,profit")] prodaje_biljaka prodaje_biljaka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodaje_biljaka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_biljke = new SelectList(db.biljke, "id", "naziv", prodaje_biljaka.id_biljke);
            return View(prodaje_biljaka);
        }

        // GET: prodajeBiljaka/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prodaje_biljaka prodaje_biljaka = db.prodaje_biljaka.Find(id);
            if (prodaje_biljaka == null)
            {
                return HttpNotFound();
            }
            return View(prodaje_biljaka);
        }

        // POST: prodajeBiljaka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            prodaje_biljaka prodaje_biljaka = db.prodaje_biljaka.Find(id);
            db.prodaje_biljaka.Remove(prodaje_biljaka);
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
