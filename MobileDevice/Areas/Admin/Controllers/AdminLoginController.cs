using MobileDevice.DAO;
using MobileDevice.DTO;
using MobileDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileDevice.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        private MobilePhoneDB db = new MobilePhoneDB();
        // GET: Admin/AdminLogin
        public ActionResult Login(Account account)
        {
            var dao = new LoginDAO();
            var result = dao.Login(account.UserName, account.Password);
            if (result == 2)
            {
                var user = db.Accounts.SingleOrDefault(u => u.UserName == account.UserName);
                var userSession = new AccountDTO();
                userSession.UserName = user.UserName;
                userSession.Password = user.Password;
                Session["Taikhoan"] = user;
                Session["username"] = user.UserName;
                Session["password"] = user.Password;
                Session["fullname"] = user.FullName;
                Session["email"] = user.Email;
                Session.Add(CommonContants.ADMIN_SESSION, userSession);
                return RedirectToAction("Index", "AdminHome");
            }
            else if (result == 0)
            {

                ViewBag.thongbao = "Tài khoản không tồn tại";
            }
            else
            {
                ViewBag.thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View("Login");
        }
        public ActionResult Logout()
        {
            Session[MobileDevice.DAO.CommonContants.ADMIN_SESSION] = null;

            return RedirectToAction("Login", "AdminLogin");
        }
    }
}