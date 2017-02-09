using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Interface;
using CRMHub.Repository;
using CRMHub.EntityFramework;

namespace CRM.Controllers
{
    public class OfficelevelAccessController : ControllerBase
    {
        private readonly IOfficeLevelAccessRepository _iofficeaccessrepository;
        private readonly IRoleRepository _iroleRepository;
        private readonly ICommonRepository _icommonpository;
        string loginId = GlobalVariables.UserID;
        string roleId = GlobalVariables.RoleID;
        public OfficelevelAccessController(IOfficeLevelAccessRepository iofficeaccessrepository, IRoleRepository iroleRepository, ICommonRepository icommonpository)
        {
            _iofficeaccessrepository = iofficeaccessrepository;
            _iroleRepository = iroleRepository;
            _icommonpository = icommonpository;
        }      
 
       /// <summary>
       /// This method binds the users based on the role.
       /// For this we have to bind roles to t
       /// </summary>
       /// <returns></returns>      
        public ActionResult Index()
        {
            try
            {
                OfficeLevelAccessModel OlaModel = new OfficeLevelAccessModel();
                role selectRole = new role();
                selectRole.RoleId = -1;
                selectRole.RoleName = "Select Level";
                OlaModel.lstRoles = new List<role>();
                OlaModel.lstRoles.Add(selectRole);
                OlaModel.lstRoles.AddRange(_iroleRepository.GetLevels(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                OlaModel.users = new user();
                //OlaModel.users.RoleId = 18;
                OlaModel.users.RoleId = 1;
                return View(OlaModel);
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method Gets the users data based on the name,email and level of the users. 
        /// </summary>
        /// <param name="OlaModel"></param>
        /// <returns></returns>
        public ActionResult getofficelevelacessdata(jQueryDataTableParamModel param)
        {
            try
            {
                string Adminname = Request["AdminName"];
                string AdminEmail = Request["AdminEmail"];
                string Adminlevel = Request["Levels"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                orderByClause = SortFieldName + " " + sSortDir_0;
                List<user> lstUser = _iofficeaccessrepository.GetUser(Adminname, AdminEmail, Adminlevel, loginId, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                  return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = lstUser,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);           
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        /// <summary>
        /// This method Edits the users data based on the userid when we click on Users Fullname.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditUserDetail(int userId)
        {
            try
            {
                OfficeLevelAccessModel OlaModel = new OfficeLevelAccessModel();
                OlaModel.users = _iofficeaccessrepository.EditUserdetailsbyuserId(userId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                OlaModel.users.UserId = userId;
                OlaModel.lstRoles = _iroleRepository.GetLevels(GlobalVariables.ConnectionString,GlobalVariables.ClientName);

                Branch selectbranch = new Branch();
                selectbranch.BranchId = -1;
                selectbranch.BranchName = "Select Office";
                OlaModel.listBranch = new List<Branch>();
                OlaModel.listBranch.Add(selectbranch);
                OlaModel.listBranch.AddRange(_iofficeaccessrepository.GetUserBranch(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                List<usState> listOfState = _icommonpository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                List<SelectListItem> listOfStates = listOfState.Select(state => new SelectListItem
                {
                    Text = state.StateName,
                    Value = state.StateCode
                }).ToList();
                ViewBag.StateList = listOfStates;
                return View(OlaModel);                
            }
            catch (Exception)
            {                
                throw;
            }          
        }

        /// <summary>
        /// This method Updates the Edited Userdata and it redirects to same page.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUserDetailByUserId(user users, FormCollection fc)
        {
            try
            {
                int result = _iofficeaccessrepository.UpdateUserdetailsbyuserId(users, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return RedirectToAction("EditUserDetail", new { userId = users.UserId });
            }
            catch (Exception)
            {                
                throw;
            }         
        }

       /// <summary>
       /// This method deletes the particular user based on the userid.
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteUserByuserid(int userId)
        {
            try
            {
                int deleteresult = _iofficeaccessrepository.DeleteUserDetailsByuserid(userId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(deleteresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                
                throw;
            }            
        }

        /// <summary>
        /// This method gets the details of particular branch details and return as a json result to bind the details 
        /// according to the change of office dropdown.
        /// </summary>
        /// <param name="branchid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetBranchDetailsById(int branchid)
        {
            try
            {
                Branch objBranch = _iofficeaccessrepository.GetBranchDetailsByBranchName(branchid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(objBranch, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method checks the entered emailid is exists or not.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckEmailExistsorNot(string email)
        {
            try
            {
                bool objemail = _iofficeaccessrepository.CheckEmailExistsareNot(email, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(objemail, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
	}
}