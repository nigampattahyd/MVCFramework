using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
   public  class ActivitySummary
    {
       public int TotalOpen { set; get; }
       public int TotalCompleted { set; get; }
       public string Type { set; get; }
       public string ActivityType { set; get; }
    }
}
