using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(EventDetailMetaData))]
  public  partial  class EventDetail
    {
        public string strisaccept { get; set; }
        public string strisdenied { get; set; }
       
    }
  public partial class EventDetailMetaData
  {
      public int Id { get; set; }
      public Nullable<long> MeetingId { get; set; }
      public Nullable<long> MentorId { get; set; }
      public Nullable<bool> IsAccepted { get; set; }
      public Nullable<bool> IsDenied { get; set; }
      public Nullable<System.DateTime> ModifiedDate { get; set; }
     
  }
}
