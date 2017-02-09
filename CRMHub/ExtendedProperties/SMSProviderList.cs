using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(SMSProviderListMetadata))]
    public partial class SMSProviderList
    {
    }
    public partial class SMSProviderListMetadata
    {   
        public int ID { get; set; }
        public string Carrier { get; set; }
        public string Email { get; set; }
        public string Emailused { get; set; }
        public DateTime DateCreated { get; set; }
    
    }
}
