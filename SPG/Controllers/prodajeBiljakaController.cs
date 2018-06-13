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
    public class prodajeBiljakaController : Controller
    {
        private Entities db = new Entities();

        public ActionResult PrintToPdf(int? id)
        {

            return new Rotativa.ActionAsPdf("Details", new { id = id }) { FileName = "ProdajaBiljkeAsPdf.pdf" };
        }

        // GET: prodajeBiljaka
        public ActionResult Index()
        {
            var prodaje_biljaka = db.prodaje_biljaka.Include(p => p.berbe);
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
            if (Url.RequestContext.RouteData.Values["id"] != null)
            {
                return View();
            }
            return HttpNotFound();
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
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                int Ajdi = Int32.Parse(x);
                prodaje_biljaka.id_berbe = Ajdi;
                db.prodaje_biljaka.Add(prodaje_biljaka);
                db.SaveChanges();
                return RedirectToAction("Details", "oranice", new { id = db.berbe.Find(prodaje_biljaka.id_berbe).sadnje.id_oranice });
            }

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
            return View(prodaje_biljaka);
        }

        // POST: prodajeBiljaka/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_berbe,kolicina,profit")] prodaje_biljaka prodaje_biljaka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodaje_biljaka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "oranice", new { id = db.berbe.Find(prodaje_biljaka.id_berbe).sadnje.id_oranice });
            }
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
            var id_oranice = db.berbe.Find(prodaje_biljaka.id_berbe).sadnje.id_oranice;
            db.prodaje_biljaka.Remove(prodaje_biljaka);
            db.SaveChanges();
            return RedirectToAction("Details", "oranice", new { id = id_oranice });
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
