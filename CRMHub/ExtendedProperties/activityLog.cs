using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(activityLogMetaData))]
    public partial class activityLog
    {
        public int? ActiveCount { get; set; }
        public int? TotalOpen { get; set; }
        public int? TotalCompleted { get; set; }

        public long? HistoryId { get; set; }
        public string HistoryType { get; set; }
      

        //public long Aid { get; set; }
        //public string Name { get; set; }
        //public string Type { get; set; }
        //public string Status { get; set; }
    }

    public partial class activityLogMetaData
    {
        public long LogId { get; set; }
        public Nullable<long> ActivityId { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Type { get; set; }
        public Nullable<decimal> AssignedTo { get; set; }
        public string ExportTest { get; set; }
    }
}
