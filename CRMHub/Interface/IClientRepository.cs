using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
    public interface IClientRepository
    {
        Client ValidateClient(string ClientId, string site_type);

        string ValidateClientExpiry(string ClientId, string site_type);

        Client GetSiteTypeByClientID(String ClientID);
    }
}
