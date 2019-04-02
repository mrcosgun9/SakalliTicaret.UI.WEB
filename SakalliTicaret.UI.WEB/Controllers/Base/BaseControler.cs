using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Controllers.Base
{
    public class BaseControler : Controller
    {
        public SakalliTicaretDb db = new SakalliTicaretDb();
        public bool IsLogin { get; private set; }
        public int LoginUserId { get; private set; }
        public User LoginUser { get; private set; }
        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.Session["LoginUserId"] != null)
            {
                IsLogin = true;
                LoginUserId = (int)requestContext.HttpContext.Session["LoginUserId"];
                LoginUser = (User)requestContext.HttpContext.Session["LoginUser"];
            }
            base.Initialize(requestContext);
        }
    }
}