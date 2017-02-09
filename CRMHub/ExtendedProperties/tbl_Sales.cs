using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{

    [MetadataType(typeof(tbl_SalesMetadata))]
    public partial class tbl_Sales
    {
       // public List<user> userList { get; set; }
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
        public string SeachIndex { get; set; }
    }
    public partial class tbl_SalesMetadata
    {
        public long SalesId { get; set; }
        [Required]
        [Remote("IsTitleExists", "Sales", AdditionalFields = "SalesId,SalesName", ErrorMessage = "SalesName already exists")]
        public string SalesName { get; set; }
        [Required(ErrorMessage = "Please Select the Company")]
        public string AccountName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Provide the Contact No")]
        public string BillingContact { get; set; }
        public string BillingEmail { get; set; }
        public string Website { get; set; }
        public string Title { get; set; }
        public string BillingAddress { get; set; }
        [Required(ErrorMessage = "Please Select the Sales Representative")]
        public string SalesRep { get; set; }
        public string SalesRepPhone { get; set; }
        public string SalesSource { get; set; }
        public string SalesAmount { get; set; }
        public string Revenue { get; set; }
        [Required(ErrorMessage = "Please Enter the Purchase Order")]
        public string PurchaseOrder { get; set; }
        public DateTime SalesCloseDate { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string PaymentTerms { get; set; }
        public string GM { get; set; }
        public string GMPercentage { get; set; }       
        public bool IsAccounting { get; set; }
        public long SaleCreatedBy { get; set; }
        public DateTime SaleCreatedOn { get; set; }
        public long SaleModifiedBy { get; set; }
        public DateTime SaleModifiedOn { get; set; }
        public string Office { get; set; }
        public int AccountId { get; set; }
        //public List<user> userList { get; set; }
    }

}
