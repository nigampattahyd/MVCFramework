using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class BranchModel
    {
        public List<Branch> listBranch { get; set; }
        public Branch branch { get; set; }
        public List<usState> getListOfStates { get; set; }
    }
}
