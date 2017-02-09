using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CRM
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            ErrorExceptionClass objError = new ErrorExceptionClass();
            string controllerName = Request.RequestContext.
                                RouteData.Values["controller"].ToString();
            string actionName = Request.RequestContext.
                                RouteData.Values["action"].ToString();
            var pageName = controllerName + "/" + actionName;


            Exception ex = Server.GetLastError().GetBaseException();

            objError.catchException(ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.ToString(), pageName);

        }

        //protected void Application_BeginRequest(Object sender, EventArgs e)
        //{
        //    CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        //    newCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
        //    newCulture.DateTimeFormat.DateSeparator = "-";
        //    Thread.CurrentThread.CurrentCulture = newCulture;
        //}
    }
}
