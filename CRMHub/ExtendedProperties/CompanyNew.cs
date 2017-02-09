using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(CompanyNewMetadata))]
  public partial class CompanyNew
    {
        public long Office { get; set; }
        public List<Branch> BranchesList { get; set; }
    }
    public class CompanyNewMetadata
    {
        public long NewsId { get; set; }
        public string NewsHeadLine { get; set; }
        public string NewsDescription { get; set; }
        public long ShareToOffice { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
     
    }
}
