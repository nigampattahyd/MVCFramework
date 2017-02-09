using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Interface;
using System.Data.SqlClient;
using System.Data;
namespace CRMHub.Repository
{
  public  class DashBoardSetUpRepository:IDashBoardSetUpRepository
    {
      //public List<DashBoard_Modules> GetDashboardsetup(string Connectionstring, string dbName)
      //{
      //    try
      //    {
      //        DevEntities obj = new DevEntities(Connectionstring);
      //        obj.Database.Connection.Open();
      //        obj.Database.Connection.ChangeDatabase(dbName);
      //        var dashboard = (from m in obj.DashBoard_Modules
      //                         where m.ShowToUserRole != "2"
      //                         orderby m.ModuleName ascending
      //                         select m);
      //        return dashboard.ToList();   
      //    }
      //    catch (Exception)
      //    {              
      //        throw;
      //    }
      //}

      //public List<DashBoard_Modules>  ApplicantDashboardsetup()
      //{
      //    try
      //    {
      //        var obj = new DevEntities();
      //        var applicantdashboard = (from m in obj.DashBoard_Modules
      //                                  where m.ShowToUserRole == "2" && m.Id != 11 && m.ModuleId != 13
      //                                  orderby m.ModuleName ascending
      //                                  select m);
      //        return applicantdashboard.ToList();
      //    }
      //    catch (Exception)
      //    {              
      //        throw;
      //    }          
      //}

      //public int UpdateDashborad(DashBoard_Modules DSBModules)
      //{
      //    try
      //    {
      //        int dashboardresult = 0;            
      //        var obj = new DevEntities();
      //        dashboardresult = obj.CRM_UpdatedashBoard(DSBModules.ModuleId, DSBModules.ModuleName, DSBModules.ModuleHeading, DSBModules.ModuleHover, Convert.ToInt32(DSBModules.IsShow));
      //        return dashboardresult;
      //    }
      //    catch(Exception)
      //    {
      //        throw;
      //    }
      //}

      //public bool UpdateDashBoardBulk(DataTable dtdashboard, string Connectionstring, string dbName)
      //{
      //    try
      //    {
      //        var parameter = new SqlParameter("@Dashboard_Modules", SqlDbType.Structured);
      //        parameter.Value = dtdashboard;
      //        parameter.TypeName = "dbo.Dashboard_ModulesType";
      //        using (DevEntities db = new DevEntities(Connectionstring))
      //        {
      //            db.Database.Connection.Open();
      //            db.Database.Connection.ChangeDatabase(dbName);
      //            db.Database.ExecuteSqlCommand("Exec CRM_InsertDashBoardDetails @Dashboard_Modules", parameter);
      //        }
      //        return true;
      //    }
      //    catch (Exception)
      //    {
      //        throw;
      //    }          
      // }
    }
}
