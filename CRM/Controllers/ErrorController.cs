using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class ErrorController : ControllerBase
    {        
        // GET: /Error/NotFound
        /// <summary>
        /// This method works when we get any 404 error.
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            try
            {
                Response.StatusCode = 404;
                return View();
            }
            catch(Exception)
            {
                throw;
            }
        }

        //Get:/Error/SyntaxError
        /// <summary>
        /// This method works when we hit any 500 error.
        /// </summary>
        /// <returns></returns>
        public ActionResult SyntaxError()
        {
            try
            {
                Response.StatusCode = 500;
                return View();
            }
            catch(Exception)
            {
                throw;
            }
        }
	}
}