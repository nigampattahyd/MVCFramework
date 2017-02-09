using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{
     [MetadataType(typeof(OpportunityMetaData))]
    public partial class Opportunity
    {
         public string CompanyName { get; set; }
         public string ContactName { get; set; }
         public string Owner { get; set; }
         public string OwnerFirstName { get; set; }
         public string OwnerLastName { get; set; }
         public string Stage { get; set; }

    }


    public partial class OpportunityMetaData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> ContactID { get; set; }
        public Nullable<decimal> EstimateBudget { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public Nullable<int> StageID { get; set; }
        public Nullable<int> BusinessTypeID { get; set; }
        public string NextStep { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string Probability { get; set; }
        public Nullable<decimal> ExpectedRevenue { get; set; }
        public Nullable<int> OppurtunitySourceID { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> OwnerID { get; set; }
    }
}
