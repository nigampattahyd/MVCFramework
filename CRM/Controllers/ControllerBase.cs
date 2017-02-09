using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRM.Controllers
{
    public class ControllerBase : Controller
    {


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // If session exists
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session.IsNewSession || Session["ssnDynamicMenus"] == null || Session["Notifications"]==null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary   
                                                      {  
                                                         { "action", "Index" },  
                                                         { "controller", "Login" },  
                                                         { "returnUrl", filterContext.HttpContext.Request.RawUrl}  
                                                      });  //RedirectToAction("Login", "Home");


            }
            //otherwise continue with action
            base.OnActionExecuting(filterContext);

        }

        //
        // GET: /ControllerBase/
        //public ActionResult Index()
        //{
        //    return View();
        //}
	}
}