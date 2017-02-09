
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using CRMHub.ViewModel;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

namespace CRM.Controllers
{
    public class ManageCompanyController : ControllerBase
    {
        private readonly ICommonRepository _icommonrepository;
        private readonly ICompanyRepository _icompanyrepository;
        private readonly ILeadRepository _ileadrepository;
        private readonly IOfficeRepository _iofficerepository;
        private readonly IUserRepository _iuserrepository;
        private readonly ICustomFieldsRepository _icustomfieldrepository;
        private readonly IContactsRepository _icontactrepository;
        private readonly IDashboardData _iDashboardDatarepository;
        private readonly IInvoiceRepository _iinvoicerepository;
        private readonly IErrorLogRepository _ierrorlogrepository;
        public ManageCompanyController(ICompanyRepository icompanyrepository, IOfficeRepository iofficerepository, IUserRepository iuserrepository, ICustomFieldsRepository icustomfieldrepository, IContactsRepository icontactrepository, ILeadRepository ileadrepository, IDashboardData idashboardData, ICommonRepository icommonrepository, IInvoiceRepository iinvoicerepository, IErrorLogRepository ierrorlogrepository)
        {
            _icompanyrepository = icompanyrepository;
            _iofficerepository = iofficerepository;
            _iuserrepository = iuserrepository;
            _icustomfieldrepository = icustomfieldrepository;
            _icontactrepository = icontactrepository;
            _ileadrepository = ileadrepository;
            _iDashboardDatarepository = idashboardData;
            _icommonrepository = icommonrepository;
            _iinvoicerepository = iinvoicerepository;
            _ierrorlogrepository = ierrorlogrepository;
        }


        public ActionResult ListParentCompIndex()
        {
            return PartialView("ListParentComp");
        }



        public int ToInt(string toParse)
        {
            try
            {
                int result;
                if (int.TryParse(toParse, out result)) return result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public ActionResult IsCompanyExists(string Companyname)
        {
            try
            {
                Companyname = Companyname.Trim();
                bool objcompanyname = _icompanyrepository.CheckCompanyExistsOrNot(Companyname, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(objcompanyname, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }




        [HttpGet]
        public ActionResult DeleteCompanyActivity(int historyId)
        {
            try
            {
                int result = _icompanyrepository.DeleteCompNewActivityByHistoryId(historyId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                updatenotification();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public ActionResult Attachment(int HistoryID)
        {
            try
            {
                Session["HistoryID"] = HistoryID;
                string HistoryId = Session["HistoryID"].ToString();
                String FilePath = ConfigurationManager.AppSettings["attachmentpath"];
                FilePath = FilePath + HistoryId + '/';
                string newFolder = Server.MapPath(FilePath);
                if (Directory.Exists(newFolder))
                {
                    ViewBag.Download = "Download Doc";
                }
                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult AttachmentActivity(int HistoryID)
        {
            try
            {
                Session["HistoryIDActivity"] = HistoryID;
                string HistoryIdActivity = Session["HistoryIDActivity"].ToString();
                String FilePath = ConfigurationManager.AppSettings["attachmentActivitypath"];
                FilePath = FilePath + HistoryIdActivity + '/';
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

        [HttpPost]
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
                            string HistoryIdActivity = Session["HistoryIDActivity"].ToString();
                            string fileName = System.IO.Path.GetFileName(file.FileName);
                            string fileExtension = System.IO.Path.GetExtension(file.FileName);
                            String FilePath = ConfigurationManager.AppSettings["attachmentActivitypath"];
                            FilePath = FilePath + HistoryIdActivity + '/';
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
                string HistoryIdActivity = Session["HistoryIDActivity"].ToString();
                String FilePath = ConfigurationManager.AppSettings["attachmentActivitypath"];
                FilePath = FilePath + HistoryIdActivity + '/';
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

        [HttpPost]
        public ActionResult UploadAttachment(HttpPostedFileBase file)
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
                            string HistoryId = Session["HistoryID"].ToString();
                            string fileName = System.IO.Path.GetFileName(file.FileName);
                            string fileExtension = System.IO.Path.GetExtension(file.FileName);
                            String FilePath = ConfigurationManager.AppSettings["attachmentpath"];
                            FilePath = FilePath + HistoryId + '/';
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
                            ViewBag.Download = "Download Doc";
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

        public FileResult Download()
        {
            try
            {
                string file = string.Empty;
                string HistoryId = Session["HistoryID"].ToString();
                String FilePath = ConfigurationManager.AppSettings["attachmentpath"];
                FilePath = FilePath + HistoryId + '/';
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


        public string BuildConnectionString()
        {
            string cs = ConfigurationManager.ConnectionStrings["ImportCompanies"].ConnectionString;
            string conx = cs;
            //Session["conx"] = cs;
            //TempData["cnstr"] = cs;
            string dataSource = "";
            string providerconnectionstring = "";
            string[] parts = cs.Split(';');
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i].Trim();
                if (part.StartsWith("provider connection string="))
                {
                    providerconnectionstring = part.Replace("provider connection string", "");


                }
                if (part.StartsWith("Data Source="))
                {
                    dataSource = part.Replace("Data Source=", "");
                    break;
                }

            }
            string InitialCatalog = "";
            string[] constrgs = cs.Split(';');
            for (int i = 0; i < constrgs.Length; i++)
            {
                string constrg = constrgs[i].Trim();
                if (constrg.StartsWith("Initial Catalog="))
                {
                    InitialCatalog = GlobalVariables.ClientName;
                }

            }

            string UserID = "";
            string[] connUID = cs.Split(';');
            for (int i = 0; i < connUID.Length; i++)
            {
                string connID = connUID[i].Trim();
                if (connID.StartsWith("User ID="))
                {
                    UserID = connID.Replace("User ID=", "");
                    break;
                }

            }

            string Password = "";
            string[] conpwd = cs.Split(';');
            for (int i = 0; i < conpwd.Length; i++)
            {
                string connpwd = conpwd[i].Trim();
                if (connpwd.StartsWith("Password="))
                {
                    Password = connpwd.Replace("Password", "");
                    break;
                }
            }

            string ds = dataSource;
            string INTC = InitialCatalog;
            string User = UserID;
            string pswd = Password;
            StringBuilder Constrng = new StringBuilder("Data Source=");
            Constrng.Append(ds);
            Constrng.Append(";Initial Catalog=");
            Constrng.Append(INTC);
            Constrng.Append(";User ID=");
            Constrng.Append(User);
            Constrng.Append(";Password");
            Constrng.Append(Password);
            string strCon = Constrng.ToString();
            return strCon;
        }
        public void ExportToCsv()
        {
            try
            {
                int totalPageCount = 0;

                string CompanyList_Keyword = TempData.Peek("CompanyList_Keyword").ToString();
                string CompanyBranch = TempData.Peek("CompanyBranch").ToString();
                string CompanyStatus = Request["CompanyStatus"];
                string CompanySalesRepresentative = TempData.Peek("CompanySalesRepresentative").ToString();
                string orderByClause = TempData.Peek("orderByClause").ToString();
                totalPageCount = Convert.ToInt32(TempData.Peek("TotalPageCount").ToString());

                string ConnectionString = BuildConnectionString();

                DataSet dsCompanyList = _icompanyrepository.GetAllCompaniesListToExport(CompanyList_Keyword == "" ? null : CompanyList_Keyword, CompanyBranch == "-1" ? "All" : CompanyBranch, CompanyStatus == "-1" ? "All" : CompanyStatus, GlobalVariables.RoleID, GlobalVariables.UserID, CompanySalesRepresentative == "0" ? "All" : CompanySalesRepresentative,
                                                       null, 0, totalPageCount, orderByClause, ConnectionString);

                // var listContact = _icompanyrepository.GetAllCompaniesList(CompanyList_Keyword == "" ? null : CompanyList_Keyword, CompanyBranch == "-1" ? "All" : CompanyBranch, CompanyStatus == "-1" ? "All" : CompanyStatus, GlobalVariables.RoleID, GlobalVariables.UserID, CompanySalesRepresentative == "0" ? "All" : CompanySalesRepresentative,
                //null, 0, totalPageCount, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                    "attachment;filename=ExportCompanies.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";


                StringBuilder sb = new StringBuilder();
                DataTable dt = dsCompanyList.Tables[0];
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Columns[k].ColumnName + ',');
                }
                //append new line
                sb.Append("\r\n");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        //add separator
                        sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                    }
                    //append new line
                    sb.Append("\r\n");
                }
                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ExportToExcel()
        {
            try
            {
                int totalPageCount = 0;

                string CompanyList_Keyword = TempData.Peek("CompanyList_Keyword").ToString();
                string CompanyBranch = TempData.Peek("CompanyBranch").ToString();
                string CompanyStatus = Request["CompanyStatus"];
                string CompanySalesRepresentative = TempData.Peek("CompanySalesRepresentative").ToString();
                string orderByClause = TempData.Peek("orderByClause").ToString();
                totalPageCount = Convert.ToInt32(TempData.Peek("TotalPageCount").ToString());
                string ConnectionString = BuildConnectionString();
                DataSet dsCompanyList = _icompanyrepository.GetAllCompaniesListToExport(CompanyList_Keyword == "" ? null : CompanyList_Keyword, CompanyBranch == "-1" ? "All" : CompanyBranch, CompanyStatus == "-1" ? "All" : CompanyStatus, GlobalVariables.RoleID, GlobalVariables.UserID, CompanySalesRepresentative == "0" ? "All" : CompanySalesRepresentative,
                                                      null, 0, totalPageCount, orderByClause, ConnectionString);
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = dsCompanyList;
                grid.DataBind();

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=ExportCompanies.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Write(sw.ToString());

                Response.End();

            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost]
        public ActionResult ExportToExcel(List<ExportCompany> item)
        {
            try
            {
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = from d in item
                                  select new
                                  {
                                      CompanyId = d.CompanyId,
                                      CompanyName = d.CompanyName,
                                      CompanySite = d.CompanySite,
                                      PhoneNumber = d.PhoneNumber,
                                      City = d.City,
                                      Zip = d.Zip,
                                      DateCreated = d.DateCreated,
                                      Type = d.Type
                                  };
                grid.DataBind();
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                string fullpath = Path.Combine(Server.MapPath("~/Content/CompaniesExport/"));
                if (System.IO.File.Exists(fullpath + "ExportCompanies.xls"))
                    System.IO.File.Delete(fullpath + "ExportCompanies.xls");
                string renderedGridView = sw.ToString();
                System.IO.File.AppendAllText(fullpath + "ExportCompanies.xls", renderedGridView);
                ViewBag.ExportCompaniesExcel = "2";
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult DownloadCompaniesExportExcel()
        {
            try
            {
                string file = "ExportCompanies.xls";
                string fullPath = Path.Combine(Server.MapPath("~/Content/CompaniesExport/"), file);
                return File(fullPath, "application/vnd.ms-excel", file);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult SelectCompany()
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



        [HttpGet]
        public ActionResult RedirectActvtyGrid()
        {
            TempData["EditActcont"] = "Editcompanyactivity";
            int cmpatid = Convert.ToInt32(Session["AccountId"]);
            return RedirectToAction("EditCompany", new { AccountId = cmpatid });

        }

        public void updatenotification()
        {
            try
            {
                string empid = GlobalVariables.UserID;
                string Status = "1";
                int totalPageCount = 0;
                List<Activity> lstActivity = _iDashboardDatarepository.GetUserActivityNotification(empid, Status, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < lstActivity.Count; i++)
                {
                    DateTime fdate = DateTime.Now;
                    //DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));
                
                    //DateTime fdate = DateTime.ParseExact(DateTime.Now.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    //DateTime rminddate = DateTime.ParseExact(lstActivity[i].DueDate.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    DateTime Datemodel = Convert.ToDateTime(lstActivity[i].DueDate);
                    DateTime rminddate = Datemodel;
                    TimeSpan differenced = rminddate - fdate;
                    int daysd = Convert.ToInt32(differenced.TotalDays);
                    lstActivity[i].daysdiff = daysd;
                    lstActivity[i].CurrentDate = fdate;
                }

                Session["Notifications"] = lstActivity;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ActionResult GetContactListforProjects()
        {
            try
            {


                return PartialView();
                //List<tbl_Contact> lstParentCont = _icompanyrepository.GetAllContacts(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //return PartialView("ListContactforactivity", lstParentCont);
                //return PartialView("ListContactforactivity");
            }
            catch (Exception)
            {
                throw;
            }

        }



        // ZOho
        public ActionResult Index()
        {
            try
            {
                if (TempData["CreateCompany"] != null)
                {
                    ViewBag.CreateCompany = "CreateCompany";

                }
                //TempData["CreateCompany"]
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult getCompanies(jQueryDataTableParamModel param)
        {
            try
            {
                string Companykeyword = Request["CompanyList_Keyword"].TrimStart();
                string OwnerName = Request["Company_Owner"].TrimStart();
                //
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
                if (OwnerName != "")
                {
                    int guserid = 0;
                    string Ownerusr = OwnerName.Replace(" ", "").ToLower();
                    userId = _icommonrepository.GetUserIdFrmNamepName(Ownerusr, out guserid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                }



                var companieslist = _icompanyrepository.GetCompaniesIndex(Companykeyword, userId, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = companieslist,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public ActionResult CreateNewCompany()
        {
            try
            {


                string Module = "company";

                CompanyViewModel CompanyMdl = new CompanyViewModel();
                CompanyMdl.CompanyObj = new Company();
                CompanyMdl.CompanyObj.RoleID = Convert.ToInt32(GlobalVariables.RoleID);
                Company cmpobject = new Company();
                int UserId = Convert.ToInt32(GlobalVariables.UserID);
                string userfullname = _icommonrepository.GetusernameById(UserId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                CompanyMdl.CompanyObj.OwnerID = UserId;
                CompanyMdl.CompanyObj.Ownership = userfullname;

                cmpobject.Ownership = userfullname;
                cmpobject.OwnerID = UserId;


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




                CompanyMdl.listCustomfields = _icustomfieldrepository.GetCustomFieldsByModule(Module, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < CompanyMdl.listCustomfields.Count(); i++)
                {
                    if (CompanyMdl.listCustomfields[i].Column_Type == "dropdown")
                    {
                        CompanyMdl.listCustomfields[i].DefaultValue = CompanyMdl.listCustomfields[i].lstCustomOptions.Where(l => l.IsDefault == true).Select(s => s.DrpValueId).SingleOrDefault().ToString();
                    }
                }
                //return View(cmpobject);
                return View(CompanyMdl);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public ActionResult CreateNewCompany(CompanyViewModel companyModel)
        {
            try
            {
                TempData["CreateCompany"] = "CreateCompany";
                /// New Company Details Insertion
                # region New Company Details Insertion
                //if (companyObj.OptEmailOut == true)
                //{
                //    companyObj.EmailOptOut = "true";
                //}
                //else companyObj.EmailOptOut = "false";

                DateTime CreatedDate = DateTime.Now;
                companyModel.CompanyObj.CreatedDate = CreatedDate;
                companyModel.CompanyObj.ModifiedDate = CreatedDate;
                companyModel.CompanyObj.CreatedBy = Convert.ToInt32(GlobalVariables.UserID);
                companyModel.CompanyObj.ModifiedBy = Convert.ToInt32(GlobalVariables.UserID);
                int CompanyId = _icompanyrepository.InserNewCompany(companyModel.CompanyObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                # endregion

                // For inserting the Custom Fileds Data
                # region Customfields Insertion
                for (int i = 0; i < companyModel.listCustomfields.Count; i++)
                {
                    if (companyModel.listCustomfields[i].Column_Type == "radiobutton")
                    {
                        Custom_Value_Master CVItem = new Custom_Value_Master();
                        string SelectedRadiobtnId = companyModel.listCustomfields[i].DefaultValue;

                        for (int a = 0; a < companyModel.listCustomfields[i].lstCustomOptions.Count; a++)
                        {

                            if (companyModel.listCustomfields[i].lstCustomOptions[a].DrpValueId.ToString() == SelectedRadiobtnId)
                            {
                                companyModel.listCustomfields[i].lstCustomOptions[a].IsDefault = Convert.ToBoolean(1);
                                CVItem.CustomFieldvalue = SelectedRadiobtnId;
                                CVItem.CreatedDate = DateTime.Now;
                                CVItem.ModifiedDate = DateTime.Now;
                                CVItem.Module = "company";
                                CVItem.MasterFieldId = companyModel.listCustomfields[i].FieldId;
                                CVItem.UserID = Convert.ToInt32(GlobalVariables.UserID);
                                CVItem.ModuleRecordIdTmp = CompanyId;

                                bool CFSuccess = _icustomfieldrepository.InsertCustomField(CVItem, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }
                            else
                            {
                                companyModel.listCustomfields[i].lstCustomOptions[a].IsDefault = Convert.ToBoolean(0);
                            }
                            // send drpId and Update Isdefault value ;/ Have to Update the Custom_Manage_Master with DefaultValue With CustomFieldValue
                            // _icustomfieldrepository.UpdateCustomOptionsForCheckBox(cmpModel.listCustomfields[i].lstCustomOptions[a], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        }
                    }

                    else
                    {
                        Custom_Value_Master CVItem = new Custom_Value_Master();
                        CVItem.CreatedDate = DateTime.Now;
                        CVItem.ModifiedDate = DateTime.Now;
                        CVItem.Module = "company";
                        CVItem.MasterFieldId = companyModel.listCustomfields[i].FieldId;
                        CVItem.UserID = Convert.ToInt32(GlobalVariables.UserID);
                        // CVItem.ModuleRecordId = result;
                        CVItem.ModuleRecordIdTmp = CompanyId;
                        CVItem.CustomFieldvalue = companyModel.listCustomfields[i].DefaultValue;

                        bool CFSuccess = _icustomfieldrepository.InsertCustomField(CVItem, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }


                }

                # endregion

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public ActionResult getCompanydetails(int CompanyId)
        {
            try
            {
                CompanyViewModel companyMdl = new CompanyViewModel();
                //  Company ObjCompany = new Company();
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


                companyMdl.CompanyObj = _icompanyrepository.GetCompnayDetailsByID(CompanyId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                //ObjCompany = _icompanyrepository.GetCompnayDetailsByID(CompanyId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                if (companyMdl.CompanyObj.AnnualRevenue == null)
                {
                    companyMdl.CompanyObj.AnnualRevenue = "0";
                }
                if (companyMdl.CompanyObj.Employees == null)
                    companyMdl.CompanyObj.Employees = "0";




                # region Get Custom fields Based on CompanyID

                string Modulename = "Company";
                //cmpModel.lstcustomVM = _icustomfieldrepository.GetCustomValueBasedonModule(AccountId, Modulename, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                companyMdl.lstcustomVM = _icustomfieldrepository.GetCustomValueBasedonModule(CompanyId, Modulename, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                for (int i = 0; i < companyMdl.lstcustomVM.Count(); i++)
                {

                    if (companyMdl.lstcustomVM[i].CValueId == 0)  // new Custom fields 
                    {
                        if (companyMdl.lstcustomVM[i].Column_Type == "dropdown")
                        {
                            companyMdl.lstcustomVM[i].DefaultValue = companyMdl.lstcustomVM[i].lstCustomOptionsVal.Where(l => l.IsDefault == true).Select(s => s.DrpValueId).SingleOrDefault().ToString();
                        }
                        else if (companyMdl.lstcustomVM[i].Column_Type != "dropdown" && companyMdl.lstcustomVM[i].Column_Type != "radiobutton")
                        {
                            companyMdl.lstcustomVM[i].CustomFieldvalue = companyMdl.lstcustomVM[i].DefaultValue;
                        }
                    }
                    else // Existing Custom Value master 
                    {
                        if (companyMdl.lstcustomVM[i].Column_Type == "dropdown")
                        {
                            //   cmpModel.lstcustomVM[i].DefaultValue = cmpModel.lstcustomVM[i].lstCustomOptionsVal.Where(l => l.IsDefault == true).Select(s => s.DrpValueId).SingleOrDefault().ToString();
                            companyMdl.lstcustomVM[i].DefaultValue = companyMdl.lstcustomVM[i].CustomFieldvalue;
                        }
                        else if (companyMdl.lstcustomVM[i].Column_Type == "radiobutton")
                        {
                            for (int k = 0; k < companyMdl.lstcustomVM[i].lstCustomOptionsVal.Count; k++)
                            {
                                if (companyMdl.lstcustomVM[i].CustomFieldvalue == companyMdl.lstcustomVM[i].lstCustomOptionsVal[k].DrpValueId.ToString())
                                {
                                    companyMdl.lstcustomVM[i].lstCustomOptionsVal[k].IsDefault = true;
                                }
                                else
                                {
                                    companyMdl.lstcustomVM[i].lstCustomOptionsVal[k].IsDefault = false;
                                }


                            }

                        }
                    }

                }

                # endregion


                if (TempData["UpdateCompany"] != null)
                {
                    ViewBag.CompanyUpdate = "UpdateCompany";

                }

                return View("getCompanydetails", companyMdl);

            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public ActionResult _EditCompanybyID(int CompanyId)
        {
            try
            {
                CompanyViewModel companyMdl = new CompanyViewModel();

                //  Company ObjCompany = new Company();
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
                companyMdl.CompanyObj = _icompanyrepository.GetCompnayDetailsByID(CompanyId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                if (companyMdl.CompanyObj.AnnualRevenue == null)
                {
                    companyMdl.CompanyObj.AnnualRevenue = "0";
                }
                if (companyMdl.CompanyObj.Employees == null)
                    companyMdl.CompanyObj.Employees = "0";

                # region Get Custom fields Based on CompanyID

                string Modulename = "Company";
                //cmpModel.lstcustomVM = _icustomfieldrepository.GetCustomValueBasedonModule(AccountId, Modulename, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                companyMdl.lstcustomVM = _icustomfieldrepository.GetCustomValueBasedonModule(CompanyId, Modulename, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                for (int i = 0; i < companyMdl.lstcustomVM.Count(); i++)
                {

                    if (companyMdl.lstcustomVM[i].CValueId == 0)  // new Custom fields 
                    {
                        if (companyMdl.lstcustomVM[i].Column_Type == "dropdown")
                        {
                            companyMdl.lstcustomVM[i].DefaultValue = companyMdl.lstcustomVM[i].lstCustomOptionsVal.Where(l => l.IsDefault == true).Select(s => s.DrpValueId).SingleOrDefault().ToString();
                        }
                        else if (companyMdl.lstcustomVM[i].Column_Type != "dropdown" && companyMdl.lstcustomVM[i].Column_Type != "radiobutton")
                        {
                            companyMdl.lstcustomVM[i].CustomFieldvalue = companyMdl.lstcustomVM[i].DefaultValue;
                        }
                    }
                    else // Existing Custom Value master 
                    {
                        if (companyMdl.lstcustomVM[i].Column_Type == "dropdown")
                        {
                            //   cmpModel.lstcustomVM[i].DefaultValue = cmpModel.lstcustomVM[i].lstCustomOptionsVal.Where(l => l.IsDefault == true).Select(s => s.DrpValueId).SingleOrDefault().ToString();
                            companyMdl.lstcustomVM[i].DefaultValue = companyMdl.lstcustomVM[i].CustomFieldvalue;
                        }
                        else if (companyMdl.lstcustomVM[i].Column_Type == "radiobutton")
                        {
                            for (int k = 0; k < companyMdl.lstcustomVM[i].lstCustomOptionsVal.Count; k++)
                            {
                                if (companyMdl.lstcustomVM[i].CustomFieldvalue == companyMdl.lstcustomVM[i].lstCustomOptionsVal[k].DrpValueId.ToString())
                                {
                                    companyMdl.lstcustomVM[i].lstCustomOptionsVal[k].IsDefault = true;
                                }
                                else
                                {
                                    companyMdl.lstcustomVM[i].lstCustomOptionsVal[k].IsDefault = false;
                                }


                            }

                        }
                    }

                }

                # endregion




                return PartialView(companyMdl);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public ActionResult UpdateCompanyDetails(CompanyViewModel companyMdl)
        {
            try
            {
                TempData["UpdateCompany"] = "UpdateCompany";
                DateTime ModifiedDate = DateTime.Now;

                companyMdl.CompanyObj.ModifiedDate = ModifiedDate;
                companyMdl.CompanyObj.ModifiedBy = Convert.ToInt32(GlobalVariables.UserID);
                int res = _icompanyrepository.updateCompanyDetailsByCompanyId(companyMdl.CompanyObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                int companyId = companyMdl.CompanyObj.ID;



                for (int i = 0; i < companyMdl.lstcustomVM.Count; i++)
                {

                    if (companyMdl.lstcustomVM[i].Column_Type == "radiobutton")
                    {
                        string SelectedRadiobtnId = companyMdl.lstcustomVM[i].DefaultValue;

                        for (int a = 0; a < companyMdl.lstcustomVM[i].lstCustomOptionsVal.Count; a++)
                        {
                            if (companyMdl.lstcustomVM[i].CValueId != 0)
                            {
                                if (companyMdl.lstcustomVM[i].lstCustomOptionsVal[a].DrpValueId.ToString() == SelectedRadiobtnId)
                                {
                                    companyMdl.lstcustomVM[i].lstCustomOptionsVal[a].IsDefault = Convert.ToBoolean(1);
                                    companyMdl.lstcustomVM[i].ModifiedDate = DateTime.Now;
                                    companyMdl.lstcustomVM[i].CustomFieldvalue = SelectedRadiobtnId;
                                    _icustomfieldrepository.UpdateCustomField(companyMdl.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                                }
                                //else
                                //{
                                //    CompanyModel.lstcustomVM[i].lstCustomOptionsVal[a].IsDefault = Convert.ToBoolean(0);
                                //}
                            }

                            else
                            {
                                // Here we have to insert for new Custom Field if added extra
                                companyMdl.lstcustomVM[i].CreatedDate = DateTime.Now;
                                companyMdl.lstcustomVM[i].ModifiedDate = DateTime.Now;
                                companyMdl.lstcustomVM[i].Module = "company";
                                companyMdl.lstcustomVM[i].ModuleRecordIdTmp = companyMdl.CompanyObj.ID;
                                companyMdl.lstcustomVM[i].MasterFieldId = companyMdl.lstcustomVM[i].FieldId;
                                companyMdl.lstcustomVM[i].UserID = Convert.ToInt32(GlobalVariables.UserID);

                                companyMdl.lstcustomVM[i].lstCustomOptionsVal[a].IsDefault = Convert.ToBoolean(1);
                                //CompanyModel.lstcustomVM[i].ModifiedDate = DateTime.Now;
                                companyMdl.lstcustomVM[i].CustomFieldvalue = SelectedRadiobtnId;
                                _icustomfieldrepository.InsertCustomField(companyMdl.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }

                        }
                    }
                    else
                    {
                        companyMdl.lstcustomVM[i].UserID = Convert.ToInt32(GlobalVariables.UserID);
                        if (companyMdl.lstcustomVM[i].CValueId != 0) // Update
                        {
                            if (companyMdl.lstcustomVM[i].Column_Type == "dropdown")
                                companyMdl.lstcustomVM[i].CustomFieldvalue = companyMdl.lstcustomVM[i].DefaultValue;
                            companyMdl.lstcustomVM[i].ModifiedDate = DateTime.Now;
                            _icustomfieldrepository.UpdateCustomField(companyMdl.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        }

                        else // Insert 
                        {
                            companyMdl.lstcustomVM[i].CreatedDate = DateTime.Now;
                            companyMdl.lstcustomVM[i].ModifiedDate = DateTime.Now;
                            companyMdl.lstcustomVM[i].Module = "company";
                            companyMdl.lstcustomVM[i].ModuleRecordIdTmp = companyMdl.CompanyObj.ID;
                            companyMdl.lstcustomVM[i].MasterFieldId = companyMdl.lstcustomVM[i].FieldId;
                            _icustomfieldrepository.InsertCustomField(companyMdl.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        }

                    }

                }






                return RedirectToAction("getCompanydetails", new { CompanyId = companyId });
                // return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [ActionName("IsCompanyExists")]
        public JsonResult IsCompanyExists(CompanyViewModel cmpModel)
        {
            try
            {
                var result = _icompanyrepository.IsCompanyExists(cmpModel.CompanyObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                if (cmpModel.CompanyObj.ID.Equals(0))
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
        public ActionResult CompanyActivity()
        {
            try
            {
                ActivityModel ACCmodel = new ActivityModel();

                // Activity Types
                activity_types selectoption = new activity_types();
                //selectoption.Name = "Select";
                //selectoption.Aid = -1;
                //ACCmodel.TypeList = new List<activity_types>();
                //ACCmodel.TypeList.Add(selectoption);
                ACCmodel.TypeList.AddRange(_icommonrepository.GetAllActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                ACCmodel.ListModuletype.AddRange(_icommonrepository.GetAllModuleType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                // Status Types list 
                Status selectStatusOption = new Status();
                //selectStatusOption.StatusName = "Select";
                //selectStatusOption.ID =-1;
                //ACCmodel.StatusList = new List<Status>();
                //ACCmodel.StatusList.Add(selectStatusOption);
                ACCmodel.StatusList.AddRange(_icommonrepository.GetAllStatusTypes(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                // Priority Type list
                Priority SelectPrioirityOption = new Priority();
                //SelectPrioirityOption.PriorityName = "Select";
                //SelectPrioirityOption.ID = -1;
                //ACCmodel.ListPriority = new List<Priority>();
                //ACCmodel.ListPriority.Add(SelectPrioirityOption);
                ACCmodel.ListPriority.AddRange(_icommonrepository.GetAllPrioriTypes(GlobalVariables.ConnectionString, GlobalVariables.ClientName));


                //int UserId = Convert.ToInt32(GlobalVariables.UserID);
                //string userfullname = _icommonrepository.GetusernameById(UserId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //ACCmodel.activity.AssignedName = userfullname;
                //ACCmodel.activity.AssignedTo = UserId;
                return View(ACCmodel);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public ActionResult NewCCompanyActivity(string CompanyId)
        {
            try
            {
                ActivityModel ACCmodel = new ActivityModel();

                // Activity Types
                activity_types selectoption = new activity_types();
                selectoption.Name = "Select";
                selectoption.Aid = -1;
                ACCmodel.TypeList = new List<activity_types>();
                ACCmodel.TypeList.Add(selectoption);
                ACCmodel.TypeList.AddRange(_icommonrepository.GetAllActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));


                ACCmodel.ListModuletype.AddRange(_icommonrepository.GetAllModuleType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                // Status Types list 
                Status selectStatusOption = new Status();
                //selectStatusOption.StatusName = "Select";
                //selectStatusOption.ID = -1;
                //ACCmodel.StatusList = new List<Status>();
                //ACCmodel.StatusList.Add(selectStatusOption);
                ACCmodel.StatusList.AddRange(_icommonrepository.GetAllStatusTypes(GlobalVariables.ConnectionString, GlobalVariables.ClientName));


                // Priority Type list
                Priority SelectPrioirityOption = new Priority();
                //SelectPrioirityOption.PriorityName = "Select";
                //SelectPrioirityOption.ID = -1;
                //ACCmodel.ListPriority = new List<Priority>();
                //ACCmodel.ListPriority.Add(SelectPrioirityOption);
                ACCmodel.ListPriority.AddRange(_icommonrepository.GetAllPrioriTypes(GlobalVariables.ConnectionString, GlobalVariables.ClientName));


                int UserId = Convert.ToInt32(GlobalVariables.UserID);
                string userfullname = _icommonrepository.GetusernameById(UserId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                ACCmodel.activity.AssignedName = userfullname;
                ACCmodel.activity.AssignedTo = UserId;

                ACCmodel.activity.ModuleName = _icommonrepository.GetCompanynameById(Convert.ToInt32(CompanyId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                return View("CompanyActivity", ACCmodel);
                //return View(ACCmodel);


            }
            catch (Exception)
            {
                throw;
            }
        }




        [HttpGet]
        public ActionResult CompanyActivities()
        {
            try
            {
                DateTime loadedDate = DateTime.Now;
                string start = Request.QueryString["dateField"];
                if (start != "")
                {
                    loadedDate = DateTime.ParseExact(start, "dd/MM/yyyy",
                                                   CultureInfo.InvariantCulture);
                }


                ActivityModel ACCmodel = new ActivityModel();

                // Activity Types
                activity_types selectoption = new activity_types();
                selectoption.Name = "Select";
                selectoption.Aid = -1;
                ACCmodel.TypeList = new List<activity_types>();
                ACCmodel.TypeList.Add(selectoption);
                ACCmodel.TypeList.AddRange(_icommonrepository.GetAllActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                // Status Types list 
                Status selectStatusOption = new Status();
                selectStatusOption.StatusName = "Select";
                selectStatusOption.ID = -1;
                ACCmodel.StatusList = new List<Status>();
                ACCmodel.StatusList.Add(selectStatusOption);
                ACCmodel.StatusList.AddRange(_icommonrepository.GetAllStatusTypes(GlobalVariables.ConnectionString, GlobalVariables.ClientName));


                // Priority Type list
                Priority SelectPrioirityOption = new Priority();
                SelectPrioirityOption.PriorityName = "Select";
                SelectPrioirityOption.ID = -1;
                ACCmodel.ListPriority = new List<Priority>();
                ACCmodel.ListPriority.Add(SelectPrioirityOption);
                ACCmodel.ListPriority.AddRange(_icommonrepository.GetAllPrioriTypes(GlobalVariables.ConnectionString, GlobalVariables.ClientName));



                int UserId = Convert.ToInt32(GlobalVariables.UserID);
                string userfullname = _icommonrepository.GetusernameById(UserId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                ACCmodel.activity.AssignedName = userfullname;
                ACCmodel.activity.AssignedTo = UserId;
                //CompanyModel cmpModel = new CompanyModel();

                //int AccountId = Convert.ToInt32(Session["AccountId"]);
                //tbl_account objTblacnt = _icompanyrepository.editCompanyByAccountId(AccountId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //List<user> lstSales = _icompanyrepository.GetAdmin(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                // cmpModel.history = new tbl_history();
                if (start != "")
                {
                    //  cmpModel.history.StartDate = loadedDate;
                    //cmpModel.history.RemindDate = loadedDate.AddDays(-15);
                    //cmpModel.history.StartDate = Convert.ToDateTime(cmpModel.history.RemindDate).AddDays(+15);
                    ACCmodel.activity.RemindDate = loadedDate.AddDays(-15);
                    ACCmodel.activity.DueDate = Convert.ToDateTime(ACCmodel.activity.RemindDate).AddDays(+15);

                }
                ACCmodel.ListModuletype.AddRange(_icommonrepository.GetAllModuleType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
               
                return View("CompanyActivity", ACCmodel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult CompanyActivity(ActivityModel actmodel, FormCollection fc)
        {
            //int result = 0;
            try
            {
                TempData["CreatedActivity"] = "CreatedActivity";

                actmodel.activity.AssignedTo = (int?)Convert.ToInt32(fc["activity.AssignedTo"]);

                if (fc["activity.DueDate"] != null && fc["activity.DueDate"].ToString() != string.Empty)
                {
                    DateTime dtDuedate = DateTime.ParseExact(fc["activity.DueDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    actmodel.activity.DueDate = Convert.ToDateTime(dtDuedate);
                }

                if (fc["activity.RemindDate"] != null && fc["activity.RemindDate"].ToString() != string.Empty)
                {
                    DateTime dtRemDate = DateTime.ParseExact(fc["activity.RemindDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    actmodel.activity.RemindDate = Convert.ToDateTime(dtRemDate);
                }
                DateTime CreatedDate = DateTime.Now;
                //actmodel.activity.ActivityTypeID=
                actmodel.activity.CreatedDate = CreatedDate;
                actmodel.activity.ModifiedDate = CreatedDate;
                actmodel.activity.Createdby = Convert.ToInt32(GlobalVariables.UserID);
                actmodel.activity.Modifiedby = Convert.ToInt32(GlobalVariables.UserID);


                actmodel.activity.NotificationFlag = false;
                bool ComActres = _icompanyrepository.InserCompanyActivity(actmodel.activity, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                updatenotification();
                return RedirectToAction("ActivitygridBind");
            }
            catch (Exception ex)
            {
                Error_Log errorLog = new Error_Log();
                errorLog.ErrorMessage = ex.Message;
                errorLog.CreatedDate = DateTime.Now;
                errorLog.ErrorPage = "Manage Company";
                _ierrorlogrepository.InsertErrorLogException(errorLog);
                return null;
            }

        }

        public ActionResult ActivitygridBind(ActivityModel actmodel)
        {
            try
            {
                if (TempData["CreatedActivity"] != null && TempData["CreatedActivity"].ToString() != "")
                {
                    ViewBag.CreatedActivity = "Activity";

                }
                if (TempData["UpdateCompanyActivity"] != null && TempData["UpdateCompanyActivity"].ToString()!="")
                {
                    ViewBag.UpdateActivity = "UpdateActivity";
                }
                ViewBag.roleid = GlobalVariables.RoleID;
                return View(actmodel);
            }
            catch (Exception)
            {
                throw;
            }

        }


        public ActionResult getActivities(jQueryDataTableParamModel param)
        {
            try
            {
                string Keyword = Request["Keyword"];
                string Type = Request["Type"];
                string Priority = Request["Priority"];
                string Status = Request["Status"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "1")
                {
                    orderByClause = "ACT.CreatedDate desc";
                }
                else
                {
                    if (SortFieldName == "ModuleName")
                    {
                        SortFieldName = "MType.AccountTypeName";
                    }
                    else if (SortFieldName == "ActivityName")
                    {
                        SortFieldName = "AT.Name";
                    }
                    else if (SortFieldName == "Priority")
                    {
                        SortFieldName = "P.PriorityName";
                    }
                    else if (SortFieldName == "Status")
                    {
                        SortFieldName = "S.StatusName";
                    }
                    orderByClause = SortFieldName + " " + sSortDir_0;
                }

                //  int totalPageCount = 0;

                    var listActivity = _icompanyrepository.GetListOfallActivities(Keyword, Convert.ToInt32(GlobalVariables.UserID), Convert.ToInt32(GlobalVariables.RoleID), param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                    TempData["CreatedActivity"] = "";
                    TempData["UpdateCompanyActivity"] = "";
                //  updatenotification();
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listActivity,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult EditActivityCmpgrid(int activityID)
        {
            try
            {
                ActivityModel ACCmodel = new ActivityModel();

                // Activity Types
                activity_types selectoption = new activity_types();
                //selectoption.Name = "Select";
                //selectoption.Aid = -1;
                //ACCmodel.TypeList = new List<activity_types>();
                //ACCmodel.TypeList.Add(selectoption);
                ACCmodel.TypeList.AddRange(_icommonrepository.GetAllActivityType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                // Status Types list 
                Status selectStatusOption = new Status();
                //selectStatusOption.StatusName = "Select";
                //selectStatusOption.ID = -1;
                //ACCmodel.StatusList = new List<Status>();
                //ACCmodel.StatusList.Add(selectStatusOption);
                ACCmodel.StatusList.AddRange(_icommonrepository.GetAllStatusTypes(GlobalVariables.ConnectionString, GlobalVariables.ClientName));


                // Priority Type list
                Priority SelectPrioirityOption = new Priority();
                //SelectPrioirityOption.PriorityName = "Select";
                //SelectPrioirityOption.ID = -1;
                //ACCmodel.ListPriority = new List<Priority>();
                //ACCmodel.ListPriority.Add(SelectPrioirityOption);
                ACCmodel.ListPriority.AddRange(_icommonrepository.GetAllPrioriTypes(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                ACCmodel.activity = _icommonrepository.GetActivityDetailsByID(activityID, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                ACCmodel.ListModuletype.AddRange(_icommonrepository.GetAllModuleType(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                if (TempData["UpdateCompanyActivity"] != null)
                {
                    ViewBag.UpdateCompanyActivity = "UpdateCompanyActivity";

                }

                return View(ACCmodel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult EditActivityCmpgrid(ActivityModel actmodel,FormCollection fc)
        {
            try
            {
                int CompActivityID = actmodel.activity.ID;
                TempData["UpdateCompanyActivity"] = "UpdateCompanyActivity";
                DateTime modifiedDate = DateTime.Now;
                actmodel.activity.ModifiedDate = modifiedDate;
                actmodel.activity.Modifiedby = Convert.ToInt32(GlobalVariables.UserID);
                actmodel.activity.NotificationFlag = false;
               
                actmodel.activity.AssignedTo = (int?)Convert.ToInt32(fc["activity.AssignedTo"]);

                if (fc["activity.DueDate"] != null && fc["activity.DueDate"].ToString() != string.Empty)
                {
                    DateTime dtDuedate = DateTime.ParseExact(fc["activity.DueDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    actmodel.activity.DueDate = Convert.ToDateTime(dtDuedate);
                }

                if (fc["activity.RemindDate"] != null && fc["activity.RemindDate"].ToString() != string.Empty)
                {
                    DateTime dtRemDate = DateTime.ParseExact(fc["activity.RemindDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    actmodel.activity.RemindDate = Convert.ToDateTime(dtRemDate);
                } 

                int res = _icommonrepository.updateActivityDetailsByActivityId(actmodel.activity, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                updatenotification();

                return RedirectToAction("ActivitygridBind", "ManageCompany");
                //return RedirectToAction("ActivitygridBind");
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public bool CompleteActivity(string activityId)
        {
            try
            {
                // var ClientEntity = new DevEntities();
                var ClientEntity = new DevEntities();
                if (!string.IsNullOrEmpty(activityId))
                {
                    int result = _icompanyrepository.CompleteActivity(Convert.ToInt32(activityId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                }
                updatenotification();
                return true;
                //return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost]
        public JsonResult DeleteActivities(List<Activity> HistData)
        {
            try
            {
                int result = 0;
                var Res = "";
                var ClientEntity = new DevEntities();
                for (int i = 0; i < HistData.Count; i++)
                {
                    if (!string.IsNullOrEmpty(HistData[i].ID.ToString()))
                    {

                        result = _icompanyrepository.DeleteActivityByActivityId(Convert.ToInt32(HistData[i].ID), GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                        //if (HistData[i].ModuleType == "Company" || HistData[i].ModuleType == "Contact")
                        //{
                        //    // result = _icompanyrepository.DeleteCompNewActivityByHistoryId(Convert.ToInt32(HistData[i].HistoryId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        //}
                        //else if (HistData[i].ModuleType == "Leads" || HistData[i].ModuleType == "Opportunities")
                        //{
                        //    // result = _ileadrepository.DeleteLeadActivity(Convert.ToInt32(HistData[i].HistoryId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        //}

                    }

                }
                if (result == 1 || result == 2)
                {
                    updatenotification();
                    Res = "True";
                }
                return Json(Res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpPost]
        public bool DeleteEachActivities(string historyIds, string Module)
        {
            try
            {
                int result = 0;
                var ClientEntity = new DevEntities();
                if (!string.IsNullOrEmpty(historyIds))
                {
                    result = _icompanyrepository.DeleteActivityByActivityId(Convert.ToInt32(historyIds), GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                }
                updatenotification();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult ListContactforactivity(string companyid)
        {
            try
            {
                List<Account> lstParentCont = _icompanyrepository.GetAllContactsbyCompanyID(Convert.ToInt32(companyid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < lstParentCont.Count; i++)
                {
                    lstParentCont[i].FirstName = lstParentCont[i].FirstName + ' ' + lstParentCont[i].LastName;
                }
                return PartialView(lstParentCont);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// To Bind the Contacts in Edit Company based on the CompanyID
        public ActionResult getCompanyContact(jQueryDataTableParamModel param)
        {
            try
            {
                // string CompanyID = Request["CompanyID"].TrimStart();
                string CompanyID = Request["CompanyID"];
                //string CompanyName = Request["CompanyName"].TrimStart();
                //string OwnerName = Request["OwnerName"].TrimStart();

                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "Id desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;

                int CompID = Convert.ToInt32(CompanyID);
                //int CompanyId = 0;
                //int CompanyId = -1;
                //if (CompanyName != "")
                //{
                //    CompanyId = _icommonrepository.GetcompanyIdbyName(CompanyName, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //}

                //int userId = -1;
                //if (OwnerName != "")
                //{
                //    int guserid = 0;
                //    string Ownerusr = OwnerName.Replace(" ", "").ToLower();
                //    userId = _icommonrepository.GetUserIdFrmNamepName(Ownerusr, out guserid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //}

                List<Account> Contactlist = _icontactrepository.GetAllCompanyContactsbyCompID(CompID, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                // List<Account> Contactlist = _icontactrepository.GetAllAccountContacts(Contactkeyword, CompanyId, userId, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int l = 0; l < Contactlist.Count; l++)
                {
                    Contactlist[l].ContactName = Contactlist[l].FirstName + " " + Contactlist[l].LastName;
                    Contactlist[l].Ownership = Contactlist[l].OwnerFirstName + " " + Contactlist[l].OwnerLastName;
                }
                //List<Accounts> Contactlist = new List<Accounts>();
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = Contactlist,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// To Bind the Activities Based on the Company ID 
        /// 
        public ActionResult getCompanyActivities(jQueryDataTableParamModel param)
        {
            try
            {
                string Company = Request["CompanyID"];
                string Keyword = Request["Keyword"];
                string Type = Request["Type"];
                string Priority = Request["Priority"];
                string Status = Request["Status"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "CreatedDate desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;

                //  int totalPageCount = 0; GetListOfallCompanyActivitiesbyID
                int CompanyID = Convert.ToInt32(Company);
                int Accounttype = 3;
                string Module = "Company";
                var listActivity = _icompanyrepository.GetListOfallCompanyActivitiesbyID(CompanyID, Accounttype, Company, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //var listActivity = _icompanyrepository.GetListOfallActivities(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


                //  updatenotification();
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listActivity,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// to get the Activities based on Company
        /// 
        //public ActionResult getCompanyActivities(jQueryDataTableParamModel param)
        //{
        //    try
        //    {
        //        string Company = Request["CompanyID"];
        //        string Keyword = Request["Keyword"];
        //        string Type = Request["Type"];
        //        string Priority = Request["Priority"];
        //        string Status = Request["Status"];
        //        string iSortCol_0 = Request["iSortCol_0"];
        //        string sSortDir_0 = Request["sSortDir_0"];
        //        string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
        //        string orderByClause = string.Empty;
        //        int totalPageCount = 0;
        //        if (iSortCol_0 == "0")
        //            orderByClause = "CreatedDate desc";
        //        else
        //            orderByClause = SortFieldName + " " + sSortDir_0;

        //        //  int totalPageCount = 0;

        //        var listActivity = _icompanyrepository.GetListOfallActivities(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


        //        //  updatenotification();
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


        /// to Get Company Invoices
        /// 
        public ActionResult getCompanyInvoicelist(jQueryDataTableParamModel param)
        {
            try
            {
                string Company = Request["CompanyID"];
                string invoiceList = Request["b_invoice"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "InvoiceId desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;

                int companyID = Convert.ToInt32(Company);
                var listinvoice = _iinvoicerepository.GetAllInvoiceListByCustomer(companyID, invoiceList, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                // var listinvoice = _iinvoicerepository.GetAllInvoiceList(invoiceList, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < listinvoice.Count; i++)
                {

                    if (listinvoice[i].InvoiceType == "Estimate - Invoice")
                    {
                        listinvoice[i].InvoiceType = "Estimate - Invoice / " + listinvoice[i].EstInvoiceNo;
                    }
                    if (listinvoice[i].Posted == 1)
                    {
                        listinvoice[i].Status = "Posted";
                    }

                }

                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listinvoice,
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
        public JsonResult AutoComplete(string Prefix,string moduleid)
        {
            Int64 roleid = 0;
            if(moduleid=="1")
            {
                roleid = 4;
            }
            else if(moduleid=="2")
            {
                roleid = 2;
            }
            List<user> lstusers = _icompanyrepository.getVenturePrefix(roleid,Prefix, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
            return Json(lstusers);
        }

        //To Get the logged in user notifications
        //GetUserNotifications
        [HttpGet]
        public ActionResult GetUserNotifications()
        {
            try
            {

                // Here use this
                // var listActivity = _icompanyrepository.GetAllActivitiesList(Keyword == "" ? "" : Keyword, Type == "Select" ? null : Type, Priority == "select" ? null : Priority, Status == "select" ? null : Status, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                // ManageCompany Controller



                //To Get the Nofications for Activities             
                string empid = GlobalVariables.UserID;
                string Status = "1";
                int totalPageCount = 0;
                List<Activity> lstActivity = _iDashboardDatarepository.GetUserActivityNotification(empid, Status, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < lstActivity.Count; i++)
                {
                    DateTime fdate = DateTime.Now;
                    //DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));

                    //DateTime fdate = DateTime.ParseExact(DateTime.Now.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    //DateTime rminddate = DateTime.ParseExact(lstActivity[i].DueDate.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    DateTime Datemodel = Convert.ToDateTime(lstActivity[i].DueDate);
                    DateTime rminddate = Datemodel;
                    TimeSpan differenced = rminddate - fdate;
                    int daysd = Convert.ToInt32(differenced.TotalDays);
                    lstActivity[i].daysdiff = daysd;
                    lstActivity[i].CurrentDate = fdate;
                }

                Session["Notifications"] = lstActivity;
                return View(lstActivity);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}


