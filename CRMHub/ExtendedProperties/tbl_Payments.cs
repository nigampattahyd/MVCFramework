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

    [MetadataType(typeof(tbl_PaymentsMetaData))]
    public partial class tbl_Payments
    {
        public string PaymentType { get; set; }
        public string Fname { get; set; }
        public string PaidDate { get; set; }
    }


    public partial class tbl_PaymentsMetaData
    {
        public int PaymentID { get; set; }
        public string PaymentNo { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public Nullable<int> BatchID { get; set; }
        public string CheckNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:n1}")]
        public Nullable<decimal> InvoiceAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:n1}")]
        public Nullable<decimal> InvoiceBalance { get; set; }
        public Nullable<decimal> LateFee { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string Memo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> AccountID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<bool> IsMSAPayment { get; set; }
        public string CreditCardNo { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string Status { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
    }
}
