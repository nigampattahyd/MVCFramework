using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class LeadModel
    {
        public LeadModel()
        {
            leadList = new List<tbl_Lead>();
            accountsList = new List<tbl_account>();
            leadactivity = new List<activity_types>();
            listCustomfields = new List<Custom_Manage_Master>();
            customVM = new Custom_Value_Master();
            lstcustomVM = new List<Custom_Value_Master>();

        }
        public List<Custom_Value_Master> lstcustomVM { get; set; }
        public List<Custom_Manage_Master> listCustomfields { get; set; }
       public tbl_Lead leadEntity { get; set; }
       public List<tbl_Lead> leadList { get; set; }
       public List<Branch> branchList { get; set; }
       public List<user> userList { get; set; }
       public Branch branch { get; set; }
       public user user { get; set; }
       public List<activity_types> industryList { get; set; }
       public activity_types activityType { get; set; }
       public List<usState> stateList { get; set; }
       public List<role> RolesList { get; set; }
       public List<tbl_crm_industry> IndustriesList { get; set; }
       public List<tbl_account> accountsList { get; set; }
       public List<tbl_Leadhistory> activityList { get; set; }
       public List<OutlookMail> mailList { get; set; }
       public List<activity_types> leadactivity { get; set; }
       public tbl_Leadhistory leadhistoryEntity { get; set; }
       public OutlookMail maildetails { get; set; }
       public Custom_Value_Master customVM { get; set; }
    }
}
