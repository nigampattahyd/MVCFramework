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
    
    public partial class candidateJobRequest
    {
        public Nullable<decimal> jobId { get; set; }
        public Nullable<decimal> userId { get; set; }
        public decimal Id { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public string comment { get; set; }
        public string requestStatus { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
        public Nullable<decimal> modifiedBy { get; set; }
    }
}
