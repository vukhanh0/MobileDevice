using MobileDevice.DAO;
using MobileDevice.DTO;
using MobileDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileDevice.Controllers
{
    public class LoginController : Controller
    {
        private MobilePhoneDB db = new MobilePhoneDB();

        #region Đăng nhập
        // GET: Login
        public ActionResult Login(Account account)
        {

            var dao = new LoginDAO();
            var result = dao.Login(account.UserName, account.Password);
            if (result == 2)
            {
                var user = db.Accounts.SingleOrDefault(u => u.UserName== account.UserName);
                var userSession = new AccountDTO();
                userSession.UserName = user.UserName;
                userSession.Password = user.Password;
                Session["Taikhoan"] = user;
                Session["username"] = user.UserName;
                Session["password"] = user.Password;
                Session["fullname"] = user.FullName;
                Session["email"] = user.Email;
                Session.Add(CommonContants.USER_SESSION, userSession);
                return RedirectToAction("Index", "Home");
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
        #endregion

        #region Đăng ký
        [HttpGet]
        public ActionResult Signin()
        {
            return View(new AccountDTO());
        }

        [HttpPost]
        public ActionResult Signin(FormCollection col, AccountDTO account)
        {
            /*Account userName = db.Accounts.Where(u => u.UserName.Equals(account.UserName.Trim())).SingleOrDefault();
            var email = db.Accounts.Where(u => u.Email.Equals(account.Email.Trim())).ToList();*/
            int countUsername = db.Accounts.Count(a => a.UserName.Equals(account.UserName));
            int countEmail = db.Accounts.Count(a => a.Email.Equals(account.Email));
            var Username = col["Signin_Username"];
            var Email = col["Signin_Email"];
            var Fullname = col["Signin_Fullname"];
            var Password = col["Signin_Password"];
            var Password2 = col["Signin_Password2"];
            //if (ModelState.IsValid)
            //{
                if (countUsername > 0)
                {
                    ViewBag.ErrorRegis = "Tên đăng nhập đã tồn tại !";
                }
                else if (countEmail > 0)
                {
                    ViewBag.ErrorRegis = "Email đã tồn tại !";
                }
                else
                {
                    Account data = new Account();
                    data.UserName = Username;
                    data.Password = Password;
                    data.Email = Email;
                    data.FullName = Fullname;
                    data.ID_Role = 1;
                    data.Status = true;
                    db.Accounts.Add(data);
                    db.SaveChanges();
                    TempData["RegisterOk"] = "Đăng kí tài khoản thành công!";//Chuyển sang view login
                    return RedirectToAction("Login", "Login");
                }
            //}
            return View(account);
        }
        #endregion

        #region Đăng xuất
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }
        #endregion

        #region Quên mật khẩu
        public ActionResult ForgotPassword()
        {
            return View();
        }
        #endregion

    }
}