using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;

namespace CRM.Controllers
{
    public class CreateClientsController : Controller
    {
        //private readonly IClientRepository _iclientrepository;
        //private readonly IUserMasterRepository _iUserMasterRepository;
        //private readonly ICreateClientsRepository _icreateclientrepository;
        //public CreateClientsController(IClientRepository iclientrepository, IUserMasterRepository iUserMasterRepository, ICreateClientsRepository icreateclientrepository)
        //{
        //    _iclientrepository = iclientrepository;
        //    _iUserMasterRepository = iUserMasterRepository;
        //    _icreateclientrepository = icreateclientrepository;
        //}
        private readonly IClientRepository _iclientrepository;
        private readonly IUserMasterRepository _iUserMasterRepository;
        private readonly ICreateClientsRepository _icreateclientrepository;
        private readonly ICommonRepository _istateRepository;
        private readonly IOfficeRepository _iofficerepository;
        private readonly IUserRepository _iuserrepository;
        public CreateClientsController(IClientRepository iclientrepository, IUserMasterRepository iUserMasterRepository, ICreateClientsRepository icreateclientrepository, ICommonRepository istateRepository, IOfficeRepository iofficerepository, IUserRepository iuserrepository)
        {
            _iclientrepository = iclientrepository;
            _iUserMasterRepository = iUserMasterRepository;
            _icreateclientrepository = icreateclientrepository;
            _istateRepository = istateRepository;
            _iofficerepository = iofficerepository;
            _iuserrepository = iuserrepository;
        }

        // GET: CreateClients
        public ActionResult Index()
        {
            Client ClientObj = new Client();
            //ClientObj.BranchStateCode = "ZZ";
            ClientObj.Level = "SuperAdmin";
            return View(ClientObj);
        }


        [HttpPost]
        public ActionResult AddClientsdata(Client clientsdts, HttpPostedFileBase file)
        // public string AddClientsdata(Client clientsdts)
        {
            try
            {

                //string msg = "New Clients Created Successfully";
                //string alertmsg = "Database Already Exists";
                string Comlogo = System.IO.Path.GetFileName(Request.Files[0].FileName);

                if (Comlogo != "")
                {
                    string logopath = Server.MapPath("~/Content/CompanyLogos");
                    bool exists = System.IO.Directory.Exists(logopath);
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(logopath);
                    }
                    string LogoLoc = string.Format("{0}\\{1}", Server.MapPath("~/Content/CompanyLogos"), Comlogo);
                    clientsdts.companylogo = Comlogo;
                    if (System.IO.File.Exists(LogoLoc))
                        System.IO.File.Delete(LogoLoc);
                    Request.Files[0].SaveAs(LogoLoc);
                }
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
                        InitialCatalog = constrg.Replace("Initial Catalog=", "");
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
                string INTC = GlobalVariables.ClientName;
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

                string dbname = INTC;
                string dbname1 = clientsdts.ClientID;
                string dbname2 = InitialCatalog;

                TempData["dbconx"] = strCon;
                TempData["dbnm"] = clientsdts.ClientID;

                var dbexists = _icreateclientrepository.testDatabaseExists(dataSource, dbname1, strCon);
                if (dbexists == false)
                {
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
                    var tokenkey = finalSerialNumber;
                      var response = _icreateclientrepository.AddClientsdata(clientsdts, dataSource, strCon, tokenkey, strCon);                   
                      addclient(dbname, strCon);
                     // write a function to create a branch and User
                    // first create branch and then user
                      insertbranchandUser(clientsdts);
                }

                //if (dbexists == true)
                //{
                //    return alertmsg;
                //}
                
                // string emailto = clientsdts.Email;
                //string password = clientsdts.Password;
                // string clientName = clientsdts.CompanyName;
                // sendEmailWithCredentials(emailto, cs, password, clientName);
                //return msg;
                TempData["Altmsg"] = "Client Created Successfully !!!";
                return RedirectToAction("ClientManagerIndex", "ClientManager");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Getkey()
        {
            Guid serialGuid = Guid.NewGuid();
            string uniqueSerial = serialGuid.ToString("N");

            string uniqueSerialLength = uniqueSerial.Substring(0, 28).ToUpper();

            char[] serialArray = uniqueSerialLength.ToCharArray();
            string finalSerialNumber = "";

            int j = 0;
            for (int i = 0; i < 28; i++)
            {
                for (j = i; j < 4 + i; j++)
                {
                    finalSerialNumber += serialArray[j];
                }
                if (j == 28)
                {
                    break;
                }
                else
                {
                    i = (j) - 1;
                    finalSerialNumber += "-";
                }
            }

            return finalSerialNumber;
        }

        public bool addclient(string dbname, string conx)
        {
            #region Create database and Restore the DB
            try
            {
                string dbnm = TempData["dbnm"].ToString();
                // string conxs= TempData["dbconx"].ToString();
                SqlConnection con = new SqlConnection(conx);
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ToString());    
                string BackUpPath = ConfigurationManager.AppSettings["RPbackupFile"].ToString();
                string Path = ConfigurationManager.AppSettings["sqlMDFPath"].ToString();
                string path1 = ConfigurationManager.AppSettings["sqlLDFPath"].ToString();
                string strCommandText = "RESTORE DATABASE " + dbnm;
                strCommandText += @" FROM DISK=N'" + BackUpPath + "'";
                strCommandText += @" WITH MOVE 'test' TO '" + Path + dbnm + "_Data.mdf',";
                strCommandText += @" MOVE 'test_log' TO '" + path1 + dbnm + "_Log.ldf',";
                strCommandText += " FILE = 1,";
                strCommandText += " STATS = 25";
                con.Open();
                SqlCommand SQLCMD = new SqlCommand(strCommandText, con);
                SQLCMD.CommandType = System.Data.CommandType.Text;
                int D = SQLCMD.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            #endregion

            return true;
        }




        public bool insertbranchandUser(Client clientdetails)
        {

            string cClientid = clientdetails.ClientID;
            Client objclienttype = _iclientrepository.GetSiteTypeByClientID(cClientid);
            if (objclienttype != null)
            {
                Client clientEntity = _iclientrepository.ValidateClient(cClientid, objclienttype.site_type);
                string ConString = objclienttype.ConnectionString.ToString();
                string ClientName = clientEntity.ClientID;
                //Branch BranchObj = new Branch();
                //BranchObj.BranchName = clientdetails.BranchName;
                //BranchObj.BranchAddress1 = clientdetails.BranchAddress1;
                //BranchObj.BranchCity = clientdetails.BranchCity;
                //BranchObj.BranchStateCode = clientdetails.BranchStateCode;
                //BranchObj.BranchZip = clientdetails.BranchZip;
                //BranchObj.BranchPhone = clientdetails.BranchPhone;

                //int result = _iofficerepository.createBranch(BranchObj, ConString, ClientName);

                //int branchID = 1;
                //Branch objBranch = _iuserrepository.GetBranchDetailsByBranchName(branchID, ConString, ClientName);
                user usersObj = new user();
                usersObj.FirstName = clientdetails.FirstName;
                usersObj.LastName = clientdetails.LastName;
                usersObj.RoleId = 1;
                //usersObj.RoleId = 18;
                //usersObj.BranchId = "1";
                //usersObj.Phone = objBranch.BranchPhone;
                //usersObj.AddressLine1 = objBranch.BranchAddress1;
                //usersObj.City = objBranch.BranchCity;
                //usersObj.StateCode = objBranch.BranchStateCode;
                usersObj.Email = clientdetails.Email;

                usersObj.LoginId = clientdetails.LoginId;
                usersObj.Password = EncryptDecrypt.Encrypt(clientdetails.Password);               
               
                int createdresult = _iuserrepository.CreateNewUser(usersObj, ConString, ClientName);
            }

            return true;
        }





        public ActionResult GenerateScript()
        {
            List<Client> lstclients = _iUserMasterRepository.Getallclientmaster(GlobalVariables.ConnectionString, GlobalVariables.ClientName).ToList().OrderByDescending(x => x.ClientNumber).ToList();
            return View(lstclients);
        }

        // Function to Execute Script to the selected Clients
        [HttpPost]
        public bool Executescriptforclientsid()
        {
            try
            {
                string LogoLoc = "";
                string ClientIDs = Request.Form["ClientIDs"];
                foreach (var ClientID in ClientIDs.Split(','))
                {
                    string cs = ConfigurationManager.ConnectionStrings["ImportCompanies"].ConnectionString;
                    string conx = cs;
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
                            InitialCatalog = constrg.Replace("Initial Catalog=", "");
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
                    string INTC = ClientID;
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

                    // To get the File Name 
                    HttpPostedFileBase hpf = Request.Files[0];
                    string scriptfile = hpf.FileName;
                    // To Set up the Directory
                    string logopath = Server.MapPath("~/Content/ScriptFiles");
                    bool exists = System.IO.Directory.Exists(logopath);
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(logopath);
                    }

                    LogoLoc = string.Format("{0}\\{1}", Server.MapPath("~/Content/ScriptFiles"), scriptfile);


                    if (System.IO.File.Exists(LogoLoc))
                        System.IO.File.Delete(LogoLoc);
                    Request.Files[0].SaveAs(LogoLoc);
                    // Executing the Script file
                    string sqlConnectionString = strCon;
                    using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        string script = System.IO.File.ReadAllText(LogoLoc);
                        command.CommandText = script.Replace("GO", "");
                        connection.Open();
                        int affectedRows = command.ExecuteNonQuery();
                    }


                }

                // Deleting the Script file after executing the Script File
                System.IO.File.Delete(LogoLoc);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

// Function to Edit the Client Details 
        // Note: We can't Edit the ClientId Field
        [HttpGet]
        public ActionResult EditClient(string ClientID)
        {
            try
            {
                Client clientsdts = new Client();
               // tbl_Contact objContact = _icontactrepository.getContactDetailsByContactId(contactId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                clientsdts = _icreateclientrepository.GetClientDetailsByClientid(ClientID, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                clientsdts.Level = "SuperAdmin";

                string cClientid = clientsdts.ClientID;
                Client objclienttype = _iclientrepository.GetSiteTypeByClientID(cClientid);
                if (objclienttype != null)
                {
                    Client clientEntity = _iclientrepository.ValidateClient(cClientid, objclienttype.site_type);
                    string ConString = objclienttype.ConnectionString.ToString();
                    string ClientName = clientEntity.ClientID;
                    user usersObj = new user();
                    int userid = 1;
                    usersObj = _iuserrepository.EditUserdetailsbyuserId(userid, ConString, ClientName);
                    clientsdts.LoginId = usersObj.LoginId;
                    clientsdts.Password = EncryptDecrypt.Decrypt(usersObj.Password); 

                }





                if (clientsdts.companylogo!=null)
                {
                    ViewBag.Download = "CompanyLogoExists";
                    ViewBag.Message = clientsdts.companylogo;

                }
                return View(clientsdts);
            }
            catch (Exception)
            {
                throw;
            }
        }

       // EditClientsdata


        [HttpPost]
        public ActionResult EditClientsdata(Client objClient, HttpPostedFileBase file)
        {
            try
            {
                string delLogo = objClient.companylogo;
                 string Comlogo = System.IO.Path.GetFileName(Request.Files[0].FileName);

                if (Comlogo != "")
                {
                    string logopath = Server.MapPath("~/Content/CompanyLogos");
                    bool exists = System.IO.Directory.Exists(logopath);
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(logopath);
                    }
                    string LogoLoc = string.Format("{0}\\{1}", Server.MapPath("~/Content/CompanyLogos"), Comlogo);
                    string DelExistLogo = string.Format("{0}\\{1}", Server.MapPath("~/Content/CompanyLogos"), delLogo);
                    if (System.IO.File.Exists(DelExistLogo))
                        System.IO.File.Delete(DelExistLogo);

                    objClient.companylogo = Comlogo;
                    if (System.IO.File.Exists(LogoLoc))
                        System.IO.File.Delete(LogoLoc);
                    Request.Files[0].SaveAs(LogoLoc);
                }
                int result = _icreateclientrepository.UpdateClientDetailsByClientId(objClient, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //int result = _itemrepository.UpdateitemDetailsByitemId(objitems, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return RedirectToAction("ClientManagerIndex", "ClientManager");
               // return RedirectToAction("ClientManagerIndex");
            }
            catch (Exception)
            {
                throw;
            }



        }




        [HttpPost]
        public bool RemoveDatabase(string DelDB)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ImportCompanies"].ConnectionString;
                string conx = cs;
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
                        InitialCatalog = constrg.Replace("Initial Catalog=", "");
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
                string INTC = GlobalVariables.ClientName;
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

                // Check wether the database exisits or Not
                var dbexists = _icreateclientrepository.testDatabaseExists(dataSource, DelDB, strCon);
                bool Res = false;
                bool returnval = false;
                // Delete the clients details from the CRMClients Database -> Clients Table               
                var response = _icreateclientrepository.DeleteClientDetailsByClientid(DelDB, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (dbexists == true)
                {
                    Res = Delclient(DelDB, strCon);
                }
                if (Res == true)
                {
                    returnval = true;
                }
                return returnval;
                //return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public bool Delclient(string dbname, string conx)
        {
            #region Delete database
            try
            {

                SqlConnection con = new SqlConnection(conx);

                String sqlCommandText = @"ALTER DATABASE " + dbname + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE [" + dbname + "]";

                con.Open();
                SqlCommand SQLCMD = new SqlCommand(sqlCommandText, con);
                SQLCMD.CommandType = System.Data.CommandType.Text;
                int D = SQLCMD.ExecuteNonQuery();
                con.Close();

                if (D == -1)
                    return true;
                else
                    return false;
                // return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            #endregion


        }


        [HttpGet]
        [ActionName("IsClientExists")]
        public JsonResult IsClientExists(Client clientObj)
        {
            try
            {
                var result = _icreateclientrepository.IsClientIdExists(clientObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (clientObj.ClientNumber.Equals(0))
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    
    }
}



