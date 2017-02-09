using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;

namespace CRMHub.Interface
{
   public interface IUserMasterRepository
    {
       //UserMaster getLoggedUsermaster(string username, string password);
       string CheckUserExists(string uname, string upassword, string Connectionstring, string dbName);
       List<Client> Getallclientmaster(string Connectionstring, string dbName);
     
    }
}
