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
    public interface IItemRepository
    {
        List<tbl_Items> GetAllItemsList(int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);
        int createitem(tbl_Items objitems, string Connectionstring, string dbName);
        tbl_Items edititemdetailsbyitemid(int ItemID, string Connectionstring, string dbName);
        int UpdateitemDetailsByitemId(tbl_Items objitems, string Connectionstring, string dbName);
        int DeleteItemByItemId(int ItemID, string Connectionstring, string dbName);
    }
}
