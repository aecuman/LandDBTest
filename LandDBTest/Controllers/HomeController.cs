using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LandDBTest.Controllers
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

        public ActionResult PriceIndex()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult PriceStat()
        {
            return View();
        }
       
    }
}