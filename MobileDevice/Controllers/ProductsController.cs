using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MobileDevice.Models;
using PagedList;

namespace MobileDevice.Controllers
{
    public class ProductsController : Controller
    {
        private MobilePhoneDB db = new MobilePhoneDB();

        // GET: Products
        public ActionResult Index(string sortOrder, string searchstring, string currentFilter, int? page)
        {
            //các biến sắp xếp
            ViewBag.CurrentSort = sortOrder;//biến lấy yêu cầu sắp xếp hiện tại
            ViewBag.saptheogiatang = sortOrder == "gia" ? "" : "gia";
            ViewBag.saptheogiagiam = sortOrder == "gia_desc" ? "" : "gia_desc";

            ViewBag.SPCaoCap = sortOrder == "caocap" ? "" : "caocap";
            ViewBag.SPTamtrung = sortOrder == "tamtrung" ? "" : "tamtrung";


            ViewBag.iphone = sortOrder == "iphone" ? "" : "iphone";
            ViewBag.samsung = sortOrder == "samsung" ? "" : "samsung";
            ViewBag.oppo = sortOrder == "oppo" ? "" : "oppo";
            ViewBag.huawei = sortOrder == "huawei" ? "" : "huawei";
            ViewBag.sony = sortOrder == "sony" ? "" : "sony";

            //Lấy giá trị bộ lọc dữ liệu hiện tại
            if (searchstring != null)
            {
                page = 1;//trang đầu tiên
            }
            else
            {
                searchstring = currentFilter;
            }
            ViewBag.CurrentFilter = searchstring;

            //lấy danh sách hàng
            var product = db.Products.Select(s => s);
            
            //var a = product.Where(p => p.ID_Category == 1).Count();
            //lọc theo tên hàng
            if (!String.IsNullOrEmpty(searchstring))
            {
                product = product.Where(p => p.Name.Contains(searchstring));
            }

            switch (sortOrder)
            {
                case "gia":
                    product = product.OrderBy(s => s.Price);
                    break;
                case "gia_desc":
                    product = product.OrderByDescending(s => s.Price);
                    break;
               
                case "iphone":
                    product = product.Where(s => s.ID_Category == 1).OrderBy(s => s.ID_Product);
                    break;
                case "samsung":
                    product = product.Where(s => s.ID_Category == 2).OrderBy(s => s.ID_Product);
                    break;
                case "oppo":
                    product = product.Where(s => s.ID_Category == 3).OrderBy(s => s.ID_Product);
                    break;
                case "huawei":
                    product = product.Where(s => s.ID_Category == 4).OrderBy(s => s.ID_Product);
                    break;
                case "sony":
                    product = product.Where(s => s.ID_Category == 5).OrderBy(s => s.ID_Product);
                    break;
                case "caocap":
                    product = product.Where(s => s.Price > 15000000).OrderBy(s => s.ID_Product);
                    break;
                case "tamtrung":
                    product = product.Where(s => s.Price <= 15000000).OrderBy(s => s.ID_Product);
                    break;
                default:
                    product = product.OrderBy(s => s.ID_Product);
                    break;
            }
            int pageSize = 9;
            int PageNumber = (page ?? 1);//nếu page bằng Null thì trả về 1
            return View(product.ToPagedList(PageNumber, pageSize));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
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
