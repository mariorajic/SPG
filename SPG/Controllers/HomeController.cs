using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPG;

namespace SPG.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            if (Session["id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login()
        {
            if (Session["id"] != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Register()
        {
            ViewBag.NoNavbar = true;
            if (Session["id"] != null)
            {
                return View("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Register(gospodarstva gospodarstva)
        {
            ViewBag.NoNavbar = true;

            if (ModelState.IsValid)
            {
                if (!db.gospodarstva.Any(g => g.email == gospodarstva.email))
                {
                    db.gospodarstva.Add(gospodarstva);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = "Uspješno ste se registrirali. Molimo prijavite se.";
                    return View("Login");
                }
                else
                {
                    ViewBag.Message = "Unijeli ste postojeću email adresu.";
                    return View();
                }
            } return View();
        }

        public ActionResult About()
        {
            ViewBag.NoNavbar = true;
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Login(gospodarstva login)
        {
            ViewBag.NoNavbar = true;

            var usr = db.gospodarstva.Where(g => g.email == login.email && g.lozinka == login.lozinka).FirstOrDefault();
               if (usr == null)
                {
                    ViewBag.Message = "Unijeli ste pogrešne podatke!";
                    return View();
                }
                else
                {
                Session["id"] = usr.id;
                ViewBag.Message = "Uspješno ste se prijavili.";
                return RedirectToAction("Index", "Home");

                }
            
        }
    }
}