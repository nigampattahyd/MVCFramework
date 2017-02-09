using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{

    [MetadataType(typeof(tbl_ItemsMetaData))]
    public partial class tbl_Items
    {
        public Nullable<decimal> ItemTotal { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public Nullable<decimal> TaxPercent { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public string ItemDesc { get; set; }
    }



    public partial class tbl_ItemsMetaData
    {
        public int ItemID { get; set; }
        public int ItemTypeID { get; set; }
        public int MeasureID { get; set; }
        public int ItemLocID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int PrimaryVendorID { get; set; }
        public int AlternateVendorID { get; set; }
        public string AccountNO { get; set; }
        public decimal RateperUnit { get; set; }
        public decimal Saleprice { get; set; }
        public decimal MinimumQty { get; set; }
        public decimal ReorderQty { get; set; }
        public decimal QtyonHand { get; set; }
        public string ItemLocation { get; set; }
        public string ItemUnits { get; set; }
        public string Warranty { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public string updatedby { get; set; }
        public string PartNumber { get; set; }
        public int CompanyID { get; set; }
        public bool IsDeleted { get; set; }
        public bool NonStock { get; set; }
        public decimal QtyCommitted { get; set; }
        public decimal QtyOnOrder { get; set; }
        public bool Checked { get; set; }
        public int ManufacturerID { get; set; }



    }
}
