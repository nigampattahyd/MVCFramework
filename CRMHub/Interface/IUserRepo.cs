using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
    public interface IUserRepo
    {
        bool UpdateUserPassword(int userId, string Password, string Connectionstring, string dbName);
        user GetUserDetailsbyloginID(int userId, string Connectionstring, string dbName);
        bool UpdateUserDetailsbyloginID(user userobj, string Connectionstring, string dbName, string operation);
        user CheckpasswordbyUserID(int userId, string Password, string Connectionstring, string dbName);
        List<user> GetUserDetails(string Keyword, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);
        bool CreateNewUser(user objuser, string Connectionstring, string dbName);
        bool Deleteuser(int userId, string clientid, bool status, string Connectionstring, string dbName);
        user GetUserDetailsbyUserID(int UserID, string Connectionstring, string dbName);
        bool UpdateUserDetailsbyUserId(user userobj, string Connectionstring, string dbName);
        bool UpdateUserDetailsbyStatus(bool status, long userId, string Connectionstring, string dbName);
    }
}
