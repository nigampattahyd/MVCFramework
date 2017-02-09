using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{


    [MetadataType(typeof(userMetaData))]
  public partial  class user
    {
        [Required(ErrorMessage = "ClientId")]
        public string ClientId { get; set; }
        public string Keyword { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Office { get; set; }
        public string SearchCity { get; set; }
        public string SearchState { get; set; }
        public string SearchPhone { get; set; }
        public string SearchEmail { get; set; }
        public string roleName { get; set; }
        public string branchName { get; set; }
        public string stateName { get; set; }
        public string first_Name { get; set; }
        public string strDateOfBirth { get; set; }
        
        public bool NoNullisAllowsedingSMS
        {
            get { return IsAllowsedingSMS ?? false; }
            set { IsAllowsedingSMS = value; }
        }
        public long? VentureId { get; set; }
        public long? MentorId { get; set; }
        public string Type { get; set; }
        public List<SelectListItem> lstRoles { get; set; }
    }
   

    public partial class userMetaData
    {

        public long UserId { get; set; }
        [Required]
        [Remote("IsLoginExists", "Users", AdditionalFields = "UserId,LoginId", ErrorMessage = "LoginId already exists")]
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string PhoneExt { get; set; }
        public string Fax { get; set; }
        [Remote("IsEmailExists", "Users", AdditionalFields = "UserId,Email", ErrorMessage = "Email already exists")]
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Comment { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public string StateCode { get; set; }
        public Nullable<long> RoleId { get; set; }
        public Nullable<bool> Status { get; set; }
        public string ModifiedBy { get; set; }
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
}
