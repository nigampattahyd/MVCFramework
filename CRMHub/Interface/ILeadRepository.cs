using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
    public interface ILeadRepository
    {
       // List<tbl_Lead> getAllLeads(string filterExpression, string Keyword,string LeadStatus, string email,string LeadIndustry, string leadOwner, string office, int loginId, int roleId, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);

        //List<OutlookMail> getLeadEmails(string type, string email, string subject, string body, string loginId, string userId, int StartIndex, int PageSize, string Sorting, out int TotalRecordsCount, string Connectionstring, string dbName);

       // tbl_Lead getLeadByLeadId(decimal leadId, string Connectionstring, string dbName);

      //  int insertLead(tbl_Lead objLead, string Connectionstring, string dbName);

       // int updateLeadByLeadId(tbl_Lead objLead, string Connectionstring, string dbName);

      //  Int64 DeleteLeadsDetailsByLeadId(Int64 Leadid, string Connectionstring, string dbName);

        List<tbl_crm_industry> GetIndustries(string Connectionstring, string dbName);

      //  Int64 ConvertToOpportunity(Int64 ConvLeadid, string Connectionstring, string dbName);

        List<activity_types> GetLeadActivityType(string Connectionstring, string dbName);

       // string createLeadNewActivity(tbl_Leadhistory leadhist, string Connectionstring, string dbName);

        //List<tbl_Leadhistory> GetCrmLeadActivities(string LeadId, string Keyword, string Type, string Priority, string Status, int StartIndex, int PageSize, string Sorting, out int TotalCount, string Connectionstring, string dbName);

       // tbl_Leadhistory editLeadNewActivityByLeadhistoryId(Int64 leadhistoryid, string Connectionstring, string dbName);

       // string updateLeadActivityByLeadHistoryId(tbl_Leadhistory tblhistory, string Connectionstring, string dbName);

       // int DeleteLeadActivity(int leadhistoryid, string Connectionstring, string dbName);

        //int CompleteLeadActivity(int leadhistoryid, string Connectionstring, string dbName);
       // string GetLeadNameById(int LeadId, string Connectionstring, string dbName);
       // List<tbl_Lead> GetLeadsList(string Connectionstring, string dbName);
       // List<tbl_Lead> GetLeadsLookUpList(string keyword, string leadOwner, string office, string logInId, string roleId, string orderByClause, string Connectionstring, string dbName);
      //  int createLeadNewActivity_Insert(tbl_Leadhistory leadhist, string Connectionstring, string dbName);
    


        // New Code for lead as of ZOHO leads
       // List<Account> GetAllAccountLeads(int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);
        bool CreateLead(Account objaccount, string Connectionstring, string dbName);
        Account GetAccountDetailsByID(int id, string Connectionstring, string dbName);
        int updateLeadDetailsByLeadId(Account objAccount, string Connectionstring, string dbName);
        bool DeleteLeadsDetails(int Leadid, string Connectionstring, string dbName);
        List<Company> GetAllCompaniesList(string Connectionstring, string dbName);


        List<Account> GetAllAccountLeads(string Keyword, string companyname, int ownerid, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);
    }
}
