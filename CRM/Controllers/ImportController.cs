using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using CRMHub.EntityFramework;
using System.Reflection;
using CRMHub.Interface;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace CRM.Controllers
{
    public class ImportController : ControllerBase
    {
        //
        // GET: /Import/
        private readonly ICompanyRepository _icompanyrepository;
        private readonly IContactsRepository _icontactsrepository;
        private readonly ICommonRepository _icommonrepository;
        private readonly ICustomFieldsRepository _customFieldsRepository;
        public ImportController(ICompanyRepository icompanyrepository, IContactsRepository icontactsrepository, ICommonRepository icommonrepository, ICustomFieldsRepository icustomfieldrepository)
        {
            _icompanyrepository = icompanyrepository;
            _icontactsrepository = icontactsrepository;
            _icommonrepository = icommonrepository;
            _customFieldsRepository = icustomfieldrepository;
        }

        public ActionResult Index()
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

        [HttpGet]
        public ActionResult ImportCompanies()
        {
            try
            {

                string erromsg = Convert.ToString(TempData["importerrmsg"]);
                if (erromsg == "Error")
                {
                    ViewBag.Importerror = "Template Format doesn't match for import process";
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public ActionResult ImportCompanies(FormCollection fc)
        {
            try
            {

                var Ppath = Server.MapPath("~/Documents/");
                if (!System.IO.Directory.Exists(Ppath))
                    System.IO.Directory.CreateDirectory(Ppath);
                string rootFolderPath = Ppath + GlobalVariables.ClientName + "\\";
                string destinationPath = Ppath;

                string[] fileList = System.IO.Directory.GetFiles(rootFolderPath);

                foreach (string file in fileList)
                {
                    string fileToMove = file;

                    var filename = Path.GetFileName(file);
                    string moveTo = destinationPath + filename;
                    System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + rootFolderPath + ";Extended Properties=\"text;HDR=Yes;FMT=Delimited;IMEX=1;\"";
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();

                    DataTable dt = new DataTable();
                    DataTable dtexcel = new DataTable();
                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }
                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {

                        //dt
                        dataAdapter.Fill(dtexcel);
                    }


                    string gr = Request.Form["Mappings"];
                    List<string> MapValues = gr.Split(',').Select(s => s).ToList();
                    DataTable dummy = new DataTable();
                    dummy = dtexcel.Clone();
                    for (int j = 0; j <= dtexcel.Rows.Count - 1; j++)
                    {
                        DataRow row1 = dummy.NewRow();
                        for (int i = 0; i <= MapValues.Count - 2; i++)
                        {
                            string x = MapValues[i].ToString();
                            int l = x.IndexOf("+");
                            var pre = "";
                            var post = "";
                            if (l > 0)
                            {
                                pre = x.Substring(0, l);
                                post = x.Substring(x.LastIndexOf('+') + 1);
                            }
                            //if (pre == "Type")
                            //{
                            //    row1[pre] = "";
                            //}
                            if (pre == "CreatedDate")
                            {
                                row1[pre] = DBNull.Value;
                            }
                            else if (pre == "ModifiedDate")
                            {
                                row1[pre] = DBNull.Value;
                            }
                            else if (pre == "Birthdate")
                            {
                                row1[pre] = DBNull.Value;
                            }
                            else
                            {
                                row1[pre] = dtexcel.Rows[j][post];
                            }
                            //dtexcel.Columns[pre].ColumnName = post;
                        }
                        dummy.Rows.Add(row1);
                        dummy.AcceptChanges();
                    }

                    string mp = Request.Form["RemoveMappings"];
                    List<string> RemoveMapValues = mp.Split(',').Select(s => s).ToList();
                    for (int i = 0; i < RemoveMapValues.Count - 1; i++)
                    {
                        string x = RemoveMapValues[i].ToString();
                        //var DataType = dummy.Columns["LName"].DataType;
                        if (x != "")
                        {
                            dummy.Columns.Remove(x);
                            dummy.Columns.Add(x);
                        }
                    }
                    dummy.AcceptChanges();
                    //state nd country id insertion
                    for (int i = 0; i <= dummy.Rows.Count - 1; i++)
                    {

                        string bstateid = Convert.ToString(dummy.Rows[i]["Billingstate"]);
                        string sstateid = Convert.ToString(dummy.Rows[i]["Shippingstate"]);
                        string bcountryid = Convert.ToString(dummy.Rows[i]["Billingcountry"]);
                        string scountryid = Convert.ToString(dummy.Rows[i]["Shippingcountry"]);
                        dummy.Rows[i]["Billingstate"] = GetStateIdFrmStateName(bstateid);
                        dummy.Rows[i]["Shippingstate"] = GetStateIdFrmStateName(sstateid);
                        dummy.Rows[i]["Billingcountry"] = GetCountryIdFrmCountryName(bcountryid);
                        dummy.Rows[i]["Shippingcountry"] = GetCountryIdFrmCountryName(scountryid);
                        dummy.Rows[i]["Ownership"] = GlobalVariables.UserID;
                    }
                    dummy.AcceptChanges();


                    DataTable tempDataexcel = new DataTable();
                    tempDataexcel = dtexcel.Clone();
                    // Validation for checking the Template Format with Standard Fields
                    DataSet cds = new DataSet();
                    cds.Tables.Add(tempDataexcel);

                    var filnam = generateFileName();
                    var testfile = filnam + ".csv";
                    var pat = destinationPath + testfile;
                    Session["sFileName"] = testfile;



                    StringBuilder sb = new StringBuilder();

                    IEnumerable<string> columnNames = dummy.Columns.Cast<DataColumn>().
                                                      Select(column => column.ColumnName);
                    sb.AppendLine(string.Join(",", columnNames));

                    foreach (DataRow row in dummy.Rows)
                    {
                        IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                        sb.AppendLine(string.Join(",", fields));
                    }
                    var dpath = destinationPath + testfile;
                    System.IO.File.WriteAllText(dpath, sb.ToString());


                    System.IO.File.Delete(file);
                }


                string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;
                CRMMasterClientsEntities xc = new CRMMasterClientsEntities();
                using (var obj = new CRMMasterClientsEntities(ConnectionString))
                {
                    ExcelImportFile recimport = new ExcelImportFile();
                    recimport.Status = "Pending";
                    recimport.Excelfile = Convert.ToString(Session["sFileName"]);
                    recimport.ClientId = GlobalVariables.ClientName;
                    recimport.ActualFName = Convert.ToString(Session["ActualFname"]);
                    recimport.Module = "Company";
                    obj.ExcelImportFiles.Add(recimport);
                    obj.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View("ImportCompanies");

        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                string flnm = System.IO.Path.GetFileName(Request.Files[0].FileName);
                string flnms = flnm;
                Session["ActualFname"] = "";
                Session["ActualFname"] = flnm;
                
                if (flnms != "importCompanies.csv")
                {
                    string flpath = Server.MapPath("~/Content/ImportFilesCompanies");


                    string[] files = Directory.GetFiles(flpath);
                    foreach (string filea in files)
                    {

                        System.IO.File.Delete(filea);
                    }

                }


                string flLoc = string.Format("{0}\\{1}", Server.MapPath("~/Content/ImportFilesCompanies"), flnm);
                if (System.IO.File.Exists(flLoc))
                    System.IO.File.Delete(flLoc);

                string exceldets = "";
                DataSet ds1 = new DataSet();
                DataTable dts1 = new DataTable();
                DataTable dt1 = new DataTable();
                List<Import> lstImport = new List<Import>();
                List<Import> lstImprt = new List<Import>();
                if ((Request.Files[0].ContentLength == 0) || file == null)
                {

                    return RedirectToAction("ImportCompanies");
                }
                if ((Request.Files.Count > 0) && (Request.Files[0].ContentLength > 0))
                {
                    if (ModelState.IsValid)
                    {
                        DataTable dt = new DataTable("tbl_Account");


                        DataSet ds = new DataSet();
                        //string[] strFieldStructure = new[] { "CompanyName", "Phone", "Fax", "WebSite", "Address1","ZipCode", "StateCode", "City" };
                        string[] strFieldStructure = new[] { "CompanyName", "Phone", "Fax", "WebSite", "Account_type", "Account_Industries", 
                                                             "Description", "AccountSite","AcountNumber","AnnualRevenue","Ownership",
                                                             "Employees","AccountOwner","Billingstreet","Shippingstreet","Billingcity","Shippingcity",
                                                             "Billingstate","Shippingstate","Billingzip","Shippingzip","Billingcountry",
                                                             "Shippingcountry","SalesMgr1","SalesMgr2","Account_office","PhoneExt",
                                                             "MailingAddress","BillingAddress","ReferredBy"};
                        int arrylength = strFieldStructure.Length;
                        string fileExtension = System.IO.Path.GetExtension(Request.Files[0].FileName);
                        string fileName = System.IO.Path.GetFileName(Request.Files[0].FileName);
                        if (fileExtension == ".csv")
                        {
                            string fileLocation = string.Format("{0}\\{1}", Server.MapPath("~/Content/ImportFilesCompanies"), fileName);
                            if (System.IO.File.Exists(fileLocation))
                                System.IO.File.Delete(fileLocation);
                            Request.Files[0].SaveAs(fileLocation);
                            if (fileExtension == ".csv")
                                fileLocation = Server.MapPath("~/Content/ImportFilesCompanies");
                            string excelConnectionString = string.Empty;
                            if (fileExtension == ".csv")
                            {
                                //excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"text;HDR=Yes;FMT=Delimited;IMEX=1;\"";
                                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"text;HDR=Yes;FMT=Delimited;IMEX=1;\"";
                            }
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            excelConnection.Open();
                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            if (dt == null)
                            {
                                return null;
                            }
                            String[] excelSheets = new String[dt.Rows.Count];
                            int t = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                excelSheets[t] = row["TABLE_NAME"].ToString();
                                t++;
                            }
                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                            string query = string.Format("Select * from [{0}]", excelSheets[0]);

                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(dts1);
                            }


                            // Validating the excel Format with Template Format for Standard Fields 
                            DataTable tempData = new DataTable();
                            tempData = dts1.Clone();
                            DataSet cds = new DataSet();
                            cds.Tables.Add(tempData);
                            int index = cds.Tables[0].Columns.IndexOf("ReferredBy");
                            if (index != -1)
                            {
                                int cdscount = cds.Tables[0].Columns.Count;
                                for (int p = index + 1; p < cdscount; )
                                {
                                    cds.Tables[0].Columns.RemoveAt(p);
                                    cdscount = cdscount - 1;
                                }
                                int cdscount1 = cds.Tables[0].Columns.Count;

                                if (cdscount1 == arrylength)
                                {
                                    int i = 0;
                                    foreach (DataColumn column in cds.Tables[0].Columns)
                                    {
                                        if (i < arrylength)
                                        {
                                            string ColumnName = column.ColumnName;
                                            // var Count = 0;
                                            var res = strFieldStructure.Select(x => x.Split(',')[0]).Intersect(new[] { ColumnName }, StringComparer.CurrentCulture).Any();
                                            if (res == false)
                                            {
                                                TempData["importerrmsg"] = "Error";
                                                return RedirectToAction("ImportCompanies");
                                            }
                                            i++;


                                        }
                                    }
                                }
                                else
                                {
                                    TempData["importerrmsg"] = "Error";
                                    return RedirectToAction("ImportCompanies");
                                }

                            }
                            else
                            {
                                TempData["importerrmsg"] = "Error";
                                return RedirectToAction("ImportCompanies");
                            }
                            // End Of validation

                            //check if company is null
                            foreach (DataRow row1 in dts1.Rows)
                            {
                                string strCompName = row1["CompanyName"].ToString();

                                if (strCompName == "")
                                {
                                    row1.Delete();
                                }



                                //else
                                //{
                                //    return RedirectToAction("ImportContacts");
                                //}                               
                            }
                            dts1.AcceptChanges();


                            excelConnection.Close();
                        }
                        // if (fileExtension == ".PNG" || fileExtension ==".html"|| fileExtension == ".png" || fileExtension == ".txt" || fileExtension == ".docx" || fileExtension == ".pdf" || fileExtension == ".jpg")
                        else
                        {
                            return RedirectToAction("ImportCompanies");
                        }
                        if (dts1.Rows.Count == 0)
                        {
                            return RedirectToAction("ImportCompanies");
                        }
                        lstImport = DTTL.ToList<Import>(dts1);
                        //lstImport = lstImport.ToList();
                        lstImport = lstImport.Where(k => k.CompanyName != null).ToList();
                        for (int a = 0; a < lstImport.Count; a++)
                        {
                            DateTime CreatedDate = DateTime.Now;
                            lstImport[a].CreatedDate = CreatedDate;
                            lstImport[a].ModifiedDate = CreatedDate;
                        }


                        String excelSheet = "";
                        // int j = 0;
                        foreach (DataRow row in dts1.Rows)
                        {
                            //excelSheet+="'"+row["CompanyName"].ToString() + "',";
                            excelSheet += row["CompanyName"].ToString() + ",";

                            exceldets = excelSheet.Remove(excelSheet.Length - 1);
                        }

                        string dbName = GlobalVariables.ClientName;
                        //List<tbl_account> accntval = _icompanyrepository.GET_UniqueAccountName(exceldets, dbName);

                        string fleName = System.IO.Path.GetFileName(Request.Files[0].FileName);
                        string fleLocation = string.Format("{0}\\{1}", Server.MapPath("~/Content/ImportFilesCompanies"), fleName);
                        if (System.IO.File.Exists(fleLocation))
                            System.IO.File.Delete(fleLocation);

                        //if (accntval.Count > 0)
                        //{
                        //    //lstImprt = lstImport.Where(c => accntval.Exists(cr => cr.Account_Name == c.CompanyName)).ToList();
                        //    lstImport = lstImport.Where(x => !accntval.Any(y => y.Account_Name == x.CompanyName)).ToList();


                        //    if (lstImport.Count == 0)
                        //    {
                        //        return RedirectToAction("ImportCompanies");

                        //    }
                        //}

                    }
                }

                lstImport = lstImport.GroupBy(x => x.CompanyName).Select(y => y.FirstOrDefault()).ToList();
                ViewBag.ImportVal = "2";
                TempData["UploadCompaniesData"] = lstImport;

                // DataTable datatblimp = new DataTable();

                DataTable datatbl = new DataTable();
                datatbl = LTDT.ToDataTable(lstImport);
                // MergeColumns(dts1,datatbl);
                DataTable dtuploadcmp = (from a in dts1.AsEnumerable()
                                         join ab in datatbl.AsEnumerable() on a["CompanyName"].ToString() equals ab["CompanyName"].ToString()
                                         select a).CopyToDataTable();
                TempData["UploadCompaniesexceldt"] = dtuploadcmp;

                // List<Import> lstImportresult = new List<Import>();
                // List <>
                // var lstImportresult = (from k in lstImport select k).Take(5).ToList();
                DataTable toprecordsdtcmp = new DataTable();
                toprecordsdtcmp = dtuploadcmp.AsEnumerable().Take(5).CopyToDataTable();
                //  datatblimp = LTDT.ToDataTable(lstImportresult);





                //pavi
                var extension = Path.GetExtension(Request.Files[0].FileName);
                string fileExt = extension.ToLower();
                if (fileExt == ".csv")
                {
                    string filename = generateFileName() + fileExt;
                    var Ppath = Server.MapPath("~/Documents/" + GlobalVariables.ClientName);
                    if (!System.IO.Directory.Exists(Ppath))
                        System.IO.Directory.CreateDirectory(Ppath);
                    var path = Path.Combine("~/Documents/" + GlobalVariables.ClientName + "/", filename);
                    Request.Files[0].SaveAs(Server.MapPath("~/Documents/" + GlobalVariables.ClientName + "/") + filename);

                    Session["FileName"] = filename;
                }
                else
                {
                    TempData["AlertMsg"] = "Some File(s) are in Invalid Format..!";
                    //return Content("select only images");
                }



                return View("ImportCompanies", toprecordsdtcmp);

            }
            catch (Exception)
            {
                throw;
            }
        }
        




        public int GetStateIdFrmStateName(string StateName)
        {
            int result = 0;
            int StateId = 0;
            result = _icommonrepository.GetStateIdFrmStateName(StateName, out StateId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
            return result;
        }
        public int GetCountryIdFrmCountryName(string CountryName)
        {
            int result = 0;
            int CountryId = 0;
            result = _icommonrepository.GetCountryIdFrmCountryName(CountryName, out CountryId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
            return result;
        }




        [HttpGet]
        public ActionResult DownloadImportCompaniesSample()
        {
            try
            {
                //string file = "Template_For_Import_Company.csv";
                string file = "ImportCompanies.csv";
                string fullPath = Path.Combine(Server.MapPath("~/Content/DownloadImportFiles"), file);
                return File(fullPath, "application/vnd.ms-excel", file);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult DownloadImportContactsSample()
        {
            try
            {
                //string file = "Template_For_Import_Contact.csv";
                string file = "ImportContact.csv";
                string fullPath = Path.Combine(Server.MapPath("~/Content/DownloadImportFiles"), file);
                return File(fullPath, "application/vnd.ms-excel", file);
            }
            catch (Exception ex)
            {
                ErrorExceptionClass x = new ErrorExceptionClass();
                x.catchException(ex.Message, "ImportController");
                return View("DownloadImportContactsSample");
            }
        }

        [HttpGet]
        public ActionResult ImportContacts()
        {
            try
            {
                string erromsg = Convert.ToString(TempData["importerrmsg"]);
                if (erromsg == "Error")
                {
                    ViewBag.Importerror = "Error";
                }
                return View();
            }
            catch (Exception ex)
            {
                ErrorExceptionClass x = new ErrorExceptionClass();
                x.catchException(ex.Message, "ImportController");
                return View("ImportContacts");
            }
        }

        //[HttpPost]
        //public ActionResult ImportContacts(FormCollection fc)
        //{
        //    try
        //    {
        //        List<ImportContact> lstContact = new List<ImportContact>();
        //        lstContact = (List<ImportContact>)TempData["UploadContactsData"];
        //        DataTable dtContacts = new DataTable();
        //        dtContacts = LTDT.ToDataTable(lstContact);
        //        if (dtContacts != null)
        //        {
        //            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ImportCompanies"].ConnectionString))
        //            {
        //                SqlTransaction transaction = null;
        //                connection.Open();
        //                try
        //                {
        //                    transaction = connection.BeginTransaction();
        //                    using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, transaction))
        //                    {
        //                        sqlBulkCopy.DestinationTableName = "tbl_Contact";
        //                        sqlBulkCopy.ColumnMappings.Add("Name", "fname");
        //                        sqlBulkCopy.ColumnMappings.Add("Email", "email");
        //                        sqlBulkCopy.ColumnMappings.Add("Address1", "mailingAddress");
        //                        sqlBulkCopy.ColumnMappings.Add("Address2", "mailingstreet");
        //                        sqlBulkCopy.ColumnMappings.Add("Phone", "phone");
        //                        sqlBulkCopy.ColumnMappings.Add("StateCode", "mailingstate");
        //                        sqlBulkCopy.ColumnMappings.Add("City", "mailingcity");
        //                        sqlBulkCopy.WriteToServer(dtContacts);
        //                    }
        //                    transaction.Commit();
        //                    ViewBag.ImportContComp = "3";
        //                }
        //                catch (Exception)
        //                {
        //                    transaction.Rollback();
        //                }
        //            }
        //        }
        //        return View("ImportContacts");
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ActionResult ImportContacts(FormCollection fc)
        //{
        //    try
        //    {
        //        DataTable dtexclconts = new DataTable();
        //        List<ImportContact> lstContact = new List<ImportContact>();
        //        lstContact = (List<ImportContact>)TempData["UploadContactsData"];
        //        lstContact = lstContact.Where(l => l.IscompanyExist == true).Select(l => l).ToList();
        //        for (int k = 0; k < lstContact.Count; k++)
        //        {
        //            var Contactoffice = lstContact[k].Contact_office;

        //            if (Contactoffice == null)
        //            {

        //                lstContact[k].Contact_office = "1";
        //            }
        //            if (Contactoffice != null)
        //            {
        //                lstContact[k].Contact_office = Contactoffice;
        //            }
        //        }
        //        //lstContact.ForEach(l => l.Contact_office = "-1");
        //        DataTable dtContacts = new DataTable();

        //        dtexclconts = (DataTable)TempData["UploadeContactsexceldt"];
        //        dtContacts = LTDT.ToDataTable(lstContact);
        //        DataTable dtxlimport = (from a in dtexclconts.AsEnumerable()
        //                                join ab in dtContacts.AsEnumerable() on a["FName"].ToString() equals ab["FName"].ToString()
        //                                select a).CopyToDataTable();

        //        var notAllowedColNames = dtexclconts.Columns.Cast<DataColumn>()
        //          .Select(c => c.ColumnName.ToUpperInvariant()).Intersect
        //          (dtContacts.Columns.Cast<DataColumn>().Select(r => r.ColumnName.ToUpperInvariant()))
        //          .ToList();
        //        foreach (var colName in notAllowedColNames)
        //            dtexclconts.Columns.Remove(colName);
        //        dtContacts = LTDT.ToDataTable(lstContact);

        //        if (dtContacts != null)
        //        {
        //            string cs = ConfigurationManager.ConnectionStrings["ImportCompanies"].ConnectionString;
        //            string conx = cs;
        //            //Session["conx"] = cs;
        //            //TempData["cnstr"] = cs;
        //            string dataSource = "";
        //            string providerconnectionstring = "";
        //            string[] parts = cs.Split(';');
        //            for (int i = 0; i < parts.Length; i++)
        //            {
        //                string part = parts[i].Trim();
        //                if (part.StartsWith("provider connection string="))
        //                {
        //                    providerconnectionstring = part.Replace("provider connection string", "");


        //                }
        //                if (part.StartsWith("Data Source="))
        //                {
        //                    dataSource = part.Replace("Data Source=", "");
        //                    break;
        //                }

        //            }
        //            string InitialCatalog = "";
        //            string[] constrgs = cs.Split(';');
        //            for (int i = 0; i < constrgs.Length; i++)
        //            {
        //                string constrg = constrgs[i].Trim();
        //                if (constrg.StartsWith("Initial Catalog="))
        //                {
        //                    InitialCatalog = GlobalVariables.ClientName;
        //                }

        //            }

        //            string UserID = "";
        //            string[] connUID = cs.Split(';');
        //            for (int i = 0; i < connUID.Length; i++)
        //            {
        //                string connID = connUID[i].Trim();
        //                if (connID.StartsWith("User ID="))
        //                {
        //                    UserID = connID.Replace("User ID=", "");
        //                    break;
        //                }

        //            }

        //            string Password = "";
        //            string[] conpwd = cs.Split(';');
        //            for (int i = 0; i < conpwd.Length; i++)
        //            {
        //                string connpwd = conpwd[i].Trim();
        //                if (connpwd.StartsWith("Password="))
        //                {
        //                    Password = connpwd.Replace("Password", "");
        //                    break;
        //                }
        //            }

        //            string ds = dataSource;
        //            string INTC = InitialCatalog;
        //            string User = UserID;
        //            string pswd = Password;
        //            StringBuilder Constrng = new StringBuilder("Data Source=");
        //            Constrng.Append(ds);
        //            Constrng.Append(";Initial Catalog=");
        //            Constrng.Append(INTC);
        //            Constrng.Append(";User ID=");
        //            Constrng.Append(User);
        //            Constrng.Append(";Password");
        //            Constrng.Append(Password);
        //            string strCon = Constrng.ToString();
        //            using (var connection = new SqlConnection(strCon))
        //            {
        //                SqlTransaction transaction = null;
        //                connection.Open();
        //                try
        //                {
        //                    transaction = connection.BeginTransaction();
        //                    using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, transaction))
        //                    {
        //                        sqlBulkCopy.DestinationTableName = "Accounts";
        //                        sqlBulkCopy.ColumnMappings.Add("Initial", "Initial");-----x
        //                        sqlBulkCopy.ColumnMappings.Add("FName", "FirstName");
        //                        sqlBulkCopy.ColumnMappings.Add("Phone", "Phone");
        //                        sqlBulkCopy.ColumnMappings.Add("Fax", "Fax");
        //                        sqlBulkCopy.ColumnMappings.Add("Email", "Email");
        //                        sqlBulkCopy.ColumnMappings.Add("Title", "Title");
        //                        sqlBulkCopy.ColumnMappings.Add("Source", "Source");-----x
        //                        sqlBulkCopy.ColumnMappings.Add("Mobile", "Mobile");------x
        //                        sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
        //                        sqlBulkCopy.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
        //                        sqlBulkCopy.ColumnMappings.Add("Creator", "CreatedBy");
        //                        sqlBulkCopy.ColumnMappings.Add("ModifiedBy", "ModifiedBy");
        //                        sqlBulkCopy.ColumnMappings.Add("CompanyName", "CompanyID");
        //                        sqlBulkCopy.ColumnMappings.Add("Homephone", "Homephone");----x
        //                        sqlBulkCopy.ColumnMappings.Add("OtherPhone", "OtherPhone");----x
        //                        sqlBulkCopy.ColumnMappings.Add("AsstPhone", "AsstPhone");----x
        //                        sqlBulkCopy.ColumnMappings.Add("Assistant", "Assistant");----x
        //                        sqlBulkCopy.ColumnMappings.Add("EmailOptOut", "EmailOptOut");---x
        //                        sqlBulkCopy.ColumnMappings.Add("Description", "Description");
        //                        sqlBulkCopy.ColumnMappings.Add("ReportTo", "ReportTo");----x
        //                        sqlBulkCopy.ColumnMappings.Add("Birthdate", "Birthdate");----x
        //                        sqlBulkCopy.ColumnMappings.Add("Department", "Department");----x
        //                        sqlBulkCopy.ColumnMappings.Add("Mailingstreet", "MailingAddress");
        //                        sqlBulkCopy.ColumnMappings.Add("Otherstreet", "MailingAddress2");
        //                        sqlBulkCopy.ColumnMappings.Add("Mailingcity", "Mailingcity");
        //                        sqlBulkCopy.ColumnMappings.Add("Othercity", "Othercity");----x
        //                        sqlBulkCopy.ColumnMappings.Add("Mailingstate", "MailingstateID");
        //                        sqlBulkCopy.ColumnMappings.Add("Otherstate", "Otherstate");-----x
        //                        sqlBulkCopy.ColumnMappings.Add("Mailingzip", "Mailingzip");
        //                        sqlBulkCopy.ColumnMappings.Add("Otherzip", "Otherzip");-------x
        //                        sqlBulkCopy.ColumnMappings.Add("Mailingcountry", "MailingcountryID");
        //                        sqlBulkCopy.ColumnMappings.Add("Othercountry", "Othercountry");--------x
        //                        sqlBulkCopy.ColumnMappings.Add("ReportingManager", "ReportingManager");---------x
        //                        sqlBulkCopy.ColumnMappings.Add("Suspect_prospect", "Suspect_prospect");-----------x
        //                        sqlBulkCopy.ColumnMappings.Add("SalesMgr1", "SalesMgr1");---------------x
        //                        sqlBulkCopy.ColumnMappings.Add("SalesMgr2", "SalesMgr2");----------------x
        //                        sqlBulkCopy.ColumnMappings.Add("Contact_office", "ContactTypeID");
        //                        sqlBulkCopy.ColumnMappings.Add("PhoneExt", "PhoneExt");----------------x
        //                        sqlBulkCopy.ColumnMappings.Add("MailingAddress", "MailingAddress");
        //                        sqlBulkCopy.ColumnMappings.Add("BillingAddress", "BillingAddress");
        //                        sqlBulkCopy.ColumnMappings.Add("IsCorporate", "IsCorporate");-----------x
        //                        sqlBulkCopy.ColumnMappings.Add("ContactIdEditable", "ContactIdEditable");------x
        //                        sqlBulkCopy.ColumnMappings.Add("IsAllowSMS", "IsAllowSMS");---------x
        //                        sqlBulkCopy.ColumnMappings.Add("ProviderId", "ProviderId");------x
        //                        sqlBulkCopy.ColumnMappings.Add("Type", "AccountTypeId");
        //                        sqlBulkCopy.ColumnMappings.Add("SeachIndex", "SeachIndex");
        //                        sqlBulkCopy.ColumnMappings.Add("FacebookUsername", "FacebookUsername");
        //                        sqlBulkCopy.ColumnMappings.Add("TwitterUsername", "TwitterUsername");
        //                        sqlBulkCopy.ColumnMappings.Add("LinkedInUsername", "LinkedInUsername");
        //                        sqlBulkCopy.ColumnMappings.Add("GooglePlusUserName", "GooglePlusUserName");--------x
        //                        sqlBulkCopy.ColumnMappings.Add("PinterestUsername", "PinterestUsername");-------x
        //                        sqlBulkCopy.ColumnMappings.Add("SkypeUsername", "SkypeUsername");
        //                        sqlBulkCopy.ColumnMappings.Add("Sector", "Sector"); -------------x                           
        //                        sqlBulkCopy.ColumnMappings.Add("MName", "Name");
        //                        sqlBulkCopy.ColumnMappings.Add("LName", "LastName");

        //                        sqlBulkCopy.WriteToServer(dtContacts);
        //                    }
        //                    transaction.Commit();
        //                    // ViewBag.ImportContComp = "3";
        //                }
        //                catch (Exception)
        //                {
        //                    transaction.Rollback();
        //                }
        //                DataTable dtcustomfieldcolumnscnt = new DataTable();
        //                DataTable dthtbl = new DataTable();
        //                dthtbl = dtexclconts.Copy();

        //                if (dthtbl.Columns.Count != 0)
        //                {
        //                    DevEntities contid = new DevEntities();
        //                    Custom_Manage_Master CSManagemastercnts = new Custom_Manage_Master();
        //                    for (int i = 0; i < dtexclconts.Columns.Count; i++)
        //                    {
        //                        string SelectedModule = "Contact";
        //                        var result = _customFieldsRepository.GetAllCustomFields(1, SelectedModule, 1, GlobalVariables.ConnectionString, GlobalVariables.ClientName).OrderByDescending(x => x.FieldId);
        //                        if (result != null && result.Count() != 0)
        //                        {
        //                            var count = result.Count();
        //                            var max = result.Max(m => m.Column_Id);
        //                            TempData["colcount"] = count;
        //                            TempData.Keep("colcount");
        //                            TempData["maxcolumnval"] = max;
        //                            TempData.Keep("maxcolval");
        //                            TempData["ModuleName"] = SelectedModule;
        //                        }
        //                        CSManagemastercnts.Module = "Contact";
        //                        CSManagemastercnts.Column_Type = "textbox";
        //                        CSManagemastercnts.Field_Type = "custom";
        //                        CSManagemastercnts.HoverText = "Custom Field";
        //                        CSManagemastercnts.UserID = 1;
        //                        CSManagemastercnts.CreatedDate = DateTime.Now;
        //                        CSManagemastercnts.Column_Label = dtexclconts.Columns[i].ColumnName;
        //                        CSManagemastercnts.ActualColumnName = dtexclconts.Columns[i].ColumnName;
        //                        CSManagemastercnts.Column_Id = Convert.ToInt16(TempData["maxcolumnval"]) + 1;
        //                        CSManagemastercnts.IsActive = true;
        //                        string mastercolumnlabel = CSManagemastercnts.Column_Label;
        //                        var checkifexists = _customFieldsRepository.CheckIfmasterlabelExists(mastercolumnlabel, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                        if (checkifexists == false)
        //                        {
        //                            dthtbl.Columns.Remove(mastercolumnlabel);
        //                        }


        //                        // var res = _customFieldsRepository.AddCustomFields(CSManagemastercnts, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //                    }
        //                    if (dthtbl.Columns.Count > 0)
        //                    {
        //                        TempData["dtcustomfieldscolumns"] = dthtbl;

        //                        dtcustomfieldcolumnscnt = (DataTable)TempData["dtcustomfieldscolumns"];
        //                        int dthcont = dthtbl.Columns.Count;
        //                        DataColumn mycolumn = new DataColumn();
        //                        mycolumn.DataType = Type.GetType("System.String");
        //                        mycolumn.ColumnName = "ContactId";

        //                        dthtbl.Columns.Add(mycolumn);

        //                        DataColumn mycolumn1 = new DataColumn();
        //                        mycolumn1.DataType = Type.GetType("System.String");
        //                        mycolumn1.ColumnName = "contact";
        //                        dthtbl.Columns.Add(mycolumn1);

        //                        DataColumn mycolumn2 = new DataColumn();
        //                        mycolumn2.DataType = Type.GetType("System.String");
        //                        mycolumn2.ColumnName = "customfields";
        //                        dthtbl.Columns.Add(mycolumn2);


        //                        DataColumn mycolumn3 = new DataColumn();
        //                        mycolumn3.DataType = Type.GetType("System.String");
        //                        mycolumn3.ColumnName = "UserID";
        //                        dthtbl.Columns.Add(mycolumn3);
        //                        string contct = "Contact";
        //                        for (int j = 0; j < dtContacts.Rows.Count; j++)
        //                        {
        //                            string compname = dtContacts.Rows[j]["CompanyName"].ToString();
        //                            string cntname = dtContacts.Rows[j]["FName"].ToString();
        //                            string cmail = dtContacts.Rows[j]["Email"].ToString();
        //                            string Lname = dtContacts.Rows[j]["LName"].ToString();
        //                            //get contactid _icompanyrepository.getAccountIdFrmCompName(compname);

        //                            //var cntsid = (from contcts in contid.tbl_Contact
        //                            //              where contcts.Fname == cntname
        //                            //              select contcts.ContactId).FirstOrDefault();

        //                            var cntsid = _icommonrepository.GetContIdFrmFName(cntname, Lname, cmail, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //                            dthtbl.Rows[j]["ContactId"] = cntsid;
        //                            dthtbl.Rows[j]["contact"] = contct;
        //                            dthtbl.Rows[j]["UserID"] = GlobalVariables.UserID;

        //                        }
        //                        DataColumn mycolumns = new DataColumn();
        //                        mycolumns.DataType = Type.GetType("System.String");
        //                        mycolumns.ColumnName = "MasterFieldId";
        //                        dthtbl.Columns.Add(mycolumns);

        //                        for (int j = 0; j < dthcont; j++)
        //                        {
        //                            string mastercolname = dthtbl.Columns[j].ColumnName;
        //                            string OptionId = "";
        //                            Custom_Manage_Master CMM = _customFieldsRepository.getManageMasterdetails(mastercolname, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //                            int FieldId = Convert.ToInt32(CMM.FieldId);
        //                            //string MasterFieldId = _customFieldsRepository.getManageMasterAccnt_Id(mastercolname, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                            for (int n = 0; n < dthtbl.Rows.Count; n++)
        //                            {

        //                                if (CMM.Column_Type == "dropdown" || CMM.Column_Type == "radiobutton")
        //                                {
        //                                    string fieldtext = dthtbl.Rows[n][mastercolname].ToString();
        //                                    OptionId = _customFieldsRepository.getOptionId(fieldtext, FieldId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                                }

        //                                dthtbl.Rows[n]["MasterFieldId"] = CMM.FieldId;
        //                                //dthtbl.Rows[n]["MasterFieldId"] = MasterFieldId;
        //                                dthtbl.Rows[n]["customfields"] = dthtbl.Columns[j].ColumnName;
        //                                Custom_Value_Master CVItem = new Custom_Value_Master();
        //                                CVItem.CreatedDate = DateTime.Now;
        //                                CVItem.ModifiedDate = DateTime.Now;
        //                                CVItem.Module = "contact";
        //                                CVItem.MasterFieldId = Convert.ToInt64(CMM.FieldId);
        //                                CVItem.UserID = Convert.ToInt32(GlobalVariables.UserID);
        //                                CVItem.ModuleRecordIdTmp = Convert.ToInt64(dthtbl.Rows[n]["ContactId"]);
        //                               // CVItem.CustomFieldvalue = dthtbl.Rows[n][mastercolname].ToString();
        //                                if (CMM.Column_Type == "dropdown" || CMM.Column_Type == "radiobutton")
        //                                    CVItem.CustomFieldvalue = OptionId;
        //                                else CVItem.CustomFieldvalue = dthtbl.Rows[n][mastercolname].ToString();

        //                                bool CFSuccess = _customFieldsRepository.Insertcustomvalue(CVItem, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //                            }

        //                            // below forloop end
        //                        }
        //                    }

        //                }
        //            }
        //            ViewBag.ImportContComp = "3";
        //        }
        //        // }
        //        return View("ImportContacts");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ActionResult ImportContacts(FormCollection fc)
        //{
        //    try
        //    {

        //        var Ppath = Server.MapPath("~/Documents/");
        //        if (!System.IO.Directory.Exists(Ppath))
        //            System.IO.Directory.CreateDirectory(Ppath);
        //        string rootFolderPath = Ppath + GlobalVariables.ClientName + "\\";
        //        string destinationPath = Ppath;

        //        string[] fileList = System.IO.Directory.GetFiles(rootFolderPath);

        //        foreach (string file in fileList)
        //        {
        //            string fileToMove = file;

        //            var filename = Path.GetFileName(file);
        //            string moveTo = destinationPath + filename;
        //            System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
        //            string excelConnectionString = string.Empty;
        //            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + rootFolderPath + ";Extended Properties=\"text;HDR=Yes;FMT=Delimited;IMEX=1;\"";
        //            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
        //            excelConnection.Open();

        //            DataTable dt = new DataTable();
        //            DataTable dtexcel = new DataTable();
        //            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //            if (dt == null)
        //            {
        //                return null;
        //            }
        //            String[] excelSheets = new String[dt.Rows.Count];
        //            int t = 0;
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                excelSheets[t] = row["TABLE_NAME"].ToString();
        //                t++;
        //            }
        //            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
        //            string query = string.Format("Select * from [{0}]", excelSheets[0]);
        //            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
        //            {

        //                //dt
        //                dataAdapter.Fill(dtexcel);
        //            }


        //            string gr = Request.Form["Mappings"];
        //            List<string> MapValues = gr.Split(',').Select(s => s).ToList();
        //            DataTable dummy = new DataTable();
        //            dummy = dtexcel.Clone();
        //            for (int j = 0; j <= dtexcel.Rows.Count - 1; j++)
        //            {
        //                DataRow row1 = dummy.NewRow();
        //                for (int i = 0; i <= MapValues.Count - 2; i++)
        //                {
        //                    string x = MapValues[i].ToString();
        //                    int l = x.IndexOf("+");
        //                    var pre = "";
        //                    var post = "";
        //                    if (l > 0)
        //                    {
        //                        pre = x.Substring(0, l);
        //                        post = x.Substring(x.LastIndexOf('+') + 1);
        //                    }
        //                    if (pre == "Type")
        //                    {
        //                        row1[pre] = "";
        //                    }
        //                    else if (pre == "CreatedDate")
        //                    {
        //                        row1[pre] = DBNull.Value;
        //                    }
        //                    else if (pre == "ModifiedDate")
        //                    {
        //                        row1[pre] = DBNull.Value;
        //                    }
        //                    else if (pre == "Birthdate")
        //                    {
        //                        row1[pre] = DBNull.Value;
        //                    }
        //                    else
        //                    {
        //                        row1[pre] = dtexcel.Rows[j][post];
        //                    }
        //                    //dtexcel.Columns[pre].ColumnName = post;
        //                }
        //                dummy.Rows.Add(row1);
        //                dummy.AcceptChanges();
        //            }

        //            string mp = Request.Form["RemoveMappings"];
        //            List<string> RemoveMapValues = mp.Split(',').Select(s => s).ToList();
        //            for (int i = 0; i < RemoveMapValues.Count - 1; i++)
        //            {
        //                string x = RemoveMapValues[i].ToString();
        //                //var DataType = dummy.Columns["LName"].DataType;
        //                dummy.Columns.Remove(x);
        //                dummy.Columns.Add(x);
        //            }
        //            dummy.AcceptChanges();



        //            DataTable tempDataexcel = new DataTable();
        //            tempDataexcel = dtexcel.Clone();
        //            // Validation for checking the Template Format with Standard Fields
        //            DataSet cds = new DataSet();
        //            cds.Tables.Add(tempDataexcel);

        //            var filnam = generateFileName();
        //            var testfile = filnam + ".csv";
        //            var pat = destinationPath + testfile;
        //            Session["sFileName"] = testfile;



        //            ////pavi---comment
        //            //StreamWriter wr = new StreamWriter(pat, false, Encoding.Unicode);

        //            //try
        //            //{
        //            //    for (int i = 0; i < dummy.Columns.Count; i++)
        //            //    {
        //            //        wr.Write(dummy.Columns[i].ToString().ToUpper() + "\t");
        //            //    }

        //            //    wr.WriteLine();

        //            //    //write rows to excel file
        //            //    for (int i = 0; i < (dummy.Rows.Count); i++)
        //            //    {
        //            //        for (int j = 0; j < dummy.Columns.Count; j++)
        //            //        {
        //            //            if (dummy.Rows[i][j] != null)
        //            //            {
        //            //                wr.Write("=\"" + Convert.ToString(dummy.Rows[i][j]) + "\"" + "\t");
        //            //            }
        //            //            else
        //            //            {
        //            //                wr.Write("\t");
        //            //            }
        //            //        }
        //            //        //go to next line
        //            //        wr.WriteLine();
        //            //    }
        //            //    //close file
        //            //    wr.Close();
        //            //}
        //            //catch (Exception ex)
        //            //{

        //            //}

        //            /////pavi----comment



        //            StringBuilder sb = new StringBuilder();

        //            IEnumerable<string> columnNames = dummy.Columns.Cast<DataColumn>().
        //                                              Select(column => column.ColumnName);
        //            sb.AppendLine(string.Join(",", columnNames));

        //            foreach (DataRow row in dummy.Rows)
        //            {
        //                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
        //                sb.AppendLine(string.Join(",", fields));
        //            }
        //            var dpath = destinationPath + testfile;
        //            System.IO.File.WriteAllText(dpath, sb.ToString());










        //            System.IO.File.Delete(file);
        //        }


        //        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;
        //        CRMMasterClientsEntities xc = new CRMMasterClientsEntities();
        //        using (var obj = new CRMMasterClientsEntities(ConnectionString))
        //        {
        //            ExcelImportFile recimport = new ExcelImportFile();
        //            recimport.Status = "Pending";
        //            recimport.Excelfile = Convert.ToString(Session["sFileName"]);
        //            recimport.ClientId = GlobalVariables.ClientName;
        //            recimport.ActualFName = Convert.ToString(Session["ActualFname"]);
        //            obj.ExcelImportFiles.Add(recimport);
        //            obj.SaveChanges();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return View("ImportContacts");

        //}

        public int GetAcntIdFrmCompName(string CompnayName)
        {
            int result = 0;
            int AccountId = 0;
            result = _icommonrepository.GetAcntIdFrmCompName(CompnayName, out AccountId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
            return result;
        }
        //pavi
        private string generateFileName()
        {
            string pFileName = "";
            Guid fileName = Guid.NewGuid();
            pFileName = fileName.ToString();
            return pFileName;
        }

        //[HttpPost]
        //public ActionResult UploadFileContact(HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        // DataTable dtexcel = new DataTable();
        //        string flnm = System.IO.Path.GetFileName(Request.Files[0].FileName);
        //        Session["ActualFname"] = flnm;
        //        string flnms = flnm;
        //        if (flnms != "importContact.csv")
        //        {
        //            string flpath = Server.MapPath("~/Content/ImportFilesContacts");


        //            string[] files = Directory.GetFiles(flpath);
        //            foreach (string filea in files)
        //            {

        //                System.IO.File.Delete(filea);
        //            }

        //        }

        //        string flLoc = string.Format("{0}\\{1}", Server.MapPath("~/Content/ImportFilesContacts"), flnm);
        //        if (System.IO.File.Exists(flLoc))
        //            System.IO.File.Delete(flLoc);
        //        List<ImportContact> lstImportCon = new List<ImportContact>();
        //        if ((Request.Files[0].ContentLength == 0) || file == null)
        //        {

        //            return RedirectToAction("ImportContacts");
        //        }
        //        DataTable dtexcel = new DataTable();
        //        if ((Request.Files.Count > 0) && (Request.Files[0].ContentLength > 0))
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                DataTable dts1 = new DataTable();
        //                DataTable dt = new DataTable();

        //                // DataSet ds = new DataSet();
        //                string[] strFieldStructure = new[] { "Initial", "FName", "MName", "LName", "Phone", "Fax", "Email", "Title", "Source", "Mobile", "CreatedDate", "ModifiedDate", "Creator", "ModifiedBy", "CompanyName", "Homephone", "OtherPhone", "AsstPhone", "Assistant", "EmailOptOut", "Description", "ReportTo", "Birthdate", "Department", "Mailingstreet", "Otherstreet", "Mailingcity", "Othercity", "Mailingstate", "Otherstate", "Mailingzip", "Otherzip", "Mailingcountry", "Othercountry", "ReportingManager", "Suspect_prospect", "SalesMgr1", "SalesMgr2", "Contact_office", "PhoneExt", "MailingAddress", "BillingAddress", "IsCorporate", "ContactIdEditable", "IsAllowSMS", "ProviderId", "Type", "SeachIndex", "FacebookUsername", "TwitterUsername", "LinkedInUsername", "GooglePlusUserName", "PinterestUsername", "SkypeUsername", "Sector" };
        //                int arrylength = strFieldStructure.Length;
        //                string fileExtension = System.IO.Path.GetExtension(Request.Files[0].FileName);
        //                string fileName = System.IO.Path.GetFileName(Request.Files[0].FileName);
        //                if (fileExtension == ".csv")
        //                {
        //                    string fileLocation = string.Format("{0}\\{1}", Server.MapPath("~/Content/ImportFilesContacts"), fileName);
        //                    if (System.IO.File.Exists(fileLocation))
        //                        System.IO.File.Delete(fileLocation);
        //                    Request.Files[0].SaveAs(fileLocation);
        //                    if (fileExtension == ".csv")
        //                        fileLocation = Server.MapPath("~/Content/ImportFilesContacts");
        //                    string excelConnectionString = string.Empty;
        //                    if (fileExtension == ".csv")
        //                    {
        //                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"text;HDR=Yes;FMT=Delimited;IMEX=1;\"";
        //                    }
        //                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
        //                    excelConnection.Open();
        //                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //                    if (dt == null)
        //                    {
        //                        return null;
        //                    }
        //                    String[] excelSheets = new String[dt.Rows.Count];
        //                    int t = 0;
        //                    foreach (DataRow row in dt.Rows)
        //                    {
        //                        excelSheets[t] = row["TABLE_NAME"].ToString();
        //                        t++;
        //                    }
        //                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
        //                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
        //                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
        //                    {

        //                        //dt
        //                        dataAdapter.Fill(dtexcel);
        //                    }

        //                    DataTable tempDataexcel = new DataTable();
        //                    tempDataexcel = dtexcel.Clone();
        //                    // Validation for checking the Template Format with Standard Fields
        //                    DataSet cds = new DataSet();
        //                    cds.Tables.Add(tempDataexcel);
        //                    int index = cds.Tables[0].Columns.IndexOf("Sector");
        //                    if (index != -1)
        //                    {
        //                        int cdscount = cds.Tables[0].Columns.Count;
        //                        for (int p = index + 1; p < cdscount; )
        //                        {
        //                            cds.Tables[0].Columns.RemoveAt(p);
        //                            cdscount = cdscount - 1;
        //                        }
        //                        int cdscount1 = cds.Tables[0].Columns.Count;

        //                        if (cdscount1 == arrylength)
        //                        {
        //                            int i = 0;
        //                            foreach (DataColumn column in cds.Tables[0].Columns)
        //                            {

        //                                if (i < arrylength)
        //                                {
        //                                    string ColumnName = column.ColumnName;
        //                                    var res = strFieldStructure.Select(x => x.Split(',')[0]).Intersect(new[] { ColumnName }, StringComparer.CurrentCulture).Any();
        //                                    if (res == false)
        //                                    {
        //                                        TempData["importerrmsg"] = "Error";
        //                                        return RedirectToAction("ImportContacts");
        //                                    }
        //                                    i++;
        //                                    //nasdfjsdn
        //                                    //var Count = 0;
        //                                    //var res = strFieldStructure.Select(x => x.Split(',')[0]).Intersect(new[] { ColumnName }, StringComparer.CurrentCulture).Any();
        //                                    //if (strFieldStructure.Select(x => x.Split(',')[0]).Intersect(new[] { ColumnName }, StringComparer.CurrentCulture).Any())
        //                                    //{
        //                                    //    Count = 1;
        //                                    //}
        //                                    //if (Count == 0)
        //                                    //{
        //                                    //    TempData["importerrmsg"] = "Error";
        //                                    //    return RedirectToAction("ImportContacts");
        //                                    //}
        //                                    //i++;
        //                                }
        //                            }

        //                        }
        //                        else
        //                        {
        //                            TempData["importerrmsg"] = "Error";
        //                            return RedirectToAction("ImportContacts");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        TempData["importerrmsg"] = "Error";
        //                        return RedirectToAction("ImportContacts");
        //                    }
        //                    // End of the Template Validation 
        //                    foreach (DataRow row1 in dtexcel.Rows)
        //                    {
        //                        string strCompName = row1["CompanyName"].ToString();
        //                        string strContName = row1["FName"].ToString();
        //                        if (strCompName == "" || strContName == "")
        //                        {
        //                            row1.Delete();
        //                            //dtexcel.Rows.Remove(row1);
        //                        }

        //                        if (strCompName != "" && strContName != "")
        //                        {
        //                            int demo = GetAcntIdFrmCompName(strCompName);
        //                            row1["CompanyName"] = demo;
        //                        }



        //                    }
        //                    dtexcel.AcceptChanges();
        //                    excelConnection.Close();
        //                }
        //                // if (fileExtension == ".PNG" || fileExtension == ".pdf" || fileExtension == ".txt"||fileExtension == ".docx" || fileExtension == ".png" || fileExtension == ".jpg")

        //                else
        //                {
        //                    return RedirectToAction("ImportContacts");


        //                }
        //                if (dtexcel.Rows.Count == 0)
        //                {
        //                    return RedirectToAction("ImportContacts");
        //                }

        //                //dt
        //                lstImportCon = DTTLcontact.ToList<ImportContact>(dtexcel);
        //                lstImportCon = lstImportCon.Where(k => k.CompanyName != null).ToList();
        //                lstImportCon = lstImportCon.Where(k => k.FName != null).ToList();
        //                for (int a = 0; a < lstImportCon.Count; a++)
        //                {
        //                    DateTime CreatedDate = DateTime.Now;
        //                    lstImportCon[a].CreatedDate = CreatedDate;
        //                    lstImportCon[a].ModifiedDate = CreatedDate;
        //                }

        //                for (int k = 0; k < lstImportCon.Count; k++)
        //                {
        //                    //   bool objcompanyname = _icompanyrepository.CheckCompanyExistsOrNot(lstImportCon[k].CompanyName, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                    if (lstImportCon[k].CompanyName != "0")
        //                    {
        //                        // lstImportCon[k].CompanyName = GetAcntIdFrmCompName(lstImportCon[k].CompanyName).ToString();
        //                        lstImportCon[k].IscompanyExist = true;
        //                    }
        //                    else
        //                    {
        //                        lstImportCon[k].IscompanyExist = false;
        //                        Guid serialGuid = Guid.NewGuid();
        //                        string uniqueSerial = serialGuid.ToString("N");
        //                        string uniqueSerialLength = uniqueSerial.Substring(0, 12).ToUpper();

        //                        char[] serialArray = uniqueSerialLength.ToCharArray();
        //                        string finalSerialNumber = "";

        //                        int j = 0;
        //                        for (int i = 0; i < 12; i++)
        //                        {
        //                            for (j = i; j < 4 + i; j++)
        //                            {
        //                                finalSerialNumber += serialArray[j];
        //                            }
        //                            if (j == 12)
        //                            {
        //                                break;
        //                            }
        //                            else
        //                            {
        //                                i = (j) - 1;
        //                                finalSerialNumber += "-";
        //                            }
        //                        }
        //                        lstImportCon[k].CompanyName = finalSerialNumber;
        //                    }

        //                }

        //                string excelcnts = "";
        //                string excelcntdets = "";
        //                flnms = flnm;
        //                if (flnms != "ImportContact.csv")
        //                {
        //                    string flLoctn = string.Format("{0}\\{1}", Server.MapPath("~/Content/ImportFilesContacts"), flnms);
        //                    if (System.IO.File.Exists(flLoctn))
        //                        System.IO.File.Delete(flLoctn);
        //                }
        //                //dt
        //                foreach (DataRow row1 in dtexcel.Rows)
        //                {
        //                    string cntname = row1["FName"].ToString();
        //                    excelcnts += row1["FName"].ToString() + ",";

        //                    excelcntdets = excelcnts.Remove(excelcnts.Length - 1);
        //                }
        //                string dbName = GlobalVariables.ClientName;

        //                string fleName = System.IO.Path.GetFileName(Request.Files[0].FileName);
        //                string fleLocation = string.Format("{0}\\{1}", Server.MapPath("~/Content/ImportFilesContacts"), fleName);
        //                if (System.IO.File.Exists(fleLocation))
        //                    System.IO.File.Delete(fleLocation);


        //            }
        //        }
        //        lstImportCon = lstImportCon.Where(c => c.IscompanyExist == true).Select(c => c).ToList();

        //        ViewBag.ImportConVal = "2";

        //        TempData["UploadContactsData"] = lstImportCon;
        //        // DataTable distconts = new DataTable();

        //        DataTable dataListconts = new DataTable();
        //        dataListconts = LTDT.ToDataTable(lstImportCon);
        //        // MergeColumns(dts1,datatbl);
        //        //DataTable dtuploadconts = (from a in dtexcel.AsEnumerable()
        //        //                           join ab in dataListconts.AsEnumerable() on a["FName"].ToString() equals ab["FName"].ToString()
        //        //                           select a).CopyToDataTable();
        //        DataTable dtuploadconts = dtexcel;

        //        DataView view = new DataView(dtuploadconts);
        //        DataTable distinctValues = view.ToTable();
        //        TempData["UploadeContactsexceldt"] = distinctValues;
        //        //  distinctValues need to be modified
        //        DataTable toprecordsdt = new DataTable();
        //        toprecordsdt = distinctValues.AsEnumerable().Take(5).CopyToDataTable();
        //        //List<DataRow> ExcelCollist = toprecordsdt.AsEnumerable().ToList();

        //        //ViewBag["ExcelCollist"] = ExcelCollist;
        //        // var lstImportConresult = (from m in lstImportCon select m).Take(5);  


        //        //pavi
        //        var extension = Path.GetExtension(Request.Files[0].FileName);
        //        string fileExt = extension.ToLower();
        //        if (fileExt == ".csv")
        //        {
        //            string filename = generateFileName() + fileExt;
        //            var Ppath = Server.MapPath("~/Documents/" + GlobalVariables.ClientName);
        //            if (!System.IO.Directory.Exists(Ppath))
        //                System.IO.Directory.CreateDirectory(Ppath);
        //            var path = Path.Combine("~/Documents/" + GlobalVariables.ClientName + "/", filename);
        //            Request.Files[0].SaveAs(Server.MapPath("~/Documents/" + GlobalVariables.ClientName + "/") + filename);

        //            Session["FileName"] = filename;
        //        }
        //        else
        //        {
        //            TempData["AlertMsg"] = "Some File(s) are in Invalid Format..!";
        //            //return Content("select only images");
        //        }







        //        return View("ImportContacts", toprecordsdt);
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw;
        //        ErrorExceptionClass x = new ErrorExceptionClass();
        //        x.catchException(ex.Message, "ImportController");
        //        return View("ImportContacts");
        //    }


        //}


        [HttpGet]
        public ActionResult GetHistory()
        {
            string Client = GlobalVariables.ClientName;

            List<ImportHistory> importhistory = _icontactsrepository.GET_ImportHistory(GlobalVariables.ConnectionString, Client);
            Session["importhistory"] = importhistory;
            try
            {
                string erromsg = Convert.ToString(TempData["importerrmsg"]);
                if (erromsg == "Error")
                {
                    ViewBag.Importerror = "Error";
                }
                return View();
            }
            catch (Exception ex)
            {
                ErrorExceptionClass x = new ErrorExceptionClass();
                x.catchException(ex.Message, "ImportController");
                return View();
            }
        }

        public ActionResult Download(string filename)
        {
            string file = Server.MapPath("~/Documents/ImportHistory/" + GlobalVariables.ClientName + "/" + filename);
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(file, contentType, Path.GetFileName(file));
        }



        [HttpPost]
        public ActionResult UploadFileContact(HttpPostedFileBase file)
        {
            try
            {
                // DataTable dtexcel = new DataTable();
                string flnm = System.IO.Path.GetFileName(Request.Files[0].FileName);
                Session["ActualFname"] = flnm;
                string flnms = flnm;
                if (flnms != "importContact.csv")
                {
                    string flpath = Server.MapPath("~/Content/ImportFilesContacts");


                    string[] files = Directory.GetFiles(flpath);
                    foreach (string filea in files)
                    {

                        System.IO.File.Delete(filea);
                    }

                }

                string flLoc = string.Format("{0}\\{1}", Server.MapPath("~/Content/ImportFilesContacts"), flnm);
                if (System.IO.File.Exists(flLoc))
                    System.IO.File.Delete(flLoc);
                List<ImportContact> lstImportCon = new List<ImportContact>();
                if ((Request.Files[0].ContentLength == 0) || file == null)
                {

                    return RedirectToAction("ImportContacts");
                }
                DataTable dtexcel = new DataTable();
                if ((Request.Files.Count > 0) && (Request.Files[0].ContentLength > 0))
                {
                    if (ModelState.IsValid)
                    {
                        DataTable dts1 = new DataTable();
                        DataTable dt = new DataTable();

                        // DataSet ds = new DataSet();
                        string[] strFieldStructure = new[] { "Initial", "FName", "MName", "LName", "Phone", "Fax", "Email", "Title", "Source", "Mobile", "CreatedDate", "ModifiedDate", "Creator", "ModifiedBy", "CompanyName", "Homephone", "OtherPhone", "AsstPhone", "Assistant", "EmailOptOut", "Description", "ReportTo", "Birthdate", "Department", "Mailingstreet", "Otherstreet", "Mailingcity", "Othercity", "Mailingstate", "Otherstate", "Mailingzip", "Otherzip", "Mailingcountry", "Othercountry", "ReportingManager", "Suspect_prospect", "SalesMgr1", "SalesMgr2", "Contact_office", "PhoneExt", "MailingAddress", "BillingAddress", "IsCorporate", "ContactIdEditable", "IsAllowSMS", "ProviderId", "Type", "SeachIndex", "FacebookUsername", "TwitterUsername", "LinkedInUsername", "GooglePlusUserName", "PinterestUsername", "SkypeUsername", "Sector" };
                        int arrylength = strFieldStructure.Length;
                        string fileExtension = System.IO.Path.GetExtension(Request.Files[0].FileName);
                        string fileName = System.IO.Path.GetFileName(Request.Files[0].FileName);
                        if (fileExtension == ".csv")
                        {
                            string fileLocation = string.Format("{0}\\{1}", Server.MapPath("~/Content/ImportFilesContacts"), fileName);
                            if (System.IO.File.Exists(fileLocation))
                                System.IO.File.Delete(fileLocation);
                            Request.Files[0].SaveAs(fileLocation);
                            if (fileExtension == ".csv")
                                fileLocation = Server.MapPath("~/Content/ImportFilesContacts");
                            string excelConnectionString = string.Empty;
                            if (fileExtension == ".csv")
                            {
                                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"text;HDR=Yes;FMT=Delimited;IMEX=1;\"";
                            }
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            excelConnection.Open();
                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            if (dt == null)
                            {
                                return null;
                            }
                            String[] excelSheets = new String[dt.Rows.Count];
                            int t = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                excelSheets[t] = row["TABLE_NAME"].ToString();
                                t++;
                            }
                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {

                                //dt
                                dataAdapter.Fill(dtexcel);
                            }

                            DataTable tempDataexcel = new DataTable();
                            tempDataexcel = dtexcel.Clone();
                            // Validation for checking the Template Format with Standard Fields
                            DataSet cds = new DataSet();
                            cds.Tables.Add(tempDataexcel);
                            excelConnection.Close();
                        }
                        // if (fileExtension == ".PNG" || fileExtension == ".pdf" || fileExtension == ".txt"||fileExtension == ".docx" || fileExtension == ".png" || fileExtension == ".jpg")

                        else
                        {
                            return RedirectToAction("ImportContacts");


                        }
                        if (dtexcel.Rows.Count == 0)
                        {
                            return RedirectToAction("ImportContacts");
                        }

                        //dt
                        lstImportCon = DTTLcontact.ToList<ImportContact>(dtexcel);
                        //lstImportCon = lstImportCon.Where(k => k.CompanyName != null).ToList();
                        //lstImportCon = lstImportCon.Where(k => k.FName != null).ToList();
                        for (int a = 0; a < lstImportCon.Count; a++)
                        {
                            DateTime CreatedDate = DateTime.Now;
                            lstImportCon[a].CreatedDate = CreatedDate;
                            lstImportCon[a].ModifiedDate = CreatedDate;
                        }



                        string excelcnts = "";
                        string excelcntdets = "";
                        flnms = flnm;
                        if (flnms != "ImportContact.csv")
                        {
                            string flLoctn = string.Format("{0}\\{1}", Server.MapPath("~/Content/ImportFilesContacts"), flnms);
                            if (System.IO.File.Exists(flLoctn))
                                System.IO.File.Delete(flLoctn);
                        }

                        string dbName = GlobalVariables.ClientName;

                        string fleName = System.IO.Path.GetFileName(Request.Files[0].FileName);
                        string fleLocation = string.Format("{0}\\{1}", Server.MapPath("~/Content/ImportFilesContacts"), fleName);
                        if (System.IO.File.Exists(fleLocation))
                            System.IO.File.Delete(fleLocation);


                    }
                }
                //lstImportCon = lstImportCon.Where(c => c.IscompanyExist == true).Select(c => c).ToList();

                ViewBag.ImportConVal = "2";

                TempData["UploadContactsData"] = lstImportCon;
                // DataTable distconts = new DataTable();

                DataTable dataListconts = new DataTable();
                dataListconts = LTDT.ToDataTable(lstImportCon);

                DataTable dtuploadconts = dtexcel;

                DataView view = new DataView(dtuploadconts);
                DataTable distinctValues = view.ToTable();
                TempData["UploadeContactsexceldt"] = distinctValues;
                //  distinctValues need to be modified
                DataTable toprecordsdt = new DataTable();
                toprecordsdt = distinctValues.AsEnumerable().Take(5).CopyToDataTable();


                //pavi
                var extension = Path.GetExtension(Request.Files[0].FileName);
                string fileExt = extension.ToLower();
                if (fileExt == ".csv")
                {
                    string filename = generateFileName() + fileExt;
                    var Ppath = Server.MapPath("~/Documents/" + GlobalVariables.ClientName);
                    if (!System.IO.Directory.Exists(Ppath))
                        System.IO.Directory.CreateDirectory(Ppath);
                    var path = Path.Combine("~/Documents/" + GlobalVariables.ClientName + "/", filename);
                    Request.Files[0].SaveAs(Server.MapPath("~/Documents/" + GlobalVariables.ClientName + "/") + filename);

                    Session["FileName"] = filename;
                }
                else
                {
                    TempData["AlertMsg"] = "Some File(s) are in Invalid Format..!";
                    //return Content("select only images");
                }



                var columnNames = from t in typeof(Account).GetProperties() select t.Name;

                ViewBag.dbcolumns = columnNames;


                return View("ImportContacts", toprecordsdt);
            }
            catch (Exception ex)
            {
                //throw;
                ErrorExceptionClass x = new ErrorExceptionClass();
                x.catchException(ex.Message, "ImportController");
                return View("ImportContacts");
            }


        }



        [HttpPost]
        public ActionResult ImportContacts(FormCollection fc)
        {
            try
            {

                var Ppath = Server.MapPath("~/Documents/");
                if (!System.IO.Directory.Exists(Ppath))
                    System.IO.Directory.CreateDirectory(Ppath);
                string rootFolderPath = Ppath + GlobalVariables.ClientName + "\\";
                string destinationPath = Ppath;

                string[] fileList = System.IO.Directory.GetFiles(rootFolderPath);

                foreach (string file in fileList)
                {
                    string fileToMove = file;

                    var filename = Path.GetFileName(file);
                    string moveTo = destinationPath + filename;
                    System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + rootFolderPath + ";Extended Properties=\"text;HDR=Yes;FMT=Delimited;IMEX=1;\"";
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();

                    DataTable dt = new DataTable();
                    DataTable dtexcel = new DataTable();
                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }
                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {

                        //dt
                        dataAdapter.Fill(dtexcel);
                    }
                    LogInTextFile("before mappings" + DateTime.Now + "");
                    string mp = Request.Form["RemoveMappings"];
                    List<string> RemoveMapValues = mp.Split(',').Select(s => s).ToList();
                    for (int i = 0; i < RemoveMapValues.Count - 1; i++)
                    {
                        string x = RemoveMapValues[i].ToString();
                        //var DataType = dummy.Columns["LName"].DataType;
                        dtexcel.Columns.Remove(x);
                        dtexcel.Columns.Add(x);
                    }
                    dtexcel.AcceptChanges();

                    string gr = Request.Form["Mappings"];
                    List<string> MapValues = gr.Split(',').Select(s => s).ToList();
                    DataTable dummy = new DataTable();

                    DevEntities obj = new DevEntities(GlobalVariables.ConnectionString);
                    List<Account> ff = obj.Accounts.Where(a => a.ID == 0).ToList();

                    //List<Account> ff = obj.Accounts.Where(a => a.ID == 0).ToList();
                    DataTable cts = new DataTable();


                    cts = LTDT.ToDataTable(ff);

                    dummy = cts.Clone();

                    LogInTextFile("loop for map" + DateTime.Now + "");
                    for (int j = 0; j <= dtexcel.Rows.Count - 1; j++)
                    {
                        DataRow row1 = dummy.NewRow();
                        for (int i = 0; i <= MapValues.Count - 2; i++)
                        {
                            string x = MapValues[i].ToString();
                            int l = x.IndexOf("+");
                            var pre = "";
                            var post = "";
                            if (l > 0)
                            {
                                pre = x.Substring(0, l);
                                post = x.Substring(x.LastIndexOf('+') + 1);
                            }
                            if (pre == "Type")
                            {
                                row1[pre] = "";
                            }
                            else if (pre == "CreatedDate")
                            {
                                row1[pre] = DBNull.Value;
                            }
                            else if (pre == "ModifiedDate")
                            {
                                row1[pre] = DBNull.Value;
                            }
                            else if (pre == "Birthdate")
                            {
                                row1[pre] = DBNull.Value;
                            }
                            else
                            {
                                row1[pre] = dtexcel.Rows[j][post];
                            }
                            //dtexcel.Columns[pre].ColumnName = post;
                        }
                        dummy.Rows.Add(row1);
                        dummy.AcceptChanges();
                    }

                    DataTable tempDataexcel = new DataTable();
                    tempDataexcel = dtexcel.Clone();
                    // Validation for checking the Template Format with Standard Fields
                    DataSet cds = new DataSet();
                    cds.Tables.Add(tempDataexcel);

                    var filnam = generateFileName();
                    var testfile = filnam + ".csv";
                    var pat = destinationPath + testfile;
                    Session["sFileName"] = testfile;





                    /////pavi----comment
                    LogInTextFile("loop for company name checking" + DateTime.Now + "");
                    for (int r = 0; r < dummy.Rows.Count; r++)
                    {

                        for (int i = 0; i < dummy.Columns.Count; i++)
                        {

                            if (dummy.Columns[i].ColumnName == "CompanyName")
                            {
                                var emailString = Convert.ToString(dummy.Rows[r][i]);

                                if (emailString == "")
                                {
                                    dummy.Rows[r].Delete();
                                    dummy.AcceptChanges();
                                    r = r - 1;
                                }

                            }

                        }
                    }
                    dummy.AcceptChanges();
                    LogInTextFile("loop for company id" + DateTime.Now + "");
                    foreach (DataRow row1 in dummy.Rows)
                    {
                        string strCompName = row1["CompanyName"].ToString();
                        //string strContName = row1["FName"].ToString();
                        if (strCompName == "")
                        {
                            row1.Delete();
                            //dtexcel.Rows.Remove(row1);
                        }

                        if (strCompName != "")
                        {
                            int demo = GetAcntIdFrmCompName(strCompName);
                            row1["CompanyID"] = demo;
                        }

                        string OwnerId = row1["OwnerID"].ToString();
                        if (OwnerId == "")
                        {
                            row1["OwnerID"] = GlobalVariables.UserID;
                        }


                    }
                    dummy.AcceptChanges();

                    StringBuilder sb = new StringBuilder();

                    IEnumerable<string> columnNames = dummy.Columns.Cast<DataColumn>().
                                                      Select(column => column.ColumnName);
                    sb.AppendLine(string.Join(",", columnNames));

                    foreach (DataRow row in dummy.Rows)
                    {
                        IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                        sb.AppendLine(string.Join(",", fields));
                    }
                    var dpath = destinationPath + testfile;
                    System.IO.File.WriteAllText(dpath, sb.ToString());
                    System.IO.File.Delete(file);
                }


                string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;
                CRMMasterClientsEntities xc = new CRMMasterClientsEntities();
                using (var obj = new CRMMasterClientsEntities(ConnectionString))
                {
                    ExcelImportFile recimport = new ExcelImportFile();
                    recimport.Status = "Pending";
                    recimport.Excelfile = Convert.ToString(Session["sFileName"]);
                    recimport.ClientId = GlobalVariables.ClientName;
                    recimport.ActualFName = Convert.ToString(Session["ActualFname"]);
                    recimport.Module = "Contact";
                    obj.ExcelImportFiles.Add(recimport);
                    obj.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View("ImportContacts");

        }

       

        public static void LogInTextFile(string text)
        {
            try
            {
                var path = ConfigurationManager.AppSettings["LogInTextFileLocation"]; //@"E:\AppServ\Example.txt";
                if (!System.IO.File.Exists(path))
                {
                    FileStream myLogfile = System.IO.File.Create(path);
                    myLogfile.Close();
                    using (TextWriter stream = new StreamWriter(path, true))
                    {
                        stream.WriteLine(text);
                        stream.Close();
                    }

                }
                else if (System.IO.File.Exists(path))
                {
                    TextWriter tw = new StreamWriter(path, true);
                    tw.WriteLine(text);
                    tw.Close();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
      

    }
}