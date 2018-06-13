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
    public class gospodarstvaController : Controller
    {
        private Entities db = new Entities();

        // GET: gospodarstva
        public ActionResult Index()
        {
            /*var gospodarstva = db.gospodarstva.Include(g => g.zadruge);
            return View(gospodarstva.ToList());*/
            return RedirectToAction("Index", "Home");
        }

        // GET: gospodarstva/Details/5
        public ActionResult Details(int? id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gospodarstva gospodarstva = db.gospodarstva.Find(id);
            if (gospodarstva == null)
            {
                return HttpNotFound();
            }
            return View(gospodarstva);*/
            return RedirectToAction("Index", "Home");
        }

        // GET: gospodarstva/Create
        public ActionResult Create()
        {
            /*ViewBag.id_zadruge = new SelectList(db.zadruge, "id", "ime");
            return View();*/
            return RedirectToAction("Index", "Home");
        }

        // POST: gospodarstva/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ime,prezime,kontakt,id_zadruge,email,lozinka")] gospodarstva gospodarstva)
        {
            /* if (ModelState.IsValid)
             {
                 db.gospodarstva.Add(gospodarstva);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }

             ViewBag.id_zadruge = new SelectList(db.zadruge, "id", "ime", gospodarstva.id_zadruge);
             return View(gospodarstva);*/
            return RedirectToAction("Index", "Home");
        }

        // GET: gospodarstva/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gospodarstva gospodarstva = db.gospodarstva.Find(id);
            var userId = Int32.Parse(User.Identity.Name);
            if (gospodarstva == null || gospodarstva.id != userId)
            {
                return HttpNotFound();
            }
            ViewBag.id_zadruge = new SelectList(db.zadruge, "id", "ime", gospodarstva.id_zadruge);
            return View(gospodarstva);
        }

        // POST: gospodarstva/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ime,lozinka,id_zadruge,prezime,kontakt,email")] gospodarstva gospodarstva)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gospodarstva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(gospodarstva);
        }

        // GET: gospodarstva/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gospodarstva gospodarstva = db.gospodarstva.Find(id);
            var userId = Int32.Parse(User.Identity.Name);
            if (gospodarstva == null || gospodarstva.id != userId)
            {
                return HttpNotFound();
            }
            return View(gospodarstva);
        }

        // POST: gospodarstva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gospodarstva gospodarstva = db.gospodarstva.Find(id);
            try
            {
                db.gospodarstva.Remove(gospodarstva);
                db.SaveChanges();
            }
            catch
            {
                return View("Error");
            }
            return RedirectToAction("Index", "Home");
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
