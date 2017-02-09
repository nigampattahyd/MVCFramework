using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class CalenderEventsViewModel
    {
        public CalenderEventsViewModel()
        {
            listMentors = new List<Mentor>();
            objcalenderevent = new CalendarEventRequest();
            objEventdetails = new EventDetail();
            objlstevedetails = new List<EventDetail>();
            objlstventure = new List<Venture>();
        }
        public List<Mentor> listMentors { get; set; }
        public CalendarEventRequest objcalenderevent { get; set; }
        public EventDetail objEventdetails { get; set; }
        public List<EventDetail> objlstevedetails { get; set; }
        public List<Venture> objlstventure { get; set; }
    }
}
