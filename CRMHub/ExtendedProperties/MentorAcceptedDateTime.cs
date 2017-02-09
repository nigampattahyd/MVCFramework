using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(MentorAcceptedDateTimeMetaData))]
  public partial  class MentorAcceptedDateTime
    {
        public string stracceptdatetime { get; set; }
        public string MentorName { get; set; }
        public string accepttime { get; set; }
    }

  public partial class MentorAcceptedDateTimeMetaData
  {
      public int Id { get; set; }
      public Nullable<System.DateTime> AcceptedDateTime { get; set; }
      public string MentorId { get; set; }
      public Nullable<int> MeetingId { get; set; }
  }
}
