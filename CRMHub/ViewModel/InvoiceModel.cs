using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRMHub.ViewModel
{
    public class InvoiceModel
    {
        public InvoiceModel()
        {
            listContact = new List<tbl_Contact>();
            listinvoice = new List<tbl_Invoice>();
            itemslist = new List<tbl_Items>();
            listinvoiceitems = new List<tbl_InvoiceItem>();

            listEstInvoice = new List<tbl_EstimateInvoice>();
            ListEstimateitems = new List<tbl_EstimateInvoiceItem>();
            Listpayments = new List<tbl_Payments>();

            //companyObj = new Company(); 
            ListCompany = new List<Company>();


        }
        public List<tbl_Contact> listContact { get; set; }
        public List<tbl_Invoice> listinvoice { get; set; }
        public List<tbl_Items> itemslist { get; set; }
        public tbl_Contact Contact { get; set; }
        public tbl_Invoice invoice { get; set; }
        public tbl_Items itemsobj { get; set; }
        public tbl_InvoiceItem InvoiceitemObj { get; set; }
        public List<tbl_InvoiceItem> listinvoiceitems { get; set; }


        public List<tbl_EstimateInvoiceItem> ListEstimateitems { get; set; }
        public tbl_EstimateInvoiceItem estinvitemsobj { get; set; }
        public List<tbl_EstimateInvoice> listEstInvoice { get; set; }
        public tbl_EstimateInvoice estinvoiceobj { get; set; }


        // Payments Table
        public tbl_Payments PaymentObj { get; set; }
        public List<tbl_Payments> Listpayments { get; set; }

        // Company New Table
        public Company companyObj { get; set; }
        public List<Company> ListCompany { get; set; }

    }

}
