using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using System.Data;

namespace CRMHub.Interface
{
    public interface IExportRepository
    {
        //List<ExportCompanyData> GetExportDocuments(string Account_office, string Connectionstring, string dbName);
        DataSet GetExportDocuments(string office, string roleId, string logInId, string Connectionstring);
    }
}
