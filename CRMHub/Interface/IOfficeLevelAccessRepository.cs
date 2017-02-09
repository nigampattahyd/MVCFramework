using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;

namespace CRMHub.Interface
{
  public interface IOfficeLevelAccessRepository
    {
      List<user> GetUser(string name, string email, string roleId, string loginId, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);

      user EditUserdetailsbyuserId(int userid, string Connectionstring, string dbName);

      int UpdateUserdetailsbyuserId(user user, string Connectionstring, string dbName);

      int DeleteUserDetailsByuserid(int userid, string Connectionstring, string dbName);

      Branch GetBranchDetailsByBranchName(int branchid, string Connectionstring, string dbName);

      bool CheckEmailExistsareNot(string email, string Connectionstring, string dbName);

      List<Branch> GetUserBranch(string Connectionstring, string dbName);
    }
}
