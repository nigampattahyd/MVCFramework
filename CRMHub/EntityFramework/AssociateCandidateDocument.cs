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
    
    public partial class AssociateCandidateDocument
    {
        public decimal docId { get; set; }
        public string documentName { get; set; }
        public Nullable<decimal> userId { get; set; }
        public Nullable<decimal> docAddedBy { get; set; }
        public Nullable<decimal> resumeId { get; set; }
        public string fileName { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
    }
}