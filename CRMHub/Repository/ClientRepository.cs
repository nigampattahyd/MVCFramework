using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Repository
{
    public class ClientRepository:IClientRepository
    {
        public Client ValidateClient(string ClientId, string site_type)
        {
            try
            {
                var dbMasterContext = new CRMMasterClientsEntities();
                List<VALIDATE_CLIENT_BY_TYPE_Result> lstClientResult = dbMasterContext.VALIDATE_CLIENT_BY_TYPE(ClientId, site_type).ToList();
                return lstClientResult.Select(objLstResult => new Client { ClientID = objLstResult.CLIENTID, Result = objLstResult.Result, active = objLstResult.active }).SingleOrDefault();
            }
            catch (Exception) 
            {                
                throw;
            }
        }

        public string ValidateClientExpiry(string ClientId, string site_type)
        {
            try
            {
                var dbMasterContext = new CRMMasterClientsEntities();
                var result = dbMasterContext.USP_VALIDATE_CLIENT_EXPIRY_BY_TYPE(ClientId, site_type).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Client GetSiteTypeByClientID(String ClientID)
        {
            try
            {
                var obj = new CRMMasterClientsEntities();
                var sitetype = (from m in obj.Clients
                                where m.ClientID == ClientID
                                select m);
                return sitetype.SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
