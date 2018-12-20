using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SakalliTicaret.UI.WEB.App_Class
{
    public class ControlLogin : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["LoginUser"].ToString()))
                {
                    base.OnActionExecuting(filterContext);

                }
                else
                {
                    HttpContext.Current.Response.Redirect("/Kullanici/Giris");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect("/Kullanici/Giris");
            }
        }
    }
}