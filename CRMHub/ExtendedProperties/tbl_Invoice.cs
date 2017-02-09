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
    [MetadataType(typeof(tbl_InvoiceMetaData))]
    public partial class tbl_Invoice
    {
        public string Fname { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }

        public string BillingAddress { get; set; }
        public string Billingstreet { get; set; }
        public string Billingcity { get; set; }
        public string Billingstate { get; set; }
        public string Billingzip { get; set; }
        public string Billingcountry { get; set; }

        public string ShippingAddress { get; set; }
        public string Shippingstreet { get; set; }
        public string Shippingcity { get; set; }
        public string Shippingstate { get; set; }
        public string Shippingzip { get; set; }
        public string Shippingcountry { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string Paymenttype { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public string Status { get; set; }

    }


    public partial class tbl_InvoiceMetaData
    {
        public int InvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<int> TermsID { get; set; }
        public Nullable<decimal> TaxPercent { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<decimal> BalanceAmount { get; set; }
        public Nullable<decimal> GrandTotal { get; set; }
        public Nullable<int> Posted { get; set; }
        public Nullable<bool> IsDrafted { get; set; }
        public string Memo { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public Nullable<bool> IsCreditMemo { get; set; }
        public Nullable<bool> isPaid { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> LateFee { get; set; }
        public string InvoiceType { get; set; }
        public string OrderNo { get; set; }
        public string JobLocation { get; set; }
        public string JobPhone { get; set; }
        public Nullable<System.DateTime> invCretaedDate { get; set; }
        public Nullable<decimal> MiscellaneousAmount { get; set; }
        public Nullable<decimal> LaborAmount { get; set; }
        public Nullable<decimal> MaterialAmount { get; set; }
        public string SoldTo { get; set; }
        public Nullable<int> CreateByUserID { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<decimal> EquipmentAmount { get; set; }
        public string Companyname { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> EstInvoiceID { get; set; }
        public string EstInvoiceNo { get; set; }
    }
}
