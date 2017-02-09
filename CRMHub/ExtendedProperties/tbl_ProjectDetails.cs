using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(tbl_ProjectDetailsMetaData))]
    public partial class tbl_ProjectDetails
    {

        public string ContactName { get; set; }
    }


    public partial class tbl_ProjectDetailsMetaData
    {
        public int ProjectId { get; set; }
        public string Projectname { get; set; }
        public Nullable<int> Contact { get; set; }
        public string Status { get; set; }
        public string URL { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        [AllowHtml]
        public string Description { get; set; }
    }
}
