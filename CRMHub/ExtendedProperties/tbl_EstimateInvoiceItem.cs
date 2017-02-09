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

    [MetadataType(typeof(tbl_EstimateInvoiceItemMetaData))]
    public partial class tbl_EstimateInvoiceItem
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        public Nullable<decimal> QtyCommitted { get; set; }
    }
    public partial class tbl_EstimateInvoiceItemMetaData
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
