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
    public class farmeController : Controller
    {
        private Entities db = new Entities();

        // GET: farme
        public ActionResult Index()
        {
            /*var farme = db.farme.Include(f => f.parcele);
            return View(farme.ToList());*/
            return RedirectToAction("Index", "parcele");
        }

        // GET: farme/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farme farme = db.farme.Find(id);
            var userId = Int32.Parse(User.Identity.Name);
            string x = Url.RequestContext.RouteData.Values["id"].ToString();
            int Ajdi = Int32.Parse(x);
            if (farme == null || farme.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            ViewData["Zivotinje"] = db.zivotinje.Where(f => f.id_farme == Ajdi).ToList();
            return View(farme);
        }

        // GET: farme/Create
        public ActionResult Create()
        {
            
            int userId = Int32.Parse(User.Identity.Name);
            ViewBag.id_parcele = new SelectList(db.parcele.Where(p => p.id_korisnika == userId), "id", "naziv");

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

        // POST: farme/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_parcele,naziv")] farme farme)
        {
            var id = Url.RequestContext.RouteData.Values["id"];

            if (id != null)
            {
                string x = Url.RequestContext.RouteData.Values["id"].ToString();
                farme.id_parcele = Int32.Parse(x);
            }
            if (ModelState.IsValid)
            {
                db.farme.Add(farme);
                db.SaveChanges();
                if (id != null)
                {
                    return RedirectToAction("Details", "parcele", new { id = Int32.Parse(id.ToString()) });
                }
                else
                {
                    return RedirectToAction("Index", "parcele");
                }
            }
            var userId = Int32.Parse(User.Identity.Name);
            ViewBag.id_parcele = new SelectList(db.parcele.Where(p => p.id_korisnika == userId), "id", "naziv");
            return View();
        }

        // GET: farme/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farme farme = db.farme.Find(id);
            var userId = Int32.Parse(User.Identity.Name);
            if (farme == null || farme.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            ViewBag.id_parcele = new SelectList(db.parcele.Where(p => p.id_korisnika == userId), "id", "naziv");
            return View(farme);
        }

        // POST: farme/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_parcele,naziv")] farme farme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "parcele", new { id = farme.id_parcele});
            }
            var userId = Int32.Parse(User.Identity.Name);
            ViewBag.id_parcele = new SelectList(db.parcele.Where(p => p.id_korisnika == userId), "id", "naziv");
            return View(farme);
        }

        // GET: farme/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farme farme = db.farme.Find(id);
            var userId = Int32.Parse(User.Identity.Name);
            if (farme == null || farme.parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            return View(farme);
        }

        // POST: farme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            farme farme = db.farme.Find(id);
            var id_parcele = farme.id_parcele;
            try {
                db.farme.Remove(farme);
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
