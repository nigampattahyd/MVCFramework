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
    
    public partial class Elearning_ContactHistory
    {
        public long EventId { get; set; }
        public long ContactId { get; set; }
        public string Message { get; set; }
        public string EventBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
