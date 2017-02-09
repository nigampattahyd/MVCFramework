using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;

namespace CRMHub.Interface
{
     public interface ICreateClientsRepository
   
    {
         Boolean testDatabaseExists(string server, string database, string conx);
         string AddClientsdata(Client clientsdts, string dataSource, string strCon, string tokenkey, string conx);
       //  bool addclient(string dbname, string conx);
        // bool DeleteClientDetailsByClientid(string clientid, string Connectionstring, string dbName);

         Client GetClientDetailsByClientid(string clientid, string Connectionstring, string dbName);

         int UpdateClientDetailsByClientId(Client objitems, string Connectionstring, string dbName);

         bool DeleteClientDetailsByClientid(string clientid, string Connectionstring, string dbName);
         bool IsClientIdExists(Client ClientObj, string Connectionstring, string dbName);
    }
}
