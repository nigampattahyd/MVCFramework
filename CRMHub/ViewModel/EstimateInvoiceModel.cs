using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class EstimateInvoiceModel
    {
        public EstimateInvoiceModel()
        {
            listContact = new List<tbl_Contact>();
            listinvoice = new List<tbl_Invoice>();
            itemslist = new List<tbl_Items>();
            listinvoiceitems = new List<tbl_InvoiceItem>();


            listEstInvoice = new List<tbl_EstimateInvoice>();
            ListEstimateitems = new List<tbl_EstimateInvoiceItem>();
        }


        public List<tbl_EstimateInvoice> listEstInvoice { get; set; }
        public tbl_EstimateInvoice estinvoiceobj { get; set; }

        public List<tbl_EstimateInvoiceItem> ListEstimateitems { get; set; }
        public tbl_EstimateInvoiceItem estinvitemsobj { get; set; }

        public List<tbl_Contact> listContact { get; set; }

        public List<tbl_Invoice> listinvoice { get; set; }
        public tbl_Invoice invoice { get; set; }

        public tbl_InvoiceItem InvoiceitemObj { get; set; }
        public List<tbl_InvoiceItem> listinvoiceitems { get; set; }

        public List<tbl_Items> itemslist { get; set; }
        public tbl_Items itemsobj { get; set; }

        public tbl_Contact Contact { get; set; }



    }
}
