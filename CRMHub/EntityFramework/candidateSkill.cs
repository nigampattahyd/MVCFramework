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
    
    public partial class candidateSkill
    {
        public decimal id { get; set; }
        public Nullable<decimal> userId { get; set; }
        public string number { get; set; }
        public string skillName { get; set; }
        public string proficiencyLevel { get; set; }
        public Nullable<decimal> yearOfExperience { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
        public string skillId { get; set; }
        public string skillLevel { get; set; }
        public string skillYear { get; set; }
        public Nullable<decimal> resumeID { get; set; }
    }
}