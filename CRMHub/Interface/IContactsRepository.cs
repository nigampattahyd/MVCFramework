using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
    public interface IContactsRepository
    {
       // List<tbl_Contact> getListOfSearchContacts(string Keyword, string Office, string RoleId, string loginId, string salesRepId, string filterExpression, string funnelSuspects, string skillSearchOption, string skillId, int startIndex, int pageSize, string sorting, out int TotalCount, string Connectionstring, string dbName);

        //List<tbl_Contact> getListOfSearchContactsLists(string Keyword, string Office,string Type, string RoleId, string loginId, string salesRepId, string filterExpression, string funnelSuspects, string skillSearchOption, string skillId, int startIndex, int pageSize, string sorting, out int TotalCount, string Connectionstring, string dbName);


        List<user> getSalesRepresentative(string loginId, string roleId);

       // tbl_Contact getContactDetailsByContactId(int contactId, string Connectionstring, string dbName);

        //Custom_Value_Master getCustomDetailsByContactId(int contactId, string Connectionstring, string dbName);

       // int insertContact(tbl_Contact objContact, string Connectionstring, string dbName);

       // int UpdateContact(tbl_Contact objContact, string Connectionstring, string dbName);

       // string NewUserContactLogin(tbl_Contact objcontact, string Connectionstring, string dbName);

        List<activity_types> getListOfActivityByType(string Type);

       // List<tbl_account> SelectCompany(string Connectionstring, string dbName);
       // List<tbl_Contact> GET_UniqueContactName(string excelcntdets,string dbname);

        int DeleteContactByContactid(int contactid, string Connectionstring, string dbName);

       // List<tbl_history> GetCrmContactActivities(int ContactId, string eventType, string Connectionstring, string dbName);

        List<activity_types> GetContactActivityType(string Connectionstring, string dbName);

        List<user> GetAdminDetail(string Connectionstring, string dbName);

        //string CreateContactPopNewActivity(tbl_history tblhistory, string Connectionstring, string dbName);

        //tbl_history EditContactPopNewActivityByHistoryId(int HistoryId, string Connectionstring, string dbName);

        //int UpdateContactPopActivityByHistoryId(tbl_history tblhistory, string Connectionstring, string dbName);

        int DeleteContActivityByHistoryId(int historyId, string Connectionstring, string dbName);

      //  int CompleteContactActivity(int historyId, string Connectionstring, string dbName);

       // List<tbl_history> GetCrmContactActivities(string ContactId, string Keyword, string Type, string Priority, string Status, int StartIndex, int PageSize, string Sorting, out int TotalCount, string Connectionstring, string dbName);
       // int  createContactNewActivity(tbl_history hist, string Connectionstring, string dbName);
        //string createContactNewActivity(tbl_history hist, string Connectionstring, string dbName);

     //   tbl_history editContactNewActivityByhistoryId(int historyid, string Connectionstring, string dbName);

      //  int updateContactActivityByContactHistoryId(tbl_history tblhistory, string Connectionstring, string dbName);

        int DeleteContactActivity(int historyid, string Connectionstring, string dbName);

        //List<OutlookMail> getContactEmails(string type, string email, string subject, string body, string loginId, string userId, int StartIndex, int PageSize, string Sorting, out int TotalRecordsCount, string Connectionstring, string dbName);

      //  tbl_Contact GetMergeContactDetails(long Contactid, string Connectionstring, string dbName);

       // string UpdateMergedContacts(tbl_Contact mergecontact, string Connectionstring, string dbName);

        List<Custom_Manage_Master> GetCustomFieldsByModule(string Module, string Connectionstring, string dbName);

        bool CheckContactExistsOrNot(string contactname, string Connectionstring, string dbName);

        //string GetAccountSitebyName(string AccountName, string Connectionstring, string dbName);
        //List<tbl_Contact> GelAllContact(string Connectionstring, string dbName);
       // List<tbl_account> GetAllCompanyListLookUp(string Keyword, string Office, string RoleId, string loginId, string salesRepId, string sorting, string Connectionstring, string dbName);
       // string GetCompanyNameById(int AccountId, string Connectionstring, string dbName);

        DataSet getListOfSearchContactsListExport(string Keyword, string Office, string Type, string RoleId, string loginId, string salesRepId, string filterExpression, string funnelSuspects, string skillSearchOption, string skillId, int startIndex, int pageSize, string sorting,string Connectionstring);

    //    List<tbl_Contact> getListOfSearchContactsLists(string Keyword, string Office, string Type, string RoleId, string loginId, string salesRepId, string filterExpression, string funnelSuspects, string skillSearchOption, string skillId, int startIndex, int pageSize, string sorting, string specifiedZip, decimal dist, out int TotalCount, string Connectionstring, string dbName);
        //List<tbl_Contact> GetAllContactsListLookUp(string Keyword, string Office, string RoleId, string loginId, string salesRepId, string sorting, string Connectionstring, string dbName);
       
        // To Get the list of all the Contact_Types
        List<tbl_ContactType> GetAllContactTypesList(string ContactType, string Status, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);

        // To Add Contacttype 
       int createContactType(tbl_ContactType objContacttype, string Connectionstring, string dbName);

        // To get the Contact Type Details by ID
        tbl_ContactType GetContactDetailsbyId(int ContactTypeId, string Connectionstring, string dbName);

        // To Update Contact Type Details by by ID
        int UpdateContactTypeDetailsById(tbl_ContactType objContacttype, string Connectionstring, string dbName);

        // To Delete the ContactType Details by ContactTypeId
        bool DeleteContactTypedetailsbyId(int ContactTypeId, string Connectionstring, string dbName);

        // To bind the Contactstype Dropdown while creating and updating the contacts of Contact Module.
     //   List<tbl_ContactType> GetAllContacttypeList(string Connectionstring, string dbName);
        bool CheckContactTypeExistsOrNot(string Contacttype, string Connectionstring, string dbName);


        /// Look Like ZOHO Code & Screen
        /// 
        //List<Account> GetAllAccountContacts(int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);
        List<Account> GetAllAccountContacts(int CreatedBy,string Contactkeyword, int CompanyId,int UserId, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);
        int InserNewContact(Account objaccount, string Connectionstring, string dbName);
        Account GetContactDetailsByID(int id, string Connectionstring, string dbName);
        int updateContactDetailsByContactId(Account objAccount, string Connectionstring, string dbName);
        List<ImportHistory> GET_ImportHistory(string Connectionstring, string Client);


        /// <summary>
        ///  To get the Contacts Based on Company
        /// </summary>
        /// <param name="CompanyId">CompanyId</param>
        /// <param name="jtStartIndex"></param>
        /// <param name="jtPageSize"></param>
        /// <param name="jtSorting"></param>
        /// <param name="RecordCount"></param>
        /// <param name="Connectionstring"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        List<Account> GetAllCompanyContactsbyCompID(int CompanyId, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);
    }
}
