using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(tbl_accountMetaData))]
    public partial class tbl_account
    {
        public string Address { get; set; }
        public string Keyword { get; set; }
        public string ParentAccountId { get; set; }
        public string CustomLabel { get; set; }
        public string Customvalue { get; set; }
    }

    public partial class tbl_accountMetaData
    {
        public int AccountId { get; set; }
        [Required]
        [Remote("IsTitleExists", "ManageCompany", AdditionalFields = "AccountId,Account_Name", ErrorMessage = "Company already exists")]
        public string Account_Name { get; set; }
        public string Parent_Account { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Account_type { get; set; }
        public string Account_Industries { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        public string AccountSite { get; set; }
        public string AcountNumber { get; set; }
        public string AnnualRevenue { get; set; }
        public string Rating { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string TrickerSymbol { get; set; }
        public string Ownership { get; set; }
        public string Employees { get; set; }         
        public string SicCode { get; set; }
        public string AccountOwner { get; set; }
        public string Billingstreet { get; set; }
        public string Shippingstreet { get; set; }
        public string Billingcity { get; set; }
        public string Shippingcity { get; set; }
        public string Billingstate { get; set; }
        public string Shippingstate { get; set; }
        public string Billingzip { get; set; }
        public string Shippingzip { get; set; }
        public string Billingcountry { get; set; }
        public string Shippingcountry { get; set; }
        public string Naicscode { get; set; }
        public string Suspect_prospect { get; set; }
        public string SalesMgr1 { get; set; }
        public string SalesMgr2 { get; set; }
        public string Account_office { get; set; }
        public string PhoneExt { get; set; }
        public string MailingAddress { get; set; }
        public string BillingAddress { get; set; }
        public Nullable<bool> IsCorporate { get; set; }
        public string AccountingCustomerId { get; set; }
        public Nullable<bool> RoundToquarter { get; set; }
        public Nullable<decimal> MarkUp { get; set; }
        public string AccountIdEditable { get; set; }
        public string ReferredBy { get; set; }
        public string Status { get; set; }
        public Nullable<int> Export_Companyid { get; set; }
        public Nullable<bool> IsExported { get; set; }
        public string Export_RecruiterId { get; set; }
        public string TraverseId { get; set; }
        public string SeachIndex { get; set; }
    }
}
