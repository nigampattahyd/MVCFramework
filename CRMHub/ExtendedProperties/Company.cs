using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(CompanyMetaData))]
     public partial class Company
     {
        public string ParentCompanyName { get; set; }
        public string MailingFullAddress { get; set; }
        public string BillingFullAddress { get; set; }

        // Search Criteria
        public string Keyword { get; set; }
        public int RoleID { get; set; }
     }


    public partial class CompanyMetaData
    {
        public int ID { get; set; }

         [Remote("IsCompanyExists", "ManageCompany", AdditionalFields = "ID,Name", ErrorMessage = "Company already exists")]
         public string Name { get; set; }       
         public Nullable<int> ParentCompanyID { get; set; }
         public string Phone { get; set; }
         public string Fax { get; set; }
         public string Website { get; set; }
         public Nullable<int> CompanytypeID { get; set; }
         public Nullable<int> CompanyIndustryID { get; set; }
         [AllowHtml]
         public string Description { get; set; }
         public string CompanySite { get; set; }
         public string CompanyCode { get; set; }
         public string AnnualRevenue { get; set; }
         public Nullable<System.DateTime> CreatedDate { get; set; }
         public Nullable<System.DateTime> ModifiedDate { get; set; }
         public Nullable<int> CreatedBy { get; set; }
         public Nullable<int> ModifiedBy { get; set; }
         public string TrickerSymbol { get; set; }
         public Nullable<int> OwnerID { get; set; }
         public string Ownership { get; set; }
         public string Employees { get; set; }
         public string SicCode { get; set; }
         public string Billingstreet { get; set; }
         public string Shippingstreet { get; set; }
         public string Billingcity { get; set; }
         public string Shippingcity { get; set; }
         public Nullable<int> BillingstateID { get; set; }
         public Nullable<int> ShippingstateID { get; set; }
         public string Billingzip { get; set; }
         public string Shippingzip { get; set; }
         public Nullable<int> BillingcountryID { get; set; }
         public Nullable<int> ShippingcountryID { get; set; }
         public string Naicscode { get; set; }
         public string MailingAddress { get; set; }
         public string BillingAddress { get; set; }
         public string ReferredBy { get; set; }
         public Nullable<int> CompanyStatusID { get; set; }
         public string SeachIndex { get; set; }
    }
}
