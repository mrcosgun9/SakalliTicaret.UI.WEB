
using System;
using System.Web;
using System.Web.Mvc;

namespace SakalliTicaret.UI.WEB.App_Class
{
    public class ControlLoginPerformed : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var LoginUser = HttpContext.Current.Session["LoginUser"];
                if (LoginUser == null)
                {
                    base.OnActionExecuting(filterContext);

                }
                else
                {
                    HttpContext.Current.Response.Redirect("/Anasayfa");
                }
            }
            catch (Exception ex)
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}