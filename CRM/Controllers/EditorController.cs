using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class EditorController : ControllerBase
    {
        //
        // GET: /Editor/
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            return View();
        }
	}
}