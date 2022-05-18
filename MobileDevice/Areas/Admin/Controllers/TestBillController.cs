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
    public class TestBillController : Controller
    {
        private MobilePhoneDB db = new MobilePhoneDB();

        // GET: Admin/TestBill
        public ActionResult Index()
        {
            var bills = db.Bills.Include(b => b.Account);
            return View(bills.ToList());
        }

        // GET: Admin/TestBill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: Admin/TestBill/Create
        public ActionResult Create()
        {
            ViewBag.ID_Account = new SelectList(db.Accounts, "ID_Account", "UserName");
            return View();
        }

        // POST: Admin/TestBill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Bill,ID_Account,ReceiverName,ReceiverAddress,ReceiverEmail,ReceiverPhone,Note,PayType,Status,CreatedDate,ModifiedDate")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Account = new SelectList(db.Accounts, "ID_Account", "UserName", bill.ID_Account);
            return View(bill);
        }

        // GET: Admin/TestBill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Account = new SelectList(db.Accounts, "ID_Account", "UserName", bill.ID_Account);
            return View(bill);
        }

        // POST: Admin/TestBill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Bill,ID_Account,ReceiverName,ReceiverAddress,ReceiverEmail,ReceiverPhone,Note,PayType,Status,CreatedDate,ModifiedDate")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Account = new SelectList(db.Accounts, "ID_Account", "UserName", bill.ID_Account);
            return View(bill);
        }

        // GET: Admin/TestBill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Admin/TestBill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
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
