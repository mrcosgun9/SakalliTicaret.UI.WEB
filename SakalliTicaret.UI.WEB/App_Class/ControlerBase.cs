using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SakalliTicaret.UI.WEB.App_Class
{
    public class ControlerBase:Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            var Islogin = false;
            if (requestContext.HttpContext.Session["LoginUser"] == null)
            {
                requestContext.HttpContext.Response.Redirect("/Kullanici/Giris");
            }
            else
            {
                base.Initialize(requestContext);

            }
        }
    }
}