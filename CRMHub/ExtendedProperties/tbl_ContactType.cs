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



    [MetadataType(typeof(tbl_ContactTypeMetaData))]
    public partial class tbl_ContactType
    {
        public string Activestatus { get; set; }
    }

    public partial class tbl_ContactTypeMetaData
    {
        public int ContactTypeId { get; set; }
        public string ContactType { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
