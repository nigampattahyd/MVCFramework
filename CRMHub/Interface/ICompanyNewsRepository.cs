using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;

namespace CRMHub.Interface
{
   public interface ICompanyNewsRepository
    {
       List<Branch> GetUserBranchById(string loginid, string roleid, string Connectionstring, string dbName);

       //List<CompanyNew> GetCompanyNewsById(long office, int jtStartIndex, int jtPageSize, string jtSortingout, out int RecordCount, string Connectionstring, string dbName);
       //int CreateCompanyNews(CompanyNew compnews, string Connectionstring, string dbName);

       //CompanyNew EditCompanyNews(long NewsId, string Connectionstring, string dbName);

       //int UpdateCompanyNews(CompanyNew compnew, string Connectionstring, string dbName);

       //int DeleteCompanyNewsById(long NewsIds, long CreatedBy, string Connectionstring, string dbName);
    }
}
