using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(ClientMetaData))]
    public partial class Client
    {
        public string Result { get; set; }
        // for Creation of branch(Office at time of Creating the Client)
        public string BranchName { get; set; }
        public string BranchAddress1 { get; set; }
        public string BranchAddress2 { get; set; }
        public string BranchCity { get; set; }
        public string BranchStateCode { get; set; }
        public string BranchZip { get; set; }
        public string BranchPhone { get; set; }

        // for Creation of User at time of Creating the Client)

        public long UserId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public Nullable<decimal> PhoneExt { get; set; }
        // public string Fax { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        public string PostalCode { get; set; }
        public string Comment { get; set; }

        public Nullable<System.DateTime> LastModified { get; set; }
        public string StateCode { get; set; }
        public Nullable<long> RoleId { get; set; }
        public Nullable<bool> Status { get; set; }
        public string ModifiedBy { get; set; }
        public string Level { get; set; }
        public string BranchId { get; set; }
        public string Cellno { get; set; }
        public Nullable<int> AccountId { get; set; }
        public string ContactId { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<bool> IsAllowsedingSMS { get; set; }
        public string SMTPEmail { get; set; }
        public string SMTPPassword { get; set; }
        public string SMTPAddress { get; set; }
        public string SMTPPort { get; set; }
        public string OutlookEmail { get; set; }
        public string Gender { get; set; }
        public Nullable<bool> IsCorporate { get; set; }
    }


    public partial class ClientMetaData
    {
        public long ClientNumber { get; set; }

        //[Required]
        [Remote("IsClientExists", "CreateClients", AdditionalFields = "ClientNumber,ClientID", ErrorMessage = "Client already exists")]
        public string ClientID { get; set; }
        public byte[] ConnectionString { get; set; }
        public string ServerName { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Timesheet_Type { get; set; }
        public string Timesheet { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string companyphone { get; set; }
        public string fax { get; set; }
        public string companywebsite { get; set; }
        public string companylogo { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public string companytype { get; set; }
        public Nullable<System.DateTime> expirydate { get; set; }
        public Nullable<bool> active { get; set; }
        public string site_type { get; set; }
        public string AttachmentUrl { get; set; }
    }
}
