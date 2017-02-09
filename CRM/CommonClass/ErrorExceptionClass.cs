using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM
{
    public class ErrorExceptionClass
    {
        ErrorLogRepository errorlogrepository = new ErrorLogRepository();


        public  void catchException(string message,string pageName)
        {
            Error_Log objErrorLog = new Error_Log();
            objErrorLog.CreatedDate = DateTime.Now;
            objErrorLog.ErrorMessage = message;
            
            objErrorLog.ErrorPage = pageName;//controllerName + "/" + actionName;

            errorlogrepository.InsertErrorLogException(objErrorLog);
        }
    }
}