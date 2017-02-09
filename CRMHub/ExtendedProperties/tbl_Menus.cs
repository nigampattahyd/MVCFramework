using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(tbl_MenusMetaData))]
    public partial class tbl_Menus
    {
        public string ParentMenuDescription { get; set; }
        public string IsSelected { get; set; }
    }
   public partial class tbl_MenusMetaData
    {
        public int MenuId { get; set; }
        public string MenuDescription { get; set; }
        public string NavigateURL { get; set; }
        public Nullable<int> ParentMenuId { get; set; }
        public string ParentMenuDescription { get; set; }
    }
}
