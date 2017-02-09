using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;

namespace CRMHub.ViewModel
{
   public class ImportantDocumentsModel
    {
       public ImportantDocumentsModel()
       {
           branchList = new List<Branch>();
           rolesList = new List<role>();
       }
       public ImportantDocument impDocsEntity { get; set; }
       public List<ImportantDocument> impDocsList { get; set; }
       public role Role { get; set; }
       public List<role> rolesList { get; set; }
       public Branch branch { get; set; }
       public List<Branch> branchList { get; set; }     
    }
}
