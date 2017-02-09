using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ScheduledEvents
{
    [MetadataType(typeof(EventMetaData))]
    public partial class Event
    {
        
    }

    public partial class EventMetaData
    {
        public long? EventId { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> EventDate { get; set; }
        public string EventTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Mentors { get; set; }
        public Nullable<long> VentureId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> MeetingId { get; set; }
    }
}
