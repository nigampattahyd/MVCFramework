using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Interface;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace CRMHub.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        # region Invoice
        public List<tbl_Invoice> GetAllInvoiceList(string invoice, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);                
                List<tbl_Invoice> invoicelist = new List<tbl_Invoice>();
                    var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));
                    List<CRM_GetAllInvoicesList_Result> ObjInvList = obj.CRM_GetAllInvoicesList(invoice,startIndex, pageSize, orderByClause, output).ToList();
                    invoicelist = ObjInvList.Select(Inv => new tbl_Invoice
                    {
                        CustomerID = Inv.CustomerID,
                        Fname = Inv.Name,
                        InvoiceID = Inv.InvoiceID.GetValueOrDefault(),                      
                        InvoiceNo = Inv.InvoiceNo,
                        GrandTotal = Inv.GrandTotal,
                        Active = Inv.Active,
                        DueDate = Inv.DueDate,
                        Posted = Inv.Posted,
                        InvoiceType = Inv.InvoiceType,
                        EstInvoiceID = Inv.EstInvoiceID,
                        EstInvoiceNo = Inv.EstInvoiceNo,
                        BalanceAmount = Inv.BalanceAmount

                    }).ToList();


                TotalCount = Convert.ToInt32(invoicelist.Count);
                return invoicelist;

               
            }
            catch (Exception)
            {
                throw;
            }
        }

        // 
        //public List<tbl_Contact> GetAllCustomerList(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

        //        List<tbl_Contact> CustomerList = obj.tbl_Contact.Where(CT => CT.Type == "1").ToList();


        //        TotalCount = CustomerList.Count;
        //        return CustomerList.Select(CL => new tbl_Contact
        //        {
        //            Fname = CL.Fname,
        //            ContactId = CL.ContactId,

        //            BillingAddress = CL.BillingAddress,
        //            Otherstreet = CL.Otherstreet,
        //            Othercity = CL.Othercity,
        //            Otherstate = CL.Otherstate,
        //            Othercountry = CL.Othercountry,
        //            Otherzip = CL.Otherzip,

        //            MailingAddress = CL.MailingAddress,
        //            Mailingstreet = CL.Mailingstreet,
        //            Mailingcity = CL.Mailingcity,
        //            Mailingstate = CL.Mailingstate,
        //            Mailingcountry = CL.Mailingcountry,
        //            Mailingzip = CL.Mailingzip,
        //            Phone = CL.Phone,
        //            Mobile = CL.Mobile
        //        }).ToList();



        //        // return CustomerList;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        // List<tbl_Items> GetAllItemsList(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);
        public List<tbl_Items> GetAllItemsListtobind(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                List<tbl_Items> LstItems = obj.tbl_Items.Where(itm => itm.Active == true).ToList();
                //List<tbl_Items> LstItems = obj.tbl_Items.ToList();

                TotalCount = LstItems.Count;
                return LstItems;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public int SetlastInvoicenumber(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                tbl_Invoice invoicedetails = new tbl_Invoice();

                string invoiceno = obj.tbl_Invoice.OrderByDescending(x => x.InvoiceNo).Select(ti => ti.InvoiceNo).FirstOrDefault();
                int Lastinvoiceno = 0;

                if (invoiceno != null)
                {
                    string[] InvNum = invoiceno.Split('-');
                    Lastinvoiceno = Convert.ToInt32(InvNum[1]);
                }
                return Lastinvoiceno;

                //int? invoiceno = obj.tbl_Invoice.OrderByDescending(x => x.InvoiceID).Select(ti => ti.InvoiceID).FirstOrDefault();
                //return invoiceno.GetValueOrDefault();

            }
            catch (Exception)
            {
                throw;
            }

        }








        public List<tbl_Items> GetAllitemsList(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<tbl_Items> getitemslist = obj.tbl_Items.ToList();
                return getitemslist;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public tbl_Items GetItemdetailsbyId(int itemid, string Connectionstring, string dbName)
        {

            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                tbl_Items getitemslist = obj.tbl_Items.Where(item => item.ItemID == itemid).FirstOrDefault();
                return getitemslist;

            }
            catch (Exception)
            {
                throw;
            }
        }

        // Inserting the Main Invoice Table Details
        public int InsertInvoiceDetails(tbl_Invoice INVObj, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                int invoiceid = 0;
                obj.tbl_Invoice.Add(INVObj);
                obj.SaveChanges();
                invoiceid = INVObj.InvoiceID;
                return invoiceid;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // To insert the Invoice item to the Invoice item table
        public bool InsertInvoiceitems(tbl_InvoiceItem INVitemsObj, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                obj.tbl_InvoiceItem.Add(INVitemsObj);
                obj.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // from Invoice Table by InvoiceId
        public bool DeleteInvoicebyID(int invoiceid, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                bool retval = false;
                var delinv = (from inv in obj.tbl_Invoice
                              where inv.InvoiceID == invoiceid
                              select inv).FirstOrDefault();
                //obj.tbl_Invoice.Remove(delinv);
                if (delinv.Posted == 0) 
                {
                    obj.tbl_Invoice.Remove(delinv);
                    obj.SaveChanges();
                    retval = true;
                }
                return retval;
            }
            catch (Exception)
            {
                throw;
            }

        }


        //From Invoiceitems Table by InvoiceId
        public bool DeleteInvoiceitemsbyInvID(int invoiceid, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                var DelInvItemsList = obj.tbl_InvoiceItem.Where(IT => IT.Invoiceid == invoiceid).ToList();

                foreach (var itemslst in DelInvItemsList)
                    obj.tbl_InvoiceItem.Remove(itemslst);
                obj.SaveChanges();



                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public tbl_Invoice GetInvoiceDetailsbyINVId(int invoiceid, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);

            var Invobj = obj.CRM_GETINVOICEDETAILSBYINVID(invoiceid).Select(Inv => new tbl_Invoice
            {

                InvoiceID = Inv.InvoiceID,
                InvoiceNo = Inv.InvoiceNo,
                CustomerID = Inv.CustomerID,
                Fname = Inv.Name,
                //Fname = Con == null ? "" : Con.Fname,
                TermsID = Inv.TermsID,
                GrandTotal = Inv.GrandTotal,
                CreatedDate = Inv.CreatedDate,
                DueDate = Inv.DueDate,
                Description = Inv.Description,
                TaxAmount = Inv.TaxAmount,
                TaxPercent = Inv.TaxPercent,
                Total = Inv.InvoiceAmount,
                DiscountPercent = Inv.DiscountPercent,
                Discount = Inv.Discount,
                MiscellaneousAmount = Inv.MiscellaneousAmount,
                Posted = Inv.Posted,
                InvoiceType = Inv.InvoiceType,
                BalanceAmount = Inv.BalanceAmount
            }).FirstOrDefault();


            return Invobj;


        }

        public List<tbl_InvoiceItem> GetAllInvoiceitemsList(int InvoiceId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<tbl_InvoiceItem> GetInvoiceitems = obj.tbl_InvoiceItem.Where(InItem => InItem.Invoiceid == InvoiceId).ToList();
                return GetInvoiceitems;


            }
            catch (Exception)
            {
                throw;
            }
        }


        //public tbl_Contact GetCustomerAddressbyId(int ContactId, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        tbl_Contact objContact = obj.tbl_Contact.Where(Con => Con.ContactId == ContactId).FirstOrDefault();
        //        return objContact;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public Company GetCustomerAddressbyId(int CompanyId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                Company ObjCompany = obj.Companies.Where(com => com.ID == CompanyId).FirstOrDefault();
               
                return ObjCompany;

            }
            catch (Exception)
            {
                throw;
            }
        }


        // To Update the Invoice table by Invoice Id
        public bool UpdateInvoicebyInvoiceId(tbl_Invoice INVObj, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                tbl_Invoice objInv = obj.tbl_Invoice.Where(Inv => Inv.InvoiceID == INVObj.InvoiceID).FirstOrDefault();
                objInv.InvoiceNo = INVObj.InvoiceNo;
                objInv.Active = INVObj.Active;
                objInv.BalanceAmount = INVObj.BalanceAmount;
                //objInv.BillingAddress = INVObj.BillingAddress;
                //objInv.Billingcity = INVObj.Billingcity;
                //objInv.Billingcountry = INVObj.Billingcountry;
                //objInv.Billingstate = INVObj.Billingstate;
                //objInv.Billingstreet = INVObj.Billingstreet;
                //objInv.Billingzip = INVObj.Billingzip;
                objInv.CompanyId = INVObj.CompanyId;
                objInv.Companyname = INVObj.Companyname;
                objInv.CreateByUserID = INVObj.CreateByUserID;
                objInv.CreatedBy = INVObj.CreatedBy;
                objInv.CreatedDate = INVObj.CreatedDate;
                objInv.CustomerID = INVObj.CustomerID;
                objInv.Description = INVObj.Description;
                objInv.Discount = INVObj.Discount;
                objInv.DiscountPercent = INVObj.DiscountPercent;
                objInv.DueDate = INVObj.DueDate;
                objInv.EquipmentAmount = INVObj.EquipmentAmount;
                objInv.Fname = INVObj.Fname;
                objInv.GrandTotal = INVObj.GrandTotal;
                objInv.invCretaedDate = INVObj.invCretaedDate;
                objInv.InvoiceAmount = INVObj.InvoiceAmount;
                objInv.InvoiceType = INVObj.InvoiceType;
                objInv.IsCreditMemo = INVObj.IsCreditMemo;
                objInv.IsDrafted = INVObj.IsDrafted;
                objInv.isPaid = INVObj.isPaid;
                // objInv.ItemID = INVObj.ItemID;
                objInv.JobLocation = INVObj.JobLocation;
                objInv.JobPhone = INVObj.JobPhone;
                objInv.LaborAmount = INVObj.LaborAmount;
                objInv.LateFee = INVObj.LateFee;
                objInv.LName = INVObj.LName;
                objInv.MaterialAmount = INVObj.MaterialAmount;
                objInv.Memo = INVObj.Memo;
                objInv.MiscellaneousAmount = INVObj.MiscellaneousAmount;
                objInv.ModifiedBy = INVObj.ModifiedBy;
                objInv.ModifiedDate = INVObj.ModifiedDate;
                objInv.OrderNo = INVObj.OrderNo;
                objInv.Posted = INVObj.Posted;
                //objInv.Quantity = INVObj.Quantity;
                objInv.SoldTo = INVObj.SoldTo;
                objInv.TaxAmount = INVObj.TaxAmount;
                objInv.TaxPercent = INVObj.TaxPercent;
                objInv.TermsID = INVObj.TermsID;
                objInv.Total = INVObj.Total;
                obj.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }


        // To Update the InvoiceItem table by InvoiceItemId
        public bool UpdateInvoiceItemsbyInvoiceItemId(tbl_InvoiceItem INVItemsObj, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                tbl_InvoiceItem objInvItems = obj.tbl_InvoiceItem.Where(InvItems => InvItems.InvoiceItemId == INVItemsObj.InvoiceItemId).FirstOrDefault();
                objInvItems.Invoiceid = INVItemsObj.Invoiceid;
                objInvItems.Invoiceno = INVItemsObj.Invoiceno;
                objInvItems.ItemId = INVItemsObj.ItemId;
                objInvItems.RatePerUnit = INVItemsObj.RatePerUnit;
                objInvItems.Quantity = INVItemsObj.Quantity;
                objInvItems.ItemTotal = INVItemsObj.ItemTotal;
                obj.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                throw;
            }

        }


        // Delete Items from the Tbl_invoiceitems based on invoiceitemid
        public bool Deleteitemsbyitemid(int invoiceitemid, string Connectionstring, string dbName)
        {
            bool Result = false;
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                tbl_InvoiceItem objInvItems = obj.tbl_InvoiceItem.Where(INVItem => INVItem.InvoiceItemId == invoiceitemid).SingleOrDefault();
                if (objInvItems != null)
                {
                    obj.tbl_InvoiceItem.Remove(objInvItems);
                    obj.SaveChanges();
                    Result = true;
                }
                return Result;
            }
            catch (Exception)
            {
                return Result;
            }
        }

        public bool ConvertInvoicetoPost(int invoiceid, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                tbl_Invoice Invobj = obj.tbl_Invoice.Where(inv => inv.InvoiceID == invoiceid).SingleOrDefault();
                Invobj.Posted = 1;

                obj.SaveChanges();


                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }




        public List<tbl_EstimateInvoice> GetAllEstimateInvoicerList(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                //var Estinvoicelist = (from Inv in obj.tbl_EstimateInvoice.Where(i => i.Posted == 0).AsEnumerable()
                //                      join Con in obj.tbl_Contact
                //                          on Inv.CustomerID equals Con.ContactId
                //                      into temp
                //                      from rt in temp.DefaultIfEmpty()
                //                      select new tbl_EstimateInvoice
                //                      {
                //                          CustomerID = Inv.CustomerID,
                //                          Fname = rt.Fname,
                //                         // Companyname = Inv.Companyname,
                //                          EstInvoiceID = Inv.EstInvoiceID,
                //                          EstInvoiceNo = Inv.EstInvoiceNo,
                //                          GrandTotal = Inv.GrandTotal,
                //                          Active = Inv.Active,
                //                          Posted = Inv.Posted
                //                      }).ToList();

                var Estinvoicelist = (from Inv in obj.tbl_EstimateInvoice.Where(i => i.Posted == 0).AsEnumerable()
                                      join Con in obj.Companies
                                          on Inv.CustomerID equals Con.ID
                                      into temp
                                      from rt in temp.DefaultIfEmpty()
                                      select new tbl_EstimateInvoice
                                      {
                                          CustomerID = Inv.CustomerID,
                                          Fname = rt.Name,
                                          EstInvoiceID = Inv.EstInvoiceID,
                                          EstInvoiceNo = Inv.EstInvoiceNo,
                                          GrandTotal = Inv.GrandTotal,
                                          Active = Inv.Active,
                                          Posted = Inv.Posted
                                      }).ToList();

                TotalCount = Convert.ToInt32(Estinvoicelist.Count);
                return Estinvoicelist;
                //List<tbl_EstimateInvoice> LstEstIvoice = obj.tbl_EstimateInvoice.Where(ETI => ETI.Posted == 0).ToList();  
                //TotalCount = LstEstIvoice.Count;
                //return LstEstIvoice;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int GetitemsListCount(string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                int count = 0;
                List<tbl_Items> getitemslist = obj.tbl_Items.ToList();
                count = getitemslist.Count();


                return count;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public int GetitemsCountfordelete(int itemid, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                int itemidcount = 0;
                int invoicecount = obj.tbl_InvoiceItem.Where(Invitem => Invitem.InvoiceItemId == itemid).Count();
                int EstInvoiceCount = obj.tbl_EstimateInvoiceItem.Where(EstInv => EstInv.EstimateInvoiceItemId == itemid).Count();
                itemidcount = invoicecount + EstInvoiceCount;
                return itemidcount;
            }
            catch (Exception)
            {
                throw;
            }
         
        }


        # endregion

        #region EstimateInvoice


        public List<tbl_EstimateInvoice> GetAllEstimateInvoiceList(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
             
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));
                List<CRM_GetAllEstimateInvoicesList_Result> ObjEstInvList = obj.CRM_GetAllEstimateInvoicesList(startIndex, pageSize, orderByClause, output).ToList();
                var Estinvoicelist = ObjEstInvList.Select(EST => new tbl_EstimateInvoice
                {
                    CustomerID = EST.CustomerID,
                    Fname = EST.Name,
                    EstInvoiceID = EST.EstInvoiceID.GetValueOrDefault(),
                    EstInvoiceNo = EST.EstInvoiceNo,
                    GrandTotal = EST.GrandTotal,
                    Active = EST.Active,
                    Posted = EST.Posted

                }).ToList();
                TotalCount = Convert.ToInt32(Estinvoicelist.Count);
                return Estinvoicelist;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int GetlastEstimateInvoicenumber(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                tbl_EstimateInvoice EstInvdetails = new tbl_EstimateInvoice();

                string Estinvoiceno = obj.tbl_EstimateInvoice.OrderByDescending(x => x.EstInvoiceNo).Select(ti => ti.EstInvoiceNo).FirstOrDefault();
                int LastEstinvoiceno = 0;

                if (Estinvoiceno != null)
                {
                    string[] EstInvNum = Estinvoiceno.Split('-');
                    LastEstinvoiceno = Convert.ToInt32(EstInvNum[1]);
                }
                return LastEstinvoiceno;




                //int? estinvoiceno = obj.tbl_EstimateInvoice.OrderByDescending(x => x.EstInvoiceID).Select(ti => ti.EstInvoiceID).FirstOrDefault();                
                //return estinvoiceno.GetValueOrDefault();

            }
            catch (Exception)
            {
                throw;
            }

        }


        // Inserting the Main Invoice Table Details
        public int InsertEstimateInvoiceDetails(tbl_EstimateInvoice ESTINVObj, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                int estinvoiceid = 0;
                obj.tbl_EstimateInvoice.Add(ESTINVObj);
                obj.SaveChanges();
                estinvoiceid = ESTINVObj.EstInvoiceID;
                return estinvoiceid;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // To insert the Estimate Invoice item to the Estimate Invoice item table
        public bool InsertEstimateInvoiceitems(tbl_EstimateInvoiceItem EstINVitemsObj, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                obj.tbl_EstimateInvoiceItem.Add(EstINVitemsObj);
                obj.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public tbl_EstimateInvoice GetEstimateInvoiceDetailsbyId(int Estinvoiceid, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);


            var EstInvObj = obj.CRM_GET_ESTIMATEINVOICEDETAILSBY_ID(Estinvoiceid).Select(ESTI => new tbl_EstimateInvoice
            {
                EstInvoiceID = ESTI.EstInvoiceID,
                EstInvoiceNo = ESTI.EstInvoiceNo,
                CustomerID = ESTI.CustomerID,
                Fname = ESTI.Name,
                GrandTotal = ESTI.GrandTotal,
                CreatedDate = ESTI.CreatedDate,
                Description = ESTI.Description,
                TaxAmount = ESTI.TaxAmount,
                TaxPercent = ESTI.TaxPercent,

                //Total = ESTI.InvoiceAmount,
                DiscountPercent = ESTI.DiscountPercent,
                Discount = ESTI.Discount,
                MiscellaneousAmount = ESTI.MiscellaneousAmount,
                Posted = ESTI.Posted,
                Total = ESTI.Total

            }).FirstOrDefault();


            return EstInvObj;


        }




        public List<tbl_EstimateInvoiceItem> GetAllEstimateInvoiceitemsList(int EstimateInvoiceId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<tbl_EstimateInvoiceItem> GetEstInvoiceItems = obj.tbl_EstimateInvoiceItem.Where(EST => EST.EstInvoiceID == EstimateInvoiceId).ToList();


                return GetEstInvoiceItems;


            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool UpdateEstimateInvoicebyEstInvoiceId(tbl_EstimateInvoice EstInvObj, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);
            tbl_EstimateInvoice objEstInv = obj.tbl_EstimateInvoice.Where(EstI => EstI.EstInvoiceID == EstInvObj.EstInvoiceID).FirstOrDefault();
            objEstInv.EstInvoiceNo = EstInvObj.EstInvoiceNo;
            objEstInv.Active = EstInvObj.Active;


            objEstInv.CompanyId = EstInvObj.CompanyId;
            objEstInv.Companyname = EstInvObj.Companyname;
            objEstInv.CreateByUserID = EstInvObj.CreateByUserID;
            objEstInv.CreatedBy = EstInvObj.CreatedBy;
            objEstInv.CreatedDate = EstInvObj.CreatedDate;
            objEstInv.CustomerID = EstInvObj.CustomerID;
            objEstInv.Description = EstInvObj.Description;
            objEstInv.Discount = EstInvObj.Discount;
            objEstInv.DiscountPercent = EstInvObj.DiscountPercent;
            //objEstInv.DueDate = EstInvObj.DueDate;
            objEstInv.EquipmentAmount = EstInvObj.EquipmentAmount;
            objEstInv.Fname = EstInvObj.Fname;
            objEstInv.GrandTotal = EstInvObj.GrandTotal;
            objEstInv.Total = EstInvObj.Total;
            objEstInv.invCretaedDate = EstInvObj.invCretaedDate;
            //objEstInv.InvoiceAmount = EstInvObj.InvoiceAmount;
            objEstInv.InvoiceType = EstInvObj.InvoiceType;
            objEstInv.IsCreditMemo = EstInvObj.IsCreditMemo;
            objEstInv.IsDrafted = EstInvObj.IsDrafted;
            objEstInv.isPaid = EstInvObj.isPaid;
            // objInv.ItemID = INVObj.ItemID;
            objEstInv.JobLocation = EstInvObj.JobLocation;
            objEstInv.JobPhone = EstInvObj.JobPhone;
            objEstInv.LaborAmount = EstInvObj.LaborAmount;
            objEstInv.LateFee = EstInvObj.LateFee;

            objEstInv.MaterialAmount = EstInvObj.MaterialAmount;
            objEstInv.Memo = EstInvObj.Memo;
            objEstInv.MiscellaneousAmount = EstInvObj.MiscellaneousAmount;
            objEstInv.ModifiedBy = EstInvObj.ModifiedBy;
            objEstInv.ModifiedDate = EstInvObj.ModifiedDate;
            objEstInv.OrderNo = EstInvObj.OrderNo;
            objEstInv.Posted = EstInvObj.Posted;
            //objInv.Quantity = INVObj.Quantity;
            objEstInv.SoldTo = EstInvObj.SoldTo;
            objEstInv.TaxAmount = EstInvObj.TaxAmount;
            objEstInv.TaxPercent = EstInvObj.TaxPercent;
            objEstInv.Posted = EstInvObj.Posted;
            //objEstInv.TermsID = EstInvObj.TermsID;
            obj.SaveChanges();

            return true;

        }

        // bool DeleteEstinvitemsbyestinvitemid(int Estinvoiceitemid, string Connectionstring, string dbName);
        public bool DeleteEstinvitemsbyestinvitemid(int Estinvoiceitemid, string Connectionstring, string dbName)
        {
            bool Result = false;
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                tbl_EstimateInvoiceItem ObjEstInvItems = obj.tbl_EstimateInvoiceItem.Where(EIIObj => EIIObj.EstimateInvoiceItemId == Estinvoiceitemid).SingleOrDefault();
                if (ObjEstInvItems != null)
                {
                    obj.tbl_EstimateInvoiceItem.Remove(ObjEstInvItems);
                    obj.SaveChanges();
                    Result = true;
                }
                return Result;
            }
            catch (Exception)
            {
                return Result;
            }
        }

        // bool UpdateEstiInvoiceItemsbyEstInvoiceItemId(tbl_InvoiceItem INVItemsObj, string Connectionstring, string dbName);
        public bool UpdateEstiInvoiceItemsbyEstInvoiceItemId(tbl_EstimateInvoiceItem EstINVitemsObj, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                tbl_EstimateInvoiceItem ObjEstInvItems = obj.tbl_EstimateInvoiceItem.Where(Einv => Einv.EstimateInvoiceItemId == EstINVitemsObj.EstimateInvoiceItemId).FirstOrDefault();
                //ObjEstInvItems.EstInvoiceID = EstINVitemsObj.EstInvoiceID;
                ObjEstInvItems.EstInvoiceNo = EstINVitemsObj.EstInvoiceNo;
                ObjEstInvItems.ItemId = EstINVitemsObj.ItemId;
                ObjEstInvItems.RatePerUnit = EstINVitemsObj.RatePerUnit;
                ObjEstInvItems.Quantity = EstINVitemsObj.Quantity;
                ObjEstInvItems.ItemTotal = EstINVitemsObj.ItemTotal;


                //tbl_InvoiceItem objInvItems = obj.tbl_InvoiceItem.Where(InvItems => InvItems.InvoiceItemId == INVItemsObj.InvoiceItemId).FirstOrDefault();
                //objInvItems.Invoiceid = INVItemsObj.Invoiceid;
                //objInvItems.Invoiceno = INVItemsObj.Invoiceno;
                //objInvItems.ItemId = INVItemsObj.ItemId;
                //objInvItems.RatePerUnit = INVItemsObj.RatePerUnit;
                //objInvItems.Quantity = INVItemsObj.Quantity;
                //objInvItems.ItemTotal = INVItemsObj.ItemTotal;
                obj.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool UpdateEstimateInvtoInvoicebyEstid(int Estinvoiceid, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                tbl_EstimateInvoice objEstInv = obj.tbl_EstimateInvoice.Where(EstI => EstI.EstInvoiceID == Estinvoiceid).FirstOrDefault();
                objEstInv.Posted = 1;
                obj.SaveChanges();
                return true;


            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteEstimateInvoicebyID(int invoiceid, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                bool retval = false;
                var delinv = (from inv in obj.tbl_EstimateInvoice
                              where inv.EstInvoiceID == invoiceid
                              select inv).FirstOrDefault();
                if (delinv.Posted == 0)
                {
                    obj.tbl_EstimateInvoice.Remove(delinv);
                    obj.SaveChanges();
                    retval = true;
                }
                return retval;
                //obj.tbl_EstimateInvoice.Remove(delinv);
                //obj.SaveChanges();
                //return true;
            }
            catch (Exception)
            {
                throw;
            }

        }


        //From Invoiceitems Table by InvoiceId
        public bool DeleteEstimateInvoiceitemsbyInvID(int invoiceid, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                var DelInvItemsList = obj.tbl_EstimateInvoiceItem.Where(IT => IT.EstInvoiceID == invoiceid).ToList();

                foreach (var itemslst in DelInvItemsList)
                    obj.tbl_EstimateInvoiceItem.Remove(itemslst);
                obj.SaveChanges();



                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

# region payments

        // Get List of Invoices based on Customerid
        public List<tbl_Invoice> GetInvoiceListbyCustomerId(int CustomerID,out int TotalCount, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);
            var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
           
            List<CRM_GET_INVOICELIST_BYCUSID_Result> invoicelst = obj.CRM_GET_INVOICELIST_BYCUSID(CustomerID).ToList();
            TotalCount = Convert.ToInt32(invoicelst.Count);

                var InvList = invoicelst.Select(INVC => new tbl_Invoice
                    {
                        InvoiceID = INVC.InvoiceID,
                        InvoiceNo = INVC.InvoiceNo,
                        CustomerID = INVC.CustomerID,
                        Fname = INVC.Name,
                        //Fname = Con == null ? "" : Con.Fname,
                        TermsID = INVC.TermsID,
                        GrandTotal = INVC.GrandTotal,
                        CreatedDate = INVC.CreatedDate,
                        DueDate = INVC.DueDate,
                        Description = INVC.Description,
                        TaxAmount = INVC.TaxAmount,
                        TaxPercent = INVC.TaxPercent,
                        Total = INVC.InvoiceAmount,
                        DiscountPercent = INVC.DiscountPercent,
                        Discount = INVC.Discount,
                        MiscellaneousAmount = INVC.MiscellaneousAmount,
                        Posted = INVC.Posted,
                        InvoiceType = INVC.InvoiceType,
                        BalanceAmount = INVC.BalanceAmount,
                         InvoiceAmount = INVC.InvoiceAmount
                    }).ToList();
                return InvList;
           // }
           
            


            //return invoicelst;


        }


        public bool UpdateInvoiceBalanceAmount(int invoiceid,string balAmount, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                tbl_Invoice Invobj = obj.tbl_Invoice.Where(inv => inv.InvoiceID == invoiceid).SingleOrDefault();
                Invobj.BalanceAmount = Convert.ToDecimal(balAmount);

                obj.SaveChanges();


                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }


        // To insert the payment Details into Payment table
        public bool InsertPaymentDetails(tbl_Payments PaymentObj, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                obj.tbl_Payments.Add(PaymentObj);
                obj.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

     
        //public List<tbl_Payments> GetAllPaymentsbyinvoiceId(int InvoiceID, string Connectionstring, string dbName)
        public List<tbl_Payments> GetAllPaymentsbyinvoiceId(int InvoiceID, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);
            var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));

            List<tbl_Payments> LstPayment = new List<tbl_Payments>();
            List<CRM_GetAllPaymentsbyinvoiceId_Result> LstPaymentObj = obj.CRM_GetAllPaymentsbyinvoiceId(InvoiceID.ToString(),startIndex, pageSize, orderByClause, output).ToList();
            TotalCount = Convert.ToInt32(LstPaymentObj.Count);

            var LstPaymentslst = LstPaymentObj.Select(Payment => new tbl_Payments
            {
                CustomerID = Payment.CustomerID,
                Fname = Payment.Name,
                InvoiceID = Payment.InvoiceID,
                InvoiceAmount = Payment.InvoiceAmount,
                InvoiceBalance = Payment.InvoiceBalance,
                AmountPaid = Payment.AmountPaid,
                PaymentMode = Payment.PaymentMode,
                CreatedDate = Payment.CreatedDate
            }).ToList();

            //List<tbl_Payments> LstPayment1 = new List<tbl_Payments>();

            //LstPayment1 = (from Payment in obj.tbl_Payments.Where(p=>p.InvoiceID ==InvoiceID).ToList()
            //               join Con in obj.tbl_Contact
            //                   on Payment.CustomerID equals Con.ContactId
            //               into temp
            //               from rt in temp.DefaultIfEmpty()
            //               select new tbl_Payments
            //               {
                               
            //                   CustomerID = Payment.CustomerID,
            //                   Fname = rt.Fname,
            //                   InvoiceID = Payment.InvoiceID,
            //                   InvoiceAmount=Payment.InvoiceAmount,
            //                   InvoiceBalance=Payment.InvoiceBalance,
            //                   AmountPaid=Payment.AmountPaid,
            //                   PaymentMode=Payment.PaymentMode,
            //                   CreatedDate=Payment.CreatedDate
                              

            //               }).ToList();


            //TotalCount = Convert.ToInt32(LstPayment1.Count);
            return LstPaymentslst;


        }

# endregion

        # region binding Invoices for Contacts from Contact module

        public List<tbl_Invoice> GetAllInvoiceListByContactid(string Contactid, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                int CustomerID = Convert.ToInt32(Contactid);

                List<CRM_GET_INVOICELIST_BYCUSID_Result> invoicelst = obj.CRM_GET_INVOICELIST_BYCUSID(CustomerID).ToList();
                TotalCount = Convert.ToInt32(invoicelst.Count);

                var InvList = invoicelst.Select(INVC => new tbl_Invoice
                {
                    InvoiceID = INVC.InvoiceID,
                    InvoiceNo = INVC.InvoiceNo,
                    CustomerID = INVC.CustomerID,
                    Fname = INVC.Name,
                    //Fname = Con == null ? "" : Con.Fname,
                    TermsID = INVC.TermsID,
                    GrandTotal = INVC.GrandTotal,
                    CreatedDate = INVC.CreatedDate,
                    DueDate = INVC.DueDate,
                    Description = INVC.Description,
                    TaxAmount = INVC.TaxAmount,
                    TaxPercent = INVC.TaxPercent,
                    Total = INVC.InvoiceAmount,
                    DiscountPercent = INVC.DiscountPercent,
                    Discount = INVC.Discount,
                    MiscellaneousAmount = INVC.MiscellaneousAmount,
                    Posted = INVC.Posted,
                    InvoiceType = INVC.InvoiceType,
                    BalanceAmount = INVC.BalanceAmount,
                    InvoiceAmount = INVC.InvoiceAmount
                    
                }).ToList();
                return InvList;


                //var invoicelist = (from Inv in obj.tbl_Invoice.Where(i => i.CustomerID == Convert.ToInt32(Contactid)).AsEnumerable()
                //                   join Con in obj.tbl_Contact
                //                       on Inv.CustomerID equals Con.ContactId
                //                   into temp
                //                   from rt in temp.DefaultIfEmpty()
                //                   select new tbl_Invoice
                //                   {
                //                       CustomerID = Inv.CustomerID,
                //                       Fname = rt.Fname,
                //                       InvoiceID = Inv.InvoiceID,
                //                       InvoiceNo = Inv.InvoiceNo,
                //                       GrandTotal = Inv.GrandTotal,
                //                       Active = Inv.Active,
                //                       DueDate = Inv.DueDate,
                //                       Posted = Inv.Posted,
                //                       InvoiceType = Inv.InvoiceType,
                //                       EstInvoiceID = Inv.EstInvoiceID,
                //                       EstInvoiceNo = Inv.EstInvoiceNo,
                //                       BalanceAmount = Inv.BalanceAmount

                //                   }).ToList();




                //TotalCount = Convert.ToInt32(InvList.Count);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Company> GetAllCustomerList(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                List<Company> CustomerList = obj.Companies.ToList();

               // List<tbl_Contact> CustomerList = obj.tbl_Contact.Where(CT => CT.Type == "1").ToList();


                TotalCount = CustomerList.Count;

                return CustomerList.Select(CL => new Company
                    {
                        Name = CL.Name,
                        ID = CL.ID,
                        Phone = CL.Phone,
                        MailingAddress = CL.MailingAddress
                        
                    }).ToList();
                //return CustomerList.Select(CL => new tbl_Contact
                //{
                //    Fname = CL.Fname,
                //    ContactId = CL.ContactId,

                //    BillingAddress = CL.BillingAddress,
                //    Otherstreet = CL.Otherstreet,
                //    Othercity = CL.Othercity,
                //    Otherstate = CL.Otherstate,
                //    Othercountry = CL.Othercountry,
                //    Otherzip = CL.Otherzip,

                //    MailingAddress = CL.MailingAddress,
                //    Mailingstreet = CL.Mailingstreet,
                //    Mailingcity = CL.Mailingcity,
                //    Mailingstate = CL.Mailingstate,
                //    Mailingcountry = CL.Mailingcountry,
                //    Mailingzip = CL.Mailingzip,
                //    Phone = CL.Phone,
                //    Mobile = CL.Mobile
                //}).ToList();



                // return CustomerList;
            }
            catch (Exception)
            {
                throw;
            }
        }



       

        //public List<tbl_Invoice> GetAllInvoiceListByCustomer(string Customerid, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

        //        int CustomerID = Convert.ToInt32(Customerid);
        //        //CRM_GetCompanyInvoicesList
        //        List<CRM_GetCompanyInvoicesList_Result> CompanyInoviceList = obj.CRM_GetCompanyInvoicesList(CustomerID, startIndex, pageSize, orderByClause,).ToList();
        //        List<CRM_GET_INVOICELIST_BYCUSID_Result> invoicelst = obj.CRM_GET_INVOICELIST_BYCUSID(CustomerID).ToList();
        //        TotalCount = Convert.ToInt32(CompanyInoviceList.Count);
        //        //TotalCount = Convert.ToInt32(invoicelist.Count);
        //        var InvList = CompanyInoviceList.Select(INVC => new tbl_Invoice
        //        {
        //            InvoiceID = INVC.InvoiceID,
        //            InvoiceNo = INVC.InvoiceNo,
        //            CustomerID = INVC.CustomerID,
        //            Fname = INVC.Name,
        //            //Fname = Con == null ? "" : Con.Fname,
        //            TermsID = INVC.TermsID,
        //            GrandTotal = INVC.GrandTotal,
        //            CreatedDate = INVC.CreatedDate,
        //            DueDate = INVC.DueDate,
        //            Description = INVC.Description,
        //            TaxAmount = INVC.TaxAmount,
        //            TaxPercent = INVC.TaxPercent,
        //            Total = INVC.InvoiceAmount,
        //            DiscountPercent = INVC.DiscountPercent,
        //            Discount = INVC.Discount,
        //            MiscellaneousAmount = INVC.MiscellaneousAmount,
        //            Posted = INVC.Posted,
        //            InvoiceType = INVC.InvoiceType,
        //            BalanceAmount = INVC.BalanceAmount,
        //            InvoiceAmount = INVC.InvoiceAmount

        //        }).ToList();
        //        return InvList;


                


        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}






        public List<tbl_Invoice> GetAllInvoiceListByCustomer(int Customerid, string invoice, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<tbl_Invoice> invoicelist = new List<tbl_Invoice>();
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));

                List<CRM_GetCompanyInvoicesList_Result> CompanyInoviceList = obj.CRM_GetCompanyInvoicesList(Customerid, invoice, startIndex, pageSize, orderByClause, output).ToList();

                //List<CRM_GetAllInvoicesList_Result> ObjInvList = obj.CRM_GetAllInvoicesList(invoice, startIndex, pageSize, orderByClause, output).ToList();
                invoicelist = CompanyInoviceList.Select(Inv => new tbl_Invoice
                {
                    CustomerID = Inv.CustomerID,
                    Fname = Inv.Name,
                    InvoiceID = Inv.InvoiceID.GetValueOrDefault(),
                    InvoiceNo = Inv.InvoiceNo,
                    GrandTotal = Inv.GrandTotal,
                    Active = Inv.Active,
                    DueDate = Inv.DueDate,
                    Posted = Inv.Posted,
                    InvoiceType = Inv.InvoiceType,
                    EstInvoiceID = Inv.EstInvoiceID,
                    EstInvoiceNo = Inv.EstInvoiceNo,
                    BalanceAmount = Inv.BalanceAmount

                }).ToList();


                TotalCount = Convert.ToInt32(CompanyInoviceList.Count);
                return invoicelist;


            }
            catch (Exception)
            {
                throw;
            }
        }
        
        #endregion

    }
}
