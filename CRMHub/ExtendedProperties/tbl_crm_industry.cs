using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRMHub.EntityFramework
{

[MetadataType(typeof(tbl_crm_industryMetadata))]
  public partial  class tbl_crm_industry
    {
    }



  public partial class tbl_crm_industryMetadata
  {
      public int Id { get; set; }
      public string Industry { get; set; }
  }
}
