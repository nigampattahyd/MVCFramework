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
    using System.Collections.Generic;
    
    public partial class LeaveApproval
    {
        public int ID { get; set; }
        public int PTOCounter { get; set; }
        public int UserID { get; set; }
        public int LeaveTypeID { get; set; }
        public System.DateTime DateOff { get; set; }
        public Nullable<double> TimeOff { get; set; }
        public Nullable<double> AllowedTimeOff { get; set; }
        public string LeaveReason { get; set; }
        public Nullable<bool> Applied { get; set; }
        public Nullable<bool> Approved { get; set; }
        public Nullable<bool> Rejected { get; set; }
        public int AssignmentId { get; set; }
        public int SupervisorId { get; set; }
        public string SupervisorComment { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LeaveRequestID { get; set; }
    }
}
