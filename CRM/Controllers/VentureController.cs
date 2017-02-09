using CRM.Helper;
using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.Repository;
using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class VentureController : ControllerBase
    {
        private readonly IVenture _iVenture = null;
        private readonly ICommonRepository _icommonrepository;
        private readonly ICustomFieldsRepository _icustomfieldrepository;
        private readonly IErrorLogRepository _ierrorlogrepository;
        private readonly IMentor _iMentor = null;
        private readonly ISchedulerRepository _iSchedulerRepository = null;
        private readonly IUserRepo _iuserrepo = null;
        public VentureController()
        {
            _iVenture = new VentureRepository();
            _icommonrepository = new CommonRepository();
            _icustomfieldrepository = new CustomFieldsRepository();
            _ierrorlogrepository = new ErrorLogRepository();
            _iMentor = new MentorRepository();
            _iSchedulerRepository = new SchedulerRepository();
            _iuserrepo = new UserRepo();
        }
        // GET: Venture
        public ActionResult Index()
        {
            try
            {
                ViewBag.Updated = TempData["VUpdated"];
                ViewBag.Created = TempData["VCreated"];

                ViewBag.roleid = GlobalVariables.RoleID;
                return View();
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/Index";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        public ActionResult getVenture(jQueryDataTableParamModel param)
        {
            try
            {
                string Keyword_VentureName = Request["Keyword_VentureName"].TrimStart();
                string AlphanumericSort = Request["Keyword_AlphabetLetter"].TrimStart();
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "vStatus.Status Desc";
                else
                    if (SortFieldName == "Status")
                    {
                        SortFieldName = "vStatus.Status";
                    }
                    else if (SortFieldName == "strVenCreatedDate")
                    {
                        SortFieldName = "venture.CreatedDate";
                    }
                    else if (SortFieldName == "strActiveDate")
                    {
                        SortFieldName = "venture.ModifiedDate";
                    }
                    else if (SortFieldName == "strInactiveDate")
                    {
                        SortFieldName = "venture.ModifiedDate";
                    }
                orderByClause = SortFieldName + " " + sSortDir_0;

                int ventureid = 0;

                if (GlobalVariables.VentureId != null && GlobalVariables.VentureId != "")
                {
                    ventureid = Convert.ToInt32(GlobalVariables.VentureId);
                }

                var lstVentures = _iVenture.GetVentureDetails(ventureid, Keyword_VentureName,AlphanumericSort, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                TempData["TotalCount"] = totalPageCount;

                TempData["VUpdated"] = "";
                TempData["VCreated"] = "";

                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = lstVentures,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/getVenture";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpGet]
        public ActionResult CreateVenture()
        {
            try
            {
                string Module = "Venture";
                VentureViewModel vmVenture = new VentureViewModel();

                vmVenture.objlstmentor = ListofMentors();
                vmVenture.objVenture = new Venture();
                int UserId = Convert.ToInt32(GlobalVariables.UserID);
                string userfullname = _icommonrepository.GetusernameById(UserId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                vmVenture.lstMVSStatus = _iVenture.GetAllVMSStatus(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                vmVenture.listCustomfields = _icustomfieldrepository.GetCustomFieldsByModule(Module, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < vmVenture.listCustomfields.Count(); i++)
                {
                    if (vmVenture.listCustomfields[i].Column_Type == "dropdown")
                    {
                        vmVenture.listCustomfields[i].DefaultValue = vmVenture.listCustomfields[i].lstCustomOptions.Where(l => l.IsDefault == true).Select(s => s.DrpValueId).SingleOrDefault().ToString();
                    }
                }
                //return View(cmpobject);
                return View(vmVenture);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/CreateVenture";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CreateVenture")]
        public ActionResult CreateVenture(VentureViewModel ventureModel, FormCollection fc)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    TempData["CreateCompany"] = "CreateVenture";
                    /// New Company Details Insertion

                    DateTime CreatedDate = DateTime.Now;
                    ventureModel.objVenture.CreatedDate = CreatedDate;
                    //ventureModel.objVenture.ModifiedDate = CreatedDate;
                    ventureModel.objVenture.CreatedBY = Convert.ToInt32(GlobalVariables.UserID);
                    //ventureModel.objVenture.ModifiedBy = Convert.ToInt32(GlobalVariables.UserID);
                    if (!string.IsNullOrEmpty(fc[14]))
                    {
                        ventureModel.objVenture.VmsStatusId = Convert.ToInt32(fc[14].ToString());
                    }

                    ventureModel.objVenture.StudentIdeaBasedOnResearch = ventureModel.objVenture.StudentIdea;
                    ventureModel.objVenture.ResearchIdeaDisclosedToTLO = ventureModel.objVenture.ResearchIdea;
                    ventureModel.objVenture.LegalEntityAtEnrollment = ventureModel.objVenture.LegalEntity;
                    ventureModel.objVenture.IsIntroduced = Convert.ToBoolean(ventureModel.objVenture.Introduced);
                    string[] password = ventureModel.objVenture.PrimaryEmail.Split('@');
                    ventureModel.objVenture.password = password[0];
                    ventureModel.objVenture.password = EncryptDecrypt.Encrypt(ventureModel.objVenture.password);
                    ventureModel.objVenture.CreatedDate = DateTime.Now;

                    ventureModel.objVenture.CreatedBY = Convert.ToInt32(GlobalVariables.UserID);
                    Int64 ventureId = _iVenture.InserNewVenture(ventureModel.objVenture, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    SendEmailToDigital55Users(ventureModel.objVenture.PrimaryEmail, ventureModel.objVenture.PrimaryEmail, password[0]);



                    TempData["VCreated"] = "VCreated";
                    return RedirectToAction("Index", "Venture/");
                }
               
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException 

                        Error_Log objErrorLog = new Error_Log();
                        objErrorLog.CreatedDate = DateTime.Now;
                        objErrorLog.ErrorMessage = message;
                        objErrorLog.ErrorPage = "Venture/CreateVenture";//controllerName + "/" + actionName;
                        _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                    }
                }
            }
            return RedirectToAction("CreateVenture", "Venture");
           
        }

        [HttpGet]
        public ActionResult getVenturedetails(Int64 VentureId, string Module)
        {
            try
            {
                VentureViewModel ventureModel = new VentureViewModel();
                //  ventureModel.getListOfcollaborator = _icommonrepository.GetAllCollaborationList(GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                //List<CollaboratorAffiliation> lstchkP = new List<CollaboratorAffiliation>();
                List<CollabP> lstchkP;
                List<CollabS> lstchkS;
                List<Advertisement> lstadvchk;
                List<CurrentStatus> lststatuschk;
                List<VMShelp> lstvmsHelpchk;
                List<Category> lstcategory;
                CheckBoxDataforVentureDetails(out  lstcategory, out lstchkP, out lstchkS, out lstadvchk, out lststatuschk, out lstvmsHelpchk);

               
                ventureModel.objlstmentor = ListofMentors();
                if(ventureModel.objlstmentor==null)
                {
                    ventureModel.objlstmentor = new List<Mentor>();
                }
                ventureModel.lstMVSStatus = _iVenture.GetAllVMSStatus(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                ventureModel.objVenture = _iVenture.GetVentureDetailsByID(VentureId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                // ventureModel.objVenturedetail = _iVenture.GetPendingVenturedetailsbyId(VentureId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                ventureModel.objVenture.Status = ventureModel.objVenture.VmsStatusId.ToString();


                ventureModel.objVenture.StudentIdea = Convert.ToBoolean(ventureModel.objVenture.StudentIdeaBasedOnResearch);
                ventureModel.objVenture.ResearchIdea = Convert.ToBoolean(ventureModel.objVenture.ResearchIdeaDisclosedToTLO);
                ventureModel.objVenture.LegalEntity = Convert.ToBoolean(ventureModel.objVenture.LegalEntityAtEnrollment);
                ventureModel.objVenture.Introduced = Convert.ToBoolean(ventureModel.objVenture.Introduced);
                GetCheckboxcheckedValues(ventureModel, lstcategory, lstchkP, lstchkS, lstadvchk, lststatuschk, lstvmsHelpchk);

                ViewData["OldVmsStatusId"] = ventureModel.objVenture.VmsStatusId;

                VMSStatu objvmststus = _iVenture.GetVmsStatusNameByID(Convert.ToInt64(ViewData["OldVmsStatusId"]), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (objvmststus != null)
                {
                    ViewData["OldVmsStatus"] = objvmststus.Status;
                }
                else
                {
                    ViewData["OldVmsStatus"] = "";
                }

                //string totalcount = TempData["TotalCount"].ToString();
                string Modulename = "Venture";



                if (TempData["UpdateCompany"] != null)
                {
                    ViewBag.CompanyUpdate = "UpdateCompany";

                }

                return View("getVenturedetails", ventureModel);

            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/getVenturedetails";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpPost]
        public ActionResult UpdateVenture(VentureViewModel ventureModel, FormCollection fc)
        {
            try
            {
                TempData["CreateCompany"] = "UpdateVenture";
                /// New Company Details Insertion
                /// 

                UpdateCheckBoxesVentureDetails(ventureModel, fc);

                DateTime CreatedDate = DateTime.Now;
                ventureModel.objVenture.IndustryCategoryName = fc["objVenture.IndustryCategoryName"];
                ventureModel.objVenture.ModifiedDate = CreatedDate;
                ventureModel.objVenture.ModifiedBY = Convert.ToInt32(GlobalVariables.UserID);
                if (!string.IsNullOrEmpty(fc["objVenture.Status"]))
                {
                    ventureModel.objVenture.VmsStatusId = Convert.ToInt32(fc["objVenture.Status"].ToString());
                }

                ventureModel.objVenture.StudentIdeaBasedOnResearch = ventureModel.objVenture.StudentIdea;
                ventureModel.objVenture.ResearchIdeaDisclosedToTLO = ventureModel.objVenture.ResearchIdea;
                ventureModel.objVenture.LegalEntityAtEnrollment = ventureModel.objVenture.LegalEntity;
                ventureModel.objVenture.IsIntroduced = Convert.ToBoolean(ventureModel.objVenture.Introduced);
                ventureModel.objVenture.strVenCreatedDate = CreatedDate.ToString();
               
                Int64 ventureId = _iVenture.updateVentureDetails(ventureModel.objVenture, GlobalVariables.ConnectionString, GlobalVariables.ClientName, "Update");
                user objuser=new user();
               
                objuser.UserId =Convert.ToInt64(ventureModel.objVenture.UserId);
                if (ventureModel.objVenture.VmsStatusId == 2)
                {
                    bool isuserupdated = _iuserrepo.UpdateUserDetailsbyStatus(true,objuser.UserId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                }
                TempData["VUpdated"] = "VUpdated";
                return RedirectToAction("Index", "Venture/");
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/UpdateVenture";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return RedirectToAction("getVenturedetails", "Venture/", new { @VentureId = ventureModel.objVenture.VentureId, @Module = "Venture" });
            }

        }

        public ActionResult IsVentureExists(VentureViewModel model)
        {
            string VentureName = string.Empty;
            try
            {
                if (model != null)
                {
                    VentureName = model.objVenture.VentureName.Trim();
                }
                bool objcompanyname = _iVenture.IsVentureExists(VentureName, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(objcompanyname, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/IsVentureExists";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpPost]
        public bool deleteVenture(string ventureIds, string status)
        {
            bool flag = false;
            try
            {
                var ClientEntity = new DevEntities();
                Venture objventure = new Venture();
                user objuser = new user();
                ///objventure.VmsStatusId = 3;
                objventure.VentureId = Convert.ToInt64(ventureIds);
               // objventure.Status = "InActive";
                objventure.ModifiedDate = DateTime.Now;
                objventure.ModifiedBY = Convert.ToInt32(GlobalVariables.UserID);
                objventure.Status = status;
                if (!string.IsNullOrEmpty(ventureIds))
                {
                    int result = _iVenture.updateVentureDetails(objventure, GlobalVariables.ConnectionString, GlobalVariables.ClientName, "Delete");
                    if (result > 1)
                    {
                        objuser.UserId = result;
                        objuser.Status = false;
                        objuser.ModifiedBy = GlobalVariables.UserID;
                        objuser.LastModified = DateTime.Now;
                        objuser.ClientId = GlobalVariables.ClientName;
                        flag = _iuserrepo.UpdateUserDetailsbyloginID(objuser, GlobalVariables.ConnectionString, GlobalVariables.ClientName, "Delete");
                        if (flag == true)
                        {
                            return flag = true;
                        }
                        else
                        {
                            return flag = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/deleteVenture";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return false;
            }
            return flag;
        }

        [HttpPost]
        public JsonResult DeleteVentures(List<Venture> ventureData)
        {
            bool flag = false;
            try
            {
                var ClientEntity = new DevEntities();
                for (int i = 0; i < ventureData.Count; i++)
                {
                    if (!string.IsNullOrEmpty(ventureData[i].VentureId.ToString()))
                    {

                        //  flag = _iVenture.DeleteVentureByVentureId(Convert.ToInt32(ventureData[i].VentureId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        Venture objventure = new Venture();
                        user objuser = new user();

                       // objventure.VmsStatusId = 3;
                        objventure.VentureId = Convert.ToInt64(ventureData[i].VentureId);
                        objventure.VmsStatusId = ventureData[i].VmsStatusId;
                        objventure.Status = ventureData[i].Status;
                        //objventure.Status = "InActive";
                        objventure.ModifiedDate = DateTime.Now;
                        objventure.ModifiedBY = Convert.ToInt32(GlobalVariables.UserID);

                        int result = _iVenture.updateVentureDetails(objventure, GlobalVariables.ConnectionString, GlobalVariables.ClientName, "Delete");
                        if (result > 1)
                        {
                            objuser.UserId = result;
                            objuser.Status = false;
                            objuser.ModifiedBy = GlobalVariables.UserID;
                            objuser.LastModified = DateTime.Now;
                            objuser.ClientId = GlobalVariables.ClientName;
                            flag = _iuserrepo.UpdateUserDetailsbyloginID(objuser, GlobalVariables.ConnectionString, GlobalVariables.ClientName, "Delete");
                            if (flag == true)
                            {
                                flag = true;
                            }
                            else
                            {
                                flag = false;
                            }
                        }

                    }
                }
                return Json(flag, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/DeleteVentures";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }

        }

        public ActionResult IsEmailExists(VentureViewModel model)
        {
            string Email = string.Empty;
            try
            {
                if (model != null)
                {
                    Email = model.objVenture.PrimaryEmail.Trim();
                }
                bool objemail = _icommonrepository.IsEmailIdExists(Email, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(objemail, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/IsEmailExists";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        public async Task SendEmailToDigital55Users(string to, string username, string password)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                string subject = "Credentials of Venture";
                string MessageBody = "";
                MessageBody = "<html><body><table><tr><td>The following are login  credentials of Mentoring </td></tr> " +
                                  " <tr><td>Username            : " + username + "</td></tr> " +
                                  " <tr><td>Password            : " + password + " </td></tr> " +
                                  " </table> </body></html>";
                SendEmail objEmail = new SendEmail();
                await Task.Run(() => objEmail.SendEmailasync(fromEmail, to, subject, MessageBody));
            
                //objEmail.SendAsyncMail(fromEmail, to, subject, MessageBody);
                //return true;
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/SendEmailToDigital55Users";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                //return false;
            }
        }

        public List<Mentor> ListofMentors()
        {
            try
            {
                List<Mentor> objmentorslist = new List<Mentor>();

                objmentorslist = _iMentor.GetAllLeadMentorsList(GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                return objmentorslist;



            }
            catch (Exception ex)
            {

                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/ListofMentors";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }
        [HttpGet]
        public ActionResult PendingVentureDetails(Int64 VentureId, string Module)
        {
            try
            {
                VentureViewModel ventureModel = new VentureViewModel();
                List<CollabP> lstchkP;
                List<CollabS> lstchkS;
                List<Advertisement> lstadvchk;
                List<CurrentStatus> lststatuschk;
                List<VMShelp> lstvmsHelpchk;
                List<Category> lstcategory;
                CheckBoxDataforVentureDetails(out  lstcategory,out lstchkP, out lstchkS, out lstadvchk, out lststatuschk, out lstvmsHelpchk);
                ventureModel.listofCategory = lstcategory;
                ventureModel.lstMVSStatus = _iVenture.GetAllVMSStatus(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                // ventureModel.objVenture = _iVenture.GetVentureDetailsByID(VentureId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                ventureModel.objVenture = _iVenture.GetPendingVenturedetailsbyId(VentureId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                ventureModel.objVenture.Status = ventureModel.objVenture.VmsStatusId.ToString();
                GetCheckboxcheckedValues(ventureModel,lstcategory, lstchkP, lstchkS, lstadvchk, lststatuschk, lstvmsHelpchk);
                return View("PendingVentureDetails", ventureModel);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/PendingVentureDetails";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpPost]
        public ActionResult ConvertactiveVenture(VentureViewModel ventureModel, FormCollection fc)
        {
            try
            {
                TempData["CreateCompany"] = "UpdateVenture";
                /// New Company Details Insertion
                UpdateCheckBoxesVentureDetails(ventureModel, fc);
                ventureModel.objVenture.ModifiedDate = DateTime.Now;
                ventureModel.objVenture.ModifiedBY = Convert.ToInt32(GlobalVariables.UserID);
                TempData["VUpdated"] = "VUpdated";
                ventureModel.objVenture.IndustryCategoryName = fc["objVenture.IndustryCategoryName"]; 
                Int32 venturedetailsid = _iVenture.updatePendingVentureDetails(ventureModel.objVenture, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                user objuser = _iuserrepo.GetUserDetailsbyUserID(venturedetailsid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
               var password=EncryptDecrypt.Decrypt(objuser.Password);
               SendEmailToDigital55Users(objuser.Email,objuser.LoginId,password.ToString()) ;
                return RedirectToAction("Index", "Venture/");
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/ConvertactiveVenture";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        private static void CheckBoxDataforVentureDetails(out List<Category> lstcategory, out List<CollabP> lstchkP, out List<CollabS> lstchkS, out List<Advertisement> lstadvchk, out List<CurrentStatus> lststatuschk, out List<VMShelp> lstvmsHelpchk)
        {
            lstcategory = new List<Category>()
            {
                  new Category {Text="One", Value="1"},
                  new Category {Text="Two", Value="2" },
                // new Category {Text="Steel", Value="Steel", ID=1,IsSelected=true},
                //new Category {Text="Pharmaceticals", Value="Pharmaceticals",ID=2 },
                //new Category {Text="Tourism", Value="Tourism",ID=3 },
            };

            lstchkP = new List<CollabP>()
               {
                new CollabP {Text="ProMedica", Value="ProMedica", ID=1},
                new CollabP {Text="Mercy", Value="Mercy",ID=2 },
                new CollabP {Text="UT", Value="UT",ID=3 },
                new CollabP {Text="BGSU", Value="BGSU",ID=4 },
                new CollabP {Text="NextTech", Value="NextTech",ID=5 },
                new CollabP {Text="Other", Value="Other",ID=6 }
               };
            lstchkS = new List<CollabS>()
               {
                new CollabS {Text="ProMedica", Value="ProMedica", ID=1},
                new CollabS {Text="Mercy", Value="Mercy",ID=2 },
                new CollabS {Text="UT", Value="UT",ID=3 },
                new CollabS {Text="BGSU", Value="BGSU",ID=4 },
                new CollabS {Text="NextTech", Value="NextTech",ID=5 },
                new CollabS {Text="Other", Value="Other",ID=6 }
               };

            lstadvchk = new List<Advertisement>()
                {
                new Advertisement {Text="Google or other search", Value="Google or other search", ID=1},
                new Advertisement {Text="Word of mouth", Value="Word of mouth",ID=2 },
                new Advertisement {Text="Press", Value="Press",ID=3 },
                new Advertisement {Text="Advertisement", Value="Advertisement",ID=4 },
                new Advertisement {Text="Social media", Value="Social media",ID=5 },
                new Advertisement {Text="Other", Value="Other",ID=6 }
                };

            lststatuschk = new List<CurrentStatus>()
                {
                new CurrentStatus {Text="Idea exploration", Value="Idea exploration", ID=1},
                new CurrentStatus {Text="Ready to launch", Value="Ready to launch",ID=2 },
                new CurrentStatus {Text="Revenue generating", Value="Revenue generating",ID=3 },
                };

            lstvmsHelpchk = new List<VMShelp>()
                {
                new VMShelp {Text="Business Development", Value="Business Development", ID=1},
                new VMShelp {Text="IP Strategy", Value="IP Strategy", ID=2},
                new VMShelp {Text="Commercial potential of technology", Value="Commercial potential of technology",ID=3 },
                new VMShelp {Text="Early Team formation", Value="Early Team formation",ID=4 },
                new VMShelp {Text="Occasional Informal feedback", Value="Occasional Informal feedback",ID=5 },
                new VMShelp {Text="Other", Value="Other",ID=6 }
                };
        }

        private static void GetCheckboxcheckedValues(VentureViewModel ventureModel, List<Category> lstcategory, List<CollabP> lstchkP, List<CollabS> lstchkS, List<Advertisement> lstadvchk, List<CurrentStatus> lststatuschk, List<VMShelp> lstvmsHelpchk)
        {
            if (ventureModel.objVenture.IndustryCategoryName != null)
            {
                string category = ventureModel.objVenture.IndustryCategoryName.ToString();
                if (category != null)
                {
                    foreach (var item in lstcategory)
                    {
                        if (category == item.Value)
                        {
                            item.IsSelected = true;
                        }
                    }
                }
            }
            ventureModel.listofCategory = lstcategory;

            if (ventureModel.objVenture.PrimaryColaborationAffiliation != null)
            {

                string[] pc = ventureModel.objVenture.PrimaryColaborationAffiliation.ToString().Split(',');
                foreach (var item in pc)
                {
                    foreach (var chkval in lstchkP)
                    {
                        if (item == chkval.Value)
                        {
                            chkval.IsChecked = true;
                        }
                    }
                }
            }
            ventureModel.ListPrimaryColaborationAffiliation = lstchkP;

            if (ventureModel.objVenture.ScondaryColaborationAffiliation != null)
            {

                string[] sc = ventureModel.objVenture.ScondaryColaborationAffiliation.ToString().Split(',');
                foreach (var item in sc)
                {
                    foreach (var chkval in lstchkS)
                    {
                        if (item == chkval.Value)
                        {
                            chkval.IsChecked = true;
                        }
                    }
                }
            }
            ventureModel.ListSecondaryColaborationAffiliation = lstchkS;

            if (ventureModel.objVenture.Advertisement != null)
            {
                string[] selectedadv = ventureModel.objVenture.Advertisement.ToString().Split(',');
                foreach (var item in selectedadv)
                {
                    foreach (var chkval in lstadvchk)
                    {
                        if (item == chkval.Value)
                        {
                            chkval.IsChecked = true;
                        }
                    }
                }
            }
            ventureModel.ListofAdvertisement = lstadvchk;

            if (ventureModel.objVenture.WhatIsYourCurrentStatus != null)
            {
                string[] selectedstatus = ventureModel.objVenture.WhatIsYourCurrentStatus.ToString().Split(',');
                foreach (var item in selectedstatus)
                {
                    foreach (var chkval in lststatuschk)
                    {
                        if (item == chkval.Value)
                        {
                            chkval.IsChecked = true;
                        }
                    }
                }
            }
            ventureModel.Listofstatus = lststatuschk;

            if (ventureModel.objVenture.WhatTypeOfVMSHelpStartUpNeeds != null)
            {
                string[] selectedvmsHelp = ventureModel.objVenture.WhatTypeOfVMSHelpStartUpNeeds.ToString().Split(',');
                foreach (var item in selectedvmsHelp)
                {
                    foreach (var chkval in lstvmsHelpchk)
                    {
                        if (item == chkval.Value)
                        {
                            chkval.IsChecked = true;
                        }
                    }
                }
            }
            ventureModel.ListofVMShelp = lstvmsHelpchk;
        }

        private static void UpdateCheckBoxesVentureDetails(VentureViewModel ventureModel, FormCollection fc)
        {
            string[] collabp = fc["chkcollbp"].ToString().Split(',');

            string collp = "";
            if (collabp.Count() != 6)
            {
                foreach (var item in collabp)
                {
                    if (item != "false")
                    {

                        collp = collp + "," + item;
                    }

                }
            }
            else
            {
                collp = ",";
            }
            if (collp.Contains(","))
            {
                collp = collp.Remove(0, 1);
            }
            ventureModel.objVenture.PrimaryColaborationAffiliation = collp;

            string[] collabs = fc["chkcollbs"].ToString().Split(',');

            string colls = "";
            if (collabs.Count() != 6)
            {
                foreach (var item in collabs)
                {
                    if (item != "false")
                    {

                        colls = colls + "," + item;
                    }

                }
            }
            else
            {
                colls = ",";
            }
            if (colls.Contains(","))
            {
                colls = colls.Remove(0, 1);
            }
            ventureModel.objVenture.ScondaryColaborationAffiliation = colls;

            string[] vmshelp = fc["chkvmsHelp"].ToString().Split(',');
            string help = "";
            if (vmshelp.Count() != 6)
            {
                foreach (var item in vmshelp)
                {
                    if (item != "false")
                    {

                        help = help + "," + item;
                    }

                }
            }
            else
            {
                help = ",";
            }
            if (help.Contains(","))
            {
                help = help.Remove(0, 1);
            }
            ventureModel.objVenture.WhatTypeOfVMSHelpStartUpNeeds = help;


            string[] advrtise = fc["chkadvertise"].ToString().Split(',');
            string adv = "";
            if (advrtise.Count() != 6)
            {
                foreach (var item in advrtise)
                {
                    if (item != "false")
                    {

                        adv = adv + "," + item;
                    }
                }
            }
            else
            {
                adv = ",";
            }
            if (adv.Contains(","))
            {
                adv = adv.Remove(0, 1);
            }
            ventureModel.objVenture.Advertisement = adv;


            string[] currentstatus = fc["chkstatus"].ToString().Split(',');
            string status = "";

            foreach (var item in currentstatus)
            {
                if (item != "false")
                {

                    status = status + "," + item;
                }
            }

            if (status.Contains(","))
            {
                status = status.Remove(0, 1);
            }
            ventureModel.objVenture.WhatIsYourCurrentStatus = status;
        }

    }
}