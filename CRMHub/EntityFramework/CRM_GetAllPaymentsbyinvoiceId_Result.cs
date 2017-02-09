//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRMHub.EntityFramework
{
    using System;
    
    public partial class CRM_GetAllPaymentsbyinvoiceId_Result
    {
        public Nullable<int> PaymentID { get; set; }
        public string PaymentNo { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public Nullable<int> BatchID { get; set; }
        public string CheckNo { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<decimal> InvoiceBalance { get; set; }
        public Nullable<decimal> LateFee { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string Memo { get; set; }
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
        public string Name { get; set; }
    }
}
