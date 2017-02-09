using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CRM.Controllers
{
    public class ToolsController : ControllerBase
    {
        private readonly ICommonRepository _icommonrepository;
        private readonly IRoleRepository _irolerepository;
        public ToolsController(ICommonRepository icommonrepository, IRoleRepository irolerepository)
        {
            _icommonrepository = icommonrepository;
            _irolerepository = irolerepository;
        }

        //
        // GET: /Tools/
        public ActionResult AccessMenus(RoleMenuModel roleMenumodel)
        {
            try
            {
                List<role> lstrole = new List<role>();
                //roleMenumodel.lstRoles = _irolerepository.GetLevels(GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                role selectRole = new role();
                selectRole.RoleId = -1;
                selectRole.RoleName = "Select Role";

                roleMenumodel.lstRoles = new List<role>();

                roleMenumodel.lstRoles.Add(selectRole);
               // lstrole.Add(selectRole);
                roleMenumodel.lstRoles.AddRange(_irolerepository.GetLevels(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
               // usermodel.listBranch.AddRange(_iuserrepository.getUserbranchlist(loginid, roleid, GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                lstrole = roleMenumodel.lstRoles.Where(x => x.RoleId != 1).ToList();
                
             
                //lstrole = roleMenumodel.lstRoles.Where(x => x.RoleId != 10 && x.RoleId != 13 && x.RoleId != 18).ToList();
                roleMenumodel.lstRoles = lstrole;

                roleMenumodel.users = new user();
                //roleMenumodel.users.RoleId = Convert.ToInt64(GlobalVariables.RoleID);
                roleMenumodel.users.RoleId = roleMenumodel.lstRoles[0].RoleId;
                roleMenumodel.listRoleMenu = _icommonrepository.getMenuByRoleId(Convert.ToInt32(roleMenumodel.users.RoleId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //roleMenumodel.listRoleMenu = _icommonrepository.getMenuByRoleId(Convert.ToInt32(GlobalVariables.RoleID), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                var selectedItems = roleMenumodel.listRoleMenu.Select(a => a.menuEntity.MenuId).ToList();
                var selectedItemsarray = string.Empty;
                foreach (var item in selectedItems)
                {
                    if (!string.IsNullOrEmpty(selectedItemsarray))
                        selectedItemsarray = selectedItemsarray + ',' + item.ToString();
                    else
                        selectedItemsarray = item.ToString();
                }
                ViewBag.listSelectedRoleMenu = selectedItemsarray;
                return View(roleMenumodel);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public ActionResult changeRole(string roleId)
        {
            try
            {
                RoleMenuModel roleMenuModel = new RoleMenuModel();
                roleMenuModel.listRoleMenu = _icommonrepository.getMenuByRoleId(Convert.ToInt32(roleId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                for(int s=0; s<roleMenuModel.listRoleMenu.Count;s++)
                {
                    roleMenuModel.listRoleMenu[s].menuEntity.IsSelected = "True";
                }
                //.Where(s => s.IsSelected == false).ToList();
                List<RoleMenuModel> selectedItems1 = roleMenuModel.listRoleMenu.ToList().Where(s=>s.roleMenuEntity.IsChecked == false).ToList();
                List<tbl_Menus> myList = new List<tbl_Menus>();
                for (int a = 0; a < selectedItems1.Count; a++)
                {
                    tbl_Menus menuobj = new tbl_Menus();
                    menuobj.IsSelected = selectedItems1[a].menuEntity.IsSelected;
                    menuobj.MenuId = selectedItems1[a].menuEntity.MenuId;
                    menuobj.ParentMenuId = selectedItems1[a].menuEntity.ParentMenuId; 
                    myList.Add(menuobj);                                       
                }

                var myArrayList = myList.ToArray();
                var selectedItems = roleMenuModel.listRoleMenu.Select(a => a.menuEntity.MenuId).ToList();
                var selectedItemsarray = string.Empty;
              
                foreach (var item in selectedItems)
                {
                    if (!string.IsNullOrEmpty(selectedItemsarray))
                        
                        selectedItemsarray = selectedItemsarray + ',' + item.ToString();
                    else
                        selectedItemsarray = item.ToString();
                }
               // listSelectedRoleMenu
                ViewBag.listSelectedRoleMenu = selectedItemsarray;
                ViewBag.listSelectedRoleMenufilter = myArrayList;

                return Json(selectedItemsarray, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public ActionResult GetMenus()
        {
            try
            {
                List<tbl_Menus> listMenu = _icommonrepository.getMenus(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                var recursiveMenuObjects = FillMenuRecursive(listMenu, 0);
                return Json(recursiveMenuObjects, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                throw;
            }
        }

        private static List<MenuModel> FillMenuRecursive(List<tbl_Menus> menuObjects, int parentId)
        {
            try
            {
                List<MenuModel> recursiveObjects = new List<MenuModel>();
                foreach (var item in menuObjects.Where(x => x.ParentMenuId.Equals(parentId)))
                {
                    recursiveObjects.Add(new MenuModel
                    {
                        text = item.MenuDescription,
                        id = item.MenuId,
                        state = new MenuModelAttribute { opened = false, selected = false },
                        children = FillMenuRecursive(menuObjects, item.MenuId)
                    });
                }


                return recursiveObjects;
            }
            catch(Exception)
            {
                throw;
            }
        }

        //[HttpPost]
        //public ActionResult InsertMenus(string RoleMenuDT)
        //{
        //    try
        //    {
        //        List<tbl_RoleMenus> lstRoleMenu = JsonConvert.DeserializeObject<List<tbl_RoleMenus>>(RoleMenuDT);
        //        DataTable dtMenuRole = new DataTable();
        //        dtMenuRole = LTDT.ToDataTable(lstRoleMenu);
               
        //        int result = _icommonrepository.InsertMenuByRole(dtMenuRole, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        return RedirectToAction("AccessMenus");
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        // written by Raghu

        [HttpPost]
        public ActionResult InsertMenus(string RoleMenuDT)
        {
            try
            {
                List<tbl_RoleMenus> lstRoleMenu = new List<tbl_RoleMenus>();
                lstRoleMenu = JsonConvert.DeserializeObject<List<tbl_RoleMenus>>(RoleMenuDT);
                DataTable dtMenuRole = new DataTable();
                dtMenuRole = LTDT.ToDataTable(lstRoleMenu);
                // Code Written by raghu
                int Roleid = lstRoleMenu[0].RoleId;
                int MenuId = lstRoleMenu[0].MenuId;

                // First Delete all the menus which are assigned to the Roleid from tbl_Rolemenus table.
                bool result = _icommonrepository.DeleteAccessmenuForRold(Roleid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

               
                if (MenuId!=0)
                {

                    bool InsResult = _icommonrepository.insertAccessmenuForRold(lstRoleMenu, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                   

                }
                // After deletion insert the selected menu item's for the role Id








              
                return RedirectToAction("AccessMenus");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}