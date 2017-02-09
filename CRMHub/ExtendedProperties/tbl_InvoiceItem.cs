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
    [MetadataType(typeof(tbl_InvoiceItemMetaData))]
    public partial class tbl_InvoiceItem
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        public Nullable<decimal> QtyCommitted { get; set; }
    }
    public partial class tbl_InvoiceItemMetaData
    {
        public int InvoiceItemId { get; set; }
        public Nullable<int> Invoiceid { get; set; }
        public string Invoiceno { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<decimal> RatePerUnit { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> ItemTotal { get; set; }
    }
}
