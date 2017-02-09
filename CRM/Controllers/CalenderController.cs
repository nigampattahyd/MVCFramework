using CRMHub.EntityFramework;
using CRMHub.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;
using CRMHub.Repository;
using CRMHub.ViewModel;


namespace CRM.Controllers
{
    public class CalenderController : ControllerBase
    {

        private readonly IDashboardData _iDashboardDatarepository;
        private readonly ICompanyRepository _icompanyrepository;

        public CalenderController(IDashboardData idashboardData, ICompanyRepository icompanyrepository)
        {
            _iDashboardDatarepository = idashboardData;
            _icompanyrepository = icompanyrepository;

        }
        // GET: `alender
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult _SchedularData()
        {
            ViewBag.showSchedulartab = "Schedu";
            return PartialView();
            //return View();
        }

        public JsonResult Data()
        {
            List<CalendarEvent> data = _icompanyrepository.GetActivityList(GlobalVariables.ConnectionString, GlobalVariables.ClientName).ToList();
            List<CalendarEvent> CalEventlst = new List<CalendarEvent>();
            //XDocument NewDoc = new XDocument(new XElement("Events"));
            for (int a = 0; a < data.Count; a++)
            {
                // NewDoc.Root.Add(new XElement("Event", new XAttribute("event_id", data[a].event_id),
                //new XElement("text", data[a].text),
                //new XElement("start_date", data[a].start_date),
                //new XElement("end_date", data[a].end_date)));
                CalendarEvent CalEvnt = new CalendarEvent();

                CalEvnt.id = Convert.ToInt32(data[a].id);
                //CalEvnt.id = Convert.ToInt32(data[a].id);
                CalEvnt.text = data[a].text;
                CalEvnt.start_date = Convert.ToDateTime(data[a].start_date);
                CalEvnt.end_date = Convert.ToDateTime(data[a].start_date);
                CalEvnt.Due_Date = Convert.ToDateTime(data[a].start_date);
                CalEvnt.Reminder_Date = Convert.ToDateTime(data[a].end_date);
                CalEvnt.Module = data[a].Module;
                CalEventlst.Add(CalEvnt);
            }

            return Json(CalEventlst, JsonRequestBehavior.AllowGet);
        }
    }
}