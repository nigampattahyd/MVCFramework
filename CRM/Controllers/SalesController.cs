using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Repository;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using CRMHub.ViewModel;
using System.IO;
using System.Web.UI;
using CRMHub.ExtendedProperties;


namespace CRM.Controllers
{
    public class SalesController : ControllerBase
    {
        #region
        private readonly ISalesRepository _salesRepository;
        private readonly ICustomFieldsRepository _icustomfieldrepository;
        private readonly IContactsRepository _icontactsrepository;
        string loginId = GlobalVariables.UserID;
        string roleId = GlobalVariables.RoleID;
        public SalesController()
        {
            this._salesRepository = new SalesRepository();
            this._icustomfieldrepository = new CustomFieldsRepository();
            this._icontactsrepository = new ContactRepository();
        }
        #endregion

        [HttpGet]
        public ActionResult ViewSales()
        {
            try
            {
                SalesModel salesModelObj = new SalesModel();
                Branch selectOption = new Branch();
                selectOption.BranchId = -1;
                selectOption.BranchName = "Select All";
                salesModelObj.branchList = new List<Branch>();
                salesModelObj.branchList.Add(selectOption);
                salesModelObj.branchList.AddRange(_salesRepository.GetBranches(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                user selectUser = new user();
                selectUser.UserId = -1;
                selectUser.FirstName = "Choose";
                salesModelObj.userList = new List<user>();
                salesModelObj.userList.Add(selectUser);
                salesModelObj.userList.AddRange(_salesRepository.GetUsers(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                return View(salesModelObj);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public ActionResult getSales(jQueryDataTableParamModel param)
        {
            try
            {
                string FilterExpression = Request["SaleFilterExp"];
                string Keyword = Request["SaleKeyword"].TrimStart();
                string Email = Request["SaleEmail"];
                string SalesRep = Request["SaleSaleRep"];
                string Branch = Request["SaleBranch"];
                string SearchFilterString = Request["SearchFilterString"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "SalesId desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                //orderByClause = SortFieldName + " " + sSortDir_0;
                var listSale = _salesRepository.SearchforSale(SearchFilterString, Keyword.TrimEnd() == "" ? null : Keyword, Email == "" ? null : Email, SalesRep == "-1" ? "All" : SalesRep, Branch == "-1" ? "All" : Branch,
                    loginId, roleId, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                TempData["SaleKeyword"] = Keyword;
                TempData["SaleBranch"] = Branch;
                TempData["SaleSaleRep"] = SalesRep;
                TempData["SaleEmail"] = Email;
                TempData["orderByClause"] = orderByClause;
                TempData["TotalPageCount"] = totalPageCount;
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listSale,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult CreateSales()
        {
            try
            {
                string Module = "sales";
                SalesModel SalesModelObj = new SalesModel();
                Branch selectOption = new Branch();
                selectOption.BranchId = -1;
                selectOption.BranchName = "Select All";
                SalesModelObj.branchList = new List<Branch>();
                SalesModelObj.branchList.Add(selectOption);
                SalesModelObj.branchList.AddRange(_salesRepository.GetBranches(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                SalesModelObj.userList = new List<user>();
                SalesModelObj.userList.AddRange(_salesRepository.GetUsers(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                SalesModelObj.listCustomfields = _icustomfieldrepository.GetCustomFieldsByModule(Module, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return View(SalesModelObj);
            }
            catch(Exception)
            {
                throw;
            }
        }

      //  [ValidateInput(false)]
        [HttpPost]
        public ActionResult CreateSales(SalesModel SalesModelObj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SalesModelObj.SalesEntity.SaleCreatedBy = Convert.ToInt64(loginId);



                    var result = _salesRepository.AddSales(SalesModelObj.SalesEntity, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    //var ires= 
                    for (int i = 0; i < SalesModelObj.listCustomfields.Count; i++)
                    {
                        if (SalesModelObj.listCustomfields[i].Column_Type == "radiobutton")
                     {
                         Custom_Value_Master CVItem = new Custom_Value_Master();
                         string SelectedRadiobtnId = SalesModelObj.listCustomfields[i].DefaultValue;

                         for (int a = 0; a < SalesModelObj.listCustomfields[i].lstCustomOptions.Count; a++)
                         {

                             if (SalesModelObj.listCustomfields[i].lstCustomOptions[a].DrpValueId.ToString() == SelectedRadiobtnId)
                             {
                                 SalesModelObj.listCustomfields[i].lstCustomOptions[a].IsDefault = Convert.ToBoolean(1);
                                 CVItem.CustomFieldvalue = SelectedRadiobtnId;
                                 CVItem.CreatedDate = DateTime.Now;
                                 CVItem.ModifiedDate = DateTime.Now;
                                 CVItem.Module = "Sales";
                                 CVItem.MasterFieldId = SalesModelObj.listCustomfields[i].FieldId;
                                 CVItem.UserID = Convert.ToInt32(GlobalVariables.UserID);
                                 CVItem.ModuleRecordIdTmp = result;

                                 bool CFSuccess = _icustomfieldrepository.InsertCustomField(CVItem, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                             }
                             else
                             {
                                 SalesModelObj.listCustomfields[i].lstCustomOptions[a].IsDefault = Convert.ToBoolean(0);
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
                         CVItem.Module = "Sales";
                         CVItem.MasterFieldId = SalesModelObj.listCustomfields[i].FieldId;
                         CVItem.UserID = Convert.ToInt32(GlobalVariables.UserID);
                         // CVItem.ModuleRecordId = result;
                         CVItem.ModuleRecordIdTmp = result;
                         CVItem.CustomFieldvalue = SalesModelObj.listCustomfields[i].DefaultValue;

                         bool CFSuccess = _icustomfieldrepository.InsertCustomField(CVItem, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                     }

                     
                 }



                   


                    SalesModelObj.branchList = new List<Branch>();
                    SalesModelObj.branchList.AddRange(_salesRepository.GetBranches(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                    SalesModelObj.userList = new List<user>();
                    SalesModelObj.userList.AddRange(_salesRepository.GetUsers(GlobalVariables.ConnectionString, GlobalVariables.ClientName));


                    return RedirectToAction("ViewSales");
                }
                else
                {
                    Branch selectOption = new Branch();
                    selectOption.BranchId = -1;
                    selectOption.BranchName = "Select All";
                    SalesModelObj.branchList = new List<Branch>();
                    SalesModelObj.branchList.Add(selectOption);
                    SalesModelObj.branchList.AddRange(_salesRepository.GetBranches(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                    SalesModelObj.userList = new List<user>();
                    SalesModelObj.userList.AddRange(_salesRepository.GetUsers(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                    return View(SalesModelObj);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult RetrieveSale(int SalesId)
        {
            try
            {
                string Module = "sales";
                SalesModel salesModelObj = new SalesModel();
                Branch selectOption = new Branch();
                selectOption.BranchId = -1;
                selectOption.BranchName = "Select All";
                salesModelObj.branchList = new List<Branch>();
                salesModelObj.branchList.Add(selectOption);
                salesModelObj.branchList.AddRange(_salesRepository.GetBranches(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                salesModelObj.userList = new List<user>();
                salesModelObj.userList.AddRange(_salesRepository.GetUsers(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                salesModelObj.SalesEntity = _salesRepository.GetSelectedSale(SalesId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                salesModelObj.lstcustomVM = _icustomfieldrepository.GetCustomValueBasedonModule(SalesId, Module, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return View("UpdateSales", salesModelObj);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult UpdateSales()
        {
            try
            {
                SalesModel salesModelObj = new SalesModel();
                Branch selectOption = new Branch();
                selectOption.BranchId = -1;
                selectOption.BranchName = "Select All";
                salesModelObj.branchList = new List<Branch>();
                salesModelObj.branchList.Add(selectOption);
                salesModelObj.branchList.AddRange(_salesRepository.GetBranches(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                salesModelObj.userList = new List<user>();
                salesModelObj.userList.AddRange(_salesRepository.GetUsers(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                return View(salesModelObj);
            }
            catch(Exception)
            {
                throw;
            }
        }

        //[ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateSales(SalesModel salesModelObj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    salesModelObj.SalesEntity.SaleModifiedBy = Int64.Parse(GlobalVariables.UserID);
                    var result = _salesRepository.UpdateSales(salesModelObj.SalesEntity, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    salesModelObj.branchList = new List<Branch>();
                    salesModelObj.branchList.AddRange(_salesRepository.GetBranches(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                    salesModelObj.userList = new List<user>();
                    salesModelObj.userList.AddRange(_salesRepository.GetUsers(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                    for (int i = 0; i < salesModelObj.lstcustomVM.Count; i++)
                    {
                        if (salesModelObj.lstcustomVM[i].Column_Type == "radiobutton")
                        {
                            string SelectedRadiobtnId = salesModelObj.lstcustomVM[i].CustomFieldvalue;

                            for (int a = 0; a < salesModelObj.lstcustomVM[i].lstCustomOptionsVal.Count; a++)
                            {
                                if (salesModelObj.lstcustomVM[i].CValueId != 0)
                                {
                                    if (salesModelObj.lstcustomVM[i].lstCustomOptionsVal[a].DrpValueId.ToString() == SelectedRadiobtnId)
                                    {
                                        salesModelObj.lstcustomVM[i].lstCustomOptionsVal[a].IsDefault = Convert.ToBoolean(1);
                                        salesModelObj.lstcustomVM[i].ModifiedDate = DateTime.Now;
                                        salesModelObj.lstcustomVM[i].CustomFieldvalue = SelectedRadiobtnId;
                                        _icustomfieldrepository.UpdateCustomField(salesModelObj.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                                    }

                                
                                
                                else
                                {
                                    salesModelObj.lstcustomVM[i].lstCustomOptionsVal[a].IsDefault = Convert.ToBoolean(0);
                                }
                            
                            }

                            else{

                                 salesModelObj.lstcustomVM[i].CreatedDate = DateTime.Now;
                            salesModelObj.lstcustomVM[i].ModifiedDate = DateTime.Now;
                            salesModelObj.lstcustomVM[i].Module = "sales";
                            salesModelObj.lstcustomVM[i].ModuleRecordIdTmp = salesModelObj.SalesEntity.SalesId;
                            salesModelObj.lstcustomVM[i].MasterFieldId = salesModelObj.lstcustomVM[i].FieldId;


                            salesModelObj.lstcustomVM[i].lstCustomOptionsVal[a].IsDefault = Convert.ToBoolean(1);
                                //CompanyModel.lstcustomVM[i].ModifiedDate = DateTime.Now;
                            salesModelObj.lstcustomVM[i].CustomFieldvalue = SelectedRadiobtnId;
                            _icustomfieldrepository.InsertCustomField(salesModelObj.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }                           

                        

                            }
                        }
                        else
                        {
                            salesModelObj.lstcustomVM[i].UserID = Convert.ToInt32(GlobalVariables.UserID);
                            if (salesModelObj.lstcustomVM[i].CValueId != 0) // Update
                            {
                                salesModelObj.lstcustomVM[i].ModifiedDate = DateTime.Now;
                                _icustomfieldrepository.UpdateCustomField(salesModelObj.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }

                            else // Insert 
                            {
                                salesModelObj.lstcustomVM[i].CreatedDate = DateTime.Now;
                                salesModelObj.lstcustomVM[i].ModifiedDate = DateTime.Now;
                                salesModelObj.lstcustomVM[i].Module = "sales";
                                salesModelObj.lstcustomVM[i].ModuleRecordIdTmp = salesModelObj.SalesEntity.SalesId;
                                salesModelObj.lstcustomVM[i].MasterFieldId = salesModelObj.lstcustomVM[i].FieldId;
                                _icustomfieldrepository.InsertCustomField(salesModelObj.lstcustomVM[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }

                        }

                    }
                   
                    
                    return RedirectToAction("ViewSales");
                }
                else
                {
                    Branch selectOption = new Branch();
                    selectOption.BranchId = -1;
                    selectOption.BranchName = "Select All";
                    salesModelObj.branchList = new List<Branch>();
                    salesModelObj.branchList.Add(selectOption);
                    salesModelObj.branchList.AddRange(_salesRepository.GetBranches(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                    salesModelObj.userList = new List<user>();
                    salesModelObj.userList.AddRange(_salesRepository.GetUsers(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                    return View(salesModelObj);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }     
 
        [HttpPost]
        public bool DeleteSales(string delSales)
        {
            try
            {
                if (!string.IsNullOrEmpty(delSales))
                {
                    foreach (var DelSelSales in delSales.Split(','))
                    {
                        var deleteSale =_salesRepository.DeleteSale(Int32.Parse(DelSelSales), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }
                }
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public ActionResult SelectCompany()
        {
            try
            {
                ////List<tbl_account> accountsList = _salesRepository.SelectCompany(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                ////return PartialView(accountsList);
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
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [ActionName("IsTitleExists")]
        public JsonResult IsTitleExists(SalesModel salesModelObj)
        {
            try
            {
                var result = _salesRepository.IsTitleExists(salesModelObj.SalesEntity, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (salesModelObj.SalesEntity.SalesId.Equals(0))
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void ExportToCsv()
        {
            try
            {
                int totalPageCount = 0;

                string Keyword = TempData.Peek("SaleKeyword").ToString();
                string Branch = TempData.Peek("SaleBranch").ToString();
                string SalesRep = TempData.Peek("SaleSaleRep").ToString();
                string Email = TempData.Peek("SaleEmail").ToString();
                string SearchFilterString = null;
                string orderByClause = TempData.Peek("orderByClause").ToString();
                totalPageCount = Convert.ToInt32(TempData.Peek("TotalPageCount").ToString());



                var listSale = _salesRepository.SearchforSale(SearchFilterString, Keyword == "" ? null : Keyword, Email == "" ? null : Email, SalesRep == "-1" ? "All" : SalesRep, Branch == "-1" ? "All" : Branch,
                                        loginId, roleId, 0, totalPageCount, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


                StringWriter sw = new StringWriter();
                sw.WriteLine("\"AccountName\",\"SalesName\",\"BillingContact\",\"Email\",\"SaleCreatedOn\"");



                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=SalesExport.csv");
                Response.ContentType = "text/csv";

                for (int i = 0; i < listSale.Count; i++)
                {

                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                    listSale[i].AccountName,
                    listSale[i].SalesName,
                    listSale[i].BillingContact,
                    listSale[i].Email,
                    listSale[i].SaleCreatedOn
                   ));
                }

                Response.Write(sw.ToString());

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

                string Keyword = TempData.Peek("SaleKeyword").ToString();
                string Branch = TempData.Peek("SaleBranch").ToString();
                string SalesRep = TempData.Peek("SaleSaleRep").ToString();
                string Email = TempData.Peek("SaleEmail").ToString();
                string SearchFilterString = null;
                string orderByClause = TempData.Peek("orderByClause").ToString();
                totalPageCount = Convert.ToInt32(TempData.Peek("TotalPageCount").ToString());


                var listSale = _salesRepository.SearchforSale(SearchFilterString, Keyword == "" ? null : Keyword, Email == "" ? null : Email, SalesRep == "-1" ? "All" : SalesRep, Branch == "-1" ? "All" : Branch,
                                                        loginId, roleId, 0, totalPageCount, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = from d in listSale
                                  select new
                                  {
                                      AccountName = d.AccountName,
                                      SalesName = d.SalesName,
                                      BillingContact = d.BillingContact,
                                      Email = d.Email,
                                      SaleCreatedOn = d.SaleCreatedOn
                                  };
                grid.DataBind();

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=SalesExport.xls");
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

        //[HttpPost]
        //public ActionResult ExportToCsv(List<ExportSales> item)
        //{
        //    try
        //    {
        //        StringWriter sw = new StringWriter();
        //        sw.Flush();
        //        sw.WriteLine("\"AccountName\",\"SalesName\",\"BillingContact\",\"Email\",\"SaleCreatedOn\"");
        //        for (int i = 0; i < item.Count; i++)
        //        {
        //            sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
        //                                       item[i].AccountName,
        //                                       item[i].SalesName,
        //                                       item[i].BillingContact,
        //                                       item[i].Email,
        //                                       item[i].SaleCreatedOn
        //                                       ));
        //        }
        //        string fullpath = Path.Combine(Server.MapPath("~/Content/SalesExport/"));
        //        if (System.IO.File.Exists(fullpath + "ExportSales.csv"))
        //            System.IO.File.Delete(fullpath + "ExportSales.csv");
        //        string renderedGridView = sw.ToString();
        //        System.IO.File.AppendAllText(fullpath + "ExportSales.csv", renderedGridView);
        //        ViewBag.ExportSalesCSV = "1";
        //        return Json(item, JsonRequestBehavior.AllowGet);
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet]
        //public ActionResult DownloadSalesExportCSV()
        //{
        //    try
        //    {
        //        string file = "ExportSales.csv";
        //        string fullPath = Path.Combine(Server.MapPath("~/Content/SalesExport/"), file);
        //        return File(fullPath, "application/vnd.ms-excel", file);
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpPost]
        public ActionResult ExportToExcel(List<ExportSales> item)
        {
            try
            {
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = from d in item
                                  select new
                                  {
                                      AccountName = d.AccountName,
                                      SalesName = d.SalesName,
                                      BillingContact = d.BillingContact,
                                      Email = d.Email,
                                      SaleCreatedOn = d.SaleCreatedOn
                                  };
                grid.DataBind();
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                string fullpath = Path.Combine(Server.MapPath("~/Content/SalesExport/"));
                if (System.IO.File.Exists(fullpath + "ExportSales.xls"))
                    System.IO.File.Delete(fullpath + "ExportSales.xls");
                string renderedGridView = sw.ToString();
                System.IO.File.AppendAllText(fullpath + "ExportSales.xls", renderedGridView);
                ViewBag.ExportSalesExcel = "2";
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult DownloadSalesExportExcel()
        {
            try
            {
                string file = "ExportSales.xls";
                string fullPath = Path.Combine(Server.MapPath("~/Content/SalesExport/"), file);
                return File(fullPath, "application/vnd.ms-excel", file);
            }
            catch(Exception)
            {
                throw;
            }
        }
	}
}