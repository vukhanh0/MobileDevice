using MobileDevice.DAO;
using MobileDevice.DTO;
using MobileDevice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                ViewBag.LoginSuccess = "Đăng nhạp thành công";
                var user = db.Accounts.SingleOrDefault(u => u.UserName == account.UserName);
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

                ViewBag.LoginError1 = "Tài khoản không tồn tại";
            }
            else
            {
                ViewBag.LoginEror2 = "Tên đăng nhập hoặc mật khẩu không đúng";
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

        [NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode, string emailfor = "VerifyAccount")
        {
            var verifyUrl = "/Login/ResetPassword/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("nhom6aspnet@gmail.com", "Cửa hàng điện thoại Sendo");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "duykhanh@2611";

            string subject = "";
            string body = "";
            if (emailfor == "VerifyAccount")
            {
                subject = "Tạo tài khoản thành công !";

                body = "<br/><br/>we are tell you is" + "Bấm vào link" + "<br/><br/><a href='" + link + "'>" + link + "<a/>";
            }
            else if (emailfor == "Resetpassword")
            {
                subject = "Đặt lại mật khẩu";
                body = "<br/><br/>Chúng tôi nhận được yêu cầu đặt lại mật khẩu. Bấm vào link bên dưới để tạo mật khẩu mới" + "<br/><br/><a href='" + link + "'>Đặt lại mật khẩu<a/>";
            }
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address.Trim(), fromEmailPassword.Trim())
            };
            smtp.EnableSsl = true;
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            })
                smtp.Send(message);
        }

        [HttpPost]
        public ActionResult ForgotPassword(FormCollection col)
        {
            //Xác minh email
            //Gửi liên kết đặt lại mật khẩu
            //Gửi email
            var Email = col["ForgotEmail"];
            var account = db.Accounts.Where(a => a.Email == Email).FirstOrDefault();
            if (account != null)
            {
                //Gửi email đặt lại mật khẩu
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(account.Email, resetCode, "Resetpassword");
                account.ResetPasswordCode = resetCode;
                // avoid comfirm password
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();

                ViewBag.ForgetOk = "Link đặt lại mật khẩu đã gửi đến Email của bạn !";
            }
            else
            {

                ViewBag.ForgetError = "Không tìm thấy email !";
            }
            return View();
        }
        public ActionResult ResetPassword(string id)
        {
            //Xác minh link đặt lại mật khẩu
            //Tìm tài khoản link gửi đến
            //Chuyển tới trang đặt lại mật khẩu
            var user = db.Accounts.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
            if (user != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model, FormCollection col)
        {
            if (ModelState.IsValid)
            {
                var forgotpass = col["ForgotPassword"];
                var confirmpass = col["ConfirmPassword"];
                var user = db.Accounts.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                if (user != null)
                {
                    //user.Password = Crypto.Hash(model.NewPassword);
                    user.Password = forgotpass;
                    user.ResetPasswordCode = "";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();

                    ViewBag.ResetOk = "Cập nhật mật khẩu mới thành công !";
                }
            }
            else
            {
                ViewBag.ResetError = "Mật khẩu không trùng khớp !";
            }
            return RedirectToAction("Login", "Login");
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