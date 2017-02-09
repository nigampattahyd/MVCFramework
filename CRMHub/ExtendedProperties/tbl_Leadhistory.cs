using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(tbl_LeadhistoryMetaData))]
    public partial class tbl_Leadhistory
    {
        public string LeadName { get; set; }
        public string ActCreatedBy { get; set; }
        public List<activity_types> TypeList { get; set; }
        public string keyword { get; set; }
        public List<user> userList { get; set; }


    }

    public partial class tbl_LeadhistoryMetaData
    {
        public long LeadhistoryId { get; set; }
        public string HistoryType { get; set; }
        public string StartTime { get; set; }
        public string Endtime { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string EventType { get; set; }
        public Nullable<long> Leadid { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string Priority { get; set; }
        public string RemindDate { get; set; }
        public string RemindTime { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public string AssignedTo { get; set; }
        public Nullable<int> AccountId { get; set; }
        public string AccountName { get; set; }
        public string ActivityFlag { get; set; }
        public string ActivityAttachment { get; set; }
        public Nullable<bool> IsLeadActivity { get; set; }
        public Nullable<bool> IsOpportunityActivity { get; set; }
        public Nullable<bool> IsSalesActivity { get; set; }
        public Nullable<bool> NotificationFlag { get; set; }
    }
}
