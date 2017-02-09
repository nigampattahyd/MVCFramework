using CRM.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace WindowsService1
{
    static class Program
    {
        
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new CRM_BULK_IMPORT() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
