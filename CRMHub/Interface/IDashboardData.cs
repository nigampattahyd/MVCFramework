using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.ViewModel;

namespace CRMHub.Interface
{
    public interface IDashboardData
    {

       // Int64 GetOpenActivityCount(DateTime fromDate, DateTime toDate, string Connectionstring, string dbName);

        //Int64 GetTotalActivityCount(int userid, int roleId, int officeId, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName);
        //Int64 GetTotalActivityCount(out int TotalCount, string Connectionstring, string dbName);
      //  Int64 GetCompletedActivityCount(DateTime fromDate, DateTime toDate, string Connectionstring, string dbName);

       // List<activityLog> GetActivitiesforDashboard(string type, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName);

     //   List<activityLog> GetActivitiesDetails(string Status, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName);

       // List<ImportantDocument> GetImportantDocs(int userid, int roleId, int officeId, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName);

       // List<tbl_history> GetData_ActivitySummery(int userid, int roleId, int officeId, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName);

        DashboardViewModel GetDashBoardData(string userid, string roleId, string officeId, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName);
        //List<activityLog> GetActivitiesforOperations(string type, string Connectionstring, string dbName);

        // void GetDashBoardActivityDetails(DateTime fromDate, DateTime toDate, string userid, string roleId, string officeId, string EmployeeId, string Connectionstring, string dbName);

        #region DashBoard new methods
       // List<CRM_Dashboard_StatusCount_Result> GetAllDashBoardStatusCount(string connectionstring, string dbName);
        List<CRM_Dashboard_ChartStatusCount_Result> GetAllDashboardChartStatusCount(string connectionstring, string dbName);
        #endregion

        DashboardViewModel GetActivityDashBoardData(DateTime fromDate, DateTime toDate, string Connectionstring, string dbName);
       // List<CRM_Dashboard_StatusCount_New_Result> GetAllDashBoardStatusCountNew(string connectionstring, string dbName);

        //List<tbl_history> GetActivitiesDetailsList(string loginid, string userid, string roleId, string empid, string officeId, string Status, DateTime fromDate, DateTime toDate, out int TotalRecords, string Connectionstring, string dbName);
        //List<CRM_Dashboard_StatusCountdetails_Result> GetAllDashBoardStatusCountDetails(string loginId, string RoldId, string connectionstring, string dbName);
       // DashboardViewModel GetActivityDashBoardDataCount(string loginid, string userid, string roleId, string empid, string officeId, string Status, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName);
       // List<tbl_history> GetActivityNotifications(string loginid, string userid, string roleId, string empid, string officeId, string Status, DateTime fromDate, DateTime toDate, out int TotalRecords, string Connectionstring, string dbName);

       // bool SetNotificationFlagforhistorytbl(string ModuleType, int Histid, string Connectionstring, string dbName);

       // List<CRM_InvoicePayment_ChartStatusCount_Result> GetAllInvoicePaymentsCountDetails(string connectionstring, string dbName);
        List<CRM_InvoicePayment_ChartStatusCount_Result> GetAllInvoicePaymentsCountDetails(string connectionstring, string dbName);
        

        List<CRM_WeeklyPayment_ChartDetails_Result> getPaymentDetailsForChart(DateTime fromDate, DateTime toDate, string Connectionstring, string dbName);
        List<CRM_Dashboard_PaymentsDetails_Result> GetPaymentCountDetails(string connectionstring, string dbName);

        //List<CRM_Dshbord_ModActivityCount_Result> GetAllModulesActivitiesCount(string Connectionstring, string dbName);

       // List<tbl_history> GetAllActivitiesNotificationsList(string Keyword, string Type, string Priority, string Status, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);

        List<Activity> GetAllNotificationslist(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);
       
        List<Activity> GetUserActivityNotification(string empid, string Status, out int TotalRecords, string Connectionstring, string dbName);

        List<CRM_Dshbord_ActivitymoduleCount_Result> GetAllModulesActivitiesCount(string Connectionstring, string dbName);

        List<CRM_Dashboard_ModulesCountdetails_Result> GetAllDashBoardStatusCountDetails(string loginId, string RoldId, string connectionstring, string dbName);
      
    }
}
