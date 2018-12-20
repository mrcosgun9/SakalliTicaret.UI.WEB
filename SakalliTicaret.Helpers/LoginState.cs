using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SakalliTicaret.UI.Helpers
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
            if (admin)
            {
                var User = db.Users.Where(x => x.Email == email.Trim() && x.Password.Trim() == password && x.IsActive == true && x.IsAdmin == admin).FirstOrDefault();
                if (User != null)
                {
                    HttpContext.Current.Session["AdminLoginUser"] = User;
                    return true;
                }
            }
            return false;

        }
    }
}