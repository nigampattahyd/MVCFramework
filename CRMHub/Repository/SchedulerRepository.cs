using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;
using CRMHub.EntityFramework;

using System.Data;

namespace CRMHub.Repository
{
   public class SchedulerRepository : ISchedulerRepository
    {
       public List<CalendarEvent> GetActivityList(string Connectionstring, string dbName)
       {
           try
           {
               DevEntities obj = new DevEntities(Connectionstring);
               obj.Database.Connection.Open();
               obj.Database.Connection.ChangeDatabase(dbName);

               //List<Activity> LstActivity = new List<Activity>();
               List<CRM_GetNotificationsList_Result> NotificationLst = obj.CRM_GetNotificationsList().ToList();
               var activitylist = NotificationLst.Select(Acti => new Activity
               {
                   // ID = Acti.ActivityID ?? 0,
                   ID = Acti.ActivityID.GetValueOrDefault(),
                   ActivityName = Acti.ActivityName,
                   CreatedDate = Acti.CreatedDate,
                   DueDate = Acti.DueDate,
                   RemindDate = Acti.RemindDate,
                   Priority = Acti.PriorityName,
                   Status = Acti.StatusName,
                   AccountTypeName = Acti.AccountTypeName

               }).ToList();

             

               var Cevent = (from a in activitylist.AsEnumerable()
                             select new CalendarEvent
                             {
                                 id = Convert.ToInt32(a.ID),
                                 start_date = Convert.ToDateTime(a.DueDate),
                                 end_date = Convert.ToDateTime(a.RemindDate),
                                 text = a.ActivityName,
                                 Module = a.AccountTypeName

                             }).ToList();

               return Cevent;

               // return MergedActivites;
           }
           catch (Exception)
           {
               throw;
           }

       }
       public List<Mentor> GetAllMentorsList(Int64 ventureid,string Connectionstring, string dbName)
       {
           try
           {
              
               DevEntities objentities = new DevEntities();
               objentities.Database.Connection.Open();
               objentities.Database.Connection.ChangeDatabase(dbName);
               List<Mentor> objlistmentors = new List<Mentor>();
               if (ventureid != 0)
               {
                  var  ment = from mentors in objentities.Mentors
                              where mentors.VentureId == ventureid
                              select mentors;
                   objlistmentors = ment.ToList();
               }
               else
               {
                  var  ment = from mentors in objentities.Mentors
                            
                              select mentors;
                     objlistmentors = ment.ToList();
               }
               
               return  objlistmentors;
           }
           catch (Exception)
           {
               
               throw;
           }
       }
       public long InsertSchedulerAppointment(CalendarEventRequest calevent, string Connectionstring, string dbName)
       {
           try
           {
               using (DevEntities dbContext = new DevEntities(Connectionstring))
               {
                   if (dbContext.Database.Connection.State == ConnectionState.Closed)
                   {
                       dbContext.Database.Connection.Open();
                   }
                   dbContext.Database.Connection.ChangeDatabase(dbName);
                   dbContext.CalendarEventRequests.Add(calevent);
                   dbContext.SaveChanges();
               }
               return calevent.ID;
           }
           catch (Exception)
           {
               throw;
           }
       }
       public long InsertSchedulerMeetingDetails(EventDetail evtdetails, string Connectionstring, string dbName)
       {
           try
           {
               using (DevEntities dbContext = new DevEntities(Connectionstring))
               {
                   if (dbContext.Database.Connection.State == ConnectionState.Closed)
                   {
                       dbContext.Database.Connection.Open();
                   }
                   dbContext.Database.Connection.ChangeDatabase(dbName);
                   dbContext.EventDetails.Add(evtdetails);
                   dbContext.SaveChanges();
               }
               return evtdetails.Id;
           }
           catch (Exception)
           {
               throw;
           }
       }

       public List<CalendarEventRequest> GetRecentAppointments(string Keyword, Int64 id, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName, int Ismentor, int isdashboard,string roleid)
       {

           try
           {
               using (DevEntities dbContext = new DevEntities(Connectionstring))
               {
                   if (dbContext.Database.Connection.State == ConnectionState.Closed)
                   {
                       dbContext.Database.Connection.Open();
                   }
                   dbContext.Database.Connection.ChangeDatabase(dbName);
                   var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                   //List<CRM_GetAllAccountContacts_Result> ContactsList = dbContext.CRM_GetAllAccountContacts(jtStartIndex, jtPageSize, jtSorting, output).ToList();
                   List<Appointments_GetAllAppointmentsbyMentorId_Result> lstrecentevents = dbContext.Appointments_GetAllAppointmentsbyMentorId(Keyword, jtStartIndex, jtPageSize, jtSorting, output, id, Ismentor, isdashboard,Convert.ToInt32(roleid)).ToList();

                   var Lstevents = lstrecentevents.Select(recenteve => new CalendarEventRequest
                   {
                       ID = recenteve.ID.GetValueOrDefault(),
                       Title=recenteve.Title,
                       Time1 = recenteve.Time1,
                       strDate1 = Convert.ToDateTime(recenteve.Date1).ToString("MM/dd/yyyy"),
                       Time2 = recenteve.Time2,
                       strDate2 = Convert.ToDateTime(recenteve.Date2).ToString("MM/dd/yyyy"),
                       Date2 = recenteve.Date2,
                       strDate3 = Convert.ToDateTime(recenteve.Date3).ToString("MM/dd/yyyy"),
                       Time3 = recenteve.Time3,
                       Date3 = recenteve.Date3,
                       Location=recenteve.Location,
                       Description=recenteve.Description,
                       Hostedby = recenteve.VentureName,
                       strCreatedDate = Convert.ToDateTime(recenteve.CreatedDate).ToString("MM/dd/yyyy"),
                       CreatedDate=recenteve.CreatedDate,
                       CreatedBy=recenteve.CreatedBy,
                      venturename=recenteve.VentureName

                   }).ToList();
                   RecordCount = Convert.ToInt32(output.Value);
                  
                   return Lstevents;
               }
           }
           catch (Exception)
           {
               throw;
           }

       }
       public CalendarEventRequest GetAppointmentDetailsByID(long id,Int64 mentorid, string Connectionstring, string dbName)
       {
           try
           {
               using (DevEntities dbContext = new DevEntities(Connectionstring))
               {
                   if (dbContext.Database.Connection.State == ConnectionState.Closed)
                   {
                       dbContext.Database.Connection.Open();
                   }
                   dbContext.Database.Connection.ChangeDatabase(dbName);

                   CalendarEventRequest objcalevents = dbContext.CalendarEventRequests.Where(calevents => calevents.ID == id).FirstOrDefault();
                   string mentid=Convert.ToString(mentorid);
                   int calid=Convert.ToInt32(id);
                   List<MentorAcceptedDateTime> objaccepteddatetime = dbContext.MentorAcceptedDateTimes.Where(accdatetime => accdatetime.MeetingId == id && accdatetime.MentorId == mentid).ToList();
                   if (mentorid != 0)
                   {
                       EventDetail objevedetails = dbContext.EventDetails.Where(evedetails => evedetails.MeetingId == id && evedetails.MentorId == mentorid).FirstOrDefault();
                       if (objevedetails.IsAccepted == null)
                       {
                           objcalevents.strIsaccepted = "-";
                       }
                       else
                       {
                           objcalevents.strIsaccepted = Convert.ToString(objevedetails.IsAccepted);
                       }
                       if (objevedetails.IsDenied == null)
                       {
                           objcalevents.strisdenied = "-";
                       }
                       else
                       {
                           objcalevents.strisdenied = Convert.ToString(objevedetails.IsDenied);
                       }
                       
                   }
                   foreach (var accept in objaccepteddatetime)
                   {
                       string[] accepteddate = Convert.ToString(accept.AcceptedDateTime).Split(' ');
                       string[] accepttime = accepteddate[1].Split(':');
                       string timeaccepet = accepttime[0] +":"+ accepttime[1]+accepteddate[2].ToLower();
                       string[] date1 = Convert.ToString(objcalevents.Date1).Split(' ');
                       string[] date2 = Convert.ToString(objcalevents.Date2).Split(' ');
                       string[] date3 = Convert.ToString(objcalevents.Date3).Split(' ');
                       if (accepteddate[0] == date1[0])
                       {
                           objcalevents.RdAcceptedDate1 = true;

                           string[] time1date1 = objcalevents.Time1.Split('|');
                           if (timeaccepet == time1date1[0])
                           {
                               objcalevents.chkDT1Time1 = true;
                           }
                           if (timeaccepet == time1date1[1])
                           {
                               objcalevents.chkDT1Time2 = true;
                           }
                           if (timeaccepet == time1date1[2])
                           {
                               objcalevents.chkDT1Time3 = true;
                           }
                       }
                       if (accepteddate[0] == date2[0])
                       {
                           objcalevents.RdAcceptedDate2 = true;
                           string[] time1date2 = objcalevents.Time2.Split('|');
                           if (timeaccepet == time1date2[0])
                           {
                               objcalevents.chkDT2Time1 = true;
                           }
                           if (timeaccepet == time1date2[1])
                           {
                               objcalevents.chkDT2Time2 = true;
                           }
                           if (timeaccepet == time1date2[2])
                           {
                               objcalevents.chkDT2Time3 = true;
                           }
                       }
                       if (accepteddate[0] == date3[0])
                       {
                           objcalevents.RdAcceptedDate3 = true;
                           string[] time1date3 = objcalevents.Time3.Split('|');
                           if (timeaccepet == time1date3[0])
                           {
                               objcalevents.chkDT3Time1 = true;
                           }
                           if (timeaccepet == time1date3[1])
                           {
                               objcalevents.chkDT3Time2 = true;
                           }
                           if (timeaccepet == time1date3[2])
                           {
                               objcalevents.chkDT3Time3 = true;
                           }
                       }

                   }
                  
                   
                  
                   return objcalevents;
               }
           }
           catch (Exception)
           {
               throw;
           }
       }

       public int updateEventDetails(EventDetail objEvedetails, string Connectionstring, string dbName)
       {
           int isInserted = 0;
           try
           {
               using (DevEntities dbContext = new DevEntities(Connectionstring))
               {
                   if (dbContext.Database.Connection.State == ConnectionState.Closed)
                   {
                       dbContext.Database.Connection.Open();
                   }
                   dbContext.Database.Connection.ChangeDatabase(dbName);
                   EventDetail getEventDetails = dbContext.EventDetails.Where(c => c.MentorId ==objEvedetails.MentorId && c.MeetingId==objEvedetails.MeetingId).SingleOrDefault();
                   if (getEventDetails != null)
                   {
                       getEventDetails.IsDenied = objEvedetails.IsDenied;
                       getEventDetails.IsAccepted = objEvedetails.IsAccepted;
                     
                       getEventDetails.ModifiedDate = DateTime.Now;
                       dbContext.SaveChanges();

                       isInserted = 1;
                   }
               }
           }
           catch (Exception ex)
           {
               throw;
           }
           return isInserted;
       }

       public List<MentorAcceptedDateTime> GetVentureAppointmentDetailsByID(string Keyword, Int64 id, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
       {
           try
           {
               using (DevEntities dbContext = new DevEntities(Connectionstring))
               {
                   if (dbContext.Database.Connection.State == ConnectionState.Closed)
                   {
                       dbContext.Database.Connection.Open();
                   }
                   dbContext.Database.Connection.ChangeDatabase(dbName);
                   var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));


                   List<GetAllMentorsAcceptedDateTime_Result> lstrecentevents = dbContext.GetAllMentorsAcceptedDateTime(Keyword, jtStartIndex, jtPageSize, jtSorting, output, id).ToList();

                   var Lstevents = lstrecentevents.Select(recenteve => new MentorAcceptedDateTime
                   {
                       AcceptedDateTime=recenteve.AcceptedDateTime,
                       MentorId=recenteve.MentorId,
                       stracceptdatetime=Convert.ToDateTime(recenteve.AcceptedDateTime).ToString(),
                       MentorName = recenteve.Mentorname,
                       accepttime=recenteve.Accepttime
                   }).ToList();
                   //if (output.Value != "" && output.Value != null)
                   //{
                   //    RecordCount = Convert.ToInt32(output.Value);
                   //}
                   //else
                   //{
                       RecordCount = 0;
                  // }

                   return Lstevents;
               }
           }
           catch (Exception)
           {
               throw;
           }
       }
       public List<Event> GetAllEvents(int jtStartIndex, int jtPageSize, out int RecordCount, string Connectionstring, string dbName)
       {
           try
           {
               using (DevEntities dbContext = new DevEntities(Connectionstring))
               {
                   if (dbContext.Database.Connection.State == ConnectionState.Closed)
                   {
                       dbContext.Database.Connection.Open();
                   }
                   dbContext.Database.Connection.ChangeDatabase(dbName);
                   var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                   List<GET_Events_Result> getevents = dbContext.GET_Events(jtPageSize, jtStartIndex, output).ToList();
                   var Lstevents = getevents.Select(recenteve => new Event
                   {
                       EventId = recenteve.EventId.GetValueOrDefault(),
                       EventDate = recenteve.EventDate,
                       Title = recenteve.Title,
                       Description = recenteve.Description,
                       Location = recenteve.Location

                   }).ToList();
                   RecordCount = Convert.ToInt32(output.Value);
                   return Lstevents;
               }

           }
           catch (Exception)
           {
               throw;
           }

       }
       public List<Venture> GetAllVentureList(string Connectionstring, string dbName)
       {
           try
           {

               DevEntities objentities = new DevEntities();
               objentities.Database.Connection.Open();
               objentities.Database.Connection.ChangeDatabase(dbName);
               List<Venture> objlistventures = new List<Venture>();
              var vent = from ventures in objentities.Ventures

                         select ventures;
                   objlistventures = vent.ToList();

                   return objlistventures;
           }
           catch (Exception)
           {

               throw;
           }
       }

       public int InsertSchedulerAcceptedDatetime(MentorAcceptedDateTime objmentoraccepteddatetime, string Connectionstring, string dbName)
       {
           try
           {
               using (DevEntities dbContext = new DevEntities(Connectionstring))
               {
                   if (dbContext.Database.Connection.State == ConnectionState.Closed)
                   {
                       dbContext.Database.Connection.Open();
                   }
                   dbContext.Database.Connection.ChangeDatabase(dbName);
                   dbContext.MentorAcceptedDateTimes.Add(objmentoraccepteddatetime);
                   dbContext.SaveChanges();
               }
               return objmentoraccepteddatetime.Id;
           }
           catch (Exception)
           {
               throw;
           }
       }
       public long InsertScheduledAppointment(Event objevent, string Connectionstring, string dbName)
       {
           try
           {
               using (DevEntities dbContext = new DevEntities(Connectionstring))
               {
                   if (dbContext.Database.Connection.State == ConnectionState.Closed)
                   {
                       dbContext.Database.Connection.Open();
                   }
                   dbContext.Database.Connection.ChangeDatabase(dbName);
                   dbContext.Events.Add(objevent);
                   dbContext.SaveChanges();
               }
               return objevent.EventId;
           }
           catch (Exception)
           {
               throw;
           }
       }
       public List<Event> GetAllEventsByMeetingId(int Meetingid, string Connectionstring, string dbName)
       {
           try
           {
               DevEntities objentities = new DevEntities();
               objentities.Database.Connection.Open();
               objentities.Database.Connection.ChangeDatabase(dbName);
               List<Event> objeventslist = new List<Event>();
               if (Meetingid != 0)
               {
                   var eve = from events in objentities.Events
                              where events.MeetingId == Meetingid
                              select events;
                   objeventslist = eve.ToList();
               }


               return objeventslist;
           }
           catch (Exception)
           {

               throw;
           }
       }
      
    }
}
