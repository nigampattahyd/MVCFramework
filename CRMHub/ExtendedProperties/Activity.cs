using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{

    [MetadataType(typeof(ActivityMetaData))]
    public partial class Activity
    {
        public string Keyword { get; set; }
       
        public string ActivityName { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string ModuleName { get; set; }
        [Required]
        public string AssignedName { get; set; }
        public string AccountTypeName { get; set; }
        public string CreatedbyName { get; set; }
        public string ModifiedbyName { get; set; }


        public int daysdiff { get; set; }
        public Nullable<System.DateTime> CurrentDate { get; set; }
    }
    public partial class ActivityMetaData
    {
        public int ID { get; set; }
        public Nullable<int> ModuleID { get; set; }
        [Required]
        public Nullable<int> ActivityTypeID { get; set; }
        [Required]
        public Nullable<int> StatusID { get; set; }
        [Required]
        public Nullable<int> PriorityID { get; set; }
         [Required]
        public Nullable<int> AssignedTo { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> Createdby { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> RemindDate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public string DueTime { get; set; }
        public string RemindTime { get; set; }
        public Nullable<bool> NotificationFlag { get; set; }
        [AllowHtml]
        public string Notes { get; set; }
    }
}
