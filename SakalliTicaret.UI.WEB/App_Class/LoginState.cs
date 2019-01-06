using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;

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

                    HttpContext.Current.Session["AdminLoginUserId"] = User.ID;
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

        public bool IsLogin()
        {
            if (HttpContext.Current.Session["LoginUserId"] != null)
            {
                return true;
            }
            return false;
        }

        public User IsLoginUser()
        {
            var loginUser = HttpContext.Current.Session["LoginUser"];
            User user = null;
            if (loginUser != null)
            {
                user = loginUser as User;
            }

            return user;
        }
    }
}