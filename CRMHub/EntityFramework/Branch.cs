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
    
    public partial class Branch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress1 { get; set; }
        public string BranchAddress2 { get; set; }
        public string BranchCity { get; set; }
        public string BranchStateCode { get; set; }
        public string BranchZip { get; set; }
        public string BranchPhone { get; set; }
        public string BranchPhoneExt { get; set; }
        public string BranchFax { get; set; }
        public string BranchEmail { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string StateOther { get; set; }
        public string RoutingEmail { get; set; }
        public string BranchType { get; set; }
    }
}
