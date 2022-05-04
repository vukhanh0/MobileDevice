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
    public class AdminBillController : Controller
    {
        private MobilePhoneDB db = new MobilePhoneDB();

        // GET: Admin/AdminBill
        public ActionResult Index()
        {
            var bills = db.Bills.Include(b => b.Account);
            return View(bills.ToList());
        }

        // GET: Admin/AdminBill/Details/5
        public ActionResult Details(int? id)
        {
            var bill_detail = db.BillDetails.Include(c => c.Bill).Include(c => c.Product).Where(a => a.ID_Bill == id);
            double iTongtien = 0;
            iTongtien = (double)bill_detail.Sum(n => n.CurrentlyPrice);
            ViewBag.Tongtien = iTongtien;
            return View(bill_detail.ToList());
        }

        // GET: Admin/AdminBill/Create
        public ActionResult Create()
        {
            ViewBag.ID_Account = new SelectList(db.Accounts, "ID_Account", "UserName");
            return View();
        }

        // POST: Admin/AdminBill/Create
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

        // GET: Admin/AdminBill/Edit/5
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

        // POST: Admin/AdminBill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Bill,ID_Account,ReceiverName,ReceiverAddress,ReceiverEmail,ReceiverPhone,Note,PayType,Status,CreatedDate,ModifiedDate")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                if (bill.Status == true)
                {
                    var bill_detail = db.BillDetails.Where(p => p.ID_Bill == bill.ID_Bill);
                    foreach (var item in bill_detail)
                    {
                        Product product_Detail = db.Products.Where(p => p.ID_Product == item.ID_Product).FirstOrDefault();
                        product_Detail.Amount -= item.Amount;
                        //product_Detail.viewcount += item.quanity;
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_custommer = new SelectList(db.Accounts, "Id_Account", "FullNamr", bill.ID_Account);
            return View(bill);
        }

        // GET: Admin/AdminBill/Delete/5
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

        // POST: Admin/AdminBill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var bill_detail = db.BillDetails.Where(n => n.ID_Bill == id).FirstOrDefault();
            if (bill_detail != null)
            {
                db.BillDetails.Remove(bill_detail);
                db.SaveChanges();
            }
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
