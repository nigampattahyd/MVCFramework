using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.Repository;
using CRMHub.ViewModel;
using System.Text.RegularExpressions;

namespace CRM.Controllers
{

    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _iuserrepository;
        private readonly IRoleRepository _irolerepository;
        private readonly ICommonRepository _icommonpository;
        private readonly ICompanyRepository _icompanyrepository;
        private readonly IContactsRepository _icontactrepository;
        public UsersController(IUserRepository iuserrepository, IRoleRepository irolerepository, ICommonRepository icommonpository, ICompanyRepository icompanyrepository, IContactsRepository icontactrepository)
        {
            _iuserrepository = iuserrepository;
            _irolerepository = irolerepository;
            _icommonpository = icommonpository;
            _icompanyrepository = icompanyrepository;
            _icontactrepository = icontactrepository;
        }
        string loginid = GlobalVariables.UserID;
        string roleid = GlobalVariables.RoleID;
        //
        // GET: /Users/

        /// <summary>
        /// This methods displays the superadmin details defaultly based on the level.
        /// </summary>
        /// <param name="usermodel"></param>
        /// <returns></returns>
        public ActionResult Index(UsersModel usermodel)
        {
            try
            {
                usermodel.lstRoles = _irolerepository.GetLevels(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                user UsrselectOption = new user();
                UsrselectOption.RoleId = -1;
                UsrselectOption.roleName = "Select All";
                //usermodel.



                usermodel.getListOfStates = _icommonpository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                Branch selectOption = new Branch();
                selectOption.BranchId = -1;
                selectOption.BranchName = "All";
                usermodel.listBranch = new List<Branch>();
                usermodel.listBranch.Add(selectOption);
                usermodel.listBranch.AddRange(_iuserrepository.getUserbranchlist(loginid, roleid, GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                usermodel.users = new user();
                usermodel.users.RoleId = -1;
                List<role> lstrole = new List<role>();
               // lstrole = usermodel.lstRoles.Where(x => x.RoleId != 10 && x.RoleId != 13 && x.RoleId != 18).ToList();
                lstrole = usermodel.lstRoles.Where(x => x.RoleId != 1).ToList();
                ViewBag.roleids = lstrole;

                if (TempData["CreateUser"] != null)
                {
                    ViewBag.CreateUser = "CreateUser";

                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(usermodel);
        }

        /// <summary>
        /// This method displays the data according to the Search criteria  based on the name,email,city,state,level,branch,phone.
        /// </summary>
        /// <param name="usermodel"></param>
        /// <returns></returns>       
        public ActionResult SearchUser(jQueryDataTableParamModel param)
        {
            try
            {
                string keyword = Request["Adkeyword"];
                string SearchName = Request["AdName"];
                string Level = Request["Adlevels"];
                string Branch = Request["AdBranch"];
                string SearchCity = Request["AdCity"];
                string SearchState = Request["Adstate"];
                string Phone = Request["AdPhone"];
                string Email = Request["Ademail"];
                string Zipcode = "";

                //string keyword = "";
                //string SearchName = "";
                //string Level = "";
                //string Branch = "-1";
                //string SearchCity = "";
                //string SearchState = "AA";
                //string Phone = "";
                //string Email = "";
                //string Zipcode = "";

                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];

                if (SortFieldName == "FirstName")
                {
                    SortFieldName = "fullName";
                }



                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = " UserId desc";
                else
                    orderByClause = "  " + SortFieldName + " " + sSortDir_0;
                //orderByClause = SortFieldName + " " + sSortDir_0;
                var listusers = _iuserrepository.getListOfUsers(SearchName == "" ? null : SearchName, Email == "" ? null : Email, SearchCity == "" ? null : SearchCity,
                    Phone == "" ? null : Phone, keyword == "" ? null : keyword, SearchState == "" ? null : SearchState, Zipcode, Level == "-1" ? "All" : Level,
                    Branch == "-1" ? "All" : Branch, loginid, Convert.ToInt64(GlobalVariables.RoleID), param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listusers,
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
        /// By using this method we can create new user.
        /// </summary>
        /// <param name="usermodel"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateUser(UsersModel usermodel)
        {
            try
            {
                Branch selectOption = new Branch();
                selectOption.BranchId = -1;
                selectOption.BranchName = "Select Office";
                usermodel.listBranch = new List<Branch>();
                usermodel.listBranch.Add(selectOption);
                usermodel.listBranch.AddRange(_iuserrepository.getUserbranchlist(loginid, roleid, GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                role selectRole = new role();
                selectRole.RoleId = -1;
                selectRole.RoleName = "Select Role";
                usermodel.lstRoles = new List<role>();
                usermodel.lstRoles.Add(selectRole);
                usermodel.lstRoles.AddRange(_irolerepository.GetLevels(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                usState selectState = new usState();
                selectState.StateCode = "-1";
                selectState.StateName = "Select State";
                usermodel.getListOfStates = new List<usState>();
                usermodel.getListOfStates.Add(selectState);
                usermodel.getListOfStates.AddRange(_icommonpository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName));


                List<role> lstrole = new List<role>();
                //lstrole = usermodel.lstRoles.Where(x => x.RoleId != 10).ToList();
                //lstrole = usermodel.lstRoles.Where(x => x.RoleId != 10 && x.RoleId != 13 && x.RoleId != 18).ToList();
                lstrole = usermodel.lstRoles.Where(x => x.RoleId != 1).ToList();
                ViewBag.roleids = lstrole;
                return View(usermodel);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method saves the created new user to the database.
        /// </summary>
        /// <param name="objuser"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser(user users, FormCollection fc)
        {
            try
            {
                TempData["CreateUser"] = "CreateUser";
                string username = _iuserrepository.NewUserName(users, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                //users.LoginId = username;
                users.Password = EncryptDecrypt.Encrypt(users.Password);
                int createdresult = _iuserrepository.CreateNewUser(users, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method Edits the particular user details based on their userid.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditUserDetail(int userid)
        {
            try
            {

                UsersModel usermodel = new UsersModel();
                usermodel.users = _iuserrepository.EditUserdetailsbyuserId(userid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                usermodel.users.UserId = userid;
                usermodel.users.Password = EncryptDecrypt.Decrypt(usermodel.users.Password);

                role selectRole = new role();
                selectRole.RoleId = -1;
                selectRole.RoleName = "Select Role";
                usermodel.lstRoles = new List<role>();
                usermodel.lstRoles.Add(selectRole);
                usermodel.lstRoles.AddRange(_irolerepository.GetLevels(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                List<role> lstrole = new List<role>();
                lstrole = usermodel.lstRoles.Where(x => x.RoleId != 1).ToList();
               // lstrole = usermodel.lstRoles.Where(x => x.RoleId != 10 && x.RoleId != 13 && x.RoleId != 18).ToList();
                ViewBag.roleids = lstrole;

                Branch selectOption = new Branch();
                selectOption.BranchId = -1;
                selectOption.BranchName = "Select Office";
                usermodel.listBranch = new List<Branch>();
                usermodel.listBranch.Add(selectOption);
                usermodel.listBranch.AddRange(_iuserrepository.getUserbranchlist(loginid, roleid, GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                List<usState> listOfState = _icommonpository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                List<SelectListItem> listOfStates = listOfState.Select(state => new SelectListItem
                {
                    Text = state.StateName,
                    Value = state.StateCode
                }).ToList();
                ViewBag.StateList = listOfStates;

                if (TempData["UpdateUsr"] != null)
                {
                    ViewBag.UserUpdate = "Update";

                }
                
                return View(usermodel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method updates the edited details of the user based on their userid.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUserDetailByUserId(user users, FormCollection fc)
        {
            try
            {
                TempData["UpdateUsr"] = "Updateuser";
                users.Password = EncryptDecrypt.Encrypt(users.Password);
                int userid = Convert.ToInt32(users.UserId);
                int result = _iuserrepository.UpdateUserdetailsbyuserId(users, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //return RedirectToAction("EditUserDetail", new { userId = users.UserId });
                //return RedirectToAction("Index");
                int GUserid = Convert.ToInt32(GlobalVariables.UserID);

                if (GUserid == userid)
                {
                    Session["Logeduser"] = users.FirstName.ToString() + " " + users.LastName.ToString();
                }
                return RedirectToAction("EditUserDetail", new { userid = userid });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This Method Deletes the particular user based on their userid.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

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
                Branch objBranch = _iuserrepository.GetBranchDetailsByBranchName(branchid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
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
                bool objemail = _iuserrepository.CheckEmailExistsareNot(email, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(objemail, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [ActionName("IsLoginExists")]
        public JsonResult IsLoginExists(user users)
        {
            try
            {
                var result = _iuserrepository.CheckLoginExistsareNot(users, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


                if (users.UserId.Equals(0))
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }




        [HttpGet]
        [ActionName("IsEmailExists")]
        public JsonResult IsEmailExists(user users)
        {
            try
            {

                var result = _iuserrepository.CheckEmailExistsareNot(users, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (users.UserId.Equals(0))
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }


        # region User Roles

        public ActionResult RoleIndex()
        {
            try
            {
                if (TempData["CreateRole"] != null)
                {
                    ViewBag.CreateLeads = "CreateRole";

                }
                //role UserRole = new role();
                //return View(UserRole);
                return View();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpGet]
        public ActionResult GetUserRolesList(jQueryDataTableParamModel param)
        {
            try
            {
                string Contacttype = Request["Contacttype"];
                string Status = Request["ContactStatus"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "RoleId desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;


                var LstRoles = _irolerepository.GetAllRoles(Contacttype == "" ? "" : Contacttype, Status == "0" ? null : Status, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < LstRoles.Count; i++)
                {
                    if(LstRoles[i].Status == "1")
                    {
                        LstRoles[i].ActiveStatus = "Active";
                    }
                    else if(LstRoles[i].Status == "0")
                    {
                        LstRoles[i].ActiveStatus = "In-Active";
                    }
                    else LstRoles[i].ActiveStatus = "";
                }
                
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = LstRoles,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public ActionResult AddRole()
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
        public ActionResult AddRole(role objrole)
        {
            try
            {
                TempData["CreateRole"] = "CreateRole";
                int result = _irolerepository.createRole(objrole, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return RedirectToAction("RoleIndex");
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost]
        [ActionName("DeleteRole")]
        public string DeleteRole(string Roleids)
        {
            //bool res = false;
            string Rolemsg = "false";
            int totRoleCount = 0;
            int totdelcount = 0;
            if (Roleids != null)
            {
               

                foreach (var RoleId in Roleids.Split(','))
                {
                    totRoleCount = totRoleCount + 1;
                    int croleid = Convert.ToInt32(RoleId);
                    int UserroleCount = _irolerepository.GetUserrolecountfordelete(croleid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    if(UserroleCount ==0)
                    {
                       var delrole = _irolerepository.DeleteRoledetailsbyId(Convert.ToInt32(RoleId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                       // res = true;
                        totdelcount = totdelcount + 1;
                    }
                }
            }

            if (totRoleCount == totdelcount)
            {
                Rolemsg = "TotDel";
            }
            else if ((totdelcount < totRoleCount) && (totdelcount  > 0))
            {
                Rolemsg = "PartialDel";
            }
            else if (totdelcount == 0)
            {
                Rolemsg = "NoDel";
            }
            return Rolemsg;
        }

        [HttpGet]
        public ActionResult EditRoledetails(int RoleId)
        {
            try
            {
                role roleObj = new role();
                roleObj = _irolerepository.EditRoledetailsbyroleId(RoleId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (TempData["Updaterole"] != null)
                {
                    ViewBag.RoleUpdate = "Update";

                }
                return View(roleObj);
            }
            catch (Exception)
            {
                throw;
            }
        }




        /// <summary>
        /// This method updates the edited details of the Roles based on their Roleid.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditRoledetails(role Urole)
        {
            try
            {
               // int result 
                TempData["Updaterole"] = "Updaterole";
                int roleid = Convert.ToInt32(Urole.RoleId);

                int result = _irolerepository.UpdateRoledetailsbyroleId(Urole, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                
                return RedirectToAction("EditRoledetails", new { RoleId = roleid });
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// to check Whether the role is already Exist or not 
        /// [HttpGet]
        [ActionName("IsRoleExists")]
        public JsonResult IsRoleExists(role roleobj)
        {
            try
            {
                //CompanyName.Replace(" ", "")
                roleobj.RoleName = roleobj.RoleName.Replace(" ", "");
                var result =_irolerepository.IsRoleExists(roleobj,GlobalVariables.ConnectionString,GlobalVariables.ClientName);
                //var result = _icompanyrepository.IsCompanyExists(cmpModel.CompanyObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (roleobj.RoleId.Equals(0))
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(result, JsonRequestBehavior.AllowGet);               
            }
            catch (Exception)
            {
                throw;
            }
        }
        # endregion


    }
}