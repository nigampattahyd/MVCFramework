using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
  public  class OfficeLevelAccessModel
    {
      public List<user> lstusers { get; set; }
      public List<role> lstRoles { get; set; }
      public user users{ get; set; }
      public List<usState> getListOfStates { get; set; }
      public List<Branch> listBranch { get; set; }      
    }
}
