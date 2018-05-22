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
        public ActionResult Index()
        {
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
        public ActionResult Login(gospodarstva login)
        {
           
            /*string s1 = "Select email,lozinka from [dbo].[gospodarstva] where email=@Email and lozinka=@Lozinka ";
            SqlCommand sqlcomm = new SqlCommand(s1);
            sqlcomm.Parameters.AddWithValue("@Email", login.Email);
            sqlcomm.Parameters.AddWithValue("@Lozinka", login.Lozinka);
            SqlDataReader sdr = dbCtrl.executeSdr(sqlcomm);
            if (sdr.Read())
            {
                Session["id"] = login.Id.ToString();
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewData["Message"] = "Unijeli ste pogrešne podatke.";
            }*/

            return View();
        }
    }
}