using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(tbl_historyMetaData))]
    public partial class tbl_history
    {
        public string Id { get; set; }
        public string createdBy { get; set; }
        public string ApplicantName { get; set; }
        public string Keyword { get; set; }
        public string contactPhone { get; set; }
        public string contactMobile { get; set; }
        public string associatedId { get; set; }
        public string FstartDate { get; set; }
        public string FremindDate { get; set; }
        public string type { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string Attachment { get; set; }
        public string ModuleType { get; set; }
        public List<activity_types> TypeList { get; set; }
        public List<user> userlist { get; set; }
        public int daysdiff { get; set; }
        public Nullable<System.DateTime> CurrentDate { get; set; }

        public string AssignedToName { get; set; }
        public string OwnerName { get; set; }
        public int OwnerID { get; set; }
    }

    public partial class tbl_historyMetaData
    {
        public long? HistoryId { get; set; }
        public string HistoryType { get; set; }
        public string StartTime { get; set; }
        public string Endtime { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Userid { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string EventType { get; set; }
        public string Result { get; set; }
        public string Contactid { get; set; }
        public string CustomerType { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> StartDate { get; set; }
        public string Priority { get; set; }
        public Nullable<System.DateTime> RemindDate { get; set; }
        public string CommentedBy { get; set; }
        public string ResumeID { get; set; }
        public string RemindTime { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public string RequisitionId { get; set; }
        public string AssignedTo { get; set; }
        public string AssignmentId { get; set; }
        public string ToDoID { get; set; }
        public Nullable<int> AccountId { get; set; }
        public string ContractOffered { get; set; }
        public string BillRate { get; set; }
        public string PayRate { get; set; }
        public string ActivityFlag { get; set; }
        public string ExportTest { get; set; }
        public Nullable<bool> NotificationFlag { get; set; }
    }
}
