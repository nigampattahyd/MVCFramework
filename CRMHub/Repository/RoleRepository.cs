using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Interface;
using System.Data;
using System.Data.Entity;
namespace CRMHub.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public List<role> GetLevels(string Connectionstring, string dbName)
        {
            DevEntities objlevels = new DevEntities(Connectionstring);
            objlevels.Database.Connection.Open();
            objlevels.Database.Connection.ChangeDatabase(dbName);

            List<CRM_GetLevels_Result> lstroles = objlevels.CRM_GetLevels().ToList();
            return lstroles.Select(objRoles => new role
            {
                RoleId = objRoles.RoleId,
                RoleName = objRoles.RoleName,
                Description = objRoles.Description,
                Status = objRoles.Status

            }).ToList();
        }





        public List<role> GetAllRoles(string ContactType, string Status, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                List<role> UserRoleslst = new List<role>();
                UserRoleslst = obj.roles.Where(r => r.RoleId != 18).ToList();
                //UserRoleslst = obj.roles.Where(r => r.Status == "1").ToList();

                TotalCount = Convert.ToInt32(UserRoleslst.Count);
                return UserRoleslst;
            }
            catch
            {
                throw;
            }

        }


        public int createRole(role objrole, string Connectionstring, string dbName)
        {
            try
            {
                int result = 0;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                //obj.roles.Add(objrole);             
                //obj.SaveChanges();
                //return Convert.ToInt32(objrole.RoleId);
                result = obj.CRM_Add_UserRoles(objrole.RoleName, objrole.Status, objrole.Description);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CreateRoleName(role objrole, string Connectionstring, string dbName)
        {
            try
            {
                bool iscreated = false;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                //obj.roles.Add(objrole);             
                //obj.SaveChanges();
                //return Convert.ToInt32(objrole.RoleId);
                var result = obj.CRM_Add_UserRoles(objrole.RoleName, objrole.Status, objrole.Description);
                if (result != null)
                {
                    iscreated = true;
                }

                return iscreated;
            }

            catch (Exception)
            {
                throw;
            }

        }



        public bool DeleteRole(int RoleId, bool status, string Connectionstring, string dbName)
        {
             bool Result = false;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    
                    role deleteObj = dbContext.roles.Where(r => r.RoleId == RoleId).SingleOrDefault();
                    if (status == false)
                    {
                        deleteObj.Status = "0";
                    }
                    dbContext.Entry(deleteObj).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    if (deleteObj.Status== "False")
                    {
                        Result = true;
                    }

                    return Result ;
                }
            }
            catch (Exception)
            {
                return Result;
            }
        }
        

        public role EditRoledetailsbyroleId(int roleId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                role roleObj = new role();
                roleObj = obj.roles.Where(r => r.RoleId == roleId).SingleOrDefault();
                return roleObj;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int UpdateRoledetailsbyroleId(role Role, string Connectionstring, string dbName)
        {
            try
            {
                int res = 1;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                role Objrole = obj.roles.Where(r => r.RoleId == Role.RoleId).SingleOrDefault();
                Objrole.RoleName = Role.RoleName;
                Objrole.Status = Role.Status;
                Objrole.Description = Role.Description;
                obj.SaveChanges();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRoledetails(role Role, string Connectionstring, string dbName)
        {
            bool isUpdated = false;
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                role Objrole = obj.roles.Where(r => r.RoleId == Role.RoleId).SingleOrDefault();
                Objrole.RoleName = Role.RoleName;
                Objrole.Status = Role.Status;
                Objrole.Description = Role.Description;
                obj.SaveChanges();
                isUpdated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isUpdated;
        }


        public bool IsRoleExists(role roleObj, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    bool Result = ClientEntity.roles.Any(r => r.RoleId != roleObj.RoleId && r.RoleName.ToLower().Trim() == roleObj.RoleName.ToLower().Trim());
                    return !Result;


                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// To check whether the role as assigned to any user are not ..
        /// 
        public int GetUserrolecountfordelete(int RoleId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                int userCount = 0;
                //int AccountCount = obj.Accounts.Where(ACC => ACC.CompanyID == CompanyId).Count();            
                //int ActivityCount = obj.Activities.Where(Activity => Activity.AccountID == CompanyId && Activity.AccountTypeID == 3).Count();
                //int Invoicecount = obj.tbl_Invoice.Where(inv => inv.CustomerID == CompanyId).Count();
                //int EstInvoiceCount = obj.tbl_EstimateInvoice.Where(Est => Est.CustomerID == CompanyId).Count();
                //moduleCount = AccountCount + ActivityCount + Invoicecount + EstInvoiceCount;
                userCount = obj.users.Where(usr => usr.RoleId == RoleId).Count();
                return userCount;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<role> GetRolesDetails(string Keyword, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
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
                    List<Roles_GetAllRoles_Result> roleslists = dbContext.Roles_GetAllRoles(Keyword, jtStartIndex, jtPageSize, jtSorting, output).ToList();
                    //List<user> listofuser = new List<user>();
                    var listofroles = roleslists.Select(a => new role
                    {
                        RoleId = Convert.ToInt64(a.RoleId),
                        RoleName = a.RoleName,
                        Description = a.Description,
                        Status = a.status,

                    }).ToList();
                    RecordCount = Convert.ToInt32(output.Value);
                    return listofroles;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool DeleteRoledetailsbyId(int roleId, string Connectionstring, string dbName)
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
                    role roles = dbContext.roles.Where(r => r.RoleId == roleId).SingleOrDefault();
                    if (roles != null)
                    {
                        dbContext.roles.Remove(roles);
                        dbContext.SaveChanges();
                        isDeleted = true;
                    }
                }

                return isDeleted;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool CheckRoleName(string RoleName, string Connectionstring, string dbName)
        {
            bool isExists = false;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    role roles = dbContext.roles.Where(r => r.RoleName == RoleName).SingleOrDefault();
                    if (roles != null)
                    {
                        isExists = false;
                    }
                    return isExists;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    

        public List<role> GetAllMentoringRoles(string Connectionstring, string dbName)
        {
            try
            {
                Connectionstring="Data Source=DataSourceName;Initial Catalog=DatabaseName;Integrated Security=True";
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);

                    List<role> UserRoleslst = new List<role>();

                    UserRoleslst = dbContext.roles.Where(r => r.Status == "1").ToList();


                    return UserRoleslst;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }


}
