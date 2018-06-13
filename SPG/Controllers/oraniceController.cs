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
    public class oraniceController : Controller
    {
        private Entities db = new Entities();

        // GET: oranice
        public ActionResult Index()
        {
            /*var oranice = db.oranice.Include(o => o.stanje_tla1).Include(o => o.parcele).Include(o => o.vrste_tla);
            return View(oranice.ToList());*/
            return RedirectToAction("Index", "parcele");
        }

        // GET: oranice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oranice oranice = db.oranice.Find(id);
            var userId = Int32.Parse(User.Identity.Name);
            if (oranice == null || oranice.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            ViewData["Sadnje"] = db.sadnje.Where(f => f.id_oranice == oranice.id).ToList();
            ViewData["Berbe"] = db.berbe.Where(f => f.sadnje.id_oranice == oranice.id).ToList();
            ViewData["Prodaje"] = db.prodaje_biljaka.Where(p => p.id_oranice == oranice.id).ToList();
            return View(oranice);
        }

        // GET: oranice/Create
        public ActionResult Create()
        {
            int userId = Int32.Parse(User.Identity.Name);
            ViewBag.stanje_tla = new SelectList(db.stanje_tla, "id", "stanje");
            ViewBag.id_parcele = new SelectList(db.parcele.Where(p => p.id_korisnika == userId), "id", "naziv");
            ViewBag.vrsta_tla = new SelectList(db.vrste_tla, "id", "vrsta");

            if (Url.RequestContext.RouteData.Values["id"] != null)
            {
                string id = Url.RequestContext.RouteData.Values["id"].ToString();

                parcele parcela = db.parcele.Find(Int32.Parse(id));
                if (parcela == null || parcela.id_korisnika != userId)
                {
                    return HttpNotFound();
                }
            }

            return View();
        }

        // POST: oranice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_parcele,naziv,dimenzije,stanje_tla,vrsta_tla")] oranice oranice)
        {
            var id = Url.RequestContext.RouteData.Values["id"];

            if (id != null)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                oranice.id_parcele = Int32.Parse(x);
            }
            if (ModelState.IsValid)
            {
                db.oranice.Add(oranice);
                db.SaveChanges();
                if (id != null)
                {
                    return RedirectToAction("Details", "parcele", new { id = Int32.Parse(id.ToString()) });
                } else
                {
                    return RedirectToAction("Index", "parcele");
                }
            }
            var userId = Int32.Parse(User.Identity.Name);
            ViewBag.stanje_tla = new SelectList(db.stanje_tla, "id", "stanje", oranice.stanje_tla);
            ViewBag.id_parcele = new SelectList(db.parcele.Where(p => p.id_korisnika == userId), "id", "naziv");
            ViewBag.vrsta_tla = new SelectList(db.vrste_tla, "id", "vrsta", oranice.vrsta_tla);
            return View(oranice);
        }

        // GET: oranice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oranice oranice = db.oranice.Find(id);
            var userId = Int32.Parse(User.Identity.Name);
            if (oranice == null || oranice.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            ViewBag.stanje_tla = new SelectList(db.stanje_tla, "id", "stanje", oranice.stanje_tla);
            ViewBag.id_parcele = new SelectList(db.parcele.Where(p => p.id_korisnika == userId), "id", "naziv");
            ViewBag.vrsta_tla = new SelectList(db.vrste_tla, "id", "vrsta", oranice.vrsta_tla);
            return View(oranice);
        }

        // POST: oranice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_parcele,naziv,dimenzije,stanje_tla,vrsta_tla")] oranice oranice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oranice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "parcele", new { id = oranice.id_parcele });
            }
            var userId = Int32.Parse(User.Identity.Name);
            ViewBag.stanje_tla = new SelectList(db.stanje_tla, "id", "stanje", oranice.stanje_tla);
            ViewBag.id_parcele = new SelectList(db.parcele.Where(p => p.id_korisnika == userId), "id", "naziv");
            ViewBag.vrsta_tla = new SelectList(db.vrste_tla, "id", "vrsta", oranice.vrsta_tla);
            return View(oranice);
        }

        // GET: oranice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oranice oranice = db.oranice.Find(id);
            var userId = Int32.Parse(User.Identity.Name);
            if (oranice == null || oranice.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            return View(oranice);
        }

        // POST: oranice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oranice oranice = db.oranice.Find(id);
            var id_parcele = oranice.id_parcele;
            try
            {
                db.oranice.Remove(oranice);
                db.SaveChanges();
            }
            catch
            {
                return View("Error");
            }

            return RedirectToAction("Details", "parcele", new { id = id_parcele });
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
