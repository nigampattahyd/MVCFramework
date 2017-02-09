using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(activity_typesMetaData))]
    public partial class activity_types
    {
        public string NameValue { get; set; }
    }

    public partial class activity_typesMetaData
    {
        public long Aid { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

    }
}
