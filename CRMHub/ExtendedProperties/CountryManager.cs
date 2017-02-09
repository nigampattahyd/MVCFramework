using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRMHub.EntityFramework
{

    [MetadataType(typeof(CountryManagerMetaData))]
  public partial  class CountryManager
    {
    }

    public partial class CountryManagerMetaData
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string SocialNumber { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string ScriptPath { get; set; }
    }
}
