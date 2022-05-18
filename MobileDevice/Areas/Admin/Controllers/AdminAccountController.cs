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
    public class AdminAccountController : Controller
    {
        private MobilePhoneDB db = new MobilePhoneDB();

        // GET: Admin/AdminAccount
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.Role).Where(p => p.ID_Role == 2);
            return View(accounts.ToList());
        }
        public ActionResult IndexCustomer()
        {
            var accounts = db.Accounts.Include(a => a.Role).Where(p => p.ID_Role == 1);
            return View(accounts.ToList());
        }

        public JsonResult DeleteAdmin(int Id_Account)
        {
            bool result = false;
            Account ca = db.Accounts.Where(p => p.ID_Account == Id_Account).SingleOrDefault();
            if (ca != null)
            {
                db.Accounts.Remove(ca);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/AdminAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Admin/AdminAccount/Create
        public ActionResult Create()
        {
            ViewBag.ID_Role = new SelectList(db.Roles, "ID_Role", "Name");
            return View();
        }

        // POST: Admin/AdminAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Account,ID_Role,UserName,Password,FullName,Phone,Address,Email,Status,Avatar,IsManager,ResetPasswordCode,ActivationCode")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.Avatar = " ";
                var f = Request.Files["Image2"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/Image/" + FileName);
                    f.SaveAs(UploadPath);
                    account.Avatar = FileName;
                }
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Role = new SelectList(db.Roles, "ID_Role", "Name", account.ID_Role);
            return View(account);
        }

        // GET: Admin/AdminAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Role = new SelectList(db.Roles, "ID_Role", "Name", account.ID_Role);
            return View(account);
        }

        // POST: Admin/AdminAccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Account,ID_Role,UserName,Password,FullName,Phone,Address,Email,Status,Avatar,IsManager,ResetPasswordCode,ActivationCode")] Account account)
        {
            if (ModelState.IsValid)
            {
            account.Avatar = " ";
            var f = Request.Files["Image"];
            if (f != null && f.ContentLength > 0)
            {
                string FileName = System.IO.Path.GetFileName(f.FileName);
                string UploadPath = Server.MapPath("~/wwwroot/Image/" + FileName);
                f.SaveAs(UploadPath);
                account.Avatar = FileName;
            }
            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            ViewBag.ID_Role = new SelectList(db.Roles, "ID_Role", "Name", account.ID_Role);
            return View(account);
        }

        // GET: Admin/AdminAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin/AdminAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
