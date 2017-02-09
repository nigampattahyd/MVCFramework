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
    
    public partial class PTOPlanLeaveType
    {
        public int ID { get; set; }
        public Nullable<int> PTOPlanID { get; set; }
        public Nullable<int> LeaveTypeID { get; set; }
        public Nullable<int> Method { get; set; }
        public Nullable<int> TimeUnits { get; set; }
        public Nullable<decimal> HoursPerDay { get; set; }
        public Nullable<int> PTOPlanFrom { get; set; }
        public Nullable<int> PTOPlanTo { get; set; }
        public Nullable<decimal> AccrualFactor { get; set; }
        public Nullable<double> MaximumCarry { get; set; }
        public Nullable<double> MaximumTime { get; set; }
        public Nullable<bool> IsAllowAccrualFromMax { get; set; }
        public Nullable<byte> AccrueOT { get; set; }
        public Nullable<byte> AccrueYearFrom { get; set; }
        public Nullable<byte> StartBenifirFrom { get; set; }
    
        public virtual PTOPlan PTOPlan { get; set; }
        public virtual tbl_LeaveType tbl_LeaveType { get; set; }
    }
}