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
    
    public partial class Expens
    {
        public decimal id { get; set; }
        public string expenseType { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string userId { get; set; }
        public string assignmentId { get; set; }
        public string weekDay { get; set; }
        public Nullable<decimal> amount { get; set; }
        public string status { get; set; }
        public string imageStatus { get; set; }
        public string modifiedBy { get; set; }
        public string reportingOffice { get; set; }
    }
}