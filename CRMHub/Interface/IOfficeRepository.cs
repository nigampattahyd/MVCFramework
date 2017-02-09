using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
    public interface IOfficeRepository
    {
        List<Branch> getListOfOffice(string name, string email, string city, string phone, string keyword, string state, string zip, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);

        int createBranch(Branch objBranch, string Connectionstring, string dbName);

        Branch editBranchByBranchID(int branchId, string Connectionstring, string dbName);

        int updateBranchByBranchId(Branch objBranch, string Connectionstring, string dbName);

        int DeleteBranchByBranchID(int branchId, string Connectionstring, string dbName);

        List<Branch> getUserBranch(string loginId, string roleId, string Connectionstring, string dbName);
        List<Branch> getUserBranchDetails(string loginId, string roleId, string Connectionstring, string dbName);
    }
}
