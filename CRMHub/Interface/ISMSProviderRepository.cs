using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Repository;
using CRMHub.EntityFramework;

namespace CRMHub.Interface
{
    public interface ISMSProviderRepository
    {
        List<SMSProviderList> GetSMSProviders();
        int CreateSMSProvider(SMSProviderList smsProviderObj, string Connectionstring, string dbName);
        List<SMSProviderList> LoadSMSProviders(string logInId, string roleId, int? startIndex, int? pagesize, string sorting, out int TotalRecordsCount, string Connectionstring, string dbName);
        SMSProviderList GetSelectedSMSProvider(int ProviderId, string Connectionstring, string dbName);
        int UpdateSMSProvider(SMSProviderList smsProviderObj, string Connectionstring, string dbName);
        int DeleteSelectedSMSProvider(string DelProviders, string Connectionstring, string dbName);
    }
}
