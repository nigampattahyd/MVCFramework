using CRMHub.EntityFramework;
using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
    public interface ICommonRepository
    {
        List<usState> getListOfStates(string Connectionstring, string dbName);
        List<usState> getListOfStatecode(string Connectionstring, string dbName);
        List<SMSProviderList> getSMSProvider(string Connectionstring, string dbName);

        List<tbl_Menus> getMenus(string Connectionstring, string dbName);

        int InsertMenuByRole(DataTable dtMenuRole, string Connectionstring, string dbName);

        List<RoleMenuModel> getMenuByRoleId(int RoleId, string Connectionstring, string dbName);

        List<tbl_Menus> getDynamicMenuByRoleId(int RoleId, string Connectionstring, string dbName);

        int GetAcntIdFrmCompName(string CompanyName,out int outCompId, string Connectionstring, string dbName);
       // int GetContIdFrmFName(string cntname,string cntLName, string email, string ConnectionString, string ClientName);
        List<tbl_RoleMenus> getAllMenuByRoleId(int RoleId, string Connectionstring, string dbName);

        // Delete the accessmenu's for a user Roleid
        bool DeleteAccessmenuForRold(int UserRoleId, string Connectionstring, string dbName);
        // Insert the new accessmenu's for a user Roleid
        bool insertAccessmenuForRold(List<tbl_RoleMenus> ObjRoleMenus, string Connectionstring, string dbName);


        /// New States Code For ZOHO Model
        List<State> GetAllStatesList(string Connectionstring, string dbName);
        List<Country> GetAllCountriesList(string Connectionstring, string dbName);
        int GetCountrybyStateId(int StateId, string Connectionstring, string dbName);
        
        List<Company> GetListOfAllCompanies(int startIndex, int pageSize, string sorting, out int TotalCount, string Connectionstring, string dbName);
        List<user> getUsersLookup(int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);
        List<Account> GetListOfAllCContacts(int startIndex, int pageSize, string sorting, out int TotalCount, string Connectionstring, string dbName);

        string GetusernameById(int userId, string Connectionstring, string dbName);

        List<activity_types> GetAllActivityType(string Connectionstring, string dbName);
        List<Status> GetAllStatusTypes(string Connectionstring, string dbName);
        List<Priority> GetAllPrioriTypes(string Connectionstring, string dbName);
        Activity GetActivityDetailsByID(int id, string Connectionstring, string dbName);
        int updateActivityDetailsByActivityId(Activity objActivity, string Connectionstring, string dbName);

        //List<Opportunity> GetListOfOpportunitiesList(int startIndex, int pageSize, string sorting, out int TotalCount, string Connectionstring, string dbName);
        List<ModuleType> GetAllModuleType(string Connectionstring, string dbName);

        //int GetModulesonUserIDfordelete(int itemid, string Connectionstring, string dbName);
        //int GetOpportunityIDfordelete(int OpporId, string Connectionstring, string dbName);
        //int GetActvityCounttoDelContact(int ContactId, string Connectionstring, string dbName);
        //int GetCompanyCountIDfordelete(int CompanyId, string Connectionstring, string dbName);
        int GetcompanyIdbyName(string CompanyName, string Connectionstring, string dbName);
        List<Account> GetListOfOpportunitiesList(string Keyword, int Companyid, int ownerid, int stageId, int startIndex, int pageSize, string sorting, out int TotalCount, string Connectionstring, string dbName);
        int GetUserIdFrmNamepName(string UserName, out int outUserId, string Connectionstring, string dbName);

        /// <summary>
        /// Used for Import Companies
        /// </summary>
        /// <param name="StateName"></param>
        /// <param name="outStateId"></param>
        /// <param name="Connectionstring"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        int GetStateIdFrmStateName(string StateName, out int outStateId, string Connectionstring, string dbName);
        int GetCountryIdFrmCountryName(string CountryName, out int outCountryId, string Connectionstring, string dbName);


        string GetCompanynameById(int companyId, string Connectionstring, string dbName);
        string GetOpportunitynameById(int OpportunityId, string Connectionstring, string dbName);

       // string GetContactnameById(int ContactId, string Connectionstring, string dbName);

        Account GetContactDetailsbyId(int ContactId, string Connectionstring, string dbName);
        bool IsEmailIdExists(string Emailid, string Connectionstring, string dbName);

     
    }
}
