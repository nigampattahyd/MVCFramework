using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
  public  interface INoticationRepository
    {
      List<Activity> GetAllNotificationslist(string Keyword,int id,int roleid, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);

    }
}
