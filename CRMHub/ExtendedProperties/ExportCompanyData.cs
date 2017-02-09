using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    public class ExportCompanyData
    {
        public int AccountId { get; set; }
        public string Account_Name { get; set; }
        public string AccountSite { get; set; }
        public string Phone { get; set; }
        public string Shippingcity { get; set; }
        public string Shippingzip { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Account_type { get; set; }
        public string CustomLabel { get; set; }
        public string Customvalue { get; set; }
    }
}
