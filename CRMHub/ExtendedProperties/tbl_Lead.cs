using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(tbl_LeadMetaData))]
    public partial class tbl_Lead
    {
        public string FilterExpression { get; set; }
        public string Keyword { get; set; }
        public string ActivityKeyword { get; set; }
        public string ActivityType { get; set; }
        public string ActivityPriority { get; set; }
        public string ActivityStatus { get; set; }
        public string MailEmail { get; set; }
        public string MailType { get; set; }
        public string MailSalesRep { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string MCreateddate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime FromDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime ToDate { get; set; }

    }

    public partial class tbl_LeadMetaData
    {
        public long LeadId { get; set; }
        [Required(ErrorMessage="Please Provide the Name")]
        [Remote("IsTitleExists", "Opportunities", AdditionalFields = "LeadId,LeadName", ErrorMessage = "Name already exists")]
        public string LeadName { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string PreferTime { get; set; }
        public string PreferMode { get; set; }
        public string EmailOptOut { get; set; }
        public Nullable<int> AccountId { get; set; }
        [Required(ErrorMessage="Please Select the Company")]
        public string AccountName { get; set; }
        public string Website { get; set; }
        public string Ownership { get; set; }
        public string Employees { get; set; }
        public string Industry { get; set; }
        public string AnnualRevenue { get; set; }
        public string Rating { get; set; }
        public string LeadStatus { get; set; }
        public string CurrentApplication { get; set; }
        public string MailingAddress { get; set; }
        public string Mailingstreet { get; set; }
        public string Mailingcity { get; set; }
        public string Mailingstate { get; set; }
        public string Mailingzip { get; set; }
        public string Mailingcountry { get; set; }
        public string BillingAddress { get; set; }
        public string Billingstreet { get; set; }
        public string Billingcity { get; set; }
        public string Billingstate { get; set; }
        public string Billingzip { get; set; }
        public string Billingcountry { get; set; }
        public string SalesMgr1 { get; set; }
        public string SalesMgr2 { get; set; }
        public string Office { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }        
        public Nullable<decimal> CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        public Nullable<bool> IsContact { get; set; }
        public string Source { get; set; }
        public Nullable<long> LeadAssigned { get; set; }
        public string LeadStage { get; set; }
        public string SeachIndex { get; set; }
        public string FacebookUsername { get; set; }
        public string TwitterUsername { get; set; }
        public string LinkedInUsername { get; set; }
        public string GooglePlusUsername { get; set; }
        public string PinterestUsername { get; set; }
        public string SkypeUsername { get; set; }
        public Nullable<bool> IsOpportunity { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> EstimatedCloseDate { get; set; }
        public string NextStep { get; set; }
        public string Probability { get; set; }
        public string BusinessType { get; set; }
      
    }
}
