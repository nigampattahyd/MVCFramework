using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Aspects;
using System.Data;

namespace CRMHub.Interface
{
    public interface IInvoiceRepository
    {
        #region Invoice
        List<tbl_Invoice> GetAllInvoiceList(string invoice, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);

       // List<tbl_Contact> GetAllCustomerList(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);

        // To Set the Invoice no 
        int SetlastInvoicenumber(string Connectionstring, string dbName);


        List<tbl_Items> GetAllitemsList(string Connectionstring, string dbName);

        tbl_Items GetItemdetailsbyId(int itemid, string Connectionstring, string dbName);

        List<tbl_Items> GetAllItemsListtobind(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);

        int InsertInvoiceDetails(tbl_Invoice INVObj, string Connectionstring, string dbName);
        bool InsertInvoiceitems(tbl_InvoiceItem INVitemsObj, string Connectionstring, string dbName);

        // from Invoice Table by InvoiceId
        bool DeleteInvoicebyID(int invoiceid, string Connectionstring, string dbName);
        // From Invoiceitems Table by InvoiceId
        bool DeleteInvoiceitemsbyInvID(int invoiceid, string Connectionstring, string dbName);

        // To get Invoice Details by InvoiceId
        tbl_Invoice GetInvoiceDetailsbyINVId(int invoiceid, string Connectionstring, string dbName);

        // To get the InvoiceItems By InvoiceId from the Invoiceitems Table
        List<tbl_InvoiceItem> GetAllInvoiceitemsList(int InvoiceId, string Connectionstring, string dbName);

        // To bind the Customer Address Details in the Edit invoice Form
       // tbl_Contact GetCustomerAddressbyId(int ContactId, string Connectionstring, string dbName);

        // To Update the Invoice table by Invoice Id
        bool UpdateInvoicebyInvoiceId(tbl_Invoice INVObj, string Connectionstring, string dbName);

        // To Update the InvoiceItems table by Invoice Id
        bool UpdateInvoiceItemsbyInvoiceItemId(tbl_InvoiceItem INVItemsObj, string Connectionstring, string dbName);

        // Delete Items from the Tbl_invoiceitems based on invoiceitemid
        bool Deleteitemsbyitemid(int invoiceitemid, string Connectionstring, string dbName);
        bool ConvertInvoicetoPost(int invoiceid, string Connectionstring, string dbName);

        List<tbl_EstimateInvoice> GetAllEstimateInvoicerList(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);

        // To Get the Total items Count from the Database for validation:
        int GetitemsListCount(string Connectionstring, string dbName);

        int GetitemsCountfordelete(int itemid,string Connectionstring, string dbName);

        // Converted Code to Zoho
        // To bind the Customer Address Details in the Edit invoice Form
        Company GetCustomerAddressbyId(int CompanyId, string Connectionstring, string dbName);

        List<Company> GetAllCustomerList(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);

        List<tbl_Invoice> GetAllInvoiceListByCustomer(int Customerid, string invoice, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);
        #endregion

        #region EstimateInvoice

        List<tbl_EstimateInvoice> GetAllEstimateInvoiceList(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);
        int GetlastEstimateInvoicenumber(string Connectionstring, string dbName);
        int InsertEstimateInvoiceDetails(tbl_EstimateInvoice ESTINVObj, string Connectionstring, string dbName);
        bool InsertEstimateInvoiceitems(tbl_EstimateInvoiceItem EstINVitemsObj, string Connectionstring, string dbName);

        tbl_EstimateInvoice GetEstimateInvoiceDetailsbyId(int Estinvoiceid, string Connectionstring, string dbName);
        List<tbl_EstimateInvoiceItem> GetAllEstimateInvoiceitemsList(int EstimateInvoiceId, string Connectionstring, string dbName);


        bool UpdateEstimateInvoicebyEstInvoiceId(tbl_EstimateInvoice EstInvObj, string Connectionstring, string dbName);

        bool DeleteEstinvitemsbyestinvitemid(int Estinvoiceitemid, string Connectionstring, string dbName);
        bool UpdateEstiInvoiceItemsbyEstInvoiceItemId(tbl_EstimateInvoiceItem EstINVitemsObj, string Connectionstring, string dbName);

        bool UpdateEstimateInvtoInvoicebyEstid(int Estinvoiceid, string Connectionstring, string dbName);
        bool DeleteEstimateInvoicebyID(int invoiceid, string Connectionstring, string dbName);
        bool DeleteEstimateInvoiceitemsbyInvID(int invoiceid, string Connectionstring, string dbName);
        #endregion


# region Payments
        // To get All the Invoices Based on CustomerID
        //List<tbl_Invoice> GetInvoiceListbyCustomerId(int CustomerID, string Connectionstring, string dbName);
        List<tbl_Invoice> GetInvoiceListbyCustomerId(int CustomerID, out int TotalCount, string Connectionstring, string dbName);

        // Updating the BalanceAmount Field in the tbl_invoice after the payment of amount
        bool UpdateInvoiceBalanceAmount(int invoiceid,string balAmount, string Connectionstring, string dbName);

        // To insert the payment Details into Payment table
        bool InsertPaymentDetails(tbl_Payments PaymentObj, string Connectionstring, string dbName);

        // To Display all the payments made of the Given InvoiceId
        //List<tbl_Payments> GetAllPaymentsbyinvoiceId(int InvoiceID, string Connectionstring, string dbName);
        List<tbl_Payments> GetAllPaymentsbyinvoiceId(int InvoiceID, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);

          // To Display all the Invoices by Contactid and dispaly in the Contact module
        List<tbl_Invoice> GetAllInvoiceListByContactid(string Contactid, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);
        
# endregion

        # region Inventory

        // To check whether the item is in use or not in InvoiceTable
        //int GetCountofItemsinInvoicebyitemid(int itemid, string Connectionstring, string dbName);
        //// To check whether the item is in use or not in EstimateInvoiceTable
        //int GetCountofItemsinEstInvoicebyitemid(int itemid, string Connectionstring, string dbName);
        # endregion
    }
}
