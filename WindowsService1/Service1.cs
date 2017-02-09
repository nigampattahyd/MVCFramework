using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using CRMHub.EntityFramework;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using CRM;
using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using System.Timers;
namespace WindowsService1
{


    public partial class CRM_BULK_IMPORT : ServiceBase
    {
        public CRM_BULK_IMPORT()
        {
            InitializeComponent();
        }

        static Timer timer;
        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Interval = 60000;//set interval of one day 
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            start_timer();
            
        }
        private static void start_timer()
        {
            //EventLog.WriteEntry("CRM_BULK_IMPORT", "CRM Timer Execution Started" + DateTime.Now.ToShortDateString());
            timer.Start();
        }

        protected override void OnStop()
        {

        }
        private void timer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {

            BusinessLogic bs = new BusinessLogic();
            //EventLog.WriteEntry("CRM_BULK_IMPORT", "CRM getexcel method called" + DateTime.Now.ToShortDateString());
            bs.GetExcelList();
           
        }







       




    }
}
