using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    public class DashBoardTitle
    {
        public string invoiceoverview { get; set; }
        public string EstimateOverview { get; set; }
        public string Activities { get; set; }
        public string Payments { get; set; }
        public string Contacttitle { get; set; }
        public string companytitle { get; set; }

        //[DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime FromDate { get; set; }
        //[DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime ToDate { get; set; }

    }
}
