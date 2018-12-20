using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SakalliTicaret.Core.Model;

namespace SakalliTicaret.UI.WEB
{
    public class LoginState
    {
        private SakalliTicaretDb db;
        public LoginState()
        {
            db = new SakalliTicaretDb();
        }
        public bool IsLoginSucces(string email, string password, bool admin)
        {

            var User = db.Users.Where(x => x.Email == email && x.Password == password && x.IsActive == true && x.IsAdmin == admin).FirstOrDefault();
            if (User != null)
            {
                if (admin)
                {

                    HttpContext.Current.Session["AdminLoginUser"] = User;
                }
                else
                {
                    HttpContext.Current.Session["LoginUserId"] = User.ID;
                    HttpContext.Current.Session["LoginUser"] = User;
                }

                return true;
            }

            return false;

        }
    }
}