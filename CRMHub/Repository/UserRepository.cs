using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Repository
{
    public class UserRepository:IUserRepository
    {
        public user getLoggedUser(string userName, string password, string Connectionstring, string dbName)
        {
            try
            {
                var obj = new DevEntities();
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                List<CRM_SecureLogin_Result> userResult = obj.CRM_SecureLogin(userName, password).ToList();
                user UserObj = userResult.Select(objuserResult => new user
                {
                    AccountId = objuserResult.AccountId,
                    AddressLine1 = objuserResult.AddressLine1,
                    AddressLine2 = objuserResult.AddressLine2,
                    BranchId = objuserResult.BranchId,
                    Cellno = objuserResult.Cellno,
                    UserId = objuserResult.UserId,
                    LoginId = objuserResult.LoginId,
                    Password = objuserResult.Password,
                    FirstName = objuserResult.FirstName,
                    MiddleInitial = objuserResult.MiddleInitial,
                    LastName = objuserResult.LastName,
                    Phone = objuserResult.Phone,
                    PhoneExt = objuserResult.phoneExt,
                    Fax = objuserResult.Fax,
                    Email = objuserResult.Email,
                    City = objuserResult.City,
                    State = objuserResult.State,
                    PostalCode = objuserResult.PostalCode,
                    Comment = objuserResult.Comment,
                    CreatedDate = objuserResult.CreatedDate,
                    LastModified = objuserResult.LastModified,
                    StateCode = objuserResult.StateCode,
                    RoleId = objuserResult.RoleId,
                    Status = objuserResult.Status,
                    ModifiedBy = objuserResult.ModifiedBy,
                    ContactId = objuserResult.ContactId,
                    DateOfBirth = objuserResult.DateOfBirth,
                    IsAllowsedingSMS = objuserResult.IsAllowsedingSMS,
                    SMTPEmail = objuserResult.SMTPEmail,
                    SMTPPassword = objuserResult.SMTPPassword,
                    SMTPAddress = objuserResult.SMTPAddress,
                    SMTPPort = objuserResult.SMTPPort,
                    OutlookEmail = objuserResult.OutlookEmail,
                    Gender = objuserResult.Gender,
                    roleName = objuserResult.RoleName,
                    VentureId=objuserResult.VentureId,
                    MentorId=objuserResult.MentorId
                }).SingleOrDefault();                
                return UserObj;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public List<user> getListOfUsers(string name, string email, string city, string phone, string keyword, string state, string zip, string level, string branch, string loginId, long roleId, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                if (state == "AA")
                    state = null;
                if (branch == "-1")
                    branch = "All";
                if (zip == string.Empty)
                    zip = null;
                List<CRM_SearchAdminList_Result> userlists = obj.CRM_SearchAdminList(name, email, city, phone, keyword, state, zip, level, branch,loginId,roleId,jtStartIndex,jtPageSize,jtSorting,output).ToList();
                RecordCount = Convert.ToInt32(output.Value);
                List<user> listofuser = new List<user>();
                listofuser = userlists.Select(a => new user
                {
                    FirstName = a.fullName,
                    Phone = a.phone,
                    City = a.city,
                    State = a.stateName,
                    RoleId =a.roleid,
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

        public List<Branch> getUserbranchlist(string loginid, string roleid, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<CRM_GetUserBranch_Result> listofbranch = obj.CRM_GetUserBranch(loginid, roleid).ToList();
                listofbranch = listofbranch.ToList();
                List<Branch> resultofbranch = new List<Branch>();
                resultofbranch = listofbranch.Select(a => new Branch
                {
                    BranchAddress1 = a.branchAddress1,
                    BranchAddress2 = a.branchAddress2,
                    BranchCity = a.branchCity,
                    BranchEmail = a.branchEmail,
                    BranchFax = a.branchFax,
                    BranchId =Convert.ToInt32(a.branchId),
                    BranchName = a.branchName,
                    BranchPhone = a.branchPhone,
                    BranchPhoneExt = a.branchPhoneExt,
                    BranchStateCode = a.branchStateCode,
                    BranchZip = a.branchZip,
                    Comment = a.comment,
                    CreatedDate = a.createdDate,
                    ModifiedBy = a.modifiedBy,
                    RoutingEmail = a.routingEmail,
                    StateOther = a.stateOther,
                    StateName = a.stateName
                }).ToList();
                return resultofbranch;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<user> getAdminList(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                //List<user> listUser1res = obj.users.Where(u => u.RoleId == 8).ToList();
                //List<user> listUser1 = listUser1res.Select(obju => new user { UserId = obju.UserId, FirstName = obju.FirstName }).ToList();
                List<CRM_getAdmin_Result> listUserResult = obj.CRM_getAdmin().ToList();
                List<user> listUser = listUserResult.Select(objUser => new user { UserId = objUser.userId, FirstName = objUser.first_Name }).ToList();
                return listUser;   
            }
            catch (Exception)
            {                
                throw;
            }
        }


        public List<user> getSalesRepList(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                List<user> listUser1res = obj.users.Where(u => u.RoleId == 8).ToList();
                List<user> listUser1 = listUser1res.Select(obju => new user { UserId = obju.UserId, FirstName = obju.FirstName }).ToList();
                //List<CRM_getAdmin_Result> listUserResult = obj.CRM_getAdmin().ToList();
                //List<user> listUser = listUserResult.Select(objUser => new user { UserId = objUser.userId, FirstName = objUser.first_Name }).ToList();
                return listUser1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CreateNewUser(user objuser, string Connectionstring, string dbName)
        {
            try
            {
                int result = 0;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                result = obj.CRM_procCreateAdmin(objuser.FirstName, objuser.MiddleInitial, objuser.LastName, objuser.AddressLine1, objuser.AddressLine2, objuser.City,
                          objuser.StateCode, objuser.PostalCode, objuser.Phone, objuser.PhoneExt,objuser.Fax, objuser.Email, objuser.Comment,objuser.OutlookEmail, objuser.RoleId, objuser.BranchId,
                           objuser.Cellno, objuser.IsCorporate, objuser.IsAllowsedingSMS, objuser.SMTPEmail, objuser.SMTPPassword, objuser.SMTPAddress, objuser.SMTPPort,
                           objuser.LoginId, objuser.Password).FirstOrDefault()??-1;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string NewUserName(user objnewuser, string Connectionstring, string dbName)
        {
            try
            {
                string newusername = string.Empty;
                if (objnewuser.LastName == null)
                    objnewuser.LastName = "";
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                newusername = obj.CRM_Get_Username(objnewuser.FirstName, objnewuser.LastName).FirstOrDefault();
                return newusername;
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
                user.MiddleInitial = lstResult[0].MiddleInitial;
                user.LastName = lstResult[0].LastName;
                user.RoleId = lstResult[0].RoleId;
                user.BranchId = lstResult[0].BranchId;
                user.Phone = lstResult[0].Phone;
                user.AddressLine1 = lstResult[0].AddressLine1;
                user.AddressLine2 = lstResult[0].AddressLine2;
                user.City = lstResult[0].City;
                user.StateCode = lstResult[0].StateCode;
                user.PostalCode = lstResult[0].PostalCode;
                user.Email = lstResult[0].Email;
                user.Fax =lstResult[0].Fax;
                user.Cellno = lstResult[0].Cellno;
                user.Comment = lstResult[0].Comment;
                user.LoginId = lstResult[0].LoginId;
                //user.Password = EncryptDecrypt.Decrypt(lstResult[0].Password);
                user.Password = lstResult[0].Password;
                user.IsAllowsedingSMS = lstResult[0].IsAllowsedingSMS;
                user.OutlookEmail = lstResult[0].OutlookEmail;
                user.SMTPEmail = lstResult[0].SMTPEmail;
                user.SMTPPassword = lstResult[0].SMTPPassword;
                user.SMTPAddress = lstResult[0].SMTPAddress;
                user.SMTPPort = lstResult[0].SMTPPort;
                return user;
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
                if (User.LastName ==null)
                    User.LastName = string.Empty;
                userresult = obj.CRM_UpdateAdminDetail(User.UserId, User.FirstName, User.MiddleInitial, User.LastName, User.AddressLine1, User.AddressLine2, User.City,
                                                        User.StateCode, User.PostalCode, User.Phone, User.PhoneExt, User.Fax, User.Email, User.Comment, User.OutlookEmail, User.RoleId, User.BranchId,
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
            catch (Exception)
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
                if (emailresult.Count() !=0)
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

        public List<user> getAdminListByBranchId(int branchId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                //List<CRM_getAdminAD_Result> listUserResult = obj.CRM_getAdminAD(branchId).ToList();
                List<CRM_getUsers_Result> listUserResult = obj.CRM_getUsers(branchId).ToList();


                List<user> listUser = listUserResult.Select(objUser => new user { UserId = objUser.userId, FirstName = objUser.first_Name }).ToList();
                return listUser;
            }
            catch (Exception)
            {
                throw;
            }
        }




        public bool CheckLoginExistsareNot(user userObj, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    bool Result = ClientEntity.users.Any(u => u.UserId != userObj.UserId && u.LoginId.ToLower() == userObj.LoginId.ToLower());                  
                    return !Result;
                    

                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool CheckEmailExistsareNot(user userObj, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    bool Result = ClientEntity.users.Any(u => u.UserId != userObj.UserId && u.Email.ToLower() == userObj.Email.ToLower());
                    return !Result;


                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public user GetPassWord(string email, string Connectionstring, string dbName)
        {
            try
            {

                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var userobj = obj.users.Where(u => u.Email == email).SingleOrDefault();
                user user = new user();
                if (userobj != null && email == userobj.Email)
                {
                    user.Email = userobj.Email;
                    user.Password = userobj.Password;
                    user.FirstName = userobj.FirstName;
                    user.LoginId = userobj.LoginId;
                }

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
