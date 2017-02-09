using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;

namespace CRMHub.Repository
{
    public class NotificationsRepository : INoticationRepository
    {
        public List<Activity> GetAllNotificationslist(string Keyword,int id,int roleid,int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                List<CRM_GETALLMentoringNotifications_Result> LstNotifications = obj.CRM_GETALLMentoringNotifications(Keyword,id,roleid, startIndex, pageSize, orderByClause, output).ToList();

                List<Activity> ActivityNotification = LstNotifications.Select(Acti => new Activity
                {
                    ID = Acti.ActivityID ?? 0,
                    ActivityName = Acti.ActivityName,
                    CreatedDate = Acti.CreatedDate,
                    DueDate = Acti.DueDate,
                    RemindDate = Acti.RemindDate,
                    AccountTypeName = Acti.AccountTypeName,
                    Priority = Acti.PriorityName,
                    Status = Acti.StatusName,
                    AssignedName = Acti.AssignedFirstName + ' ' + Acti.AssignedLastName

                }).ToList();
                TotalCount = Convert.ToInt32(output.Value);
                return ActivityNotification;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
