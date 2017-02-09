using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;
using CRMHub.EntityFramework;
namespace CRMHub.Repository
{
    public class OfficeLevelAccessRepository : IOfficeLevelAccessRepository
    {
        public List<user> GetUser(string name, string email, string roleId, string loginId, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities objlevels = new DevEntities(Connectionstring);
                objlevels.Database.Connection.Open();
                objlevels.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                List<CRM_GetUsersByRole_Result> lstroles = objlevels.CRM_GetUsersByRole(name,email,(roleId), loginId,jtStartIndex,jtPageSize,jtSorting,output).ToList();
                RecordCount = Convert.ToInt32(output.Value);
                return lstroles.Select(objusers => new user
                {
                    UserId =Convert.ToInt64(objusers.userId),
                    FirstName = objusers.fullName,
                    BranchId = objusers.branchId,
                    branchName=objusers.branchName,
                    RoleId =Convert.ToInt64(objusers.RoleID)
                }).ToList();
            }
            catch (Exception)
            {                
                throw;
            }      
        }

        public user EditUserdetailsbyuserId(int userId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                user user = new user();
                List<CRM_GetAdminDetail_Result> lstResult = obj.CRM_GetAdminDetail(userId).ToList();                
                    user.FirstName = lstResult[0].Firstname;
                    user.MiddleInitial =lstResult[0].MiddleInitial;
                    user.LastName = lstResult[0].LastName;
                    user.RoleId = lstResult[0].RoleId;
                    user.BranchId = lstResult[0].BranchId;
                    user.Phone = lstResult[0].Phone;
                    user.AddressLine1 =lstResult[0].AddressLine1;
                    user.AddressLine2 =lstResult[0].AddressLine2;
                    user.City = lstResult[0].City;
                    user.StateCode = lstResult[0].StateCode;
                    user.PostalCode = lstResult[0].PostalCode;
                    user.Email = lstResult[0].Email;
                    user.Fax = lstResult[0].Fax;
                    user.Cellno =lstResult[0].Cellno;
                    user.Comment = lstResult[0].Comment;
                    user.LoginId = lstResult[0].LoginId;
                    user.Password =lstResult[0].Password;
                    user.IsAllowsedingSMS =lstResult[0].IsAllowsedingSMS;
                    user.OutlookEmail = lstResult[0].OutlookEmail;
                    user.SMTPEmail =lstResult[0].SMTPEmail;
                    user.SMTPPassword =lstResult[0].SMTPPassword;
                    user.SMTPAddress = lstResult[0].SMTPAddress;
                    user.SMTPPort = lstResult[0].SMTPPort;
                    return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Branch> GetUserBranch(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<CRM_GetBranchListAll_Result> userbranchlist = obj.CRM_GetBranchListAll().ToList();
                return userbranchlist.Select(userbranch => new Branch
                {
                    BranchId = userbranch.branchId,
                    BranchName = userbranch.branchName
                }).ToList();
            }
            catch (Exception)
            {                
                throw;
            }       
        }

        public int UpdateUserdetailsbyuserId(user User, string Connectionstring, string dbName)
        {
            try
            {
                int userresult = 0;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                userresult = obj.CRM_UpdateAdminDetail(User.UserId, User.FirstName, User.MiddleInitial, User.LastName, User.AddressLine1, User.AddressLine2, User.City,
                                                        User.StateCode, User.PostalCode, User.Phone, User.PhoneExt, (User.Fax), User.Email, User.Comment, User.OutlookEmail, User.RoleId, User.BranchId,
                                                        User.Cellno, User.LoginId, User.Password,  User.IsAllowsedingSMS, User.SMTPEmail, User.SMTPPassword, User.SMTPAddress,
                                                        User.SMTPPort);
                return userresult;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public int DeleteUserDetailsByuserid(int userId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                return obj.CRM_DeleteCandidate(userId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Branch GetBranchDetailsByBranchName(int branchid, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var result = obj.CRM_getBranchDetailByBranchId(branchid).Select(branch => new Branch
                {
                    BranchAddress1 = branch.branchAddress1,
                    BranchAddress2 = branch.branchAddress2,
                    BranchCity = branch.branchCity,
                    BranchEmail = branch.branchEmail,
                    BranchFax = branch.branchFax,
                    BranchId = branch.branchId,
                    BranchName = branch.branchName,
                    BranchPhone = branch.branchPhone,
                    BranchPhoneExt = branch.branchPhoneExt,
                    BranchStateCode = branch.branchStateCode,
                    BranchType = branch.BranchType,
                    BranchZip = branch.branchZip,
                    Comment = branch.comment,
                    CreatedDate = branch.createdDate,
                    ModifiedBy = branch.modifiedBy,
                    RoutingEmail = branch.routingEmail,
                    StateOther = branch.stateOther
                }).SingleOrDefault();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckEmailExistsareNot(string email, string Connectionstring, string dbName)
        {
            var flag = false;
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var emailresult = obj.CRM_EmailExistsOrNot(email);
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
    }
}