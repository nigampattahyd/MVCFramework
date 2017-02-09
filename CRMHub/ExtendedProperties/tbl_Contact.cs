using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(tbl_ContactMetaData))]
    public partial class tbl_Contact
    {
       [Required(ErrorMessage = "Company Name is required")]
        public string Account_Name { get; set; }
        public string Account_type { get; set; }
        public string accountSite { get; set; }
        public string Keyword { get; set; }
        public string specifiedZip { get; set; }
        public int OfficeId { get; set; }
        public int SalesRepId { get; set; }
        public string AccountOffice { get; set; }
        public string mcreatedDate { get; set; }
        public string mbirthdate { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public decimal? UserID { get; set; }
        //public bool NoNullisAllowSMS
        //{
        //    get { return IsAllowSMS ?? false; }
        //    set { IsAllowSMS = value; }
        //}

        public string lastname { get; set; }
        public int ContactId1 { get; set; }
        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime FromDate { get; set; }
        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime ToDate { get; set; }
        public string CustomLabel { get; set; }
        public string Customvalue { get; set; }
        public string MailingFullAddress { get; set; }
        public string BillingFullAddress { get; set; }

        public string ContactType { get; set; }

    }

    public partial class tbl_ContactMetaData
    {

        public int ContactId { get; set; }
        public string Initial { get; set; }
        public string Fname { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public string Mobile { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Creator { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> AccountId { get; set; }
        public string Homephone { get; set; }
        public string OtherPhone { get; set; }
        public string AsstPhone { get; set; }
        public string Assistant { get; set; }
        public string EmailOptOut { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string ReportTo { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Department { get; set; }
        public string Mailingstreet { get; set; }
        public string Otherstreet { get; set; }
        public string Mailingcity { get; set; }
        public string Othercity { get; set; }
        public string Mailingstate { get; set; }
        public string Otherstate { get; set; }
        public string Mailingzip { get; set; }
        public string Otherzip { get; set; }
        public string Mailingcountry { get; set; }
        public string Othercountry { get; set; }
        public Nullable<bool> ReportingManager { get; set; }
        public string Suspect_prospect { get; set; }
        public string SalesMgr1 { get; set; }
        public string SalesMgr2 { get; set; }
        public string Contact_office { get; set; }
        public string PhoneExt { get; set; }
        public string MailingAddress { get; set; }
        public string BillingAddress { get; set; }
        public Nullable<bool> IsCorporate { get; set; }
        public string ContactIdEditable { get; set; }
        public Nullable<bool> IsAllowSMS { get; set; }
        public string ProviderId { get; set; }
        public string Type { get; set; }
        public string SeachIndex { get; set; }
        public string FacebookUsername { get; set; }
        public string TwitterUsername { get; set; }
        public string LinkedInUsername { get; set; }
        public string GooglePlusUserName { get; set; }
        public string PinterestUsername { get; set; }
        public string SkypeUsername { get; set; }
        public string Sector { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
    }
}
