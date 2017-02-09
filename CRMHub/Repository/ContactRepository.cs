using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRMHub.Repository
{
    public class ContactRepository : IContactsRepository
    {
        //public List<tbl_Contact> getListOfSearchContacts(string Keyword, string Office, string RoleId, string loginId, string salesRepId, string filterExpression, string funnelSuspects, string skillSearchOption, string skillId, int startIndex, int pageSize, string sorting, out int TotalCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));
        //        List<CRM_GetSearchContacts_Result> lstSearchContactResult = obj.CRM_GetSearchContacts(Keyword, Office, RoleId, loginId, salesRepId, filterExpression, funnelSuspects, skillSearchOption, skillId, startIndex, pageSize, sorting, output).ToList();
        //        TotalCount = Convert.ToInt32(output.Value);
        //        List<tbl_Contact> listContacts = lstSearchContactResult.Select(contact => new tbl_Contact
        //        {
        //            ContactId = Convert.ToInt32(contact.ContactId),
        //            Fname = contact.fname,
        //            Title = contact.title,
        //            Phone = contact.Phone,
        //            Mobile = contact.mobile,
        //            Email = contact.Email,
        //            CreatedDate = Convert.ToDateTime(contact.createdDate),
        //            Contact_office = contact.contact_office,
        //            Suspect_prospect = contact.suspect_prospect,
        //            Mailingzip = contact.mailingzip,
        //            Account_Name = contact.Account_Name,
        //            accountSite = contact.contact_office
        //        }).ToList();
        //        return listContacts;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<tbl_Contact> getListOfSearchContactsLists(string Keyword, string Office, string Type, string RoleId, string loginId, string salesRepId, string filterExpression, string funnelSuspects, string skillSearchOption, string skillId, int startIndex, int pageSize, string sorting, string specifiedZip, decimal dist, out int TotalCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecords", typeof(int));
        //        // List<CRM_SearchContactList_Result> lstSearchContactResult = obj.CRM_SearchContactList(Keyword, Office, RoleId, loginId, salesRepId, filterExpression, funnelSuspects, skillSearchOption, skillId, startIndex, pageSize, sorting, output).ToList();
        //        // List<CRM_GetSearchContactsList_Result> lstSearchContactResult = obj.CRM_GetSearchContactsList(Keyword, Office, RoleId, loginId, salesRepId, filterExpression, funnelSuspects, skillSearchOption, skillId, startIndex, pageSize, sorting, output).ToList();


        //        List<CRM_SearchContactList_temp1_Result> lstSearchContactResult = obj.CRM_SearchContactList_temp1(Keyword, Office, Type, RoleId, loginId, salesRepId, filterExpression, funnelSuspects, skillSearchOption, skillId, startIndex, pageSize, sorting, specifiedZip, dist, output).ToList();

        //        TotalCount = Convert.ToInt32(output.Value);
        //        List<tbl_Contact> listContacts = lstSearchContactResult.Select(contact => new tbl_Contact
        //        {
        //            ContactId = Convert.ToInt32(contact.ContactId),
        //            Fname = contact.fname + " " + contact.lname,
        //            LName = contact.lname,
        //            ContactType = contact.ContactType,
        //            Title = contact.title,
        //            Account_Name = contact.Account_Name,
        //            AccountId = contact.AccountId,
        //            Phone = string.IsNullOrEmpty(contact.Phone) ? "" : contact.Phone,
        //            Mobile = string.IsNullOrEmpty(contact.mobile) ? "" : contact.mobile,
        //            Email = string.IsNullOrEmpty(contact.Email) ? "" : contact.Email,
        //            Mailingzip = string.IsNullOrEmpty(contact.mailingzip) ? "" : contact.mailingzip,
        //            //Phone = contact.Phone,
        //            //Mobile = contact.mobile,
        //            //Email = contact.Email,
        //            //Mailingzip = contact.mailingzip,
        //            CreatedDate = Convert.ToDateTime(contact.createdDate)

        //        }).ToList();
        //        //.OrderByDescending(x=>x.ContactId).ToList();
        //        return listContacts;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        public List<user> getSalesRepresentative(string loginId, string roleId)
        {
            try
            {
                var obj = new DevEntities();
                List<CRM_getSalesRepresentative_Result> lstSalesRepResult = obj.CRM_getSalesRepresentative(loginId, roleId).ToList();
                List<user> lstSalesRepUser = lstSalesRepResult.Select(usr => new user
                {
                    FirstName = usr.first_Name,
                    UserId = usr.userId,
                    RoleId = usr.roleId
                }).ToList();
                return lstSalesRepUser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public tbl_Contact getContactDetailsByContactId(int contactId, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var result = obj.CRM_GetContactDetailByContactId(contactId).Select(contact => new tbl_Contact
        //        {
        //            ContactId = contact.ContactId,
        //            Initial = contact.Initial,
        //            Fname = contact.Fname,
        //            Phone = contact.Phone,
        //            Fax = contact.Fax,
        //            Email = contact.Email,
        //            Title = contact.Title,
        //            Source = contact.Source,
        //            Account_type = contact.Type,
        //            Mobile = contact.Mobile,
        //            CreatedDate = contact.CreatedDate,
        //            ModifiedDate = contact.ModifiedDate,
        //            Creator = contact.Creator,
        //            ModifiedBy = contact.ModifiedBy,
        //            AccountId = contact.AccountId,
        //            Homephone = contact.Homephone,
        //            OtherPhone = contact.OtherPhone,
        //            AsstPhone = contact.AsstPhone,
        //            Assistant = contact.Assistant,
        //            EmailOptOut = contact.EmailOptOut,
        //            Sector = contact.Sector,
        //            Description = contact.Description,
        //            ReportTo = contact.ReportTo,
        //            Birthdate = contact.Birthdate,
        //            Department = contact.Department,
        //            Mailingstreet = contact.Mailingstreet,
        //            Mailingcity = contact.Mailingcity,
        //            Mailingstate = contact.Mailingstate,
        //            Otherstreet = contact.Otherstreet,
        //            Othercity = contact.Othercity,
        //            Otherstate = contact.otherstate,
        //            Mailingzip = contact.mailingzip,
        //            Otherzip = contact.Otherzip,
        //            Mailingcountry = contact.Mailingcountry,
        //            Othercountry = contact.Othercountry,
        //            ReportingManager = contact.ReportingManager,
        //            Suspect_prospect = contact.Suspect_prospect,
        //            SalesMgr1 = contact.SalesMgr1,
        //            SalesMgr2 = contact.SalesMgr2,
        //            Contact_office = contact.Contact_office,
        //            PhoneExt = contact.PhoneExt,
        //            MailingAddress = contact.MailingAddress,
        //            BillingAddress = contact.BillingAddress,
        //            IsCorporate = contact.IsCorporate,
        //            ContactIdEditable = contact.ContactIdEditable,
        //            IsAllowSMS = contact.IsAllowSMS,
        //            ProviderId = contact.ProviderId,
        //            Type = contact.Type,
        //            SeachIndex = contact.SeachIndex,
        //            FacebookUsername = contact.FacebookUsername,
        //            TwitterUsername = contact.TwitterUsername,
        //            LinkedInUsername = contact.LinkedInUsername,
        //            GooglePlusUserName = contact.GooglePlusUserName,
        //            PinterestUsername = contact.PinterestUsername,
        //            SkypeUsername = contact.SkypeUsername,
        //            //NickName = contact.NickName,
        //            mcreatedDate = contact.mcreatedDate,
        //            mbirthdate = contact.mbirthdate,
        //            LoginID = contact.LoginID,
        //            Password = contact.Password,
        //            UserID = contact.UserID,
        //            AccountOffice = contact.Account_office,
        //            Account_Name = contact.Account_Name,
        //            MName = contact.MName,
        //            LName = contact.LName
        //        }).SingleOrDefault();
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int insertContact(tbl_Contact objContact, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        if (objContact.Initial == null)
        //            objContact.Initial = "";
        //        int result = Convert.ToInt32(obj.CRM_InsertContact(objContact.Initial, objContact.Fname, objContact.Email, objContact.AccountId, objContact.Title, objContact.Source,
        //           objContact.Mailingstreet, objContact.Otherstreet, objContact.Mailingcity, objContact.Othercity, objContact.Mailingstate, objContact.Otherstate, objContact.Mailingzip,
        //           objContact.Otherzip, objContact.Mailingcountry, objContact.Othercountry, objContact.Phone, objContact.Mobile, objContact.Homephone, objContact.OtherPhone, objContact.AsstPhone,
        //           objContact.Fax, objContact.Description, objContact.EmailOptOut, objContact.Sector, objContact.MName, objContact.LName, objContact.ReportTo, objContact.Birthdate, objContact.Department, objContact.Assistant, objContact.Suspect_prospect,
        //           objContact.SalesMgr1, objContact.SalesMgr2, objContact.Contact_office, objContact.IsCorporate, objContact.MailingAddress, objContact.BillingAddress, objContact.IsAllowSMS,
        //           objContact.ProviderId, objContact.Type, objContact.FacebookUsername, objContact.TwitterUsername, objContact.LinkedInUsername, objContact.GooglePlusUserName,
        //           objContact.PinterestUsername, objContact.SkypeUsername, objContact.LoginID, objContact.Password).FirstOrDefault() ?? -1);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public bool CheckContactExistsOrNot(string contactname, string Connectionstring, string dbName)
        {
            var flag = false;
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var emailresult = obj.CRM_IsContactExists(contactname);
                if (emailresult.Count() != 0)
                {
                    flag = true;
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public string NewUserContactLogin(tbl_Contact objcontact, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        string newusername = string.Empty;
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        if (objcontact.lastname == null)
        //            objcontact.lastname = "";
        //        newusername = obj.CRM_Get_Username(objcontact.Fname, objcontact.lastname).FirstOrDefault();
        //        return newusername;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int UpdateContact(tbl_Contact objContact, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        //int result = Convert.ToInt32(obj.CRM_UpdateContactByContactId(Convert.ToString(objContact.ContactId),objContact.Initial,objContact.Fname,objContact.Email,Convert.ToString(objContact.AccountId),objContact.Title,
        //        //    objContact.Source,objContact.Mailingstreet,objContact.Otherstreet,objContact.Mailingcity,objContact.Othercity,objContact.Mailingstate,objContact.Otherstate,
        //        //    objContact.Mailingzip,objContact.Otherzip,objContact.Mailingcountry,objContact.Othercountry,objContact.Phone,objContact.Mobile,objContact.Homephone,objContact.OtherPhone,objContact.AsstPhone,objContact.Fax,
        //        //    objContact.Description,objContact.EmailOptOut,objContact.ReportTo,objContact.Birthdate,objContact.Department,objContact.Assistant,objContact.Suspect_prospect,objContact.SalesMgr1,
        //        //    objContact.SalesMgr2,objContact.Contact_office,objContact.IsCorporate,objContact.MailingAddress,objContact.Mailingcity,objContact.BillingAddress,objContact.Type,
        //        //    objContact.FacebookUsername,objContact.TwitterUsername,objContact.LinkedInUsername,objContact.GooglePlusUserName,objContact.SkypeUsername,objContact.LoginID,
        //        //    objContact.Password).FirstOrDefault()??"");
        //        int result = Convert.ToInt32(obj.CRM_UpdateContactByContactId(Convert.ToString(objContact.ContactId), objContact.Initial, objContact.Fname, objContact.Email, Convert.ToString(objContact.AccountId), objContact.Title,
        //           objContact.Source, objContact.Mailingstreet, objContact.Otherstreet, objContact.Mailingcity, objContact.Othercity, objContact.Mailingstate, objContact.Otherstate,
        //           objContact.Mailingzip, objContact.Otherzip, objContact.Mailingcountry, objContact.Othercountry, objContact.Phone, objContact.Mobile, objContact.Homephone, objContact.OtherPhone, objContact.AsstPhone, objContact.Fax, objContact.Description,
        //           objContact.EmailOptOut, objContact.Sector, objContact.MName, objContact.LName, objContact.ReportTo, objContact.Birthdate, objContact.Department, objContact.Assistant, objContact.Suspect_prospect, objContact.SalesMgr1, objContact.SalesMgr2, objContact.Contact_office, objContact.IsCorporate,
        //           objContact.MailingAddress, objContact.BillingAddress, objContact.Type, objContact.FacebookUsername, objContact.TwitterUsername, objContact.LinkedInUsername, objContact.GooglePlusUserName, objContact.PinterestUsername, objContact.SkypeUsername,
        //           objContact.LoginID, objContact.Password).FirstOrDefault() ?? "");
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public List<activity_types> getListOfActivityByType(string Type)
        {
            try
            {
                var dbContext = new DevEntities();
                List<CRM_GetActivityByType_Result> lstActivityResult = dbContext.CRM_GetActivityByType(Type).ToList();
                return lstActivityResult.Select(objLstAct => new activity_types { Aid = objLstAct.aid, Name = objLstAct.name, NameValue = objLstAct.NameValue, Status = objLstAct.status, Type = objLstAct.type }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public List<tbl_account> SelectCompany(string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        List<CRM_ParentCompany_Result> getParentComp = obj.CRM_ParentCompany().ToList();
        //        return getParentComp.Select(getPComp => new tbl_account
        //        {
        //            AccountId = getPComp.AccountId,
        //            Account_Name = getPComp.Account_Name,
        //            Phone = getPComp.Phone,
        //            MailingAddress = getPComp.mailingAddress,
        //            Status = getPComp.Status
        //        }).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public int DeleteContactByContactid(int contactid, string Connectionstring, string dbName)
        {
            try
            {
                int res = 1;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                Account GetContactById = obj.Accounts.Where(acn => acn.ID == contactid).SingleOrDefault();
                obj.Accounts.Remove(GetContactById);               
                obj.SaveChanges();
                return res;

               // return obj.CRM_deleteContactInformation(contactid);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public List<tbl_history> GetCrmContactActivities(int ContactId, string eventType, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        List<crm_ActivitybyContactId_Result> listcontactivity = obj.crm_ActivitybyContactId(ContactId, eventType).ToList();
        //        List<tbl_history> listcrmContActv = listcontactivity.Select(conActresult => new tbl_history
        //        {
        //            HistoryId = conActresult.historyId,
        //            HistoryType = conActresult.HistoryType,
        //            Details = conActresult.details,
        //            CustomerType = conActresult.CustomerType,
        //            //StartDate = conActresult.startDate,
        //            StartDate = Convert.ToDateTime(conActresult.startDate),
        //            CompanyName = conActresult.Account_Name,
        //            CommentedBy = conActresult.commentedBy,
        //            createdBy = conActresult.createdBy,
        //            ContactName = conActresult.fname,
        //            AccountId = conActresult.AccountId,
        //            CompletedDate = DateTime.Parse(conActresult.completedDate),
        //            Status = conActresult.Status,
        //            Userid = conActresult.userid,
        //            ApplicantName = conActresult.ApplicantName,
        //            CreatedDate = conActresult.createdDate,
        //            Attachment = conActresult.Attachment
        //        }).ToList();
        //        return listcrmContActv;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public List<activity_types> GetContactActivityType(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var GetActivityContact = obj.USP_GET_ACTIVTY_TYPE("Account").ToList();

                return GetActivityContact.Select(getContactTypeActiv => new activity_types
                {

                    Name = getContactTypeActiv.name,
                    Aid = getContactTypeActiv.aid
                }).ToList();
                // var getcontactActivities = obj.CRM_GetContactActivityType().ToList();
                //return getcontactActivities.Select(getContactTypeActiv => new activity_types
                //{
                //    Name = getContactTypeActiv.name,
                //    Aid = getContactTypeActiv.aid
                //}).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<user> GetAdminDetail(string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities())
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

        //public string CreateContactPopNewActivity(tbl_history tblhistory, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        var result = "";
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        result = obj.CRM_WorkOrder_InsertActivity("0", tblhistory.Contactid, tblhistory.EventType, tblhistory.HistoryType, tblhistory.Priority, tblhistory.CustomerType,
        //            tblhistory.Status, tblhistory.Details, tblhistory.StartDate, tblhistory.Userid, tblhistory.RemindDate, tblhistory.StartTime, tblhistory.RemindTime,
        //            tblhistory.associatedId, tblhistory.type, DateTime.Now.ToString(), tblhistory.AssignmentId, tblhistory.AccountId.ToString(), tblhistory.ResumeID, tblhistory.ContractOffered, tblhistory.PayRate,
        //            tblhistory.BillRate, tblhistory.AssignedTo).FirstOrDefault() ?? "";
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public tbl_history EditContactPopNewActivityByHistoryId(int HistoryId, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var result = obj.CRM_getHistoryByHistoryID(HistoryId).Select(tblhist => new tbl_history
        //        {
        //            HistoryId = tblhist.HistoryId,
        //            HistoryType = tblhist.HistoryType,
        //            StartTime = tblhist.StartTime,
        //            Endtime = tblhist.Endtime,
        //            Status = tblhist.Status,
        //            Details = tblhist.Details,
        //            CreatedDate = tblhist.CreatedDate,
        //            Userid = tblhist.Userid,
        //            ModifiedDate = tblhist.ModifiedDate,
        //            EventType = tblhist.EventType,
        //            Result = tblhist.Result,
        //            Contactid = tblhist.Contactid,
        //            //StartDate = Convert.ToDateTime(tblhist.StartDate),
        //            StartDate = tblhist.StartDate,
        //            Priority = tblhist.Priority,
        //            RemindDate = tblhist.RemindDate,
        //            CommentedBy = tblhist.CommentedBy,
        //            ResumeID = tblhist.ResumeID,
        //            RemindTime = tblhist.RemindTime,
        //            CompletedDate = tblhist.CompletedDate,
        //            RequisitionId = tblhist.RequisitionId,
        //            AssignedTo = tblhist.AssignedTo,
        //            AssignmentId = tblhist.AssignmentId,
        //            ToDoID = tblhist.ToDoID,
        //            ContractOffered = tblhist.ContractOffered,
        //            PayRate = tblhist.Payrate,
        //            BillRate = tblhist.Billrate,
        //            FstartDate = tblhist.FstartDate,
        //            FremindDate = tblhist.FremindDate,
        //            type = tblhist.Type,
        //            CustomerType = tblhist.CustomerType,
        //            AccountId = tblhist.AccountId,
        //            ContactName = tblhist.ContactName,
        //            Phone = tblhist.Phone
        //        }).SingleOrDefault();
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int UpdateContactPopActivityByHistoryId(tbl_history tblhistory, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        int result = 0;
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        result = obj.CRM_WorkOrder_UpdateActivity(tblhistory.HistoryId, tblhistory.Id, tblhistory.Contactid,
        //            tblhistory.HistoryType, tblhistory.StartTime, tblhistory.StartDate, tblhistory.Status,
        //            tblhistory.Details, tblhistory.Priority, tblhistory.CustomerType, (tblhistory.RemindDate),
        //            tblhistory.RemindTime, tblhistory.associatedId, tblhistory.type, tblhistory.AccountId, tblhistory.ResumeID, tblhistory.ContractOffered,
        //            tblhistory.PayRate, tblhistory.BillRate, tblhistory.NotificationFlag, tblhistory.AssignedTo);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public int DeleteContActivityByHistoryId(int historyId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                return obj.CRM_DeleteTask(historyId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public int CompleteContactActivity(int historyId, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        return obj.CRM_CompleteActivity(historyId);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<tbl_history> GetCrmContactActivities(string ContactId, string Keyword, string Type, string Priority, string Status, int StartIndex, int PageSize, string Sorting, out int TotalCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
        //        List<CRM_GETCONTACT_ACTIVITIES_Result> lstGetContactResult = dbContext.CRM_GETCONTACT_ACTIVITIES(ContactId, Keyword, Type, Priority, Status, StartIndex, PageSize, Sorting, output).ToList();
        //        TotalCount = Convert.ToInt32(output.Value);
        //        return lstGetContactResult.Select(objLstRst => new tbl_history
        //        {
        //            HistoryId = Convert.ToInt32(objLstRst.historyId),
        //            createdBy = objLstRst.createdBy,
        //            CreatedDate = Convert.ToDateTime(objLstRst.createdDate),
        //            // StartDate = Convert.ToDateTime(objLstRst.startDate),
        //            StartDate = objLstRst.startDate == null ? (DateTime?)null : Convert.ToDateTime(objLstRst.startDate),
        //            HistoryType = objLstRst.HistoryType,
        //            Status = objLstRst.Status,
        //            Details = objLstRst.details,
        //            Attachment = objLstRst.Attachment
        //        }).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public int createContactNewActivity(tbl_history hist, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        //string result = "";
        //        int res = 0;

        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("HistoryId", typeof(int));
        //        //result = obj.CRM_WorkOrder_InsertActivity(hist.Id,Convert.ToString(hist.Contactid),hist.EventType, hist.HistoryType, hist.Priority,hist.CustomerType, hist.Status,
        //        //                                                    hist.Details,hist.StartDate,hist.createdBy,hist.RemindDate,hist.StartTime,hist.RemindTime,hist.associatedId,
        //        //                                                    hist.type,Convert.ToString(hist.CreatedDate),hist.AssignmentId,Convert.ToString(hist.AccountId),hist.ResumeID,hist.ContractOffered,
        //        //                                                    hist.PayRate,hist.BillRate,hist.AssignedTo).FirstOrDefault()??"";
        //        res = Convert.ToInt32(obj.CRM_WorkOrder_InsertActivity_New(hist.Id, Convert.ToString(hist.Contactid), hist.EventType, hist.HistoryType, hist.Priority, hist.CustomerType, hist.Status,
        //                                                            hist.Details, hist.StartDate, hist.createdBy, hist.RemindDate, hist.StartTime, hist.RemindTime, hist.associatedId,
        //                                                            hist.type, Convert.ToString(hist.CreatedDate), hist.AssignmentId, Convert.ToString(hist.AccountId), hist.ResumeID, hist.ContractOffered,
        //                                                            hist.PayRate, hist.BillRate, hist.NotificationFlag, hist.AssignedTo, hist.ModuleType, output));
        //        res = Convert.ToInt32(output.Value);
        //        return res;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public tbl_history editContactNewActivityByhistoryId(int historyid, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        List<CRM_getHistoryByHistoryID_Result> lstContactsResult = dbContext.CRM_getHistoryByHistoryID(historyid).ToList();
        //        return lstContactsResult.Select(objContactActivity => new tbl_history
        //        {
        //            HistoryId = objContactActivity.HistoryId,
        //            HistoryType = objContactActivity.HistoryType,
        //            StartTime = objContactActivity.StartTime,
        //            Endtime = objContactActivity.Endtime,
        //            Status = objContactActivity.Status,
        //            Details = objContactActivity.Details,
        //            CreatedDate = objContactActivity.CreatedDate,
        //            Userid = objContactActivity.Userid,
        //            ModifiedDate = objContactActivity.ModifiedDate,
        //            EventType = objContactActivity.EventType,
        //            Result = objContactActivity.Result,
        //            Contactid = objContactActivity.Contactid,
        //            //StartDate=Convert.ToDateTime(objContactActivity.StartDate),
        //            StartDate = objContactActivity.StartDate,
        //            Priority = objContactActivity.Priority,
        //            RemindDate = objContactActivity.RemindDate,
        //            CommentedBy = objContactActivity.CommentedBy,
        //            ResumeID = objContactActivity.ResumeID,
        //            RemindTime = objContactActivity.RemindTime,
        //            CompletedDate = objContactActivity.CompletedDate,
        //            RequisitionId = objContactActivity.RequisitionId,
        //            AssignedTo = objContactActivity.AssignedTo,
        //            AssignmentId = objContactActivity.AssignmentId,
        //            ToDoID = objContactActivity.ToDoID,
        //            ContractOffered = objContactActivity.ContractOffered,
        //            PayRate = objContactActivity.Payrate,
        //            BillRate = objContactActivity.Billrate,
        //            FstartDate = objContactActivity.FstartDate,
        //            FremindDate = objContactActivity.FremindDate,
        //            type = objContactActivity.Type,
        //            CustomerType = objContactActivity.CustomerType,
        //            AccountId = objContactActivity.AccountId,
        //            ContactName = objContactActivity.ContactName,
        //            Phone = objContactActivity.Phone
        //        }).FirstOrDefault();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int updateContactActivityByContactHistoryId(tbl_history tblhistory, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var result = obj.CRM_WorkOrder_UpdateActivity(tblhistory.HistoryId, tblhistory.Id, tblhistory.Contactid, tblhistory.HistoryType, tblhistory.StartTime, tblhistory.StartDate,
        //                                                       tblhistory.Status, tblhistory.Details, tblhistory.Priority, tblhistory.CustomerType, (tblhistory.RemindDate), tblhistory.RemindTime,
        //                                                       tblhistory.associatedId, tblhistory.type, tblhistory.AccountId, tblhistory.ResumeID, tblhistory.ContractOffered,
        //                                                       tblhistory.PayRate, tblhistory.BillRate, tblhistory.NotificationFlag, tblhistory.AssignedTo);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public int DeleteContactActivity(int historyid, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var result = ClientEntity.CRM_DeleteTask(historyid);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public List<OutlookMail> getContactEmails(string type, string email, string subject, string body, string loginId, string userId, int StartIndex, int PageSize, string Sorting, out int TotalRecordsCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities dbContext = new DevEntities(Connectionstring);
        //        dbContext.Database.Connection.Open();
        //        dbContext.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
        //        List<CRM_GETCONTACTMAILS_Result> listGetResult = dbContext.CRM_GETCONTACTMAILS(type, email, subject, body, loginId, userId, StartIndex, PageSize, Sorting, output).ToList();
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

        //public tbl_Contact GetMergeContactDetails(long Contactid, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities objmerge = new DevEntities(Connectionstring);
        //        objmerge.Database.Connection.Open();
        //        objmerge.Database.Connection.ChangeDatabase(dbName);
        //        List<CRM_GetContactDetail_Result> mergeresult = objmerge.CRM_GetContactDetail(Contactid).ToList();
        //        return mergeresult.Select(objmergeresult => new tbl_Contact
        //        {
        //            ContactId = objmergeresult.ContactId,
        //            Initial = objmergeresult.Initial,
        //            Fname = objmergeresult.Fname,
        //            Phone = objmergeresult.Phone,
        //            Fax = objmergeresult.Fax,
        //            Email = objmergeresult.Email,
        //            Title = objmergeresult.Title,
        //            Source = objmergeresult.Source,
        //            Mobile = objmergeresult.Mobile,
        //            CreatedDate = objmergeresult.CreatedDate,
        //            ModifiedDate = objmergeresult.ModifiedDate,
        //            Creator = objmergeresult.Creator,
        //            ModifiedBy = objmergeresult.ModifiedBy,
        //            AccountId = objmergeresult.AccountId,
        //            Homephone = objmergeresult.Homephone,
        //            OtherPhone = objmergeresult.OtherPhone,
        //            AsstPhone = objmergeresult.AsstPhone,
        //            Assistant = objmergeresult.Assistant,
        //            EmailOptOut = objmergeresult.EmailOptOut,
        //            Description = objmergeresult.Description,
        //            ReportTo = objmergeresult.ReportTo,
        //            Birthdate = objmergeresult.Birthdate,
        //            Department = objmergeresult.Department,
        //            Mailingstreet = objmergeresult.Mailingstreet,
        //            Otherstreet = objmergeresult.Otherstreet,
        //            Mailingzip = objmergeresult.Mailingzip,
        //            Otherzip = objmergeresult.Otherzip,
        //            Mailingcountry = objmergeresult.Mailingcountry,
        //            Othercountry = objmergeresult.Othercountry,
        //            ReportingManager = objmergeresult.ReportingManager,
        //            Suspect_prospect = objmergeresult.Suspect_prospect,
        //            SalesMgr1 = objmergeresult.SalesMgr1,
        //            SalesMgr2 = objmergeresult.SalesMgr2,
        //            Contact_office = objmergeresult.Contact_office,
        //            PhoneExt = objmergeresult.PhoneExt,
        //            MailingAddress = objmergeresult.MailingAddress,
        //            BillingAddress = objmergeresult.BillingAddress,
        //            IsCorporate = objmergeresult.IsCorporate,
        //            ContactIdEditable = objmergeresult.ContactIdEditable,
        //            IsAllowSMS = objmergeresult.IsAllowSMS,
        //            ProviderId = objmergeresult.ProviderId,
        //            Type = objmergeresult.Type,
        //            SeachIndex = objmergeresult.SeachIndex,
        //            FacebookUsername = objmergeresult.FacebookUsername,
        //            TwitterUsername = objmergeresult.TwitterUsername,
        //            LinkedInUsername = objmergeresult.LinkedInUsername,
        //            GooglePlusUserName = objmergeresult.GooglePlusUserName,
        //            PinterestUsername = objmergeresult.PinterestUsername,
        //            SkypeUsername = objmergeresult.SkypeUsername,
        //            mcreatedDate = objmergeresult.mcreatedDate,
        //            mbirthdate = objmergeresult.mbirthdate,
        //            LoginID = objmergeresult.LoginID,
        //            Password = objmergeresult.Password,
        //            UserID = objmergeresult.UserID,
        //        }).FirstOrDefault();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public string UpdateMergedContacts(tbl_Contact mergecontact, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities objupdatemerge = new DevEntities(Connectionstring);
        //        objupdatemerge.Database.Connection.Open();
        //        objupdatemerge.Database.Connection.ChangeDatabase(dbName);
        //        string resultmerge = objupdatemerge.crm_updateContactOnMerge(mergecontact.ContactId, mergecontact.ContactId1, mergecontact.Fname, mergecontact.Email, mergecontact.Phone,
        //                              mergecontact.MailingAddress, mergecontact.Mailingstreet, mergecontact.Mailingcity, mergecontact.Mailingstate, mergecontact.Mailingzip).FirstOrDefault();
        //        return resultmerge;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        //public List<tbl_Contact> GET_UniqueContactName(string excelcntdets, string dbName)
        //{
        //    try
        //    {
        //        var obj = new DevEntities();
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        List<CRM_GET_UniqueContactName1_Result> cntname = obj.CRM_GET_UniqueContactName1(excelcntdets).ToList();
        //        List<tbl_Contact> Fnames = new List<tbl_Contact>();
        //        Fnames = cntname.Select(cnt => new tbl_Contact
        //        {
        //            Fname = cnt.Fname

        //        }).ToList();

        //        return Fnames;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public List<Custom_Manage_Master> GetCustomFieldsByModule(string Module, string Connectionstring, string dbName)
        {
            try
            {


                var obj = new DevEntities();
                List<Custom_Manage_Master> CustomFieldsList = obj.Custom_Manage_Master.Where(CMM => CMM.Module == Module).ToList();

                return CustomFieldsList;


            }
            catch (Exception)
            {
                throw;
            }

        }


        //public List<tbl_account> GetAllCompanyListLookUp(string Keyword, string Office, string RoleId, string loginId, string salesRepId, string sorting, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        //var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
        //        List<CRM_GETALL_COMPANIESList_New_Result> LstCompanies = obj.CRM_GETALL_COMPANIESList_New(Keyword, Office, RoleId, loginId, salesRepId, sorting).ToList();
        //        //TotalRecordsCount = Convert.ToInt32(output.Value);
        //        return LstCompanies.Select(CompLst => new tbl_account
        //        {
        //            AccountId = CompLst.AccountId ?? 0,
        //            //AccountId = (CompLst == null?0:CompLst.AccountId),
        //            Account_Name = CompLst.Account_Name,
        //            AccountSite = CompLst.accountSite,
        //            Phone = CompLst.Phone,
        //            MailingAddress = CompLst.mailingAddress,
        //            Status = CompLst.Status
        //        }).ToList();


        //        //List<CRM_ParentCompany_Result> getParentComp = obj.CRM_ParentCompany().ToList();
        //        //return getParentComp.Select(getPComp => new tbl_account
        //        //{
        //        //    AccountId = getPComp.AccountId,
        //        //    Account_Name = getPComp.Account_Name,
        //        //    Phone = getPComp.Phone,
        //        //    MailingAddress = getPComp.mailingAddress,
        //        //    Status = getPComp.Status
        //        //}).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public string GetAccountSitebyName(string AccountName, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        string AccountSite = string.Empty;
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);

        //        AccountSite = obj.tbl_account.Where(TA => TA.Account_Name == AccountName).Select(tbl => tbl.AccountSite).FirstOrDefault();

        //        //if (objcontact.lastname == null)
        //        //    objcontact.lastname = "";
        //        //newusername = obj.CRM_Get_Username(objcontact.Fname, objcontact.lastname).FirstOrDefault();
        //        return AccountSite;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        // To get all the Contact List
        //public List<tbl_Contact> GelAllContact(string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);

        //        var ContactList = (from ContactObj in obj.tbl_Contact.AsEnumerable()
        //                           join accountobj in obj.tbl_account.AsEnumerable() on ContactObj.AccountId.ToString() equals accountobj.AccountId.ToString()
        //                           into temp
        //                           from Cont in temp.DefaultIfEmpty()
        //                           select new tbl_Contact
        //                           {
        //                               ContactId = ContactObj.ContactId == null ? 0 : ContactObj.ContactId,
        //                               Fname = ContactObj.Fname == null ? "" : ContactObj.Fname,
        //                               Phone = ContactObj.Phone == null ? "" : ContactObj.Phone,
        //                               Mobile = ContactObj.Mobile == null ? "" : ContactObj.Mobile,
        //                               Email = ContactObj.Email == null ? "" : ContactObj.Email,
        //                               Account_Name = Cont == null ? "" : Cont.Account_Name,
        //                               AccountId = Cont == null ? 0 : Cont.AccountId
        //                           }).ToList();


        //        return ContactList;


        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public List<tbl_Contact> GetAllContactsListLookUp(string Keyword, string Office, string RoleId, string loginId, string salesRepId, string sorting, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);

        //        List<CRM_ActivitiesContactList_Result> ContactsLookupList = obj.CRM_ActivitiesContactList(Keyword, Office, RoleId, loginId, salesRepId, sorting).ToList();

        //        // List<CRM_SearchContactListLookUP_Result> ContactsLookupList = obj.CRM_SearchContactListLookUP(Keyword, Office, RoleId, loginId, salesRepId, sorting).ToList();

        //        //List<CRM_GETALL_COMPANIESList_New_Result> LstCompanies = obj.CRM_GETALL_COMPANIESList_New(Keyword, Office, RoleId, loginId, salesRepId, sorting).ToList();
        //        return ContactsLookupList.Select(ContLst => new tbl_Contact
        //        {
        //            ContactId = ContLst.ContactId ?? 0,
        //            Fname = ContLst.fname,
        //            Phone = ContLst.Phone,
        //            Mobile = ContLst.mobile,
        //            Email = ContLst.Email,
        //            Account_Name = ContLst.Account_Name,
        //            AccountId = ContLst.AccountId,
        //            //Title= ContLst.title



        //        }).ToList();
        //        //return LstCompanies.Select(CompLst => new tbl_account
        //        //{
        //        //    AccountId = CompLst.AccountId ?? 0,
        //        //    //AccountId = (CompLst == null?0:CompLst.AccountId),
        //        //    Account_Name = CompLst.Account_Name,
        //        //    AccountSite = CompLst.accountSite,
        //        //    Phone = CompLst.Phone,
        //        //    MailingAddress = CompLst.mailingAddress,
        //        //    Status = CompLst.Status
        //        //}).ToList();



        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //public string GetCompanyNameById(int AccountId, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        string CompanyName = string.Empty;
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);

        //        CompanyName = (from TA in obj.tbl_account
        //                       where TA.AccountId == AccountId
        //                       select TA.Account_Name).SingleOrDefault();
        //        return CompanyName;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public DataSet getListOfSearchContactsListExport(string Keyword, string Office, string Type, string RoleId, string loginId, string salesRepId, string filterExpression, string funnelSuspects, string skillSearchOption, string skillId, int startIndex, int pageSize, string sorting, string Connectionstring)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(Connectionstring))
                {
                    SqlCommand sqlComm = new SqlCommand("CRM_SearchContactListForExport", conn);
                    sqlComm.Parameters.AddWithValue("@keyword", Keyword);
                    sqlComm.Parameters.AddWithValue("@office", Office);
                    sqlComm.Parameters.AddWithValue("@Type", Type);
                    sqlComm.Parameters.AddWithValue("@RoleID", RoleId);
                    sqlComm.Parameters.AddWithValue("@logInId", loginId);
                    sqlComm.Parameters.AddWithValue("@salesRep", salesRepId);
                    sqlComm.Parameters.AddWithValue("@FilterExpression", filterExpression);
                    sqlComm.Parameters.AddWithValue("@funnelSuspects", funnelSuspects);
                    sqlComm.Parameters.AddWithValue("@SkillSerachOption", skillSearchOption);
                    sqlComm.Parameters.AddWithValue("@SkillId", skillId);
                    sqlComm.Parameters.AddWithValue("@StartIndex", startIndex);
                    sqlComm.Parameters.AddWithValue("@PageSize", pageSize);
                    sqlComm.Parameters.AddWithValue("@Sorting", sorting);
                    foreach (SqlParameter Parameter in sqlComm.Parameters)
                    {
                        if (Parameter.Value == null)
                        {
                            Parameter.Value = DBNull.Value;
                        }
                    }

                    sqlComm.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    da.Fill(ds);
                }

                return ds;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public int createContactType(tbl_ContactType objContacttype, string Connectionstring, string dbName)
        {
            try
            {
                int result = 0;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                result = obj.CRM_Add_ContactType(objContacttype.ContactType, objContacttype.Status, objContacttype.Description);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<tbl_ContactType> GetAllContactTypesList(string ContactType, string Status, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                List<CRM_GET_AllContactTypes_Result> Contacttypelst = obj.CRM_GET_AllContactTypes(ContactType, Status, startIndex, pageSize, orderByClause, output).ToList();

                TotalCount = Convert.ToInt32(output.Value);
                List<tbl_ContactType> lstContactTypeDetails = Contacttypelst.Select(contacttype => new tbl_ContactType
                {
                    ContactTypeId = contacttype.ContactTypeId ?? 0,
                    ContactType = contacttype.ContactType,
                    Status = contacttype.Status,
                    Description = contacttype.Description
                }).ToList();
                return lstContactTypeDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public tbl_ContactType GetContactDetailsbyId(int ContactTypeId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                tbl_ContactType contacttypedetails = new tbl_ContactType();
                contacttypedetails = obj.tbl_ContactType.Where(ct => ct.ContactTypeId == ContactTypeId).FirstOrDefault();
                return contacttypedetails;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public int UpdateContactTypeDetailsById(tbl_ContactType objContacttype, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var result = 1;
                tbl_ContactType contacttypedetails = new tbl_ContactType();
                contacttypedetails = obj.tbl_ContactType.Where(CT => CT.ContactTypeId == objContacttype.ContactTypeId).FirstOrDefault();
                contacttypedetails.ContactType = objContacttype.ContactType;
                contacttypedetails.Status = objContacttype.Status;
                contacttypedetails.Description = objContacttype.Description;
                obj.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteContactTypedetailsbyId(int ContactTypeId, string Connectionstring, string dbName)
        {
            bool Result = false;
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                tbl_ContactType deleteObj = obj.tbl_ContactType.Where(d => d.ContactTypeId == ContactTypeId).SingleOrDefault();
                if (deleteObj != null)
                {
                    obj.tbl_ContactType.Remove(deleteObj);
                    obj.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return Result;
            }
        }
        public List<tbl_ContactType> GetAllContacttypeList(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<tbl_ContactType> ContacttypeList = obj.tbl_ContactType.Where(ct => ct.Status == true).ToList();
                return ContacttypeList;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool CheckContactTypeExistsOrNot(string Contacttype, string Connectionstring, string dbName)
        {
            var flag = false;
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var result = obj.tbl_ContactType.Where(ct => ct.ContactType == Contacttype).ToList();
                // var result = obj.CRM_IscolumnlabelExistsForModule(columnlabel, modulename);
                if (result.Count() != 0)
                {
                    flag = true;
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// Look Like Zoho Screen
        /// 
        // code for Zoho leads
        // Displaying the list of lead in the Index page
        public List<Account> GetAllAccountContacts(int Createdby, string Contactkeyword, int CompanyId, int UserId,int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                List<CRM_GetAllAccountContacts_Result> ContactsList = dbContext.CRM_GetAllAccountContacts(Createdby,Contactkeyword, CompanyId, UserId, jtStartIndex, jtPageSize, jtSorting, output).ToList();

                var LstContacts = ContactsList.Select(Con => new Account
                {
                    ID = Con.ContactID.GetValueOrDefault(),
                    Title = Con.Title,
                    FirstName = Con.FirstName,
                    LastName = Con.LastName,
                    Phone = Con.Phone,
                    Email = Con.Email,
                    OwnerID = Con.OwnerID,
                    OwnerFirstName = Con.OwnerfirstName,
                    OwnerLastName = Con.ownerlastname,
                    CompanyID = Con.CompanyID,
                    CompanyName = Con.CompanyName
                }).ToList();
                RecordCount = Convert.ToInt32(output.Value);
               // List<CRM_GetAllAccountOpportunities_Result> OpporLst = dbContext.CRM_GetAllAccountOpportunities(jtStartIndex, jtPageSize, jtSorting, output).ToList();

                //List<Account> AccountLst = dbContext.Accounts.Where(a => a.AccountTypeId == 2).ToList();
                //RecordCount = Convert.ToInt32(LstContacts.Count);
               // RecordCount = AccountLst.Count;
                //RecordCount = Convert.ToInt32(output.Value);
                return LstContacts;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public int InserNewContact(Account objaccount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                dbContext.Accounts.Add(objaccount);
                dbContext.SaveChanges();
                return objaccount.ID; ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Account GetContactDetailsByID(int id, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);

                var result = dbContext.CRM_AccountContactDetailsByID(id).Select(objAccount => new Account
                {
                    ID = objAccount.ID,
                    Name = objAccount.Name,
                    Title = objAccount.Title,
                    FirstName = objAccount.ConFirstName,
                    LastName = objAccount.ConLastName,
                    Phone = objAccount.Phone,
                    Mobile = objAccount.Mobile,
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
                    LeadName = objAccount.ConFirstName + " " + objAccount.ConLastName,
                    MailingAddress = objAccount.MailingAddress,
                    MailingAddress2 = objAccount.MailingAddress2,
                    Mailingcity = objAccount.Mailingcity,
                    MailingstateID = objAccount.MailingstateID,
                    Mailingzip = objAccount.Mailingzip,
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
                    BillingAddress = objAccount.BillingAddress,
                    BillingAddress2 = objAccount.BillingAddress2,
                    Billingcity = objAccount.Billingcity,
                    BillingstateID = objAccount.BillingstateID,
                    BillingcountryID = objAccount.BillingcountryID,
                    Billingzip = objAccount.Billingzip,
                    OwnerID = objAccount.OwnerID,
                   

                    MaillingCountry = objAccount.MailingCountry,
                    MaillingStateName = objAccount.MailingAbbreviation + " " + objAccount.MailingStateName,

                    BillingCountry = objAccount.BillingCountry,
                    BillingStateName = objAccount.billingAbbreviation +" "+ objAccount.BillingStateName

                }).SingleOrDefault();
                return result;
                //List<Account> lstAccount = dbContext.Accounts.Where(a => a.ID == id).ToList();
                //return lstAccount.Select(objAccount => new Account
                //{
                //    ID = objAccount.ID,
                //    Name = objAccount.Name,
                //    Title = objAccount.Title,
                //    FirstName = objAccount.FirstName,
                //    LastName = objAccount.LastName,
                //    Phone = objAccount.Phone,
                //    Mobile = objAccount.Mobile,
                //    Fax = objAccount.Fax,
                //    Email = objAccount.Email,
                //    EmailOptOut = objAccount.EmailOptOut,
                //    AccountTypeId = objAccount.AccountTypeId,
                //    Website = objAccount.Website,
                //    Ownership = objAccount.Ownership,
                //    TotalEmployees = objAccount.TotalEmployees,
                //    IndustryID = objAccount.IndustryID,
                //    AnnualRevenue = objAccount.AnnualRevenue,
                //    Rating = objAccount.Rating,
                //    LeadStatus = objAccount.LeadStatus,
                //    LeadSource = objAccount.LeadSource,
                //    LeadName = objAccount.FirstName + " " + objAccount.LastName,
                //    MailingAddress = objAccount.MailingAddress,
                //    MailingAddress2 = objAccount.MailingAddress2,
                //    Mailingcity = objAccount.Mailingcity,
                //    MailingstateID = objAccount.MailingstateID,
                //    Mailingzip = objAccount.Mailingzip,
                //    MailingcountryID = objAccount.MailingcountryID,
                //    CreatedBy = objAccount.CreatedBy,
                //    CreatedDate = objAccount.CreatedDate,
                //    ModifiedBy = objAccount.ModifiedBy,
                //    ModifiedDate = objAccount.ModifiedDate,
                //    Description = objAccount.Description,
                //    FacebookUsername = objAccount.FacebookUsername,
                //    TwitterUsername = objAccount.TwitterUsername,
                //    LinkedInUsername = objAccount.LinkedInUsername,
                //    SkypeUsername = objAccount.SkypeUsername,
                //    CompanyID = objAccount.CompanyID,
                //    CompanyName = objAccount.CompanyName,
                //    ContactTypeID = objAccount.ContactTypeID,
                //    BillingAddress = objAccount.BillingAddress,
                //    BillingAddress2 = objAccount.BillingAddress2,
                //    Billingcity = objAccount.Billingcity,
                //    BillingstateID = objAccount.BillingstateID,
                //    BillingcountryID = objAccount.BillingcountryID,
                //    Billingzip = objAccount.Billingzip,
                //   // Ownership = objAccount.Ownership

                //}).FirstOrDefault();


            }
            catch (Exception)
            {
                throw;
            }

        }


        public int updateContactDetailsByContactId(Account objAccount, string Connectionstring, string dbName)
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
                getAccountDetails.BillingAddress = objAccount.BillingAddress;
                getAccountDetails.BillingAddress2 = objAccount.BillingAddress2;
                getAccountDetails.Billingcity = objAccount.Billingcity;
                getAccountDetails.BillingstateID = objAccount.BillingstateID;
                getAccountDetails.BillingcountryID = objAccount.BillingcountryID;
                getAccountDetails.Billingzip = objAccount.Billingzip;
                getAccountDetails.OwnerID = objAccount.OwnerID;
                dbContext.SaveChanges();

                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ImportHistory> GET_ImportHistory(string Connectionstring, string Client)
        {
            try
            {
                CRMMasterClientsEntities dbContext = new CRMMasterClientsEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase("CRMClients");

                List<ImportHistory> importlist = dbContext.ImportHistories.Where(tblc => tblc.ClientId == Client).ToList();
                return importlist;

            }
            catch (Exception)
            {
                throw;
            }
        }




        // Displaying the list of all Company Contacts 
        public List<Account> GetAllCompanyContactsbyCompID(int CompanyId, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                List<CRM_GetAllCompanyContacts_Result> CompanyContactList = dbContext.CRM_GetAllCompanyContacts(CompanyId, jtStartIndex, jtPageSize, jtSorting, output).ToList();

              //  List<CRM_GetAllAccountContacts_Result> ContactsList = dbContext.CRM_GetAllAccountContacts(Contactkeyword, CompanyId, UserId, jtStartIndex, jtPageSize, jtSorting, output).ToList();

                var LstContacts = CompanyContactList.Select(Con => new Account
                {
                    ID = Con.ContactID.GetValueOrDefault(),
                    Title = Con.Title,
                    FirstName = Con.FirstName,
                    LastName = Con.LastName,
                    Phone = Con.Phone,
                    Email = Con.Email,
                    OwnerID = Con.OwnerID,
                    OwnerFirstName = Con.OwnerfirstName,
                    OwnerLastName = Con.ownerlastname,
                    CompanyID = Con.CompanyID,
                    CompanyName = Con.CompanyName
                }).ToList();
                RecordCount = Convert.ToInt32(output.Value);
               
                return LstContacts;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}








