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
    
    public partial class tbl_LeaveType
    {
        public tbl_LeaveType()
        {
            this.PTOPlanLeaveTypes = new HashSet<PTOPlanLeaveType>();
        }
    
        public int Id { get; set; }
        public string LeaveCode { get; set; }
        public string LeaveDescription { get; set; }
        public bool Status { get; set; }
    
        public virtual ICollection<PTOPlanLeaveType> PTOPlanLeaveTypes { get; set; }
    }
}