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
    
    public partial class tbl_UserLoginDetails
    {
        public decimal Id { get; set; }
        public string UserId { get; set; }
        public string Current_IP { get; set; }
        public Nullable<bool> Login_Status { get; set; }
        public Nullable<System.DateTime> Login_Time { get; set; }
        public string Session_Id { get; set; }
    }
}
