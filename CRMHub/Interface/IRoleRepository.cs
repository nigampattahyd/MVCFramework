using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
namespace CRMHub.Interface
{
  public  interface IRoleRepository
    {
      List<role> GetLevels(string Connectionstring, string dbName);

      List<role> GetAllRoles(string ContactType, string Status, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);
      bool DeleteRole(int roleId,bool status, string Connectionstring, string dbName);
      List<role> GetRolesDetails(string Keyword, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);
      int createRole(role objrole, string Connectionstring, string dbName);
      bool DeleteRoledetailsbyId(int RoleId, string Connectionstring, string dbName);
      role EditRoledetailsbyroleId(int roleId, string Connectionstring, string dbName);
      int UpdateRoledetailsbyroleId(role Role, string Connectionstring, string dbName);
      bool IsRoleExists(role roleObj, string Connectionstring, string dbName);
      int GetUserrolecountfordelete(int RoleId, string Connectionstring, string dbName);
      bool UpdateRoledetails(role Role, string Connectionstring, string dbName);
      bool CreateRoleName(role objrole, string Connectionstring, string dbName);
      bool CheckRoleName(string RoleName, string Connectionstring, string dbName);
      List<role> GetAllMentoringRoles(string Connectionstring, string dbName);
    }
}
