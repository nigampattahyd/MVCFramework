using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.Repository;
using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class SchedulerController : ControllerBase
    {
        private readonly ISchedulerRepository _iSchedulerRepository = null;
        private readonly IErrorLogRepository _ierrorlogrepository = null;
        private readonly IMentor _imentorrepository = null;
        public readonly IDBoard _iDBoard = null;
        public SchedulerController()
        {
            _iSchedulerRepository = new SchedulerRepository();
            _ierrorlogrepository = new ErrorLogRepository();
            _imentorrepository = new MentorRepository();
            _iDBoard = new DBoardRepository();
        }
        // GET: DBoard
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                string ventureid = "";
                CalenderEventsViewModel objcalviewmodel = new CalenderEventsViewModel();


                objcalviewmodel.objlstventure = new List<Venture>();
                objcalviewmodel.objlstventure = ListofVentures();
                objcalviewmodel.listMentors = new List<Mentor>();
                if (GlobalVariables.RoleID != "1")
                {
                    objcalviewmodel.listMentors = ListofMentors(ventureid);
                }
                objcalviewmodel.objcalenderevent = new CalendarEventRequest();
                objcalviewmodel.objEventdetails = new EventDetail();
                ViewData["RoleId"] = GlobalVariables.RoleID;
                return View(objcalviewmodel);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Scheduler/Index";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }


        }

        public List<Mentor> ListofMentors(string ventureid)
        {
            try
            {
                List<Mentor> objmentorslist = new List<Mentor>();

                CalenderEventsViewModel objcalviewmodel = new CalenderEventsViewModel();
                if (GlobalVariables.RoleID == "1")
                {
                    objmentorslist = _iSchedulerRepository.GetAllMentorsList(Convert.ToInt64(ventureid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                }
                else
                {
                    if (GlobalVariables.VentureId != "")
                    {
                        objmentorslist = _iSchedulerRepository.GetAllMentorsList(Convert.ToInt64(GlobalVariables.VentureId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }

                    else
                    {
                        objmentorslist = _iSchedulerRepository.GetAllMentorsList(0, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }
                }


                return objmentorslist;



            }
            catch (Exception ex)
            {

                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Scheduler/ListofMentors";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }
        public List<Venture> ListofVentures()
        {
            try
            {
                List<Venture> objventureslist = new List<Venture>();

                objventureslist = _iSchedulerRepository.GetAllVentureList(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return objventureslist;
            }
            catch (Exception ex)
            {

                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Scheduler/ListofMentors";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpPost]
        public ActionResult AddAppointments(CalendarEventRequest objcalenderevent, EventDetail objEventdetails, FormCollection fc)
        {
            try
            {
                objcalenderevent.Time1 = objcalenderevent.DT1Time1 + "|" + objcalenderevent.DT1Time2 + "|" + objcalenderevent.DT1Time3;
                objcalenderevent.Time2 = objcalenderevent.DT2Time1 + "|" + objcalenderevent.DT2Time2 + "|" + objcalenderevent.DT2Time3;
                objcalenderevent.Time3 = objcalenderevent.DT3Time1 + "|" + objcalenderevent.DT3Time2 + "|" + objcalenderevent.DT3Time3;
                objcalenderevent.CreatedBy = Convert.ToInt64(GlobalVariables.UserID);
                objcalenderevent.CreatedDate = DateTime.Now;
                objcalenderevent.MentorsList = fc["objcalenderevent.MentorsList"];
                if (objcalenderevent.Venture == null)
                {
                    objcalenderevent.Venture = Convert.ToInt32(GlobalVariables.VentureId);
                }
                Int64 calid = _iSchedulerRepository.InsertSchedulerAppointment(objcalenderevent, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                string mentorslist = fc["objcalenderevent.MentorsList"];
                string[] mentorids = mentorslist.Split(',');
                foreach (string i in mentorids)
                {
                    objEventdetails.MeetingId = calid;
                    objEventdetails.MentorId = Convert.ToInt64(i);
                    Int64 meetingsinsertion = _iSchedulerRepository.InsertSchedulerMeetingDetails(objEventdetails, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                }

            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Scheduler/AddAppointments";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
            return RedirectToAction("getUpComingEvents", "Scheduler");
        }

        [HttpGet]
        public ActionResult EditAppointment(Int64 Appointmentid, string Page)
        {
            try
            {
                CalendarEventRequest objcalevents = new CalendarEventRequest();

                objcalevents.ID = Appointmentid;
                if (GlobalVariables.MentorId != "")
                {
                    objcalevents = _iSchedulerRepository.GetAppointmentDetailsByID(Appointmentid, Convert.ToInt64(GlobalVariables.MentorId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                }
                else
                {
                    objcalevents = _iSchedulerRepository.GetAppointmentDetailsByID(Appointmentid, 0, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                }

                string[] date1times = objcalevents.Time1.Split('|');
                objcalevents.DT1Time1 = date1times[0];
                objcalevents.DT1Time2 = date1times[1];
                objcalevents.DT1Time3 = date1times[2];
                string[] date2times = objcalevents.Time2.Split('|');
                objcalevents.DT2Time1 = date2times[0];
                objcalevents.DT2Time2 = date2times[1];
                objcalevents.DT2Time3 = date2times[2];
                string[] date3times = objcalevents.Time3.Split('|');
                objcalevents.DT3Time1 = date3times[0];
                objcalevents.DT3Time2 = date3times[1];
                objcalevents.DT3Time3 = date3times[2];
                TempData["PageName"] = Page;
                return View(objcalevents);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Scheduler/EditAppointment";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }
        [HttpPost]
        [ActionName("EditAppointment")]
        public ActionResult UpdateEventDetails(CalendarEventRequest objcalevents, FormCollection fc)
        {
            try
            {
                if (fc["IsIamNotAvailable"] != "True")
                {

                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[3] { new DataColumn("AcceptedDateTime", typeof(DateTime)),
                            new DataColumn("MentorId", typeof(string)),
                            new DataColumn("MeetingId",typeof(int)) });

                    MentorAcceptedDateTime objaccepteddate = new MentorAcceptedDateTime();
                    if (objcalevents.RdAcceptedDate1 == true)
                    {
                        string[] datet1 = Convert.ToString(objcalevents.Date1).Split(' ');
                        if (objcalevents.chkDT1Time1 == true)
                        {

                            var chkdate1t1 = datet1[0] + " " + objcalevents.DT1Time1;
                            dt.Rows.Add(Convert.ToDateTime(chkdate1t1), GlobalVariables.MentorId, objcalevents.ID);
                        }
                        if (objcalevents.chkDT1Time2 == true)
                        {
                            var chkdate1t2 = datet1[0] + " " + objcalevents.DT1Time2;
                            dt.Rows.Add(Convert.ToDateTime(chkdate1t2), GlobalVariables.MentorId, objcalevents.ID);
                        }
                        if (objcalevents.chkDT1Time3 == true)
                        {
                            var chkdate1t3 = datet1[0] + " " + objcalevents.DT1Time3;
                            dt.Rows.Add(Convert.ToDateTime(chkdate1t3), GlobalVariables.MentorId, objcalevents.ID);
                        }
                    }
                    if (objcalevents.RdAcceptedDate2 == true)
                    {
                        string[] datet2 = Convert.ToString(objcalevents.Date2).Split(' ');
                        if (objcalevents.chkDT2Time1 == true)
                        {
                            var chkdate2t1 = datet2[0] + " " + objcalevents.DT2Time1;
                            dt.Rows.Add(Convert.ToDateTime(chkdate2t1), GlobalVariables.MentorId, objcalevents.ID);
                        }
                        if (objcalevents.chkDT2Time2 == true)
                        {
                            var chkdate2t2 = datet2[0] + " " + objcalevents.DT2Time2;
                            dt.Rows.Add(Convert.ToDateTime(chkdate2t2), GlobalVariables.MentorId, objcalevents.ID);
                        }
                        if (objcalevents.chkDT2Time3 == true)
                        {
                            var chkdate2t3 = datet2[0] + " " + objcalevents.DT2Time3;
                            dt.Rows.Add(Convert.ToDateTime(chkdate2t3), GlobalVariables.MentorId, objcalevents.ID);
                        }
                    }
                    if (objcalevents.RdAcceptedDate3 == true)
                    {
                        string[] datet3 = Convert.ToString(objcalevents.Date3).Split(' ');
                        if (objcalevents.chkDT3Time1 == true)
                        {
                            var chkdate3t1 = datet3[0] + " " + objcalevents.DT3Time1;
                            dt.Rows.Add(Convert.ToDateTime(chkdate3t1), GlobalVariables.MentorId, objcalevents.ID);
                        }
                        if (objcalevents.chkDT3Time2 == true)
                        {
                            var chkdate3t2 = datet3[0] + " " + objcalevents.DT3Time2;
                            dt.Rows.Add(Convert.ToDateTime(chkdate3t2), GlobalVariables.MentorId, objcalevents.ID);
                        }
                        if (objcalevents.chkDT3Time3 == true)
                        {
                            var chkdate3t3 = datet3[0] + " " + objcalevents.DT3Time3;
                            dt.Rows.Add(Convert.ToDateTime(chkdate3t3), GlobalVariables.MentorId, objcalevents.ID);
                        }
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objaccepteddate.AcceptedDateTime = Convert.ToDateTime(dt.Rows[i]["AcceptedDateTime"]);
                            objaccepteddate.MentorId = Convert.ToString(dt.Rows[i]["MentorId"]);
                            objaccepteddate.MeetingId = Convert.ToInt32(dt.Rows[i]["MeetingId"]);
                            int isupdated = _iSchedulerRepository.InsertSchedulerAcceptedDatetime(objaccepteddate, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        }
                    }
                    EventDetail objeventdetails = new EventDetail();
                    objeventdetails.IsAccepted = true;
                    objeventdetails.IsDenied = false;
                    objeventdetails.MeetingId = objcalevents.ID;
                    objeventdetails.MentorId = Convert.ToInt64(GlobalVariables.MentorId);
                    int isaccpeted = _iSchedulerRepository.updateEventDetails(objeventdetails, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                }
                if (TempData["PageName"] != null && TempData["PageName"].ToString() != "")
                {
                    string pagenames = TempData["PageName"].ToString();
                    if (pagenames == "DBoard")
                    {
                        return RedirectToAction("Index", "DBoard");
                    }
                    else
                    {
                        return RedirectToAction("getUpComingEvents", "Scheduler");
                    }
                }
                else
                {
                    return RedirectToAction("getUpComingEvents", "Scheduler");
                }
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Scheduler/UpdateEventDetails";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return RedirectToAction("EditAppointment", new { Appointmentid = Convert.ToInt64(fc["ID"]) });
            }
        }

        public ActionResult getUpComingEventsbyLoggedId(jQueryDataTableParamModel param)
        {
            try
            {
                int ismentor = 0;
                string Keyword_RecentEvents = Request["Keyword_Event"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "CreatedDate desc";

                else
                    if (SortFieldName == "strDate1")
                    {
                        SortFieldName = "Date1";
                    }
                    else if (SortFieldName == "strDate2")
                    {
                        SortFieldName = "Date2";
                    }
                    else if (SortFieldName == "strDate3")
                    {
                        SortFieldName = "Date3";
                    }
                    else if (SortFieldName == "strCreatedDate")
                    {
                        SortFieldName = "CreatedDate";
                    }

                orderByClause = SortFieldName + " " + sSortDir_0;
                if (GlobalVariables.RoleID == "4")
                {
                    ismentor = 1;
                }
                else
                {
                    ismentor = 0;
                }
                int isdashboard = 0;
                var lstrecentevents = _iSchedulerRepository.GetRecentAppointments(Keyword_RecentEvents, Convert.ToInt64(GlobalVariables.UserID), param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName, ismentor, isdashboard, GlobalVariables.RoleID);

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
                objErrorLog.ErrorPage = "Scheduler/getUpComingEventsbyLoggedId";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        public ActionResult getUpComingEvents()
        {
            try
            {
                ViewData["RoleId"] = GlobalVariables.RoleID;
                return View();
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Scheduler/getUpComingEvents";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        public ActionResult getEvents()
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

                return Json(lstrecentevents, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Scheduler/getEvents";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpGet]
        public ActionResult EditVentureAppointment(Int64 Appointmentid)
        {
            try
            {

                CalendarEventRequest objcalevents = new CalendarEventRequest();
                objcalevents.ID = Appointmentid;

                objcalevents = _iSchedulerRepository.GetAppointmentDetailsByID(Appointmentid, 0, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                string[] date1times = objcalevents.Time1.Split('|');
                objcalevents.DT1Time1 = date1times[0];
                objcalevents.DT1Time2 = date1times[1];
                objcalevents.DT1Time3 = date1times[2];
                string[] date2times = objcalevents.Time2.Split('|');
                objcalevents.DT2Time1 = date2times[0];
                objcalevents.DT2Time2 = date2times[1];
                objcalevents.DT2Time3 = date2times[2];
                string[] date3times = objcalevents.Time3.Split('|');
                objcalevents.DT3Time1 = date3times[0];
                objcalevents.DT3Time2 = date3times[1];
                objcalevents.DT3Time3 = date3times[2];
                var lstscheduledevents = _iSchedulerRepository.GetAllEventsByMeetingId(Convert.ToInt32(Appointmentid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (lstscheduledevents.Count > 0)
                {
                    foreach (var item in lstscheduledevents)
                    {
                        //string[] dateval = Convert.ToString(item.EventDate).Split(' ');
                        //var accepteddate = dateval[0];
                        //var timeval = item.EventTime;
                        //var acceptedtime = Convert.ToDateTime(accepteddate + ' ' + timeval).ToString("hh:mm:ss tt");
                        //var accpteddatetime = accepteddate + ' ' + acceptedtime;

                        objcalevents.Scheduledeventdetails = item.EventTime;
                    }
                  
                }
                return View(objcalevents);

            }
            catch (Exception ex)
            {

                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Scheduler/EditVentureAppointment";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        public ActionResult GetAllAppointmentRelatedMentors(jQueryDataTableParamModel param)
        {
            try
            {

                string Keyword_RecentEvents = Request["Keyword_Event"];
                Int64 Meetingid = Convert.ToInt64(Request["MeetingId"]);
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "Id desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                var lstrecentevents = _iSchedulerRepository.GetVentureAppointmentDetailsByID(Keyword_RecentEvents, Meetingid, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
               
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
                objErrorLog.ErrorPage = "Scheduler/GetAllAppointmentRelatedMentors";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpPost]
        public ActionResult ListofMentorsbyId(string ventureid)
        {
            try
            {
                List<Mentor> objmentorslist = new List<Mentor>();

                CalenderEventsViewModel objcalviewmodel = new CalenderEventsViewModel();

                if (GlobalVariables.RoleID == "1")
                {
                    objmentorslist = _iSchedulerRepository.GetAllMentorsList(Convert.ToInt64(ventureid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                }
                else
                {
                    if (GlobalVariables.VentureId != "")
                    {
                        objmentorslist = _iSchedulerRepository.GetAllMentorsList(Convert.ToInt64(GlobalVariables.VentureId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }

                    else
                    {
                        objmentorslist = _iSchedulerRepository.GetAllMentorsList(0, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }
                }


                return Json(new { objmentorslist }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {

                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Scheduler/ListofMentors";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        [HttpPost]
        [ActionName("EditVentureAppointment")]
        public ActionResult InsertScheduledEvent(CalendarEventRequest objcalevents)
        {
            try
            {
                Event objevent = new Event();
                string mentorslist = "";
                objevent.Title = objcalevents.Title;
                objevent.Location = objcalevents.Location;
                objevent.Description = objcalevents.Description;
                if (GlobalVariables.VentureId != null && GlobalVariables.VentureId != "")
                {
                    objevent.VentureId = Convert.ToInt64(GlobalVariables.VentureId);
                }
                else
                {
                    objevent.VentureId = Convert.ToInt64(GlobalVariables.UserID);
                }
                objevent.MeetingId =Convert.ToInt32(objcalevents.ID);
                objevent.CreatedBy = Convert.ToInt32(GlobalVariables.UserID);
                objevent.CreatedDate = DateTime.Now;
                if (objcalevents.ScheduledEventDatetime != null && objcalevents.ScheduledEventDatetime != string.Empty)
                {

                    string[] splittedatetime = objcalevents.ScheduledEventDatetime.Split(' ');
                    objevent.EventDate = DateTime.Parse(splittedatetime[0]);
                    string[] time = splittedatetime[1].Split(':');
                    objevent.EventTime = (time[0] + ":" + time[1] + splittedatetime[2]).Trim();
                    var cultureSource = new CultureInfo("en-US", false);
                    var cultureDest = new CultureInfo("de-DE", false);

                    var dt = DateTime.Parse(objevent.EventTime, cultureSource);
                    var h24Format = dt.ToString("t", cultureDest);
                    objevent.EventTime = h24Format+":00";
                
                }
                if (objcalevents.ScheduledMentors != null && objcalevents.ScheduledMentors != string.Empty)
                {
                    string[] splittedmentorlist = objcalevents.ScheduledMentors.Split(',');
                    foreach (var item in splittedmentorlist)
                    {

                        //long mentorid = _imentorrepository.getMentorIdbyMentorName(item.Trim(), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        //if (mentorid != null && mentorid != 0)
                        //{
                            mentorslist = mentorslist + "," +item;
                        //}

                    }
                    mentorslist = mentorslist.Remove(0, 1);
                    objevent.Mentors = mentorslist;
                }
                long eventid = _iSchedulerRepository.InsertScheduledAppointment(objevent, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                if (eventid != null && eventid != 0)
                {
                    return RedirectToAction("getUpComingEvents", "Scheduler");
                }
                else
                {
                    return RedirectToAction("EditVentureAppointment", "Scheduler", new { Appointmentid = objcalevents.ID});
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult ScheduledEvents()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}