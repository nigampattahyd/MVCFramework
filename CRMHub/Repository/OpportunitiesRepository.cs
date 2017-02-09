using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using CRMHub.ViewModel;

namespace CRMHub.Repository
{
    public class OpportunitiesRepository : IOpportunitiesRepository
    {
        public List<Branch> GetBranches(string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
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


        public List<tbl_crm_industry> GetIndustries(string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    List<getCRMIndustry_Result> lstindustries = ClientEntity.getCRMIndustry().ToList();
                    return lstindustries.Select(objindustry => new tbl_crm_industry
                    {
                        Id = objindustry.Id,
                        Industry = objindustry.Industry
                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<usState> GetStates(string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    //List<usState> getStates = ClientEntity.usStates.ToList();
                    //return getStates;
                    List<CRM_GetState_Result> lststates = ClientEntity.CRM_GetState().ToList();
                    return lststates.Select(objstate => new usState
                    {
                        StateCode = objstate.StateCode,
                        StateName = objstate.StateName
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
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
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

        //public List<tbl_account> SelectCompany(string Connectionstring, string dbName)
        //{
        //    DevEntities obj = new DevEntities(Connectionstring);
        //    obj.Database.Connection.Open();
        //    obj.Database.Connection.ChangeDatabase(dbName);
        //    List<CRM_ParentCompany_Result> getParentComp = obj.CRM_ParentCompany().ToList();
        //    return getParentComp.Select(getPComp => new tbl_account
        //    {
        //        AccountId = getPComp.AccountId,
        //        Account_Name = getPComp.Account_Name,
        //        Phone = getPComp.Phone,
        //        MailingAddress = getPComp.mailingAddress,
        //        Status = getPComp.Status
        //    }).ToList();
        //}

        //public List<tbl_Lead> SearchforOpportunity(string filterExpression, string keyword, string Email, string BusinessType, string OpportunityStatus, string OpportunityStage, string Opportunityindustries, string Owner, string Branch, string logInId, string roleId, int? startIndex, int? pagesize, string sorting, out int totalcount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
        //            List<CRM_SearchforOpportunity_Result> listofOpp = ClientEntity.CRM_SearchforOpportunity(filterExpression, keyword, Email, BusinessType, OpportunityStatus, OpportunityStage, Opportunityindustries, Owner, Branch, Convert.ToInt64(logInId), Convert.ToInt16(roleId), startIndex, pagesize, sorting, output).ToList();
        //            var result = listofOpp.Select(selopp => new tbl_Lead
        //            {
        //                LeadId = Convert.ToInt64(selopp.LeadId),
        //                AccountName = selopp.AccountName,
        //                LeadName = selopp.LeadName,
        //                Phone = selopp.Phone,
        //                Email = selopp.Email,
        //                SalesMgr1 = selopp.salesMgr1,
        //                CreatedDate = selopp.createdDate,
        //                ModifiedDate = selopp.modifiedDate,
        //                EstimatedCloseDate = Convert.ToDateTime(selopp.EstimatedCloseDate),
        //                AnnualRevenue = selopp.AnnualRevenue,
        //                Probability = selopp.Probability
        //            }).ToList();
        //            totalcount = Convert.ToInt32(output.Value);
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<tbl_Lead> GetOpportunities()
        //{
        //    try
        //    {
        //        using (var ClientEntity = new DevEntities())
        //        {
        //            List<CRM_GetAllOpportunities_Result> lstopp = ClientEntity.CRM_GetAllOpportunities().ToList();
        //            return lstopp.Select(opportunity => new tbl_Lead
        //            {
        //                LeadId = opportunity.LeadId,
        //                AccountName = opportunity.AccountName,
        //                LeadName = opportunity.LeadName,
        //                Phone = opportunity.Phone,
        //                Email = opportunity.Email,
        //                Ownership = opportunity.Ownership,
        //                CreatedDate = opportunity.CreatedDate,
        //                ModifiedDate = opportunity.ModifiedDate,
        //                EstimatedCloseDate = opportunity.EstimatedCloseDate,
        //                AnnualRevenue = opportunity.AnnualRevenue,
        //                Probability = opportunity.Probability
        //            }).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int InsertOpportunity(tbl_Lead LeadObj, string Connectionstring, string dbName)
        //{
        //    int result = 0;
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            result = ClientEntity.CRM_InsertOpportunity(LeadObj.LeadName, LeadObj.Title, LeadObj.Phone, LeadObj.Fax, LeadObj.Email, LeadObj.Mobile,
        //                 LeadObj.PreferTime, LeadObj.PreferMode, LeadObj.EmailOptOut, Convert.ToString(LeadObj.AccountId), LeadObj.AccountName, LeadObj.Website,
        //                 LeadObj.Ownership, LeadObj.Employees, LeadObj.Industry, LeadObj.AnnualRevenue, LeadObj.Rating, LeadObj.LeadStatus, LeadObj.CurrentApplication,
        //                 LeadObj.MailingAddress, LeadObj.Mailingstreet, LeadObj.Mailingcity, LeadObj.Mailingstate, LeadObj.Mailingzip, LeadObj.Mailingcountry,
        //                 LeadObj.SalesMgr1, LeadObj.SalesMgr2, LeadObj.Office, Convert.ToInt64(LeadObj.CreatedBy),
        //                 LeadObj.ModifiedBy, LeadObj.Description, LeadObj.Source, LeadObj.LeadAssigned, LeadObj.LeadStage, LeadObj.FacebookUsername, LeadObj.TwitterUsername,
        //                 LeadObj.LinkedInUsername, LeadObj.GooglePlusUsername, LeadObj.PinterestUsername, LeadObj.SkypeUsername, LeadObj.EstimatedCloseDate,
        //                 LeadObj.NextStep, LeadObj.Probability, LeadObj.BusinessType);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return result;
        //}

        //public tbl_Lead GetSelectedOpportunity(int LeadId, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var SelOpp = ClientEntity.CRM_GetOpportunitybyLeadId(LeadId).ToList();
        //            var Opportunity = SelOpp.Select(seloppbyId => new tbl_Lead
        //            {
        //                LeadId = seloppbyId.LeadId,
        //                LeadName = seloppbyId.LeadName,
        //                Title = seloppbyId.Title,
        //                Phone = seloppbyId.Phone,
        //                Fax = seloppbyId.Fax,
        //                Email = seloppbyId.Email,
        //                Mobile = seloppbyId.Mobile,
        //                PreferTime = seloppbyId.PreferTime,
        //                PreferMode = seloppbyId.PreferMode,
        //                EmailOptOut = seloppbyId.EmailOptOut,
        //                AccountId = seloppbyId.AccountId,
        //                AccountName = seloppbyId.AccountName,
        //                Website = seloppbyId.Website,
        //                Ownership = seloppbyId.Ownership,
        //                Employees = seloppbyId.Employees,
        //                Industry = seloppbyId.Industry,
        //                AnnualRevenue = seloppbyId.AnnualRevenue,
        //                Rating = seloppbyId.Rating,
        //                LeadStatus = seloppbyId.LeadStatus,
        //                CurrentApplication = seloppbyId.CurrentApplication,
        //                MailingAddress = seloppbyId.mailingAddress,
        //                Mailingstreet = seloppbyId.mailingstreet,
        //                Mailingcity = seloppbyId.mailingcity,
        //                Mailingstate = seloppbyId.mailingstate,
        //                Mailingzip = seloppbyId.mailingzip,
        //                Mailingcountry = seloppbyId.mailingcountry,
        //                SalesMgr1 = seloppbyId.salesMgr1,
        //                SalesMgr2 = seloppbyId.salesMgr2,
        //                Office = seloppbyId.office,
        //                CreatedBy = seloppbyId.CreatedBy,
        //                ModifiedBy = seloppbyId.ModifiedBy,
        //                Description = seloppbyId.description,
        //                IsContact = seloppbyId.IsContact,
        //                Source = seloppbyId.Source,
        //                LeadAssigned = seloppbyId.LeadAssigned,
        //                LeadStage = seloppbyId.LeadStage,
        //                SeachIndex = seloppbyId.SeachIndex,
        //                FacebookUsername = seloppbyId.FacebookUsername,
        //                TwitterUsername = seloppbyId.TwitterUsername,
        //                LinkedInUsername = seloppbyId.LinkedInUsername,
        //                GooglePlusUsername = seloppbyId.GooglePlusUsername,
        //                PinterestUsername = seloppbyId.PinterestUsername,
        //                SkypeUsername = seloppbyId.SkypeUsername,
        //                IsOpportunity = seloppbyId.IsOpportunity,
        //                IsSales = seloppbyId.IsSales,
        //                EstimatedCloseDate = seloppbyId.EstimatedCloseDate,
        //                NextStep = seloppbyId.NextStep,
        //                Probability = seloppbyId.Probability,
        //                BusinessType = seloppbyId.BusinessType
        //            }).ToList();
        //            return Opportunity.SingleOrDefault();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int UpdateOpportunity(tbl_Lead LeadObj, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var result = ClientEntity.CRM_UpdateOpportunity(Convert.ToString(LeadObj.LeadId), LeadObj.LeadName, LeadObj.Title, LeadObj.Phone, LeadObj.Fax, LeadObj.Email, LeadObj.Mobile,
        //                 LeadObj.PreferTime, LeadObj.PreferMode, LeadObj.EmailOptOut, Convert.ToString(LeadObj.AccountId), LeadObj.AccountName, LeadObj.Website,
        //                 LeadObj.Ownership, LeadObj.Employees, LeadObj.Industry, LeadObj.AnnualRevenue, LeadObj.Rating, LeadObj.LeadStatus, LeadObj.CurrentApplication,
        //                 LeadObj.MailingAddress, LeadObj.Mailingstreet, LeadObj.Mailingcity, LeadObj.Mailingstate, LeadObj.Mailingzip, LeadObj.Mailingcountry,
        //                 LeadObj.SalesMgr1, LeadObj.SalesMgr2, LeadObj.Office, LeadObj.ModifiedBy, LeadObj.Description, LeadObj.Source,
        //                 LeadObj.LeadAssigned, LeadObj.LeadStage, LeadObj.FacebookUsername, LeadObj.TwitterUsername, LeadObj.LinkedInUsername, LeadObj.GooglePlusUsername,
        //                 LeadObj.PinterestUsername, LeadObj.SkypeUsername, LeadObj.EstimatedCloseDate, LeadObj.NextStep, LeadObj.Probability, LeadObj.BusinessType);
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int ConvertToSales(int ConvOpp, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var result = ClientEntity.CRM_ConvertOpportunityToSale(ConvOpp);
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //public int DeleteOpportunity(int DelOpp, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var result = ClientEntity.CRM_DeleteSelectedOpportunity(DelOpp);
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //public bool IsTitleExists(tbl_Lead LeadObj, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var OppObjList = ClientEntity.tbl_Lead.ToList();
        //            // tbl_Lead OppObj1 = OppObjList.Where(m => m.LeadId != LeadObj.LeadId && m.LeadName.ToLower() == LeadObj.LeadName.ToLower()).FirstOrDefault();
        //            tbl_Lead OppObj1 = ClientEntity.tbl_Lead.Where(m => m.LeadName.ToLower() == LeadObj.LeadName.ToLower() && m.IsOpportunity == true).FirstOrDefault();
        //            if (OppObj1 != null)
        //                return false;
        //            else
        //                return true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public List<activity_types> GetOppActivityType(string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);
            List<CRM_GetLeadActivityType_Result> getOppActivities = obj.CRM_GetLeadActivityType().ToList();
            return getOppActivities.Select(getOpportunityActivities => new activity_types
            {
                Name = getOpportunityActivities.name,
                Aid = getOpportunityActivities.aid

            }).ToList();
        }

        //public List<tbl_Leadhistory> GetOpportunityActivities(string LeadId, string Keyword, string historyType, string Priority, string Status, int StartIndex, int PageSize, string Sorting, out int TotalCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
        //        List<CRM_SearchforOpportunityActivities_Result> lstOppActivityRlt = obj.CRM_SearchforOpportunityActivities(LeadId, Keyword, historyType, Priority, Status, StartIndex, PageSize, Sorting, output).ToList();
        //        TotalCount = Convert.ToInt32(output.Value);
        //        List<tbl_Leadhistory> listOpportunitiesActivity = lstOppActivityRlt.Select(oppActivity => new tbl_Leadhistory
        //        {
        //            LeadhistoryId = Convert.ToInt64(oppActivity.LeadhistoryId),
        //            Leadid = oppActivity.Leadid,
        //            ActCreatedBy = oppActivity.createdBy,
        //            CreatedDate = oppActivity.createdDate,
        //            StartDate = oppActivity.startDate,
        //            HistoryType = oppActivity.HistoryType,
        //            AccountName = oppActivity.AccountName,
        //            Status = oppActivity.status,
        //            Details = oppActivity.details,
        //            ActivityAttachment = oppActivity.ActivityAttachment
        //        }).ToList();
        //        return listOpportunitiesActivity;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public string CreateOpportunityActivity(tbl_Leadhistory leadhistoryObj, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var result = ClientEntity.CRM_InsertUpdateOpportunityActivity(leadhistoryObj.LeadhistoryId, leadhistoryObj.HistoryType, leadhistoryObj.StartTime,
        //                 leadhistoryObj.Endtime, leadhistoryObj.Status, leadhistoryObj.Details, Convert.ToString(leadhistoryObj.CreatedBy), leadhistoryObj.EventType, leadhistoryObj.Leadid,
        //                 leadhistoryObj.StartDate, leadhistoryObj.Priority, leadhistoryObj.RemindDate, leadhistoryObj.RemindTime, leadhistoryObj.CompletedDate, leadhistoryObj.AssignedTo,
        //                 Convert.ToString(leadhistoryObj.AccountId), leadhistoryObj.AccountName, leadhistoryObj.NotificationFlag, leadhistoryObj.ActivityAttachment, Convert.ToString(leadhistoryObj.CreatedDate));
        //            return result.ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public tbl_Leadhistory GetOpportunityActivityById(int LeadhistoryId, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var OppAct = ClientEntity.CRM_GetOppActivitybyId(LeadhistoryId).ToList();
        //            var OpportunityActivity = OppAct.Select(seloppactbyId => new tbl_Leadhistory
        //            {
        //                LeadhistoryId = seloppactbyId.LeadhistoryId,
        //                Leadid = seloppactbyId.Leadid,
        //                CreatedBy = seloppactbyId.CreatedBy,
        //                StartTime = seloppactbyId.startTime,
        //                Endtime = seloppactbyId.endtime,
        //                RemindTime = seloppactbyId.remindTime,
        //                RemindDate = seloppactbyId.remindDate,
        //                AssignedTo = seloppactbyId.assignedTo,
        //                AccountId = seloppactbyId.AccountId,
        //                CreatedDate = seloppactbyId.createdDate,
        //                StartDate = seloppactbyId.startDate,
        //                HistoryType = seloppactbyId.HistoryType,
        //                AccountName = seloppactbyId.AccountName,
        //                Status = seloppactbyId.status,
        //                Details = seloppactbyId.details,
        //                EventType = seloppactbyId.eventType,
        //                ModifiedDate = seloppactbyId.modifiedDate,
        //                Priority = seloppactbyId.priority,
        //                CompletedDate = seloppactbyId.completedDate,
        //                ActivityFlag = seloppactbyId.ActivityFlag,
        //                ActivityAttachment = seloppactbyId.ActivityAttachment
        //            }).ToList();
        //            return OpportunityActivity.SingleOrDefault();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public string UpdateOpportunityActivity(tbl_Leadhistory leadhistoryObj, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var result = ClientEntity.CRM_InsertUpdateOpportunityActivity(leadhistoryObj.LeadhistoryId, leadhistoryObj.HistoryType, leadhistoryObj.StartTime,
        //                 leadhistoryObj.Endtime, leadhistoryObj.Status, leadhistoryObj.Details, Convert.ToString(leadhistoryObj.CreatedBy), leadhistoryObj.EventType, leadhistoryObj.Leadid,
        //                 leadhistoryObj.StartDate, leadhistoryObj.Priority, leadhistoryObj.RemindDate, leadhistoryObj.RemindTime, leadhistoryObj.CompletedDate, leadhistoryObj.AssignedTo,
        //                 Convert.ToString(leadhistoryObj.AccountId), leadhistoryObj.AccountName, leadhistoryObj.NotificationFlag, leadhistoryObj.ActivityAttachment, Convert.ToString(leadhistoryObj.CreatedDate));
        //            return result.ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int DeleteSelectedOppActivity(int DelOppAct, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);

        //            var result = ClientEntity.CRM_DeleteSelOpportunityActivity(DelOppAct);
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<tbl_Lead> GetOpportunityLookUpList(string keyword, string leadOwner, string office, string logInId, string roleId, string orderByClause, string Connectionstring, string dbName)
        //{
        //    DevEntities obj = new DevEntities(Connectionstring);
        //    obj.Database.Connection.Open();
        //    obj.Database.Connection.ChangeDatabase(dbName);

        //    List<CRM_OpportunityList_Result> GetOpporLst = obj.CRM_OpportunityList(keyword, leadOwner, office, Convert.ToInt32(logInId), Convert.ToInt32(roleId), orderByClause).ToList();

        //    //List<CRM_OpportunityLookup_Result> GetOpporLst = obj.CRM_OpportunityLookup(keyword, leadOwner, office, Convert.ToInt32(logInId), Convert.ToInt32(roleId), orderByClause).ToList();

        //    return GetOpporLst.Select(OpprLst => new tbl_Lead
        //    {
        //        LeadId = OpprLst.LeadId ?? 0,
        //        LeadName = OpprLst.LeadName,
        //        AccountId = OpprLst.AccountId,
        //        AccountName = OpprLst.AccountName,
        //        LeadStatus = OpprLst.LeadStatus,

        //    }).ToList();
        //}

        //public bool CompleteOpprtunityActivity(int leadhistoryid, string Connectionstring, string dbName)
        //{
        //    DevEntities obj = new DevEntities(Connectionstring);
        //    obj.Database.Connection.Open();
        //    obj.Database.Connection.ChangeDatabase(dbName);

        //    var CompActFields = (from CmpActList in obj.tbl_Leadhistory
        //                         where CmpActList.LeadhistoryId == leadhistoryid
        //                         select CmpActList).FirstOrDefault();

        //    if (CompActFields != null)
        //    {
        //        CompActFields.Status = "Completed";
        //    }

        //    obj.SaveChanges();
        //    return true;
        //}
        //public List<tbl_Lead> SelectOpportunity(string Connectionstring, string dbName)
        //{
        //    DevEntities obj = new DevEntities(Connectionstring);
        //    obj.Database.Connection.Open();
        //    obj.Database.Connection.ChangeDatabase(dbName);
        //    List<CRM_ParentOpportunity_Result> getParentopp = obj.CRM_ParentOpportunity().ToList();
        //    return getParentopp.Select(getPopp => new tbl_Lead
        //    {
        //        LeadId = getPopp.LeadId,
        //        LeadName = getPopp.LeadName,
        //        AccountId = getPopp.AccountId,
        //        AccountName = getPopp.AccountName,
        //    }).ToList();
        //}
        //public int CreateOpportunityActivity_Insert(tbl_Leadhistory leadhistoryObj, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            int res = 0;
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var output = new System.Data.Entity.Core.Objects.ObjectParameter("OpotunityHisId", typeof(int));
        //            res = ClientEntity.CRM_Insert_OpportunityActivity(leadhistoryObj.LeadhistoryId, leadhistoryObj.HistoryType, leadhistoryObj.StartTime,
        //                 leadhistoryObj.Endtime, leadhistoryObj.Status, leadhistoryObj.Details, Convert.ToString(leadhistoryObj.CreatedBy), leadhistoryObj.EventType, leadhistoryObj.Leadid,
        //                 leadhistoryObj.StartDate, leadhistoryObj.Priority, leadhistoryObj.RemindDate, leadhistoryObj.RemindTime, leadhistoryObj.CompletedDate, leadhistoryObj.AssignedTo,
        //                 Convert.ToString(leadhistoryObj.AccountId), leadhistoryObj.AccountName, leadhistoryObj.NotificationFlag, leadhistoryObj.ActivityAttachment, Convert.ToString(leadhistoryObj.CreatedDate), output);
        //            res = Convert.ToInt32(output.Value);
        //            return res;
        //            //var result = ClientEntity.CRM_InsertUpdateOpportunityActivity(leadhistoryObj.LeadhistoryId, leadhistoryObj.HistoryType, leadhistoryObj.StartTime,
        //            //     leadhistoryObj.Endtime, leadhistoryObj.Status, leadhistoryObj.Details, Convert.ToString(leadhistoryObj.CreatedBy), leadhistoryObj.EventType, leadhistoryObj.Leadid,
        //            //     leadhistoryObj.StartDate, leadhistoryObj.Priority, leadhistoryObj.RemindDate, leadhistoryObj.RemindTime, leadhistoryObj.CompletedDate, leadhistoryObj.AssignedTo,
        //            //     Convert.ToString(leadhistoryObj.AccountId), leadhistoryObj.AccountName, leadhistoryObj.NotificationFlag, leadhistoryObj.ActivityAttachment, Convert.ToString(leadhistoryObj.CreatedDate));
        //            //return result.ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        /// Zoho Model Code
        //public List<Opportunity> GetAllopportunities(int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

        //        List<CRM_GetAllOpportunitiesList_Result> OpporLst = dbContext.CRM_GetAllOpportunitiesList(jtStartIndex, jtPageSize, jtSorting, output).ToList();

        //        var Opportunitylist = OpporLst.Select(OPP => new Opportunity
        //        {
        //            ID = OPP.opporID.GetValueOrDefault(),
        //            Name = OPP.OpporName,
        //            NextStep = OPP.NextStep,
        //            CloseDate = OPP.closedate,
        //            CreatedDate = OPP.createdDate,
        //            ModifiedDate= OPP.modifiedDate,
        //            ContactID = OPP.ContactID,
        //            CompanyID= OPP.CompanyID,
        //            Probability = OPP.Probability,
        //            EstimateBudget = OPP.EstimateBudget,
        //            StatusID =OPP.StatusID,
        //            StageID = OPP.StageID,
        //            BusinessTypeID = OPP.BusinessTypeID,
        //            ExpectedRevenue = OPP.ExpectedRevenue,
        //            Description = OPP.Description,
        //            CreatedBy = OPP.CreatedBy,
        //            ModifiedBy = OPP.ModifiedBy,
        //            ContactName = OPP.ContactFirstName,
        //            CompanyName = OPP.CompanyName,
        //            //Owner = OPP.OwnerfirstName,
        //            OwnerFirstName = OPP.OwnerfirstName,
        //            OwnerLastName = OPP.ownerlastname,
                    
        //            OwnerID = OPP.OwnerID,
        //            OppurtunitySourceID = OPP.OppurtunitySourceID

        //        }).ToList();
        //        RecordCount = Convert.ToInt32(Opportunitylist.Count);

        //        //List<CRM_GetAllEstimateInvoicesList_Result> ObjEstInvList = obj.CRM_GetAllEstimateInvoicesList(startIndex, pageSize, orderByClause, output).ToList();

        //        //List<Opportunity> Opplst = dbContext.Opportunities.ToList();
        //        //RecordCount = Opplst.Count;
        //        return Opportunitylist;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}



        //public bool CreateOpprtunity(Opportunity objOpp, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        dbContext.Opportunities.Add(objOpp);
        //        dbContext.SaveChanges();
        //        return true; ;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        //public Opportunity GetAOpportunityDetailsByID(int id, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);

        //        var result = dbContext.CRM_OpportunityDetailsByID(id).Select(Oppor => new Opportunity
        //        {
        //            ID = Oppor.ID,
        //            Name = Oppor.Name,
        //            CompanyID = Oppor.CompanyID,
        //            ContactID = Oppor.ContactID,
        //            EstimateBudget = Oppor.EstimateBudget,
        //            CloseDate = Oppor.CloseDate,
        //            StageID = Oppor.StageID,
        //            BusinessTypeID = Oppor.BusinessTypeID,
        //            NextStep = Oppor.NextStep,
        //            StatusID = Oppor.StatusID,
        //            Probability = Oppor.Probability,
        //            ExpectedRevenue = Oppor.ExpectedRevenue,
        //            OppurtunitySourceID = Oppor.OppurtunitySourceID,
        //            Description = Oppor.Description,
        //            CreatedBy = Oppor.CreatedBy,
        //            CreatedDate = Oppor.CreatedDate,
        //            ModifiedBy = Oppor.ModifiedBy,
        //            ModifiedDate = Oppor.ModifiedDate,
        //            OwnerID = Oppor.OwnerID,
        //            Owner = Oppor.OwnerfirstName,
        //            CompanyName = Oppor.CompanyName,
        //            ContactName = Oppor.ContactFirstName

        //        }).SingleOrDefault();



        //        return result;


        //        //Opportunity OpprObj = dbContext.Opportunities.Where(o => o.ID == id).SingleOrDefault();
        //        //return OpprObj;





        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}



        //public int updateopportunityDetailsByID(Opportunity objoppormodel, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        Opportunity GetopporDetails = dbContext.Opportunities.Where(o => o.ID == objoppormodel.ID).SingleOrDefault();

        //        GetopporDetails.Name = objoppormodel.Name;
        //        GetopporDetails.CompanyID = objoppormodel.CompanyID;
        //        GetopporDetails.ContactID = objoppormodel.ContactID;
        //        GetopporDetails.EstimateBudget = objoppormodel.EstimateBudget;
        //        GetopporDetails.CloseDate = objoppormodel.CloseDate;
        //        GetopporDetails.StageID = objoppormodel.StageID;
        //        GetopporDetails.BusinessTypeID = objoppormodel.BusinessTypeID;
        //        GetopporDetails.NextStep = objoppormodel.NextStep;
        //        GetopporDetails.StatusID = objoppormodel.StatusID;
        //        GetopporDetails.Probability = objoppormodel.Probability;
        //        GetopporDetails.ExpectedRevenue = objoppormodel.ExpectedRevenue;
        //        GetopporDetails.OppurtunitySourceID = objoppormodel.OppurtunitySourceID;
        //        GetopporDetails.Description = objoppormodel.Description;
        //        GetopporDetails.CreatedBy = objoppormodel.CreatedBy;
        //        GetopporDetails.CreatedDate = objoppormodel.CreatedDate;
        //        GetopporDetails.ModifiedBy = objoppormodel.ModifiedBy;
        //        GetopporDetails.ModifiedDate = objoppormodel.ModifiedDate;
        //        GetopporDetails.OwnerID = objoppormodel.OwnerID;
        //        dbContext.SaveChanges();

        //        return 1;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        //public bool DeleteOpportunityDetails(int Opporid, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        Opportunity ObjOppor = obj.Opportunities.Where(o => o.ID == Opporid).SingleOrDefault();
        //        obj.Opportunities.Remove(ObjOppor);
        //        obj.SaveChanges();
        //        return true;


        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        public List<Account> GetAllopportunities(string Keyword, int Companyid, int ownerid,int stageId, int jtStartIndex, int jtPageSize, string jtSorting, out int totalcount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
               // var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                List<CRM_GetAllAccountOpportunities_Result> OpporLst = dbContext.CRM_GetAllAccountOpportunities(Keyword, Companyid, ownerid, stageId,jtStartIndex, jtPageSize, jtSorting, output).ToList();

               

                var Opportunitylist = OpporLst.Select(OPP => new Account
                {
                    ID = OPP.opporID.GetValueOrDefault(),
                    Name = OPP.OpporName,
                    NextStep = OPP.NextStep,
                    CloseDate = OPP.closedate,
                    CreatedDate = OPP.createdDate,
                    ModifiedDate = OPP.modifiedDate,
                    ContactID = OPP.ContactID,
                    CompanyID = OPP.CompanyID,
                    Probability = OPP.Probability,
                    EstimateBudget = OPP.EstimateBudget,
                   // StatusID = OPP.StatusID,
                    StageID = OPP.StageID,
                    BusinessTypeID = OPP.BusinessTypeID,
                    ExpectedRevenue = OPP.ExpectedRevenue,
                    Description = OPP.Description,
                    CreatedBy = OPP.CreatedBy,
                    ModifiedBy = OPP.ModifiedBy,
                    ContactName = OPP.ContactFirstName,
                    CompanyName = OPP.CompanyName,
                    Owner = OPP.OwnerfirstName,
                    OwnerFirstName = OPP.OwnerfirstName,
                    OwnerLastName = OPP.ownerlastname,

                    OwnerID = OPP.OwnerID,
                    OppurtunitySourceID = OPP.OppurtunitySourceID

                }).ToList();
                totalcount = Convert.ToInt32(output.Value);
               // RecordCount = Convert.ToInt32(Opportunitylist.Count);               
                return Opportunitylist;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public Account GetAOpportunityDetailsByID(int id, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);

                CRM_AccountOpportunityDetailsByID_Result AccRes = dbContext.CRM_AccountOpportunityDetailsByID(id).SingleOrDefault();

                //Account AccObj = dbContext.CRM_AccountOpportunityDetailsByID(id).SingleOrDefault();


                var result = dbContext.CRM_AccountOpportunityDetailsByID(id).Select(Oppor => new Account
                    {
                        ID = Oppor.ID,
                        Name = Oppor.Name,
                        CompanyID = Oppor.CompanyID,
                        ContactID = Oppor.ContactID,
                        EstimateBudget = Oppor.EstimateBudget,
                        CloseDate = Oppor.CloseDate,
                        StageID = Oppor.StageID,
                        BusinessTypeID = Oppor.BusinessTypeID,
                        NextStep = Oppor.NextStep,
                        //StatusID = Oppor.StatusID,
                        Probability = Oppor.Probability,
                        ExpectedRevenue = Oppor.ExpectedRevenue,
                        OppurtunitySourceID = Oppor.OppurtunitySourceID,
                        Description = Oppor.Description,
                        CreatedBy = Oppor.CreatedBy,
                        CreatedDate = Oppor.CreatedDate,
                        ModifiedBy = Oppor.ModifiedBy,
                        ModifiedDate = Oppor.ModifiedDate,
                        OwnerID = Oppor.OwnerID,
                        Owner = Oppor.OwnerfirstName +" "+Oppor.ownerlastname,
                        CompanyName = Oppor.CompanyName,

                       // ContactName = Oppor.ConFirstName ,
                        ContactName = Oppor.ConFirstName + " " + Oppor.ConLastName,
                        AccountTypeId = Oppor.AccountTypeId
                    }).SingleOrDefault();

                //var result = dbContext.CRM_OpportunityDetailsByID(id).Select(Oppor => new Opportunity
                //{
                //    ID = Oppor.ID,
                //    Name = Oppor.Name,
                //    CompanyID = Oppor.CompanyID,
                //    ContactID = Oppor.ContactID,
                //    EstimateBudget = Oppor.EstimateBudget,
                //    CloseDate = Oppor.CloseDate,
                //    StageID = Oppor.StageID,
                //    BusinessTypeID = Oppor.BusinessTypeID,
                //    NextStep = Oppor.NextStep,
                //    StatusID = Oppor.StatusID,
                //    Probability = Oppor.Probability,
                //    ExpectedRevenue = Oppor.ExpectedRevenue,
                //    OppurtunitySourceID = Oppor.OppurtunitySourceID,
                //    Description = Oppor.Description,
                //    CreatedBy = Oppor.CreatedBy,
                //    CreatedDate = Oppor.CreatedDate,
                //    ModifiedBy = Oppor.ModifiedBy,
                //    ModifiedDate = Oppor.ModifiedDate,
                //    OwnerID = Oppor.OwnerID,
                //    Owner = Oppor.OwnerfirstName,
                //    CompanyName = Oppor.CompanyName,
                //    ContactName = Oppor.ContactFirstName

                //}).SingleOrDefault();



                return result;


                //Opportunity OpprObj = dbContext.Opportunities.Where(o => o.ID == id).SingleOrDefault();
                //return OpprObj;





            }
            catch (Exception)
            {
                throw;
            }

        }


        public int updateopportunityDetailsByID(Account objoppormodel, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);

                Account GetopporDetails = dbContext.Accounts.Where(o => o.ID == objoppormodel.ID).SingleOrDefault();
                //Opportunity GetopporDetails = dbContext.Opportunities.Where(o => o.ID == objoppormodel.ID).SingleOrDefault();

                GetopporDetails.Name = objoppormodel.Name;
                GetopporDetails.CompanyID = objoppormodel.CompanyID;
                GetopporDetails.ContactID = objoppormodel.ContactID;
                GetopporDetails.EstimateBudget = objoppormodel.EstimateBudget;
                GetopporDetails.CloseDate = objoppormodel.CloseDate;
                GetopporDetails.StageID = objoppormodel.StageID;
                GetopporDetails.BusinessTypeID = objoppormodel.BusinessTypeID;
                GetopporDetails.NextStep = objoppormodel.NextStep;
                //GetopporDetails.StatusID = objoppormodel.StatusID;
                GetopporDetails.Probability = objoppormodel.Probability;
                GetopporDetails.ExpectedRevenue = objoppormodel.ExpectedRevenue;
                GetopporDetails.OppurtunitySourceID = objoppormodel.OppurtunitySourceID;
                GetopporDetails.Description = objoppormodel.Description;
                GetopporDetails.CreatedBy = objoppormodel.CreatedBy;
                GetopporDetails.CreatedDate = objoppormodel.CreatedDate;
                GetopporDetails.ModifiedBy = objoppormodel.ModifiedBy;
                GetopporDetails.ModifiedDate = objoppormodel.ModifiedDate;
                GetopporDetails.OwnerID = objoppormodel.OwnerID;
                GetopporDetails.FirstName = objoppormodel.FirstName;
                GetopporDetails.LastName = objoppormodel.LastName;
                dbContext.SaveChanges();

                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
    
    
    }
}
