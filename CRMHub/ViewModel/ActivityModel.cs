using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class ActivityModel
    {

        public ActivityModel()
        {
            activity = new Activity();
            listactivity = new List<Activity>();
            TypeList = new List<activity_types>();
            StatusList = new List<Status>();
            ListPriority = new List<Priority>();
            ListModuletype = new List<ModuleType>();
        }
        public Activity activity { get; set; }
        public List<Activity> listactivity { get; set; }
        public List<activity_types> TypeList { get; set; }
        public List<Status> StatusList { get; set; }
        public List<Priority> ListPriority { get; set; }
        public List<ModuleType> ListModuletype { get; set; }
        
    }
}
