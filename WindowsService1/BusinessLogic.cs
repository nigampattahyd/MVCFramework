using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using CRMHub.EntityFramework;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using CRM;
using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WindowsService1
{
    public class BusinessLogic
    {
        public void GetExcelList()
        {
            string Module = "Contact";
            string fileStatus = "Pending";
            //EventLog.WriteEntry("CRM_BULK_IMPORT", "getexcel Method Execution Started" + DateTime.Now.ToShortDateString());
            LogInTextFile("getexcel Method Execution Started" + DateTime.Now + "");
            try
            {
                var connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;

                var obj = new CRMMasterClientsEntities(connectionstring);


                List<CRM_GetExcelfilesbasedonstatus_Result> lstExcelfile = obj.CRM_GetExcelfilesbasedonstatus(fileStatus, Module).ToList();

                //  var lstExcelfile = (from r in obj.ExcelImportFiles.AsEnumerable()
                //where (r.Status==fileStatus)
                //select r).ToList();

                List<ExcelImportFile> lstimport = lstExcelfile.Select(objExcel => new ExcelImportFile
                {
                    Excelfile = objExcel.Excelfile,
                    Module = objExcel.Module,
                    Importoption = objExcel.Importoption,
                    Status = objExcel.Status,
                    ClientId = objExcel.ClientId,
                    Fileid = objExcel.Fileid,
                    UId = objExcel.UId,
                    ActualFName = objExcel.ActualFName
                }).ToList();

                for (int i = 0; i < lstimport.Count; i++)
                {
                    //EventLog.WriteEntry("CRM_BULK_IMPORT", "importcontacts Method Execution called" + DateTime.Now.ToShortDateString());
                    LogInTextFile("importcontacts Method Execution called" + DateTime.Now + "");
                    ImportContacts(lstimport[i]);
                }


            }

            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
            //EventLog.WriteEntry("Method Execution Done" + DateTime.Now.ToShortDateString());
            //Thread.Sleep(1 * 1000 * 60);
        }
        public void ImportContacts(ExcelImportFile excelimportfle)
        {
            //EventLog.WriteEntry("CRM_BULK_IMPORT", "importcontacts Method Execution started" + DateTime.Now.ToShortDateString());
            LogInTextFile("importcontacts Method Execution started" + DateTime.Now + "");
            try
            {
                string getExcel = excelimportfle.Excelfile;
                string getStatus = excelimportfle.Status;
                int getFieldId = excelimportfle.Fileid;
                string getClientId = excelimportfle.ClientId;
                string getModule = excelimportfle.Module;
                string GetOption = excelimportfle.Importoption;
                string userid = excelimportfle.UId;
                string ActualFilename = excelimportfle.ActualFName;

                var CrmConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["CRMDevEntities"].ConnectionString;
                DataTable dtUploadContactsexcel = new DataTable();
                DataTable exceltodt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dtexclconts = new DataTable();
                string exel = getExcel;
                string filepath = ConfigurationManager.AppSettings["ImportFilePath"].ToString();
                string fileextension = System.IO.Path.GetExtension(exel);
                string xv = filepath + exel;
                string excelConnectionString = string.Empty;
                if (fileextension == ".csv")
                {
                    excelConnectionString = "Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + System.IO.Path.GetDirectoryName(xv) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"";
                }
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                excelConnection.Open();
                //EventLog.WriteEntry("CRM_BULK_IMPORT", "excel connection Method  opened" + DateTime.Now.ToShortDateString());
                LogInTextFile("excel connection Method  opened" + DateTime.Now + "");
                dt1 = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                String[] excelSheets = new String[dt1.Rows.Count];
                int t = 0;
                foreach (DataRow row in dt1.Rows)
                {
                    excelSheets[t] = row["TABLE_NAME"].ToString();
                    t++;
                }
                OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                //EventLog.WriteEntry("CRM_BULK_IMPORT", "excelConnectionString opened" + DateTime.Now.ToShortDateString());
                LogInTextFile("excelConnectionString opened" + DateTime.Now + "");
                exel = exel.Replace('.', '#');
                for (int n = 0; n < excelSheets.Length; n++)
                {
                    if (excelSheets[n] == exel)
                    {
                        LogInTextFile("both file names are same" + DateTime.Now + "");
                        string query = string.Format("Select * from [{0}]", excelSheets[n]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(exceltodt);
                            LogInTextFile("fill data to datatable" + DateTime.Now + "");
                        }
                        dtUploadContactsexcel = exceltodt;
                        List<ImportContact> lstContact = new List<ImportContact>();
                        // To form  data table dt1 from excel
                        List<ImportContact> lstImportCon = new List<ImportContact>();
                        lstImportCon = DTTLcontact.ToList<ImportContact>(exceltodt);
                        lstImportCon = lstImportCon.Where(k => k.CompanyName != null).ToList();
                        //lstImportCon = lstImportCon.Where(k => k.FName != null).ToList();
                        for (int b = 0; b < lstImportCon.Count; b++)
                        {
                            DateTime CreatedDate = DateTime.Now;
                            lstImportCon[b].CreatedDate = CreatedDate;
                            lstImportCon[b].ModifiedDate = CreatedDate;
                        }
                        LogInTextFile("" + lstImportCon.Count + DateTime.Now + "");
                        for (int k = 0; k < lstImportCon.Count; k++)
                        {
                            //   bool objcompanyname = _icompanyrepository.CheckCompanyExistsOrNot(lstImportCon[k].CompanyName, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            if (lstImportCon[k].CompanyName != "0")
                            {
                                // lstImportCon[k].CompanyName = GetAcntIdFrmCompName(lstImportCon[k].CompanyName).ToString();
                                lstImportCon[k].IscompanyExist = true;
                            }
                            else
                            {
                                lstImportCon[k].IscompanyExist = false;
                                Guid serialGuid = Guid.NewGuid();
                                string uniqueSerial = serialGuid.ToString("N");
                                string uniqueSerialLength = uniqueSerial.Substring(0, 12).ToUpper();

                                char[] serialArray = uniqueSerialLength.ToCharArray();
                                string finalSerialNumber = "";

                                int j = 0;
                                for (int i = 0; i < 12; i++)
                                {
                                    for (j = i; j < 4 + i; j++)
                                    {
                                        finalSerialNumber += serialArray[j];
                                    }
                                    if (j == 12)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        i = (j) - 1;
                                        finalSerialNumber += "-";
                                    }
                                }
                                lstImportCon[k].CompanyName = finalSerialNumber;
                            }

                        }

                        
                        LogInTextFile("" + exceltodt.Rows.Count + DateTime.Now + "");
                        string dbName = getClientId;
                        // string dbName = GlobalVariables.ClientName;
                        lstImportCon = lstImportCon.Where(c => c.IscompanyExist == true).Select(c => c).ToList();
                        LogInTextFile("" + lstImportCon.Count + DateTime.Now + "");
                        //  ViewBag.ImportConVal = "2";
                        // TempData["UploadContactsData"] = lstImportCon;
                        lstContact = lstImportCon;
                        //  lstContact = (List<ImportContact>)TempData["UploadContactsData"];
                        lstContact = lstContact.Where(l => l.IscompanyExist == true).Select(l => l).ToList();
                        for (int k = 0; k < lstContact.Count; k++)
                        {
                            var Contactoffice = lstContact[k].Contact_office;
                            if (Contactoffice == null)
                            {
                                lstContact[k].Contact_office = "1";
                            }
                            if (Contactoffice != null)
                            {
                                lstContact[k].Contact_office = Contactoffice;
                            }
                        }
                        //lstContact.ForEach(l => l.Contact_office = "-1");
                        DataTable dtContacts = new DataTable();
                        //  dtexclconts = (DataTable)TempData["UploadeContactsexceldt"];
                        dtexclconts = dtUploadContactsexcel;
                        //dtContacts = LTDT.ToDataTable(lstContact);
                        dtContacts = dtexclconts;
                        //pavi---for testing error
                        //DataTable dtxlimport = (from a in dtexclconts.AsEnumerable()
                        //                        join ab in dtContacts.AsEnumerable() on a["FName"].ToString() equals ab["FName"].ToString()
                        //                        select a).CopyToDataTable();
                        //var notAllowedColNames = dtexclconts.Columns.Cast<DataColumn>()
                        //  .Select(c => c.ColumnName.ToUpperInvariant()).Intersect
                        //  (dtContacts.Columns.Cast<DataColumn>().Select(r => r.ColumnName.ToUpperInvariant()))
                        //  .ToList();
                        //foreach (var colName in notAllowedColNames)
                        //    dtexclconts.Columns.Remove(colName);
                        //dtContacts = LTDT.ToDataTable(lstContact);
                        //dtContacts.Columns.Remove("FieldValue");
                        //dtContacts.Columns.Remove("FieldHeader");

                        if (dtContacts != null)
                        {
                            LogInTextFile("before import contacts" + DateTime.Now + "");
                            string cs = ConfigurationManager.ConnectionStrings["ImportContacts"].ConnectionString;
                            LogInTextFile("after import contacts" + DateTime.Now + "");
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
                                    InitialCatalog = getClientId;
                                    //InitialCatalog = GlobalVariables.ClientName;
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

                            //var CrmConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["CRMDevEntities"].ConnectionString;   

                            // string CrmPath = ConfigurationManager.AppSettings["CRMDevEntities"].ToString();

                            DataTable errorDT = new DataTable();
                            errorDT.Clear();
                                    errorDT.Columns.Add("Email");
                                    errorDT.Columns.Add("Message");
                                    int Tcount = dtContacts.Rows.Count;
                                    for (int r = 0; r < dtContacts.Rows.Count; r++)
                                    {

                                        for (int i = 0; i < dtContacts.Columns.Count; i++)
                                        {
                                            var message = "";
                                            if (dtContacts.Columns[i].ColumnName == "Email")
                                            {
                                                var emailString = Convert.ToString(dtContacts.Rows[r][i]);
                                                if (emailString != "")
                                                {
                                                    bool isEmail = Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                                                    if (isEmail != true)
                                                    {
                                                        message = "Invalid Email Format";
                                                        DataRow newrow = errorDT.NewRow();
                                                        newrow["Email"] = emailString;
                                                        newrow["Message"] = message;
                                                        errorDT.Rows.Add(newrow);
                                                        errorDT.AcceptChanges();
                                                        dtContacts.Rows[r].Delete();
                                                        r = r - 1;
                                                        dtContacts.AcceptChanges();
                                                    }
                                                }
                                                else
                                                {
                                                    message = "Blank Email";
                                                    DataRow newrow = errorDT.NewRow();
                                                    newrow["Email"] = emailString;
                                                    newrow["Message"] = message;
                                                    errorDT.Rows.Add(newrow);
                                                    errorDT.AcceptChanges();
                                                    dtContacts.Rows[r].Delete();
                                                    r = r - 1;
                                                    dtContacts.AcceptChanges();
                                                }

                                            }

                                        }
                                    }
                                        dtContacts.AcceptChanges();

                                        // create error history file

                                        StringBuilder sb = new StringBuilder();

                                        IEnumerable<string> columnNames = errorDT.Columns.Cast<DataColumn>().
                                                                          Select(column => column.ColumnName);
                                        sb.AppendLine(string.Join(",", columnNames));

                                        foreach (DataRow row1 in errorDT.Rows)
                                        {
                                            IEnumerable<string> fields = row1.ItemArray.Select(field => field.ToString());
                                            sb.AppendLine(string.Join(",", fields));
                                        }

                                        string dtpath = filepath + "ImportHistory" + "\\" + getClientId + "\\";
                                        if (!System.IO.Directory.Exists(dtpath))
                                            System.IO.Directory.CreateDirectory(dtpath);

                                        var filnam = generateFileName();
                                        var testfile = filnam + ".csv";
                                        var dpath = dtpath + testfile;
                                        System.IO.File.WriteAllText(dpath, sb.ToString());

                                        //insert for history
                                        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;
                                        CRMMasterClientsEntities xc = new CRMMasterClientsEntities();
                                        using (var obj = new CRMMasterClientsEntities(ConnectionString))
                                        {
                                            ImportHistory recimport = new ImportHistory();
                                            recimport.ExcelFile = ActualFilename;
                                            recimport.TotalRecords = Tcount;
                                            recimport.ClientId = getClientId;
                                            recimport.FailedRecords = errorDT.Rows.Count;
                                            recimport.Filepath = testfile;
                                            obj.ImportHistories.Add(recimport);
                                            obj.SaveChanges();
                                        }
                                    


                            foreach (DataRow row in dtContacts.Rows)
                            {

                                for (int i = 0; i < dtContacts.Columns.Count; i++)
                                {
                                    //if (dtContacts.Columns[i].ColumnName == "CompanyName")
                                    //{
                                    //    string AccountName = Convert.ToString(row[i]);
                                    //    int AccountId = getAccountIdFrmCompName(AccountName, CrmConnStr, getClientId);
                                    //    //int AccountId = getAccountIdFrmCompName(AccountName, GlobalVariables.ConnectionString, getClientId);
                                    //    //string AccountName = _icompanyrepository.getCompNameFrmAccountId(accountid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                                    //    row[i] = AccountId;
                                    //}
                                    if (dtContacts.Columns[i].ColumnName == "AccountTypeId")
                                    {
                                        row[i] = "2";
                                    }
                                }
                            }















                            //pavi
                            var AllRecords = from item in dtContacts.AsEnumerable() select item;
                            int nextRecords = 0;

                            DataTable dtc = new DataTable();
                            while (true)
                            {
                                var top10000 = AllRecords.Skip(nextRecords).Take(10000);
                                if (top10000.Count() == 0)
                                {
                                    break;
                                }
                                else
                                {

                                    dtc = top10000.CopyToDataTable();

                                }

                                using (var connection = new SqlConnection(strCon))
                                {
                                    SqlTransaction transaction = null;
                                    LogInTextFile("for bulk copy before con open" + DateTime.Now + "");
                                    connection.Open();
                                    //EventLog.WriteEntry("CRM_BULK_IMPORT", "for bulk copy con open" + DateTime.Now.ToShortDateString());
                                    LogInTextFile("for bulk copy con open" + DateTime.Now + "");

                                    try
                                    {
                                        transaction = connection.BeginTransaction();
                                        using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, transaction))
                                        {
                                            sqlBulkCopy.DestinationTableName = "Accounts";
                                            //sqlBulkCopy.ColumnMappings.Add("Initial", "Initial");-----x
                                            sqlBulkCopy.ColumnMappings.Add("FirstName", "FirstName");
                                            sqlBulkCopy.ColumnMappings.Add("Phone", "Phone");
                                            sqlBulkCopy.ColumnMappings.Add("Fax", "Fax");
                                            sqlBulkCopy.ColumnMappings.Add("Email", "Email");
                                            sqlBulkCopy.ColumnMappings.Add("Title", "Title");
                                            //sqlBulkCopy.ColumnMappings.Add("Source", "Source");-----x
                                            //sqlBulkCopy.ColumnMappings.Add("Mobile", "Mobile");------x
                                            sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                                            sqlBulkCopy.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
                                            //-           sqlBulkCopy.ColumnMappings.Add("Creator", "CreatedBy");
                                            //-           sqlBulkCopy.ColumnMappings.Add("ModifiedBy", "ModifiedBy");
                                            sqlBulkCopy.ColumnMappings.Add("CompanyID", "CompanyID");
                                            //sqlBulkCopy.ColumnMappings.Add("Homephone", "Homephone");----x
                                            //sqlBulkCopy.ColumnMappings.Add("OtherPhone", "OtherPhone");----x
                                            //sqlBulkCopy.ColumnMappings.Add("AsstPhone", "AsstPhone");----x
                                            //sqlBulkCopy.ColumnMappings.Add("Assistant", "Assistant");----x
                                            //sqlBulkCopy.ColumnMappings.Add("EmailOptOut", "EmailOptOut");---x
                                            sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                                            //sqlBulkCopy.ColumnMappings.Add("ReportTo", "ReportTo");----x
                                            //sqlBulkCopy.ColumnMappings.Add("Birthdate", "Birthdate");----x
                                            //sqlBulkCopy.ColumnMappings.Add("Department", "Department");----x
                                            //sqlBulkCopy.ColumnMappings.Add("Mailingstreet", "MailingAddress");----duplict
                                            sqlBulkCopy.ColumnMappings.Add("MailingAddress2", "MailingAddress2");
                                            sqlBulkCopy.ColumnMappings.Add("Mailingcity", "Mailingcity");
                                            //sqlBulkCopy.ColumnMappings.Add("Othercity", "Othercity");----x
                                            //-            sqlBulkCopy.ColumnMappings.Add("Mailingstate", "MailingstateID");
                                            //sqlBulkCopy.ColumnMappings.Add("Otherstate", "Otherstate");-----x
                                            sqlBulkCopy.ColumnMappings.Add("Mailingzip", "Mailingzip");
                                            //sqlBulkCopy.ColumnMappings.Add("Otherzip", "Otherzip");-------x
                                            //-             sqlBulkCopy.ColumnMappings.Add("Mailingcountry", "MailingcountryID");
                                            //sqlBulkCopy.ColumnMappings.Add("Othercountry", "Othercountry");--------x
                                            //sqlBulkCopy.ColumnMappings.Add("ReportingManager", "ReportingManager");---------x
                                            //sqlBulkCopy.ColumnMappings.Add("Suspect_prospect", "Suspect_prospect");-----------x
                                            //sqlBulkCopy.ColumnMappings.Add("SalesMgr1", "SalesMgr1");---------------x
                                            //sqlBulkCopy.ColumnMappings.Add("SalesMgr2", "SalesMgr2");----------------x
                                            //-              sqlBulkCopy.ColumnMappings.Add("Contact_office", "ContactTypeID");
                                            //sqlBulkCopy.ColumnMappings.Add("PhoneExt", "PhoneExt");----------------x
                                            sqlBulkCopy.ColumnMappings.Add("MailingAddress", "MailingAddress");
                                            sqlBulkCopy.ColumnMappings.Add("BillingAddress", "BillingAddress");
                                            //sqlBulkCopy.ColumnMappings.Add("IsCorporate", "IsCorporate");-----------x
                                            //sqlBulkCopy.ColumnMappings.Add("ContactIdEditable", "ContactIdEditable");------x
                                            //sqlBulkCopy.ColumnMappings.Add("IsAllowSMS", "IsAllowSMS");---------x
                                            //sqlBulkCopy.ColumnMappings.Add("ProviderId", "ProviderId");------x
                                            sqlBulkCopy.ColumnMappings.Add("AccountTypeId", "AccountTypeId");
                                            sqlBulkCopy.ColumnMappings.Add("SeachIndex", "SeachIndex");
                                            sqlBulkCopy.ColumnMappings.Add("FacebookUsername", "FacebookUsername");
                                            sqlBulkCopy.ColumnMappings.Add("TwitterUsername", "TwitterUsername");
                                            sqlBulkCopy.ColumnMappings.Add("LinkedInUsername", "LinkedInUsername");
                                            //sqlBulkCopy.ColumnMappings.Add("GooglePlusUserName", "GooglePlusUserName");--------x
                                            //sqlBulkCopy.ColumnMappings.Add("PinterestUsername", "PinterestUsername");-------x
                                            sqlBulkCopy.ColumnMappings.Add("SkypeUsername", "SkypeUsername");
                                            //sqlBulkCopy.ColumnMappings.Add("Sector", "Sector"); -------------x                           
                                            sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                                            sqlBulkCopy.ColumnMappings.Add("LastName", "LastName");
                                            sqlBulkCopy.ColumnMappings.Add("CompanyName", "CompanyName");
                                            sqlBulkCopy.ColumnMappings.Add("OwnerID", "OwnerID");















                                            sqlBulkCopy.WriteToServer(dtc);

                                            nextRecords = nextRecords + 10000;

                                        }
                                        transaction.Commit();
                                        // ViewBag.ImportContComp = "3";
                                    }
                                    catch (Exception ex)
                                    {
                                        var excptn = Convert.ToString(ex.Message);
                                        //EventLog.WriteEntry("CRM_BULK_IMPORT", excptn + "exception" + DateTime.Now.ToShortDateString());
                                        LogInTextFile(excptn + "exception" + DateTime.Now + "");
                                        transaction.Rollback();
                                    }
                                    
                                }
                            }//pavi
                            //  ViewBag.ImportContComp = "3";
                        }

                    }
                    //EventLog.WriteEntry("CRM_BULK_IMPORT", "into else" + DateTime.Now.ToShortDateString());
                    LogInTextFile("into else" + DateTime.Now + "");
                }
                //EventLog.WriteEntry("CRM_BULK_IMPORT", "records inserted" + DateTime.Now.ToShortDateString());
                LogInTextFile("records inserted" + DateTime.Now + "");
                excelimportfle.Status = "Success";
                int updateres = UpdateImportFileStatus(excelimportfle);
                if (updateres > 0)
                {
                    string LogoLoc = "";
                    exel = exel.Replace('#', '.');
                    //LogoLoc = string.Format("{0}\\{1}", Server.MapPath("~/Content/ScriptFiles"), scriptfile);
                    //LogoLoc = filepath + "\\" + exel;
                    LogoLoc = filepath + exel;

                    if (System.IO.File.Exists(LogoLoc))
                        System.IO.File.Delete(LogoLoc);

                    //filepath
                }

            }
            catch (Exception ex)
            {
                LogInTextFile(ex.Message + DateTime.Now + "");
                throw;
            }
        }
        private string generateFileName()
        {
            string pFileName = "";
            Guid fileName = Guid.NewGuid();
            pFileName = fileName.ToString();
            return pFileName;
        }
        public int UpdateImportFileStatus(ExcelImportFile updateExcelData)
        {
            try
            {
                var connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;
                var obj = new CRMMasterClientsEntities(connectionstring);
                string fieldid = Convert.ToString(updateExcelData.Fileid);
                string Status = updateExcelData.Status;
                string module = updateExcelData.Module;
                string excelfile = updateExcelData.Excelfile;
                string clientid = updateExcelData.ClientId;
                var Result = obj.CRM_UpdateImportExcel(fieldid, Status, module);
                return Result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Custom_Manage_Master> GetAllCustomField(long userId, string moduleId, int flag, string Connectionstring, string dbName)
        {
            try
            {
                using (CRMDevEntities ClientEntity = new CRMDevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    List<CRM_GetCustomFields_Result> lstCustomFields = ClientEntity.CRM_GetCustomFields(userId, moduleId, flag).ToList();
                    if (lstCustomFields.Count > 0)
                    {
                        return lstCustomFields.Select(customfields => new Custom_Manage_Master
                        {
                            FieldId = customfields.FieldId,
                            Module = customfields.Module,
                            Column_Id = customfields.Column_Id,
                            ActualColumnName = customfields.ActualColumnName,
                            Column_Label = customfields.Column_Label,
                            Column_Type = customfields.Column_Type,
                            Field_Type = customfields.Field_Type,
                            Column_Description = customfields.Column_Description,
                            HoverText = customfields.HoverText,
                            InputDataType = customfields.InputDataType,
                            DefaultValue = customfields.DefaultValue,
                            DisplayPosition = customfields.DisplayPosition,
                            MaxLength = customfields.MaxLength,
                            RequiredField = customfields.RequiredField,
                            MultiValued = customfields.MultiValued,
                            ColumnDrpChkValues = customfields.ColumnDrpChkValues,
                            ModifiedDate = customfields.ModifiedDate,
                            CreatedDate = customfields.CreatedDate,
                            IsActive = customfields.IsActive,
                        }).ToList();
                    }
                    else
                    {
                        List<Custom_Manage_Master> objcustom = new List<Custom_Manage_Master>();
                        return objcustom;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckIfmasterlabelExists(string mastercolumnlabel, string Connectionstring, string dbName)
        {
            var Result = false;
            try
            {
                CRMDevEntities obj = new CRMDevEntities();

                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var res = obj.CRM_IscolumnlabelExists(mastercolumnlabel);
                if (res.Count() != 0)
                {
                    Result = true;
                }
                return Result;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public int GetContIdFrmFName(string cntname, string cntLName, string email, string Connectionstring, string dbName)
        {
            CRMDevEntities obj = new CRMDevEntities(Connectionstring);
            try
            {

                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                tbl_Contact ContactEntity = new tbl_Contact();
                if (cntname != "" && cntLName != "" && email != "")
                {
                    ContactEntity = obj.tbl_Contact.Where(tblc => tblc.Fname == cntname && tblc.LName == cntLName && tblc.Email == email).FirstOrDefault();
                }
                else if (cntname == "" && cntLName != "" && email != "")
                {
                    ContactEntity = obj.tbl_Contact.Where(tblc => tblc.LName == cntLName && tblc.Email == email).FirstOrDefault();
                }
                else if (cntname != "" && cntLName == "" && email != "")
                {
                    ContactEntity = obj.tbl_Contact.Where(tblc => tblc.Fname == cntname && tblc.Email == email).FirstOrDefault();
                }
                else if (cntname != "" && cntLName != "" && email == "")
                {
                    ContactEntity = obj.tbl_Contact.Where(tblc => tblc.Fname == cntname && tblc.LName == cntLName).FirstOrDefault();
                }
                else if (cntname != "" && cntLName == "" && email == "")
                {
                    ContactEntity = obj.tbl_Contact.Where(tblc => tblc.Fname == cntname).FirstOrDefault();
                }
                else if (cntname == "" && cntLName != "" && email == "")
                {
                    ContactEntity = obj.tbl_Contact.Where(tblc => tblc.LName == cntLName).FirstOrDefault();
                }
                else if (cntname == "" && cntLName == "" && email != "")
                {
                    ContactEntity = obj.tbl_Contact.Where(tblc => tblc.Email == email).FirstOrDefault();
                }


                int ConId = ContactEntity.ContactId;

                return ConId;


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                obj.Database.Connection.Close();
            }
        }

        public Custom_Manage_Master getManageMasterdetails(string Column_Label, string Connectionstring, string dbName)
        {
            try
            {
                CRMDevEntities obj = new CRMDevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                Custom_Manage_Master Custom_Manage_Master = new Custom_Manage_Master();
                Custom_Manage_Master = obj.Custom_Manage_Master.Where(c => c.Column_Label == Column_Label).FirstOrDefault();
                return Custom_Manage_Master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string getOptionId(string OptValue, int FieldId, string Connectionstring, string dbName)
        {
            try
            {
                CRMDevEntities obj = new CRMDevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                // var cmp = Convert.ToInt32(Column_Label);

                Custom_DrpChkValues customOptionsValues = new Custom_DrpChkValues();
                customOptionsValues = obj.Custom_DrpChkValues.Where(o => o.DrpValue == OptValue && o.FieldId == FieldId).FirstOrDefault();
                if (customOptionsValues != null)
                    return customOptionsValues.DrpValueId.ToString();
                else
                    return "0";
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insertcustomvalue(Custom_Value_Master CVMObj, string Connectionstring, string dbName)
        {
            using (CRMDevEntities ClientEntity = new CRMDevEntities(Connectionstring))
            {
                ClientEntity.Database.Connection.Open();
                ClientEntity.Database.Connection.ChangeDatabase(dbName);
                ClientEntity.Custom_Value_Master.Add(CVMObj);
                ClientEntity.SaveChanges();
                return true;
            }
        }


        public int getAccountIdFrmCompName(string AccountName, string Connectionstring, string dbName)
        {
            using (CRMDevEntities ClientEntity = new CRMDevEntities(Connectionstring))
            {
                ClientEntity.Database.Connection.Open();
                ClientEntity.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("OutCompanyId", typeof(int));
                int id = ClientEntity.CRM_GetAccountIdFromCompanyName(AccountName, output);
                return id;
            }
        }


        public static void LogInTextFile(string text)
        {
            //try
            //{
            //    var path = ConfigurationManager.AppSettings["LogInTextFileLocation"]; //@"E:\AppServ\Example.txt";
            //    if (!File.Exists(path))
            //    {
            //        File.Create(path);
            //        TextWriter tw = new StreamWriter(path);
            //        tw.WriteLine(text);
            //        tw.Close();
            //    }
            //    else if (File.Exists(path))
            //    {
            //        TextWriter tw = new StreamWriter(path, true);
            //        tw.WriteLine(text);
            //        tw.Close();
            //    }
            //}
            try
            {
                var path = ConfigurationManager.AppSettings["LogInTextFileLocation"]; //@"E:\AppServ\Example.txt";
                if (!File.Exists(path))
                {
                    FileStream myLogfile = File.Create(path);
                    myLogfile.Close();
                    using (TextWriter stream = new StreamWriter(path, true))
                    {
                        stream.WriteLine(text);
                        stream.Close();
                    }

                }
                else if (File.Exists(path))
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
