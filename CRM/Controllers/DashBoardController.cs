using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.ViewModel;
using CRMHub.Interface;
using CRMHub.Repository;
using CRMHub.EntityFramework;
using System.Data;
using System.Data.SqlClient;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    public class DashBoardController : ControllerBase
    {
        private readonly IDashboardData _iDashboardDatarepository;
        private readonly IContactsRepository _icontactrepository;
        private readonly IOfficeRepository _iofficerepository;
        private readonly IUserRepository _iuserrepository;
        public DashBoardController()
        {
            this._iDashboardDatarepository = new DashboardDataRepository();
            this._icontactrepository = new ContactRepository();
            this._iofficerepository = new OfficeRepository();
            this._iuserrepository = new UserRepository();
        }

        public ActionResult Index(DashboardViewModel dashboardModel)
        {
            try
            {
                var loginid = GlobalVariables.UserID;
                var roldid = GlobalVariables.RoleID;
                var dashboardStatusCountLists = new DashboardStatusCount(); //GetAllDashBoardStatusCountNew
                // dashboardStatusCountLists.CRMDashboardStatusCountlatestdetailsLst = _iDashboardDatarepository.GetAllDashBoardStatusCountDetails(loginid, roldid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //To Get the Nofications for Activities

                string empid = GlobalVariables.UserID;
                string Status = "1";
                int totalPageCount = 0;
                dashboardModel.LstActivityRemind = _iDashboardDatarepository.GetUserActivityNotification(empid, Status, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                // ***Ashish 22-06-16
                for (int i = 0; i < dashboardModel.LstActivityRemind.Count; i++)
                {
                    // DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));
                    DateTime fdate = DateTime.Now.Date;
                    DateTime Datemodel = Convert.ToDateTime(dashboardModel.LstActivityRemind[i].RemindDate);
                    // DateTime rminddate = Convert.ToDateTime(Datemodel.ToString("MM/dd/yyyy"));
                    DateTime rminddate = Datemodel;
                    TimeSpan differenced = rminddate - fdate;
                    int daysd = Convert.ToInt32(differenced.TotalDays);
                    dashboardModel.LstActivityRemind[i].daysdiff = daysd;
                    dashboardModel.LstActivityRemind[i].CurrentDate = fdate;
                }

                Session["Notifications"] = dashboardModel.LstActivityRemind;
                return View(dashboardStatusCountLists);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult _DashBoardData()
        {
            var loginid = GlobalVariables.UserID;
            var roldid = GlobalVariables.RoleID;
            DateTime dtSpecifiedDate = DateTime.Now.Date;
            var dashboardStatusCountLists = new DashboardStatusCount(); //GetAllDashBoardStatusCountNew
            dashboardStatusCountLists.DBTitles = new DashBoardTitle();

            DateTime FromDate = Convert.ToDateTime(dtSpecifiedDate.AddDays((7 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek))).AddDays(-7));
            DateTime ToDateE = Convert.ToDateTime(dtSpecifiedDate.AddDays(7 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek)).AddDays(-1));


            dashboardStatusCountLists.DBTitles.FromDate = FromDate;
            dashboardStatusCountLists.DBTitles.ToDate = ToDateE;


            dashboardStatusCountLists.CRMPaymentDetailslst = _iDashboardDatarepository.GetPaymentCountDetails(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
            dashboardStatusCountLists.CRMDashboardStatusCountlatestdetailsLst = _iDashboardDatarepository.GetAllDashBoardStatusCountDetails(loginid, roldid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

            return PartialView(dashboardStatusCountLists);
        }



        [HttpGet]
        public ActionResult _ActivityDashBoardMain()
        {
            try
            {
                DashboardViewModel dashboardModel = new DashboardViewModel();
                string UsrId = GlobalVariables.UserID;
                string RoleId = GlobalVariables.RoleID;
                string loginid = GlobalVariables.UserID;
                //string Status = "Open";
                //string type = "Sales";
                //string OperationType = "Operations";
                //string officeId = "All";
                //string empid = "-1";
                string empid = GlobalVariables.UserID;
                //int totalPageCount = 0;

                dashboardModel.LstActivityDshCount = _iDashboardDatarepository.GetAllModulesActivitiesCount(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                dashboardModel.OpenActivity = Convert.ToInt32(dashboardModel.LstActivityDshCount[0].OpenCount);
                dashboardModel.CompleteActivity = Convert.ToInt32(dashboardModel.LstActivityDshCount[0].CompCount);
                return PartialView(dashboardModel);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public ActionResult ActivityDashBoard()
        {
            try
            {
                DashboardViewModel dashboardModel = new DashboardViewModel();

                string UsrId = GlobalVariables.UserID;
                string RoleId = GlobalVariables.RoleID;
                string loginid = GlobalVariables.UserID;
                string Status = "Open";
                string type = "Sales";
                string OperationType = "Operations";
                string officeId = "All";
                string empid = "-1";
                int totalPageCount = 0;
                DateTime dtSpecifiedDate = DateTime.Now.Date;
                DateTime FromDate = Convert.ToDateTime(dtSpecifiedDate.AddDays((7 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek))).AddDays(-7).ToString("MM/dd/yyyy"));
                DateTime ToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(7 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek)).AddDays(-1).ToString("MM/dd/yyyy"));


                dashboardModel = _iDashboardDatarepository.GetDashBoardData(UsrId, RoleId, officeId, FromDate, ToDate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                //DashboardViewModel TempdashboardModel = new DashboardViewModel();

                //TempdashboardModel = _iDashboardDatarepository.GetActivityDashBoardDataCount(loginid, UsrId, RoleId, empid, officeId, Status, FromDate, ToDate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                //dashboardModel.TotalActivity = TempdashboardModel.TotalActivity;
                //dashboardModel.OpenActivity = TempdashboardModel.OpenActivity;
                //dashboardModel.CompleteActivity = TempdashboardModel.CompleteActivity;


                dashboardModel.listBranch = _iofficerepository.getUserBranchDetails(GlobalVariables.UserID, GlobalVariables.RoleID, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                dashboardModel.listUsers = _iuserrepository.getAdminList(GlobalVariables.ConnectionString, GlobalVariables.ClientName);


               // dashboardModel.lstHistory = _iDashboardDatarepository.GetActivitiesDetailsList(loginid, UsrId, RoleId, empid, officeId, Status, FromDate, ToDate, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);



              //  dashboardModel.listActivitylogsales = _iDashboardDatarepository.GetActivitiesforDashboard(type, FromDate, ToDate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
               // dashboardModel.listActivitylogoperations = _iDashboardDatarepository.GetActivitiesforDashboard(OperationType, FromDate, ToDate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //dashboardModel.Contact = new tbl_Contact();
               // dashboardModel.Contact.FromDate = FromDate;
               // dashboardModel.Contact.ToDate = ToDate;
                return View(dashboardModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpGet]
        //public ActionResult _ActivityDashBoard(string gFromDate, string gToDate, string officeLoc, string Usrid)
        //{
        //    DashboardViewModel dashboardModel = new DashboardViewModel();

        //    string UsrId = GlobalVariables.UserID;
        //    string RoleId = GlobalVariables.RoleID;
        //    string type = "Sales";
        //    string OperationType = "Operations";
        //    string Status = "Open";
        //    DateTime dtSpecifiedDate = DateTime.Now.Date;
        //    DateTime FromDate = Convert.ToDateTime(gFromDate);
        //    DateTime ToDate = Convert.ToDateTime(gToDate);
        //    int totalPageCount = 0;
        //    string loginid = GlobalVariables.UserID;
        //    string officeId = "All";
        //    string empid = "-1";
        //    //string empid = GlobalVariables.UserID;



        //    dashboardModel = _iDashboardDatarepository.GetDashBoardData(UsrId, RoleId, officeId, FromDate, ToDate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


        //    DashboardViewModel TempdashboardModel = new DashboardViewModel();
        //    TempdashboardModel = _iDashboardDatarepository.GetActivityDashBoardDataCount(loginid, UsrId, RoleId, empid, officeId, Status, FromDate, ToDate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //    dashboardModel.TotalActivity = TempdashboardModel.TotalActivity;
        //    dashboardModel.OpenActivity = TempdashboardModel.OpenActivity;
        //    dashboardModel.CompleteActivity = TempdashboardModel.CompleteActivity;
        //    dashboardModel.lstHistory = _iDashboardDatarepository.GetActivitiesDetailsList(loginid, UsrId, RoleId, empid, officeId, Status, FromDate, ToDate, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //    dashboardModel.listActivitylogsales = _iDashboardDatarepository.GetActivitiesforDashboard(type, FromDate, ToDate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //    dashboardModel.listActivitylogoperations = _iDashboardDatarepository.GetActivitiesforDashboard(OperationType, FromDate, ToDate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //    dashboardModel.ListActivity = _iDashboardDatarepository.GetActivitiesDetails(Status, FromDate, ToDate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //    return PartialView(dashboardModel);

        //    // return View(dashboardModel);
        //}

        //[HttpGet]
        //public ActionResult _GetOpenCloseActivities(string Status, string FromDate, string ToDate)
        //{
        //    DashboardViewModel dashboardModel = new DashboardViewModel();
        //    string UsrId = GlobalVariables.UserID;
        //    string RoleId = GlobalVariables.RoleID;
        //    string type = "Sales";
        //    string OperationType = "Operations";

        //    //int totalPageCount = 0;
        //    DateTime dtSpecifiedDate = DateTime.Now.Date;
        //    DateTime gFromDate = Convert.ToDateTime(FromDate);
        //    DateTime gToDate = Convert.ToDateTime(ToDate);
        //    int totalPageCount = 0;
        //    string loginid = GlobalVariables.UserID;
        //    string officeId = "All";

        //    string empid = "-1";


        //    dashboardModel.lstHistory = _iDashboardDatarepository.GetActivitiesDetailsList(loginid, UsrId, RoleId, empid, officeId, Status, gFromDate, gToDate, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //    return PartialView(dashboardModel);

        //}


        [HttpGet]
        public JsonResult LoadDashboardCharts()
        {
            try
            {
                List<CRM_Dashboard_ChartStatusCount_Result> result = _iDashboardDatarepository.GetAllDashboardChartStatusCount(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpGet]
        //public JsonResult LoadPaymentCharts()
        //{
        //    try
        //    {
        //       // dashboardStatusCountLists.CRMInvoicePaymentChartStatusCountlst = _iDashboardDatarepository.GetAllInvoicePaymentsCountDetails(GlobalVariables.ConnectionString, GlobalVariables.ClientName);

        //        List<CRM_InvoicePayment_ChartStatusCount_Result> Presult = _iDashboardDatarepository.GetAllInvoicePaymentsCountDetails(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //     
        //        return Json(Presult, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// To Display the Graphs in the Graphs Tab
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult _GraphsData()
        {
            try
            {
                ViewBag.showGraphtab = "Graphs";
                var dashboardStatusCountLists = new DashboardStatusCount();

                var loginid = GlobalVariables.UserID;
                var roldid = GlobalVariables.RoleID;
                DateTime dtSpecifiedDate = DateTime.Now.Date;             
                dashboardStatusCountLists.DBTitles = new DashBoardTitle();

                DateTime FromDate = Convert.ToDateTime(dtSpecifiedDate.AddDays((7 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek))).AddDays(-7));
                DateTime ToDateE = Convert.ToDateTime(dtSpecifiedDate.AddDays(7 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek)).AddDays(-1));


                dashboardStatusCountLists.DBTitles.FromDate = FromDate;
                dashboardStatusCountLists.DBTitles.ToDate = ToDateE;


                //dashboardStatusCountLists.CRMPaymentDetailslst = _iDashboardDatarepository.GetPaymentCountDetails(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //dashboardStatusCountLists.CRMDashboardStatusCountlatestdetailsLst = _iDashboardDatarepository.GetAllDashBoardStatusCountDetails(loginid, roldid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                return PartialView(dashboardStatusCountLists);

            }
            catch (Exception)
            {
                throw;
            }

        }





        [HttpGet]
        public ActionResult Scheduler()
        {
            return View();
        }



        [HttpGet]
        public JsonResult UsersByBranchId(int branchId)
        {
            try
            {
                //string result = "";
                List<user> result = _iuserrepository.getAdminListByBranchId(branchId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                result = result.Select(objList => new user
                {
                    UserId = Convert.ToInt64(objList.UserId),
                    FirstName = string.IsNullOrEmpty(objList.FirstName) ? string.Empty : objList.FirstName
                }).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //To Get the All Nofications for Activities 
        [HttpGet]
        public ActionResult GetNotificationList()
        {
            try
            {
                ActivityModel ModelActivity = new ActivityModel();
                return View(ModelActivity);

            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult getNotifications(jQueryDataTableParamModel param)
        {
            try
            {
                //string Keyword = "";
                //string Type = "Select";
                //string Priority = "select";
                //string Status = "select";
                //string Status = "Open";
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "CreatedDate desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;

                var lstActivitynotification = _iDashboardDatarepository.GetAllNotificationslist(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                for (int i = 0; i < lstActivitynotification.Count; i++)
                {
                    DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));
                    DateTime Datemodel = Convert.ToDateTime(lstActivitynotification[i].DueDate);
                    DateTime rminddate = Convert.ToDateTime(Datemodel.ToString("MM/dd/yyyy"));
                    TimeSpan differenced = rminddate - fdate;
                    int daysd = Convert.ToInt32(differenced.TotalDays);
                    lstActivitynotification[i].daysdiff = daysd;
                    lstActivitynotification[i].CurrentDate = fdate;
                }

                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = lstActivitynotification,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }






        //To Get the logged in user notifications
        //GetUserNotifications
        [HttpGet]
        public ActionResult GetUserNotifications()
        {
            try
            {

                // Here use this
                // var listActivity = _icompanyrepository.GetAllActivitiesList(Keyword == "" ? "" : Keyword, Type == "Select" ? null : Type, Priority == "select" ? null : Priority, Status == "select" ? null : Status, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                // ManageCompany Controller



                //To Get the Nofications for Activities             
                string empid = GlobalVariables.UserID;
                string Status = "1";
                int totalPageCount = 0;
                List<Activity> lstActivity = _iDashboardDatarepository.GetUserActivityNotification(empid, Status, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < lstActivity.Count; i++)
                {
                    DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));
                    DateTime Datemodel = Convert.ToDateTime(lstActivity[i].DueDate);
                    DateTime rminddate = Convert.ToDateTime(Datemodel.ToString("MM/dd/yyyy"));
                    TimeSpan differenced = rminddate - fdate;
                    int daysd = Convert.ToInt32(differenced.TotalDays);
                    lstActivity[i].daysdiff = daysd;
                    lstActivity[i].CurrentDate = fdate;
                }

                Session["Notifications"] = lstActivity;
                return View(lstActivity);
            }
            catch (Exception)
            {
                throw;
            }
        }




        //[HttpPost]
        //public ActionResult ActivateNotification(string ModuleType, string notificationIds)
        //{
        //    try
        //    {
        //        bool res = false;

        //        string actionmethod = "";
        //        string ControllerMethod = "";
        //        //if ((ModuleType == "HistoryId") || (ModuleType == "historyid"))
        //        //{
        //        res = _iDashboardDatarepository.SetNotificationFlagforhistorytbl(ModuleType, Convert.ToInt32(notificationIds), GlobalVariables.ConnectionString, GlobalVariables.ClientName);


        //        if (ModuleType == "HistoryId")
        //        {

        //            actionmethod = "EditActivityCmpgrid";
        //            ControllerMethod = "ManageCompany";

        //        }
        //        else if (ModuleType == "historyid")
        //        {
        //            actionmethod = "EditActivitygridContact";
        //            ControllerMethod = "Contact";
        //        }
        //        else if (ModuleType == "Leadhistid")
        //        {
        //            actionmethod = "EditOpportunityActivityGrid";
        //            ControllerMethod = "Opportunities";
        //        }
        //        else if (ModuleType == "leadhistoryid")
        //        {
        //            actionmethod = "EditLeadActivityGrid";
        //            ControllerMethod = "Leads";
        //        }
        //        string empid = GlobalVariables.UserID;
        //        string Status = "1";
        //        int totalPageCount = 0;
        //        List<Activity> lstActivity = _iDashboardDatarepository.GetUserActivityNotification(empid, Status, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        for (int i = 0; i < lstActivity.Count; i++)
        //        {
        //            DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));
        //            DateTime Datemodel = Convert.ToDateTime(lstActivity[i].DueDate);
        //            DateTime rminddate = Convert.ToDateTime(Datemodel.ToString("MM/dd/yyyy"));
        //            TimeSpan differenced = rminddate - fdate;
        //            int daysd = Convert.ToInt32(differenced.TotalDays);
        //            lstActivity[i].daysdiff = daysd;
        //            lstActivity[i].CurrentDate = fdate;
        //        }

        //        Session["Notifications"] = lstActivity;
        //        return RedirectToAction(actionmethod, ControllerMethod, new { HistoryId = notificationIds });

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //GetWeeklyPaymentCharts
        [HttpGet]
        public JsonResult GetWeeklyPaymentCharts()
        {
            try
            {
                DateTime dtSpecifiedDate = DateTime.Now;
                DateTime FromDate = Convert.ToDateTime(dtSpecifiedDate.AddDays((7 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek))).AddDays(-7).ToString("MM/dd/yyyy"));
                DateTime ToDateE = Convert.ToDateTime(dtSpecifiedDate.AddDays(7 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek)).AddDays(-1).ToString("MM/dd/yyyy"));
                DateTime ToDate = Convert.ToDateTime(ToDateE.AddDays(1).ToString("MM/dd/yyyy"));
                DateTime SMinites = Convert.ToDateTime(ToDate.AddMinutes(-1).ToString("MM/dd/yyyy"));

                // ***** Ashish 
                //DateTime FromDate = Convert.ToDateTime(dtSpecifiedDate.AddDays((7 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek))).AddDays(-7));
                //DateTime ToDateE = Convert.ToDateTime(dtSpecifiedDate.AddDays(7 - Convert.ToInt32(dtSpecifiedDate.DayOfWeek)).AddDays(-1));
                //DateTime ToDate = Convert.ToDateTime(ToDateE.AddDays(1));
                //DateTime SMinites = Convert.ToDateTime(ToDate.AddMinutes(-1));


                //List<CRM_WeeklyPayment_StatusChartDetails_Result> WeekPStatus = _iDashboardDatarepository.GetPaymentsforweekDetails(FromDate, ToDate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                List<CRM_WeeklyPayment_ChartDetails_Result> WeekPStatus = _iDashboardDatarepository.getPaymentDetailsForChart(FromDate, ToDate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


                return Json(WeekPStatus, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
