using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class MenusController : ControllerBase
    {
        private readonly IMenuRepository _imenurepository;
        public MenusController(IMenuRepository imenurepository)
        {
            _imenurepository = imenurepository;            
        }
        //
        // GET: /Menus/
        public ActionResult Index()
        {
            try
            {
                 List<tbl_Menus> menulist=new List<tbl_Menus>();
                 menulist = _imenurepository.GetMenus(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                 return View(menulist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult CreateMenus()
        {
            try
            {
                MenuViewModel objMenuModel = new MenuViewModel();
                objMenuModel.MenusList = new List<tbl_Menus>();
                objMenuModel.MenusList = _imenurepository.GetParentMenu(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return View(objMenuModel);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult CreateMenus(MenuViewModel menumodelobj)
        {
            try
            {
                int result = _imenurepository.insertMenu(menumodelobj.MenusEntity, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult EditMenu(int MenuId)
        {
            try
            {
                MenuViewModel objMenuModel = new MenuViewModel();
                objMenuModel.MenusEntity = _imenurepository.getMenuByMenuId(MenuId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return View(objMenuModel);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult UpdateMenus(tbl_Menus objMenuModel,FormCollection fc)
        {
            try
            {
                objMenuModel.MenuDescription = fc[0];
                objMenuModel.MenuId = int.Parse(fc[1]);
                objMenuModel.NavigateURL = fc[2];
                int result = _imenurepository.updateMenuByMenuId(objMenuModel, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return RedirectToAction("EditMenu", new { objMenuModel.MenuId });
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public bool DeleteMenus(string menuid)
        {
            try
            {
                if (!string.IsNullOrEmpty(menuid))
                {
                    foreach (var menuids in menuid.Split(','))
                    {
                        var deleteOppor =_imenurepository.DeleteMenusDetailsByLeadId(Convert.ToInt32(menuid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }
                }
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }
	}
}