using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRMHub.EntityFramework
{
     [MetadataType(typeof(DashBoard_ModulesMetaData))]
  public partial  class DashBoard_Modules
    {

  }


      public partial class  DashBoard_ModulesMetaData
      {
          public long Id { get; set; }
          public int ModuleId { get; set; }
          public string ModuleName { get; set; }
          public string ModuleNameToShow { get; set; }
          public string ModuleHeading { get; set; }
          public string ModuleHover { get; set; }
          public bool IsDefault { get; set; }
          public bool IsShow { get; set; }
          public bool IsActive { get; set; }
          public string ShowToUserRole { get; set; }
          public System.DateTime CreatedDate { get; set; }
          public System.DateTime ModifiedDate { get; set; }
      }
    }

