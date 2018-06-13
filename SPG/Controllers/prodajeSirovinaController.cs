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
    public class prodajeSirovinaController : Controller
    {
        private Entities db = new Entities();

        public ActionResult PrintToPdf(int? id)
        {

            return new Rotativa.ActionAsPdf("Details", new { id = id }) { FileName = "ProdajaSirovineAsPdf.pdf" };
        }

        // GET: prodajeSirovina
        public ActionResult Index()
        {
            /*var prodaje_sirovina = db.prodaje_sirovina.Include(p => p.sirovine);
            return View(prodaje_sirovina.ToList());*/
            return RedirectToAction("Index", "parcele");
        }

        // GET: prodajeSirovina/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prodaje_sirovina prodaje_sirovina = db.prodaje_sirovina.Find(id);
            if (prodaje_sirovina == null)
            {
                return HttpNotFound();
            }
            return View(prodaje_sirovina);
        }

        // GET: prodajeSirovina/Create
        public ActionResult Create()
        {
            if (Url.RequestContext.RouteData.Values["id"] != null)
            {
                return View();
            }
            return HttpNotFound();
        }

        // POST: prodajeSirovina/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_sirovine,kolicina,datum,profit")] prodaje_sirovina prodaje_sirovina)
        {
            if (ModelState.IsValid)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                int Ajdi = Int32.Parse(x);
                prodaje_sirovina.id_sirovine = Ajdi;
                db.prodaje_sirovina.Add(prodaje_sirovina);
                db.SaveChanges();
                return RedirectToAction("Details", "farme", new { id = db.sirovine.Find(Ajdi).zivotinje.id_farme });
            }

            return View(prodaje_sirovina);
        }

        // GET: prodajeSirovina/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prodaje_sirovina prodaje_sirovina = db.prodaje_sirovina.Find(id);
            if (prodaje_sirovina == null)
            {
                return HttpNotFound();
            }
            return View(prodaje_sirovina);
        }

        // POST: prodajeSirovina/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_sirovine,kolicina,datum,profit")] prodaje_sirovina prodaje_sirovina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodaje_sirovina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "farme", new { id = db.sirovine.Find(prodaje_sirovina.id_sirovine).zivotinje.id_farme });
            }
            return View(prodaje_sirovina);
        }

        // GET: prodajeSirovina/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prodaje_sirovina prodaje_sirovina = db.prodaje_sirovina.Find(id);
            if (prodaje_sirovina == null)
            {
                return HttpNotFound();
            }
            return View(prodaje_sirovina);
        }

        // POST: prodajeSirovina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            prodaje_sirovina prodaje_sirovina = db.prodaje_sirovina.Find(id);
            var id_farme = db.sirovine.Find(prodaje_sirovina.id_sirovine).zivotinje.id_farme;
            try
            {
                db.prodaje_sirovina.Remove(prodaje_sirovina);
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
