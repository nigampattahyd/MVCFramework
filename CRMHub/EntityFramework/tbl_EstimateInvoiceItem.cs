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
    using System.Collections.Generic;
    
    public partial class tbl_EstimateInvoiceItem
    {
        public int EstimateInvoiceItemId { get; set; }
        public Nullable<int> EstInvoiceID { get; set; }
        public string EstInvoiceNo { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<decimal> RatePerUnit { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> ItemTotal { get; set; }
    }
}