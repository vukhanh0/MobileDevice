using MobileDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileDevice.DAO
{
    public class LoginDAO
    {
        private MobilePhoneDB db = new MobilePhoneDB();
        public int Login(string Login_Username, string Login_Password)
        {
            var result = db.Accounts.SingleOrDefault(x => x.UserName == Login_Username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Password == Login_Password)
                    return 2;
                else
                    return -2;
            }
        }
    }
}