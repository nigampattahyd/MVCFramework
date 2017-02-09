using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;
using CRMHub.Repository;
using CRMHub.EntityFramework;

namespace CRMHub.ViewModel
{
   public class UsersModel
    {
       public List<role> lstRoles { get; set; }
       public List<usState> getListOfStates { get; set; }
       public user users { get; set; }
       public List<user> lstusers { get; set; }
       public List<Branch> listBranch { get; set; }
    }
}
