using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Repository
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        public int InsertErrorLogException(Error_Log objErrorLog)
        {
            try
            {
                var dbCons = new DevEntities();
                var res = dbCons.CRM_InsertErrorLog(objErrorLog.ErrorMessage, objErrorLog.ErrorPage, objErrorLog.CreatedDate);
                return res;
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
