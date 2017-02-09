using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.ViewModel;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using CRMHub.Repository;
using CRM.Helper;

namespace CRM.Controllers
{
    public class DBoardController : Controller
    {
        private readonly ISchedulerRepository _iSchedulerRepository = null;
        private readonly IErrorLogRepository _ierrorlogrepository = null;
        private readonly IDBoard _iDBoard = null;
        public DBoardController()
        {
            _iSchedulerRepository = new SchedulerRepository();
            _iDBoard = new DBoardRepository();
            _ierrorlogrepository = new ErrorLogRepository();
        }
        // GET: DBoard
        public ActionResult Index()
        {
            DBoardViewModel viewModel = new DBoardViewModel();
            dropbox drpbx = new dropbox();
            string ClientId = "6is686wp3lt27ea"; //get from dropbox app console
            string ClientSecret = "aoq337lgrbjqgk2"; //get from dropbox app console
            string AccessToken = "fLFc7qceZPIAAAAAAAAEfHZV4Az9wzc9Xyke8Hzq-HZ6sWFMfjIQ06Ziu2Q70rn9";
            viewModel.lstContent = drpbx.getDropboxContent(ClientId, ClientSecret, AccessToken);
            ViewData["RoleId"] = GlobalVariables.RoleID;
            return View(viewModel);
        }

        public ActionResult getRecentAppointments(jQueryDataTableParamModel param)
        {
            try
            {
                int ismentor = 0;
                string Keyword_RecentEvents = Request["Keyword_RecentEvents"].TrimStart();

                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "CreatedDate desc";
                else
                {
                    if (SortFieldName == "Hostedby")
                    {
                        SortFieldName = "CreatedBy";
                    }
                    orderByClause = SortFieldName + " " + sSortDir_0;
                }


                if (GlobalVariables.RoleID == "4")
                {
                    ismentor = 1;
                }
                else
                {
                    ismentor = 0;
                }
                int Isdashboard = 1;

                var lstrecentevents = _iSchedulerRepository.GetRecentAppointments(Keyword_RecentEvents, Convert.ToInt64(GlobalVariables.UserID), param.iDisplayStart, 5, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName, ismentor, Isdashboard, GlobalVariables.RoleID);
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = lstrecentevents,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "DBoard/getRecentAppointments";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }


        public ActionResult getScheduledAppointments()
        {
            try
            {
                string Role = string.Empty;
                string keyword = "EventDate";
                Int64 Id = 0;
                if (GlobalVariables.RoleID == "1")
                {
                    Role = "A";
                }
                else if (GlobalVariables.RoleID == "2")
                {
                    Role = "V";
                    Id = Convert.ToInt64(GlobalVariables.VentureId);
                }
                else if (GlobalVariables.RoleID == "4")
                {
                    Role = "M";
                    Id = Convert.ToInt64(GlobalVariables.MentorId);
                }

                var lstrecentevents = _iDBoard.GetEvents(Role, Id, keyword, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //return Json(new
                //{
                //    aaData = lstrecentevents,
                //}, JsonRequestBehavior.AllowGet);
                return new JsonResult() { Data = lstrecentevents };
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "DBoard/getScheduledAppointments";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }



    }
}