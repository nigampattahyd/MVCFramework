using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;

namespace CRMHub.Interface
{
    public interface IDBoard
    {
        List<Event> GetEvents(string Role, long Id, string keyword, string Connectionstring, string dbName);
    }
}
