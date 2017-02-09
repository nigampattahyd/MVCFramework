using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(DashBoard_UserPreferencesOrderMetaData))]
    public partial class DashBoard_UserPreferencesOrder
    {

    }


   public partial class DashBoard_UserPreferencesOrderMetaData
    {
        public long PreferenceId { get; set; }
        public decimal UserId { get; set; }
        public string PreferencesOrder { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
