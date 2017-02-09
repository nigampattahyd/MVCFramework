using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Repository;
using CRMHub.EntityFramework;

namespace CRMHub.Interface
{
    public interface IOpportunitiesRepository
    {
        List<Branch> GetBranches(string Connectionstring, string dbName);
        List<tbl_crm_industry> GetIndustries(string Connectionstring, string dbName);
        List<usState> GetStates(string Connectionstring, string dbName);
        List<user> GetUsers(string Connectionstring, string dbName);
       // List<tbl_account> SelectCompany(string Connectionstring, string dbName);
       // List<tbl_Lead> GetOpportunities();
        //int InsertOpportunity(tbl_Lead LeadObj, string Connectionstring, string dbName);
       // tbl_Lead GetSelectedOpportunity(int LeadId, string Connectionstring, string dbName);
       // int UpdateOpportunity(tbl_Lead LeadObj, string Connectionstring, string dbName);
      //  int ConvertToSales(int ConvOpp, string Connectionstring, string dbName);
      //  int DeleteOpportunity(int DelOpp, string Connectionstring, string dbName);
       // List<tbl_Lead> SearchforOpportunity(string filterExpression, string keyword, string Email, string BusinessType, string OpportunityStatus, string OpportunityStage, string Opportunityindustries, string Owner, string Branch, string logInId, string roleId, int? startIndex, int? pagesize, string sorting, out int totalcount, string Connectionstring, string dbName);
       // bool IsTitleExists(tbl_Lead LeadObj, string Connectionstring, string dbName);
        List<activity_types> GetOppActivityType(string Connectionstring, string dbName);
       // List<tbl_Leadhistory> GetOpportunityActivities(string LeadId, string Keyword, string historyType, string Priority, string Status, int StartIndex, int PageSize, string Sorting, out int TotalCount, string Connectionstring, string dbName);
       // string CreateOpportunityActivity(tbl_Leadhistory leadhistoryObj, string Connectionstring, string dbName);
       // tbl_Leadhistory GetOpportunityActivityById(int LeadhistoryId, string Connectionstring, string dbName);
       // string UpdateOpportunityActivity(tbl_Leadhistory leadhistoryObj, string Connectionstring, string dbName);
       // int DeleteSelectedOppActivity(int DelOppAct, string Connectionstring, string dbName);
       // List<tbl_Lead> SelectOpportunity(string Connectionstring, string dbName);
       // List<tbl_Lead> GetOpportunityLookUpList(string keyword, string leadOwner, string office, string logInId, string roleId, string orderByClause, string Connectionstring, string dbName);
        //bool CompleteOpprtunityActivity(int leadhistoryid, string Connectionstring, string dbName);
      //  int CreateOpportunityActivity_Insert(tbl_Leadhistory leadhistoryObj, string Connectionstring, string dbName);

        /// Zoho Model Code
       // List<Opportunity> GetAllopportunities(int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);

       // bool CreateOpprtunity(Opportunity objOpp, string Connectionstring, string dbName);
        //Opportunity GetAOpportunityDetailsByID(int id, string Connectionstring, string dbName);
      //  bool DeleteOpportunityDetails(int Opporid, string Connectionstring, string dbName);
        //int updateopportunityDetailsByID(Opportunity objoppormodel, string Connectionstring, string dbName);

        List<Account> GetAllopportunities(string Keyword, int Companyid, int ownerid, int stageId, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);
        Account GetAOpportunityDetailsByID(int id, string Connectionstring, string dbName);
        int updateopportunityDetailsByID(Account objoppormodel, string Connectionstring, string dbName);
    }
}
