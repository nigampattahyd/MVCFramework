using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using CRMHub.ViewModel;
using System.Configuration;
using System.IO;
using CRMHub.ExtendedProperties;
using System.Web.UI;
using System.Data;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CRM.Controllers
{
    public class InventoryController : ControllerBase
    {
        private readonly IItemRepository _itemrepository;
        private readonly IInvoiceRepository _invoicerepository;
        public InventoryController(IItemRepository iitemrepository,IInvoiceRepository iinvoicerepository)
        {
            _itemrepository = iitemrepository;
            _invoicerepository = iinvoicerepository;
        }
        // GET: Inventory
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
                var listProject = _itemrepository.GetAllItemsList(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
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


        public ActionResult CreateItems()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateItems(tbl_Items ItemObj)
        {
            try
            {
                TempData["createitem"] = "createitem";
                int result = _itemrepository.createitem(ItemObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return RedirectToAction("Index");
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
                objitems = _itemrepository.edititemdetailsbyitemid(Convert.ToInt32(ItemID), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
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
                int result = _itemrepository.UpdateitemDetailsByitemId(objitems, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


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
                bool returnval = false;
                if (!string.IsNullOrEmpty(Itemids))
                {
                  //  int count 

                    foreach (var itemid in Itemids.Split(','))
                    {
                        int IdItem = Convert.ToInt32(itemid);

                        int itemIdCount = _invoicerepository.GetitemsCountfordelete(IdItem, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        if(itemIdCount == 0)
                        {
                            var deleteitemid = _itemrepository.DeleteItemByItemId(Convert.ToInt32(itemid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            returnval = true;
                        }
                        //var deleteitemid = _itemrepository.DeleteItemByItemId(Convert.ToInt32(itemid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }
                }
                return returnval;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}