using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.Repository;
using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class ExportController : ControllerBase
    {
        private readonly ICompanyRepository _icompanyrepository;
        private readonly IExportRepository _iexportrepository;
        public ExportController()
        {
            this._icompanyrepository = new CompanyRepository();
            this._iexportrepository = new ExportRepository();
        }

        //
        // GET: /Export/
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult ExportCompaniesData()
        {
            try
            {
                Branch selectOption = new Branch();
                selectOption.BranchId = -1;
                selectOption.BranchName = "--Select All--";
                CompanyModel cmpModel = new CompanyModel();
                cmpModel.branchList = new List<Branch>();
                cmpModel.branchList.Add(selectOption);
                cmpModel.branchList.AddRange(_icompanyrepository.GetBranch(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                return View(cmpModel);
            }
            catch(Exception)
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


        //[HttpPost]
        //public ActionResult ExportCompaniesData(CompanyModel cmpmodel)
        //{
        //    try
        //    {
               
        //        string CompanyBranch = cmpmodel.compnay.Account_office;
               
        //        string ConnectionString = BuildConnectionString();

        //        DataSet dsCompanyList = _iexportrepository.GetExportDocuments(CompanyBranch == "-1" ? "All" : CompanyBranch, GlobalVariables.RoleID, GlobalVariables.UserID,ConnectionString);

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.AddHeader("content-disposition",
        //            "attachment;filename=ExportCompanies.csv");
        //        Response.Charset = "";
        //        Response.ContentType = "application/text";


        //        StringBuilder sb = new StringBuilder();
        //        DataTable dt = dsCompanyList.Tables[0];
        //        for (int k = 0; k < dt.Columns.Count; k++)
        //        {
        //            //add separator
        //            sb.Append(dt.Columns[k].ColumnName + ',');
        //        }
        //        //append new line
        //        sb.Append("\r\n");
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            for (int k = 0; k < dt.Columns.Count; k++)
        //            {
        //                //add separator
        //                sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
        //            }
        //            //append new line
        //            sb.Append("\r\n");
        //        }
        //        Response.Output.Write(sb.ToString());
        //        Response.Flush();
        //        Response.End();

        //        return View(cmpmodel);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [HttpGet]
        public ActionResult DownloadExportCompaniesData()
        {
            try
            {
                string file = "ExportCompaniesData.csv";
                string fullPath = Path.Combine(Server.MapPath("~/Content/ExportCompaniesData/"), file);
                return File(fullPath, "application/vnd.ms-excel", file);
            }
            catch (Exception)
            {
                throw;
            }
        }
	}
}