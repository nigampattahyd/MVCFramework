
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using CRMHub.ViewModel;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using CRMHub.Repository;

namespace CRM.Controllers
{
    public class DashboardHomeController : Controller
    {
        private readonly IDashboardData _iDashboardDatarepository;
        private readonly IContactsRepository _icontactrepository;
        private readonly IOfficeRepository _iofficerepository;
        private readonly IUserRepository _iuserrepository;
        private readonly IErrorLogRepository _ierrorlogrepository;
        public DashboardHomeController()
        {

            this._iDashboardDatarepository = new DashboardDataRepository();
            this._icontactrepository = new ContactRepository();
            this._iofficerepository = new OfficeRepository();
            this._iuserrepository = new UserRepository();
            this._ierrorlogrepository = new ErrorLogRepository();
        }

        // GET: DashboardHome
        public ActionResult Index()
        {
            try
            {


                string empid = GlobalVariables.UserID;
                string Status = "1";
                int totalPageCount = 0;
                List<Activity> lstActivity = _iDashboardDatarepository.GetUserActivityNotification(empid, Status, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < lstActivity.Count; i++)
                {
                    DateTime fdate = DateTime.Now.Date;//Convert.ToDateTime((DateTime.Now.Date).ToString("MM/dd/yyyy"));
                    DateTime Datemodel = Convert.ToDateTime(lstActivity[i].DueDate);
                    DateTime rminddate = Datemodel;//Convert.ToDateTime(Datemodel.ToString("MM/dd/yyyy"));
                    TimeSpan differenced = rminddate - fdate;
                    int daysd = Convert.ToInt32(differenced.TotalDays);
                    lstActivity[i].daysdiff = daysd;
                    lstActivity[i].CurrentDate = fdate;
                }

                Session["Notifications"] = lstActivity;
                return View();
            }

            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "DashboardHome/Index";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }
    }
}