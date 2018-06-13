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
    public class zadrugeController : Controller
    {
        private Entities db = new Entities();

        // GET: zadruge
        public ActionResult Index()
        {
            int idUsera = Int32.Parse(User.Identity.Name);

            ViewData["Korisnik"] = db.gospodarstva.Find(idUsera);

            return View(db.zadruge.ToList());
        }

        // GET: zadruge/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zadruge zadruge = db.zadruge.Find(id);
            if (zadruge == null)
            {
                return HttpNotFound();
            }
            ViewData["Clanovi"] = db.gospodarstva.Where(g => g.zadruge.id == id).ToList();
            return View(zadruge);
        }

        // GET: zadruge/Join/id
        public ActionResult Join(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zadruge zadruge = db.zadruge.Find(id);
            if (zadruge == null)
            {
                return HttpNotFound();
            }
            return View(zadruge);
        }

        // POST: zadruge/Join
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join()
        {
            int idUsera = Int32.Parse(User.Identity.Name);
            gospodarstva gospodarstvo = db.gospodarstva.Find(idUsera);
            gospodarstvo.id_zadruge = Int32.Parse(Request["id_zadruge"]);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: zadruge/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: zadruge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ime")] zadruge zadruge)
        {
            if (ModelState.IsValid)
            {
                db.zadruge.Add(zadruge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zadruge);
        }

        // GET: zadruge/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zadruge zadruge = db.zadruge.Find(id);
            if (zadruge == null)
            {
                return HttpNotFound();
            }
            return View(zadruge);
        }

        // POST: zadruge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ime")] zadruge zadruge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zadruge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zadruge);
        }

        // GET: zadruge/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zadruge zadruge = db.zadruge.Find(id);
            if (zadruge == null)
            {
                return HttpNotFound();
            }
            return View(zadruge);
        }

        // POST: zadruge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            zadruge zadruge = db.zadruge.Find(id);
            db.zadruge.Remove(zadruge);
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
