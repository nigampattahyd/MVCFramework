//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRMHub.EntityFramework
{
    using System;
    
    public partial class CRM_GetActivitiesListByCompanyID_Result
    {
        public Nullable<int> ActivityID { get; set; }
        public Nullable<int> ActivityTypeID { get; set; }
        public string ActivityName { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string StatusName { get; set; }
        public Nullable<int> PriorityID { get; set; }
        public string PriorityName { get; set; }
        public Nullable<int> AccountTypeID { get; set; }
        public string AccountTypeName { get; set; }
        public Nullable<int> ContactId { get; set; }
        public string ContactName { get; set; }
        public Nullable<int> AccountID { get; set; }
        public Nullable<int> AssignedTo { get; set; }
        public string AssignedToName { get; set; }
        public Nullable<int> OwnerID { get; set; }
        public string OwnerName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> RemindDate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<int> Createdby { get; set; }
        public string creator { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public string modifier { get; set; }
        public string DueTime { get; set; }
        public string RemindTime { get; set; }
        public Nullable<bool> NotificationFlag { get; set; }
        public string Notes { get; set; }
    }
}
