using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Interface;
using System.Web;


namespace CRMHub.Repository
{
    public class CompanyNewsRepository : ICompanyNewsRepository
    {
        public List<Branch> GetUserBranchById(string loginid, string roleid, string Connectionstring, string dbName)
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
                    BranchId = Convert.ToInt32(a.branchId),
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

        //public List<CompanyNew> GetCompanyNewsById(long office, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
        //        List<CRM_GetCompanyNewsByOfficeId_Result> compnewslist = obj.CRM_GetCompanyNewsByOfficeId(office,jtStartIndex,jtPageSize,jtSorting,output).ToList();
        //        RecordCount = Convert.ToInt32(output.Value);
        //        List<CompanyNew> lstcompnews = new List<CompanyNew>();
        //        lstcompnews = compnewslist.Select(a => new CompanyNew
        //        {
        //            NewsId = a.NewsId,
        //            NewsHeadLine = a.NewsHeadLine,
        //            NewsDescription = a.NewsDescription,
        //            ShareToOffice = a.ShareToOffice,
        //            IsPublic = a.IsPublic,
        //            IsActive = a.IsActive,
        //            CreatedBy = a.CreatedBy,
        //            CreateDate = a.CreateDate,
        //            ModifiedBy = a.ModifiedBy,
        //            ModifiedDate = a.ModifiedDate
        //        }).ToList();
        //        return lstcompnews.ToList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int CreateCompanyNews(CompanyNew compnews, string Connectionstring, string dbName)
        //{
        //    int result = 0;
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        result = obj.CRM_AddUpdateCompanyNews(compnews.NewsId, compnews.NewsHeadLine, compnews.NewsDescription, compnews.ShareToOffice, compnews.IsActive,
        //           compnews.CreatedBy, 1).FirstOrDefault() ?? -1;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return result;
        //}

        //public CompanyNew EditCompanyNews(long NewsId, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        var companynews = obj.CRM_GetCompanyNewsByNewsId(NewsId).ToList();
        //        var newsofcompany = companynews.Select(a => new CompanyNew
        //        {
        //            NewsId = a.NewsId,
        //            NewsHeadLine = a.NewsHeadLine,
        //            NewsDescription = a.NewsDescription,
        //            ShareToOffice = a.ShareToOffice,
        //            IsPublic = a.IsPublic,
        //            IsActive = a.IsActive,
        //            CreatedBy = a.CreatedBy,
        //            CreateDate = a.CreateDate,
        //            ModifiedBy = a.ModifiedBy,
        //            ModifiedDate = a.ModifiedDate
        //        }).ToList();
        //        return newsofcompany.SingleOrDefault();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int UpdateCompanyNews(CompanyNew compnews, string Connectionstring, string dbName)
        //{
        //    int result = 0;
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        result = obj.CRM_AddUpdateCompanyNews(compnews.NewsId, compnews.NewsHeadLine, compnews.NewsDescription, compnews.ShareToOffice, compnews.IsActive,
        //           compnews.CreatedBy,2).FirstOrDefault() ?? -1;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return result;
        //}

        //public int DeleteCompanyNewsById(long NewsIds, long CreatedBy, string Connectionstring, string dbName)
        //{
        //    int result = 0;
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        result = obj.CRM_DeleteCompanyNewsById(NewsIds,CreatedBy);
        //        return result;
        //    }
        //    catch (Exception)
        //    {                
        //        throw;
        //    }
        //}
    }
}
