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
    public class punjenjeMljekomataController : Controller
    {
        private Entities db = new Entities();

        // GET: punjenjeMljekomata
        public ActionResult Index()
        {
            int userId = Int32.Parse(User.Identity.Name);
            var punjenje_mljekomata = db.punjenje_mljekomata.Include(p => p.mljekomati).Where(p => p.mljekomati.id_korisnika == userId);
            return View(punjenje_mljekomata.ToList());
        }

        // GET: punjenjeMljekomata/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int userId = Int32.Parse(User.Identity.Name);
            punjenje_mljekomata punjenje_mljekomata = db.punjenje_mljekomata.Find(id);
            if (punjenje_mljekomata == null || punjenje_mljekomata.mljekomati.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            return View(punjenje_mljekomata);
        }

        // GET: punjenjeMljekomata/Create
        public ActionResult Create()
        {
            int userId = Int32.Parse(User.Identity.Name);
            if (Url.RequestContext.RouteData.Values["id"] != null)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                int Ajdi = Int32.Parse(x);
                var idPunjenja = db.mljekomati.Where(p => p.id_korisnika == userId && p.id == Ajdi).FirstOrDefault();
                if (idPunjenja == null)
                {
                    return HttpNotFound();
                }
                return View();
            }
            return HttpNotFound();
        }

        // POST: punjenjeMljekomata/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,datum,troskovi,profit,preostala_kolicina")] punjenje_mljekomata punjenje_mljekomata)
        {
            if (ModelState.IsValid)
            {
                db.punjenje_mljekomata.Add(punjenje_mljekomata);
                db.SaveChanges();
                return RedirectToAction("Index", "mljekomati");
            }

            ViewBag.id_mljekomata = new SelectList(db.mljekomati, "id", "lokacija", punjenje_mljekomata.id_mljekomata);
            return View(punjenje_mljekomata);
        }

        // GET: punjenjeMljekomata/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            punjenje_mljekomata punjenje_mljekomata = db.punjenje_mljekomata.Find(id);
            int userId = Int32.Parse(User.Identity.Name);
            if (punjenje_mljekomata == null || punjenje_mljekomata.mljekomati.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            ViewBag.id_mljekomata = new SelectList(db.mljekomati, "id", "lokacija", punjenje_mljekomata.id_mljekomata);
            return View(punjenje_mljekomata);
        }

        // POST: punjenjeMljekomata/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_mljekomata,datum,troskovi,profit,preostala_kolicina")] punjenje_mljekomata punjenje_mljekomata)
        {
            if (ModelState.IsValid)
            {              
                db.Entry(punjenje_mljekomata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "mljekomati");

            }
            ViewBag.id_mljekomata = new SelectList(db.mljekomati, "id", "lokacija", punjenje_mljekomata.id_mljekomata);
            return View(punjenje_mljekomata);
        }

        // GET: punjenjeMljekomata/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int userId = Int32.Parse(User.Identity.Name);
            punjenje_mljekomata punjenje_mljekomata = db.punjenje_mljekomata.Find(id);
            if (punjenje_mljekomata == null || punjenje_mljekomata.mljekomati.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            return View(punjenje_mljekomata);
        }

        // POST: punjenjeMljekomata/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            punjenje_mljekomata punjenje_mljekomata = db.punjenje_mljekomata.Find(id);
            db.punjenje_mljekomata.Remove(punjenje_mljekomata);
            db.SaveChanges();
            return RedirectToAction("Index", "mljekomati");
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
