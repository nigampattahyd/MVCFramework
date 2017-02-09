using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Aspects;
using System.Data;


namespace CRMHub.Interface
{
    public interface ICompanyRepository
    {
     
      
        List<tbl_crm_industry> GetIndustry(string Connectionstring, string dbName);

        List<usState> GetState(string Connectionstring, string dbName);

        List<user> GetAdmin(string Connectionstring, string dbName);

        List<Branch> GetBranch(string Connectionstring, string dbName);

        bool CheckCompanyExistsOrNot(string companyname, string Connectionstring, string dbName);

     

        int DeleteCompanyByAccountId(int AccountId, string Connectionstring, string dbName);

      

        int DeleteCompNewActivityByHistoryId(int historyId, string Connectionstring, string dbName);

       

        string GetContactIdFrmAcntId(int AccountId);

        
        bool CheckifStandardColumnExists(string mastercolumnlabel, string Connectionstring, string dbName);
        
       DataSet GetAllCompaniesListToExport(string keyword, string office, string Status, string roleId, string logInId, string salesRep, string filterExpression, int startIndex, int pageSize, string sorting, string Connectionstring);
     
       List<CalendarEvent> GetActivityList(string Connectionstring, string dbName);
    
     
       int InserNewCompany(Company objcompany, string Connectionstring, string dbName);
       Company GetCompnayDetailsByID(int id, string Connectionstring, string dbName);
       int updateCompanyDetailsByCompanyId(Company objCompany, string Connectionstring, string dbName);
       bool IsCompanyExists(Company CompanyObj, string Connectionstring, string dbName);


       bool InserCompanyActivity(Activity objActivity, string Connectionstring, string dbName);
       List<Activity> GetListOfallActivities(string keyword,int userid,int roleid,int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);
       int CompleteActivity(int ActivityId, string Connectionstring, string dbName);
       int DeleteActivityByActivityId(int ActivityId, string Connectionstring, string dbName);
       List<Account> GetAllContactsbyCompanyID(int CompanyID, string Connectionstring, string dbName);


       List<Company> GetCompaniesIndex(string Keyword, int OwnerID, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);

        /// <summary>
        /// To Get all Company Activities Based on the Company ID
        /// </summary>
       /// <param name="CompanyID">CompanyID</param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByClause"></param>
        /// <param name="TotalCount"></param>
        /// <param name="Connectionstring"></param>
        /// <param name="dbName"></param>
        /// <returns>List of all Activities of certain company</returns>
       List<Activity> GetListOfallCompanyActivitiesbyID(int CompanyID, int Accounttype, string Module, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);
       List<user> getVenturePrefix(Int64 roleid,string Prefix, string Connectionstring, string dbName);
    }
}
