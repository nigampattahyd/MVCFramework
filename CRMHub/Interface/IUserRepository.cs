using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;

namespace CRMHub.Interface
{
    public interface IUserRepository
    {
        user getLoggedUser(string userName, string password, string Connectionstring, string dbName);

        List<user> getListOfUsers(string name, string email, string city, string phone, string keyword, string state, string zip, string level, string branch, string loginId, long roleId, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);

        List<Branch> getUserbranchlist(string loginid, string roleid, string Connectionstring, string dbName);

        int CreateNewUser(user objuser, string Connectionstring, string dbName);

        string NewUserName(user objuser, string Connectionstring, string dbName);

        user EditUserdetailsbyuserId(int userId, string Connectionstring, string dbName);

        int UpdateUserdetailsbyuserId(user User, string Connectionstring, string dbName);

        int DeleteUserDetailsByuserid(int userId, string Connectionstring, string dbName);

        Branch GetBranchDetailsByBranchName(int branchid, string Connectionstring, string dbName);

        bool CheckEmailExistsareNot(string email, string Connectionstring, string dbName);

        List<user> getAdminList(string Connectionstring, string dbName);

        List<user> getAdminListByBranchId(int branchId, string Connectionstring, string dbName);

        bool CheckLoginExistsareNot(user userObj, string Connectionstring, string dbName);
        bool CheckEmailExistsareNot(user userObj, string Connectionstring, string dbName);


        List<user> getSalesRepList(string Connectionstring, string dbName);

        user GetPassWord(string email, string Connectionstring, string dbName);
    }
}
