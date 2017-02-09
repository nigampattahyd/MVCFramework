using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Repository;

namespace CRMHub.ViewModel
{
    public class DashboardViewModel
    {

        public DashboardViewModel()
        {
            //LstActivityDshCount = new List<CRM_Dshbord_ModActivityCount_Result>();
            LstActivityDshCount = new List<CRM_Dshbord_ActivitymoduleCount_Result>();
        }
    
        public Int64 contacts { get; set; }
        public Int64 leads { get; set; }
        public Int64 opportunity { get; set; }
        public Int64 sales { get; set; }
        public Int64 companies { get; set; }
        public Int64 OpenActivity { get; set; }
        public Int64 TotalActivity { get; set; }
        public Int64 CompleteActivity { get; set; }

        public List<Branch> listBranch { get; set; }
        public Branch branch { get; set; }
        public List<tbl_Contact> listContact { get; set; }
        public tbl_Contact Contact { get; set; }
        public user user { get; set; }
        public List<user> listUsers { get; set; }
        public tbl_Lead lead { get; set; }
        public tbl_history Activityhistory { get; set; }

        public List<tbl_history> lstHistory { get; set; }
        public List<Activity> LstActivityRemind { get; set; }



        public List<activityLog> listActivitylogsales { get; set; }
        public List<activityLog> listActivitylogoperations { get; set; }
        public List<activityLog> ListActivity { get; set; }

        public List<ImportantDocument> ListImpDocs { get; set; }


        public List<ActivityDetails> lstSalesActdetails { get; set; }
        public List<ActivityDetails> lstOperationActdetails { get; set; }

        public ActivitySummary OpenActSummary { get; set; }
        public ActivitySummary CompletedActSummary { get; set; }

       // public List<CRM_Dshbord_ModActivityCount_Result> LstActivityDshCount { get; set; }

        public List<CRM_Dshbord_ActivitymoduleCount_Result> LstActivityDshCount { get; set; }

    }
}
