using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CRMHub.Repository
{
    public class MenuRepository : IMenuRepository
    {
        public List<tbl_Menus> GetMenus(string Connectionstring, string dbName)
        {
            try
            {
                DevEntities clientenity = new DevEntities(Connectionstring);
                clientenity.Database.Connection.Open();
                clientenity.Database.Connection.ChangeDatabase(dbName);
                List<CRM_MenusBinding_Result> lstLeadResult = clientenity.CRM_MenusBinding().ToList();
                return lstLeadResult.Select(menus => new tbl_Menus
                {
                    MenuId=menus.MenuId,
                    MenuDescription = menus.MenuDescription,
                    NavigateURL = menus.NavigateURL,
                    ParentMenuId = menus.ParentMenuId,
                    ParentMenuDescription=menus.ParentMenuDescription
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<tbl_Menus> GetParentMenu(string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    List<getParentMenu_Result> lstimenus = ClientEntity.getParentMenu().ToList();
                    return lstimenus.Select(objparentmenu => new tbl_Menus
                    {
                        MenuId = objparentmenu.MenuId,
                        MenuDescription = objparentmenu.MenuDescription
                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int insertMenu(tbl_Menus menus, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var  result = obj.CRM_InsertMenu(menus.MenuDescription,menus.NavigateURL, menus.ParentMenuId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public tbl_Menus getMenuByMenuId(int MenuId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                List<CRM_GetMenusbyMenuId_Result> lstMenusResult = dbContext.CRM_GetMenusbyMenuId(MenuId).ToList();
                return lstMenusResult.Select(objMenu => new tbl_Menus
                {
                    MenuId = objMenu.MenuId,
                    MenuDescription = objMenu.MenuDescription,
                    NavigateURL =objMenu.NavigateURL,
                    ParentMenuId = objMenu.ParentMenuId,
                    }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int updateMenuByMenuId(tbl_Menus objMenu, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities dbContext = new DevEntities(Connectionstring);
                dbContext.Database.Connection.Open();
                dbContext.Database.Connection.ChangeDatabase(dbName);
                var result = dbContext.CRM_UpdateMenuByMenuId(objMenu.MenuId,objMenu.MenuDescription,objMenu.NavigateURL);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteMenusDetailsByLeadId(int Menuid, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                return obj.CRM_DeleteMenu(Menuid);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
