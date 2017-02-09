using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    public class ExportCompany
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanySite { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string DateCreated { get; set; }
        public string Type { get; set; }
    }
}
