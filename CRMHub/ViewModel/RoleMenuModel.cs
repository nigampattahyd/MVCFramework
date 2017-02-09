using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class RoleMenuModel
    {
        public List<role> lstRoles { get; set; }
        public user users { get; set; }
        public tbl_Menus menuEntity { get; set; }
        public tbl_RoleMenus roleMenuEntity { get; set; }
        public List<RoleMenuModel> listRoleMenu { get; set; }
    }
}
