using MobileDevice.DAO;
using MobileDevice.DTO;
using MobileDevice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        #region Đổi thông tin cá nhân
        [HttpGet]
        public ActionResult ChangeInfo()
        {
            var username = Session["username"];
            Account acc = db.Accounts.Where(p => p.UserName.Equals((string)username)).FirstOrDefault();
            //if(acc.Phone != null)
            //{
            //    acc.Phone = acc.Phone.Trim();
            //}
            return View(acc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeInfo([Bind(Include = "UserName,Password,FullName,Phone,Address,Email,Avatar")] Account account)
        {
            var userName = Session["username"];
            Account acc = db.Accounts.Where(u => u.UserName.Equals((string)userName)).FirstOrDefault();
            var email1 = account.Email.Trim();
            var email = db.Accounts.Where(u => u.Email.Equals(email1) && !u.UserName.Equals((string)userName)).ToList();
            if (email.Count() > 0)
            {
                ViewBag.ErrorUpdate = "Email đã tồn tại !";
            }
            else
            {
                //if (ModelState.IsValid)
                //{
                    var f = Request.Files["Image"];
                    if (f != null && f.ContentLength > 0)
                    {
                        //Use Namespace called : System.IO
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        //Lấy tên file upload
                        string UploadPath = Server.MapPath("~/wwwroot/Image/" + FileName);
                        //Copy Và lưu file vào server. 
                        f.SaveAs(UploadPath);
                        //Lưu tên file vào db
                        account.Avatar = FileName;
                        acc.Avatar = account.Avatar;
                        Session["Avatar"] = account.Avatar;
                    }
                    //acc.Password = account.Password;
                    acc.Email = account.Email;
                    acc.FullName = account.FullName;
                    acc.Address = account.Address;
                    acc.Phone = account.Phone;
                    db.SaveChanges();
                    ViewBag.UpdateOk = "Cập nhật thành công !";
                    Session["fullname"] = account.FullName;
                    return View(acc);
                //}
            }
            return View(acc);
        }

        public ActionResult ChangePassword(FormCollection col)
        {
            var OldPass = col["OldPassword"];
            var NewPass = col["NewPassword"];
            var userName = Session["username"];
            Account acc = db.Accounts.Where(u => u.UserName.Equals((string)userName)).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (OldPass == acc.Password)
                {
                    acc.Password = NewPass;
                    db.Entry(acc).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Success = "Cập nhật thành công!";
                    return RedirectToAction("ChangeInfo", "Login");
                }
            }
            return RedirectToAction("ChangeInfo", "Login");
        }
        #endregion
    }
}