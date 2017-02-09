using CRMHub.EntityFramework;
using CRMHub.ExtendedProperties;
using CRMHub.Interface;
using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CRM.Controllers
{
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemrepository;
        public ItemsController(IItemRepository iitemrepository)
        {
            _itemrepository = iitemrepository;
        }

        public ActionResult Index()
        {

            if (TempData["createitem"] != null)
            {
                ViewBag.createitem = "Create";

            }

            if (TempData["Updateitem"] != null)
            {
                ViewBag.Updateitem = "Update";

            }
            return View();
        }

        public ActionResult InsertItems()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertItems(tbl_Items ItemObj)
        {
            try
            {
                TempData["createitem"] = "createitem";
                int result =  _itemrepository.createitem(ItemObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult getItemsList(jQueryDataTableParamModel param)
        {
            try
            {
                //string ProjectName = Request["Projectname"];
                //string Status = Request["ProStatus"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "ItemID desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                var listProject =  _itemrepository.GetAllItemsList(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < listProject.Count; i++)
                {
                    if (listProject[i].ItemTypeID == 0)
                    {
                        listProject[i].ItemDesc = "Select";
                    }
                    else if (listProject[i].ItemTypeID == 1)
                    {
                        listProject[i].ItemDesc = "Inventory";
                    }
                    else if (listProject[i].ItemTypeID == 2)
                    {
                        listProject[i].ItemDesc = "Non Inventory";
                    }
                    else if (listProject[i].ItemTypeID == 3)
                    {
                        listProject[i].ItemDesc = "Service";
                    }

                }
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listProject,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult EditItemsDetail(string ItemID)
        {
            try
            {
                tbl_Items objitems = new tbl_Items();
                objitems =  _itemrepository.edititemdetailsbyitemid(Convert.ToInt32(ItemID), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return View(objitems);
            }

            catch (Exception)
            {
                throw;
            }


        }

        [HttpPost]
        public ActionResult Updateitems(tbl_Items objitems)
        {
            try
            {
                TempData["Updateitem"] = "Updateitem";
                //objproject.ProjectDetails.Contact = objproject.ProjectDetails.ContactId;
                int result =  _itemrepository.UpdateitemDetailsByitemId(objitems, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public bool Deleteitems(string Itemids)
        {
            try
            {
                if (!string.IsNullOrEmpty(Itemids))
                {
                    foreach (var itemid in Itemids.Split(','))
                    {
                        var deleteitemid =  _itemrepository.DeleteItemByItemId(Convert.ToInt32(itemid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}