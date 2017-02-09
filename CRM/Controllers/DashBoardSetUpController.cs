using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.ViewModel;
using System.Data;
using System.Data.SqlClient;

namespace CRM.Controllers
{
    public class DashBoardSetUpController : ControllerBase
    {
        private readonly IDashBoardSetUpRepository _idashboardsetuprepository;
        public DashBoardSetUpController(IDashBoardSetUpRepository idashboardrepository)
        {
            _idashboardsetuprepository = idashboardrepository;
        }
     
        /// <summary>
        /// This Method Shows the modules present in the dashboard.
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    try
        //    {
        //        DashBoardSetUpModel DSPModel = new DashBoardSetUpModel();
        //        DSPModel.GetDashboardsetup = _idashboardsetuprepository.GetDashboardsetup(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        return View(DSPModel);
        //    }
        //    catch (Exception)
        //    {                
        //        throw;
        //    }           
        //}

        /// <summary>
        /// This method updates the headings of the dashboard  modules according to the  requirement.
        /// </summary>
        /// <param name="dsbmodules"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult Updatedashboard(DashBoard_Modules dsbmodules,FormCollection fc)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("ModuleId", typeof(string));
        //        dt.Columns.Add("ModuleHeading", typeof(string));
        //        string[] strModuleIds = fc[1].Split(',');
        //        string[] strModuleHeadings = fc[3].Split(',');
        //        int i = 0;
        //        foreach (string item in strModuleIds)
        //        {
        //            DataRow row = dt.NewRow();
        //            row["ModuleId"] = item.ToString();
        //            row["ModuleHeading"] = strModuleHeadings[i].ToString();
        //            dt.Rows.Add(row);
        //            i++;
        //        }
        //        bool result = _idashboardsetuprepository.UpdateDashBoardBulk(dt, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}