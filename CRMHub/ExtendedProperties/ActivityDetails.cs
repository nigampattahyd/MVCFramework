using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRMHub.EntityFramework
{
    public class ActivityDetails
    {
        public int ActivityId { set; get; }
        public string Name { set; get; }
        public string Type { set; get; }
        public bool Status { set; get; }
        public string nameValue { set; get; }
        public int ActivityCount { set; get; }
      
    }
}
