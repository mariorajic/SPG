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
    public class berbeController : Controller
    {
        private Entities db = new Entities();

        // GET: berbe
        public ActionResult Index()
        {
            var berbe = db.berbe.Include(b => b.sadnje).Include(b => b.sezone);
            return View(berbe.ToList());
        }

        // GET: berbe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            berbe berbe = db.berbe.Find(id);
            if (berbe == null)
            {
                return HttpNotFound();
            }
            return View(berbe);
        }

        // GET: berbe/Create
        public ActionResult Create()
        {
            ViewBag.id_sadnje = new SelectList(db.sadnje, "id", "id");
            ViewBag.sezona = new SelectList(db.sezone, "id", "sezona");
            return View();
        }

        // POST: berbe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_sadnje,datum,kolicina,sezona")] berbe berbe)
        {
            if (ModelState.IsValid)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                int Ajdi = Int32.Parse(x);
                berbe.id_sadnje = Ajdi;
                db.berbe.Add(berbe);
                db.SaveChanges();
                return RedirectToAction("Details", "oranice", new { id = db.sadnje.Find(berbe.id_sadnje).id_oranice });
            }

            ViewBag.id_sadnje = new SelectList(db.sadnje, "id", "id", berbe.id_sadnje);
            ViewBag.sezona = new SelectList(db.sezone, "id", "sezona", berbe.sezona);
            return View(berbe);
        }

        // GET: berbe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            berbe berbe = db.berbe.Find(id);
            if (berbe == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_sadnje = new SelectList(db.sadnje, "id", "kolicina", berbe.id_sadnje);
            ViewBag.sezona = new SelectList(db.sezone, "id", "sezona", berbe.sezona);
            return View(berbe);
        }

        // POST: berbe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_sadnje,datum,kolicina,sezona")] berbe berbe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(berbe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "oranice", new { id = db.sadnje.Find(berbe.id_sadnje).id_oranice });
            }
            ViewBag.id_sadnje = new SelectList(db.sadnje, "id", "kolicina", berbe.id_sadnje);
            ViewBag.sezona = new SelectList(db.sezone, "id", "sezona", berbe.sezona);
            return View(berbe);
        }

        // GET: berbe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            berbe berbe = db.berbe.Find(id);
            if (berbe == null)
            {
                return HttpNotFound();
            }
            return View(berbe);
        }

        // POST: berbe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            berbe berbe = db.berbe.Find(id);
            var id_sadnje = berbe.id_sadnje;
            db.berbe.Remove(berbe);
            db.SaveChanges();
            return RedirectToAction("Details", "oranice", new { id = db.sadnje.Find(id_sadnje).id_oranice });
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
