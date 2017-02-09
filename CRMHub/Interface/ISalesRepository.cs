using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Repository;
using CRMHub.EntityFramework;

namespace CRMHub.Interface
{
    public interface ISalesRepository
    {
        List<Branch> GetBranches(string Connectionstring, string dbName);
        List<user> GetUsers(string Connectionstring, string dbName);
        List<tbl_account> SelectCompany(string Connectionstring, string dbName);
        List<tbl_Sales> SearchforSale(string filterExpression, string keyword, string Email, string salesRep, string Branch, string logInId, string roleId, int? startIndex, int? pagesize, string sorting, out int TotalRecordsCount, string Connectionstring, string dbName);
        List<tbl_Sales> GetAllSales();
        int AddSales(tbl_Sales salesObj, string Connectionstring, string dbName);
        tbl_Sales GetSelectedSale(int SalesId, string Connectionstring, string dbName);
        int UpdateSales(tbl_Sales SalesObj, string Connectionstring, string dbName);
        int DeleteSale(int DelSale, string Connectionstring, string dbName);
        bool IsTitleExists(tbl_Sales SalesObj, string Connectionstring, string dbName);
        List<Custom_Manage_Master> GetCustomFieldsByModule(string Module, string Connectionstring, string dbName);
    }
}
