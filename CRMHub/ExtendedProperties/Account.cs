using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(AccountMetaData))]
    public partial class Account
    {
        public bool OptEmailOut { get; set; }
        public string LeadName { get; set; }
        public string CompanyExists { get; set; }
        public bool IsOpportunity { set; get; }

        // For Potential
        public string OppAmount { get; set; }
        public string OppName { get; set; }
        //public Nullable<int> StageID { get; set; }
        //public Nullable<System.DateTime> CloseDate { get; set; }
        // Common
       // public Nullable<int> OwnerusrID { get; set; }

        // Opportunity
        //public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Owner { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string Stage { get; set; }

        // Country and State
       // Mailing
        public string MaillingCountry { get; set; }
        public string MaillingStateName { get; set; }
        public string MaillingAbbreviation { get; set; }

        // Billing
        public string BillingCountry { get; set; }
        public string BillingStateName { get; set; }
        public string BillingAbbreviation { get; set; }

        // Fields For Search Criteria
        public string Keyword { get; set; }
       // public string SCompanyName { get; set; }

        public int RoleID { get; set; }

    }


    public partial class AccountMetaData
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
        //public long ID { get; set; }
        //public string Name { get; set; }
        //public string Title { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Phone { get; set; }
        //public string Fax { get; set; }
        //public string Email { get; set; }
        //public string Mobile { get; set; }
        //public string EmailOptOut { get; set; }
        //public Nullable<int> AccountTypeId { get; set; }
        //public string Website { get; set; }
        //public string Ownership { get; set; }
        //public string TotalEmployees { get; set; }
        //public Nullable<int> IndustryID { get; set; }
        //public string AnnualRevenue { get; set; }
        //public string Rating { get; set; }
        //public string LeadStatus { get; set; }
        //public string LeadSource { get; set; }
        //public string MailingAddress { get; set; }
        //public string MailingAddress2 { get; set; }
        //public string Mailingcity { get; set; }
        //public Nullable<int> MailingstateID { get; set; }
        //public string Mailingzip { get; set; }
        //public Nullable<int> MailingcountryID { get; set; }
        //public string BillingAddress { get; set; }
        //public string BillingAddress2 { get; set; }
        //public string Billingcity { get; set; }
        //public Nullable<int> BillingstateID { get; set; }
        //public string Billingzip { get; set; }
        //public string BillingcountryID { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public Nullable<System.DateTime> ModifiedDate { get; set; }
        //public Nullable<int> CreatedBy { get; set; }
        //public Nullable<int> ModifiedBy { get; set; }
        //[AllowHtml]
        //public string Description { get; set; }
        //public string SeachIndex { get; set; }
        //public string FacebookUsername { get; set; }
        //public string TwitterUsername { get; set; }
        //public string LinkedInUsername { get; set; }
        //public string SkypeUsername { get; set; }
        //public Nullable<int> ContactTypeID { get; set; }
        //public Nullable<int> CompanyID { get; set; }

        //// [Remote("IsCompanyExists", "Leads", AdditionalFields = "CompanyID,CompanyName", ErrorMessage = "Company already exists")]
        //public string CompanyName { get; set; }
        //public Nullable<int> OwnerID { get; set; }
    }
    
}
