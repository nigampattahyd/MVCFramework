using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class ClientManagerController : Controller
    { 
        private readonly IClientRepository _iclientrepository;
        private readonly IUserMasterRepository _iUserMasterRepository;

        public ClientManagerController(IClientRepository iclientrepository, IUserMasterRepository iUserMasterRepository)
        {
            _iclientrepository = iclientrepository;
            _iUserMasterRepository = iUserMasterRepository;
           
        }
        public ActionResult ClientManagerIndex()
        {
            try
            {
                //List<Client> lstclients = _iUserMasterRepository.Getallclientmaster(GlobalVariables.ConnectionString, GlobalVariables.ClientName).ToList().OrderByDescending(x => x.ClientNumber).ToList();
                List<Client> lstclients = _iUserMasterRepository.Getallclientmaster(GlobalVariables.ConnectionString, GlobalVariables.ClientName).ToList().OrderByDescending(x => x.ClientNumber).ToList();
                return View(lstclients);
            }
            catch (Exception)
            {
                throw;
            }
        }





    }
}