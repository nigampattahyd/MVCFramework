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
    
    public partial class candidateEmploymentHistroy_Parsed
    {
        public long candidateEmpHistoryId { get; set; }
        public long resumeId { get; set; }
        public string organizationName { get; set; }
        public string positionType { get; set; }
        public string Title { get; set; }
        public string organizationAddress { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public string enddate { get; set; }
        public Nullable<bool> isCurrentEmployer { get; set; }
        public string Comments { get; set; }
        public string supervisorName { get; set; }
        public string supervisorPhone { get; set; }
        public Nullable<decimal> salary { get; set; }
        public string salaryType { get; set; }
        public string superVisorTitle { get; set; }
        public string reasonForLeaving { get; set; }
        public string workduties { get; set; }
        public string Address { get; set; }
        public string TypeOfBusiness { get; set; }
        public string StartingSalary { get; set; }
        public string StartingSalaryType { get; set; }
        public Nullable<decimal> StartingRate { get; set; }
        public Nullable<decimal> EndingRate { get; set; }
        public string city { get; set; }
        public string stateCode { get; set; }
        public string zipCode { get; set; }
        public Nullable<bool> isChargeExperience { get; set; }
        public Nullable<bool> isContactEmployer { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
    
        public virtual Resume_Parsed Resume_Parsed { get; set; }
    }
}
