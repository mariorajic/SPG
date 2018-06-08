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
    public class zivotinjeController : Controller
    {
        private Entities db = new Entities();

        // GET: zivotinje
        public ActionResult Index()
        {
            var zivotinje = db.zivotinje.Include(z => z.farme);
            return View(zivotinje.ToList());
        }

        // GET: zivotinje/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zivotinje zivotinje = db.zivotinje.Find(id);
            if (zivotinje == null)
            {
                return HttpNotFound();
            }
            return View(zivotinje);
        }

        // GET: zivotinje/Create
        public ActionResult Create()
        {
            int userId = Int32.Parse(User.Identity.Name);
            if (Url.RequestContext.RouteData.Values["id"] != null)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                int Ajdi = Int32.Parse(x);
                var idZivotinje = db.farme.Where(p => p.parcele.id_korisnika == userId && p.id == Ajdi).FirstOrDefault();
                if (idZivotinje == null)
                {
                    return HttpNotFound();
                }
                return View();
            }
            return HttpNotFound();
        }

        // POST: zivotinje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_farme,vrsta,broj_taga,kolicina,status,uzrok_smrti")] zivotinje zivotinje)
        {
            if (ModelState.IsValid)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                int Ajdi = Int32.Parse(x);
                zivotinje.id_farme = Ajdi;
                db.zivotinje.Add(zivotinje);
                db.SaveChanges();
                return RedirectToAction("Details","Farme", new { id = Ajdi });
            }

            return View(zivotinje);
        }

        // GET: zivotinje/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zivotinje zivotinje = db.zivotinje.Find(id);
            if (zivotinje == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_farme = new SelectList(db.farme, "id", "naziv", zivotinje.id_farme);
            return View(zivotinje);
        }

        // POST: zivotinje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_farme,vrsta,broj_taga,kolicina,status,uzrok_smrti")] zivotinje zivotinje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zivotinje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_farme = new SelectList(db.farme, "id", "naziv", zivotinje.id_farme);
            return View(zivotinje);
        }

        // GET: zivotinje/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zivotinje zivotinje = db.zivotinje.Find(id);
            if (zivotinje == null)
            {
                return HttpNotFound();
            }
            return View(zivotinje);
        }

        // POST: zivotinje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            zivotinje zivotinje = db.zivotinje.Find(id);
            db.zivotinje.Remove(zivotinje);
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
