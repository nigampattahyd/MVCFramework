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
    
    public partial class CRM_Dashboard_PaymentsDetails_Result
    {
        public Nullable<long> InvoiceSent { get; set; }
        public Nullable<long> InvoiceNotSent { get; set; }
        public Nullable<long> EstimateSent { get; set; }
        public Nullable<long> EstimateNotSent { get; set; }
        public Nullable<long> InvoicePaid { get; set; }
        public Nullable<long> Invoicepartiallypaid { get; set; }
        public Nullable<long> PaymentReceivedToday { get; set; }
        public Nullable<long> PaymentNotInitiated { get; set; }
    }
}
