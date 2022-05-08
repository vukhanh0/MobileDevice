using MobileDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileDevice.Controllers
{
    public class HomeController : Controller
    {
        private MobilePhoneDB db = new MobilePhoneDB();
        // GET: Home
        public ActionResult Index()
        {
            var pro = db.Products.Select(n => n).OrderByDescending(p=>p.ID_Product).Take(6);
            return View(pro);
        }

        public ActionResult Introduce()
        {
            return View();
        }

    }
}