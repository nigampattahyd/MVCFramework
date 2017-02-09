
using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
    public interface IMenuRepository
    {
        List<tbl_Menus> GetMenus(string Connectionstring, string dbName);
        List<tbl_Menus> GetParentMenu(string Connectionstring, string dbName);
        int insertMenu(tbl_Menus menus, string Connectionstring, string dbName);
        tbl_Menus getMenuByMenuId(int MenuId, string Connectionstring, string dbName);
        int updateMenuByMenuId(tbl_Menus objMenu, string Connectionstring, string dbName);
        int DeleteMenusDetailsByLeadId(int Menuid, string Connectionstring, string dbName);
    }
}