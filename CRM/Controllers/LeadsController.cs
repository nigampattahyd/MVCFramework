using CRMHub.EntityFramework;
using CRMHub.ExtendedProperties;
using CRMHub.Interface;
using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CRM.Controllers
{
    public class LeadsController : ControllerBase
    {
        private readonly IUserRepository _iuserrepository;
        private readonly IRoleRepository _irolerepository;
        private readonly ILeadRepository _ileadrepository;
        private readonly IContactsRepository _icontactsrepository;
        private readonly ICommonRepository _icommonrepository;
        private readonly ICompanyRepository _icompanyrepository;
        private readonly ICustomFieldsRepository _icustomfieldrepository;
        private readonly IDashboardData _iDashboardDatarepository;
        private readonly IOpportunitiesRepository _opportunitiesRepo;

        public LeadsController(IUserRepository iuserrepository, IRoleRepository irolerepository, ILeadRepository ileadrepository, IContactsRepository icontactsrepository, ICommonRepository icommonrepository, ICompanyRepository icompanyrepository, ICustomFieldsRepository icustomfieldrepository, IDashboardData idashboardData,IOpportunitiesRepository iopportunityrepo)
        {
            _iuserrepository = iuserrepository;
            _irolerepository = irolerepository;
            _ileadrepository = ileadrepository;
            _icontactsrepository = icontactsrepository;
            _icommonrepository = icommonrepository;
            _icompanyrepository = icompanyrepository;
            _icustomfieldrepository = icustomfieldrepository;
            _iDashboardDatarepository = idashboardData;
            _opportunitiesRepo = iopportunityrepo;
        }

        //
        // GET: /Leads/
        public ActionResult Index()
        {
            try
            {
                if (TempData["CreateLeads"] != null)
                {
                    ViewBag.CreateLeads = "CreateLeads";

                }
                if (TempData["ConvertLead"] != null)
                {
                    ViewBag.ConvertLead = "ConvertLead";

                }
               // TempData["ConvertLead"]

              //  TempData["CreateLeads"]
                return View();

                //string uricont = this.Request.UrlReferrer.AbsolutePath;
                //Session["urlead"] = uricont;
                //LeadModel objLeadModel = new LeadModel();
                //Branch selectOption = new Branch();
                //selectOption.BranchId = -1;
                //selectOption.BranchName = "--Select All--";
                //objLeadModel.branchList = new List<Branch>();
                //objLeadModel.branchList.Add(selectOption);
                //objLeadModel.branchList.AddRange(_iuserrepository.getUserbranchlist(GlobalVariables.UserID, GlobalVariables.RoleID, GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                //user selectOptionUser = new user();
                //selectOptionUser.FirstName = "Select All";
                //selectOptionUser.UserId = -1;
                //objLeadModel.userList = new List<user>();
                //objLeadModel.userList.Add(selectOptionUser);
                //objLeadModel.userList.AddRange(_iuserrepository.getAdminList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                //objLeadModel.industryList = new List<activity_types>();
                //objLeadModel.IndustriesList = (_ileadrepository.GetIndustries(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                //return View(objLeadModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult GetallLeads(jQueryDataTableParamModel param)
        {
            try
            {
                // int totalPageCount = 0;
                string Leadkeyword = Request["Leadkeyword"].TrimStart();
                //string CompanyName = Request["CompanyName"].TrimStart();
                string OwnerName = Request["OwnerName"].TrimStart();
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "ID desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                string CompanyName = "";
                //int CompanyId = -1;
                //if (CompanyName != "")
                //{
                //    CompanyName = CompanyName.Replace(" ", "").ToLower();
                //    //CompanyId = _icommonrepository.GetcompanyIdbyName(CompanyName, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //}

                int userId = -1;
                if (OwnerName != "")
                {
                    int guserid = 0;
                    string Ownerusr = OwnerName.Replace(" ", "").ToLower();
                    userId = _icommonrepository.GetUserIdFrmNamepName(Ownerusr, out guserid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                }


                List<Account> leadlist = _ileadrepository.GetAllAccountLeads(Leadkeyword, CompanyName, userId,param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int l = 0; l < leadlist.Count; l++)
                {
                    leadlist[l].LeadName = leadlist[l].FirstName + " " + leadlist[l].LastName;
                }
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = leadlist,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpGet]
        //public ActionResult CreateLead()
        //{
        //    try
        //    {
        //        string Module = "leads";
        //        LeadModel objLeadModel = new LeadModel();
        //        Branch selectOption = new Branch();
        //        selectOption.BranchId = -1;
        //        selectOption.BranchName = "--Select All--";
        //        objLeadModel.branchList = new List<Branch>();
        //        objLeadModel.branchList.Add(selectOption);
        //        objLeadModel.branchList.AddRange(_iuserrepository.getUserbranchlist(GlobalVariables.UserID, GlobalVariables.RoleID, GlobalVariables.ConnectionString, GlobalVariables.ClientName));

        //        user selectOptionUser = new user();
        //        selectOptionUser.FirstName = "Select All";
        //        selectOptionUser.UserId = -1;
        //        objLeadModel.userList = new List<user>();
        //        objLeadModel.userList.Add(selectOptionUser);
        //        objLeadModel.userList.AddRange(_iuserrepository.getAdminList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        objLeadModel.industryList = new List<activity_types>();
        //        objLeadModel.IndustriesList = (_ileadrepository.GetIndustries(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        objLeadModel.stateList = _icommonrepository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        // objLeadModel.listCustomfields = _icustomfieldrepository.GetCustomFieldsByModule(Module, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        objLeadModel.listCustomfields = _icustomfieldrepository.GetCustomFieldsByModule(Module, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //        return View(objLeadModel);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        // [ValidateInput(false)]
        //[HttpPost]
        //public ActionResult CreateLead(LeadModel leadModel)
        //{
        //    try
        //    {
        //        leadModel.leadEntity.CreatedBy = Convert.ToInt32(GlobalVariables.UserID);

        //        bool CFSuccess = false;

        //        int result = _ileadrepository.insertLead(leadModel.leadEntity, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        for (int i = 0; i < leadModel.listCustomfields.Count; i++)

        //            if (leadModel.listCustomfields[i].Column_Type == "radiobutton")
        //            {
        //                Custom_Value_Master CVItem = new Custom_Value_Master();
        //                string SelectedRadiobtnId = leadModel.listCustomfields[i].DefaultValue;

        //                for (int a = 0; a < leadModel.listCustomfields[i].lstCustomOptions.Count; a++)
        //                {

        //                    if (leadModel.listCustomfields[i].lstCustomOptions[a].DrpValueId.ToString() == SelectedRadiobtnId)
        //                    {
        //                        leadModel.listCustomfields[i].lstCustomOptions[a].IsDefault = Convert.ToBoolean(1);
        //                        CVItem.CustomFieldvalue = SelectedRadiobtnId;
        //                        CVItem.CreatedDate = DateTime.Now;
        //                        CVItem.ModifiedDate = DateTime.Now;
        //                        CVItem.Module = "Leads";
        //                        CVItem.MasterFieldId = leadModel.listCustomfields[i].FieldId;
        //                        CVItem.UserID = Convert.ToInt32(GlobalVariables.UserID);
        //                        CVItem.ModuleRecordIdTmp = result;

        //                        CFSuccess = _icustomfieldrepository.InsertCustomField(CVItem, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                    }
        //                    else
        //                    {
        //                        leadModel.listCustomfields[i].lstCustomOptions[a].IsDefault = Convert.ToBoolean(0);
        //                    }
        //                    // send drpId and Update Isdefault value ;/ Have to Update the Custom_Manage_Master with DefaultValue With CustomFieldValue
        //                    // _icustomfieldrepository.UpdateCustomOptionsForCheckBox(cmpModel.listCustomfields[i].lstCustomOptions[a], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                }
        //            }

        //            else
        //            {
        //                Custom_Value_Master CVItem = new Custom_Value_Master();
        //                CVItem.CreatedDate = DateTime.Now;
        //                CVItem.ModifiedDate = DateTime.Now;
        //                CVItem.Module = "Leads";
        //                CVItem.MasterFieldId = leadModel.listCustomfields[i].FieldId;
        //                CVItem.UserID = Convert.ToInt32(GlobalVariables.UserID);
        //                // CVItem.ModuleRecordId = result;
        //                CVItem.ModuleRecordIdTmp = result;
        //                CVItem.CustomFieldvalue = leadModel.listCustomfields[i].DefaultValue;

        //                CFSuccess = _icustomfieldrepository.InsertCustomField(CVItem, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //            }

        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet]
        //public ActionResult EditLead(Int64 LeadId)
        //{
        //    try
        //    {
        //        string urilead = this.Request.UrlReferrer.AbsolutePath;
        //        Session["urilead"] = urilead;
        //        string Module = "leads";

        //        LeadModel objLeadModel = new LeadModel();
        //        TempData["LeadID"] = LeadId;
        //        TempData.Keep("LeadID");
        //        Session["Leadid"] = LeadId;
        //        int Ldid = Convert.ToInt32(LeadId);
        //        objLeadModel.listCustomfields = _icustomfieldrepository.GetCustomFieldsByModule(Module, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        Branch selectOption = new Branch();
        //        selectOption.BranchId = -1;
        //        selectOption.BranchName = "--Select All--";
        //        objLeadModel.branchList = new List<Branch>();
        //        objLeadModel.branchList.Add(selectOption);
        //        objLeadModel.branchList.AddRange(_iuserrepository.getUserbranchlist(GlobalVariables.UserID, GlobalVariables.RoleID, GlobalVariables.ConnectionString, GlobalVariables.ClientName));

        //        user selectOptionUser = new user();
        //        selectOptionUser.FirstName = "Select All";
        //        selectOptionUser.UserId = -1;
        //        objLeadModel.userList = new List<user>();
        //        objLeadModel.userList.Add(selectOptionUser);
        //        objLeadModel.userList.AddRange(_iuserrepository.getAdminList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        objLeadModel.IndustriesList = (_ileadrepository.GetIndustries(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        decimal leadid = Convert.ToDecimal(LeadId);
        //        objLeadModel.stateList = _icommonrepository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        objLeadModel.leadEntity = _ileadrepository.getLeadByLeadId(Convert.ToDecimal(LeadId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        TempData["AccountName"] = objLeadModel.leadEntity.AccountName;

        //        activity_types selectoption = new activity_types();
        //        selectoption.Name = "Select";
        //        selectoption.Aid = -1;
        //        objLeadModel.leadactivity = new List<activity_types>();
        //        objLeadModel.leadactivity.Add(selectoption);
        //        objLeadModel.leadactivity.AddRange(_ileadrepository.GetLeadActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        //objLeadModel.customVM = _icustomfieldrepository.getCustomDetailsByLeadId(LeadId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        objLeadModel.lstcustomVM = _icustomfieldrepository.GetCustomValueBasedonModule(Ldid, Module, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        if (TempData["Leadactivity"] != null)
        //        {
        //            ViewBag.showActivitiesleadtab = "leadid";
        //        }
        //        if (TempData["EditLeadactivity"] != null)
        //        {
        //            ViewBag.EditActivitiesleadtab = "EditLeadId";

        //        }

        //        return View("EditLead", objLeadModel);
        //        // return View(objLeadModel);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public ActionResult getLeadActivity(jQueryDataTableParamModel param)
        //{
        //    try
        //    {
        //        string LeadKeyword = Request["LeadKeyword"];
        //        string LeadType = Request["LeadType"];
        //        string LeadPriority = Request["LeadPriority"];
        //        string LeadStatus = Request["LeadStatus"];
        //        string iSortCol_0 = Request["iSortCol_0"];
        //        string sSortDir_0 = Request["sSortDir_0"];
        //        string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
        //        string orderByClause = string.Empty;
        //        string LeadId = Convert.ToString(TempData["LeadID"]);
        //        TempData.Keep("LeadId");
        //        string NewActLeadId = LeadId;
        //        TempData["NewActLeadId"] = LeadId;
        //        int totalPageCount = 0;
        //        if (iSortCol_0 == "0")
        //            orderByClause = "LeadhistoryId desc";
        //        else
        //            orderByClause = SortFieldName + " " + sSortDir_0;
        //        //orderByClause = SortFieldName + " " + sSortDir_0;
        //        var listActivity = _ileadrepository.GetCrmLeadActivities(LeadId, LeadKeyword == "" ? null : LeadKeyword, LeadType == "Select" ? "All" : LeadType, LeadPriority == "select" ? "All" : LeadPriority, LeadStatus == "select" ? "All" : LeadStatus, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        updatenotification();
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            aaData = listActivity,
        //            iTotalRecords = totalPageCount,
        //            iTotalDisplayRecords = totalPageCount
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[ValidateInput(false)]
        //[HttpPost]
        //public ActionResult UpdateLead(LeadModel leadModel)
        //{
        //    try
        //    {
        //        leadModel.leadEntity.ModifiedBy = Convert.ToInt32(GlobalVariables.UserID);
        //        TempData["AccountName"] = leadModel.leadEntity.AccountName;
        //        int result = _ileadrepository.updateLeadByLeadId(leadModel.leadEntity, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //        for (int i = 0; i < leadModel.lstcustomVM.Count; i++)
        //        {


        //            if (leadModel.lstcustomVM[i].Column_Type == "radiobutton")
        //            {
        //                string SelectedRadiobtnId = leadModel.lstcustomVM[i].CustomFieldvalue;

        //                for (int a = 0; a < leadModel.lstcustomVM[i].lstCustomOptionsVal.Count; a++)
        //                {
        //                    if (leadModel.lstcustomVM[i].CValueId != 0)
        //                    {
        //                        if (leadModel.lstcustomVM[i].lstCustomOptionsVal[a].DrpValueId.ToString() == SelectedRadiobtnId)
        //                        {
        //                            leadModel.lstcustomVM[i].lstCustomOptionsVal[a].IsDefault = Convert.ToBoolean(1);
        //                            leadModel.lstcustomVM[i].ModifiedDate = DateTime.Now;
        //                            leadModel.lstcustomVM[i].CustomFieldvalue = SelectedRadiobtnId;
        //                            _icustomfieldrepository.UpdateCustomField(leadModel.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                        }



        //                        else
        //                        {
        //                            leadModel.lstcustomVM[i].lstCustomOptionsVal[a].IsDefault = Convert.ToBoolean(0);
        //                        }

        //                    }
        //                    else
        //                    {
        //                        leadModel.lstcustomVM[i].CreatedDate = DateTime.Now;
        //                        leadModel.lstcustomVM[i].ModifiedDate = DateTime.Now;
        //                        leadModel.lstcustomVM[i].Module = "leads";
        //                        leadModel.lstcustomVM[i].ModuleRecordIdTmp = leadModel.leadEntity.LeadId;
        //                        leadModel.lstcustomVM[i].MasterFieldId = leadModel.lstcustomVM[i].FieldId;


        //                        leadModel.lstcustomVM[i].lstCustomOptionsVal[a].IsDefault = Convert.ToBoolean(1);
        //                        //CompanyModel.lstcustomVM[i].ModifiedDate = DateTime.Now;
        //                        leadModel.lstcustomVM[i].CustomFieldvalue = SelectedRadiobtnId;
        //                        _icustomfieldrepository.InsertCustomField(leadModel.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                    }


        //                }
        //            }
        //            else
        //            {
        //                leadModel.lstcustomVM[i].UserID = Convert.ToInt32(GlobalVariables.UserID);
        //                if (leadModel.lstcustomVM[i].CValueId != 0) // Update
        //                {
        //                    leadModel.lstcustomVM[i].ModifiedDate = DateTime.Now;
        //                    _icustomfieldrepository.UpdateCustomField(leadModel.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                }

        //                else // Insert 
        //                {
        //                    leadModel.lstcustomVM[i].CreatedDate = DateTime.Now;
        //                    leadModel.lstcustomVM[i].ModifiedDate = DateTime.Now;
        //                    leadModel.lstcustomVM[i].Module = "leads";
        //                    leadModel.lstcustomVM[i].ModuleRecordIdTmp = leadModel.leadEntity.LeadId;
        //                    leadModel.lstcustomVM[i].MasterFieldId = leadModel.lstcustomVM[i].FieldId;
        //                    _icustomfieldrepository.InsertCustomField(leadModel.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                }

        //            }

        //        }

        //        // return RedirectToAction("EditLead", new { leadModel.leadEntity.LeadId });
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public bool DeleteLeads(string leadid)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(leadid))
        //        {
        //            foreach (var leadids in leadid.Split(','))
        //            {
        //                var deleteOppor = _ileadrepository.DeleteLeadsDetailsByLeadId(Int64.Parse(leadids), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public bool ConvertToOpportunity(string convtoOppr)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(convtoOppr))
        //        {
        //            foreach (var ConvLeadids in convtoOppr.Split(','))
        //            {
        //                var ConvLeadToOpp = _ileadrepository.ConvertToOpportunity(Int64.Parse(convtoOppr), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet]
        //public ActionResult NewLeadActivity()
        //{
        //    try
        //    {

        //        string getleadulpth = this.Request.UrlReferrer.AbsoluteUri;
        //        var leaduri = new Uri(getleadulpth);
        //        Session["editleadActvityurl"] = leaduri;
        //        tbl_Leadhistory leadhist = new tbl_Leadhistory();
        //        leadhist.Leadid = Convert.ToInt64(TempData["NewActLeadId"]);
        //        activity_types selectoption = new activity_types();
        //        selectoption.Name = "Select";
        //        selectoption.Aid = -1;
        //        leadhist.TypeList = new List<activity_types>();
        //        leadhist.TypeList.Add(selectoption);
        //        leadhist.TypeList.AddRange(_ileadrepository.GetLeadActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

        //        List<user> lstSales = _icompanyrepository.GetAdmin(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        leadhist.userList = lstSales;


        //        return View(leadhist);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ActionResult NewLeadActivity(tbl_Leadhistory leadhist, FormCollection fc, HttpPostedFileBase uploadFile)
        //{
        //    try
        //    {
        //        int result = 0;
        //        TempData["Leadactivity"] = "EditLeadId";
        //        string leadurlpath = "";
        //        if (Session["urlead"] != null)
        //        {

        //            string leaduipth = Session["editleadActvityurl"].ToString();
        //            leadurlpath = leaduipth;
        //        }

        //        if (uploadFile != null && uploadFile.ContentLength > 0)
        //        {
        //            string filePath = Path.Combine(HttpContext.Server.MapPath("~/Content/Files"),
        //            Path.GetFileName(uploadFile.FileName));
        //            uploadFile.SaveAs(filePath);
        //            leadhist.AccountName = Convert.ToString(TempData["AccountName"]);
        //            leadhist.ActivityAttachment = uploadFile.FileName;
        //            TempData["File"] = leadhist.ActivityAttachment;
        //            leadhist.CreatedBy = Convert.ToInt32(GlobalVariables.UserID);
        //            leadhist.CreatedDate = DateTime.Now;
        //            // leadhist.AssignedTo = Convert.ToString(GlobalVariables.UserID);
        //            leadhist.NotificationFlag = false;
        //            if (leadhist.Leadid != null)
        //            {
        //                result = _ileadrepository.createLeadNewActivity_Insert(leadhist, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                //string result = _ileadrepository.createLeadNewActivity(leadhist, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //            }
        //            //return RedirectToAction("Index");
        //            // return Redirect(leadurlpath);
        //        }
        //        else
        //        {
        //            activity_types selectoption = new activity_types();
        //            selectoption.Name = "Select";
        //            leadhist.TypeList = new List<activity_types>();
        //            leadhist.TypeList.Add(selectoption);
        //            leadhist.TypeList.AddRange(_ileadrepository.GetLeadActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //            leadhist.CreatedBy = Convert.ToInt32(GlobalVariables.UserID);
        //            leadhist.CreatedDate = DateTime.Now;
        //            // leadhist.AssignedTo = Convert.ToString(GlobalVariables.UserID);
        //            leadhist.NotificationFlag = false;
        //            if (leadhist.Leadid != null)
        //            {
        //                result = _ileadrepository.createLeadNewActivity_Insert(leadhist, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                //string result = _ileadrepository.createLeadNewActivity(leadhist, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //            }
        //            //return View(leadhist);
        //            //return Redirect(leadurlpath);
        //        }

        //        if (Request.Files.Count > 0)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                if (uploadFile == null)
        //                {
        //                    ModelState.AddModelError("File", "Please Upload Your File");
        //                }
        //                else
        //                {
        //                    string LeadHistoryIdActivity = result.ToString();
        //                    //string LeadHistoryIdActivity = Session["LeadHistoryIDActivity"].ToString();
        //                    string fileName = System.IO.Path.GetFileName(uploadFile.FileName);
        //                    string fileExtension = System.IO.Path.GetExtension(uploadFile.FileName);
        //                    String FilePath = ConfigurationManager.AppSettings["leadattachmentActivitypath"];
        //                    FilePath = FilePath + LeadHistoryIdActivity + '/';
        //                    string newFolder = Server.MapPath(FilePath);
        //                    if (Directory.Exists(newFolder))
        //                    {
        //                        string[] filePaths = Directory.GetFiles(newFolder);
        //                        foreach (string filePath in filePaths)
        //                        {
        //                            System.IO.File.Delete(filePath);
        //                        }
        //                        Directory.Delete(newFolder);
        //                        Directory.CreateDirectory(newFolder);
        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(newFolder);
        //                    }
        //                    var path = Server.MapPath(FilePath) + fileName;
        //                    uploadFile.SaveAs(path);
        //                    ModelState.Clear();
        //                    ViewBag.DownloadAttachment = "Download Attachment Activity";
        //                }
        //            }
        //        }
        //        //To Get the Nofications for Activities
        //        var loginid = GlobalVariables.UserID;
        //        var roldid = GlobalVariables.RoleID;
        //        //To Get the Nofications for Activities
        //        DateTime dtSpecifiedDate = DateTime.Now.Date;
        //        DateTime RemindToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(-15).ToString("MM/dd/yyyy"));
        //        DateTime DueToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(15).ToString("MM/dd/yyyy"));

        //        //DateTime RemindToDate = Convert.ToDateTime(dtSpecifiedDate.ToString("MM/dd/yyyy"));
        //        //DateTime RemindToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(-15).ToString("MM/dd/yyyy"));
        //        //DateTime DueToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(15 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek)).AddDays(-1).ToString("MM/dd/yyyy"));
        //        string UsrId = GlobalVariables.UserID;
        //        string empid = GlobalVariables.UserID;
        //        // string empid = "-1";
        //        string Status = "Open";
        //        int totalPageCount = 0;
        //        string officeId = "All";
        //        //List<tbl_history> noticehistory =  GetActivityNotifications
        //        List<tbl_history> lstHistoryNofication = _iDashboardDatarepository.GetActivityNotifications(loginid, UsrId, roldid, empid, officeId, Status, RemindToDate, DueToDate, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        for (int i = 0; i < lstHistoryNofication.Count; i++)
        //        {
        //            DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));
        //            DateTime Datemodel = Convert.ToDateTime(lstHistoryNofication[i].StartDate);
        //            DateTime rminddate = Convert.ToDateTime(Datemodel.ToString("MM/dd/yyyy"));
        //            TimeSpan differenced = rminddate - fdate;
        //            int daysd = Convert.ToInt32(differenced.TotalDays);
        //            string curdate = Convert.ToString(fdate);
        //            string sdate = Convert.ToString(lstHistoryNofication[i].StartDate);
        //            string SStime = Convert.ToString(lstHistoryNofication[i].StartDate);
        //            lstHistoryNofication[i].FremindDate = Convert.ToString(fdate);
        //            lstHistoryNofication[i].FstartDate = SStime;
        //            lstHistoryNofication[i].daysdiff = daysd;
        //            lstHistoryNofication[i].CurrentDate = fdate;
        //        }
        //        Session["Notifications"] = lstHistoryNofication;
        //        return Redirect(leadurlpath);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet]
        //public ActionResult EditLeadActivity(Int64 leadhistoryid)
        //{
        //    try
        //    {
        //        string editleadulpth = this.Request.UrlReferrer.AbsoluteUri;
        //        var editleaduri = new Uri(editleadulpth);
        //        Session["editleadActvityurl"] = editleaduri;
        //        int LeadId = Convert.ToInt32(TempData["LeadID"]);
        //        tbl_Leadhistory leadhist = new tbl_Leadhistory();
        //        leadhist = _ileadrepository.editLeadNewActivityByLeadhistoryId(leadhistoryid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        leadhist.LeadName = _ileadrepository.GetLeadNameById(Convert.ToInt32(leadhist.Leadid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        activity_types selectoption = new activity_types();
        //        selectoption.Name = "Select";
        //        leadhist.TypeList = new List<activity_types>();
        //        leadhist.TypeList.Add(selectoption);
        //        leadhist.TypeList.AddRange(_ileadrepository.GetLeadActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        string file = string.Empty;
        //        Session["LeadHistoryIDActivity"] = leadhistoryid;
        //        string LeadHistoryIdActivity = leadhistoryid.ToString();
        //        String FilePath = ConfigurationManager.AppSettings["leadattachmentActivitypath"];
        //        FilePath = FilePath + LeadHistoryIdActivity + '/';
        //        string newFolder = Server.MapPath(FilePath);
        //        if (Directory.Exists(newFolder))
        //        {

        //            string[] filenames = Directory.GetFiles(newFolder);
        //            foreach (string filename in filenames)
        //            {
        //                file = System.IO.Path.GetFileName(filename);
        //                ViewBag.Message = file;
        //            }


        //            ViewBag.DownloadAttachment = "Download Attachment Activity";
        //        }

        //        List<user> lstSales = _icompanyrepository.GetAdmin(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        leadhist.userList = lstSales;
        //        return View(leadhist);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ActionResult UpdateLeadActivity(tbl_Leadhistory tblleadhist, HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        TempData["EditLeadactivity"] = "EditLeadactivity";
        //        string editleadurlpath = "";

        //        if (Session["urilead"] != null)
        //        {

        //            string editleaduipth = Session["editleadActvityurl"].ToString();
        //            editleadurlpath = editleaduipth;
        //        }
        //        var todaydate = DateTime.Now;
        //        var duedate = tblleadhist.StartDate;
        //        if (duedate > todaydate)
        //        {
        //            tblleadhist.NotificationFlag = false;
        //        }
        //        string result = _ileadrepository.updateLeadActivityByLeadHistoryId(tblleadhist, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        if (Request.Files.Count > 0)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                if (file == null)
        //                {
        //                    ModelState.AddModelError("File", "Please Upload Your File");
        //                }
        //                else
        //                {
        //                    string LeadHistoryIdActivity = tblleadhist.LeadhistoryId.ToString();
        //                    //string LeadHistoryIdActivity = result.ToString();
        //                    //string LeadHistoryIdActivity = Session["LeadHistoryIDActivity"].ToString();
        //                    string fileName = System.IO.Path.GetFileName(file.FileName);
        //                    string fileExtension = System.IO.Path.GetExtension(file.FileName);
        //                    String FilePath = ConfigurationManager.AppSettings["leadattachmentActivitypath"];
        //                    FilePath = FilePath + LeadHistoryIdActivity + '/';
        //                    string newFolder = Server.MapPath(FilePath);
        //                    if (Directory.Exists(newFolder))
        //                    {
        //                        string[] filePaths = Directory.GetFiles(newFolder);
        //                        foreach (string filePath in filePaths)
        //                        {
        //                            System.IO.File.Delete(filePath);
        //                        }
        //                        Directory.Delete(newFolder);
        //                        Directory.CreateDirectory(newFolder);
        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(newFolder);
        //                    }
        //                    var path = Server.MapPath(FilePath) + fileName;
        //                    file.SaveAs(path);
        //                    ModelState.Clear();
        //                    ViewBag.DownloadAttachment = "Download Attachment Activity";
        //                }
        //            }
        //        }
        //        //return RedirectToAction("EditLeadActivity", new { tblleadhist.LeadhistoryId });
        //        //To Get the Nofications for Activities
        //        var loginid = GlobalVariables.UserID;
        //        var roldid = GlobalVariables.RoleID;
        //        DateTime dtSpecifiedDate = DateTime.Now.Date;
        //        DateTime RemindToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(-15).ToString("MM/dd/yyyy"));
        //        DateTime DueToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(15).ToString("MM/dd/yyyy"));
        //        TimeSpan t = DueToDate - RemindToDate;
        //        double NrOfDays = t.TotalDays;
        //        string UsrId = GlobalVariables.UserID;
        //        string empid = GlobalVariables.UserID;
        //        //string empid = "-1";
        //        string Status = "Open";
        //        int totalPageCount = 0;
        //        string officeId = "All";
        //        List<tbl_history> lstHistory = _iDashboardDatarepository.GetActivityNotifications(loginid, UsrId, roldid, empid, officeId, Status, RemindToDate, DueToDate, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        for (int i = 0; i < lstHistory.Count; i++)
        //        {
        //            DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));
        //            DateTime Datemodel = Convert.ToDateTime(lstHistory[i].StartDate);
        //            DateTime rminddate = Convert.ToDateTime(Datemodel.ToString("MM/dd/yyyy"));
        //            TimeSpan differenced = rminddate - fdate;
        //            int daysd = Convert.ToInt32(differenced.TotalDays);
        //            string curdate = Convert.ToString(fdate);
        //            string sdate = Convert.ToString(lstHistory[i].StartDate);
        //            string SStime = Convert.ToString(lstHistory[i].StartDate);
        //            lstHistory[i].FremindDate = Convert.ToString(fdate);
        //            lstHistory[i].FstartDate = SStime;
        //            lstHistory[i].daysdiff = daysd;
        //            lstHistory[i].CurrentDate = fdate;
        //        }
        //        Session["Notifications"] = lstHistory;

        //        return Redirect(editleadurlpath);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public bool DeleteLeadActivity(string leadhistoryid)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(leadhistoryid))
        //        {
        //            foreach (var Delleadactivities in leadhistoryid.Split(','))
        //            {
        //                var deleteOppor = _ileadrepository.DeleteLeadActivity(Int32.Parse(leadhistoryid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //            }
        //          //  updatenotification();
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet]
        //public ActionResult completeLeadActivity(int leadhistoryid)
        //{
        //    try
        //    {
        //        int result = _ileadrepository.CompleteLeadActivity(leadhistoryid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public ActionResult ListParentComp()
        {
            try
            {
                ////List<tbl_account> lstParentComp = _icompanyrepository.ParentComp(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                ////return PartialView(lstParentComp);
                //string orderByClause = string.Empty;
                //orderByClause = "AccountId desc";
                //int UsrId = Convert.ToInt32(GlobalVariables.UserID);
                //string RoleId = GlobalVariables.RoleID;
                //string loginid = GlobalVariables.UserID;
                //string Keyword = null;
                //string officeId = "All";
                //string empid = "-1";
                //string salesRepId = "All";
                //List<tbl_account> accountsList = _icontactsrepository.GetAllCompanyListLookUp(Keyword, officeId, RoleId, loginid, salesRepId, orderByClause, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public ActionResult GetLeadEmails(jQueryDataTableParamModel param)
        //{
        //    try
        //    {
        //        string Email = Request["MailEmail"];
        //        string Type = Request["MailType"];
        //        string SalesRepresentative = Request["MailSalesRepresentative"];
        //        string Subject = Request["MailSubject"];
        //        string Body = Request["MailBody"];
        //        string iSortCol_0 = Request["iSortCol_0"];
        //        string sSortDir_0 = Request["sSortDir_0"];
        //        string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
        //        string orderByClause = string.Empty;
        //        string LeadId = Convert.ToString(TempData["LeadID"]);
        //        TempData.Keep("LeadId");
        //        string NewActLeadId = LeadId;
        //        TempData["NewActLeadId"] = LeadId;
        //        int totalPageCount = 0;
        //        string loginid = GlobalVariables.UserID;
        //        string userid = GlobalVariables.UserID;
        //        orderByClause = SortFieldName + " " + sSortDir_0;
        //        var listActivity = _ileadrepository.getLeadEmails(Type == "Select" ? "Select" : Type, Email == "" ? null : Email, Subject == "" ? null : Subject, Body == "" ? null : Body,
        //                                                            loginid, userid, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            aaData = listActivity,
        //            iTotalRecords = totalPageCount,
        //            iTotalDisplayRecords = totalPageCount
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void ExportToCsv()
        //{
        //    try
        //    {
        //        int totalPageCount = 0;

        //        string filterExpression = "";
        //        string LeadKeyword = TempData.Peek("LeadKeyword").ToString();
        //        string LeadBranch = TempData.Peek("LeadBranch").ToString();
        //        string LeadIndustry = TempData.Peek("LeadIndustry").ToString();
        //        string LeadStatus = TempData.Peek("LeadStatus").ToString();
        //        string orderByClause = TempData.Peek("orderByClause").ToString();
        //        string LeadEmail = TempData.Peek("LeadEmail").ToString();
        //        string LeadOwner = TempData.Peek("LeadOwner").ToString();
        //        totalPageCount = Convert.ToInt32(TempData.Peek("TotalPageCount").ToString());

        //        List<tbl_Lead> leadlist = _ileadrepository.getAllLeads(filterExpression == "" ? null : filterExpression, LeadKeyword == "" ? null : LeadKeyword, LeadStatus == "None" ? "All" : LeadStatus, LeadEmail == "" ? null : LeadEmail,
        //           LeadIndustry == "" ? "All" : LeadIndustry, LeadOwner == "-1" ? "All" : LeadOwner, LeadBranch == "-1" ? "All" : LeadBranch, Convert.ToInt32(GlobalVariables.UserID), Convert.ToInt32(GlobalVariables.RoleID),
        //           0, totalPageCount, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


        //        StringWriter sw = new StringWriter();
        //        sw.Flush();
        //        sw.WriteLine("\"AccountName\",\"LeadName\",\"Phone\",\"Email\",\"SalesMgr1\",\"LeadStatus\",\"Source\",\"CreatedDate\",\"ModifiedDate\"");

        //        Response.ClearContent();
        //        Response.AddHeader("content-disposition", "attachment;filename=LeadsExport.csv");
        //        Response.ContentType = "text/csv";
        //        for (int i = 0; i < leadlist.Count; i++)
        //        {
        //            sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"",
        //                                       leadlist[i].AccountName,
        //                                       leadlist[i].LeadName,
        //                                       leadlist[i].Phone,
        //                                       leadlist[i].Email,
        //                                       leadlist[i].SalesMgr1,
        //                                       leadlist[i].LeadStatus,
        //                                       leadlist[i].Source,
        //                                       leadlist[i].CreatedDate,
        //                                       leadlist[i].ModifiedDate
        //                                      ));
        //        }
        //        Response.Write(sw.ToString());

        //        Response.End();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void ExportToExcel()
        //{
        //    try
        //    {
        //        int totalPageCount = 0;

        //        string filterExpression = "";
        //        string LeadKeyword = TempData.Peek("LeadKeyword").ToString();
        //        string LeadBranch = TempData.Peek("LeadBranch").ToString();
        //        string LeadIndustry = TempData.Peek("LeadIndustry").ToString();
        //        string LeadStatus = TempData.Peek("LeadStatus").ToString();
        //        string orderByClause = TempData.Peek("orderByClause").ToString();
        //        string LeadEmail = TempData.Peek("LeadEmail").ToString();
        //        string LeadOwner = TempData.Peek("LeadOwner").ToString();
        //        totalPageCount = Convert.ToInt32(TempData.Peek("TotalPageCount").ToString());

        //        List<tbl_Lead> leadlist = _ileadrepository.getAllLeads(filterExpression == "" ? null : filterExpression, LeadKeyword == "" ? null : LeadKeyword, LeadStatus == "None" ? "All" : LeadStatus, LeadEmail == "" ? null : LeadEmail,
        //           LeadIndustry == "" ? "All" : LeadIndustry, LeadOwner == "-1" ? "All" : LeadOwner, LeadBranch == "-1" ? "All" : LeadBranch, Convert.ToInt32(GlobalVariables.UserID), Convert.ToInt32(GlobalVariables.RoleID),
        //           0, totalPageCount, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


        //        var grid = new System.Web.UI.WebControls.GridView();
        //        grid.DataSource = from d in leadlist
        //                          select new
        //                          {
        //                              AccountName = d.AccountName,
        //                              LeadName = d.LeadName,
        //                              Phone = d.Phone,
        //                              Email = d.Email,
        //                              SalesMgr1 = d.SalesMgr1,
        //                              LeadStatus = d.LeadStatus,
        //                              Source = d.Source,
        //                              CreatedDate = d.CreatedDate,
        //                              ModifiedDate = d.ModifiedDate
        //                          };
        //        grid.DataBind();

        //        Response.ClearContent();
        //        Response.AddHeader("content-disposition", "attachment; filename=LeadsExport.xls");
        //        Response.ContentType = "application/excel";
        //        StringWriter sw = new StringWriter();
        //        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //        grid.RenderControl(htw);

        //        Response.Write(sw.ToString());

        //        Response.End();



        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ActionResult ExportToCsv(List<ExportLeads> item)
        //{
        //    try
        //    {
        //        StringWriter sw = new StringWriter();
        //        sw.Flush();
        //        sw.WriteLine("\"AccountName\",\"LeadName\",\"Phone\",\"Email\",\"SalesMgr1\",\"LeadStatus\",\"Source\",\"CreatedDate\",\"ModifiedDate\"");
        //        for (int i = 0; i < item.Count; i++)
        //        {
        //            sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"",
        //                                       item[i].AccountName,
        //                                       item[i].LeadName,
        //                                       item[i].Phone,
        //                                       item[i].Email,
        //                                       item[i].SalesMgr1,
        //                                       item[i].LeadStatus,
        //                                       item[i].Source,
        //                                       item[i].CreatedDate,
        //                                       item[i].ModifiedDate
        //                                       ));
        //        }
        //        string fullpath = Path.Combine(Server.MapPath("~/Content/LeadsExport/"));
        //        if (System.IO.File.Exists(fullpath + "ExportLeads.csv"))
        //            System.IO.File.Delete(fullpath + "ExportLeads.csv");
        //        string renderedGridView = sw.ToString();
        //        System.IO.File.AppendAllText(fullpath + "ExportLeads.csv", renderedGridView);
        //        ViewBag.ExportLeadsCSV = "1";
        //        return Json(item, JsonRequestBehavior.AllowGet);
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet]
        //public ActionResult DownloadLeadsExportCSV()
        //{
        //    try
        //    {
        //        string file = "ExportLeads.csv";
        //        string fullPath = Path.Combine(Server.MapPath("~/Content/LeadsExport/"), file);
        //        return File(fullPath, "application/vnd.ms-excel", file);
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpPost]
        public ActionResult ExportToExcel(List<ExportLeads> item)
        {
            try
            {
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = from d in item
                                  select new
                                  {
                                      AccountName = d.AccountName,
                                      LeadName = d.LeadName,
                                      Phone = d.Phone,
                                      Email = d.Email,
                                      SalesMgr1 = d.SalesMgr1,
                                      LeadStatus = d.LeadStatus,
                                      Source = d.Source,
                                      CreatedDate = d.CreatedDate,
                                      ModifiedDate = d.ModifiedDate
                                  };
                grid.DataBind();
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                string fullpath = Path.Combine(Server.MapPath("~/Content/LeadsExport/"));
                if (System.IO.File.Exists(fullpath + "ExportLeads.xls"))
                    System.IO.File.Delete(fullpath + "ExportLeads.xls");
                string renderedGridView = sw.ToString();
                System.IO.File.AppendAllText(fullpath + "ExportLeads.xls", renderedGridView);
                ViewBag.ExportLeadsExcel = "2";
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult DownloadLeadsExportExcel()
        {
            try
            {
                string file = "ExportLeads.xls";
                string fullPath = Path.Combine(Server.MapPath("~/Content/LeadsExport/"), file);
                return File(fullPath, "application/vnd.ms-excel", file);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult AttachmentActivity(int LeadHistoryID)
        {
            try
            {
                Session["LeadHistoryIDActivity"] = LeadHistoryID;
                string LeadHistoryIdActivity = Session["LeadHistoryIDActivity"].ToString();
                String FilePath = ConfigurationManager.AppSettings["leadattachmentActivitypath"];
                FilePath = FilePath + LeadHistoryIdActivity + '/';
                string newFolder = Server.MapPath(FilePath);
                if (Directory.Exists(newFolder))
                {
                    ViewBag.DownloadAttachment = "Download Attachment Activity";
                }
                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult UploadAttachmentActivity(HttpPostedFileBase file)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    if (ModelState.IsValid)
                    {
                        if (file == null)
                        {
                            ModelState.AddModelError("File", "Please Upload Your File");
                        }
                        else
                        {
                            string LeadHistoryIdActivity = Session["LeadHistoryIDActivity"].ToString();
                            string fileName = System.IO.Path.GetFileName(file.FileName);
                            string fileExtension = System.IO.Path.GetExtension(file.FileName);
                            String FilePath = ConfigurationManager.AppSettings["leadattachmentActivitypath"];
                            FilePath = FilePath + LeadHistoryIdActivity + '/';
                            string newFolder = Server.MapPath(FilePath);
                            if (Directory.Exists(newFolder))
                            {
                                string[] filePaths = Directory.GetFiles(newFolder);
                                foreach (string filePath in filePaths)
                                {
                                    System.IO.File.Delete(filePath);
                                }
                                Directory.Delete(newFolder);
                                Directory.CreateDirectory(newFolder);
                            }
                            else
                            {
                                Directory.CreateDirectory(newFolder);
                            }
                            var path = Server.MapPath(FilePath) + fileName;
                            file.SaveAs(path);
                            ModelState.Clear();
                            ViewBag.DownloadAttachment = "Download Attachment Activity";
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FileResult DownloadAttachmentActivity()
        {
            try
            {
                string file = string.Empty;
                string LeadHistoryIdActivity = Session["LeadHistoryIDActivity"].ToString();
                String FilePath = ConfigurationManager.AppSettings["leadattachmentActivitypath"];
                FilePath = FilePath + LeadHistoryIdActivity + '/';
                string newFolder = Server.MapPath(FilePath);
                if (Directory.Exists(newFolder))
                {
                    string[] filenames = Directory.GetFiles(newFolder);
                    foreach (string filename in filenames)
                    {
                        file = System.IO.Path.GetFileName(filename);
                    }
                }
                string contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
                return File(Path.Combine(FilePath, file), contentType, file);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //[HttpGet]
        //public ActionResult CreateLeadActivityfromHome()
        //{
        //    try
        //    {
        //        tbl_Leadhistory leadhist = new tbl_Leadhistory();
        //        // leadhist.Leadid = Convert.ToInt64(TempData["NewActLeadId"]);
        //        activity_types selectoption = new activity_types();
        //        selectoption.Name = "Select";
        //        selectoption.Aid = -1;
        //        leadhist.TypeList = new List<activity_types>();
        //        leadhist.TypeList.Add(selectoption);
        //        leadhist.TypeList.AddRange(_ileadrepository.GetLeadActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        return View(leadhist);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //[HttpGet]
        //public ActionResult CreateLeadActivityfromHome()
        //{
        //    try
        //    {
        //        DateTime loadedDate = DateTime.Now;
        //        string start = Request.QueryString["dateField"];
        //        if (start != "")
        //        {
        //            loadedDate = DateTime.ParseExact(start, "dd/MM/yyyy",
        //                                      CultureInfo.InvariantCulture);
        //        }
        //        tbl_Leadhistory leadhist = new tbl_Leadhistory();
        //        if (start != "")
        //        {
        //            leadhist.StartDate = loadedDate;
        //            leadhist.RemindDate = Convert.ToString(loadedDate.AddDays(-15));
        //        }

        //        // leadhist.Leadid = Convert.ToInt64(TempData["NewActLeadId"]);
        //        activity_types selectoption = new activity_types();
        //        selectoption.Name = "Select";
        //        selectoption.Aid = -1;
        //        leadhist.TypeList = new List<activity_types>();
        //        leadhist.TypeList.Add(selectoption);
        //        leadhist.TypeList.AddRange(_ileadrepository.GetLeadActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));


        //        List<user> lstSales = _icompanyrepository.GetAdmin(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        leadhist.userList = lstSales;
        //        return View(leadhist);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //[HttpPost]
        //public ActionResult CreateLeadActivityfromHome(tbl_Leadhistory leadhist, FormCollection fc, HttpPostedFileBase uploadFile)
        //{

        //    try
        //    {
        //        int result = 0;
        //        tbl_Lead leadEntity = _ileadrepository.getLeadByLeadId(Convert.ToDecimal(leadhist.Leadid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        if (uploadFile != null && uploadFile.ContentLength > 0)
        //        {
        //            string filePath = Path.Combine(HttpContext.Server.MapPath("~/Content/Files"),
        //            Path.GetFileName(uploadFile.FileName));
        //            uploadFile.SaveAs(filePath);
        //            leadhist.AccountName = leadEntity.AccountName;
        //            //leadhist.AccountName = Convert.ToString(TempData["AccountName"]);
        //            leadhist.ActivityAttachment = uploadFile.FileName;
        //            TempData["File"] = leadhist.ActivityAttachment;
        //            leadhist.CreatedBy = Convert.ToInt32(GlobalVariables.UserID);
        //            leadhist.CreatedDate = DateTime.Now;
        //            // leadhist.AssignedTo = Convert.ToString(GlobalVariables.UserID);
        //            leadhist.NotificationFlag = false;
        //            if (leadhist.Leadid != null)
        //            {
        //                result = _ileadrepository.createLeadNewActivity_Insert(leadhist, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //            }
        //            //  return RedirectToAction("Index");
        //            // return RedirectToAction("ActivitygridBind", "ManageCompany");
        //        }
        //        else
        //        {
        //            activity_types selectoption = new activity_types();
        //            selectoption.Name = "Select";
        //            leadhist.AccountId = 0;
        //            leadhist.AccountName = "";
        //            leadhist.TypeList = new List<activity_types>();
        //            leadhist.TypeList.Add(selectoption);
        //            leadhist.TypeList.AddRange(_ileadrepository.GetLeadActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //            leadhist.CreatedBy = Convert.ToInt32(GlobalVariables.UserID);
        //            leadhist.CreatedDate = DateTime.Now;
        //            // leadhist.AssignedTo = Convert.ToString(GlobalVariables.UserID);
        //            leadhist.NotificationFlag = false;
        //            if (leadhist.Leadid != null)
        //            {
        //                result = _ileadrepository.createLeadNewActivity_Insert(leadhist, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //            }
        //            // return View(leadhist);
        //            // return RedirectToAction("ActivitygridBind", "ManageCompany");
        //        }

        //        if (Request.Files.Count > 0)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                if (uploadFile == null)
        //                {
        //                    ModelState.AddModelError("File", "Please Upload Your File");
        //                }
        //                else
        //                {
        //                    string LeadHistoryIdActivity = result.ToString();
        //                    //string LeadHistoryIdActivity = Session["LeadHistoryIDActivity"].ToString();
        //                    string fileName = System.IO.Path.GetFileName(uploadFile.FileName);
        //                    string fileExtension = System.IO.Path.GetExtension(uploadFile.FileName);
        //                    String FilePath = ConfigurationManager.AppSettings["leadattachmentActivitypath"];
        //                    FilePath = FilePath + LeadHistoryIdActivity + '/';
        //                    string newFolder = Server.MapPath(FilePath);
        //                    if (Directory.Exists(newFolder))
        //                    {
        //                        string[] filePaths = Directory.GetFiles(newFolder);
        //                        foreach (string filePath in filePaths)
        //                        {
        //                            System.IO.File.Delete(filePath);
        //                        }
        //                        Directory.Delete(newFolder);
        //                        Directory.CreateDirectory(newFolder);
        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(newFolder);
        //                    }
        //                    var path = Server.MapPath(FilePath) + fileName;
        //                    uploadFile.SaveAs(path);
        //                    ModelState.Clear();
        //                    ViewBag.DownloadAttachment = "Download Attachment Activity";
        //                }
        //            }
        //        }


        //        //To Get the Nofications for Activities
        //        var loginid = GlobalVariables.UserID;
        //        var roldid = GlobalVariables.RoleID;
        //        //To Get the Nofications for Activities
        //        DateTime dtSpecifiedDate = DateTime.Now.Date;
        //        DateTime RemindToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(-15).ToString("MM/dd/yyyy"));
        //        DateTime DueToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(15).ToString("MM/dd/yyyy"));

        //        //DateTime RemindToDate = Convert.ToDateTime(dtSpecifiedDate.ToString("MM/dd/yyyy"));
        //        //DateTime RemindToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(-15).ToString("MM/dd/yyyy"));
        //        //DateTime DueToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(15 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek)).AddDays(-1).ToString("MM/dd/yyyy"));
        //        string UsrId = GlobalVariables.UserID;
        //        string empid = GlobalVariables.UserID;
        //        //string empid = "-1";
        //        string Status = "Open";
        //        int totalPageCount = 0;
        //        string officeId = "All";
        //        // List<tbl_history> noticehistory =  GetActivityNotifications
        //        List<tbl_history> lstHistoryNofication = _iDashboardDatarepository.GetActivityNotifications(loginid, UsrId, roldid, empid, officeId, Status, RemindToDate, DueToDate, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        for (int i = 0; i < lstHistoryNofication.Count; i++)
        //        {
        //            DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));
        //            DateTime Datemodel = Convert.ToDateTime(lstHistoryNofication[i].StartDate);
        //            DateTime rminddate = Convert.ToDateTime(Datemodel.ToString("MM/dd/yyyy"));
        //            TimeSpan differenced = rminddate - fdate;
        //            int daysd = Convert.ToInt32(differenced.TotalDays);
        //            string curdate = Convert.ToString(fdate);
        //            string sdate = Convert.ToString(lstHistoryNofication[i].StartDate);
        //            string SStime = Convert.ToString(lstHistoryNofication[i].StartDate);
        //            lstHistoryNofication[i].FremindDate = Convert.ToString(fdate);
        //            lstHistoryNofication[i].FstartDate = SStime;
        //            lstHistoryNofication[i].daysdiff = daysd;
        //            lstHistoryNofication[i].CurrentDate = fdate;
        //        }
        //        Session["Notifications"] = lstHistoryNofication;

        //        return RedirectToAction("ActivitygridBind", "ManageCompany");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //public ActionResult SelectLead()
        //{
        //    try
        //    {
        //        // List<tbl_Lead> LeadList = _ileadrepository.GetLeadsList(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        string orderByClause = string.Empty;
        //        orderByClause = "LeadId desc";
        //        string LeadOwner = "All";
        //        string RoleId = GlobalVariables.RoleID;
        //        string loginid = GlobalVariables.UserID;
        //        string Keyword = null;
        //        string officeId = "All";

        //        List<tbl_Lead> LeadList = _ileadrepository.GetLeadsLookUpList(Keyword, LeadOwner, officeId, loginid, RoleId, orderByClause, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        return PartialView(LeadList);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //[HttpGet]
        //public ActionResult EditLeadActivityGrid(Int64 leadhistoryid)
        //{
        //    try
        //    {
        //        // int LeadId = Convert.ToInt32(TempData["LeadID"]);
        //        tbl_Leadhistory leadhist = new tbl_Leadhistory();

        //        leadhist = _ileadrepository.editLeadNewActivityByLeadhistoryId(leadhistoryid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        leadhist.LeadName = _ileadrepository.GetLeadNameById(Convert.ToInt32(leadhist.Leadid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        activity_types selectoption = new activity_types();
        //        selectoption.Name = "Select";
        //        leadhist.TypeList = new List<activity_types>();
        //        leadhist.TypeList.Add(selectoption);
        //        leadhist.TypeList.AddRange(_ileadrepository.GetLeadActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        string file = string.Empty;
        //        Session["LeadHistoryIDActivity"] = leadhistoryid;
        //        string LeadHistoryIdActivity = leadhistoryid.ToString();
        //        String FilePath = ConfigurationManager.AppSettings["leadattachmentActivitypath"];
        //        FilePath = FilePath + LeadHistoryIdActivity + '/';
        //        string newFolder = Server.MapPath(FilePath);
        //        if (Directory.Exists(newFolder))
        //        {

        //            string[] filenames = Directory.GetFiles(newFolder);
        //            foreach (string filename in filenames)
        //            {
        //                file = System.IO.Path.GetFileName(filename);
        //                ViewBag.Message = file;
        //            }


        //            ViewBag.DownloadAttachment = "Download Attachment Activity";
        //        }
        //        List<user> lstSales = _icompanyrepository.GetAdmin(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        leadhist.userList = lstSales;

        //        return View(leadhist);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ActionResult EditLeadActivityGrid(tbl_Leadhistory tblleadhist, HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        var todaydate = DateTime.Now;
        //        var duedate = tblleadhist.StartDate;
        //        if (duedate > todaydate)
        //        {
        //            tblleadhist.NotificationFlag = false;
        //        }
        //        string result = _ileadrepository.updateLeadActivityByLeadHistoryId(tblleadhist, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        if (Request.Files.Count > 0)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                if (file == null)
        //                {
        //                    ModelState.AddModelError("File", "Please Upload Your File");
        //                }
        //                else
        //                {
        //                    string LeadHistoryIdActivity = tblleadhist.LeadhistoryId.ToString();
        //                    //string LeadHistoryIdActivity = result.ToString();
        //                    //string LeadHistoryIdActivity = Session["LeadHistoryIDActivity"].ToString();
        //                    string fileName = System.IO.Path.GetFileName(file.FileName);
        //                    string fileExtension = System.IO.Path.GetExtension(file.FileName);
        //                    String FilePath = ConfigurationManager.AppSettings["leadattachmentActivitypath"];
        //                    FilePath = FilePath + LeadHistoryIdActivity + '/';
        //                    string newFolder = Server.MapPath(FilePath);
        //                    if (Directory.Exists(newFolder))
        //                    {
        //                        string[] filePaths = Directory.GetFiles(newFolder);
        //                        foreach (string filePath in filePaths)
        //                        {
        //                            System.IO.File.Delete(filePath);
        //                        }
        //                        Directory.Delete(newFolder);
        //                        Directory.CreateDirectory(newFolder);
        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(newFolder);
        //                    }
        //                    var path = Server.MapPath(FilePath) + fileName;
        //                    file.SaveAs(path);
        //                    ModelState.Clear();
        //                    ViewBag.DownloadAttachment = "Download Attachment Activity";
        //                }
        //            }
        //        }

        //        //return RedirectToAction("EditLeadActivity", new { tblleadhist.LeadhistoryId });
        //        //To Get the Nofications for Activities
        //        var loginid = GlobalVariables.UserID;
        //        var roldid = GlobalVariables.RoleID;
        //        DateTime dtSpecifiedDate = DateTime.Now.Date;
        //        DateTime RemindToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(-15).ToString("MM/dd/yyyy"));
        //        DateTime DueToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(15).ToString("MM/dd/yyyy"));
        //        TimeSpan t = DueToDate - RemindToDate;
        //        double NrOfDays = t.TotalDays;
        //        string UsrId = GlobalVariables.UserID;
        //        string empid = GlobalVariables.UserID;
        //        //string empid = "-1";
        //        string Status = "Open";
        //        int totalPageCount = 0;
        //        string officeId = "All";
        //        List<tbl_history> lstHistory = _iDashboardDatarepository.GetActivityNotifications(loginid, UsrId, roldid, empid, officeId, Status, RemindToDate, DueToDate, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        for (int i = 0; i < lstHistory.Count; i++)
        //        {
        //            DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));
        //            DateTime Datemodel = Convert.ToDateTime(lstHistory[i].StartDate);
        //            DateTime rminddate = Convert.ToDateTime(Datemodel.ToString("MM/dd/yyyy"));
        //            TimeSpan differenced = rminddate - fdate;
        //            int daysd = Convert.ToInt32(differenced.TotalDays);
        //            string curdate = Convert.ToString(fdate);
        //            string sdate = Convert.ToString(lstHistory[i].StartDate);
        //            string SStime = Convert.ToString(lstHistory[i].StartDate);
        //            lstHistory[i].FremindDate = Convert.ToString(fdate);
        //            lstHistory[i].FstartDate = SStime;
        //            lstHistory[i].daysdiff = daysd;
        //            lstHistory[i].CurrentDate = fdate;
        //        }
        //        Session["Notifications"] = lstHistory;

        //        return RedirectToAction("ActivitygridBind", "ManageCompany");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpGet]
        public ActionResult RedirectLeadActivityGrid()
        {
            TempData["EditLeadactivity"] = "EditLeadactivity";
            // int LeadId = Convert.ToInt32(TempData["LeadID"]);
            int LeadId = Convert.ToInt32(Session["Leadid"]);
            return RedirectToAction("EditLead", new { LeadId = LeadId });
        }
        //public void updatenotification()
        //{
        //    var loginid = GlobalVariables.UserID;
        //    var roldid = GlobalVariables.RoleID;
        //    DateTime dtSpecifiedDate = DateTime.Now.Date;
        //    // DateTime RemindToDate = Convert.ToDateTime(dtSpecifiedDate.ToString("MM/dd/yyyy"));
        //    // DateTime DueToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(30).ToString("MM/dd/yyyy"));
        //    DateTime RemindToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(-15).ToString("MM/dd/yyyy"));
        //    DateTime DueToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(15).ToString("MM/dd/yyyy"));
        //    TimeSpan t = DueToDate - RemindToDate;
        //    double NrOfDays = t.TotalDays;
        //    string UsrId = GlobalVariables.UserID;
        //    string empid = GlobalVariables.UserID;
        //    // string empid = "-1";
        //    string Status = "Open";
        //    int totalPageCount = 0;
        //    string officeId = "All";
        //    // List<tbl_history> noticehistory =  GetActivityNotifications
        //    List<tbl_history> lstHistory = _iDashboardDatarepository.GetActivityNotifications(loginid, UsrId, roldid, empid, officeId, Status, RemindToDate, DueToDate, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //    for (int i = 0; i < lstHistory.Count; i++)
        //    {
        //        DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));
        //        DateTime Datemodel = Convert.ToDateTime(lstHistory[i].StartDate);
        //        DateTime rminddate = Convert.ToDateTime(Datemodel.ToString("MM/dd/yyyy"));
        //        TimeSpan differenced = rminddate - fdate;
        //        int daysd = Convert.ToInt32(differenced.TotalDays);
        //        lstHistory[i].daysdiff = daysd;
        //        lstHistory[i].CurrentDate = fdate;
        //    }
        //    //dashboardModel.lstHistory = _iDashboardDatarepository.GetActivitiesDetailsList(loginid, UsrId, roldid, empid, officeId, Status, RemindToDate, DueToDate, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //    Session["Notifications"] = lstHistory;
        //}


        // Constructing the code for Zoho type leads

        [HttpGet]
        public ActionResult CreateNewLead()
        {
            try
            {
                Account accountObj = new Account();
                accountObj.RoleID = Convert.ToInt32(GlobalVariables.RoleID);
                int UserId = Convert.ToInt32(GlobalVariables.UserID);
                string userfullname = _icommonrepository.GetusernameById(UserId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //accountObj.Ownership = GlobalVariables.FirstName + " " + GlobalVariables.LastName;
                accountObj.Ownership = userfullname;
               // accountObj.Ownership = GlobalVariables.UserName;
                accountObj.OwnerID = UserId;
                accountObj.CompanyID = 0;

                accountObj.AccountTypeId = 1;

                /// To get the States List

                State selectState = new State();
                selectState.ID = 0;
                selectState.FullStateName = "Select All";
                List<State> LstState = new List<State>();
                LstState.Add(selectState);
                LstState.AddRange(_icommonrepository.GetAllStatesList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                List<SelectListItem> listofStates = LstState.Select(s => new SelectListItem
                    {
                        // Text = s.Abbreviation,
                        Text = s.FullStateName,
                        Value = s.ID.ToString()
                    }).ToList();

                ViewBag.StateList = listofStates;

                /// To get the Country List
                Country selectCountry = new Country();
                selectCountry.CountryID = 0;
                selectCountry.CountryName = "Select All";
                List<Country> LstCountry = new List<Country>();
                LstCountry.Add(selectCountry);
                LstCountry.AddRange(_icommonrepository.GetAllCountriesList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                List<SelectListItem> listofCountries = LstCountry.Select(c => new SelectListItem
                    {
                        Text = c.CountryName,
                        Value = c.CountryID.ToString()
                    }).ToList();
                ViewBag.CountryList = listofCountries;
                //GetAllCountriesList

                return View(accountObj);

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult CreateNewLead(Account accountObj)
        {
            try
            {
                TempData["CreateLeads"] = "CreateLeads";
                if (accountObj.OptEmailOut == true)
                {
                    accountObj.EmailOptOut = "true";
                }
                else accountObj.EmailOptOut = "false";

                DateTime CreatedDate = DateTime.Now;
                accountObj.CreatedDate = CreatedDate;
                accountObj.ModifiedDate = CreatedDate;
                accountObj.CreatedBy = Convert.ToInt32(GlobalVariables.UserID);
                bool res = _ileadrepository.CreateLead(accountObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public ActionResult getLeaddetails(int LeadId)
        {
            try
            {
                Account ObjAccount = new Account();
                ObjAccount = _ileadrepository.GetAccountDetailsByID(LeadId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (ObjAccount.EmailOptOut == "true")
                {
                    ObjAccount.OptEmailOut = true;
                }

                if (ObjAccount.AnnualRevenue == null)
                {
                    ObjAccount.AnnualRevenue = "0";
                }
                if (ObjAccount.TotalEmployees == null)
                    ObjAccount.TotalEmployees = "0";


                if (TempData["UpdateLeads"] != null)
                {
                    ViewBag.UpdateLeads = "UpdateLeads";

                }

                return View("getLeaddetails", ObjAccount);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult _EditLeadbyID(int LeadId)
        {
            try
            {
                Account ObjAccount = new Account();
                ObjAccount = _ileadrepository.GetAccountDetailsByID(LeadId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (ObjAccount.EmailOptOut == "true")
                {
                    ObjAccount.OptEmailOut = true;
                }

                /// To get the States List

                State selectState = new State();
                selectState.ID = 0;
                selectState.FullStateName = "Select All";
                List<State> LstState = new List<State>();
                LstState.Add(selectState);
                LstState.AddRange(_icommonrepository.GetAllStatesList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                List<SelectListItem> listofStates = LstState.Select(s => new SelectListItem
                {
                    // Text = s.Abbreviation,
                    Text = s.FullStateName,
                    Value = s.ID.ToString()
                }).ToList();

                ViewBag.StateList = listofStates;

                /// To get the Country List
                Country selectCountry = new Country();
                selectCountry.CountryID = 0;
                selectCountry.CountryName = "Select All";
                List<Country> LstCountry = new List<Country>();
                LstCountry.Add(selectCountry);
                LstCountry.AddRange(_icommonrepository.GetAllCountriesList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                List<SelectListItem> listofCountries = LstCountry.Select(c => new SelectListItem
                {
                    Text = c.CountryName,
                    Value = c.CountryID.ToString()
                }).ToList();
                ViewBag.CountryList = listofCountries;
                return PartialView(ObjAccount);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public ActionResult EditLeadDetails(Account accountObj)
        {
            try
            {
                TempData["UpdateLeads"] = "UpdateLeads";
                if (accountObj.OptEmailOut == true)
                {
                    accountObj.EmailOptOut = "true";
                }
                else accountObj.EmailOptOut = "false";

                DateTime ModifiedDate = DateTime.Now;
                accountObj.ModifiedDate = ModifiedDate;
                accountObj.ModifiedBy = Convert.ToInt32(GlobalVariables.UserID);
                int res = _ileadrepository.updateLeadDetailsByLeadId(accountObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                int LeadId = accountObj.ID;
                return RedirectToAction("getLeaddetails", new { LeadId = LeadId });
                //return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost]
        public bool DeleteLeadsbyId(string leadid)
        {
            try
            {
                if (!string.IsNullOrEmpty(leadid))
                {
                    foreach (var leadids in leadid.Split(','))
                    {
                        var deleteOppor = _ileadrepository.DeleteLeadsDetails(Convert.ToInt32(leadids), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public int GetCountrybyStateid(string StateId)
        {
            try
            {

                int CountryId = 0;
                CountryId = _icommonrepository.GetCountrybyStateId(Convert.ToInt32(StateId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return CountryId;

                //int result = _icontactrepository.CompleteContactActivity(historyid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpGet]
        public ActionResult ConvertLead(int LeadId, string CompanyName, int CompanyId)
        {
            try
            {
                string Companyname = CompanyName.Trim();
                List<Company> CompanyList = _ileadrepository.GetAllCompaniesList(GlobalVariables.ConnectionString, GlobalVariables.ClientName).ToList();
                int CCount = 0;
                if (CompanyList.Count > 0)
                {
                    for (int i = 0; i < CompanyList.Count; i++)
                    {
                        string CName = CompanyList[i].Name;
                        if (Companyname == CName.Trim())
                        {
                            CCount = CCount + 1;
                        }
                    }
                }
                ViewBag.companyCount = CCount;
                /// To get the States List
                Account ObjAccount = new Account();
                ObjAccount = _ileadrepository.GetAccountDetailsByID(LeadId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                // return View();
                return View(ObjAccount);
            }
            catch (Exception)
            {
                throw;
            }
        }




        [HttpPost]
        public ActionResult ConvertLead(Account accountObj)
        {
            try
            {
                TempData["ConvertLead"] = "ConvertLead";
                Company CompanyObj = new Company();
                Opportunity OpporModel = new Opportunity();
                int companyId = accountObj.CompanyID ?? 0;
                if (accountObj.CompanyID == 0)
                {
                   
                    CompanyObj.Name = accountObj.CompanyName;
                    CompanyObj.Phone = accountObj.Phone;
                    CompanyObj.Fax = accountObj.Fax;
                    CompanyObj.Website = accountObj.Website;
                    CompanyObj.CreatedDate = accountObj.CreatedDate;
                    CompanyObj.CreatedBy = accountObj.CreatedBy;
                    CompanyObj.ModifiedDate = accountObj.ModifiedDate;
                    CompanyObj.ModifiedBy = accountObj.ModifiedBy;
                    CompanyObj.Ownership = accountObj.Ownership;

                    CompanyObj.BillingAddress = accountObj.MailingAddress; 
                    CompanyObj.Billingstreet = accountObj.MailingAddress2;
                    CompanyObj.Billingcity = accountObj.Mailingcity; 
                    CompanyObj.BillingstateID = accountObj.MailingstateID; 
                    CompanyObj.Billingzip = accountObj.Mailingzip; 
                    CompanyObj.BillingcountryID = accountObj.MailingcountryID;

                    CompanyObj.MailingAddress = accountObj.BillingAddress;
                    CompanyObj.Shippingstreet = accountObj.BillingAddress2;
                    CompanyObj.Shippingcity = accountObj.Billingcity;
                    CompanyObj.ShippingstateID = accountObj.BillingstateID;
                    CompanyObj.Shippingzip = accountObj.Billingzip;
                    CompanyObj.ShippingcountryID = Convert.ToInt32(accountObj.BillingcountryID);

                    CompanyObj.Employees = accountObj.TotalEmployees;
                    CompanyObj.AnnualRevenue = accountObj.AnnualRevenue;
                    CompanyObj.OwnerID = accountObj.OwnerID;

                    int Comres = _icompanyrepository.InserNewCompany(CompanyObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    companyId = CompanyObj.ID;
                }              

                accountObj.AccountTypeId = 2;
               // accountObj.CompanyID = CompanyObj.ID;
                accountObj.CompanyID = companyId;
                int res = _ileadrepository.updateLeadDetailsByLeadId(accountObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (accountObj.IsOpportunity == true)
                {

                    accountObj.AccountTypeId = 4;
                    accountObj.CompanyID = companyId;
                    accountObj.Name = accountObj.OppName;
                    accountObj.ContactID = accountObj.ID;
                    //Account Objaccount = new Account();
                    //Objaccount.AccountTypeId = 4;
                    //Objaccount.Name = accountObj.OppName;
                    //Objaccount.CreatedDate = accountObj.CreatedDate;
                    //Objaccount.CreatedBy = accountObj.CreatedBy;
                    //Objaccount.ModifiedDate = accountObj.ModifiedDate;
                    //Objaccount.ModifiedBy = accountObj.ModifiedBy;
                    //Objaccount.StageID = accountObj.StageID;
                    //Objaccount.Ownership = accountObj.Ownership;
                    //Objaccount.CompanyName = accountObj.CompanyName;

                    //if (accountObj.CompanyID == 0)
                    //{
                    //    //OpporModel.CompanyID = CompanyObj.ID;
                    //    Objaccount.CompanyID = companyId;
                    //}
                    //else Objaccount.CompanyID = companyId;

                    //Objaccount.ContactID = Convert.ToInt32(accountObj.ID);
                    //Objaccount.CloseDate = accountObj.CloseDate;
                    //Objaccount.OwnerID = accountObj.OwnerID;

                    bool OPPres = _ileadrepository.CreateLead(accountObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


                    //Account Objaccount = new Account();
                    //Objaccount.AccountTypeId = 4;
                    //Objaccount.Name = accountObj.OppName;
                    //Objaccount.CreatedDate = accountObj.CreatedDate;
                    //Objaccount.CreatedBy = accountObj.CreatedBy;
                    //Objaccount.ModifiedDate = accountObj.ModifiedDate;
                    //Objaccount.ModifiedBy = accountObj.ModifiedBy;
                    //Objaccount.StageID = accountObj.StageID;
                    //Objaccount.Ownership = accountObj.Ownership;
                    //Objaccount.CompanyName = accountObj.CompanyName;

                    //if (accountObj.CompanyID == 0)
                    //{
                    //    //OpporModel.CompanyID = CompanyObj.ID;
                    //    Objaccount.CompanyID = companyId;
                    //}
                    //else Objaccount.CompanyID = companyId;

                    //Objaccount.ContactID = Convert.ToInt32(accountObj.ID);
                    //Objaccount.CloseDate = accountObj.CloseDate;
                    //Objaccount.OwnerID = accountObj.OwnerID;

                    //bool OPPres = _ileadrepository.CreateLead(Objaccount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                  

                    //OpporModel.Name = accountObj.OppName;
                    //OpporModel.CreatedDate = accountObj.CreatedDate;
                    //OpporModel.CreatedBy = accountObj.CreatedBy;
                    //OpporModel.ModifiedDate = accountObj.ModifiedDate;
                    //OpporModel.ModifiedBy = accountObj.ModifiedBy;
                    //OpporModel.StageID = accountObj.StageID;
                    //OpporModel.Owner = accountObj.Ownership;
                    //OpporModel.CompanyName = accountObj.CompanyName;
                    
                    //if (accountObj.CompanyID == 0)
                    //{
                    //    //OpporModel.CompanyID = CompanyObj.ID;
                    //    OpporModel.CompanyID = companyId;
                    //}
                    //else OpporModel.CompanyID = companyId;
                    
                    //OpporModel.ContactID = Convert.ToInt32(accountObj.ID);
                    //OpporModel.CloseDate = accountObj.CloseDate;
                    //OpporModel.OwnerID = accountObj.OwnerID;
                    //bool OPPres = _opportunitiesRepo.CreateOpprtunity(OpporModel, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
        

        // To Get the List of Companies.

        public ActionResult GetallCompanies()
        {
            try
            {
                
                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult getCompaniesLookup(jQueryDataTableParamModel param)
        {
            try
            {
                
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "ID desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                int userId = -1;
                string Companykeyword = "";
                //var listcompany = _icommonrepository.GetListOfAllCompanies(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                var listcompany = _icompanyrepository.GetCompaniesIndex(Companykeyword, userId, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listcompany,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
    

        // To get the users list
        public ActionResult GetallUsers()
        {
            try
            {

                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult getUsersLookup(jQueryDataTableParamModel param)
        {
            try
            {

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


               

                var listusers = _icommonrepository.getUsersLookup(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
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



        [HttpPost]
        public bool ConvertToOpportunity(string convtoOppr)
        {
            try
            {
                Company CompanyObj = new Company();
                if (!string.IsNullOrEmpty(convtoOppr))
                {
                    foreach (var ConvLeadids in convtoOppr.Split(','))
                    {
                        Account accountObj = new Account();
                        accountObj = _ileadrepository.GetAccountDetailsByID(Convert.ToInt32(ConvLeadids), GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                        if (accountObj.CompanyID == 0)
                        {
                           
                            CompanyObj.Name = accountObj.CompanyName;
                            CompanyObj.Phone = accountObj.Phone;
                            CompanyObj.Fax = accountObj.Fax;
                            CompanyObj.Website = accountObj.Website;
                            CompanyObj.CreatedDate = accountObj.CreatedDate;
                            CompanyObj.CreatedBy = accountObj.CreatedBy;
                            CompanyObj.ModifiedDate = accountObj.ModifiedDate;
                            CompanyObj.ModifiedBy = accountObj.ModifiedBy;
                            CompanyObj.Ownership = accountObj.Ownership;
                            CompanyObj.BillingAddress = accountObj.BillingAddress;
                            CompanyObj.Billingstreet = accountObj.BillingAddress2;
                            CompanyObj.Billingcity = accountObj.Billingcity;
                            CompanyObj.BillingstateID = accountObj.BillingstateID;
                            CompanyObj.BillingcountryID = Convert.ToInt32(accountObj.BillingcountryID);
                            CompanyObj.MailingAddress = accountObj.MailingAddress;
                            CompanyObj.Shippingstreet = accountObj.MailingAddress2;
                            CompanyObj.Shippingcity = accountObj.Mailingcity;
                            CompanyObj.ShippingstateID = accountObj.MailingstateID;
                            CompanyObj.Shippingzip = accountObj.Mailingzip;
                            CompanyObj.ShippingcountryID = accountObj.MailingcountryID;
                            int Comres = _icompanyrepository.InserNewCompany(CompanyObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        }

                        accountObj.AccountTypeId = 2;
                        int res = _ileadrepository.updateLeadDetailsByLeadId(accountObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                      
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }



        // To get the users list
        public ActionResult LoadOpportunities()
        {
            try
            {
                Account accountObj = new Account();
                return PartialView(accountObj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public int GetCompanyIdbyName(string Companyname)
        {
            try
            {

                int CompanyId = 0;
                CompanyId = _icommonrepository.GetcompanyIdbyName(Companyname, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return CompanyId;

                //int result = _icontactrepository.CompleteContactActivity(historyid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpGet]
        //[ActionName("IsCompanyExists")]
        //public JsonResult IsCompanyExists(Account AccountObj)
        //{
        //    try
        //    {
        //        Company cmpModel = new Company();
        //        cmpModel.ID = AccountObj.CompanyID ?? 0;
        //        cmpModel.Name = AccountObj.CompanyName;
        //        var result = _icompanyrepository.IsCompanyExists(cmpModel, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //        if (cmpModel.ID.Equals(0))
        //        {
        //            return Json(result, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //            return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
