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
    
    public partial class candidateWorkExp
    {
        public decimal id { get; set; }
        public Nullable<decimal> userId { get; set; }
        public Nullable<decimal> number { get; set; }
        public string jobTitle { get; set; }
        public Nullable<decimal> lengthOfJob { get; set; }
        public Nullable<decimal> phone { get; set; }
        public Nullable<decimal> payRate { get; set; }
        public string reasonForLeaving { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
        public string company { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string salary { get; set; }
        public string companyName { get; set; }
        public string superVisorName { get; set; }
        public string superVisorTitle { get; set; }
        public string superVisorPhone { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string stateCode { get; set; }
        public string zipCode { get; set; }
        public string salaryType { get; set; }
        public Nullable<decimal> resumeID { get; set; }
        public string workduties { get; set; }
        public string Address { get; set; }
        public string TypeOfBusiness { get; set; }
        public string StartingSalary { get; set; }
        public string StartingSalaryType { get; set; }
        public Nullable<decimal> StartingRate { get; set; }
        public Nullable<decimal> EndingRate { get; set; }
        public Nullable<bool> isChargeExperience { get; set; }
        public Nullable<bool> isContactEmployer { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}