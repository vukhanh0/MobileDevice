using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MobileDevice.Models;

namespace MobileDevice.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        private MobilePhoneDB db = new MobilePhoneDB();

        // GET: Admin/AdminProduct
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Color);
            return View(products.ToList());
        }

        public JsonResult DeleteProduct(int Id_Product)
        {
            bool result = false;
            Product ca = db.Products.Where(p => p.ID_Product == Id_Product).SingleOrDefault();
            if (ca != null)
            {
                db.Products.Remove(ca);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/AdminProduct/Details/5
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

        // GET: Admin/AdminProduct/Create
        public ActionResult Create()
        {
            ViewBag.ID_Category = new SelectList(db.Categories, "ID_Category", "Name");
            ViewBag.ID_Color = new SelectList(db.Colors, "ID_Color", "Name");
            return View();
        }

        // POST: Admin/AdminProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Product,ID_Category,ID_Color,Name,Screen,SystemOperation,RearCamera,FrontCamera,Ram,Rom,Sim,Battery,Price,Image,ShortDescription,Detail,Amount,Origin,CreatedDate,Status,CreatedBy,ModifiedDate,ModifiedBy")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Image = " ";
                var f = Request.Files["Image"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/Image/" + FileName);
                    f.SaveAs(UploadPath);
                    product.Image = FileName;
                }
                product.CreatedDate = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Category = new SelectList(db.Categories, "ID_Category", "Name", product.ID_Category);
            ViewBag.ID_Color = new SelectList(db.Colors, "ID_Color", "Name", product.ID_Color);
            return View(product);
        }

        // GET: Admin/AdminProduct/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ID_Category = new SelectList(db.Categories, "ID_Category", "Name", product.ID_Category);
            ViewBag.ID_Color = new SelectList(db.Colors, "ID_Color", "Name", product.ID_Color);
            return View(product);
        }

        // POST: Admin/AdminProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Product,ID_Category,ID_Color,Name,Screen,SystemOperation,RearCamera,FrontCamera,Ram,Rom,Sim,Battery,Price,Image,ShortDescription,Detail,Amount,Origin,CreatedDate,Status,CreatedBy,ModifiedDate,ModifiedBy")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Image = " ";
                var f = Request.Files["Image"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/Image/" + FileName);
                    f.SaveAs(UploadPath);
                    product.Image = FileName;
                }
                product.ModifiedDate = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Category = new SelectList(db.Categories, "ID_Category", "Name", product.ID_Category);
            ViewBag.ID_Color = new SelectList(db.Colors, "ID_Color", "Name", product.ID_Color);
            return View(product);
        }

        // GET: Admin/AdminProduct/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/AdminProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
