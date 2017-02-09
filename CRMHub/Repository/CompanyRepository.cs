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
using System.Globalization;
using System.ComponentModel;

namespace CRMHub.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
         
        public bool CheckCompanyExistsOrNot(string companyname, string Connectionstring, string dbName)
        {
            var flag = false;
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var emailresult = obj.CRM_IsCompanyExists(companyname);
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

       
        public List<tbl_crm_industry> GetIndustry(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<getCRMIndustry_Result> getIndustries = obj.getCRMIndustry().ToList();
                return getIndustries.Select(getIndu => new tbl_crm_industry
                {
                    Id = getIndu.Id,
                    Industry = getIndu.Industry
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<usState> GetState(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                //var getStates = obj.usStates.Select(statecode => new usState
                //    {
                //        StateCode = statecode.StateCode,
                //    }).ToList();

                //return getStates;
                List<CRM_GetState_Result> getStates = obj.CRM_GetState().ToList();
                return getStates.Select(getStat => new usState
                {
                    StateCode = getStat.StateCode,
                    StateName = getStat.StateName,
                    CountryCode = getStat.CountryCode
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<user> GetAdmin(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<CRM_getAdmin_Result> getAdm = obj.CRM_getAdmin().ToList();
                return getAdm.Select(getAd => new user
                {
                    first_Name = getAd.first_Name,
                    UserId = getAd.userId
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Branch> GetBranch(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<CRM_GetUserBranch_Result> getBranch = obj.CRM_GetUserBranch("1", "18").ToList();
                return getBranch.Select(getBrnch => new Branch
                {
                    BranchId = Convert.ToInt32(getBrnch.branchId),
                    BranchName = getBrnch.branchName
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }





        public int DeleteCompNewActivityByHistoryId(int historyId, string Connectionstring, string dbName)
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

        public string GetContactIdFrmAcntId(int AccountId)
        {
            try
            {
                var obj = new DevEntities();
                return obj.CRM_GET_ContactIdfrmAcntId(AccountId).FirstOrDefault() ?? "";
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


                var obj = new DevEntities();
                List<Custom_Manage_Master> CustomFieldsList = obj.Custom_Manage_Master.Where(CMM => CMM.Module == Module).ToList();

                return CustomFieldsList;


            }
            catch (Exception)
            {
                throw;
            }

        }


        public bool CheckifStandardColumnExists(string mastercolumnlabel, string Connectionstring, string dbName)
        {
            var Result = false;
            try
            {
                DevEntities obj = new DevEntities();

                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var res = obj.CRM_CheckifStandardColumnExists(mastercolumnlabel);
                //if (res.Count() != 0)
                //{
                //    Result = true;
                //}
                return Result;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public DataSet GetAllCompaniesListToExport(string keyword, string office, string Status, string roleId, string logInId, string salesRep, string filterExpression, int startIndex, int pageSize, string sorting, string Connectionstring)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(Connectionstring))
                {
                    SqlCommand sqlComm = new SqlCommand("CRM_GETALL_COMPANIES_Export", conn);
                    sqlComm.Parameters.AddWithValue("@keyword", keyword);
                    sqlComm.Parameters.AddWithValue("@office", office);
                    sqlComm.Parameters.AddWithValue("@Status", Status);
                    sqlComm.Parameters.AddWithValue("@RoleID", roleId);
                    sqlComm.Parameters.AddWithValue("@logInId", logInId);
                    sqlComm.Parameters.AddWithValue("@salesRep", salesRep);
                    sqlComm.Parameters.AddWithValue("@FilterExpression", filterExpression);
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


        // ZOHO CModel Code

        public List<Company> GetCompaniesIndex(string Keyword, int OwnerID, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                //List<CRM_GetAllAccountContacts_Result> ContactsList = dbContext.CRM_GetAllAccountContacts(jtStartIndex, jtPageSize, jtSorting, output).ToList();
                List<CRM_GetAllCompanies_Result> CompaniesList = dbContext.CRM_GetAllCompanies(Keyword, OwnerID, jtStartIndex, jtPageSize, jtSorting, output).ToList();


                var LstCompanies = CompaniesList.Select(Com => new Company
                {
                    ID = Com.CompanyID.GetValueOrDefault(),
                    Name = Com.CompanyName,
                    Phone = Com.Phone,
                    Website = Com.Website,
                    OwnerID = Com.OwnerID,
                    Ownership = Com.OwnerfirstName + " " + Com.ownerlastname

                    //ID = Con.ContactID.GetValueOrDefault(),
                    //Title = Con.Title,
                    //FirstName = Con.FirstName,
                    //LastName = Con.LastName,
                    //Phone = Con.Phone,
                    //Email = Con.Email,
                    //OwnerID = Con.OwnerID,
                    //OwnerFirstName = Con.OwnerfirstName,
                    //OwnerLastName = Con.ownerlastname,
                    //CompanyID = Con.CompanyID,
                    //CompanyName = Con.CompanyName
                }).ToList();
                RecordCount = Convert.ToInt32(output.Value);
                // RecordCount = Convert.ToInt32(LstCompanies.Count);
                //List<Company> CompaniesList1 = dbContext.Companies.ToList();
                //RecordCount = CompaniesList.Count;
                return LstCompanies;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        ///  For inserting the New Company details
        /// </summary>
        /// <param name="objCompany"></param>
        /// <param name="Connectionstring"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public int InserNewCompany(Company objcompany, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                dbContext.Companies.Add(objcompany);
                dbContext.SaveChanges();
                return objcompany.ID; ;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Company GetCompnayDetailsByID(int id, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);

                Company CmpObj = dbContext.Companies.Where(c => c.ID == id).FirstOrDefault();
                if (CmpObj.ParentCompanyID != null)
                {
                    string parentCompany = dbContext.Companies.Where(pc => pc.ID == CmpObj.ParentCompanyID).Select(PCC => PCC.Name).SingleOrDefault();
                    CmpObj.ParentCompanyName = parentCompany;
                }

                return CmpObj;


            }
            catch (Exception)
            {
                throw;
            }

        }


        public int updateCompanyDetailsByCompanyId(Company objCompany, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                Company getCompanyDetails = dbContext.Companies.Where(c => c.ID == objCompany.ID).SingleOrDefault();

                getCompanyDetails.Name = objCompany.Name;
                getCompanyDetails.ParentCompanyID = objCompany.ParentCompanyID;
                getCompanyDetails.Phone = objCompany.Phone;
                getCompanyDetails.Fax = objCompany.Fax;
                getCompanyDetails.Website = objCompany.Website;
                getCompanyDetails.CompanytypeID = objCompany.CompanytypeID;
                getCompanyDetails.CompanyIndustryID = objCompany.CompanyIndustryID;
                getCompanyDetails.Description = objCompany.Description;
                getCompanyDetails.CompanySite = objCompany.CompanySite;
                getCompanyDetails.CompanyCode = objCompany.CompanyCode;
                getCompanyDetails.AnnualRevenue = objCompany.AnnualRevenue;


                getCompanyDetails.CreatedBy = objCompany.CreatedBy;
                getCompanyDetails.CreatedDate = objCompany.CreatedDate;
                getCompanyDetails.ModifiedBy = objCompany.ModifiedBy;
                getCompanyDetails.ModifiedDate = objCompany.ModifiedDate;
                getCompanyDetails.TrickerSymbol = objCompany.TrickerSymbol;
                getCompanyDetails.Ownership = objCompany.Ownership;
                getCompanyDetails.OwnerID = objCompany.OwnerID;
                getCompanyDetails.Employees = objCompany.Employees;
                getCompanyDetails.SicCode = objCompany.SicCode;
                getCompanyDetails.BillingAddress = objCompany.BillingAddress;
                getCompanyDetails.Billingstreet = objCompany.Billingstreet;
                getCompanyDetails.Billingcity = objCompany.Billingcity;
                getCompanyDetails.BillingstateID = objCompany.BillingstateID;
                getCompanyDetails.BillingcountryID = objCompany.BillingcountryID;
                getCompanyDetails.Billingzip = objCompany.Billingzip;

                getCompanyDetails.MailingAddress = objCompany.MailingAddress;
                getCompanyDetails.Shippingstreet = objCompany.Shippingstreet;
                getCompanyDetails.Shippingcity = objCompany.Shippingcity;
                getCompanyDetails.ShippingstateID = objCompany.ShippingstateID;
                getCompanyDetails.Shippingzip = objCompany.Shippingzip;
                getCompanyDetails.ShippingcountryID = objCompany.ShippingcountryID;
                getCompanyDetails.Naicscode = objCompany.Naicscode;
                getCompanyDetails.CompanyStatusID = objCompany.CompanyStatusID;




                dbContext.SaveChanges();

                return 1;                       
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool IsCompanyExists(Company CompanyObj, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);

                    bool Result = ClientEntity.Companies.Any(c => c.ID != CompanyObj.ID && c.Name.ToLower().Trim() == CompanyObj.Name.ToLower().Trim());
                    // tbl_account ObjAccount = ClientEntity.tbl_account.Where(c => c.AccountId != AccountObj.AccountId && c.Account_Name.ToLower() == AccountObj.Account_Name.ToLower()).FirstOrDefault();
                    //bool Result = ClientEntity.tbl_account.Any(c => c.AccountId != AccountObj.AccountId && c.Account_Name.ToLower() == AccountObj.Account_Name.ToLower());
                    return !Result;


                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InserCompanyActivity(Activity objActivity, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                dbContext.Activity.Add(objActivity);
                dbContext.SaveChanges();
                return true; ;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }  
        }

        public List<Activity> GetListOfallActivities(string keyword,int userid,int roleid,int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                List<CRM_GetAllActivitiesList_Result> LstActivities = obj.CRM_GetAllActivitiesList(keyword,userid,roleid,startIndex, pageSize, orderByClause, output).ToList();

                var activitylist = LstActivities.Select(Acti => new Activity
                    {
                        // ID = Acti.ActivityID ?? 0,
                        ID = Acti.ActivityID.GetValueOrDefault(),
                        ActivityName = Acti.ActivityName,
                        CreatedDate = Acti.CreatedDate,
                        DueDate = Acti.DueDate,
                        Priority = Acti.PriorityName,
                        Status = Acti.StatusName,
                        ModuleName = Acti.AccountTypeName

                    }).ToList();

                TotalCount = Convert.ToInt32(output.Value);



                return activitylist;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public int CompleteActivity(int ActivityId, string Connectionstring, string dbName)
        {
            try
            {
                int res = 1;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                Activity GetActivitybyID = obj.Activity.Where(ac => ac.ID == ActivityId).SingleOrDefault();
                user getuserdetails = obj.users.Where(u => u.UserId == GetActivitybyID.AssignedTo).SingleOrDefault();
                GetActivitybyID.AssignedName=getuserdetails.FirstName + " " + (getuserdetails.LastName!=null?getuserdetails.LastName:"");
                DateTime ActivityCompleteDate = DateTime.Now;
                GetActivitybyID.StatusID = 2;
                GetActivitybyID.CompletedDate = ActivityCompleteDate;
                obj.SaveChanges();
                //return obj.CRM_CompleteActivity(historyId);
                return res;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }  
        }

        public int DeleteCompanyByAccountId(int AccountId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                Company CompanyObj = obj.Companies.Where(c => c.ID == AccountId).SingleOrDefault();
                obj.Companies.Remove(CompanyObj);
                obj.SaveChanges();
                return 1;

                // return obj.CRM_deleteCompanyByAccountId(AccountId);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int DeleteActivityByActivityId(int ActivityId, string Connectionstring, string dbName)
        {
            try
            {
                int res = 1;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                Activity GetActivitybyID = obj.Activity.Where(ac => ac.ID == ActivityId).SingleOrDefault();
                obj.Activity.Remove(GetActivitybyID);
                obj.SaveChanges();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// To get all the contacts based on CompanyID
        /// 
        public List<Account> GetAllContactsbyCompanyID(int CompanyID, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<CRM_GETCONTACTSFORCOMPANY_Result> LstContactsCompanyResults = obj.CRM_GETCONTACTSFORCOMPANY(CompanyID).ToList();
                List<Account> listCompanyContacts = LstContactsCompanyResults.Select(CompContact => new Account
                    {
                        ID = CompContact.ID,
                        FirstName = CompContact.FirstName,
                        LastName = CompContact.LastName,
                        Phone = CompContact.Phone,
                        Email = CompContact.Email,
                        CreatedDate = CompContact.CreatedDate,
                        Title = CompContact.Title
                    }).ToList();

                // List<CRM_GETCRM_COMPANY_CONTACTS_Result> lstCompaniesContactsResult = obj.CRM_GETCRM_COMPANY_CONTACTS(AccountId).ToList();
                //List<tbl_Contact> listCompanyContacts = lstCompaniesContactsResult.Select(CompContact => new tbl_Contact
                //{
                //    ContactId = CompContact.ContactId,
                //    Fname = CompContact.fName,
                //    Phone = CompContact.phone,
                //    Email = CompContact.email,
                //    CreatedDate = CompContact.createdDate,
                //    Title = CompContact.title
                //}).ToList();
                return listCompanyContacts;
            }
            catch (Exception)
            {
                throw;
            }

        }



        public List<CalendarEvent> GetActivityList(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                //List<Activity> LstActivity = new List<Activity>();
                List<CRM_GetNotificationsList_Result> NotificationLst = obj.CRM_GetNotificationsList().ToList();
                var activitylist = NotificationLst.Select(Acti => new Activity
                {
                    // ID = Acti.ActivityID ?? 0,
                    ID = Acti.ActivityID.GetValueOrDefault(),
                    ActivityName = Acti.ActivityName,
                    CreatedDate = Acti.CreatedDate,
                    DueDate = Acti.DueDate,
                    RemindDate = Acti.RemindDate,
                    Priority = Acti.PriorityName,
                    Status = Acti.StatusName,
                    //AccountTypeName = Acti.AccountTypeName

                }).ToList();

             

                var Cevent = (from a in activitylist.AsEnumerable()
                              select new CalendarEvent
                              {
                                  id = Convert.ToInt32(a.ID),
                                  start_date = Convert.ToDateTime(a.DueDate),
                                  end_date = Convert.ToDateTime(a.RemindDate),
                                  text = a.ActivityName,
                                  Module = a.ModuleName

                              }).ToList();

                return Cevent;

                // return MergedActivites;
            }
            catch (Exception)
            {
                throw;
            }

        }


        /// <summary>
        /// To Get all Company Activities Based on the Company ID
        /// </summary>
        /// <param name="CompanyID">CompanyID</param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByClause"></param>
        /// <param name="TotalCount"></param>
        /// <param name="Connectionstring"></param>
        /// <param name="dbName"></param>
        /// <returns>List of all Activities of certain company</returns>
        public List<Activity> GetListOfallCompanyActivitiesbyID(int CompanyID, int Accounttype, string Module, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                //int Accounttype = 3;
                List<CRM_GetActivitiesListByCompanyID_Result> LstcompanyActivities = obj.CRM_GetActivitiesListByCompanyID(CompanyID, Accounttype, Module, startIndex, pageSize, orderByClause, output).ToList();

                // List<CRM_GetAllActivitiesList_Result> LstActivities = obj.CRM_GetAllActivitiesList(startIndex, pageSize, orderByClause, output).ToList();

                var activitylist = LstcompanyActivities.Select(Acti => new Activity
                {
                    // ID = Acti.ActivityID ?? 0,
                    ID = Acti.ActivityID.GetValueOrDefault(),
                    ActivityName = Acti.ActivityName,
                    CreatedDate = Acti.CreatedDate,
                    DueDate = Acti.DueDate,
                    Priority = Acti.PriorityName,
                    Status = Acti.StatusName,
                    //AccountTypeName = Acti.AccountTypeName

                }).ToList();

                TotalCount = Convert.ToInt32(activitylist.Count);



                return activitylist;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<user> getVenturePrefix(Int64 roleid,string Prefix, string Connectionstring, string dbName)
        {
            List<user> lstuser = new List<user>();

            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);

                    lstuser = dbContext.users.Where(v => (v.FirstName.Contains(Prefix) || v.LastName.Contains(Prefix)) && (v.RoleId == roleid)).ToList();
                    return lstuser;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}



