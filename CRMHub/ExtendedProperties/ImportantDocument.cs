using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{

   [MetadataType(typeof(ImportantDocumentMetadata))]
   public partial class ImportantDocument
    {
       //public List<Branch> branchList { get; set; }
       //public List<role> RolesList { get; set; }
    }
    public partial class ImportantDocumentMetadata
    {
        public long DocId { get; set; }
        public string DocName { get; set; }
        public string DocDescription { get; set; }
        public string DocFileNameOriginal { get; set; }
        public string DocFileName { get; set; }
        public long DocShareToUser { get; set; }
        public long DocShareToOffice { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
}
