using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SakalliTicaret.UI.WEB.App_Start;

namespace SakalliTicaret.UI.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            ModelMetadataProviders.Current = new CachedDataAnnotationsModelMetadataProvider();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
