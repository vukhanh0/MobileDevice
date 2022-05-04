using MobileDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileDevice.Controllers
{
    public class CategoryController : Controller
    {
        private MobilePhoneDB db = new MobilePhoneDB();
        // GET: Category
        [ChildActionOnly]
        public ActionResult _Danhmuc()
        {
            var listCategory = db.Categories.Where(c => c.Status == true);
            return PartialView(listCategory);
        }
    }
}