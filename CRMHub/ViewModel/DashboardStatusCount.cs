using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;


namespace CRMHub.ViewModel
{
    [Serializable]
   public class DashboardStatusCount
    {
        public DashboardStatusCount()
        {
          //  DashboardStatusCountResultLists = new List<CRM_Dashboard_StatusCount_Result>();
            DashboardChartStatusCountResultLists = new List<CRM_Dashboard_ChartStatusCount_Result>();
           // DashboardChartStatusCountNewResultLists = new List<CRM_Dashboard_StatusCount_New_Result>();
            //CRMDashboardStatusCountlatestdetailsLst = new List<CRM_Dashboard_StatusCountdetails_Result>();
            CRMInvoicePaymentChartStatusCountlst = new List<CRM_InvoicePayment_ChartStatusCount_Result>();
            CRMPaymentDetailslst = new List<CRM_Dashboard_PaymentsDetails_Result>();

           

        }
       // public List<CRM_Dashboard_StatusCount_Result> DashboardStatusCountResultLists { get; set; }
        public List<CRM_Dashboard_ChartStatusCount_Result> DashboardChartStatusCountResultLists { get; set; }
       // public List<CRM_Dashboard_StatusCount_New_Result> DashboardChartStatusCountNewResultLists { get; set; }
        //public List<CRM_Dashboard_StatusCountdetails_Result> CRMDashboardStatusCountlatestdetailsLst { get; set; }
        public List<CRM_InvoicePayment_ChartStatusCount_Result> CRMInvoicePaymentChartStatusCountlst { get; set; }
        public List<CRM_Dashboard_PaymentsDetails_Result> CRMPaymentDetailslst { get; set; }

        public List<CRM_Dashboard_ModulesCountdetails_Result> CRMDashboardStatusCountlatestdetailsLst { get; set; }

       
        public DashBoardTitle DBTitles { get; set; }
    }
}
