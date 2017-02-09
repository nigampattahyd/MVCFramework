using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Interface;
using CRMHub.Repository;
using CRMHub.EntityFramework;
using System.Data;
using System.Data.SqlClient;
using CRMHub.ExtendedProperties;
using CRMHub.ViewModel;

namespace CRM.Controllers
{
    public class NotificationsController : Controller
    {

        private readonly INoticationRepository _iNoticationRepository;
        private readonly IErrorLogRepository _ierrorlogrepository = null;

        public NotificationsController()
        {
            this._iNoticationRepository = new NotificationsRepository();
            _ierrorlogrepository = new ErrorLogRepository();
        }
        //To Get the All Nofications for Activities 
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                ActivityModel objactivity = new ActivityModel();
                return View(objactivity);
            }
            catch (Exception ex)
            {

                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Notifications/Index";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
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
                string Keyword_Notification = Request["Keyword_Notification"].TrimStart();
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0" && sSortDir_0=="asc")
                    
                    orderByClause = "CreatedDate desc";
                else
                {
                    if (SortFieldName == "ActivityName")
                    {
                        SortFieldName = "ActivityTypeID";
                    }

                    else if (SortFieldName == "Priority")
                    {
                        SortFieldName = "PriorityID";
                    }
                    else if (SortFieldName == "Status")
                    {
                        SortFieldName = "StatusID";
                    }
                    else if (SortFieldName == "AccountTypeName")
                    {
                        SortFieldName = "ModuleID";
                    }
                    else if (SortFieldName == "AssignedName")
                    {
                        SortFieldName = "AssignedTo";
                    }
                    

                    orderByClause = SortFieldName + " " + sSortDir_0;
                
                }
                int id = Convert.ToInt32(GlobalVariables.UserID);
                int roleid = Convert.ToInt32(GlobalVariables.RoleID);
                var lstActivitynotification = _iNoticationRepository.GetAllNotificationslist(Keyword_Notification,id,roleid,param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                

                for (int i = 0; i < lstActivitynotification.Count; i++)
                {
                    DateTime fdate = DateTime.Now;
                    //DateTime fdate = Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));

                    //DateTime Datemodel = Convert.ToDateTime(lstActivitynotification[i].DueDate);
                    //DateTime rminddate = Convert.ToDateTime(Datemodel.ToString("MM/dd/yyyy"));

                    DateTime Datemodel = Convert.ToDateTime(lstActivitynotification[i].DueDate);
                    DateTime rminddate = Datemodel;
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
            catch (Exception ex)
            {

                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Notifications/getNotifications";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }
	}
}