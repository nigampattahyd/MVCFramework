using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using CRMHub.ViewModel;
using System.Globalization;

namespace CRMHub.Repository
{
    public class DashboardDataRepository : IDashboardData
    {

        //public Int64 GetOpenActivityCount(DateTime fromDate, DateTime toDate, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);


        //        //USP_GET_DASHBOARD_ACTIVTY_Open
        //        int ActivityCountResult = obj.tbl_history.Where(tblh => tblh.Status == "Open" && tblh.ModifiedDate >= fromDate && tblh.ModifiedDate <= toDate).ToList().Count();

        //        return ActivityCountResult;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}


        //public Int64 GetCompletedActivityCount(DateTime fromDate, DateTime toDate, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        //var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));
        //        int ActivityCountResult = obj.tbl_history.Where(tblh => tblh.Status == "Completed" && tblh.ModifiedDate >= fromDate && tblh.ModifiedDate <= toDate).ToList().Count();
        //        //TotalCount = Convert.ToInt32(output.Value);
        //        //return Convert.ToInt32(lstActivityCountResult);
        //        return ActivityCountResult;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //public Int64 GetTotalActivityCount(int userid, int roleId, int officeId, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        //var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));
        //        //int ActivityCountResult = obj.tbl_history.Where(tblh => tblh.Status == "Completed" && tblh.ModifiedDate >= fromDate && tblh.ModifiedDate <= toDate).ToList().Count();
        //        int ActivityCountResult = obj.tbl_history.Where(tblh => tblh.ModifiedDate >= fromDate && tblh.ModifiedDate <= toDate).ToList().Count();
        //        // int ActivityCountResult = obj.tbl_history.ToList().Count();
        //        //TotalCount = Convert.ToInt32(output.Value);
        //        //return Convert.ToInt32(lstActivityCountResult);
        //        return ActivityCountResult;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //public Int64 GetTotalActivityCount(out int TotalCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));
        //        int ActivityCountResult = obj.tbl_history.ToList().Count();
        //        TotalCount = Convert.ToInt32(output.Value);
        //        //return Convert.ToInt32(lstActivityCountResult);
        //        return ActivityCountResult;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}


        //public List<activityLog> GetActivitiesforDashboard(string type, DateTime FromDate, DateTime ToDate, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();//CRM_GET_DASHBOARD_ACTIVTY_DETAILS
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        List<CRM_GET_DASHBOARD_ACTIVTY_DETAILS_Result> lstActiveSalesDashboard = obj.CRM_GET_DASHBOARD_ACTIVTY_DETAILS(type, FromDate, ToDate).ToList();
        //        List<activityLog> ListActivityDashBSales = lstActiveSalesDashboard.Select(activityLog => new activityLog
        //            {

        //                //HolidayMasterId = (rt == null ? 0 : rt.HolidayMasterId),
        //                ActiveCount = (activityLog == null ? 0 : activityLog.ActiveCount),
        //                ActivityType = activityLog.activitytype


        //            }).ToList();
        //        return ListActivityDashBSales;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public List<activityLog> GetActivitiesDetails(string Status, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName)
        {
            try
            {
                var obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                //List<activityLog> ActivityList = obj.activityLogs.Where(tblh => tblh.ModifiedDate >= fromDate && tblh.ModifiedDate <= fromDate && tblh.Status == "Open").ToList();

                List<activityLog> ActivityList = obj.activityLogs.Where(tblh => tblh.Status == Status && tblh.ModifiedDate >= fromDate && tblh.ModifiedDate <= toDate).ToList();

                //return Convert.ToInt32(lstActivityCountResult);
                return ActivityList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public List<ImportantDocument> GetImportantDocs(int userid, int roleId, int officeId, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);

        //        List<Usp_DashBoard_ImportantDocument_Result> lstimpDoc = obj.Usp_DashBoard_ImportantDocument(userid, roleId, officeId, fromDate, toDate, 1).ToList();
        //        List<ImportantDocument> lstImpDocs = lstimpDoc.Select(ImportantDocument => new ImportantDocument
        //        {
        //            DocId = ImportantDocument.DocId,
        //            DocName = ImportantDocument.DocName,
        //            DocDescription = ImportantDocument.DocDescription


        //        }).ToList();
        //        return lstImpDocs;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //public List<tbl_history> GetData_ActivitySummery(int userid, int roleId, int officeId, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);

        //        //List<tbl_history> lstHistory = obj.tbl_history.Where(tblh => tblh.Status == "Completed" && tblh.ModifiedDate >= fromDate && tblh.ModifiedDate <= toDate).ToList().Count();

        //        List<Usp_DashBoard_GetData_ActivitySummery_Result> lstSummaryres = obj.Usp_DashBoard_GetData_ActivitySummery(userid, roleId, officeId, fromDate, toDate, 1).ToList();
        //        List<tbl_history> lstHistory = lstSummaryres.Select(tbl_history => new tbl_history
        //        {
        //            HistoryType = tbl_history.HistoryType,
        //            // HistoryId = tbl_history.

        //        }).ToList();



        //        //}).ToList();
        //        return lstHistory;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}


        public DashboardViewModel GetDashBoardData(string userid, string roleId, string officeId, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName)
        {
            try
            {
                var obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                CRM_DashBoard_GetData_ActivityDashBoard_Result DashBoardRes = obj.CRM_DashBoard_GetData_ActivityDashBoard(userid, roleId, officeId, fromDate, toDate, 1).SingleOrDefault();
                // Usp_DashBoard_GetDashBoardDataAll_Result DashBoardRes = obj.Usp_DashBoard_GetDashBoardDataAll(userid, roleId, officeId, fromDate, toDate, 1).SingleOrDefault();
                DashboardViewModel DashBoardResObj = new DashboardViewModel();
                DashBoardResObj.companies = Convert.ToInt32(DashBoardRes.CompanyCount);
                DashBoardResObj.contacts = Convert.ToInt32(DashBoardRes.ContactCount);
                DashBoardResObj.leads = Convert.ToInt32(DashBoardRes.LeadCount);
                DashBoardResObj.opportunity = Convert.ToInt32(DashBoardRes.OpportunityCount);
                DashBoardResObj.sales = Convert.ToInt32(DashBoardRes.SalesCount);


                return DashBoardResObj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public void GetDashBoardActivityDetails(DateTime fromDate, DateTime toDate, string userid, string roleId, string officeId, string EmployeeId, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //       // USP_GET_ACTIVTY_TYPE_Result
        //    //var obj1  = obj.USP_GET_DASHBOARD_ACTIVTY_TYPE(fromDate, toDate, userid, roleId, officeId, EmployeeId);



        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        #region DashBoard New Methods
        //public List<CRM_Dashboard_StatusCount_Result> GetAllDashBoardStatusCount(string connectionstring, string dbName)
        //{
        //    var obj = new DevEntities(connectionstring);
        //    obj.Database.Connection.Open();
        //    obj.Database.Connection.ChangeDatabase(dbName);
        //    //List<CRM_Dashboard_StatusCount_New_Result> dashBoardStatusCountLists = obj.CRM_Dashboard_StatusCount_New().ToList();
        //    List<CRM_Dashboard_StatusCount_Result> dashBoardStatusCountLists = obj.CRM_Dashboard_StatusCount().ToList();
        //    return dashBoardStatusCountLists;
        //}



        //public List<CRM_Dashboard_StatusCount_New_Result> GetAllDashBoardStatusCountNew(string connectionstring, string dbName)
        //{
        //    var obj = new DevEntities(connectionstring);
        //    obj.Database.Connection.Open();
        //    obj.Database.Connection.ChangeDatabase(dbName);
        //    List<CRM_Dashboard_StatusCount_New_Result> dashBoardStatusCountLists = obj.CRM_Dashboard_StatusCount_New().ToList();
        //    //List<CRM_Dashboard_StatusCount_Result> dashBoardStatusCountLists = obj.CRM_Dashboard_StatusCount().ToList();
        //    return dashBoardStatusCountLists;
        //}


        public List<CRM_Dashboard_ChartStatusCount_Result> GetAllDashboardChartStatusCount(string connectionstring, string dbName)
        {
            var obj = new DevEntities(connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);
            List<CRM_Dashboard_ChartStatusCount_Result> dashBoardChartStatusCountLists = obj.CRM_Dashboard_ChartStatusCount().ToList();
            return dashBoardChartStatusCountLists; ;
        }
        #endregion

        public DashboardViewModel GetActivityDashBoardData(DateTime fromDate, DateTime toDate, string Connectionstring, string dbName)
        {
            try
            {
                var obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                CRM_AcivityDashboard_StatusCount_Result ActivityDashBoardRes = obj.CRM_AcivityDashboard_StatusCount(fromDate, toDate).SingleOrDefault();
                DashboardViewModel DashBoardResObj = new DashboardViewModel();
                DashBoardResObj.TotalActivity = Convert.ToInt32(ActivityDashBoardRes.TotalAcivity);
                DashBoardResObj.OpenActivity = Convert.ToInt32(ActivityDashBoardRes.OpenAcivity);
                DashBoardResObj.CompleteActivity = Convert.ToInt32(ActivityDashBoardRes.CompleteAcivity);

                return DashBoardResObj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public DashboardViewModel GetActivityDashBoardDataCount(string loginid, string userid, string roleId, string empid, string officeId, string Status, DateTime fromDate, DateTime toDate, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        DashboardViewModel DashBoardResObj = new DashboardViewModel();
        //        DashBoardResObj.TotalActivity = 0;
        //        DashBoardResObj.OpenActivity = 0;
        //        DashBoardResObj.CompleteActivity = 0;

        //        List<CRM_ActivityDashboard_TotalActivityCount_Result> LstActivityCount = obj.CRM_ActivityDashboard_TotalActivityCount(loginid, roleId, fromDate.ToString(), toDate.ToString(), officeId, empid, "", 0, "HISTORYID DESC", "").ToList();

        //        for (int i = 0; i < LstActivityCount.Count; i++)
        //        {
        //            if (LstActivityCount[i].ActivityStatus == "Open")
        //            {
        //                DashBoardResObj.OpenActivity = Convert.ToInt32(LstActivityCount[i].StatusCount);
        //            }
        //            else if (LstActivityCount[i].ActivityStatus == "Completed")
        //            {
        //                DashBoardResObj.CompleteActivity = Convert.ToInt32(LstActivityCount[i].StatusCount);
        //            }

        //        }
        //        DashBoardResObj.TotalActivity = DashBoardResObj.OpenActivity + DashBoardResObj.CompleteActivity;
        //        return DashBoardResObj;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        //public List<activityLog> GetActivitiesDetailsList(long loginid, long userid, long roleId, long empid, long officeId, string Status, DateTime fromDate, DateTime toDate, out int TotalRecords, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));
        //        // string usrid = "1";
        //        //List<CRM_GET_DASHBOARD_ACTIVTESList_Result> DshActivityList = obj.CRM_GET_DASHBOARD_ACTIVTESList(Convert.ToDouble(loginid),Convert.ToDouble(roleId),fromDate,toDate,Convert.ToDouble(officeId),Convert.ToDouble(empid),"null",0,10,"HISTORYID DESC","open",0).ToList();
        //        //List<CRM_GET_DASHBOARD_ACTIVTESList_Result> DshActivityList = obj.CRM_GET_DASHBOARD_ACTIVTESList("852","1","1/11/2014","1/1/2015","-1","-1","","0","25","HISTORYID DESC","Completed",0).ToList();
        //        List<CRM_GET_DASHBOARD_ACTIVTESList_Result> DshActivityList = obj.CRM_GET_DASHBOARD_ACTIVTESList(loginid, roleId, fromDate, toDate, officeId, empid, "", 0, 25, "HISTORYID DESC", Status, output).ToList();
        //        TotalRecords = Convert.ToInt32(output.Value);
        //        List<activityLog> ActivityList = obj.activityLogs.Where(tblh => tblh.Status == Status && tblh.ModifiedDate >= fromDate && tblh.ModifiedDate <= toDate).ToList();
        //        return ActivityList;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //public List<CRM_Dashboard_StatusCountdetails_Result> GetAllDashBoardStatusCountDetails(string loginId, string RoldId, string connectionstring, string dbName)
        //{
        //    var obj = new DevEntities(connectionstring);
        //    obj.Database.Connection.Open();
        //    obj.Database.Connection.ChangeDatabase(dbName);
        //    List<CRM_Dashboard_StatusCountdetails_Result> dashBoardStatusCountListsRes = obj.CRM_Dashboard_StatusCountdetails(loginId, RoldId).ToList();
        //    return dashBoardStatusCountListsRes;
        //}



        // To get the invoice details on DashBoard
        public List<CRM_InvoicePayment_ChartStatusCount_Result> GetAllInvoicePaymentsCountDetails(string connectionstring, string dbName)
        {
            var obj = new DevEntities(connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);

            List<CRM_InvoicePayment_ChartStatusCount_Result> InvoicepaymentDetails = obj.CRM_InvoicePayment_ChartStatusCount().ToList();
            return InvoicepaymentDetails;
        }




        //public List<tbl_history> GetActivitiesDetailsList(string loginid, string userid, string roleId, string empid, string officeId, string Status, DateTime fromDate, DateTime toDate, out int TotalRecords, string Connectionstring, string dbName)
        //{
        //    //GetActivitiesDetailsList(string loginid, string userid, string roleId, string empid, string officeId, string Status, DateTime fromDate, DateTime toDate, out int TotalRecords, string Connectionstring, string dbName)
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));

        //        List<CRM_ActivityDashboard_TotalActivityDetails_Result> ActivityDetailsLst = obj.CRM_ActivityDashboard_TotalActivityDetails(loginid, roleId, fromDate.ToString(), toDate.ToString(), officeId, empid, "", 0, 1000, "HISTORYID DESC", Status).ToList();

        //        TotalRecords = Convert.ToInt32(output.Value);
        //        List<tbl_history> LstHistory = ActivityDetailsLst.Select(TH => new tbl_history
        //        {

        //            // DefaultValue = (rt == null ? "0" : rt.DefaultValue),
        //            HistoryId = TH.HistoryId.GetValueOrDefault(),
        //            //  HistoryId = TH.historyId,
        //            HistoryType = TH.HistoryType,
        //            Status = TH.Status,
        //            CompanyName = TH.Account_Name,
        //            AccountId = TH.AccountId,
        //            //ApplicantName = TH.AssignedToName,
        //            //Userid = Convert.ToString(TH.AssignedtoUserId),
        //            ModuleType = TH.Type



        //        }).ToList();

        //        return LstHistory;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
        //// To Get the Notifications for the Activities Created

        //public List<tbl_history> GetActivityNotifications(string loginid, string userid, string roleId, string empid, string officeId, string Status, DateTime fromDate, DateTime toDate, out int TotalRecords, string Connectionstring, string dbName)
        //{
        //    //GetActivitiesDetailsList(string loginid, string userid, string roleId, string empid, string officeId, string Status, DateTime fromDate, DateTime toDate, out int TotalRecords, string Connectionstring, string dbName)
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));
        //        List<CRM_Get_Activity_Notifications_Reminder_Result> Activitynotification = obj.CRM_Get_Activity_Notifications_Reminder(loginid, roleId, officeId, empid, 0, 1000, "HISTORYID DESC", Status).ToList();

        //        //  List<CRM_ActivityDashboard_TotalActivityDetails_Result> ActivityDetailsLst = obj.CRM_ActivityDashboard_TotalActivityDetails(loginid, roleId, fromDate.ToString(), toDate.ToString(), officeId, empid, "", 0, 1000, "HISTORYID DESC", Status).ToList();

        //        TotalRecords = Convert.ToInt32(output.Value);
        //        List<tbl_history> LstHistory = Activitynotification.Select(TH => new tbl_history
        //        {
        //            HistoryId = TH.HistoryId.GetValueOrDefault(),
        //            HistoryType = TH.HistoryType,
        //            Status = TH.Status,
        //            CompanyName = TH.Account_Name,
        //            AccountId = TH.AccountId,
        //            StartDate = TH.DueDate,
        //            RemindDate = TH.remindDate,
        //            ModuleType = TH.Type
        //        }).ToList();

        //        return LstHistory;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //public bool SetNotificationFlagforhistorytbl(string ModuleType, int HistoryId, string Connectionstring, string dbName)
        //{
        //    bool Result = false;
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);

        //        if ((ModuleType == "HistoryId") || (ModuleType == "historyid"))
        //        {

        //            var Inactnotification = (from InActList in obj.tbl_history
        //                                     where InActList.HistoryId == HistoryId
        //                                     select InActList).FirstOrDefault();

        //            if (Inactnotification != null)
        //            {
        //                Inactnotification.NotificationFlag = true;
        //            }
        //            obj.SaveChanges();
        //            Result = true;
        //        }
        //        else if ((ModuleType == "Leadhistid") || (ModuleType == "leadhistoryid"))
        //        {
        //            var Inactnotification = (from InActList in obj.tbl_Leadhistory
        //                                     where InActList.LeadhistoryId == HistoryId
        //                                     select InActList).FirstOrDefault();

        //            if (Inactnotification != null)
        //            {
        //                Inactnotification.NotificationFlag = true;
        //            }
        //            obj.SaveChanges();
        //            Result = true;
        //        }
        //        return Result;
        //    }
        //    catch (Exception)
        //    {
        //        return Result;
        //    }
        //}


        public List<CRM_WeeklyPayment_ChartDetails_Result> getPaymentDetailsForChart(DateTime fromDate, DateTime toDate, string Connectionstring, string dbName)
        {
            var obj = new DevEntities(Connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);
            string st = fromDate.ToShortDateString();
            string strTodate = toDate.ToShortDateString();
            //**Ashish 22 june 16
            //DateTime dtfromdate = DateTime.ParseExact(st, "dd-MM-yyyy",
            //                      CultureInfo.InvariantCulture);
            //string strfromDate = dtfromdate.ToString("yyyy-MM-dd");
            //DateTime dttodate = DateTime.ParseExact(strTodate, "dd-MM-yyyy",
            //                     CultureInfo.InvariantCulture);
            //string strtoDate = dttodate.ToString("yyyy-MM-dd");
            List<CRM_WeeklyPayment_ChartDetails_Result> ChartPayDetailsWeekly = obj.CRM_WeeklyPayment_ChartDetails(fromDate.ToString(), toDate.ToString()).ToList();
            // List<CRM_WeeklyPayment_ChartDetails_Result> ChartPayDetailsWeekly = obj.CRM_WeeklyPayment_ChartDetails(strfromDate, strtoDate).ToList();

            // List<CRM_WeeklyPayment_ChartDetails_Result> ChartPayDetailsWeekly = obj.CRM_WeeklyPayment_ChartDetails(fromDate.ToString(), toDate.ToString()).ToList();
            return ChartPayDetailsWeekly;
        }

        public List<CRM_Dashboard_PaymentsDetails_Result> GetPaymentCountDetails(string connectionstring, string dbName)
        {
            var obj = new DevEntities(connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);
            List<CRM_Dashboard_PaymentsDetails_Result> PaymentDetailsCount = obj.CRM_Dashboard_PaymentsDetails().ToList();
            return PaymentDetailsCount;
        }

        //public List<CRM_Dshbord_ModActivityCount_Result> GetAllModulesActivitiesCount(string Connectionstring, string dbName)
        //{
        //    var obj = new DevEntities(Connectionstring);
        //    obj.Database.Connection.Open();
        //    obj.Database.Connection.ChangeDatabase(dbName);
        //    List<CRM_Dshbord_ModActivityCount_Result> ActivityModCount = obj.CRM_Dshbord_ModActivityCount().ToList();
        //    return ActivityModCount;
        //}

        // To get all the Notifications for SuperAdmin (All users notifications)

        //public List<tbl_history> GetAllActivitiesNotificationsList(string Keyword, string Type, string Priority, string Status, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

        //        List<CRM_GETALL_Activities_Notification_Result> LstNotifications = obj.CRM_GETALL_Activities_Notification(Keyword, Type, Priority, Status, startIndex, pageSize, orderByClause, output).ToList();

        //        //List<CRM_GETALL_Activities_Result> LstActivities = obj.CRM_GETALL_Activities(Keyword, Type, Priority, Status, startIndex, pageSize, orderByClause, output).ToList();
        //        TotalCount = Convert.ToInt32(output.Value);

        //        List<tbl_history> LstHistory = LstNotifications.Select(Hist => new tbl_history
        //        {
        //            HistoryId = Hist.HistoryId ?? 0,
        //            HistoryType = Hist.HistoryType,
        //            CreatedDate = Hist.CreatedDate,
        //            StartDate = Hist.StartDate,
        //            RemindDate = Hist.remindDate,
        //            ModuleType = Hist.ModuleType,
        //            Priority = Hist.Priority,
        //            Status = Hist.Status,
        //            ContactName = Hist.AssignedToName



        //        }).ToList();

        //        return LstHistory;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        // Zoho Based Notification 
        /// <summary>
        ///  To get all the Notification 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByClause"></param>
        /// <param name="TotalCount"></param>
        /// <param name="Connectionstring"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public List<Activity> GetAllNotificationslist(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                List<CRM_GETALLNotification_Result> LstNotifications = obj.CRM_GETALLNotification(startIndex, pageSize, orderByClause, output).ToList();

                List<Activity> ActivityNotification = LstNotifications.Select(Acti => new Activity
                    {
                        ID = Acti.ActivityID ?? 0,
                        ActivityName = Acti.ActivityName,
                        CreatedDate = Acti.CreatedDate,
                        DueDate = Acti.DueDate,
                        RemindDate = Acti.RemindDate,
                        AccountTypeName = Acti.AccountTypeName,
                        Priority = Acti.PriorityName,
                        Status = Acti.StatusName,
                        AssignedName = Acti.AssignedFirstName + ' ' + Acti.AssignedLastName

                    }).ToList();
                TotalCount = Convert.ToInt32(ActivityNotification.Count);
                return ActivityNotification;

            }
            catch (Exception)
            {
                throw;
            }
        }

        // To Get the Notifications for the Activities Created
        /// <summary>
        /// To get the notification based on login user.
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="userid"></param>
        /// <param name="roleId"></param>
        /// <param name="empid"></param>
        /// <param name="officeId"></param>
        /// <param name="Status"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="TotalRecords"></param>
        /// <param name="Connectionstring"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>

        public List<Activity> GetUserActivityNotification(string empid, string Status, out int TotalRecords, string Connectionstring, string dbName)
        {
            try
            {
                var obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));
                List<Activity> LstActivity1 = new List<Activity>();

                List<CRM_GETEMPLOYEENotification_Result> EmployeeActivitylist = obj.CRM_GETEMPLOYEENotification(empid, Status, "ActivityID DESC").ToList();
                TotalRecords = Convert.ToInt32(output.Value);

                List<Activity> LstActivity = EmployeeActivitylist.Select(AC => new Activity
                    {
                        ID = AC.ActivityID.GetValueOrDefault(),
                        ActivityName = AC.ActivityName,
                        Status = AC.StatusName,
                        AccountTypeName = AC.AccountTypeName,
                        DueDate = AC.DueDate,
                        RemindDate = AC.RemindDate
                    }).ToList();

                return LstActivity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public List<tbl_history> GetActivityNotifications(string loginid, string userid, string roleId, string empid, string officeId, string Status, DateTime fromDate, DateTime toDate, out int TotalRecords, string Connectionstring, string dbName)
        //{
        //    //GetActivitiesDetailsList(string loginid, string userid, string roleId, string empid, string officeId, string Status, DateTime fromDate, DateTime toDate, out int TotalRecords, string Connectionstring, string dbName)
        //    try
        //    {
        //        var obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));
        //        List<CRM_Get_Activity_Notifications_Reminder_Result> Activitynotification = obj.CRM_Get_Activity_Notifications_Reminder(loginid, roleId, officeId, empid, 0, 1000, "HISTORYID DESC", Status).ToList();

        //        //  List<CRM_ActivityDashboard_TotalActivityDetails_Result> ActivityDetailsLst = obj.CRM_ActivityDashboard_TotalActivityDetails(loginid, roleId, fromDate.ToString(), toDate.ToString(), officeId, empid, "", 0, 1000, "HISTORYID DESC", Status).ToList();

        //        TotalRecords = Convert.ToInt32(output.Value);
        //        List<tbl_history> LstHistory = Activitynotification.Select(TH => new tbl_history
        //        {
        //            HistoryId = TH.HistoryId.GetValueOrDefault(),
        //            HistoryType = TH.HistoryType,
        //            Status = TH.Status,
        //            CompanyName = TH.Account_Name,
        //            AccountId = TH.AccountId,
        //            StartDate = TH.DueDate,
        //            RemindDate = TH.remindDate,
        //            ModuleType = TH.Type
        //        }).ToList();

        //        return LstHistory;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        /// To get Activity Count for each module.

        public List<CRM_Dshbord_ActivitymoduleCount_Result> GetAllModulesActivitiesCount(string Connectionstring, string dbName)
        {
            var obj = new DevEntities(Connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);
            List<CRM_Dshbord_ActivitymoduleCount_Result> ActivityModCount = obj.CRM_Dshbord_ActivitymoduleCount().ToList();
            return ActivityModCount;

        }

        public List<CRM_Dashboard_ModulesCountdetails_Result> GetAllDashBoardStatusCountDetails(string loginId, string RoldId, string connectionstring, string dbName)
        {
            var obj = new DevEntities(connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);
            List<CRM_Dashboard_ModulesCountdetails_Result> dashBoardStatusCountListsRes = obj.CRM_Dashboard_ModulesCountdetails(loginId, RoldId).ToList();
            return dashBoardStatusCountListsRes;

        }

        //public List<CRM_Dashboard_StatusCountdetails_Result> GetAllDashBoardStatusCountDetails(string loginId, string RoldId, string connectionstring, string dbName)
        //{
        //    var obj = new DevEntities(connectionstring);
        //    obj.Database.Connection.Open();
        //    obj.Database.Connection.ChangeDatabase(dbName);
        //    List<CRM_Dashboard_StatusCountdetails_Result> dashBoardStatusCountListsRes = obj.CRM_Dashboard_StatusCountdetails(loginId, RoldId).ToList();
        //    return dashBoardStatusCountListsRes;
        //}

    }

}
