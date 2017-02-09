using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRMHub.Repository
{
    public class LeadRepository : ILeadRepository
    {
        //public List<tbl_Lead> getAllLeads(string filterExpression, string Keyword,string LeadStatus, string email,string LeadIndustry, string leadOwner, string office, int loginId, int roleId, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
        //        List<CRM_GET_ALL_LEADS_Result> lstLeadResult = dbContext.CRM_GET_ALL_LEADS(filterExpression, Keyword,LeadStatus, email,LeadIndustry, leadOwner, office, loginId, roleId, jtStartIndex, jtPageSize, jtSorting, output).ToList();
        //        RecordCount = Convert.ToInt32(output.Value);
        //        return lstLeadResult.Select(objLead => new tbl_Lead
        //        {
        //            LeadId =Convert.ToInt64(objLead.LeadId),
        //            LeadName = objLead.LeadName,
        //            Title = objLead.Title,
        //            Phone = objLead.Phone,
        //            Fax = objLead.Fax,
        //            Email = objLead.Email,
        //            Mobile = objLead.Mobile,
        //            PreferTime =objLead.PreferTime,
        //            PreferMode =objLead.PreferMode,
        //            AccountId = objLead.AccountId,
        //            CreatedDate = objLead.createdDate,
        //            ModifiedDate = objLead.modifiedDate,
        //            SalesMgr1 = objLead.salesMgr1,
        //            SalesMgr2 = objLead.salesMgr2,
        //            Office = objLead.office,
        //            IsContact = objLead.IsContact,
        //            FacebookUsername = objLead.FacebookUsername,
        //            TwitterUsername = objLead.TwitterUsername,
        //            LinkedInUsername = objLead.LinkedInUsername,
        //            GooglePlusUsername = objLead.GooglePlusUsername,
        //            PinterestUsername = objLead.PinterestUsername,
        //            SkypeUsername = objLead.SkypeUsername,
        //            LeadStatus = objLead.LeadStatus,
        //            AccountName=objLead.AccountName,
        //            Source = objLead.Source
        //        }).ToList();
        //    }
        //    catch (Exception)
        //    {                
        //        throw;
        //    }
        //}

        //public List<OutlookMail> getLeadEmails(string type, string email, string subject, string body, string loginId, string userId) 
        //{
        //    try
        //    {
        //        var dbContext = new DevEntities();
        //        List<CRM_GET_LEAD_MAILS_Result> listGetResult = dbContext.CRM_GET_LEAD_MAILS(type, email, subject, body, loginId, userId).ToList();
        //        return listGetResult.Select(objLstRslt => new OutlookMail { ID =  Convert.ToInt64(objLstRslt.ID), CreatedBy = objLstRslt.createdby, Sender = objLstRslt.Sender, Recipient = objLstRslt.recipient, Subject = objLstRslt.subject, Date = objLstRslt.date, testdate = objLstRslt.testdate, Type = objLstRslt.Type, Body = objLstRslt.Body }).ToList();
        //    }
        //    catch (Exception)
        //    {                
        //        throw;
        //    }
        //}

        //public List<tbl_Leadhistory> GetCrmLeadActivities(string LeadId, string Keyword, string Type, string Priority, string Status, int StartIndex, int PageSize, string Sorting, out int TotalCount, string Connectionstring, string dbName) 
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
        //        List<CRM_SearchforLeadActivities_Result> lstGetLeadResult = dbContext.CRM_SearchforLeadActivities(LeadId, Keyword, Type, Priority, Status, StartIndex, PageSize, Sorting, output).ToList();
        //        TotalCount = Convert.ToInt32(output.Value);
        //        return lstGetLeadResult.Select(objLstRst => new tbl_Leadhistory
        //        {
        //            LeadhistoryId = Convert.ToInt64(objLstRst.LeadhistoryId),
        //            ActCreatedBy = objLstRst.createdBy,
        //            CreatedDate = Convert.ToDateTime(objLstRst.createdDate),
        //            StartDate = objLstRst.startDate,
        //           // StartDate = Convert.ToDateTime(objLstRst.startDate),
        //            HistoryType = objLstRst.HistoryType,
        //            Status = objLstRst.status,
        //            Details = objLstRst.details,
        //            AccountName=objLstRst.AccountName,
        //            ActivityAttachment = objLstRst.ActivityAttachment
        //        }).ToList();
        //    }
        //    catch (Exception)
        //    {                
        //        throw;
        //    }
        //}

        //public tbl_Lead getLeadByLeadId(decimal leadId, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        List<crm_GetLeadbyLeadId_Result> lstLeadsResult = dbContext.crm_GetLeadbyLeadId(leadId).ToList();
        //        return lstLeadsResult.Select(objLead => new tbl_Lead
        //        {
        //            LeadId = objLead.LeadId,
        //            LeadName = objLead.LeadName,
        //            Title = objLead.Title,
        //            Phone = objLead.Phone,
        //            Fax = objLead.Fax,
        //            Email = objLead.Email,
        //            Mobile = objLead.Mobile,
        //            PreferTime = objLead.PreferTime,
        //            PreferMode = objLead.PreferMode,
        //            EmailOptOut = objLead.EmailOptOut,
        //            AccountId = objLead.AccountId,
        //            AccountName = objLead.AccountName,
        //            Website = objLead.Website,
        //            Ownership = objLead.Ownership,
        //            Employees = objLead.Employees,
        //            Industry = objLead.Industry,
        //            AnnualRevenue = objLead.AnnualRevenue,
        //            Rating = objLead.Rating,
        //            LeadStatus = objLead.LeadStatus,
        //            CurrentApplication = objLead.CurrentApplication,
        //            MailingAddress = objLead.mailingAddress,
        //            Mailingstreet = objLead.mailingstreet,
        //            Mailingcity = objLead.mailingcity,
        //            Mailingstate = objLead.mailingstate,
        //            Mailingzip = objLead.mailingzip,
        //            Mailingcountry = objLead.mailingcountry,
        //            BillingAddress = objLead.billingAddress,
        //            Billingstreet = objLead.billingstreet,
        //            Billingcity = objLead.billingcity,
        //            Billingstate = objLead.billingstate,
        //            Billingzip = objLead.billingzip,
        //            Billingcountry = objLead.billingcountry,
        //            SalesMgr1 = objLead.salesMgr1,
        //            SalesMgr2 = objLead.salesMgr2,
        //            Office = objLead.office,
        //            CreatedDate = objLead.createdDate,
        //            CreatedBy = objLead.CreatedBy,
        //            ModifiedBy = objLead.ModifiedBy,
        //            Description = objLead.description,
        //            IsContact = objLead.IsContact,
        //            Source = objLead.Source,
        //            LeadAssigned = objLead.LeadAssigned,
        //            LeadStage = objLead.LeadStage,
        //            SeachIndex = objLead.SeachIndex,
        //            FacebookUsername = objLead.FacebookUsername,
        //            TwitterUsername = objLead.TwitterUsername,
        //            LinkedInUsername = objLead.LinkedInUsername,
        //            GooglePlusUsername = objLead.GooglePlusUsername,
        //            PinterestUsername = objLead.PinterestUsername,
        //            SkypeUsername = objLead.SkypeUsername,
        //            IsOpportunity = objLead.IsOpportunity,
        //            EstimatedCloseDate = objLead.EstimatedCloseDate,
        //            NextStep = objLead.NextStep,
        //            Probability = objLead.Probability,
        //            BusinessType = objLead.BusinessType,
        //            MCreateddate = objLead.mcreatedDate
        //        }).FirstOrDefault();
        //    }
        //    catch (Exception)
        //    {               
        //        throw;
        //    }
        //}

        //public int insertLead(tbl_Lead objLead, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        objLead.ModifiedBy = 0;
        //        objLead.SalesMgr2 = objLead.SalesMgr1;
        //        var result = dbContext.CRM_InsertLead(objLead.LeadName, objLead.Title, objLead.Phone, objLead.Fax, objLead.Email, objLead.Mobile, objLead.PreferTime, objLead.PreferMode,
        //            objLead.EmailOptOut, objLead.AccountId.ToString(), objLead.AccountName, objLead.Website, objLead.Ownership, objLead.Employees, objLead.Industry, objLead.AnnualRevenue, objLead.Rating,
        //            objLead.LeadStatus, objLead.CurrentApplication, objLead.MailingAddress, objLead.Mailingstreet, objLead.Mailingcity, objLead.Mailingstate,
        //            objLead.Mailingzip, objLead.Mailingcountry, objLead.SalesMgr1, objLead.SalesMgr2, objLead.Office,Convert.ToInt64(objLead.CreatedBy), objLead.ModifiedBy, objLead.Description, objLead.Source,
        //            objLead.LeadAssigned, objLead.LeadStage, objLead.FacebookUsername, objLead.TwitterUsername, objLead.LinkedInUsername, objLead.GooglePlusUsername, objLead.PinterestUsername,
        //            objLead.SkypeUsername).FirstOrDefault()??-1;
        //        return result;
        //    }
        //    catch (Exception)
        //    {                
        //        throw;
        //    }
        //}

        //public int updateLeadByLeadId(tbl_Lead objLead, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);                
        //        var result = dbContext.CRM_UpdateLeadByLeadId(objLead.LeadId.ToString(), objLead.LeadName, objLead.Title, objLead.Phone, objLead.Fax, objLead.Email, objLead.Mobile, objLead.PreferTime, objLead.PreferMode,
        //            objLead.EmailOptOut, objLead.AccountId.ToString(), objLead.AccountName, objLead.Website, objLead.Ownership, objLead.Employees, objLead.Industry, objLead.AnnualRevenue, objLead.Rating,
        //            objLead.LeadStatus, objLead.CurrentApplication, objLead.MailingAddress, objLead.Mailingstreet, objLead.Mailingcity, objLead.Mailingstate,
        //            objLead.Mailingzip, objLead.Mailingcountry, objLead.SalesMgr1, objLead.SalesMgr2, objLead.Office, objLead.ModifiedBy, objLead.Description, objLead.Source,
        //            objLead.LeadAssigned, objLead.LeadStage, objLead.FacebookUsername, objLead.TwitterUsername, objLead.LinkedInUsername, objLead.GooglePlusUsername, objLead.PinterestUsername,
        //            objLead.SkypeUsername);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public Int64 DeleteLeadsDetailsByLeadId(Int64 Leadid, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);

        //        return obj.CRM_DeleteLead(Leadid);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

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

        //public Int64 ConvertToOpportunity(Int64 ConvLeadid, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var result = ClientEntity.CRM_ConvertLeadToOpportunity(ConvLeadid);
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public List<activity_types> GetLeadActivityType(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<CRM_GetLeadActivityType_Result> getleadActivities = obj.CRM_GetLeadActivityType().ToList();
                return getleadActivities.Select(getLeadTypeActiv => new activity_types
                {
                    Name = getLeadTypeActiv.name,
                    Aid = getLeadTypeActiv.aid,

                }).ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }

        //public string createLeadNewActivity(tbl_Leadhistory tblhistory, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var result = "";
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        result = obj.CRM_USP_INSERT_UPDATE_LEAD_ACTIVITY(Convert.ToInt64(tblhistory.LeadhistoryId),tblhistory.HistoryType, tblhistory.StartTime, tblhistory.Endtime, tblhistory.Status,
        //                                                            tblhistory.Details, Convert.ToString(tblhistory.CreatedBy), "Task", Convert.ToInt64(tblhistory.Leadid), tblhistory.StartDate, 
        //                                                            tblhistory.Priority, tblhistory.RemindDate,tblhistory.RemindTime, tblhistory.CompletedDate,tblhistory.AssignedTo,
        //                                                            Convert.ToString(tblhistory.AccountId), tblhistory.AccountName, tblhistory.NotificationFlag, tblhistory.ActivityAttachment, Convert.ToString(tblhistory.CreatedDate)).FirstOrDefault();
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public tbl_Leadhistory editLeadNewActivityByLeadhistoryId(Int64 leadhistoryid, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        List<CRM_GET_LEAD_DETAIL_Result> lstLeadsResult = dbContext.CRM_GET_LEAD_DETAIL(leadhistoryid).ToList();
        //        return lstLeadsResult.Select(objLeadActivity => new tbl_Leadhistory
        //        {
        //            LeadhistoryId = objLeadActivity.LeadhistoryId,
        //            HistoryType = objLeadActivity.HistoryType,
        //            StartTime = objLeadActivity.startTime,
        //            Endtime = objLeadActivity.endtime,
        //            Status = objLeadActivity.status,
        //            Details = objLeadActivity.details,
        //            CreatedDate = objLeadActivity.createdDate,
        //            CreatedBy = objLeadActivity.CreatedBy,
        //            ModifiedDate = objLeadActivity.modifiedDate,
        //            EventType = objLeadActivity.eventType,
        //            Leadid = objLeadActivity.Leadid,
        //            StartDate =objLeadActivity.startDate,
        //            Priority = objLeadActivity.priority,
        //            RemindDate = objLeadActivity.remindDate,
        //            RemindTime = objLeadActivity.remindTime,
        //            CompletedDate = objLeadActivity.completedDate,
        //            AssignedTo = objLeadActivity.assignedTo,
        //            AccountId = objLeadActivity.AccountId,
        //            AccountName = objLeadActivity.AccountName,
        //            ActivityFlag = objLeadActivity.ActivityFlag,
        //            ActivityAttachment = objLeadActivity.ActivityAttachment,        
        //        }).FirstOrDefault();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }            
        //}

        //public string updateLeadActivityByLeadHistoryId(tbl_Leadhistory tblhistory, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var result = obj.CRM_USP_INSERT_UPDATE_LEAD_ACTIVITY(Convert.ToInt64(tblhistory.LeadhistoryId), tblhistory.HistoryType, tblhistory.StartTime, tblhistory.Endtime, tblhistory.Status,
        //                                                            tblhistory.Details, Convert.ToString(tblhistory.CreatedBy), "Task", Convert.ToInt64(tblhistory.Leadid), tblhistory.StartDate,
        //                                                            tblhistory.Priority, tblhistory.RemindDate, tblhistory.RemindTime, tblhistory.CompletedDate, tblhistory.AssignedTo,
        //                                                            Convert.ToString(tblhistory.AccountId), tblhistory.AccountName, tblhistory.NotificationFlag, tblhistory.ActivityAttachment, Convert.ToString(tblhistory.CreatedDate)).FirstOrDefault();
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int DeleteLeadActivity(int leadhistoryid, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
        //        {
        //            ClientEntity.Database.Connection.Open();
        //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
        //            var result = ClientEntity.CRM_DELETE_LEADACTIVITYDETAILS(leadhistoryid);
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //public int CompleteLeadActivity(int leadhistoryid, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        return obj.crm_LeadCompleteActivity(leadhistoryid);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<OutlookMail> getLeadEmails(string type, string email, string subject, string body, string loginId, string userId, int StartIndex, int PageSize, string Sorting, out int TotalRecordsCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
        //        List<CRM_GetLeadMails_Result> listGetResult = dbContext.CRM_GetLeadMails(type, email, subject, body, loginId, userId, StartIndex, PageSize, Sorting, output).ToList();
        //        TotalRecordsCount = Convert.ToInt32(output.Value);
        //        return listGetResult.Select(objLstRslt => new OutlookMail
        //        {
        //            ID = Convert.ToInt64(objLstRslt.ID),
        //            CreatedBy = objLstRslt.Createdby,
        //            Sender = objLstRslt.Sender,
        //            Recipient = objLstRslt.Recipient,
        //            Subject = objLstRslt.Subject,
        //            Date = objLstRslt.date,
        //            Folder = objLstRslt.Type,
        //            Body = (Regex.Replace(objLstRslt.Body, @"<[^>]+>|&nbsp;", "")).Length > 100 ? (Regex.Replace(objLstRslt.Body, @"<[^>]+>|&nbsp;", "")).Substring(0, 99) : (Regex.Replace(objLstRslt.Body, @"<[^>]+>|&nbsp;", ""))
        //        }).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public string GetLeadNameById(int LeadId, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        string LeadName = string.Empty;
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);

        //        LeadName = (from TL in obj.tbl_Lead
        //                    where TL.LeadId == LeadId
        //                    select TL.LeadName).SingleOrDefault();
        //        return LeadName;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public List<tbl_Lead> GetLeadsLookUpList(string keyword, string leadOwner, string office, string logInId, string roleId, string orderByClause, string Connectionstring, string dbName)
        //{
        //    DevEntities dbContext = new DevEntities(Connectionstring);
        //    dbContext.Database.Connection.Open();
        //    dbContext.Database.Connection.ChangeDatabase(dbName);

        //    List<CRM_LEADSList_Result> LeadLookupLst = dbContext.CRM_LEADSList(keyword, leadOwner, office, Convert.ToInt32(logInId), Convert.ToInt32(roleId), orderByClause).ToList();

        //    //List<CRM_LEADSLOOKUP_Result> LeadLookupLst = dbContext.CRM_LEADSLOOKUP(keyword, leadOwner, office, Convert.ToInt32(logInId), Convert.ToInt32(roleId), orderByClause).ToList();

        //    return LeadLookupLst.Select(leadst => new tbl_Lead
        //    {
        //        LeadId = leadst.LeadId ?? 0,
        //        LeadName = leadst.LeadName,
        //        AccountId = leadst.AccountId,
        //        AccountName = leadst.AccountName,
        //        LeadStatus = leadst.LeadStatus,

        //    }).ToList();
        //}

        //public List<tbl_Lead> GetLeadsList(string Connectionstring, string dbName)
        //{
        //    DevEntities dbContext = new DevEntities(Connectionstring);
        //    dbContext.Database.Connection.Open();
        //    dbContext.Database.Connection.ChangeDatabase(dbName);

        //    List<tbl_Lead> Leadlst = dbContext.tbl_Lead.ToList();
        //    return Leadlst;
        //}

        //public int createLeadNewActivity_Insert(tbl_Leadhistory tblhistory, string Connectionstring, string dbName)
        //{
        //    try
        //    {


        //        int res = 0;
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("LeadHisId", typeof(int));
        //        //res = obj.CRM_INSERT_UPDATE_LEAD_ACTIVITY(Convert.ToInt64(tblhistory.LeadhistoryId), tblhistory.HistoryType, tblhistory.StartTime, tblhistory.Endtime, tblhistory.Status,
        //        //                                                        tblhistory.Details, Convert.ToString(tblhistory.CreatedBy), "Task", Convert.ToInt64(tblhistory.Leadid), tblhistory.StartDate,
        //        //                                                        tblhistory.Priority, tblhistory.RemindDate, tblhistory.RemindTime, tblhistory.CompletedDate, tblhistory.AssignedTo,
        //        //                                                        Convert.ToString(tblhistory.AccountId), tblhistory.AccountName, tblhistory.NotificationFlag, tblhistory.ActivityAttachment, Convert.ToString(tblhistory.CreatedDate),output);

        //        res = Convert.ToInt32(obj.CRM_INSERT_UPDATE_LEAD_ACTIVITY(Convert.ToInt64(tblhistory.LeadhistoryId), tblhistory.HistoryType, tblhistory.StartTime, tblhistory.Endtime, tblhistory.Status,
        //                                                               tblhistory.Details, Convert.ToString(tblhistory.CreatedBy), "Task", Convert.ToInt64(tblhistory.Leadid), tblhistory.StartDate,
        //                                                               tblhistory.Priority, tblhistory.RemindDate, tblhistory.RemindTime, tblhistory.CompletedDate, tblhistory.AssignedTo,
        //                                                               Convert.ToString(tblhistory.AccountId), tblhistory.AccountName, tblhistory.NotificationFlag, tblhistory.ActivityAttachment, Convert.ToString(tblhistory.CreatedDate), output));
        //        res = Convert.ToInt32(output.Value);
        //        return res;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        // code for Zoho leads
        // Displaying the list of lead in the Index page
        public List<Account> GetAllAccountLeads(string Keyword, string companyname,int ownerid,int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                List<CRM_GetAllAccountLeads_Result> LstLeadsRes = dbContext.CRM_GetAllAccountLeads(Keyword, ownerid,jtStartIndex, jtPageSize, jtSorting, output).ToList();
                var LeadsLst = LstLeadsRes.Select(Led => new Account
                    {
                         ID = Led.ContactID.GetValueOrDefault(),
                         Title = Led.Title,
                         FirstName = Led.FirstName,
                         LastName = Led.LastName,
                         Phone = Led.Phone,
                         Email = Led.Email,
                         OwnerID = Led.OwnerID,
                         OwnerFirstName = Led.OwnerfirstName,
                         OwnerLastName = Led.ownerlastname,
                         CompanyID = Led.CompanyID,
                         CompanyName = Led.CompanyName,
                         LeadSource = Led.LeadSource,
                         Ownership = Led.LeadOwner

                    }).ToList();
                RecordCount = Convert.ToInt32(output.Value);
                



                //List<Account> AccountLst = dbContext.Accounts.Where(a => a.AccountTypeId == 1).ToList();
                //RecordCount = AccountLst.Count;
                //RecordCount = Convert.ToInt32(output.Value);
                return LeadsLst;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // Creating the Leads
        public bool CreateLead(Account objaccount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                dbContext.Accounts.Add(objaccount);
                dbContext.SaveChanges();
                return true; ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Account GetAccountDetailsByID(int id, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                List<Account> lstAccount = dbContext.Accounts.Where(a => a.ID == id).ToList();
                return lstAccount.Select(objAccount => new Account
                    {
                        ID = objAccount.ID,
                        Name = objAccount.Name,
                        Title = objAccount.Title,
                        FirstName = objAccount.FirstName,
                        LastName = objAccount.LastName,
                        Phone = objAccount.Phone,
                        Mobile= objAccount.Mobile,
                        Fax = objAccount.Fax,
                        Email = objAccount.Email,
                        EmailOptOut = objAccount.EmailOptOut,
                        AccountTypeId = objAccount.AccountTypeId,
                        Website = objAccount.Website,
                        Ownership = objAccount.Ownership,
                        TotalEmployees = objAccount.TotalEmployees,
                        IndustryID = objAccount.IndustryID,
                        AnnualRevenue = objAccount.AnnualRevenue,
                        Rating = objAccount.Rating,
                        LeadStatus = objAccount.LeadStatus,
                        LeadSource = objAccount.LeadSource,
                        LeadName = objAccount.FirstName + " " + objAccount.LastName,
                        MailingAddress = objAccount.MailingAddress,
                        MailingAddress2= objAccount.MailingAddress2,
                        Mailingcity = objAccount.Mailingcity,
                        MailingstateID=objAccount.MailingstateID,
                        Mailingzip=objAccount.Mailingzip,
                        MailingcountryID = objAccount.MailingcountryID,
                        CreatedBy = objAccount.CreatedBy,
                        CreatedDate = objAccount.CreatedDate,
                        ModifiedBy = objAccount.ModifiedBy,
                        ModifiedDate = objAccount.ModifiedDate,
                        Description = objAccount.Description,
                        FacebookUsername = objAccount.FacebookUsername,
                        TwitterUsername = objAccount.TwitterUsername,
                        LinkedInUsername = objAccount.LinkedInUsername,
                        SkypeUsername = objAccount.SkypeUsername,
                        CompanyID = objAccount.CompanyID,
                        CompanyName = objAccount.CompanyName,
                        ContactTypeID = objAccount.ContactTypeID,
                        OwnerID = objAccount.OwnerID
                        
                    }).FirstOrDefault();

                
            }
            catch (Exception)
            {
                throw;
            }

        }



        public int updateLeadDetailsByLeadId(Account objAccount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                Account getAccountDetails = dbContext.Accounts.Where(a => a.ID == objAccount.ID).SingleOrDefault();

               
                        getAccountDetails.Name = objAccount.Name;
                        getAccountDetails.Title = objAccount.Title;
                        getAccountDetails.FirstName = objAccount.FirstName;
                        getAccountDetails.LastName = objAccount.LastName;
                        getAccountDetails.Phone = objAccount.Phone;
                        getAccountDetails.Mobile = objAccount.Mobile;
                        getAccountDetails.Fax = objAccount.Fax;
                        getAccountDetails.Email = objAccount.Email;
                        getAccountDetails.EmailOptOut = objAccount.EmailOptOut;
                        getAccountDetails.AccountTypeId = objAccount.AccountTypeId;
                        getAccountDetails.Website = objAccount.Website;
                        getAccountDetails.Ownership = objAccount.Ownership;
                        getAccountDetails.TotalEmployees = objAccount.TotalEmployees;
                        getAccountDetails.IndustryID = objAccount.IndustryID;
                        getAccountDetails.AnnualRevenue = objAccount.AnnualRevenue;
                        getAccountDetails.Rating = objAccount.Rating;
                        getAccountDetails.LeadStatus = objAccount.LeadStatus;
                        getAccountDetails.LeadSource = objAccount.LeadSource;
                        getAccountDetails.LeadName = objAccount.FirstName + " " + objAccount.LastName;
                        getAccountDetails.MailingAddress = objAccount.MailingAddress;
                        getAccountDetails.MailingAddress2 = objAccount.MailingAddress2;
                        getAccountDetails.Mailingcity = objAccount.Mailingcity;
                        getAccountDetails.MailingstateID = objAccount.MailingstateID;
                        getAccountDetails.Mailingzip = objAccount.Mailingzip;
                        getAccountDetails.MailingcountryID = objAccount.MailingcountryID;
                        getAccountDetails.CreatedBy = objAccount.CreatedBy;
                        getAccountDetails.CreatedDate = objAccount.CreatedDate;
                        getAccountDetails.ModifiedBy = objAccount.ModifiedBy;
                        getAccountDetails.ModifiedDate = objAccount.ModifiedDate;
                        getAccountDetails.Description = objAccount.Description;
                        getAccountDetails.FacebookUsername = objAccount.FacebookUsername;
                        getAccountDetails.TwitterUsername = objAccount.TwitterUsername;
                        getAccountDetails.LinkedInUsername = objAccount.LinkedInUsername;
                        getAccountDetails.SkypeUsername = objAccount.SkypeUsername;
                        getAccountDetails.CompanyID = objAccount.CompanyID;
                        getAccountDetails.CompanyName = objAccount.CompanyName;
                        getAccountDetails.ContactTypeID = objAccount.ContactTypeID;

                        dbContext.SaveChanges();

                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool DeleteLeadsDetails(int Leadid, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                Account ObjAccount = obj.Accounts.Where(a => a.ID == Leadid).SingleOrDefault();
                obj.Accounts.Remove(ObjAccount);
                obj.SaveChanges();
                return true;

                
            }
            catch (Exception)
            {
                throw;
            }
        }



        public List<Company> GetAllCompaniesList(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                List<Company> LstCompany = obj.Companies.ToList();


                return LstCompany;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
