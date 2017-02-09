using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRMHub.Repository
{
    public class CommonRepository : ICommonRepository
    {
        public List<usState> getListOfStates(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                //List<usState> getStates = obj.usStates.ToList();
                //return getStates;


                // List<usState> lstState = obj.CRM_GetState().ToList();

                List<CRM_GetState_Result> lstState = obj.CRM_GetState().ToList();
                List<usState> lstStatesCodes = lstState.Select(objstate => new usState
                {
                    CountryCode = objstate.CountryCode,
                    StateCode = objstate.StateCode,
                    StateName = objstate.StateName
                }).ToList();
                return lstStatesCodes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SMSProviderList> getSMSProvider(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<CRM_GetSMSProvider_Result> lstSmsProviderResult = obj.CRM_GetSMSProvider().ToList();
                List<SMSProviderList> lstSmsProvider = lstSmsProviderResult.Select(objSMS => new SMSProviderList { Carrier = objSMS.Carrier, Email = objSMS.emailused, ID = objSMS.ID }).ToList();
                return lstSmsProvider;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<tbl_Menus> getMenus(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<CRM_GetMenus_Result> lstMenusResult = obj.CRM_GetMenus().ToList();
                List<tbl_Menus> lstMenus = lstMenusResult.Select(objMenu => new tbl_Menus { MenuDescription = objMenu.MenuDescription, MenuId = objMenu.MenuId, NavigateURL = objMenu.NavigateURL, ParentMenuId = objMenu.ParentMenuId }).ToList();
                //List<tbl_Menus> lstMenus = lstMenusResult.Select(objMenu => new tbl_Menus { MenuDescription = objMenu.MenuDescription, MenuId = objMenu.MenuId, NavigateURL = objMenu.NavigateURL, ParentMenuId = objMenu.ParentMenuId }).ToList().Where(s => s.IsSelected == false).ToList();
                return lstMenus;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RoleMenuModel> getMenuByRoleId(int RoleId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                RoleMenuModel RoleModelLst = new RoleMenuModel();
                List<CRM_GetMenuByRoleId_Result> lstMenuRoleResult = obj.CRM_GetMenuByRoleId(RoleId).ToList().Where(s => s.IsChecked == false).ToList();


                return lstMenuRoleResult.Select(objResult => new RoleMenuModel
                {
                    menuEntity = new tbl_Menus()
                    {
                        MenuDescription = objResult.MenuDescription,
                        MenuId = objResult.MenuId,
                        NavigateURL = objResult.NavigateURL,
                        ParentMenuId = objResult.ParentMenuId
                    },
                    roleMenuEntity = new tbl_RoleMenus()
                    {
                        RoleId = objResult.RoleId,

                    }
                }).ToList();
                //return RoleModelLst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<tbl_Menus> getDynamicMenuByRoleId(int RoleId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<CRM_GetDynamicMenuByRoleId_Result> lstDynamciRoleResult = obj.CRM_GetDynamicMenuByRoleId(RoleId).ToList();
                return lstDynamciRoleResult.Select(objDynamicResult => new tbl_Menus
                {
                    MenuId = objDynamicResult.MenuId,
                    MenuDescription = objDynamicResult.MenuDescription,
                    ParentMenuId = objDynamicResult.ParentMenuId,
                    ActionName = objDynamicResult.ActionName,
                    ControllerName = objDynamicResult.ControllerName
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertMenuByRole(DataTable dtMenuRole, string Connectionstring, string dbName)
        {
            try
            {
                dtMenuRole.Columns.Remove("RoleMenuId");
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var tabletypeparameter = new SqlParameter("@SelectedMenuIdsDT", SqlDbType.Structured);
                tabletypeparameter.Value = dtMenuRole;
                tabletypeparameter.TypeName = "[dbo].CRM_Type_SelectedMenuIds";
                int output = obj.Database.ExecuteSqlCommand("Exec CRM_InsertMenuForRole @SelectedMenuIdsDT", tabletypeparameter);
                return output;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int GetAcntIdFrmCompName(string CompanyName, out int outCompId, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            try
            {
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("OutCompanyId", typeof(int));
                int id = obj.CRM_GetAccountIdFromCompanyName(CompanyName, output);
                outCompId = Convert.ToInt32(output.Value);
                return outCompId;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                obj.Database.Connection.Close();
            }
        }

        public int GetUserIdFrmNamepName(string UserName, out int outUserId, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            try
            {
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("outUserId", typeof(int));
                int id = obj.CRM_GetUSERIdFromUSERName(UserName, output);
                outUserId = Convert.ToInt32(output.Value);
                return outUserId;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                obj.Database.Connection.Close();
            }
        }

        // for contacts


        //public int GetContIdFrmFName(string cntname, string cntLName, string email, string Connectionstring, string dbName)
        //{
        //    DevEntities obj = new DevEntities(Connectionstring);
        //    try
        //    {

        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        tbl_Contact ContactEntity = new tbl_Contact();
        //        if (cntname != "" && cntLName != "" && email != "")
        //        {
        //            ContactEntity = obj.tbl_Contact.Where(tblc => tblc.Fname == cntname && tblc.LName == cntLName && tblc.Email == email).FirstOrDefault();
        //        }
        //        else if (cntname == "" && cntLName != "" && email != "")
        //        {
        //            ContactEntity = obj.tbl_Contact.Where(tblc => tblc.LName == cntLName && tblc.Email == email).FirstOrDefault();
        //        }
        //        else if (cntname != "" && cntLName == "" && email != "")
        //        {
        //            ContactEntity = obj.tbl_Contact.Where(tblc => tblc.Fname == cntname && tblc.Email == email).FirstOrDefault();
        //        }
        //        else if (cntname != "" && cntLName != "" && email == "")
        //        {
        //            ContactEntity = obj.tbl_Contact.Where(tblc => tblc.Fname == cntname && tblc.LName == cntLName).FirstOrDefault();
        //        }
        //        else if (cntname != "" && cntLName == "" && email == "")
        //        {
        //            ContactEntity = obj.tbl_Contact.Where(tblc => tblc.Fname == cntname).FirstOrDefault();
        //        }
        //        else if (cntname == "" && cntLName != "" && email == "")
        //        {
        //            ContactEntity = obj.tbl_Contact.Where(tblc => tblc.LName == cntLName).FirstOrDefault();
        //        }
        //        else if (cntname == "" && cntLName == "" && email != "")
        //        {
        //            ContactEntity = obj.tbl_Contact.Where(tblc => tblc.Email == email).FirstOrDefault();
        //        }


        //        int ConId = ContactEntity.ContactId;

        //        return ConId;

        //        //tbl_Contact ContactEntity = obj.tbl_Contact.Where(tblc => tblc.Fname == cntname && tblc.LName == cntLName && tblc.Email == email).FirstOrDefault();
        //        //int ConId = ContactEntity.ContactId;
        //        //int ConId = obj.tbl_Contact.Max(c => c.ContactId);

        //        //var cntid = obj.CRM_GetContactIdfrmFName(cntname);         
        //        //int? cntid1 = cntid.FirstOrDefault();
        //        //return cntid1==null?0 :Convert.ToInt32(cntid1);

        //        //  return ConId; 


        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        obj.Database.Connection.Close();
        //    }
        //}

        public List<usState> getListOfStatecode(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                List<CRM_GetStatecode_Result> lstState = obj.CRM_GetStatecode().ToList();
                List<usState> lstStatesCodes = lstState.Select(objstate => new usState
                {
                    //CountryCode=objstate.CountryCode,
                    StateCode = objstate.StateCode,
                    StateName = objstate.StateName
                }).ToList();
                return lstStatesCodes;


            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<tbl_RoleMenus> getAllMenuByRoleId(int RoleId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                List<tbl_RoleMenus> MenusList = obj.tbl_RoleMenus.Where(m => m.RoleId == RoleId).ToList();
                return MenusList;
                //List<CRM_GetMenuByRoleId_Result> lstMenuRoleResult = obj.CRM_GetMenuByRoleId(RoleId).ToList();
                //return lstMenuRoleResult.Select(objResult => new RoleMenuModel
                //{
                //    menuEntity = new tbl_Menus()
                //    {
                //        MenuDescription = objResult.MenuDescription,
                //        MenuId = objResult.MenuId,
                //        NavigateURL = objResult.NavigateURL,
                //        ParentMenuId = objResult.ParentMenuId
                //    },
                //    roleMenuEntity = new tbl_RoleMenus()
                //    {
                //        RoleId = objResult.RoleId
                //    }
                //}).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }



        public bool DeleteAccessmenuForRold(int UserRoleId, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                //obj.tbl_RoleMenus.AddRange(list);
                //obj.SaveChanges();


                var results = obj.tbl_RoleMenus.Where(rm => rm.RoleId == UserRoleId).ToList();
                if (results.Count > 0)
                {
                    obj.tbl_RoleMenus.RemoveRange(results);
                    obj.SaveChanges();

                }

                return true;

            }
            catch (Exception)
            {
                throw;

            }

        }

        public bool insertAccessmenuForRold(List<tbl_RoleMenus> ObjRoleMenus, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            obj.Database.Connection.Open();
            obj.Database.Connection.ChangeDatabase(dbName);
            obj.tbl_RoleMenus.AddRange(ObjRoleMenus);
            obj.SaveChanges();
            return true;
        }


        public List<State> GetAllStatesList(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<State> LstState = obj.States.ToList();
                List<State> lstStatesCodes = LstState.Select(objstate => new State
                {
                    CountryID = objstate.CountryID,
                    ID = objstate.ID,
                    StateName = objstate.StateName,
                    FullStateName = objstate.Abbreviation + '-' + objstate.StateName,
                    Abbreviation = objstate.Abbreviation
                }).ToList();


                return lstStatesCodes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Country> GetAllCountriesList(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                List<Country> LstCountry = obj.Countries.ToList();


                return LstCountry;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public int GetCountrybyStateId(int StateId, string Connectionstring, string dbName)
        {
            try
            {
                int CountryID = 0;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                CountryID = obj.States.Where(S => S.ID == StateId).Select(coun => coun.CountryID).FirstOrDefault() ?? 0;

                return CountryID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Zoho
        public List<Company> GetListOfAllCompanies(int startIndex, int pageSize, string sorting, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                List<Company> CompanyLst = new List<Company>();
                CompanyLst = obj.Companies.ToList();
                TotalCount = Convert.ToInt32(output.Value);

                return CompanyLst;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<user> getUsersLookup(int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                string name = null;
                string email = null;
                string city = null;
                string phone = null;
                string keyword = null;
                string state = null;
                string zip = null;
                string level = "";
                string branch = "All";
                string loginId = "1";
                long roleId = 18;
                List<CRM_SearchAdminList_Result> userlists = obj.CRM_SearchAdminList(name, email, city, phone, keyword, state, zip, level, branch, loginId, roleId, jtStartIndex, jtPageSize, jtSorting, output).ToList();
                RecordCount = Convert.ToInt32(output.Value);
                List<user> listofuser = new List<user>();
                listofuser = userlists.Select(a => new user
                {
                    FirstName = a.fullName,
                    Phone = a.phone,
                    City = a.city,
                    State = a.stateName,
                    RoleId = a.roleid,
                    Email = a.email,
                    UserId = Convert.ToInt64(a.userId),
                    roleName = a.roleName,
                    BranchId = Convert.ToString(a.branchId),
                    branchName = a.branchname
                }).ToList();

                var listofusers = listofuser.Where(y => y.RoleId != 10).ToList();
                return listofuser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Account> GetListOfAllCContacts(int startIndex, int pageSize, string sorting, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                List<Account> ContactLst = new List<Account>();
                ContactLst = obj.Accounts.Where(a => a.AccountTypeId == 2).ToList();
                TotalCount = Convert.ToInt32(output.Value);

                return ContactLst;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public string GetusernameById(int userId, string Connectionstring, string dbName)
        {
            try
            {
                string UserFullName = string.Empty;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                user userobj = obj.users.Where(u => u.UserId == userId).SingleOrDefault();
                UserFullName = userobj.FirstName + " " + userobj.LastName;

                return UserFullName;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<activity_types> GetAllActivityType(string Connectionstring, string dbName)
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

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Status> GetAllStatusTypes(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var Statuslist = obj.Status.ToList();
                return Statuslist.Select(Sta => new Status
                    {
                        ID = Sta.ID,
                        StatusName = Sta.StatusName
                    }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Priority> GetAllPrioriTypes(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var Prioritylist = obj.Priorities.ToList();
                return Prioritylist.Select(pri => new Priority
                    {
                        ID = pri.ID,
                        PriorityName = pri.PriorityName
                    }).ToList();


            }
            catch (Exception)
            {
                throw;
            }
        }



        public List<ModuleType> GetAllModuleType(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var accounttypelist1 = obj.ModuleType.ToList();
                //var accounttypelist = obj.ModuleType.Where(Act => Act.ID != 1).ToList();
                var accounttypelist = obj.ModuleType.ToList();

                return accounttypelist.Select(ACC => new ModuleType
                {
                    ID = ACC.ID,
                    AccountTypeName = ACC.AccountTypeName
                }).ToList();


            }
            catch (Exception)
            {
                throw;
            }

        }

        public Activity GetActivityDetailsByID(int id, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);

                var result = dbContext.CRM_ActivityDetailsByID(id).Select(activ => new Activity
                    {
                        ID = activ.ActivityID,
                        ActivityName = activ.ActivityName,
                        ActivityTypeID=activ.ActivityTypeID,
                        AssignedTo = activ.AssignedTo,
                        AssignedName = activ.AssignedToName,
                        CompletedDate = activ.CompletedDate,
                        DueDate = activ.DueDate,
                        RemindDate = activ.RemindDate,
                        ModuleID=activ.ModuleID,
                        Createdby = activ.Createdby,
                        CreatedbyName = activ.creator,
                        CreatedDate = activ.CreatedDate,
                        Modifiedby = activ.Modifiedby,
                        ModifiedbyName = activ.modifier,
                        ModifiedDate = activ.ModifiedDate,
                        NotificationFlag = activ.NotificationFlag,
                        PriorityID = activ.PriorityID,
                        StatusID = activ.StatusID,
                        Notes = activ.Notes

                    }).SingleOrDefault();


                return result;

            }
            catch (Exception)
            {
                throw;
            }

        }



        public int updateActivityDetailsByActivityId(Activity objActivity, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);

                Activity GetActivityDetails = dbContext.Activity.Where(act => act.ID == objActivity.ID).SingleOrDefault();
                user getuserdetails = dbContext.users.Where(u => u.UserId == GetActivityDetails.AssignedTo).SingleOrDefault();
                GetActivityDetails.AssignedName = getuserdetails.FirstName + " " + (getuserdetails.LastName != null ? getuserdetails.LastName : "");
                GetActivityDetails.Modifiedby = objActivity.Modifiedby;
                GetActivityDetails.ModifiedDate = objActivity.ModifiedDate;
                GetActivityDetails.Notes = objActivity.Notes;
                GetActivityDetails.NotificationFlag = objActivity.NotificationFlag;
                GetActivityDetails.ActivityTypeID = objActivity.ActivityTypeID;
                GetActivityDetails.PriorityID = objActivity.PriorityID;
                GetActivityDetails.StatusID = objActivity.StatusID;
                GetActivityDetails.RemindDate = objActivity.RemindDate;
                GetActivityDetails.DueDate = objActivity.DueDate;
                GetActivityDetails.ModuleID = objActivity.ModuleID;
                GetActivityDetails.AssignedTo = objActivity.AssignedTo;
                GetActivityDetails.Createdby = objActivity.Createdby;
                dbContext.SaveChanges();

                return 1;
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



       
        public List<Account> GetListOfOpportunitiesList(string Keyword, int Companyid, int ownerid, int stageId, int startIndex, int pageSize, string sorting, out int TotalCount, string Connectionstring, string dbName)
        {
            List<Account> OpporList = new List<Account>();
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                //var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));


                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                List<CRM_GetAllAccountOpportunities_Result> OpporLst = obj.CRM_GetAllAccountOpportunities(Keyword,Companyid,ownerid,stageId, startIndex, pageSize, sorting, output).ToList();




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
                TotalCount = Convert.ToInt32(output.Value);
                // RecordCount = Convert.ToInt32(Opportunitylist.Count);               
                


                //List<CRM_GetAllAccountOpportunities_Result> OpporLst = obj.CRM_GetAllAccountOpportunities(startIndex, pageSize, sorting, output).ToList();

                OpporList = obj.Accounts.ToList();

                //List<Company> CompanyLst = new List<Company>();
                //CompanyLst = obj.Companies.ToList();
                TotalCount = Convert.ToInt32(output.Value);

                //return Opportunitylist;

                return OpporList;
            }
            catch (Exception)
            {
                throw;
            }
        }

       
        public int GetcompanyIdbyName(string CompanyName, string Connectionstring, string dbName)
        {
            try
            {
                int CompanyID = 0;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                CompanyID = obj.Companies.Where(c => c.Name.Replace(" ", "").ToLower() == CompanyName.Replace(" ", "").ToLower()).Select(CID => CID.ID).FirstOrDefault();
                // CompanyID = obj.Companies.Where(c => c.Name.ToLower().TrimStart() == CompanyName.ToLower().TrimStart()).Select(CID => CID.ID).FirstOrDefault();


                return CompanyID;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public int GetStateIdFrmStateName(string StateName, out int outStateId, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            try
            {
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("OutStateId", typeof(int));
                int id = obj.CRM_GetStateIdFromStateName(StateName, output);
                outStateId = Convert.ToInt32(output.Value);
                return outStateId;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                obj.Database.Connection.Close();
            }
        }


        public int GetCountryIdFrmCountryName(string CountryName, out int outCountryId, string Connectionstring, string dbName)
        {
            DevEntities obj = new DevEntities(Connectionstring);
            try
            {
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("OutCountryId", typeof(int));
                int id = obj.CRM_GetCountryIdFromCountryName(CountryName, output);
                outCountryId = Convert.ToInt32(output.Value);
                return outCountryId;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                obj.Database.Connection.Close();
            }
        }

        public string GetCompanynameById(int companyId, string Connectionstring, string dbName)
        {
            try
            {
                string CompanyName = string.Empty;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                Company companyObj = obj.Companies.Where(c => c.ID == companyId).SingleOrDefault();
                CompanyName = companyObj.Name;            

                return CompanyName;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string GetOpportunitynameById(int OpportunityId, string Connectionstring, string dbName)
        {
            try
            {
                string OpportunityName = string.Empty;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                Account accountObj = obj.Accounts.Where(a => a.ID == OpportunityId && a.AccountTypeId == 4).SingleOrDefault();

               // Company companyObj = obj.Companies.Where(c => c.ID == companyId).SingleOrDefault();
                OpportunityName = accountObj.Name;

                return OpportunityName;
            }
            catch (Exception)
            {
                throw;
            }
        }


     

        public Account GetContactDetailsbyId(int ContactId, string Connectionstring, string dbName)
        {
            try
            {
                string OpportunityName = string.Empty;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                Account accountObj = obj.Accounts.Where(a => a.ID == ContactId && a.AccountTypeId == 2).SingleOrDefault();
               // OpportunityName = accountObj.FirstName + " " + accountObj.LastName;

                return accountObj;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Ment
        public bool IsEmailIdExists(string Emailid, string Connectionstring, string dbName)
        {
            var flag = false;

            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    var useremail = dbContext.users.Where(u => u.Email == Emailid ).FirstOrDefault();
                    if (useremail != null) // update
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                    return flag;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public List<CollaboratorAffiliation> GetAllCollaborationList(string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        List<CollaboratorAffiliation> Lstcollaborator = obj.CollaboratorAffiliations.ToList();

        //        return Lstcollaborator;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
     
    }
}
