using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Repository;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using CRMHub.ViewModel;

namespace CRM.Controllers
{
    public class CustomFieldsController : ControllerBase
    {
        #region
        private readonly ICustomFieldsRepository _customFieldsRepository;
        string loginId = GlobalVariables.UserID;
        string roleId = GlobalVariables.RoleID;
        public CustomFieldsController()
        {
            this._customFieldsRepository = new CustomFieldsRepository();
        }
        #endregion

        public ActionResult ViewCustomFields()
        {
            try
            {
                if (TempData["AddCF"] != null)
                {
                    ViewBag.CField = "Create";

                }

                if (TempData["UpdateCF"] != null)
                {
                    ViewBag.UField = "Update";

                }

                ViewData["Count"] = Convert.ToInt32(TempData["colcount"]);
                return View();
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [ActionName("EditEditCustomFields")]
        public ActionResult EditEditCustomFields(int FieldidfrmView)
        {
            try
            {
                Custom_Manage_Master customfieldsobj = new Custom_Manage_Master();
                var data = RetrieveCustomFields(Convert.ToInt64(loginId), FieldidfrmView);
                return View("AddEditCustomFields", data);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [ActionName("AddEditCustomFields")]
        public ActionResult AddEditCustomFields()
        {
            try
            {


                Custom_Manage_Master customMaster = new Custom_Manage_Master();
                customMaster.Module = Convert.ToString(TempData["ModuleName"]);
                if (customMaster.Module == "company")
                {
                    customMaster.Column_Id = Convert.ToInt16(TempData["companycount"]) + 1;
                }
                else if (customMaster.Module == "Contact")
                {
                    customMaster.Column_Id = Convert.ToInt16(TempData["contactcount"]) + 1;
                }
                ViewData["VDColId"] = customMaster.Column_Id;
                customMaster.ActualColumnName = "CustomField" + Convert.ToInt16(ViewData["VDColId"]);
                
                //customMaster.Column_Id = Convert.ToInt16(TempData["maxcolval"]) + 1;
                //ViewData["VDColId"] = customMaster.Column_Id;
                //customMaster.ActualColumnName = "CustomField" + Convert.ToInt16(ViewData["VDColId"]);
                //customMaster.Module = Convert.ToString(TempData["ModuleName"]);
                return View(customMaster);
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult AddEditCustomFields(Custom_Manage_Master customfieldsobj)
        {
            try
            {
              //  TempData["AddUpdateCF"] = "AddUpdateCF";
                customfieldsobj.UserID = Convert.ToInt64(loginId);


                if (customfieldsobj.FieldId != null)
                {
                    if (customfieldsobj.FieldId != 0)
                    {
                        TempData["UpdateCF"] = "UpdateCF";
                        if (customfieldsobj.RequiredField == null)
                        {
                            customfieldsobj.RequiredField = false;
                        }
                        int FieldId = 0;
                        var result = _customFieldsRepository.ChangeCustomFields(customfieldsobj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                        if (customfieldsobj.lstCustomOptions.Count > 0)
                            customfieldsobj.lstCustomOptions.ForEach(l => l.FieldId = FieldId);



                        for (int i = 0; i < customfieldsobj.lstCustomOptions.Count; i++)
                        {

                            if (customfieldsobj.lstCustomOptions[i].DrpValueId != 0)
                            {
                                customfieldsobj.lstCustomOptions[i].CreatedDate = DateTime.Now;

                                //bool res = _customFieldsRepository.UpdateCustomOptions(customfieldsobj.lstCustomOptions[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }
                            else
                            {
                                // DevEntities dbcontext = new DevEntities(GlobalVariables.ConnectionString);
                                // Custom_Manage_Master customfieldsobjs= new Custom_Manage_Master();
                                // int  Fid= (from  custMM  in  dbcontext.Custom_Manage_Master select  custMM.FieldId ).Count();  
                                customfieldsobj.lstCustomOptions[i].CreatedDate = DateTime.Now;
                                customfieldsobj.lstCustomOptions[i].FieldId = customfieldsobj.FieldId;
                                customfieldsobj.lstCustomOptions[i].IsDefault = customfieldsobj.lstCustomOptions[i].defaultval == "1" ? true : false;
                              
                                if (customfieldsobj.lstCustomOptions[i].DrpValue != null)
                                {
                                    int res = _customFieldsRepository.AddCustomOptions(customfieldsobj.lstCustomOptions[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                                }
                                

                            }


                        }
                        return RedirectToAction("ViewCustomFields");
                    }
                    else
                    {
                        TempData["AddCF"] = "AddCF";
                        if (Convert.ToInt16(TempData["colcount"]) < 50)
                        {
                            int FieldId = 0;

                            var result = _customFieldsRepository.InsertCustomFields(customfieldsobj, GlobalVariables.ConnectionString, GlobalVariables.ClientName, out FieldId);

                            if (customfieldsobj.lstCustomOptions.Count > 0)
                                customfieldsobj.lstCustomOptions.ForEach(l => l.FieldId = FieldId);

                            for (int i = 0; i < customfieldsobj.lstCustomOptions.Count; i++)
                            {
                                if (customfieldsobj.lstCustomOptions[i].DrpValue != "Deleted_Row")
                                {
                                    customfieldsobj.lstCustomOptions[i].CreatedDate = DateTime.Now;
                                    customfieldsobj.lstCustomOptions[i].IsDefault = customfieldsobj.lstCustomOptions[i].defaultval == "1" ? true : false;
                                    int res = _customFieldsRepository.AddCustomOptions(customfieldsobj.lstCustomOptions[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                                }                               
                            }
                            return RedirectToAction("ViewCustomFields");
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
                else
                {
                    int FieldId = 0;
                    var result = _customFieldsRepository.InsertCustomFields(customfieldsobj, GlobalVariables.ConnectionString, GlobalVariables.ClientName, out FieldId);
                    return RedirectToAction("ViewCustomFields");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Custom_Manage_Master RetrieveCustomFields(long UserId, long FieldId)
        {
            try
            {
                UserId = Convert.ToInt64(loginId);
                var result = _customFieldsRepository.GetCustomFieldsbyFieldId(UserId, FieldId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return result;
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult InActivateFields(string chkbxIds)
        {
            try
            {

                string Connectionstring = GlobalVariables.ConnectionString;
                string dbName = GlobalVariables.ClientName;
                var ClientEntity = new DevEntities();
                ClientEntity.Database.Connection.Open();
                ClientEntity.Database.Connection.ChangeDatabase(dbName);

                var customObj = new Custom_Manage_Master();
               
                if (!string.IsNullOrEmpty(chkbxIds))
                {
                    foreach (var InActId in chkbxIds.Split(','))
                    {
                        long id = Convert.ToInt64(InActId);
                        var InactFields = (from InActList in ClientEntity.Custom_Manage_Master
                                           where InActList.FieldId == id
                                           select InActList).FirstOrDefault();
                        if (InactFields != null)
                        {
                            InactFields.IsActive = false;
                        }                        
                        ClientEntity.SaveChanges();
                    }
                }
                return Json(true,JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult PartialViewCustomFields(string SelectedModule)
        {
            try
            {
                var result = _customFieldsRepository.GetAllCustomFields(1, SelectedModule, 1, GlobalVariables.ConnectionString, GlobalVariables.ClientName).OrderByDescending(x => x.FieldId);
                if (result.Count() > 0 && result != null)
                {
                    var count = result.Count();
                    var companycount = result.Where(x => x.Module == "company").Count();
                    var contactcount = result.Where(x => x.Module == "Contact").Count();
                    TempData["companycount"] = companycount;
                    TempData["contactcount"] = contactcount;
                    var max = result.Max(m => m.Column_Id);
                    TempData["colcount"] = count;
                    TempData.Keep("colcount");
                    TempData["maxcolval"] = max;
                    TempData.Keep("maxcolval");
                    TempData["ModuleName"] = SelectedModule;
                    TempData.Keep("ModuleName");
                }
                else
                //        if (result.Count() == 0)
                {
                    TempData["ModuleName"] = SelectedModule;
                    //  TempData["result"] = "true";
                }

                return PartialView(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
         public ActionResult _BindDrpOptions(string Fieldid)
        {
            Custom_Manage_Master LstCMM = new Custom_Manage_Master();
            if (Fieldid != "0")
            {
                LstCMM.lstCustomOptions = _customFieldsRepository.GetDrpOption(Convert.ToInt32(Fieldid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
            }
           // List<Custom_DrpChkValues> lstCustomDrpVal = _customFieldsRepository.GetDrpOption(Convert.ToInt32(Fieldid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
            return PartialView(LstCMM);
        }

        //[HttpPost]
        //public ActionResult InsertDrpOptions(string Optionval, string DefaultVal, string FieldId)
        //{
        //    try
        //    {
        //        Custom_DrpChkValues ObjCustomOptionValues = new Custom_DrpChkValues();
               
        //        ObjCustomOptionValues.DrpValue = Optionval;
        //        if (DefaultVal == "1")
        //        {
        //            ObjCustomOptionValues.IsDefault = Convert.ToBoolean(1);
        //        }
        //        else if (DefaultVal == "0")
        //        {
        //            ObjCustomOptionValues.IsDefault = Convert.ToBoolean(0);
        //        }
        //        ObjCustomOptionValues.FieldId = Convert.ToInt32(FieldId);
        //        ObjCustomOptionValues.CreatedDate = DateTime.Now;
        //        var result = _customFieldsRepository.AddCustomOptions(ObjCustomOptionValues, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


        //        return View();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpPost]
        public ActionResult InsertDrpOptions(string Optionval, string DefaultVal, string FieldId)
        {
            try
            {
                //string getulpth = this.Request.UrlReferrer.AbsoluteUri;
                string uri = this.Request.UrlReferrer.AbsolutePath;
                int currentFieldId = Convert.ToInt32(FieldId);
                string urlpath = uri + "?FieldidfrmView=" + currentFieldId;
                Custom_DrpChkValues ObjCustomOptionValues = new Custom_DrpChkValues();
                ObjCustomOptionValues.DrpValue = Optionval;
                if (DefaultVal == "1")
                {
                    ObjCustomOptionValues.IsDefault = Convert.ToBoolean(1);
                }
                else if (DefaultVal == "0")
                {
                    ObjCustomOptionValues.IsDefault = Convert.ToBoolean(0);
                }
                ObjCustomOptionValues.FieldId = Convert.ToInt32(FieldId);
                ObjCustomOptionValues.CreatedDate = DateTime.Now;
                int result = _customFieldsRepository.AddCustomOptions(ObjCustomOptionValues, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                //If default value is checked then update Custom Manage Master table for column Default value with fieldId
                if (DefaultVal == "1")
                {
                    //Update custom manage master with new drpvalueId in defaalut values UpdateCustomManagemaster
                    bool ManageMasterSuccess = _customFieldsRepository.UpdateCustomManagemaster(Convert.ToInt32(ObjCustomOptionValues.FieldId), result, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                }

                return View(urlpath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ActionName("DeleteCustomOptions")]
        public bool DeleteCustomOptions(string OptionIds)
        {

            if (OptionIds != null)
            {
                foreach (var OptionId in OptionIds.Split(','))
                {
                    var delOptions = _customFieldsRepository.DeleteCustomOptions(Int32.Parse(OptionId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                }
            }
            return true;
        }


        [HttpPost]
        public bool DeleteCustomfields(string fieldid)
        {
            try
            {
                if (!string.IsNullOrEmpty(fieldid))
                {
                    foreach (var menuids in fieldid.Split(','))
                    {
                        var deleteCustfield = _customFieldsRepository.DeleteCustomfieldsDetailsByFieldId(Convert.ToInt32(fieldid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
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
        [ActionName("IsColumnLabelUnique")]
        public bool IsColumnLabelUnique(string Module, string Column_Label)
        {
            string[] TableTextFields = new string[50];


            if (Module == "company")
            {
                TableTextFields = new string[] { "CompanyName", "Phone", "Fax", "CompanySite", "LastName", "ParentCompany", "WebSite", "CompanyNumber", "Ownership", "Type", "Employees", "Industry", "SICCode", "AnnualRevenue", "NAICSCode", "ReferredBy", "Status", "Address1", "Address2", "City", "State", "ZipCode", "Country", "SalesRepresentative1", "SalesRepresentative2", "Office/Location", "MailingAddress", "BillingAddress" };

            }
            else if (Module == "Contact")
            {
                TableTextFields = new string[] { "Name ", "Title", "Company", "Department", "Email", "CompanySite", "BirthDate", "Assistant", "LeadSource", "ContactType", "ReportsTo", "EmailOptOut", "Sector", "Business", "Asst", "Home", "Fax", "Mobile", "Other", "Address1", "Address2", "City", "State", "ZipCode", "Country", "SalesRepresentative1", "SalesRepresentative2", "Office/Location", "MailingAddress", "BillingAddress", "FacebookUserName", "TwitterUserName", "LinkedinUserName", "Google+UserName", "PinterestUserName", "SkypeUserName", "UserName", "Password" };

            }
            int Count = 0;
            string EnteredLabel = Column_Label.Trim().ToLower().Replace(" ", string.Empty);
            //if (TableTextFields.Select(x => x.Split('=')[0])
            //        .Intersect(new[] { EnteredLabel },
            //                   StringComparer.InvariantCultureIgnoreCase).Any())
            if (TableTextFields.Select(x => x.Split(',')[0])
                    .Intersect(new[] { EnteredLabel },
                               StringComparer.InvariantCultureIgnoreCase).Any())
            {
                Count = 1;
            }

            if (Count == 1)
                return true;
            else
                return false;
        }
        [HttpPost]
        public ActionResult IsColumnLabelExists(string columnlabel, string modulename, string Connectionstring, string dbName)
        {
            try
            {
                bool objcolumnlabel = _customFieldsRepository.CheckColumnLabelExistsOrNot(columnlabel, modulename, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(objcolumnlabel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}