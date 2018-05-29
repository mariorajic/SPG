using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using SPG;

namespace SPG.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        [Authorize]
        public ActionResult Index()
        {
                return View();
        }


        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
            }
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(gospodarstva gospodarstva)
        {

            if (ModelState.IsValid)
            {
                if (!db.gospodarstva.Any(g => g.email == gospodarstva.email))
                {
                    gospodarstva.lozinka = Crypto.SHA256(gospodarstva.lozinka);
                    db.gospodarstva.Add(gospodarstva);
                    db.SaveChanges();
                    ModelState.Clear();
                    TempData["shortMessage"] = "Uspješno ste se registrirali. Molimo prijavite se.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Message = "Unijeli ste postojeću email adresu.";
                    return View();
                }
            }
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(gospodarstva login, string returnUrl)
        {
            var hashedLozinka = Crypto.SHA256(login.lozinka);
            var usr = db.gospodarstva.Where(g => g.email == login.email && g.lozinka == hashedLozinka).FirstOrDefault();
            if (usr != null)
            {
                FormsAuthentication.SetAuthCookie(usr.email, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Netočan email ili lozinka.");
                return View();
            }
        }

    }
}
