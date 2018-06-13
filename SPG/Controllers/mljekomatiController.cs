using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SPG;
using Rotativa;

namespace SPG.Controllers
{
    public class mljekomatiController : Controller
    {
        private Entities db = new Entities();


        // GET: mljekomati
        public ActionResult Index()
        {
            int userId = Int32.Parse(User.Identity.Name);
            var mljekomati = db.mljekomati.Include(m => m.gospodarstva).Where(m => m.id_korisnika == userId);
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
            int userId = Int32.Parse(User.Identity.Name);
            string x = Url.RequestContext.RouteData.Values["id"].ToString();
            int Ajdi = Int32.Parse(x);
            if (mljekomati == null || mljekomati.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            ViewData["Servisi"] = db.servisi_mljekomata.Where(f => f.id_mljekomata == Ajdi).ToList();
            ViewData["Punjenja"] = db.punjenje_mljekomata.Where(f => f.id_mljekomata == Ajdi).ToList();
            return View(mljekomati);
        }

        // GET: mljekomati/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: mljekomati/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,lokacija,kapacitet")] mljekomati mljekomati)
        {
            if (ModelState.IsValid)
            {
                int userId = Int32.Parse(User.Identity.Name);
                mljekomati.id_korisnika = userId;
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
            int userId = Int32.Parse(User.Identity.Name);
            if (mljekomati == null || mljekomati.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            
            return View(mljekomati);
        }

        // POST: mljekomati/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,lokacija,kapacitet")] mljekomati mljekomati)
        {
            if (ModelState.IsValid)
            {
                int userId = Int32.Parse(User.Identity.Name);
                db.Entry(mljekomati).State = EntityState.Modified;
                mljekomati.id_korisnika = userId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            int userId = Int32.Parse(User.Identity.Name);
            if (mljekomati == null || mljekomati.id_korisnika != userId)
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
