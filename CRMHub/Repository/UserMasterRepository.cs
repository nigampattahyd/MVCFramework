using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using System.Web.Mvc;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;


namespace CRMHub.Repository
{
   public class  UserMasterRepository:IUserMasterRepository
    {

       public string CheckUserExists(string username, string password, string Connectionstring, string dbName)
       {
           try
           {
               //var obj = new CRMMasterClientsEntities();
               CRMMasterClientsEntities obj = new CRMMasterClientsEntities(Connectionstring);
               obj.Database.Connection.Open();
               obj.Database.Connection.ChangeDatabase(dbName);
               string usermasterresult = obj.CRM_CheckifUserExists(username, password).ToString();
              //string conx = Session["conx"];
               //string cs = ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;


               //    UserMaster UserMasterObj = Listusermasterinfo.Select(objusermasterResult => new UserMaster
               //    {
               //        Userid = objusermasterResult.Userid,
               //        Username = objusermasterResult.Username,
               //        password = objusermasterResult.password,
               //        Name = objusermasterResult.Name,
               //        Email = objusermasterResult.Email
               //    }).SingleOrDefault();
               //    return UserMasterObj;
               //}
               return usermasterresult;
           }

           catch (Exception)
           {

               throw;
           }

       }
       //public Boolean testDatabaseExists(string server, string database)
       //{
       //    string conx = Session["conx"];
         
       //  SqlConnection connString = new SqlConnection(conx);
       //    String cmdText = ("select * from master.dbo.sysdatabases where name=\'" + (database + "\'"));
       //    Boolean bRet;

       //    System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand(cmdText, connString);

       //    try
       //    {
       //        connString.Open();
       //        System.Data.SqlClient.SqlDataReader reader = sqlCmd.ExecuteReader();
       //        bRet = reader.HasRows;
       //        connString.Close();
       //    }
       //    catch (Exception e)
       //    {
       //        bRet = false;
       //        connString.Close();

       //        return false;
       //    }


       //    if (bRet == true)
       //    {

       //        return true;
       //    }
       //    else
       //    {

       //        return false;
       //    }


       //} 



       public List<Client> Getallclientmaster(string Connectionstring, string dbName)
       {
           try
           {
               CRMMasterClientsEntities obj = new CRMMasterClientsEntities(Connectionstring);
               obj.Database.Connection.Open();
               obj.Database.Connection.ChangeDatabase(dbName);
              // var obj = new CRMMasterClientsEntities();

               List<Client> lstclientsdet = new List<Client>();
                  // lstclientsinfo = obj.Clients.ToList();
               List<CRM_Getclients_Result> lstclientsinfo = obj.CRM_Getclients().ToList();
               lstclientsdet = lstclientsinfo.Select(objLstResult1 => new Client
               {
                   CompanyName = objLstResult1.CompanyName,
                   companytype = objLstResult1.companytype,
                   ClientNumber=objLstResult1.ClientNumber,
                   ClientID = objLstResult1.ClientID,



                  site_type = objLstResult1.site_type,companyphone = objLstResult1.companyphone,expirydate=objLstResult1.expirydate}).ToList();
            
               //lstclientsdet=lstclientsinfo.Select(x=> new 
               //                     cli)
                return lstclientsdet;
             
           }

           catch (Exception)
           {

               throw;
           }

       }


    
   



    //}

   
     //public user getLoggedUser(string userName, string password, string Connectionstring, string dbName)
     //   {
     //       try
     //       {
     //           var obj = new DevEntities();
     //           List<CRM_SecureLogin_Result> userResult = obj.CRM_SecureLogin(userName, password).ToList();
     //           user UserObj = userResult.Select(objuserResult => new user
     //           {
     //               AccountId = objuserResult.AccountId,
     //               AddressLine1 = objuserResult.AddressLine1,
     //               AddressLine2 = objuserResult.AddressLine2,
     //               BranchId = objuserResult.BranchId,
     //               Cellno = objuserResult.Cellno,
     //               UserId = objuserResult.UserId,
     //               LoginId = objuserResult.LoginId,
     //               Password = objuserResult.Password,
     //               FirstName = objuserResult.FirstName,
     //               MiddleInitial = objuserResult.MiddleInitial,
     //               LastName = objuserResult.LastName,
     //               Phone = objuserResult.Phone,
     //               PhoneExt = objuserResult.phoneExt,
     //               Fax = objuserResult.Fax,
     //               Email = objuserResult.Email,
     //               City = objuserResult.City,
     //               State = objuserResult.State,
     //               PostalCode = objuserResult.PostalCode,
     //               Comment = objuserResult.Comment,
     //               CreatedDate = objuserResult.CreatedDate,
     //               LastModified = objuserResult.LastModified,
     //               StateCode = objuserResult.StateCode,
     //               RoleId = objuserResult.RoleId,
     //               Status = objuserResult.Status,
     //               ModifiedBy = objuserResult.ModifiedBy,
     //               ContactId = objuserResult.ContactId,
     //               DateOfBirth = objuserResult.DateOfBirth,
     //               IsAllowsedingSMS = objuserResult.IsAllowsedingSMS,
     //               SMTPEmail = objuserResult.SMTPEmail,
     //               SMTPPassword = objuserResult.SMTPPassword,
     //               SMTPAddress = objuserResult.SMTPAddress,
     //               SMTPPort = objuserResult.SMTPPort,
     //               OutlookEmail = objuserResult.OutlookEmail,
     //               Gender = objuserResult.Gender,
     //               roleName = objuserResult.RoleName
     //           }).SingleOrDefault();                
     //           return UserObj;
     //       }
     //       catch (Exception)
     //       {                
     //           throw;
     //       }
     }

}
