using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using WindowsService1;
//using CRMCompanyImport;

namespace ServiceConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {
                BusinessLogic s1 = new BusinessLogic();
                //Timer myTimer = null;
                //var timerDelegate1 = new TimerCallback(s1.GetExcelList);
                //myTimer = new System.Threading.Timer(timerDelegate1, "true", 0, Timeout.Infinite);
             
                //System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));
                s1.GetExcelList();
            }

        
            Console.ReadLine();
            
        }



    }
}
