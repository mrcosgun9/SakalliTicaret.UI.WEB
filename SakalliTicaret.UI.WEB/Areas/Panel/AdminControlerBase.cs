using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SakalliTicaret.UI.WEB.Areas.Panel
{
    public class AdminControlerBase:Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            var Islogin = false;
            if (requestContext.HttpContext.Session["AdminLoginUser"]==null)
            {
                requestContext.HttpContext.Response.Redirect("/Panel/Login");
            }
            else
            {
                base.Initialize(requestContext);

            }
        }
    }
}