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
    public class sirovineController : Controller
    {
        private Entities db = new Entities();

        // GET: sirovine
        public ActionResult Index()
        {
            /*var sirovine = db.sirovine.Include(s => s.zivotinje);
            return View(sirovine.ToList());*/
            return RedirectToAction("Index", "parcele");
        }

        // GET: sirovine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sirovine sirovine = db.sirovine.Find(id);
            var userId = Int32.Parse(User.Identity.Name);
            if (sirovine == null || sirovine.zivotinje.farme.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            return View(sirovine);
        }

        // GET: sirovine/Create
        public ActionResult Create()
        {
            if (Url.RequestContext.RouteData.Values["id"] != null)
            {
                return View();
            }
            return HttpNotFound();
        }

        // POST: sirovine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_zivotinje,naziv,datum,kolicina")] sirovine sirovine)
        {
            if (ModelState.IsValid)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                int Ajdi = Int32.Parse(x);
                sirovine.id_zivotinje = Ajdi;
                db.sirovine.Add(sirovine);
                db.SaveChanges();
                return RedirectToAction("Details", "farme", new { id = db.zivotinje.Find(sirovine.id_zivotinje).id_farme});
            }

            return View(sirovine);
        }

        // GET: sirovine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sirovine sirovine = db.sirovine.Find(id);
            var userId = Int32.Parse(User.Identity.Name);
            if (sirovine == null || sirovine.zivotinje.farme.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
           
            return View(sirovine);
        }

        // POST: sirovine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_zivotinje,naziv,datum,kolicina")] sirovine sirovine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sirovine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "farme", new { id = db.zivotinje.Find(sirovine.id_zivotinje).id_farme });
            }
            return View(sirovine);
        }

        // GET: sirovine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sirovine sirovine = db.sirovine.Find(id);
            var userId = Int32.Parse(User.Identity.Name);
            if (sirovine == null || sirovine.zivotinje.farme.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            return View(sirovine);
        }

        // POST: sirovine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sirovine sirovine = db.sirovine.Find(id);
            var id_farme = sirovine.zivotinje.id_farme;
            try
            {
                db.sirovine.Remove(sirovine);
                db.SaveChanges();
            }
            catch
            {
                return View("Error");
            }

            return RedirectToAction("Details", "farme", new { id = id_farme });
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
