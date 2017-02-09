using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Interface;
using CRMHub.EntityFramework;

namespace CRM.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IClientRepository _iclientrepository;
        private readonly IUserMasterRepository _iUserMasterRepository;
        //
        // GET: /Home/
        public HomeController(IClientRepository iclientrepository, IUserMasterRepository iUserMasterRepository)
        {
            _iclientrepository = iclientrepository;
            _iUserMasterRepository = iUserMasterRepository;
           
        }
        public ActionResult Home()
        {
            try
            {

                List<Client> lstclients = _iUserMasterRepository.Getallclientmaster(GlobalVariables.ConnectionString,GlobalVariables.ClientName);

                return View(lstclients);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public ActionResult Index()
        {
            try
            {

                List<Client> lstclients = _iUserMasterRepository.Getallclientmaster(GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                return View(lstclients);
            }
            catch (Exception)
            {
                throw;
            }
        }
	}
}