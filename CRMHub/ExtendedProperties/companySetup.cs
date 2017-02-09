using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CRMHub.EntityFramework
{

    [MetadataType(typeof(companySetupMetaData))]
  public partial  class companySetup
    {
        public string From { get; set; }
    }

    public partial class companySetupMetaData
    {
        public long CompanyId { get; set; }
        public string AdminEmail { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyName { get; set; }
        public string ModifiedBy { get; set; }
        public string SiteURL { get; set; }
        public string TwitterId { get; set; }
        public string TwitterPassword { get; set; }
        public string FacebookId { get; set; }
        public string FacebookPassword { get; set; }
        public string LinkedInId { get; set; }
        public string LinkedInPassword { get; set; }
        public string Countrycode { get; set; }
        public string CompanyURL { get; set; }
        public string OutlookUrl { get; set; }
        public string AccountingLink { get; set; }
        public string JoinNowLogo { get; set; }
        public Nullable<int> AssociateBranch { get; set; }
        public byte[] JoinNowImage { get; set; }
        public string Position { get; set; }
        public string HomeContent { get; set; }
        public string EmailExtension { get; set; }
        public string Companytype { get; set; }
        public string TimeZone { get; set; }
        public Nullable<int> DefaultBranch { get; set; }
        public Nullable<bool> EnableBranchSelection { get; set; }
        public string PTOCalculation { get; set; }
        public string LogoImage { get; set; }
    }

}
