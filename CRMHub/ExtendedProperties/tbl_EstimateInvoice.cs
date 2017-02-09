﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{




    [MetadataType(typeof(tbl_EstimateInvoiceMetaData))]
    public partial class tbl_EstimateInvoice
    {
        public string Fname { get; set; }
        public string EstInvStatus { get; set; }

        //public Nullable<decimal> Total { get; set; }
    }
    public partial class tbl_EstimateInvoiceMetaData
    {
        public int EstInvoiceID { get; set; }
        public string EstInvoiceNo { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<decimal> TaxPercent { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> EstInvoiceAmount { get; set; }
        public Nullable<decimal> Total { get; set; }
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
        public Nullable<int> ParentEstimateInvoiceID { get; set; }
    }
}