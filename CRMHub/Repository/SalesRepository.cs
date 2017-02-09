using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;
using CRMHub.EntityFramework;

namespace CRMHub.Repository
{
    public class SalesRepository : ISalesRepository
    {
        public List<Branch> GetBranches(string Connectionstring, string dbName)
        {
            try
            {
                using (CRMDevEntities ClientEntity = new CRMDevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    List<CRM_GetBranchListAll_Result> lstbranches = ClientEntity.CRM_GetBranchListAll().ToList();
                    return lstbranches.Select(objbranches => new Branch
                    {
                        BranchId = objbranches.branchId,
                        BranchName = objbranches.branchName
                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<user> GetUsers(string Connectionstring, string dbName)
        {
            try
            {
                using (CRMDevEntities ClientEntity = new CRMDevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    List<CRM_getAdmin_Result> lstUsers = ClientEntity.CRM_getAdmin().ToList();
                    return lstUsers.Select(objuser => new user
                    {
                        UserId = objuser.userId,
                        FirstName = objuser.first_Name
                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<tbl_account> SelectCompany(string Connectionstring, string dbName)
        {
            try
            {
                CRMDevEntities obj = new CRMDevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<CRM_ParentCompany_Result> getParentComp = obj.CRM_ParentCompany().ToList();
                return getParentComp.Select(getPComp => new tbl_account
                {
                    AccountId = getPComp.AccountId,
                    Account_Name = getPComp.Account_Name,
                    Phone = getPComp.Phone,
                    MailingAddress = getPComp.mailingAddress,
                    Status = getPComp.Status
                }).ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<tbl_Sales> SearchforSale(string filterExpression, string keyword, string Email, string salesRep, string Branch, string logInId, string roleId, int? startIndex, int? pagesize, string sorting, out int TotalRecordsCount, string Connectionstring, string dbName)
        {
            try
            {
                using (CRMDevEntities ClientEntity = new CRMDevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                    List<CRM_SearchforSale_Result> listofsales = ClientEntity.CRM_SearchforSale(filterExpression, keyword, Email, salesRep, Branch, Convert.ToInt64(logInId), Convert.ToInt16(roleId), startIndex, pagesize, sorting, output).ToList();
                    var result = listofsales.Select(selsale => new tbl_Sales
                    {
                        SalesId = Convert.ToInt64(selsale.SalesId),
                        AccountName = selsale.AccountName,
                        SalesName = selsale.SalesName,
                        BillingContact = selsale.BillingContact,
                        Email = selsale.Email,
                        SaleCreatedOn=selsale.SaleCreatedOn
                    }).ToList();
                    TotalRecordsCount = Convert.ToInt32(output.Value);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<tbl_Sales> GetAllSales()
        {
            try
            {
                using (var ClientEntity = new CRMDevEntities())
                {
                    List<CRM_GetAllSales_Result> lstsale = ClientEntity.CRM_GetAllSales().ToList();
                    return lstsale.Select(saleobj => new tbl_Sales
                    {
                        SalesId = saleobj.SalesId,
                        AccountId= saleobj.AccountId,
                        AccountName = saleobj.AccountName,
                        SalesName = saleobj.SalesName,
                        BillingContact = saleobj.BillingContact,
                        Email = saleobj.Email,
                        Office= saleobj.Office,
                        SaleCreatedOn = saleobj.SaleCreatedOn
                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public int AddSales(tbl_Sales salesObj, string Connectionstring, string dbName)
        {
           
            try
            {

                CRMDevEntities dbContext = new CRMDevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                var result = dbContext.CRM_InsertSales(salesObj.SalesName, salesObj.AccountId, salesObj.AccountName, salesObj.Contact, salesObj.Email, salesObj.BillingContact,
                        salesObj.BillingEmail, salesObj.Website, salesObj.Title, salesObj.BillingAddress, salesObj.SalesRep, salesObj.SalesRepPhone, salesObj.SalesSource,
                        salesObj.SalesAmount, salesObj.Revenue, salesObj.PurchaseOrder, salesObj.SalesCloseDate, salesObj.Description, salesObj.PaymentTerms, salesObj.GM,
                        salesObj.GMPercentage, salesObj.IsAccounting, salesObj.SalesStatus, salesObj.SaleCreatedBy, salesObj.SaleCreatedOn,
                        salesObj.Office, salesObj.OpportunityId).FirstOrDefault()??-1;

               

                //CRM_InserSales - Proc

                return result;


              
            }
            catch (Exception)
            {
                throw;
            }
          
        }
        //public int AddSales(tbl_Sales salesObj, string Connectionstring, string dbName)
        //{
        //    int result = 0;
        //    try
        //    {
        //        using (CRMDevEntities ClientEntity = new CRMDevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            result = ClientEntity.CRM_InsertSales(salesObj.SalesName, salesObj.AccountId,salesObj.AccountName, salesObj.Contact, salesObj.Email, salesObj.BillingContact,
        //                salesObj.BillingEmail, salesObj.Website, salesObj.Title, salesObj.BillingAddress, salesObj.SalesRep, salesObj.SalesRepPhone, salesObj.SalesSource,
        //                salesObj.SalesAmount, salesObj.Revenue, salesObj.PurchaseOrder, salesObj.SalesCloseDate, salesObj.Description, salesObj.PaymentTerms, salesObj.GM,
        //                salesObj.GMPercentage, salesObj.IsAccounting, salesObj.SalesStatus, salesObj.SaleCreatedBy, salesObj.SaleCreatedOn, salesObj.Office,salesObj.OpportunityId);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return result;
        //}

        public tbl_Sales GetSelectedSale(int SalesId, string Connectionstring, string dbName)
        {
            try
            {
                using (CRMDevEntities ClientEntity = new CRMDevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var Selsale = ClientEntity.CRM_GetSalebySalesId(SalesId).ToList();
                    var sale = Selsale.Select(selsalebyId => new tbl_Sales
                    {
                        SalesId = selsalebyId.SalesId,
                        OpportunityId = selsalebyId.OpportunityId,
                        SalesName = selsalebyId.SalesName,
                        AccountId=selsalebyId.AccountId,
                        AccountName = selsalebyId.AccountName,
                        Contact = selsalebyId.Contact,
                        Email = selsalebyId.Email,
                        BillingContact = selsalebyId.BillingContact,
                        BillingEmail = selsalebyId.BillingEmail,
                        Website = selsalebyId.Website,
                        Title = selsalebyId.Title,
                        BillingAddress = selsalebyId.BillingAddress,
                        SalesRep = selsalebyId.SalesRep,
                        SalesRepPhone = selsalebyId.SalesRepPhone,
                        SalesSource = selsalebyId.SalesSource,
                        SalesAmount = selsalebyId.SalesAmount,
                        Revenue = selsalebyId.Revenue,
                        PurchaseOrder = selsalebyId.PurchaseOrder,
                        SalesCloseDate = selsalebyId.SalesCloseDate,
                        Description = selsalebyId.Description,
                        PaymentTerms = selsalebyId.PaymentTerms,
                        Office = selsalebyId.Office,
                        GM = selsalebyId.GM,
                        GMPercentage = selsalebyId.GMPercentage,
                        IsAccounting = selsalebyId.IsAccounting,
                        SalesStatus = selsalebyId.SalesStatus,
                        SaleCreatedBy = selsalebyId.SaleCreatedBy,
                        SaleCreatedOn = selsalebyId.SaleCreatedOn,
                        SaleModifiedBy = selsalebyId.SaleModifiedBy,
                        SaleModifiedOn = selsalebyId.SaleModifiedOn
                    }).ToList();
                    return sale.SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateSales(tbl_Sales SalesObj, string Connectionstring, string dbName)
        {
            try
            {
                using (CRMDevEntities ClientEntity = new CRMDevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var result = ClientEntity.CRM_UpdateSales(SalesObj.SalesId, SalesObj.SalesName, SalesObj.AccountId, SalesObj.AccountName, SalesObj.Contact, SalesObj.Email,
                        SalesObj.BillingContact, SalesObj.BillingEmail, SalesObj.Website, SalesObj.Title, SalesObj.BillingAddress, SalesObj.SalesRep,
                        SalesObj.SalesRepPhone,SalesObj.SalesSource, SalesObj.SalesAmount, SalesObj.Revenue, SalesObj.PurchaseOrder, SalesObj.SalesCloseDate, 
                        SalesObj.Description, SalesObj.PaymentTerms,SalesObj.GM, SalesObj.GMPercentage, SalesObj.IsAccounting, SalesObj.SaleModifiedBy, 
                        SalesObj.SaleModifiedOn, SalesObj.Office);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteSale(int DelSale, string Connectionstring, string dbName)
        {
            try
            {
                using (CRMDevEntities ClientEntity = new CRMDevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var result = ClientEntity.CRM_DeleteSelectedSale(DelSale);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsTitleExists(tbl_Sales SalesObj, string Connectionstring, string dbName)
        {
            try
            {
                using (CRMDevEntities ClientEntity = new CRMDevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var SalesObjList = ClientEntity.tbl_Sales.ToList();
                    tbl_Sales SalesObj1 = SalesObjList.Where(m => m.SalesId != SalesObj.SalesId && m.SalesName.ToLower() == SalesObj.SalesName.ToLower()).FirstOrDefault();
                    if (SalesObj1 != null)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Custom_Manage_Master> GetCustomFieldsByModule(string Module, string Connectionstring, string dbName)
        {
            try
            {
                var obj = new CRMDevEntities();
                List<Custom_Manage_Master> CustomFieldsList = obj.Custom_Manage_Master.Where(CMM => CMM.Module == Module).ToList();
                return CustomFieldsList;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
