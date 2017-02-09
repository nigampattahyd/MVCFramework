using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
namespace CRMHub.ViewModel
{
   public class DashBoardSetUpModel
    {
     public  List<DashBoard_Modules> GetDashboardsetup { get; set; }
     public  List<DashBoard_Modules> ApplicantDashboardsetup { get; set; }
    }
}
