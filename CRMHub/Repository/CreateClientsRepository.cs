using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CRMHub.EntityFramework;
using System.Data;


namespace CRMHub.Repository
{
   public class CreateClientsRepository : ICreateClientsRepository
    {



       public Boolean testDatabaseExists(string server, string database, string conx)
       {
           
          // string conn = ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;
           SqlConnection connString = new SqlConnection(conx);
           String cmdText = ("select * from master.dbo.sysdatabases where name=\'" + (database + "\'"));
           Boolean bRet;

           System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand(cmdText, connString);

           try
           {
               connString.Open();
               System.Data.SqlClient.SqlDataReader reader = sqlCmd.ExecuteReader();
               bRet = reader.HasRows;
               connString.Close();
           }
           catch (Exception e)
           {
               bRet = false;
               connString.Close();

               return false;
           }


           if (bRet == true)
           {

               return true;
           }
           else
           {

               return false;
           }


       }



       public string AddClientsdata(Client clientsdts, string dataSource, string strCon, string tokenkey, string conx)
       {
           try
           {

               SqlConnection connString = new SqlConnection(conx);
               SqlCommand com = new SqlCommand("CRM_InsertClientDetails", connString);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.AddWithValue("@ClientID", clientsdts.ClientID);
               com.Parameters.AddWithValue("@ClientName", clientsdts.CompanyName);
               com.Parameters.AddWithValue("@companytype", clientsdts.companytype);
               com.Parameters.AddWithValue("@Country", clientsdts.Country);
               com.Parameters.AddWithValue("@address1", clientsdts.address1);
               com.Parameters.AddWithValue("@address2", clientsdts.address2);
               com.Parameters.AddWithValue("@city", clientsdts.city);
               com.Parameters.AddWithValue("@companywebsite", clientsdts.companywebsite);
               com.Parameters.AddWithValue("@CompanyLogo", clientsdts.companylogo);
               SqlParameter insertUser = new SqlParameter("@ConnectionString", SqlDbType.VarBinary, 500);
               insertUser.Value = System.Text.Encoding.ASCII.GetBytes(strCon);
               com.Parameters.Add(insertUser);
               com.Parameters.AddWithValue("@ServerName", dataSource);
               connString.Open();
               int i = com.ExecuteNonQuery();
               connString.Close();
               if (i >= 1)
               {
                   return "New Clients Added Successfully";
               }
               else
               {
                   return "Clients Not Added";
               }
           }
           catch (Exception)
           {

               throw;
           }
       }



       public Client GetClientDetailsByClientid(string clientid, string Connectionstring, string dbName)
       {
           try
           {
               CRMMasterClientsEntities obj = new CRMMasterClientsEntities(Connectionstring);
               obj.Database.Connection.Open();
               obj.Database.Connection.ChangeDatabase(dbName);
               Client DelClientDetails = obj.Clients.Where(c => c.ClientID == clientid).SingleOrDefault();

               return DelClientDetails;
           }
           catch (Exception)
           {
               throw;
           }
       }


       public int UpdateClientDetailsByClientId( Client objitems, string Connectionstring, string dbName)
       {
           try
           {
               CRMMasterClientsEntities obj = new CRMMasterClientsEntities(Connectionstring);
               obj.Database.Connection.Open();
               obj.Database.Connection.ChangeDatabase(dbName);

               var result = obj.CRM_UpdateClientDetailsByClientId(objitems.ClientID, objitems.CompanyName, objitems.companytype, objitems.Country, objitems.address1, objitems.address2, objitems.city, objitems.companylogo,objitems.companywebsite);
              
               return result;
           }
           catch (Exception)
           {
               throw;
           }
           
       }



       public bool DeleteClientDetailsByClientid(string clientid, string Connectionstring, string dbName)
       {
           try
           {
               CRMMasterClientsEntities obj = new CRMMasterClientsEntities(Connectionstring);
               obj.Database.Connection.Open();
               obj.Database.Connection.ChangeDatabase(dbName);
               Client DelClientDetails = obj.Clients.Where(c => c.ClientID == clientid).SingleOrDefault();
               if (DelClientDetails != null)
               {
                   obj.Clients.Remove(DelClientDetails);
                   obj.SaveChanges();
               }
               return true;
           }
           catch (Exception)
           {
               throw;
           }
       }




       public bool IsClientIdExists(Client ClientObj, string Connectionstring, string dbName)
       {
           try
           {

               CRMMasterClientsEntities obj = new CRMMasterClientsEntities(Connectionstring);
               obj.Database.Connection.Open();
               obj.Database.Connection.ChangeDatabase(dbName);
               bool Result = obj.Clients.Any(c => c.ClientNumber != ClientObj.ClientNumber && c.ClientID.ToLower() == ClientObj.ClientID.ToLower());
               return !Result;               
           }
           catch (Exception)
           {
               throw;
           }
       }
    }
}
