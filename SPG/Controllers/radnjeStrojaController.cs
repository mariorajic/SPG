using System;
using System.Collections;
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
    public class radnjeStrojaController : Controller
    {
        private Entities db = new Entities();

        // GET: radnjeStroja
        public ActionResult Index()
        {
            int userId = Int32.Parse(User.Identity.Name);
            var radnje_stroja = db.radnje_stroja.Include(r => r.strojevi).Include(r => r.tip_radnje_stroja1).Where(r => r.strojevi.parcele.id_korisnika == userId);
            return View(radnje_stroja.ToList());
        }

        // GET: radnjeStroja/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            radnje_stroja radnje_stroja = db.radnje_stroja.Find(id);
            int userId = Int32.Parse(User.Identity.Name);
            if (radnje_stroja == null || radnje_stroja.strojevi.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            return View(radnje_stroja);
        }

        // GET: radnjeStroja/Create
        public ActionResult Create()
        {
            int userId = Int32.Parse(User.Identity.Name);
            if (Url.RequestContext.RouteData.Values["id"] != null)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                int Ajdi = Int32.Parse(x);
                var idStroja = db.strojevi.Where(p => p.parcele.id_korisnika == userId && p.id == Ajdi).FirstOrDefault();
                if (idStroja == null)
                {
                    return HttpNotFound();
                }
                ViewBag.tip_radnje_stroja = new SelectList(db.tip_radnje_stroja, "id", "naziv");
                return View();
            }
            return HttpNotFound();
        }

        // POST: radnjeStroja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,naziv,datum,trosak,profit,tip_radnje_stroja")] radnje_stroja radnje_stroja)
        {
            if (ModelState.IsValid)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                int Ajdi = Int32.Parse(x);
                radnje_stroja.id_stroja = Ajdi;
                db.radnje_stroja.Add(radnje_stroja);
                db.SaveChanges();
                return RedirectToAction("Details", "strojevi", new { id = Ajdi });
            }
            ViewBag.tip_radnje_stroja = new SelectList(db.tip_radnje_stroja, "id", "naziv", radnje_stroja.tip_radnje_stroja);
            return View(radnje_stroja);
        }

     

        // GET: radnjeStroja/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            radnje_stroja radnje_stroja = db.radnje_stroja.Find(id);

            int userId = Int32.Parse(User.Identity.Name);
            if (radnje_stroja == null || radnje_stroja.strojevi.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            ViewBag.id_stroja = new SelectList(db.strojevi.Where(p => p.parcele.id_korisnika == userId), "id", "naziv", radnje_stroja.id_stroja);
            ViewBag.tip_radnje_stroja = new SelectList(db.tip_radnje_stroja, "id", "naziv", radnje_stroja.tip_radnje_stroja);
            return View(radnje_stroja);
        }

        // POST: radnjeStroja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,naziv,datum,trosak,profit,id_stroja,tip_radnje_stroja")] radnje_stroja radnje_stroja)
        {
            if (ModelState.IsValid)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                int Ajdi = Int32.Parse(x);
                db.Entry(radnje_stroja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "strojevi", new { id = radnje_stroja.id_stroja });
            }
        
            int userId = Int32.Parse(User.Identity.Name);
            ViewBag.id_stroja = new SelectList(db.strojevi.Where(p => p.parcele.id_korisnika == userId), "id", "naziv", radnje_stroja.id_stroja);
            ViewBag.tip_radnje_stroja = new SelectList(db.tip_radnje_stroja, "id", "naziv", radnje_stroja.tip_radnje_stroja);
            return View(radnje_stroja);
        }

        // GET: radnjeStroja/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            radnje_stroja radnje_stroja = db.radnje_stroja.Find(id);
            if (radnje_stroja == null)
            {
                return HttpNotFound();
            }
            return View(radnje_stroja);
        }

        // POST: radnjeStroja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            radnje_stroja radnje_stroja = db.radnje_stroja.Find(id);
            var id_stroja = radnje_stroja.id_stroja;
            db.radnje_stroja.Remove(radnje_stroja);
            db.SaveChanges();
            return RedirectToAction("Details", "strojevi", new { id = id_stroja });
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
