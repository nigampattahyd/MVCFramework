using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.Repository;
using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class RolesController : Controller
    {

        private readonly IRoleRepository _irolerepository;
        public RolesController()
        {
            _irolerepository = new RoleRepository();
        }
        // GET: Roles
        public ActionResult Index()
        {
            ViewBag.Updated = TempData["Updated"];
            ViewBag.Created = TempData["Created"];
            return View();
        }
        public ActionResult getRoles(jQueryDataTableParamModel param)
        {
            ViewBag.Created = TempData["Created"];
            try{
            string loginid = GlobalVariables.UserID;
            string Keyword_RoleName = Request["Keyword_RoleName"].TrimStart();
            string iSortCol_0 = Request["iSortCol_0"];
            string sSortDir_0 = Request["sSortDir_0"];
            string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
            string orderByClause = string.Empty;
            int totalPageCount = 0;
            if (iSortCol_0 == "0")
                orderByClause = "RoleId desc";
            else
                if (SortFieldName == "RoleName")
                {
                    SortFieldName = "RoleName";
                }
                else if (SortFieldName == "Description")
                {
                    SortFieldName = "Description";
                }
                else if (SortFieldName == "Status")
                {
                    SortFieldName = "ActiveStatus";
                }
                orderByClause = SortFieldName + " " + sSortDir_0;
            var listofRoles = _irolerepository.GetRolesDetails(Keyword_RoleName, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
            for (int i = 0; i < listofRoles.Count; i++)
            {
                if (listofRoles[i].Status == "1")
                {
                    listofRoles[i].ActiveStatus = "Active";
                }
              
            }
               return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listofRoles,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult DeleteRole(string RoleId)
        {
            try
            {
                bool isdeleted = false;
                string ClientID = GlobalVariables.ClientName;
                // Int32 UserId = Convert.ToInt32(GlobalVariables.UserID);
                string conStr = GlobalVariables.ConnectionString;
                bool status = false;
                role rolemodel = new role();
                int roleid = Convert.ToInt32(RoleId);
               
                if (RoleId != null)
                {
                    isdeleted = _irolerepository.DeleteRole(roleid, status, conStr, GlobalVariables.ClientName);
                    isdeleted = true;
                }
                return Json(isdeleted, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult DeleteRoles(List<role> RoleData)
        {
            try
            {
                bool isdeleted = false;
                string ClientID = GlobalVariables.ClientName;
                // Int32 UserId = Convert.ToInt32(GlobalVariables.UserID);
                string conStr = GlobalVariables.ConnectionString;
                bool status = false;
         for (int i = 0; i < RoleData.Count; i++)
                {
                    if (!string.IsNullOrEmpty(RoleData[i].RoleId.ToString()))
                    {
                        isdeleted = _irolerepository.DeleteRole(Convert.ToInt32(RoleData[i].RoleId),status, conStr, GlobalVariables.ClientName);
                        isdeleted = true;
                    }
                }
                return Json(isdeleted, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult CreateRole()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public ActionResult CreateRole(role objrole)
        {
            try
            {
              
                bool result = _irolerepository.CreateRoleName(objrole, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
              
                TempData["Created"] = "Created";
                return RedirectToAction("Index","Roles/");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult EditRole(int RoleId)
        {
            try
            {
                role roleObj = new role();
                roleObj = _irolerepository.EditRoledetailsbyroleId(RoleId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return View(roleObj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult EditRole(role roles)
        {
            try
            {
                // int result 
               // TempData["Updaterole"] = "Updaterole";
                int roleid = Convert.ToInt32(roles.RoleId);
                bool result = _irolerepository.UpdateRoledetails(roles, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                TempData["Updated"] = "Updated";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult CheckRoleName(string RoleName)
        {
            try
            {
                string conStr = GlobalVariables.ConnectionString;
                role roles = new role();
                bool isexists = _irolerepository.CheckRoleName(RoleName,conStr, GlobalVariables.ClientName);
                if (roles.RoleName == null)
                {
                    
                    isexists = true;
                }
                else
                {
                    isexists = false;
                }
                return Json(isexists, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }

    }
}