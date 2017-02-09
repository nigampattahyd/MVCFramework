using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Repository
{
    public class DBoardRepository : IDBoard
    {
        public List<Event> GetEvents(string Role, long Id,string keyword, string Connectionstring, string dbName)
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
                    List<Scheduler_GetAllMeetings_Result> lstAppointments = dbContext.Scheduler_GetAllMeetings(Role, Id, keyword).ToList();

                    return lstAppointments.Select(evt => new Event
                        {
                            EventId =Convert.ToInt64(evt.EventId),
                            Title = evt.Title,
                            EventDate = evt.EventDate,
                            EventTime = evt.EventTime.Trim(),
                            Location = evt.Location,
                            Description = evt.Description,
                            Mentors = evt.Mentors,
                            //CreatedBy = evt.CreatedBy,
                           //CreatedDate = evt.CreatedDate,
                            //ModifedBy = evt.ModifedBy,
                            //ModifiedDate = evt.ModifiedDate
                        }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
