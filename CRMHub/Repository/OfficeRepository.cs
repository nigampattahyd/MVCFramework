using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
namespace CRMHub.Repository
{
    public class OfficeRepository : IOfficeRepository
    {
        public List<Branch> getListOfOffice(string name, string email, string city, string phone, string keyword, string state, string zip, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                 var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                List<CRM_GetAllBranchList_Result> listOfBranchResult = obj.CRM_GetAllBranchList(name,email,city,phone,keyword,state,zip,jtStartIndex,jtPageSize,jtSorting,output).ToList();
                RecordCount = Convert.ToInt32(output.Value);
                List<Branch> listOfBranch = new List<Branch>();
                listOfBranch = listOfBranchResult.Select(a => new Branch
                {
                    BranchId=Convert.ToInt32(a.branchId),
                    BranchAddress1 = a.branchAddress1,
                    BranchCity = a.branchCity,
                    BranchEmail = a.branchEmail,
                    BranchName = a.branchName,
                    BranchPhone = a.branchPhone,
                    StateName=a.stateName
                }).ToList();            
                return listOfBranch;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public int createBranch(Branch objBranch, string Connectionstring, string dbName)
        {
            try
            {
                int result = 0;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                result = obj.CRM_CreateBranch(objBranch.BranchName, objBranch.BranchAddress1, objBranch.BranchAddress2, objBranch.BranchCity, objBranch.BranchStateCode,
                     objBranch.BranchZip, objBranch.BranchPhone, objBranch.BranchPhoneExt, objBranch.BranchFax, objBranch.BranchEmail, objBranch.Comment,
                     objBranch.StateOther, objBranch.RoutingEmail, objBranch.BranchType);
                return result;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public Branch editBranchByBranchID(int branchId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);                
                var result = obj.CRM_getBranchDetailByBranchId(branchId).Select(branch => new Branch
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

        public int updateBranchByBranchId(Branch objBranch, string Connectionstring, string dbName)
        {
            try
            {
                int result = 0;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                result = obj.CRM_UpdateBranchDetailByBranchId(objBranch.BranchId,objBranch.BranchName, objBranch.BranchAddress1, objBranch.BranchAddress2, objBranch.BranchCity, objBranch.BranchStateCode,
                     objBranch.BranchZip, objBranch.BranchPhone, objBranch.BranchPhoneExt, objBranch.BranchFax, objBranch.BranchEmail, objBranch.Comment,
                     objBranch.StateOther, objBranch.RoutingEmail, objBranch.BranchType);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteBranchByBranchID(int branchId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                return obj.CRM_deleteBranchByBranchId(branchId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Branch> getUserBranch(string loginId, string roleId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);                
                List<CRM_GetUserBranch_Result> listUserBranchResult = obj.CRM_GetUserBranch(loginId, roleId).ToList();
                List<Branch> listUserBranch = listUserBranchResult.Select(userObj => new Branch
                {
                    BranchId = Convert.ToInt32(userObj.branchId),
                    BranchName = string.IsNullOrEmpty(userObj.branchName) ? string.Empty : userObj.branchName,
                    BranchAddress1 = string.IsNullOrEmpty(userObj.branchAddress1)?string.Empty:userObj.branchAddress1,
                    BranchAddress2 = string.IsNullOrEmpty(userObj.branchAddress2) ? string.Empty : userObj.branchAddress2,
                    BranchCity = string.IsNullOrEmpty(userObj.branchCity) ? string.Empty : userObj.branchCity,
                    BranchStateCode = string.IsNullOrEmpty(userObj.branchStateCode) ? string.Empty : userObj.branchStateCode,
                    BranchZip = string.IsNullOrEmpty(userObj.branchZip) ? string.Empty : userObj.branchZip,
                    BranchPhone = string.IsNullOrEmpty(userObj.branchPhone) ? string.Empty : userObj.branchPhone,
                    BranchPhoneExt = string.IsNullOrEmpty(userObj.branchPhoneExt)? string.Empty :userObj.branchPhoneExt,
                    CreatedDate = userObj.createdDate.GetValueOrDefault(),
                  //  BranchFax = string.IsNullOrEmpty(userObj.branchFax.ToString()) ? 0 : Convert.ToDecimal(userObj.branchFax),
                    BranchFax = string.IsNullOrEmpty(userObj.branchFax) ? string.Empty : userObj.branchFax,
                    BranchEmail = string.IsNullOrEmpty(userObj.branchEmail) ? string.Empty : userObj.branchEmail,
                    Comment = string.IsNullOrEmpty(userObj.comment) ? string.Empty : userObj.comment,
                    ModifiedBy = string.IsNullOrEmpty(userObj.modifiedBy) ? string.Empty : userObj.modifiedBy,
                    StateOther = string.IsNullOrEmpty(userObj.stateOther) ? string.Empty : userObj.stateOther,
                    RoutingEmail = string.IsNullOrEmpty(userObj.routingEmail) ? string.Empty : userObj.routingEmail,
                    StateName = string.IsNullOrEmpty(userObj.stateName) ? string.Empty : userObj.stateName
                }).ToList();
                return listUserBranch;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public List<Branch> getUserBranchDetails(string loginId, string roleId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);

                List<CRM_GetUserBranch_Result> listUserBranchResult = obj.CRM_GetUserBranch(loginId, roleId).ToList();                
                List<Branch> listBranch = listUserBranchResult.Select(userObj => new Branch
                {
                    BranchId = Convert.ToInt32(userObj.branchId),
                    BranchName = string.IsNullOrEmpty(userObj.branchName) ? string.Empty : userObj.branchName

                }).ToList();
                return listBranch;
                

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
