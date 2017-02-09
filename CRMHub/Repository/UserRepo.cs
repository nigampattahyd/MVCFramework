using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRMHub.Repository
{
    public class UserRepo : IUserRepo
    {

        public user CheckpasswordbyUserID(int userId, string password, string Connectionstring, string dbName)
        {

            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    var userobj = dbContext.users.Where(u => u.UserId == userId && u.Password == password).SingleOrDefault();
                    user usr = new user();
                    if (userobj != null && password == userobj.Password)
                    {
                        usr.Password = userobj.Password;

                    }
                    return usr;
                }
            }
            catch
            {
                throw;
            }


        }


        public bool UpdateUserPassword(int userId, string Password, string Connectionstring, string dbName)
        {
            bool isUpdated = false;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    decimal userid = (decimal)userId;
                    int isupdated = dbContext.CRM_UpdatePassword(userid, Password);
                    if (isupdated == 1)
                    {

                        isUpdated = true;
                    }
                    else
                    {
                        isUpdated = false;
                    }
                }
                return isUpdated;
            }
            catch (Exception ex)
            {

            }
            return isUpdated;
        }

        public bool Deleteuser(int userId, string clientid, bool status, string Connectionstring, string dbName)
        {
            bool isDeleted = false;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    user users = dbContext.users.Where(u => u.UserId == userId).SingleOrDefault();
                    users.ClientId = clientid;
                    users.Status = status;
                    dbContext.Entry(users).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    if (users.Status == false)
                    {

                        isDeleted = true;
                    }
                    else
                    {
                        isDeleted = false;
                    }

                }

                return isDeleted;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public user GetUserDetailsbyloginID(int userId, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    var userobj = dbContext.users.Where(u => u.UserId == userId).SingleOrDefault();
                    // List<CRM_GetAdminDetail_Result> lstResult = dbContext.CRM_GetAdminDetail(userId).ToList();
                    user usr = new user();
                    //usr.LoginId = lstResult[0].LoginId;
                    usr.FirstName = userobj.FirstName;
                    usr.LastName = userobj.LastName;
                    usr.Phone = userobj.Phone;
                    usr.AddressLine1 = userobj.AddressLine1;
                    usr.AddressLine2 = userobj.AddressLine2;
                    usr.City = userobj.City;
                    usr.StateCode = userobj.StateCode;
                    usr.PostalCode = userobj.PostalCode;
                    usr.Email = userobj.Email;
                    usr.Fax = userobj.Fax;
                    usr.Cellno = userobj.Cellno;
                    usr.Comment = userobj.Comment;
                    usr.Gender = userobj.Gender;
                    usr.DateOfBirth = userobj.DateOfBirth;
                    usr.PhoneExt = userobj.PhoneExt;
                    //user.Password = EncryptDecrypt.Decrypt(lstResult[0].Password);
                    //usr.Password = userobj.Password;
                    return usr;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateUserDetailsbyloginID(user userobj, string Connectionstring, string dbName, string operation)
        {
            bool isUpdated = false;
            try
            {

                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    //int userid = Convert.ToInt32(userobj.UserId);
                    //var loginid = userobj.LoginId;
                    if (operation == "")
                    {
                        var userdetails = dbContext.CRM_UpdateUserDetails(userobj.UserId, userobj.FirstName, userobj.LastName, userobj.AddressLine1, userobj.AddressLine2, userobj.City,
                                                                userobj.StateCode, userobj.PostalCode, userobj.Phone, userobj.PhoneExt, userobj.Fax, userobj.Comment,
                                                                userobj.Cellno, userobj.DateOfBirth, userobj.Gender);

                        if (userdetails != null)
                        {
                            isUpdated = true;
                        }
                    }
                    if (operation == "Delete")
                    {
                        user getuserDetails = dbContext.users.Where(c => c.UserId == userobj.UserId).SingleOrDefault();
                        userobj.UserId = getuserDetails.UserId;
                        userobj.LoginId = getuserDetails.LoginId;
                        userobj.Password = getuserDetails.Password;
                        userobj.FirstName = getuserDetails.FirstName;
                        
                        if (getuserDetails != null)
                        {
                            getuserDetails.UserId = userobj.UserId;
                            getuserDetails.LoginId = userobj.LoginId;
                            getuserDetails.Password = userobj.Password;
                            getuserDetails.FirstName = userobj.FirstName;
                            getuserDetails.Status = userobj.Status;
                            getuserDetails.ModifiedBy = userobj.ModifiedBy;
                            getuserDetails.LastModified = userobj.LastModified;
                            getuserDetails.ClientId = userobj.ClientId;
                            dbContext.SaveChanges();
                            isUpdated = true;

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw;
            }
            return isUpdated;
        }

        public List<user> GetUserDetails(string Keyword, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                    List<User_GetAllUsers_Result> userlists = dbContext.User_GetAllUsers(Keyword, jtStartIndex, jtPageSize, jtSorting, output).ToList();
                    //List<user> listofuser = new List<user>();
                    var listofuser = userlists.Select(a => new user
                    {
                        FirstName = a.firstName,
                        Phone = a.phone,
                        City = a.city,
                        State = a.stateName,
                        StateCode = a.stateName,
                        stateName = a.stateName,
                        RoleId = a.roleid,
                        Email = a.email,
                        UserId = Convert.ToInt64(a.userId),
                        roleName = a.roleName,
                        BranchId = Convert.ToString(a.branchId),
                        branchName = a.branchname,
                    }).ToList();
                    RecordCount = Convert.ToInt32(output.Value);
                    return listofuser;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CreateNewUser(user objuser, string Connectionstring, string dbName)
        {
            bool iscreated = false;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    objuser.CreatedDate = DateTime.Now;
                    objuser.Status = true;
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    dbContext.users.Add(objuser);
                    dbContext.SaveChanges();
                    //user usr = new user();
                    //usr.ClientId = dbName;
                    //usr.FirstName = objuser.FirstName;
                    //usr.MiddleInitial = objuser.MiddleInitial;
                    //usr.LastName = objuser.LastName;
                    //usr.AddressLine1 = objuser.AddressLine1;
                    //usr.AddressLine2 = objuser.AddressLine2;
                    //usr.City = objuser.City;
                    //usr.StateCode = objuser.StateCode;
                    //usr.PostalCode = objuser.PostalCode;
                    //usr.Phone = objuser.Phone;
                    //usr.PhoneExt = objuser.PhoneExt;
                    //usr.Fax = objuser.Fax;
                    //usr.Email = objuser.Email;
                    //usr.Comment = objuser.Comment;
                    //usr.OutlookEmail = objuser.OutlookEmail;
                    //usr.RoleId = objuser.RoleId;
                    //usr.BranchId = objuser.BranchId;
                    //usr.Cellno = objuser.Cellno;
                    //usr.strDateOfBirth =Convert.ToDateTime(objuser.DateOfBirth).ToString("MM/dd/yyyy");
                    //usr.IsCorporate = objuser.IsCorporate;
                    //usr.IsAllowsedingSMS = objuser.IsAllowsedingSMS;
                    //usr.SMTPEmail = objuser.SMTPEmail;
                    //usr.SMTPPassword = objuser.SMTPPassword;
                    //usr.SMTPAddress = objuser.SMTPAddress;
                    //usr.SMTPPort = objuser.SMTPPort;
                    //usr.LoginId = objuser.LoginId;
                    //usr.Password = objuser.Password;
                    //dbContext.users.Add(usr);
                    //dbContext.SaveChanges();
                    if (objuser != null)
                    {
                        iscreated = true;
                    }
                    else
                    {
                        iscreated = false;
                    }
                    //var result = obj.Mentor_CreateUserDetails(objuser.FirstName, objuser.MiddleInitial, objuser.LastName, objuser.AddressLine1, objuser.AddressLine2, objuser.City,
                    //          objuser.StateCode, objuser.PostalCode, objuser.Phone, objuser.PhoneExt, objuser.Fax, objuser.Email, objuser.Comment, objuser.OutlookEmail, objuser.RoleId, objuser.BranchId,
                    //           objuser.Cellno, objuser.DateOfBirth, objuser.IsCorporate, objuser.IsAllowsedingSMS, objuser.SMTPEmail, objuser.SMTPPassword, objuser.SMTPAddress, objuser.SMTPPort,
                    //           objuser.LoginId, objuser.Password);
                    return iscreated;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public user GetUserDetailsbyUserID(int UserID, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    var userobj = dbContext.users.Where(u => u.UserId == UserID).SingleOrDefault();
                    //List<CRM_GetAdminDetail_Result> lstResult = dbContext.CRM_GetAdminDetail(UserID).ToList();

                    user usr = new user();
                    // usr.LoginId = lstResult[0].LoginId;
                    usr.FirstName = userobj.FirstName;
                    usr.LastName = userobj.LastName;
                    usr.Phone = userobj.Phone;
                    usr.AddressLine1 = userobj.AddressLine1;
                    usr.AddressLine2 = userobj.AddressLine2;
                    usr.City = userobj.City;
                    usr.RoleId = userobj.RoleId;
                    usr.StateCode = userobj.StateCode;
                    usr.PostalCode = userobj.PostalCode;
                    usr.Email = userobj.Email;
                    usr.Fax = userobj.Fax;
                    usr.Cellno = userobj.Cellno;
                    usr.Comment = userobj.Comment;
                    usr.DateOfBirth = userobj.DateOfBirth;
                    usr.OutlookEmail = userobj.OutlookEmail;
                    usr.LoginId = userobj.LoginId;
                    usr.Password = userobj.Password;
                    //user.Password = EncryptDecrypt.Decrypt(lstResult[0].Password);
                    //usr.Password = userobj.Password;
                    return usr;
                }
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateUserDetailsbyUserId(user userobj, string Connectionstring, string dbName)
        {
            bool isUpdated = false;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    userobj.CreatedDate = DateTime.Now;
                    user usr = dbContext.users.Where(u => u.UserId == userobj.UserId).SingleOrDefault();

                   
                    if (usr != null)
                    {
                        usr.ClientId = userobj.ClientId;
                        usr.MiddleInitial = userobj.MiddleInitial;
                        usr.LastName = userobj.LastName;
                        usr.Phone = userobj.Phone;
                        usr.AddressLine1 = userobj.AddressLine1;
                        usr.AddressLine2 = userobj.AddressLine2;
                        usr.City = userobj.City;
                        usr.RoleId = userobj.RoleId;
                        usr.StateCode = userobj.StateCode;
                        usr.PostalCode = userobj.PostalCode;
                        // usr.Email = userobj.Email;
                        usr.Fax = userobj.Fax;
                        usr.Cellno = userobj.Cellno;
                        usr.Comment = userobj.Comment;
                        usr.DateOfBirth = userobj.DateOfBirth;
                        usr.OutlookEmail = userobj.OutlookEmail;
                        usr.LoginId = userobj.LoginId;
                        usr.Status = userobj.Status;
                        //usr.Password = userobj.Password;
                        dbContext.Entry(usr).State = EntityState.Modified;
                        dbContext.SaveChanges();
                        isUpdated = true;
                        if (isUpdated == true)
                        {
                            isUpdated = true;
                        }
                        else
                        {
                            isUpdated = false;
                        }
                    }
                }

                return isUpdated;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool UpdateUserDetailsbyStatus(bool status,long userId, string Connectionstring, string dbName)
        {
            bool isUpdated = false;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                   
                    user usr = dbContext.users.Where(u => u.UserId == userId).SingleOrDefault();
                    usr.Status = status;
                    usr.LastModified = DateTime.Now;
                    usr.ModifiedBy =Convert.ToString(userId);
                    usr.LoginId = usr.LoginId;
                    usr.ClientId = "Mentor";
                    usr.Email = usr.Email;
                    dbContext.SaveChanges();
                 
                }
                return isUpdated = true;
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
    }
}
