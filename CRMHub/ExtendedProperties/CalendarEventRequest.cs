using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(CalendarEventRequestMetaData))]
    public partial class CalendarEventRequest
    {

        public string Hostedby { get; set; }
        [Required]
        public string DT1Time1 { get; set; }
        [Required]
        public string DT1Time2 { get; set; }
        [Required]
        public string DT1Time3 { get; set; }
        [Required]
        public string DT2Time1 { get; set; }
        [Required]
        public string DT2Time2 { get; set; }
        [Required]
        public string DT2Time3 { get; set; }
        [Required]
        public string DT3Time1 { get; set; }
        [Required]
        public string DT3Time2 { get; set; }
        [Required]
        public string DT3Time3 { get; set; }
        public bool RdAcceptedDate1 { get; set; }
        public string RdAcceptedTime { get; set; }
        public bool RdAcceptedDate2 { get; set; }
        [Required]
        public string MentorsList { get; set; }
        public bool RdAcceptedDate3 { get; set; }
       
        public bool IsIamAvailable { get; set; }
        public bool IsIamNotAvailable { get; set; }
        public string Keyword { get; set; }
        public string strDate1 { get; set; }
        public string strDate2 { get; set; }
        public string strDate3 { get; set; }
        public string strCreatedDate { get; set; }
        public string strIsaccepted { get; set; }
        public string strisdenied { get; set; }
        public string strAccepteddatetime { get; set; }

        public string venturename { get; set; }
        
        public bool chkDT1Time1 { get; set; }

        public bool chkDT1Time2 { get; set; }

        public bool chkDT1Time3 { get; set; }

        public bool chkDT2Time1 { get; set; }

        public bool chkDT2Time2 { get; set; }

        public bool chkDT2Time3 { get; set; }

        public bool chkDT3Time1 { get; set; }

        public bool chkDT3Time2 { get; set; }

        public bool chkDT3Time3 { get; set; }
        public string ScheduledEventDatetime { get; set; }
        public string ScheduledMentors { get; set; }
        public string Scheduledeventdetails { get; set; }
    }
    public partial class CalendarEventRequestMetaData
    {
        public long ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Nullable<System.DateTime> Date1 { get; set; }
        [Required]
        public string Time1 { get; set; }
        [Required]
        public Nullable<System.DateTime> Date2 { get; set; }
        [Required]
        public string Time2 { get; set; }
        [Required]
        public Nullable<System.DateTime> Date3 { get; set; }
        [Required]
        public string Time3 { get; set; }
        [Required]
        public string Location { get; set; }
        public string Description { get; set; }
       
        public Nullable<int> Venture { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
