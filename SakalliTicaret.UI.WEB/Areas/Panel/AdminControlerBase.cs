using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Areas.Panel
{
    public class AdminControlerBase : Controller
    {
        //public bool IsLogin { get; private set; }
        //public int AdminLoginUserId { get; private set; }
        //public User AdminLoginUser { get; private set; }
        //protected override void Initialize(RequestContext requestContext)
        //{
        //    var Islogin = false;
        //    if (requestContext.HttpContext.Session["AdminLoginUser"] ==null)
        //    {

        //        requestContext.HttpContext.Response.Redirect("/Panel/Login");
        //    }
        //    else
        //    {
        //        IsLogin = true;
        //        AdminLoginUserId = (int)requestContext.HttpContext.Session["AdminLoginUser"];
        //        AdminLoginUser = (User)requestContext.HttpContext.Session["AdminLoginUser"];
        //        base.Initialize(requestContext);

        //    }
        //}
        public bool IsLogin { get; private set; }
        public int AdminLoginUserId { get; private set; }
        public User AdminLoginUser { get; private set; }

        public SakalliTicaretDb db = new SakalliTicaretDb();
        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.Session["AdminLoginUserId"] != null)
            {
                IsLogin = true;
                AdminLoginUserId = (int)requestContext.HttpContext.Session["AdminLoginUserId"];
                AdminLoginUser = (User)requestContext.HttpContext.Session["AdminLoginUser"];
                base.Initialize(requestContext);
            }
            else
            {

                requestContext.HttpContext.Response.Redirect("/Panel/Login");
            }

        }

    }
}