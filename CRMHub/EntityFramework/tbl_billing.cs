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
    
    public partial class tbl_billing
    {
        public int Id { get; set; }
        public Nullable<int> AccountId { get; set; }
        public string Position_Name { get; set; }
        public string billrate { get; set; }
        public string payrate { get; set; }
        public string markup { get; set; }
    }
}
