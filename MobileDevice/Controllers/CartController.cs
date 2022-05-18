
using MobileDevice.DTO;
using MobileDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileDevice.Controllers
{
    public class CartController : Controller
    {
        private MobilePhoneDB db = new MobilePhoneDB();
        // GET: Cart
        public List<CartDTO> Laygiohang()
        {
            List<CartDTO> lstGiohang = Session["Giohang"] as List<CartDTO>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<CartDTO>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGiohang(int IDsanpham, string strURL)
        {
            List<CartDTO> lstGiohang = Laygiohang();
            CartDTO sanpham = lstGiohang.Find(n => n.iIDsanpham == IDsanpham);
            if (sanpham == null)
            {
                sanpham = new CartDTO(IDsanpham);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<CartDTO> lstGiohang = Session["Giohang"] as List<CartDTO>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);

            }
            return iTongSoLuong;
        }

        private double Tongtien()
        {
            double iTongtien = 0;
            List<CartDTO> lstGiohang = Session["Giohang"] as List<CartDTO>;
            if (lstGiohang != null)
            {
                iTongtien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongtien;
        }
        public ActionResult XoaGiohang(int iMaSp)
        {
            List<CartDTO> lstGiohang = Laygiohang();
            CartDTO sanpham = lstGiohang.SingleOrDefault(n => n.iIDsanpham == iMaSp);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iIDsanpham == iMaSp);
                return RedirectToAction("Cart");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("CartNull");
            }
            return RedirectToAction("Cart");
        }
        public ActionResult CapnhatGiohangTang(int iMaSp, FormCollection f)
        {
            List<CartDTO> lstGiohang = Laygiohang();
            CartDTO sanpham = lstGiohang.SingleOrDefault(n => n.iIDsanpham == iMaSp);
            if (sanpham != null)
            {
                sanpham.iSoluong++;
            }
            return RedirectToAction("Cart");
        }
        public ActionResult CapnhatGiohangGiam(int iMaSp, FormCollection f)
        {
            List<CartDTO> lstGiohang = Laygiohang();
            CartDTO sanpham = lstGiohang.SingleOrDefault(n => n.iIDsanpham == iMaSp);
            if (sanpham != null)
            {
                sanpham.iSoluong--;
            }
            return RedirectToAction("Cart");
        }
        // GET: Giohang
        public ActionResult XoaTatcaGiohang()
        {
            List<CartDTO> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Cart()
        {
            List<CartDTO> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("CartNull");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            Session["tongsoluong"] = TongSoLuong();
            ViewBag.Tongtien = Tongtien();
            return View(lstGiohang);
        }
        [HttpGet]
        public ActionResult Checkout()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Login");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<CartDTO> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = Tongtien();
            return View(lstGiohang);
        }
        [HttpPost]
        public ActionResult Checkout(FormCollection collection)
        {
            var Checkout_Address = collection["Checkout_Address"];
            var Checkout_Phone = collection["Checkout_Phone"];
            Bill ddh = new Bill();
            Account kh = (Account)Session["Taikhoan"];
            List<CartDTO> gh = Laygiohang();
            ddh.ID_Account = kh.ID_Account;
            ddh.ReceiverName = kh.FullName;
            ddh.ReceiverAddress = !String.IsNullOrEmpty(kh.Address) ? kh.Address : Checkout_Address;
            ddh.ReceiverEmail = kh.Email;
            ddh.ReceiverPhone = !String.IsNullOrEmpty(kh.Phone) ? kh.Phone : Checkout_Phone;
            ddh.Status = true;
            ddh.PayType = "Thanh toán khi nhận hàng";
            ddh.Note = collection["Note"];
            ddh.CreatedDate = DateTime.Now;
            //var Ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.Status = false;
            db.Bills.Add(ddh);
            foreach (var item in gh)
            {
                BillDetail bill_detail = new BillDetail();
                bill_detail.ID_Bill = ddh.ID_Bill;
                bill_detail.ID_Product = item.iIDsanpham;
                bill_detail.Amount = item.iSoluong;
                //bill_detail.CurrentlyPrice =Convert.ToDecimal(item.tTongtien);
                bill_detail.CurrentlyPrice = Convert.ToDecimal(item.iSoluong * item.dDongia);
                db.BillDetails.Add(bill_detail);
            }


            db.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Cart");
        }

        public ActionResult Xacnhandonhang()
        {
            return View();
        }

        public ActionResult CartNull()
        {
            return View();
        }
    }
}