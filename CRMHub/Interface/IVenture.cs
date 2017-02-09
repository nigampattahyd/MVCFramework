using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
    public interface IVenture
    {
        Int64 InserNewVenture(Venture venture, string Connectionstring, string dbName);
        Venture GetVentureDetailsByID(Int64 id, string Connectionstring, string dbName);
        int updateVentureDetails(Venture objVenture, string Connectionstring, string dbName,string operation);
        List<Venture> GetVentureDetails(int ventureid, string Keyword, string alphanumericsort, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);
        List<VMSStatu> GetAllVMSStatus(string Connectionstring, string dbName);
        bool IsVentureExists(string VentureName, string Connectionstring, string dbName);
        bool DeleteVentureByVentureId(Int64 VentureId, string Connectionstring, string dbName);
        VMSStatu GetVmsStatusNameByID(long id, string Connectionstring, string dbName);
        Int64 InsertVentureDetails(Venture venture, VentureDetail venturedetails, string Connectionstring, string dbName);
        Venture GetPendingVenturedetailsbyId(long id, string Connectionstring, string dbName);
        int updatePendingVentureDetails(Venture objVenturedetail, string Connectionstring, string dbName);
    }
}
