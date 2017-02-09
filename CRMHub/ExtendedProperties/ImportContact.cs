using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(ImportContact))]
    public partial class ImportContact
    {
        public bool IscompanyExist { get; set; }
        public string Sector { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
    }
    public partial class ImportContact
    {
        public string Initial { get; set; }
        public string FName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public string Mobile { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Creator { get; set; }
        public string ModifiedBy { get; set; }
        public string  CompanyName { get; set; }
        public string Homephone { get; set; }
        public string OtherPhone { get; set; }
        public string AsstPhone { get; set; }
        public string Assistant { get; set; }
        public string EmailOptOut { get; set; }
        public string Description { get; set; }
        public string ReportTo { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Department { get; set; }
        public string Mailingstreet { get; set; }
        public string Otherstreet { get; set; }
        public string Mailingcity { get; set; }
        public string Othercity { get; set; }
        public string Mailingstate { get; set; }
        public string Otherstate { get; set; }
        public int Mailingzip { get; set; }
        public int Otherzip { get; set; }
        public string Mailingcountry { get; set; }
        public string Othercountry { get; set; }
        public bool ReportingManager { get; set; }
        public string Suspect_prospect { get; set; }
        public string SalesMgr1 { get; set; }
        public string SalesMgr2 { get; set; }
        public string Contact_office { get; set; }
        public string PhoneExt { get; set; }
        public string MailingAddress { get; set; }
        public string BillingAddress { get; set; }
        public bool IsCorporate { get; set; }
        public string ContactIdEditable { get; set; }
        public bool IsAllowSMS { get; set; }
        public string ProviderId { get; set; }
        public string Type { get; set; }
        public string SeachIndex { get; set; }
        public string FacebookUsername { get; set; }
        public string TwitterUsername { get; set; }
        public string LinkedInUsername { get; set; }
        public string GooglePlusUserName { get; set; }
        public string PinterestUsername { get; set; }
        public string SkypeUsername { get; set; }
        
        
    }
}
