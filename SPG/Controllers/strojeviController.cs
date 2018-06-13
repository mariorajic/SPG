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
    [Authorize]
    public class strojeviController : Controller
    {
        private Entities db = new Entities();


        public int DobaviID()
        {
            int IdUsera = Int32.Parse(User.Identity.Name);
            return IdUsera;
        }
        // GET: strojevi
        public ActionResult Index()
        {
            int userId = DobaviID();
            var strojevi = db.strojevi.Include(s => s.parcele).Where(s => s.parcele.id_korisnika == userId);
            return View(strojevi.ToList());
        }

        // GET: strojevi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            strojevi strojevi = db.strojevi.Find(id);
            var userId = DobaviID();
            string x = Url.RequestContext.RouteData.Values["id"].ToString();
            int Ajdi = Int32.Parse(x);
            if (strojevi == null || strojevi.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            ViewData["Radnje"] = db.radnje_stroja.Where(f => f.id_stroja == Ajdi).ToList();
            return View(strojevi);
        }

        // GET: strojevi/Create
        public ActionResult Create()
        {
            int userId = DobaviID();
            ViewBag.id_parcele = new SelectList(db.parcele.Where(p => p.id_korisnika == userId), "id", "naziv");
            return View();
        }

        // POST: strojevi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,naziv,datum_kupovine,trosak,vrijednost,id_parcele")] strojevi strojevi)
        {
            if (ModelState.IsValid)
            {
                db.strojevi.Add(strojevi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_parcele = new SelectList(db.parcele, "id", "naziv", strojevi.id_parcele);
            return View(strojevi);
        }

        // GET: strojevi/Edit/5
        public ActionResult Edit(int? id)
        {
            int userId = DobaviID();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            strojevi strojevi = db.strojevi.Find(id);
            if (strojevi == null || strojevi.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            ViewBag.id_parcele = new SelectList(db.parcele.Where(p => p.id_korisnika == userId), "id", "naziv", strojevi.id_parcele);
            return View(strojevi);
        }

        // POST: strojevi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,naziv,datum_kupovine,trosak,vrijednost,id_parcele")] strojevi strojevi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(strojevi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_parcele = new SelectList(db.parcele, "id", "naziv", strojevi.id_parcele);
            return View(strojevi);
        }

        // GET: strojevi/Delete/5
        public ActionResult Delete(int? id)
        {
            int userId = DobaviID();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            strojevi strojevi = db.strojevi.Find(id);
            if (strojevi == null || strojevi.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            return View(strojevi);
        }

        // POST: strojevi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            strojevi strojevi = db.strojevi.Find(id);
            db.strojevi.Remove(strojevi);
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
