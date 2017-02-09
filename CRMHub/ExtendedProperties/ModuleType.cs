using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ExtendedProperties
{
       [MetadataType(typeof(ModuleTypeMetaData))]
    public partial class ModuleType
    {
    }

       public partial class ModuleTypeMetaData
    {
        public int ID { get; set; }
        public string AccountTypeName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
