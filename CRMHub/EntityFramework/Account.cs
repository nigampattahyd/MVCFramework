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
    
    public partial class Account
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string EmailOptOut { get; set; }
        public Nullable<int> AccountTypeId { get; set; }
        public string Website { get; set; }
        public string Ownership { get; set; }
        public string TotalEmployees { get; set; }
        public Nullable<int> IndustryID { get; set; }
        public string AnnualRevenue { get; set; }
        public string Rating { get; set; }
        public string LeadStatus { get; set; }
        public string LeadSource { get; set; }
        public string MailingAddress { get; set; }
        public string MailingAddress2 { get; set; }
        public string Mailingcity { get; set; }
        public Nullable<int> MailingstateID { get; set; }
        public string Mailingzip { get; set; }
        public Nullable<int> MailingcountryID { get; set; }
        public string BillingAddress { get; set; }
        public string BillingAddress2 { get; set; }
        public string Billingcity { get; set; }
        public Nullable<int> BillingstateID { get; set; }
        public string Billingzip { get; set; }
        public string BillingcountryID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string Description { get; set; }
        public string SeachIndex { get; set; }
        public string FacebookUsername { get; set; }
        public string TwitterUsername { get; set; }
        public string LinkedInUsername { get; set; }
        public string SkypeUsername { get; set; }
        public Nullable<int> ContactTypeID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> OwnerID { get; set; }
        public Nullable<decimal> EstimateBudget { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public Nullable<int> StageID { get; set; }
        public Nullable<int> BusinessTypeID { get; set; }
        public string Probability { get; set; }
        public string NextStep { get; set; }
        public Nullable<decimal> ExpectedRevenue { get; set; }
        public Nullable<int> OppurtunitySourceID { get; set; }
        public Nullable<int> ContactID { get; set; }
        public string IsLead { get; set; }
    }
}