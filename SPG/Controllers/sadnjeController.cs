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
    public class sadnjeController : Controller
    {
        private Entities db = new Entities();

        // GET: sadnje
        public ActionResult Index()
        {
            var sadnje = db.sadnje.Include(s => s.biljke).Include(s => s.oranice).Include(s => s.sezone);
            return View(sadnje.ToList());
        }

        // GET: sadnje/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sadnje sadnje = db.sadnje.Find(id);
            if (sadnje == null)
            {
                return HttpNotFound();
            }
            return View(sadnje);
        }

        // GET: sadnje/Create
        public ActionResult Create()
        {
            ViewBag.biljka = new SelectList(db.biljke, "id", "naziv");
            ViewBag.id_oranice = new SelectList(db.oranice, "id", "naziv");
            ViewBag.sezona = new SelectList(db.sezone, "id", "sezona");
            return View();
        }

        // POST: sadnje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,biljka,id_oranice,datum,kolicina,troskovi,sezona")] sadnje sadnje)
        {
            if (ModelState.IsValid)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                int Ajdi = Int32.Parse(x);
                sadnje.id_oranice = Ajdi;
                db.sadnje.Add(sadnje);
                db.SaveChanges();
                return RedirectToAction("Details", "oranice", new { id = sadnje.id_oranice });
            }

            ViewBag.biljka = new SelectList(db.biljke, "id", "naziv", sadnje.biljka);
            ViewBag.id_oranice = new SelectList(db.oranice, "id", "naziv", sadnje.id_oranice);
            ViewBag.sezona = new SelectList(db.sezone, "id", "sezona", sadnje.sezona);
            return View(sadnje);
        }

        // GET: sadnje/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sadnje sadnje = db.sadnje.Find(id);
            if (sadnje == null)
            {
                return HttpNotFound();
            }
            ViewBag.biljka = new SelectList(db.biljke, "id", "naziv", sadnje.biljka);
            ViewBag.id_oranice = new SelectList(db.oranice, "id", "naziv", sadnje.id_oranice);
            ViewBag.sezona = new SelectList(db.sezone, "id", "sezona", sadnje.sezona);
            return View(sadnje);
        }

        // POST: sadnje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,biljka,id_oranice,datum,kolicina,troskovi,sezona")] sadnje sadnje)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(sadnje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "oranice", new { id = sadnje.id_oranice });
            }
            ViewBag.biljka = new SelectList(db.biljke, "id", "naziv", sadnje.biljka);
            ViewBag.id_oranice = new SelectList(db.oranice, "id", "naziv", sadnje.id_oranice);
            ViewBag.sezona = new SelectList(db.sezone, "id", "sezona", sadnje.sezona);
            return View(sadnje);
        }

        // GET: sadnje/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sadnje sadnje = db.sadnje.Find(id);
            if (sadnje == null)
            {
                return HttpNotFound();
            }
            return View(sadnje);
        }

        // POST: sadnje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sadnje sadnje = db.sadnje.Find(id);
            var id_oranice = sadnje.id_oranice;
            try
            {
                db.sadnje.Remove(sadnje);
                db.SaveChanges();
            }
            catch
            {
                return View("Error");
            }
            return RedirectToAction("Details", "oranica", new { id = id_oranice });
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
