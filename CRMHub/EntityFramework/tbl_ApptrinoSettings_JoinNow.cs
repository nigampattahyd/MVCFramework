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
    
    public partial class tbl_ApptrinoSettings_JoinNow
    {
        public int Id { get; set; }
        public bool Skill_Visible { get; set; }
        public bool SchoolInfo_Visible { get; set; }
        public bool WorkEx_Visible { get; set; }
        public bool Refrences_Visible { get; set; }
        public bool Skill_Required { get; set; }
        public bool SchoolInfo_Required { get; set; }
        public bool WorkEx_Required { get; set; }
        public bool Refrences_Required { get; set; }
        public bool ResumeUpload_Required { get; set; }
        public string JoinNowAgreement { get; set; }
        public Nullable<bool> Certificate_Visible { get; set; }
        public Nullable<bool> Certificate_Required { get; set; }
        public Nullable<bool> JoinNowAgreement_Required { get; set; }
    }
}
