using CRM.Helper;
using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.Repository;
using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class MentorController : ControllerBase
    {
        private readonly IMentor _iMentor = null;
        private readonly IVenture _iVenture = null;
        private readonly ICommonRepository _icommonrepository;
        private readonly ICustomFieldsRepository _icustomfieldrepository;
        private readonly IErrorLogRepository _ierrorlogrepository;
        private readonly IUserRepo _iuserrepo = null;
        public MentorController()
        {
            _iVenture = new VentureRepository();
            _icommonrepository = new CommonRepository();
            _icustomfieldrepository = new CustomFieldsRepository();
            _iMentor = new MentorRepository();
            _ierrorlogrepository = new ErrorLogRepository();
            _iuserrepo = new UserRepo();
        }
        // GET: Venture
        // GET: Mentor
        public ActionResult Index()
        {
            ViewBag.Updated = TempData["MUpdated"];
            ViewBag.Created = TempData["MCreated"];
            ViewBag.roleid = GlobalVariables.RoleID;
            return View();
        }

        [HttpGet]
        public ActionResult CreateMentor(string ventureid, string Module)
        {
            try
            {

                MentorViewmodel vmMentor = new MentorViewmodel();
                vmMentor.objMentor = new Mentor();
                int UserId = Convert.ToInt32(GlobalVariables.UserID);
                string userfullname = _icommonrepository.GetusernameById(UserId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                vmMentor.lstMVSStatus = _iVenture.GetAllVMSStatus(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                vmMentor.lstMentorType = _iMentor.getMentorTypeDetails(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //vmMentor.lstVenture = _iMentor.getVentureDetails(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                vmMentor.listCustomfields = _icustomfieldrepository.GetCustomFieldsByModule(Module, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < vmMentor.listCustomfields.Count(); i++)
                {
                    if (vmMentor.listCustomfields[i].Column_Type == "dropdown")
                    {
                        vmMentor.listCustomfields[i].DefaultValue = vmMentor.listCustomfields[i].lstCustomOptions.Where(l => l.IsDefault == true).Select(s => s.DrpValueId).SingleOrDefault().ToString();
                    }
                }
                ViewBag.Roleid = GlobalVariables.RoleID;
                if (GlobalVariables.RoleID == "1" && Module == "Venture")
                {
                    Venture objvent = _iVenture.GetVentureDetailsByID(Convert.ToInt32(ventureid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    ViewBag.VentureName = objvent.VentureName;
                }
                else
                {
                    ViewBag.VentureName = GlobalVariables.FullName;
                }


                TempData["pagename"] = Module;
                //return View(cmpobject);
                return View(vmMentor);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/CreateMentor";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CreateMentor")]
        public ActionResult CreateMentor(MentorViewmodel mentorModel, FormCollection fc)
        {
            try
            {

                /// New mentor Details Insertion
                # region New Company Details Insertion

                DateTime CreatedDate = DateTime.Now;
                mentorModel.objMentor.CreatedDate = CreatedDate;
                //ventureModel.objVenture.ModifiedDate = CreatedDate;
                mentorModel.objMentor.CreatedBy = Convert.ToInt32(GlobalVariables.UserID);
                //ventureModel.objVenture.ModifiedBy = Convert.ToInt32(GlobalVariables.UserID);
                if (fc["objMentor.VentureId"] == "")
                {
                    mentorModel.objMentor.VentureId = 0;
                }
                if (GlobalVariables.RoleID == "2")
                {
                    mentorModel.objMentor.VentureId = Convert.ToInt64(GlobalVariables.VentureId);
                }
                if (mentorModel.objMentor.MentorType != null)
                {
                    mentorModel.objMentor.MentorTypeId = Convert.ToInt32(mentorModel.objMentor.MentorType);
                }

                else
                {
                    mentorModel.objMentor.MentorTypeId = 0;
                }

                if (fc["objMentor.StartDate"] != null && fc["objMentor.StartDate"].ToString() != string.Empty)
                {
                    DateTime dt1 = DateTime.ParseExact(fc["objMentor.StartDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    mentorModel.objMentor.StartDate = Convert.ToDateTime(dt1);
                }
                if (fc["objMentor.EndDate"] != null && fc["objMentor.EndDate"].ToString() != string.Empty)
                {
                    DateTime dt2 = DateTime.ParseExact(fc["objMentor.EndDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    mentorModel.objMentor.EndDate = Convert.ToDateTime(dt2); ;
                }


                if (fc["objMentor.isHaverelevantExp"] != null && fc["objMentor.isHaverelevantExp"].ToString().Contains(','))
                {
                    mentorModel.objMentor.HaverelevantExp = true;
                }
                else
                {
                    mentorModel.objMentor.HaverelevantExp = false;
                }

                if (fc["objMentor.isWillhelpifNeeded"] != null && fc["objMentor.isWillhelpifNeeded"].ToString().Contains(','))
                {

                    mentorModel.objMentor.WillhelpifNeeded = true;
                }
                else
                {
                    mentorModel.objMentor.WillhelpifNeeded = false;
                }
                if (fc["objMentor.isInterested"] != null && fc["objMentor.isInterested"].ToString().Contains(','))
                {
                    mentorModel.objMentor.Interested = true;

                }
                else
                {
                    mentorModel.objMentor.Interested = false;
                }

                if (fc["objMentor.isRecruited"] != null && fc["objMentor.isRecruited"].ToString().Contains(','))
                {
                    mentorModel.objMentor.Recruited = true;
                }
                else
                {
                    mentorModel.objMentor.Recruited = false;
                }
                if (fc["objMentor.isConflictFinancialInterest"] != null && fc["objMentor.isConflictFinancialInterest"].ToString().Contains(','))
                {
                    mentorModel.objMentor.Conflict_FinancialInterest = true;
                }
                else
                {
                    mentorModel.objMentor.Conflict_FinancialInterest = false;
                }
                if (fc["objMentor.isActive"] != null && fc["objMentor.isActive"].ToString().Contains(','))
                {
                    mentorModel.objMentor.Active = true;
                }
                else
                {
                    mentorModel.objMentor.Active = false;
                }

                string[] password = mentorModel.objMentor.Email.Split('@');
                mentorModel.objMentor.Password = EncryptDecrypt.Encrypt(password[0]);
                mentorModel.objMentor.CreatedDate = DateTime.Now;
                mentorModel.objMentor.CreatedBy = Convert.ToInt32(GlobalVariables.UserID);
                Int64 ventureId = _iMentor.InserMentor(mentorModel.objMentor, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                SendEmailToDigital55Users(mentorModel.objMentor.Email, mentorModel.objMentor.Email, password[0]);
                # endregion
                int ventureid = 0;
                TempData["MCreated"] = "MCreated";
                if (TempData["pagename"].ToString() == "Venture")
                {
                    ventureid = Convert.ToInt32(mentorModel.objMentor.VentureId);
                    return RedirectToAction("getVentureDetails", "Venture", new { @VentureId = ventureid, @Module = "Mentor" });
                }
                else
                {
                    return RedirectToAction("Index", "Mentor/");
                }


            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/CreateMentor";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }


        [HttpGet]
        public ActionResult EditMentor(long MentorId, string Module)
        {
            try
            {
                TempData["UpdateLeads"] = "UpdateMentor";
                MentorViewmodel vmMentor = new MentorViewmodel();
                vmMentor.objMentor = new Mentor();
                vmMentor.objmentordetail = new MentorDetail();
                vmMentor.objmentorquestionnary = new MentorQuestionary();
                int UserId = Convert.ToInt32(GlobalVariables.UserID);
                string userfullname = _icommonrepository.GetusernameById(UserId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                vmMentor.lstMentorType = _iMentor.getMentorTypeDetails(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                vmMentor.lstMVSStatus = _iVenture.GetAllVMSStatus(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                vmMentor.objMentor = _iMentor.GetMentorDetailsByID(MentorId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                vmMentor.objMentor.MentorType = vmMentor.objMentor.MentorTypeId.ToString();
                vmMentor.objMentor.isRecruited = Convert.ToBoolean(vmMentor.objMentor.Recruited);

                vmMentor.objMentor.isHaverelevantExp = Convert.ToBoolean(vmMentor.objMentor.HaverelevantExp);
                vmMentor.objMentor.isWillhelpifNeeded = Convert.ToBoolean(vmMentor.objMentor.WillhelpifNeeded);
                vmMentor.objMentor.isConflictFinancialInterest = Convert.ToBoolean(vmMentor.objMentor.Conflict_FinancialInterest);
                vmMentor.objMentor.isInterested = Convert.ToBoolean(vmMentor.objMentor.Interested);

                vmMentor.listCustomfields = _icustomfieldrepository.GetCustomFieldsByModule(Module, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < vmMentor.listCustomfields.Count(); i++)
                {
                    if (vmMentor.listCustomfields[i].Column_Type == "dropdown")
                    {
                        vmMentor.listCustomfields[i].DefaultValue = vmMentor.listCustomfields[i].lstCustomOptions.Where(l => l.IsDefault == true).Select(s => s.DrpValueId).SingleOrDefault().ToString();
                    }
                }
                vmMentor.lststate = ListofState();
                vmMentor.lstcountry = ListofCountry();
                List<MQStartUps> lstmqstartups;
                List<MQBusinessModel> lstmqbusinessmodel;
                List<MQFunding> lstmqfunding;
                List<MQSoftware> lstmqsoftware;
                List<MQLifeSciences> lstmqlifesciences;
                List<MQIndustryOther> lstmqindustryother;
                List<MQFunctionalAreas> lstmqfunctionalareas;
                CheckBoxDataforMentorQuestionnaire(out  lstmqstartups, out lstmqbusinessmodel, out lstmqfunding, out lstmqsoftware, out  lstmqlifesciences,
                    out lstmqindustryother, out lstmqfunctionalareas);
                vmMentor.objmentordetail = _iMentor.GetPendingMentorDetailsByID(MentorId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                vmMentor.objmentorquestionnary = _iMentor.GetMentorQuestionaryByMID(MentorId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (vmMentor.objmentordetail == null)
                {
                    vmMentor.objmentordetail = new MentorDetail();
                }
                else
                {
                    if (vmMentor.objmentordetail.Resume != null && vmMentor.objmentordetail.Resume != "")
                    {
                        string[] resumefile = vmMentor.objmentordetail.Resume.Split('\\');
                        int count = resumefile.Count();
                        var getnamewithoutextn = System.IO.Path.GetFileNameWithoutExtension(resumefile[count - 1]);
                        vmMentor.objmentordetail.ResumeName = getnamewithoutextn;
                    }
                    else
                    {
                        vmMentor.objmentordetail.ResumeName = "";
                    }

                }

                if (vmMentor.objmentorquestionnary == null)
                {
                    vmMentor.objmentorquestionnary = new MentorQuestionary();
                }
                GetCheckboxcheckedValues(vmMentor, lstmqstartups, lstmqbusinessmodel, lstmqfunding, lstmqsoftware, lstmqlifesciences, lstmqindustryother, lstmqfunctionalareas);
                ViewBag.Ventureid = vmMentor.objMentor.VentureId;
                ViewBag.Roleid = GlobalVariables.RoleID;
                ViewBag.VentureName = GlobalVariables.FullName;
                TempData["Module"] = Module;
                //return View(cmpobject);
                return View(vmMentor);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/EditMentor";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpPost]
        public ActionResult EditMentor(MentorViewmodel mentorModel, FormCollection fc)
        {
            try
            {
                DateTime CreatedDate = DateTime.Now;
                mentorModel.objMentor.ModifiedDate = CreatedDate;
                mentorModel.objMentor.ModifiedBy = Convert.ToInt32(GlobalVariables.UserID);

                if (mentorModel.objMentor.MentorType != null)
                {
                    mentorModel.objMentor.MentorTypeId = Convert.ToInt32(mentorModel.objMentor.MentorType);
                }

                else
                {
                    mentorModel.objMentor.MentorTypeId = 0;
                }
                if (fc["objMentor.VentureId"] == "")
                {
                    mentorModel.objMentor.VentureId = 0;
                }

                if (fc["objMentor.StartDate"] != null && fc["objMentor.StartDate"].ToString() != string.Empty)
                {
                    DateTime dt1 = DateTime.ParseExact(fc["objMentor.StartDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    mentorModel.objMentor.StartDate = Convert.ToDateTime(dt1);
                }
                if (fc["objMentor.EndDate"] != null && fc["objMentor.EndDate"].ToString() != string.Empty)
                {
                    DateTime dt2 = DateTime.ParseExact(fc["objMentor.EndDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    mentorModel.objMentor.EndDate = Convert.ToDateTime(dt2); ;
                }

                if (mentorModel.objMentor.isHaverelevantExp != null)
                {
                    mentorModel.objMentor.HaverelevantExp = mentorModel.objMentor.isHaverelevantExp;
                }
                else
                {
                    mentorModel.objMentor.HaverelevantExp = false;
                }

                if (mentorModel.objMentor.isWillhelpifNeeded != null)
                {
                    mentorModel.objMentor.WillhelpifNeeded = mentorModel.objMentor.isWillhelpifNeeded;
                }
                else
                {
                    mentorModel.objMentor.WillhelpifNeeded = false;
                }
                if (mentorModel.objMentor.isInterested != null)
                {
                    mentorModel.objMentor.Interested = mentorModel.objMentor.isInterested;
                }
                else
                {
                    mentorModel.objMentor.Interested = false;
                }

                if (mentorModel.objMentor.isRecruited != null)
                {
                    mentorModel.objMentor.Recruited = mentorModel.objMentor.isRecruited;
                }
                else
                {
                    mentorModel.objMentor.Recruited = false;
                }
                if (mentorModel.objMentor.isConflictFinancialInterest != null)
                {
                    mentorModel.objMentor.Conflict_FinancialInterest = mentorModel.objMentor.isConflictFinancialInterest;
                }
                else
                {
                    mentorModel.objMentor.Conflict_FinancialInterest = false;
                }
                UpdateCheckBoxesMentorQuestionarrie(mentorModel, fc);

                mentorModel.objMentor.ModifiedBy = Convert.ToInt32(GlobalVariables.UserID);
                TempData["MUpdated"] = "MUpdated";
                
                Int64 ventureId = _iMentor.updateMentorDetails(mentorModel.objMentor, mentorModel.objmentordetail, mentorModel.objmentorquestionnary, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
               
                if (TempData["Module"].ToString() == "Venture")
                {
                    int ventureid = Convert.ToInt32(mentorModel.objMentor.VentureId);
                    return RedirectToAction("getVentureDetails", "Venture", new { @VentureId = ventureid, @Module = "Mentor" });
                }
                else
                {
                    return RedirectToAction("Index", "Mentor/");
                }


            }
            catch (Exception ex)
            {

                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/EditMentor";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpPost]
        public bool deleteMentor(string mentorIds, string Status)
        {
            bool flag = false;
            Mentor mentor = new Mentor();
            user objuser = new user();
            mentor.MentorId = Convert.ToInt64(mentorIds);
            mentor.Status = Status;
            mentor.ModifiedDate = DateTime.Now;
            mentor.ModifiedBy = Convert.ToInt32(GlobalVariables.UserID);
            try
            {
                var ClientEntity = new DevEntities();
                if (!string.IsNullOrEmpty(mentorIds))
                {
                    int result = _iMentor.DeleteMentorByMentorIdNew(mentor, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
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
                objErrorLog.ErrorPage = "Mentor/deleteMentor";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return false;
            }
            return flag;
        }

        [HttpPost]
        public JsonResult DeleteMentors(List<Mentor> MentorData)
        {
            bool flag = false;
            try
            {
                var ClientEntity = new DevEntities();
                for (int i = 0; i < MentorData.Count; i++)
                {
                    if (!string.IsNullOrEmpty(MentorData[i].MentorId.ToString()))
                    {
                        Mentor mentor = new Mentor();
                        user objuser = new user();
                        mentor.MentorId = Convert.ToInt64(MentorData[i].MentorId);
                        mentor.VmsStatus = MentorData[i].VmsStatus;
                        mentor.Status = MentorData[i].Status;
                       // mentor.Status = Status;
                        mentor.ModifiedDate = DateTime.Now;
                        mentor.ModifiedBy = Convert.ToInt32(GlobalVariables.UserID);
                        int result = _iMentor.DeleteMentorByMentorIdNew(mentor, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
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
                       // flag = _iMentor.DeleteMentorByMentorId(Convert.ToInt32(MentorData[i].MentorId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }
                }
                return Json(flag, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/DeleteMentors";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }

        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            try
            {
                List<Venture> lstVentures = _iMentor.getVenturePrefix(Prefix, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(lstVentures);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/AutoComplete";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }

        }

        public ActionResult getMentor(jQueryDataTableParamModel param)
        {
            try
            {

                TempData["MCreated"] = "";
                TempData["MUpdated"] = "";
                string Keyword_MentorName = Request["Keyword_MentorName"].TrimStart();
                string Module = Request["Module"];
                string ventureid = Request["Ventureid"];
                string AlphanumericSort = Request["Keyword_AlphabetLetter"].TrimStart();
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "mentor.VmsStatus desc";

                else
                    if (SortFieldName == "VentureName")
                    {
                        SortFieldName = "venture.VentureName";
                    }
                    else if (SortFieldName == "MentorType")
                    {
                        SortFieldName = "mType.MentorType";
                    }
                    else if (SortFieldName == "strStartDate")
                    {
                        SortFieldName = "mentor.StartDate";
                    }
                    else if (SortFieldName == "strEndDate")
                    {
                        SortFieldName = "mentor.EndDate";
                    }
                    else if (SortFieldName == "strRecruited")
                    {
                        SortFieldName = "mentor.Recruited";
                    }
                    else if (SortFieldName == "Status")
                    {
                        SortFieldName = "mentor.VmsStatus";
                    }
                orderByClause = SortFieldName + " " + sSortDir_0;
                int id = 0;
                if (Module == "Mentor")
                {
                    if (GlobalVariables.RoleID == "4")
                    {
                        if (GlobalVariables.MentorId != "")
                        {
                            id = Convert.ToInt32(GlobalVariables.MentorId);
                        }
                        else
                        {
                            id = 0;
                        }
                    }
                    if (GlobalVariables.RoleID == "2")
                    {
                        if (GlobalVariables.VentureId != "")
                        {
                            id = Convert.ToInt32(GlobalVariables.VentureId);
                        }
                        else
                        {
                            id = 0;
                        }
                    }
                    else if (GlobalVariables.RoleID == "1")
                    {
                        id = 0;
                    }
                }
                if (Module == "Venture")
                {
                    if (ventureid != "")
                    {
                        id = Convert.ToInt32(ventureid);
                    }

                }
                var lstMentors = _iMentor.GetMentorDetails(Keyword_MentorName, id, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName, GlobalVariables.RoleID, Module, AlphanumericSort);
                foreach (var item in lstMentors)
                {
                    if (item.Recruited == true)
                    {
                        item.strRecruited = "True";
                    }
                    else
                    {
                        item.strRecruited = "False";
                    }
                    if (item.StartDate == null)
                    {
                        item.strStartDate = "";
                    }
                    else {
                        item.strStartDate = Convert.ToDateTime(item.StartDate).ToString("MM/dd/yyyy");
                    }
                    if (item.EndDate == null)
                    {
                        item.strEndDate = "";
                    }
                    else {
                        item.strEndDate = Convert.ToDateTime(item.EndDate).ToString("MM/dd/yyyy");
                    }
                }
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = lstMentors,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/getMentor";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        public ActionResult IsMentorExists([Bind(Prefix = "objMentor.Name")]string Name)
        {
            string MentorName = Name;//string.Empty;
            try
            {
                //if (model != null)
                //{
                //    MentorName = model.objMentor.Name.Trim();
                //}
                bool objcompanyname = _iMentor.IsMentorExists(MentorName, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(objcompanyname, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/IsMentorExists";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        public ActionResult IsEmailExists(MentorViewmodel model)
        {
            string Email = string.Empty;
            try
            {
                if (model != null)
                {
                    Email = model.objMentor.Email.Trim();
                }
                bool objemail = _icommonrepository.IsEmailIdExists(Email, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(objemail, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/IsEmailExists";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        public async Task SendEmailToDigital55Users(string to, string username, string password)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                string subject = "Credentials of Mentor";
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
                objErrorLog.ErrorPage = "Mentor/SendEmailToDigital55Users";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                //return false;
            }
        }

        [HttpGet]
        public ActionResult PendingMentorDetails(Int64 Mentorid)
        {
            try
            {
                MentorViewmodel viewmentormodel = new MentorViewmodel();
                viewmentormodel.lststate = ListofState();
                viewmentormodel.lstcountry = ListofCountry();
                viewmentormodel.objmentorquestionnary = new MentorQuestionary();
                viewmentormodel.objmentordetail = new MentorDetail();
                viewmentormodel.objMentor = new Mentor();
                List<MQStartUps> lstmqstartups;
                List<MQBusinessModel> lstmqbusinessmodel;
                List<MQFunding> lstmqfunding;
                List<MQSoftware> lstmqsoftware;
                List<MQLifeSciences> lstmqlifesciences;
                List<MQIndustryOther> lstmqindustryother;
                List<MQFunctionalAreas> lstmqfunctionalareas;
                CheckBoxDataforMentorQuestionnaire(out  lstmqstartups, out lstmqbusinessmodel, out lstmqfunding, out lstmqsoftware, out  lstmqlifesciences,
                    out lstmqindustryother, out lstmqfunctionalareas);
                viewmentormodel.objMentor.VmsStatus = 1;
                viewmentormodel.objmentordetail = _iMentor.GetPendingMentorDetailsByID(Mentorid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (viewmentormodel.objmentordetail == null)
                {
                    viewmentormodel.objmentordetail = new MentorDetail();
                }
                else
                {
                    if (viewmentormodel.objmentordetail.Resume != null && viewmentormodel.objmentordetail.Resume != "")
                    {
                        string[] resumefile = viewmentormodel.objmentordetail.Resume.Split('\\');
                        int count = resumefile.Count();
                        var getnamewithoutextn = System.IO.Path.GetFileNameWithoutExtension(resumefile[count - 1]);
                        viewmentormodel.objmentordetail.ResumeName = getnamewithoutextn;
                        
                    }
                    else{
                        viewmentormodel.objmentordetail.ResumeName = "";
                    }
                    
                }
                viewmentormodel.objmentorquestionnary = _iMentor.GetMentorQuestionaryByMID(Mentorid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (viewmentormodel.objmentorquestionnary == null)
                {
                    viewmentormodel.objmentorquestionnary = new MentorQuestionary();
                }
                GetCheckboxcheckedValues(viewmentormodel, lstmqstartups, lstmqbusinessmodel, lstmqfunding, lstmqsoftware, lstmqlifesciences, lstmqindustryother, lstmqfunctionalareas);
                return View(viewmentormodel);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/PendingMentorDetails";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpPost]

        public ActionResult ConvertactiveMentor(MentorViewmodel mentorviewmodel, FormCollection fc)
        {
            try
            {
                TempData["CreateCompany"] = "UpdateMentor";
                /// New Company Details Insertion
                UpdateCheckBoxesMentorQuestionarrie(mentorviewmodel, fc);
                mentorviewmodel.objMentor.StartDate = DateTime.Now; 
                mentorviewmodel.objMentor.ModifiedBy = Convert.ToInt32(GlobalVariables.UserID);
                Int32 mentordetid = _iMentor.updatePendingMentorDetails(mentorviewmodel.objMentor, mentorviewmodel.objmentordetail, mentorviewmodel.objmentorquestionnary, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                user objuser = _iuserrepo.GetUserDetailsbyUserID(mentordetid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                var password = EncryptDecrypt.Decrypt(objuser.Password);
                // SendEmailToDigital55Users(objuser.Email, objuser.LoginId, password.ToString());
                TempData["VUpdated"] = "MUpdated";
                return RedirectToAction("Index", "Mentor/");
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

        public List<usState> ListofState()
        {
            try
            {
                List<usState> objliststates = new List<usState>();
                objliststates = _icommonrepository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return objliststates;
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/ListofState";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return new List<usState>();
            }
        }

        public List<Country> ListofCountry()
        {
            try
            {
                List<Country> objlistcountry = new List<Country>();
                objlistcountry = _icommonrepository.GetAllCountriesList(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return objlistcountry;
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/ListofCountry";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return new List<Country>();
            }
        }


        private static void CheckBoxDataforMentorQuestionnaire(out List<MQStartUps> lstmqstartups, out List<MQBusinessModel> lstmqbusinessmodel,
            out List<MQFunding> lstmqfunding, out List<MQSoftware> lstmqsoftware, out List<MQLifeSciences> lstmqlifesciences, out List<MQIndustryOther> lstmqindustryother,
            out List<MQFunctionalAreas> lstmqfunctionalareas)
        {
            try
            {


                lstmqstartups = new List<MQStartUps>()
            {
                 
                new MQStartUps {Text="First Steps",      Value="First Steps", ID=1},
                new MQStartUps {Text="Founders Issues",  Value="Founders Issues",ID=2 },
                new MQStartUps {Text="Team Formation",        Value="Team Formation",ID=3 },
                new MQStartUps {Text="IP / Licensing Issues + Strategy",      Value="IP / Licensing Issues + Strategy",ID=4 },
                
            };

                lstmqbusinessmodel = new List<MQBusinessModel>()
               {
                new MQBusinessModel {Text="Product - Market Fit", Value="Product - Market Fit", ID=1},
                new MQBusinessModel {Text="Distribuiton Channels",    Value="Distribuiton Channels",ID=2 },
                new MQBusinessModel {Text="Customer Acquisition",       Value="Customer Acquisition",ID=3 },
                new MQBusinessModel {Text="Revenue Streams and Strategies",     Value="Revenue Streams and Strategies",ID=4 },
                new MQBusinessModel {Text="Market Research / Competitive Analysys", Value="Market Research / Competitive Analysys",ID=5 },
                new MQBusinessModel {Text="Business Model Validation", Value="Business Model Validation",ID=6 },
                 new MQBusinessModel {Text="Review Of Financials", Value="Review Of Financials",ID=7 }
               };
                lstmqfunding = new List<MQFunding>()
               {
                new MQFunding {Text="Angels", Value="Angels", ID=1},
                new MQFunding {Text="VCs", Value="VCs",ID=2 },
                new MQFunding {Text="Crowdfunding", Value="Crowdfunding",ID=3 },
                new MQFunding {Text="SBIR / Other grants", Value="SBIR / Other grants",ID=4 },
                new MQFunding {Text="Bootstrapping", Value="Bootstrapping",ID=5 },
                new MQFunding {Text="TeamSheets: Review + negotiation", Value="TeamSheets: Review + negotiation",ID=6 },
                new MQFunding {Text="Board Formation", Value="Board Formation",ID=7 },
               };

                lstmqsoftware = new List<MQSoftware>()
                {
                new MQSoftware {Text="Enterprise", Value="Enterprise", ID=1},
                new MQSoftware {Text="Cloud Computing", Value="Cloud Computing",ID=2 },
                new MQSoftware {Text="Consumer", Value="Consumer",ID=3 },
                new MQSoftware {Text="Web/Mobile", Value="Web/Mobile",ID=4 },
                
                };

                lstmqlifesciences = new List<MQLifeSciences>()
                {
                new MQLifeSciences {Text="Pharma", Value="Pharma", ID=1},
                new MQLifeSciences {Text="Medical Device", Value="Medical Device",ID=2 },
                new MQLifeSciences {Text="Software (incl healthcare IT)", Value="Software (incl healthcare IT)",ID=3 },
                new MQLifeSciences {Text="Other Biotech", Value="Other Biotech",ID=4 },
                };

                lstmqindustryother = new List<MQIndustryOther>()
                {
                new MQIndustryOther {Text="Hardware - Computer / Electronics", Value="Hardware - Computer / Electronics", ID=1},
                new MQIndustryOther {Text="Process Industries / Manufacturing", Value="Process Industries / Manufacturing", ID=2},
                new MQIndustryOther {Text="Communications (data, telecom, wireless)", Value="Communications (data, telecom, wireless)",ID=3 },
                new MQIndustryOther {Text="Media and Entertainment", Value="Media and Entertainment",ID=4 },
                new MQIndustryOther {Text="Energy", Value="Energy",ID=5 },
                new MQIndustryOther {Text="Services", Value="Services",ID=6 }
                };
                lstmqfunctionalareas = new List<MQFunctionalAreas>()
                {
                new MQFunctionalAreas {Text="Startup Founder / Team member", Value="Startup Founder / Team member", ID=1},
                new MQFunctionalAreas {Text="General Management", Value="General Management", ID=2},
                new MQFunctionalAreas {Text="Product Development", Value="Product Development",ID=3 },
                new MQFunctionalAreas {Text="Sales", Value="Sales",ID=4 },
                new MQFunctionalAreas {Text="Marketing", Value="Marketing",ID=5 },
                new MQFunctionalAreas {Text="Finance", Value="Finance",ID=6 },
                 new MQFunctionalAreas {Text="Operations", Value="Operations",ID=7 },
                new MQFunctionalAreas {Text="Manufacturing", Value="Manufacturing",ID=8 },
                new MQFunctionalAreas {Text="Legal", Value="Legal",ID=9 }
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GetCheckboxcheckedValues(MentorViewmodel mentorviewmodel, List<MQStartUps> lstmqstartups, List<MQBusinessModel> lstmqbusinessmodel,
            List<MQFunding> lstmqfunding, List<MQSoftware> lstmqsoftware, List<MQLifeSciences> lstmqlifesciences, List<MQIndustryOther> lstmqindustryother,
            List<MQFunctionalAreas> lstmqfunctionalareas)
        {
            try
            {

                if (mentorviewmodel.objmentorquestionnary.Startups != null)
                {

                    string[] startups = mentorviewmodel.objmentorquestionnary.Startups.ToString().Split(',');
                    foreach (var item in startups)
                    {
                        foreach (var chkval in lstmqstartups)
                        {
                            if (item == chkval.Value)
                            {
                                chkval.IsChecked = true;
                            }
                        }
                    }
                }
                mentorviewmodel.listofMQStartUps = lstmqstartups;

                if (mentorviewmodel.objmentorquestionnary.BusinessModel != null)
                {

                    string[] businessmodel = mentorviewmodel.objmentorquestionnary.BusinessModel.ToString().Split(',');
                    foreach (var item in businessmodel)
                    {
                        foreach (var chkval in lstmqbusinessmodel)
                        {
                            if (item == chkval.Value)
                            {
                                chkval.IsChecked = true;
                            }
                        }
                    }
                }
                mentorviewmodel.listofMQBusinessModel = lstmqbusinessmodel;

                if (mentorviewmodel.objmentorquestionnary.Funding != null)
                {

                    string[] funding = mentorviewmodel.objmentorquestionnary.Funding.ToString().Split(',');
                    foreach (var item in funding)
                    {
                        foreach (var chkval in lstmqfunding)
                        {
                            if (item == chkval.Value)
                            {
                                chkval.IsChecked = true;
                            }
                        }
                    }
                }
                mentorviewmodel.listofMQFunding = lstmqfunding;


                if (mentorviewmodel.objmentorquestionnary.Software != null)
                {

                    string[] software = mentorviewmodel.objmentorquestionnary.Software.ToString().Split(',');
                    foreach (var item in software)
                    {
                        foreach (var chkval in lstmqsoftware)
                        {
                            if (item == chkval.Value)
                            {
                                chkval.IsChecked = true;
                            }
                        }
                    }
                }
                mentorviewmodel.listofMQSoftware = lstmqsoftware;

                if (mentorviewmodel.objmentorquestionnary.LifeSciences != null)
                {

                    string[] lifesciences = mentorviewmodel.objmentorquestionnary.LifeSciences.ToString().Split(',');
                    foreach (var item in lifesciences)
                    {
                        foreach (var chkval in lstmqlifesciences)
                        {
                            if (item == chkval.Value)
                            {
                                chkval.IsChecked = true;
                            }
                        }
                    }
                }
                mentorviewmodel.listofMQLifeSciences = lstmqlifesciences;

                if (mentorviewmodel.objmentorquestionnary.IndustryOther != null)
                {

                    string[] industryother = mentorviewmodel.objmentorquestionnary.IndustryOther.ToString().Split(',');
                    foreach (var item in industryother)
                    {
                        foreach (var chkval in lstmqindustryother)
                        {
                            if (item == chkval.Value)
                            {
                                chkval.IsChecked = true;
                            }
                        }
                    }
                }
                mentorviewmodel.listofMQIndustryOther = lstmqindustryother;

                if (mentorviewmodel.objmentorquestionnary.FunctionalAreas != null)
                {

                    string[] functionalareas = mentorviewmodel.objmentorquestionnary.FunctionalAreas.ToString().Split(',');
                    foreach (var item in functionalareas)
                    {
                        foreach (var chkval in lstmqfunctionalareas)
                        {
                            if (item == chkval.Value)
                            {
                                chkval.IsChecked = true;
                            }
                        }
                    }
                }
                mentorviewmodel.listofMQFunctionalAreas = lstmqfunctionalareas;

            }

            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/GetCheckboxcheckedValues";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                
            }
        }

        public void UpdateCheckBoxesMentorQuestionarrie(MentorViewmodel mentorviewmodel, FormCollection fc)
        {
            try
            {


                string[] startups = fc["chkstartups"].ToString().Split(',');

                string startups1 = "";

                foreach (var item in startups)
                {
                    if (item != "false")
                    {
                        startups1 = startups1 + "," + item;
                    }

                }

                if (startups1.Contains(","))
                {
                    startups1 = startups1.Remove(0, 1);
                }
                mentorviewmodel.objmentorquestionnary.Startups = startups1;

                string[] businessmodel = fc["chkbusinessmodel"].ToString().Split(',');

                string businessmodel1 = "";

                foreach (var item in businessmodel)
                {
                    if (item != "false")
                    {

                        businessmodel1 = businessmodel1 + "," + item;
                    }

                }

                if (businessmodel1.Contains(","))
                {
                    businessmodel1 = businessmodel1.Remove(0, 1);
                }
                mentorviewmodel.objmentorquestionnary.BusinessModel = businessmodel1;

                string[] funding = fc["chkfunding"].ToString().Split(',');
                string funding1 = "";
                foreach (var item in funding)
                {
                    if (item != "false")
                    {

                        funding1 = funding1 + "," + item;
                    }

                }

                if (funding1.Contains(","))
                {
                    funding1 = funding1.Remove(0, 1);
                }
                mentorviewmodel.objmentorquestionnary.Funding = funding1;


                string[] software = fc["chksoftware"].ToString().Split(',');
                string software1 = "";

                foreach (var item in software)
                {
                    if (item != "false")
                    {

                        software1 = software1 + "," + item;
                    }
                }


                if (software1.Contains(","))
                {
                    software1 = software1.Remove(0, 1);
                }
                mentorviewmodel.objmentorquestionnary.Software = software1;


                string[] lifesciences = fc["chklifesciences"].ToString().Split(',');
                string lifesciences1 = "";

                foreach (var item in lifesciences)
                {
                    if (item != "false")
                    {

                        lifesciences1 = lifesciences1 + "," + item;
                    }
                }

                if (lifesciences1.Contains(","))
                {
                    lifesciences1 = lifesciences1.Remove(0, 1);
                }
                mentorviewmodel.objmentorquestionnary.LifeSciences = lifesciences1;

                string[] industryother = fc["chkindustryother"].ToString().Split(',');
                string industryother1 = "";

                foreach (var item in industryother)
                {
                    if (item != "false")
                    {

                        industryother1 = industryother1 + "," + item;
                    }
                }

                if (industryother1.Contains(","))
                {
                    industryother1 = industryother1.Remove(0, 1);
                }
                mentorviewmodel.objmentorquestionnary.IndustryOther = industryother1;


                string[] functionalareas = fc["chkfunctionalareas"].ToString().Split(',');
                string functionalareas1 = "";

                foreach (var item in functionalareas)
                {
                    if (item != "false")
                    {

                        functionalareas1 = functionalareas1 + "," + item;
                    }
                }

                if (functionalareas1.Contains(","))
                {
                    functionalareas1 = functionalareas1.Remove(0, 1);
                }
                mentorviewmodel.objmentorquestionnary.FunctionalAreas = functionalareas1;

            }
            catch (Exception ex)
            {

                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/UpdateCheckBoxesMentorQuestionarrie";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
               
            }
        }

        public JsonResult SendQuestionarie(string Mentorid, string primaryemail, string vmsstatus)
        {
            try
            {

                SendEmailToDigital55UsersForMentoringQuestionarie(primaryemail, Convert.ToInt64(Mentorid));
                ////if (vmsstatus == "1")
                ////{
                //    return RedirectToAction("PendingMentorDetails", "Mentor", new { @MentorId = Convert.ToInt64(Mentorid), Module = "Mentor" });
                ////}
                ////else
                ////{
                ////    return RedirectToAction("EditMentor", "Mentor", new { @MentorId = Convert.ToInt64(Mentorid), Module = "Mentor" });
                ////}
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/SendQuestionarie";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                //return RedirectToAction("PendingMentorDetails", "Mentor", new { @MentorId = Convert.ToInt64(Mentorid), Module = "Mentor" });
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        public async Task SendEmailToDigital55UsersForMentoringQuestionarie(string to, Int64 mentorid)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                string questionurl = ConfigurationManager.AppSettings["QuestionaryUrl"].ToString();
                // fromEmail = "support@priyanet.com";
                string subject = " Mentoring Questionnarie Form";
                string MessageBody = "";
                MessageBody = "<html><body><table><tr><td>The following is  Mentoring Questionnarie Form </td></tr> " +
                              "<tr><td><a href=" + questionurl + mentorid + ">Click here for the Mentoring Questionnarie Form</a></td></tr>" +
                               " </table> </body></html>";
                SendEmail objEmail = new SendEmail();
                await Task.Run(() => objEmail.SendEmailasync(fromEmail, to, subject, MessageBody));

            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Mentor/SendEmailToDigital55UsersForMentoringQuestionarie";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
               
            }
        }
    }
}