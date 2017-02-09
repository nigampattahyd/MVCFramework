using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
    public interface ISchedulerRepository
    {
        List<CalendarEvent> GetActivityList(string Connectionstring, string dbName);
        List<Mentor> GetAllMentorsList(Int64 ventureid, string Connectionstring, string dbName);
        Int64 InsertSchedulerAppointment(CalendarEventRequest calevent, string Connectionstring, string dbName);
        Int64 InsertSchedulerMeetingDetails(EventDetail evtdetails, string Connectionstring, string dbName);
        List<CalendarEventRequest> GetRecentAppointments(string Keyword, Int64 id, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName, int Ismentor, int isdashboard, string roleid);
        CalendarEventRequest GetAppointmentDetailsByID(Int64 id, Int64 mentorid, string Connectionstring, string dbName);
        int updateEventDetails(EventDetail objevedetails, string Connectionstring, string dbName);
        List<MentorAcceptedDateTime> GetVentureAppointmentDetailsByID(string Keyword, Int64 id, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName);
        List<Event> GetAllEvents(int jtStartIndex, int jtPageSize, out int RecordCount, string Connectionstring, string dbName);
        List<Venture> GetAllVentureList(string Connectionstring, string dbName);
        int InsertSchedulerAcceptedDatetime(MentorAcceptedDateTime objmentoraccepteddatetime, string Connectionstring, string dbName);
        long InsertScheduledAppointment(Event objevent, string Connectionstring, string dbName);
        List<Event> GetAllEventsByMeetingId(int Meetingid, string Connectionstring, string dbName);
    }
}
