using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
    public interface IErrorLogRepository
    {
        int InsertErrorLogException(Error_Log objErrorLog);
    }
}
