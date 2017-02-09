using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRMHub.EntityFramework
{
    public class Import
    {
        //public string CompanyName { get; set; }
        //public string Phone { get; set; }
        //public string Fax { get; set; }    
        //public string WebSite { get; set; }
        //public string Address1 { get; set; }        
        //public string ZipCode { get; set; }
        //public string StateCode { get; set; }
        //public string City { get; set; }
        //public string Account_office { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string WebSite { get; set; }
        public string Account_type { get; set; }
        public string Account_Industries { get; set; }
        public string Description { get; set; }
        public string AccountSite { get; set; }
        public string AcountNumber { get; set; }
        public string AnnualRevenue { get; set; }
        public string Ownership { get; set; }
        public string Employees { get; set; }
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
        public string SalesMgr1 { get; set; }
        public string SalesMgr2 { get; set; }
        public string Account_office { get; set; }
        public string PhoneExt { get; set; }
        public string MailingAddress { get; set; }
        public string BillingAddress { get; set; }
        public string ReferredBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
      
}
