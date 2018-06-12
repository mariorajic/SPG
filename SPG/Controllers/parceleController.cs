using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using SPG;

namespace SPG.Controllers
{
    [Authorize]
    public class parceleController : Controller
    {
        public int DobaviID()
        {
            int IdUsera = Int32.Parse(User.Identity.Name);
            return IdUsera;
        }

        private Entities db = new Entities();
        
        // GET: parcele
        public ActionResult Index()
        {
            var userId = DobaviID();
            var parcele = db.parcele.Include(p => p.gospodarstva).Include(p => p.gradovi)
                .Where(p => p.id_korisnika == userId);
            ViewData["Parcele"] = parcele.ToList();
            ViewData["Farme"] = db.farme.Where(f => f.parcele.id_korisnika == userId).ToList();
            ViewData["Oranice"] = db.oranice.Where(f => f.parcele.id_korisnika == userId).ToList(); ;

            return View();
        }

        // GET: parcele/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parcele parcele = db.parcele.Find(id);
            var userId = DobaviID();
            if (parcele == null || parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            return View(parcele);
        }

        // GET: parcele/Create
        public ActionResult Create()
        {
            ViewBag.id_grada = new SelectList(db.gradovi, "id", "ime");
            return View();
        }

        // POST: parcele/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,koordinate,dimenzije,id_grada,lokacija,naziv")] parcele parcele)
        {

            parcele.id_korisnika = DobaviID();

            if (ModelState.IsValid)
            {
                db.parcele.Add(parcele);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_korisnika = new SelectList(db.gospodarstva, "id", "ime", parcele.id_korisnika);
            ViewBag.id_grada = new SelectList(db.gradovi, "id", "ime", parcele.id_grada);
            return View(parcele);
        }

        // GET: parcele/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parcele parcele = db.parcele.Find(id);
            var userId = DobaviID();
            if (parcele == null || parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            ViewBag.id_korisnika = new SelectList(db.gospodarstva, "id", "ime", parcele.id_korisnika);
            ViewBag.id_grada = new SelectList(db.gradovi, "id", "ime", parcele.id_grada);
            return View(parcele);
        }

        // POST: parcele/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,koordinate,dimenzije,id_grada,lokacija,naziv")] parcele parcele)
        {
            parcele.id_korisnika = DobaviID();

            if (ModelState.IsValid)
            {
                db.Entry(parcele).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_korisnika = new SelectList(db.gospodarstva, "id", "ime", parcele.id_korisnika);
            ViewBag.id_grada = new SelectList(db.gradovi, "id", "ime", parcele.id_grada);
            return View(parcele);
        }

        // GET: parcele/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parcele parcele = db.parcele.Find(id);
            var userId = DobaviID();
            if (parcele == null || parcele.id_korisnika != userId)
            {
                return HttpNotFound();
            }
            return View(parcele);
        }

        // POST: parcele/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            parcele parcele = db.parcele.Find(id);
            try
            {
                db.parcele.Remove(parcele);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return View("Error");
            }


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
