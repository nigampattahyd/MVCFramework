using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{

    [MetadataType(typeof(roleMetaData))]
    public partial class role
    {
        public string ActiveStatus { get; set; }
        public Nullable<bool> RoleStatus { get; set; }
        public string Keyword { get; set; }
    }
    public partial class roleMetaData
    {
        public long RoleId { get; set; }
        [Remote("IsRoleExists", "Users", AdditionalFields = "RoleId,RoleName", ErrorMessage = "Role already exists")]
        public string RoleName { get; set; }
         [AllowHtml]
        public string Description { get; set; }
        public string Status { get; set; }
     


    }
}
