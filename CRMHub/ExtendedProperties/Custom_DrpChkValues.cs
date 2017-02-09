using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(Custom_DrpChkValuesMetadata))]
    public partial class Custom_DrpChkValues
    {
        public string defaultval { get; set; }
    }

    public partial class Custom_DrpChkValuesMetadata
    {

        public long DrpValueId { get; set; }
        public long FieldId { get; set; }
        public string DrpValue { get; set; }
        public bool IsDefault { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }

   
}
