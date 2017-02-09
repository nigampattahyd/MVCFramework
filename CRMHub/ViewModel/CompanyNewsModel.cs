using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
namespace CRMHub.ViewModel
{
 public  class CompanyNewsModel
    {
     public List<Branch> listBranch { get; set; }
     public List<CompanyNew> lstCompNews { get; set; }
     public CompanyNew Compnews { get; set; }
    }
}
