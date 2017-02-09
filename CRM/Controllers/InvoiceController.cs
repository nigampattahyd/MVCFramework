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
using System.Drawing.Printing;

namespace CRM.Controllers
{
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _iinvoicerepository;
        private readonly ICompanyRepository _icompanyrepository;
        public InvoiceController(IInvoiceRepository iinvoicerepository, ICompanyRepository icompanyrepository)
        {


            _iinvoicerepository = iinvoicerepository;
            _icompanyrepository = icompanyrepository;

        }


        # region Invoice

        public ActionResult Index()
        {
            if (TempData["CreateInvoice"] != null)
            {
                ViewBag.invoice = "CreateEstimate";

            }
            if (TempData["Updateinvoice"] != null)
            {
                ViewBag.invoiceupdate = "invoiceupdate";

            }
            if (TempData["CreatePostInvoice"] != null)
            {
                ViewBag.invoicecreateupdate = "CreatePostInvoice";

            }
            // TempData["CreatePostInvoice"]
            return View();
        }


        public ActionResult getInvoicelist(jQueryDataTableParamModel param)
        {
            try
            {
                string invoiceList = Request["b_invoice"];                
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "InvoiceId desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                var listinvoice = _iinvoicerepository.GetAllInvoiceList(invoiceList, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < listinvoice.Count; i++)
                {

                    if (listinvoice[i].InvoiceType == "Estimate - Invoice")
                    {
                        listinvoice[i].InvoiceType = "Estimate - Invoice / " + listinvoice[i].EstInvoiceNo;
                    }
                    if (listinvoice[i].Posted == 1)
                    {
                        listinvoice[i].Status = "Posted";
                    }

                }


                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listinvoice,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }







        //public ActionResult _GetCustomersList()
        //{
        //    try
        //    {
              
        //        return PartialView();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //public ActionResult getCustomerListforinvoice(jQueryDataTableParamModel param)
        //{
        //    try
        //    {
               
        //        string iSortCol_0 = Request["iSortCol_0"];
        //        string sSortDir_0 = Request["sSortDir_0"];
        //        string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
        //        string orderByClause = string.Empty;
        //        int totalPageCount = 0;
        //        if (iSortCol_0 == "0")
        //            orderByClause = "ContactId desc";
        //        else
        //            orderByClause = SortFieldName + " " + sSortDir_0;
        //        var Customerlst = _iinvoicerepository.GetAllCustomerList(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        // var listContacttype = _icontactrepository.GetAllContactTypesList(Contacttype == "" ? "" : Contacttype, Status == "0" ? null : Status, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            aaData = Customerlst,
        //            iTotalRecords = totalPageCount,
        //            iTotalDisplayRecords = totalPageCount
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        public ActionResult _GetItemsList()
        {
            try
            {
                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult getItemsListforPopup(jQueryDataTableParamModel param)
        {
            try
            {
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "ItemID desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                var itemslst = _iinvoicerepository.GetAllItemsListtobind(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //var itemslst = _iinvoicerepository.GetAllCustomerList(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //var itemslst = _iinvoicerepository.GetAllCustomerList(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = itemslst,
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
        public int GetItemsCount()
        {
            try
            {
                int itemsTotalCount = 0;
                //int Invid = Convert.ToInt32(InvoiceID);
                //bool res = _iinvoicerepository.ConvertInvoicetoPost(Invid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                itemsTotalCount = _iinvoicerepository.GetitemsListCount(GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                return itemsTotalCount;
            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpGet]
        [ActionName("_BindItemslist")]
        public ActionResult _BindItemslist()
        {

            try
            {
                InvoiceModel InvoBJ = new InvoiceModel();
                //tbl_ContactType selectOptionType = new tbl_ContactType();
                //selectOptionType.ContactType = "Select";
                //selectOptionType.ContactTypeId = 0;
                //contactModel.Lstcontacttype = new List<tbl_ContactType>();
                //contactModel.Lstcontacttype.Add(selectOptionType);
                //contactModel.Lstcontacttype.AddRange(_icontactrepository.GetAllContacttypeList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                //InvoBJ.itemslist =
                tbl_Items selectItemsOption = new tbl_Items();
                selectItemsOption.ItemName = "Select";
                selectItemsOption.ItemID = 0;
                InvoBJ.itemslist = new List<tbl_Items>();
                InvoBJ.itemslist.Add(selectItemsOption);
                InvoBJ.itemslist.AddRange(_iinvoicerepository.GetAllitemsList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                return PartialView(InvoBJ);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult getItemDetails(string ItemId)
        {
            try
            {
                int itemid = Convert.ToInt32(ItemId);
                var result = _iinvoicerepository.GetItemdetailsbyId(itemid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //return Objitemdetails;
                //var result = "";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }


        public ActionResult CreateInvoice()
        {
            try
            {
                InvoiceModel InvoBJ = new InvoiceModel();
                // Here get the List of Contacts from Contacts table where Contact Type is Customer
                InvoBJ.itemslist = new List<tbl_Items>();
                InvoBJ.itemsobj = new tbl_Items();
                InvoBJ.invoice = new tbl_Invoice();
                int SlastInvoiceno = _iinvoicerepository.SetlastInvoicenumber(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                string Lastinvoicenumber = "";
                SlastInvoiceno = SlastInvoiceno + 1;
                Lastinvoicenumber = string.Format("{0:000000}", SlastInvoiceno);
                InvoBJ.invoice.InvoiceNo = "INV-" + Lastinvoicenumber;
                DateTime currDate = DateTime.Now;
                InvoBJ.invoice.CreatedDate = currDate;

                DateTime dtSpecifiedDate = DateTime.Now.Date;

                DateTime RemindToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(30).ToString("MM/dd/yyyy"));
                DateTime DueToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(30).ToString("MM/dd/yyyy"));

                InvoBJ.invoice.DueDate = DueToDate;
                return View(InvoBJ);
            }
            catch (Exception)
            {
                throw;
            }


        }

        // Inserting the Invoice Post Method
        [HttpPost]
        public ActionResult SaveInvoice(InvoiceModel INVModelObj, string SavePostINV, string SaveInV)
        {
            try
            {
                //TempData["CreateInvoice"] = "Createinvoice";
                if (INVModelObj.invoice.EstInvoiceID != null)
                {
                    var EstimateList = INVModelObj.ListEstimateitems.ToList();
                    var EstimateItemmodel = (from p in EstimateList.AsEnumerable()
                                             select new tbl_InvoiceItem
                                             {
                                                 ItemCode = p.ItemCode,
                                                 ItemId = p.ItemId,
                                                 ItemName = p.ItemName,
                                                 ItemTotal = p.ItemTotal,
                                                 Quantity = p.Quantity,
                                                 RatePerUnit = p.RatePerUnit

                                             }).ToList();

                    INVModelObj.listinvoiceitems = EstimateItemmodel;
                    INVModelObj.invoice.InvoiceType = "Estimate - Invoice";
                }
                else
                {
                    INVModelObj.invoice.InvoiceType = "Invoice";
                }
                if (SavePostINV != null)
                {
                    TempData["CreatePostInvoice"] = "CreatePostinvoice";
                    if (INVModelObj.listinvoiceitems.Count != 0)
                    {
                        int lastInvoiceid = 0;
                        INVModelObj.invoice.Posted = 1;
                        INVModelObj.invoice.BalanceAmount = INVModelObj.invoice.GrandTotal;
                        lastInvoiceid = _iinvoicerepository.InsertInvoiceDetails(INVModelObj.invoice, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                        for (int i = 0; i < INVModelObj.listinvoiceitems.Count; i++)
                        {
                            if (INVModelObj.listinvoiceitems[i].ItemName != null && INVModelObj.listinvoiceitems[i].ItemName != "DeletedRow")
                            {
                                INVModelObj.listinvoiceitems[i].Invoiceno = INVModelObj.invoice.InvoiceNo;
                                INVModelObj.listinvoiceitems[i].Invoiceid = lastInvoiceid;
                                bool res = _iinvoicerepository.InsertInvoiceitems(INVModelObj.listinvoiceitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }
                        }
                    }

                }
                else if (SaveInV != null)
                {
                    TempData["CreateInvoice"] = "Createinvoice";
                    if (INVModelObj.listinvoiceitems.Count != 0)
                    {
                        int lastInvoiceid = 0;
                        INVModelObj.invoice.Posted = 0;
                        INVModelObj.invoice.BalanceAmount = INVModelObj.invoice.GrandTotal;
                        lastInvoiceid = _iinvoicerepository.InsertInvoiceDetails(INVModelObj.invoice, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                        for (int i = 0; i < INVModelObj.listinvoiceitems.Count; i++)
                        {
                            if (INVModelObj.listinvoiceitems[i].ItemName != null && INVModelObj.listinvoiceitems[i].ItemName != "DeletedRow")
                            {
                                INVModelObj.listinvoiceitems[i].Invoiceno = INVModelObj.invoice.InvoiceNo;
                                INVModelObj.listinvoiceitems[i].Invoiceid = lastInvoiceid;
                                bool res = _iinvoicerepository.InsertInvoiceitems(INVModelObj.listinvoiceitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }
                        }
                    }

                }
                if (INVModelObj.invoice.EstInvoiceID != null)
                {
                    int estinviid = Convert.ToInt32(INVModelObj.invoice.EstInvoiceID);
                    bool isposted = _iinvoicerepository.UpdateEstimateInvtoInvoicebyEstid(estinviid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                }
                #region misc
                //}
                //else
                //{
                //    if (SavePostINV != null)
                //    {
                //        if (INVModelObj.listinvoiceitems.Count != 0)
                //        {
                //            int lastInvoiceid = 0;
                //            INVModelObj.invoice.Posted = 1;
                //             lastInvoiceid = _iinvoicerepository.InsertInvoiceDetails(INVModelObj.invoice, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                //            for (int i = 0; i < INVModelObj.listinvoiceitems.Count; i++)
                //            {
                //                if (INVModelObj.listinvoiceitems[i].ItemName != null && INVModelObj.listinvoiceitems[i].ItemName != "DeletedRow")
                //                {
                //                    INVModelObj.listinvoiceitems[i].Invoiceno = INVModelObj.invoice.InvoiceNo;
                //                    INVModelObj.listinvoiceitems[i].Invoiceid = lastInvoiceid;
                //                    bool res = _iinvoicerepository.InsertInvoiceitems(INVModelObj.listinvoiceitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //                }
                //            }
                //        }
                //    }
                //    else if (SaveInV != null)
                //    {
                //        if (INVModelObj.listinvoiceitems.Count != 0)
                //        {
                //            int lastInvoiceid = 0;
                //            INVModelObj.invoice.Posted = 0;
                //            lastInvoiceid = _iinvoicerepository.InsertInvoiceDetails(INVModelObj.invoice, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                //            for (int i = 0; i < INVModelObj.listinvoiceitems.Count; i++)
                //            {
                //                if (INVModelObj.listinvoiceitems[i].ItemName != null && INVModelObj.listinvoiceitems[i].ItemName != "DeletedRow")
                //                {
                //                    INVModelObj.listinvoiceitems[i].Invoiceno = INVModelObj.invoice.InvoiceNo;
                //                    INVModelObj.listinvoiceitems[i].Invoiceid = lastInvoiceid;
                //                    bool res = _iinvoicerepository.InsertInvoiceitems(INVModelObj.listinvoiceitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //                }
                //            }
                //        }
                //    }

                //}

                //if (INVModelObj.listinvoiceitems.Count != 0)
                //{
                //    int lastInvoiceid = 0;
                //    INVModelObj.invoice.Posted = 0;
                //    lastInvoiceid = _iinvoicerepository.InsertInvoiceDetails(INVModelObj.invoice, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                //    for (int i = 0; i < INVModelObj.listinvoiceitems.Count; i++)
                //    {
                //        if (INVModelObj.listinvoiceitems[i].ItemName != null && INVModelObj.listinvoiceitems[i].ItemName != "DeletedRow")
                //        {
                //            INVModelObj.listinvoiceitems[i].Invoiceno = INVModelObj.invoice.InvoiceNo;
                //            INVModelObj.listinvoiceitems[i].Invoiceid = lastInvoiceid;
                //            bool res = _iinvoicerepository.InsertInvoiceitems(INVModelObj.listinvoiceitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //        }
                //    }
                //}
                # endregion
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }

        }


        // Updating the Invoice
        [HttpPost]
        public ActionResult UpdateInvoice(InvoiceModel INVModelObj)
        {
            try
            {
                TempData["Updateinvoice"] = "Updateinvoice";
                if (INVModelObj.listinvoiceitems.Count != 0)
                {
                    // Update the Main invoice table
                    INVModelObj.invoice.BalanceAmount = INVModelObj.invoice.GrandTotal;
                    bool res = _iinvoicerepository.UpdateInvoicebyInvoiceId(INVModelObj.invoice, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    for (int i = 0; i < INVModelObj.listinvoiceitems.Count; i++)
                    {
                        INVModelObj.listinvoiceitems[i].Invoiceid = INVModelObj.invoice.InvoiceID;
                        INVModelObj.listinvoiceitems[i].Invoiceno = INVModelObj.invoice.InvoiceNo;

                        if (INVModelObj.listinvoiceitems[i].InvoiceItemId != 0)
                        {
                            if (INVModelObj.listinvoiceitems[i].ItemName != null && INVModelObj.listinvoiceitems[i].ItemName != "DeletedRow")
                            {
                                // Update the invoiceitem to tbl_invoiceitems with invoiceitemId.
                                bool ItemUpdate = _iinvoicerepository.UpdateInvoiceItemsbyInvoiceItemId(INVModelObj.listinvoiceitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }
                            else
                            {
                                // Delete the Item with InvoiceItemId
                               bool itemdel = _iinvoicerepository.Deleteitemsbyitemid(INVModelObj.listinvoiceitems[i].InvoiceItemId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }

                        }

                        else
                        {
                            if (INVModelObj.listinvoiceitems[i].ItemName != null && INVModelObj.listinvoiceitems[i].ItemName != "DeletedRow" && INVModelObj.listinvoiceitems[i].ItemName != " ")
                            {
                                // insert the new Invoiceitem to the tbl_invoiceitems
                                bool resIns = _iinvoicerepository.InsertInvoiceitems(INVModelObj.listinvoiceitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public bool PostInvoice(string InvoiceID)
        {
            try
            {
                int Invid = Convert.ToInt32(InvoiceID);
                bool res = _iinvoicerepository.ConvertInvoicetoPost(Invid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpGet]
        public ActionResult EditInvoice(int InvoiceId)
        {
            try
            {

                InvoiceModel INVmodelObj = new InvoiceModel();
                INVmodelObj.invoice = _iinvoicerepository.GetInvoiceDetailsbyINVId(InvoiceId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                //return View(objBranch);
                return View(INVmodelObj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [ActionName("_BindEditItemslist")]
        public ActionResult _BindEditItemslist(string InvoiceID)
        {

            try
            {
                int Invoiceid = Convert.ToInt32(InvoiceID);
                InvoiceModel InvoBJ = new InvoiceModel();
                InvoBJ.InvoiceitemObj = new tbl_InvoiceItem();
                InvoBJ.itemsobj = new tbl_Items();


                tbl_InvoiceItem AddInvoiceitemsRow = new tbl_InvoiceItem();
                AddInvoiceitemsRow.ItemTotal = null;
                AddInvoiceitemsRow.ItemName = "";
                AddInvoiceitemsRow.ItemCode = "";
                AddInvoiceitemsRow.ItemId = 0;
                AddInvoiceitemsRow.InvoiceItemId = 0;
                AddInvoiceitemsRow.Invoiceno = "";
                InvoBJ.listinvoiceitems = new List<tbl_InvoiceItem>();
                InvoBJ.listinvoiceitems.Add(AddInvoiceitemsRow);
                InvoBJ.listinvoiceitems.AddRange(_iinvoicerepository.GetAllInvoiceitemsList(Invoiceid, GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                // InvoBJ.listinvoiceitems = _iinvoicerepository.GetAllInvoiceitemsList(Invoiceid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 1; i < InvoBJ.listinvoiceitems.Count; i++)
                {
                    tbl_Items itemsobj = _iinvoicerepository.GetItemdetailsbyId(Convert.ToInt32(InvoBJ.listinvoiceitems[i].ItemId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    InvoBJ.listinvoiceitems[i].ItemCode = itemsobj.ItemCode;
                    InvoBJ.listinvoiceitems[i].ItemName = itemsobj.ItemName;
                    InvoBJ.listinvoiceitems[i].ItemId = itemsobj.ItemID;
                }

                tbl_Items selectItemsOption = new tbl_Items();
                selectItemsOption.ItemName = "Select";
                selectItemsOption.ItemID = 0;
                InvoBJ.itemslist = new List<tbl_Items>();
                InvoBJ.itemslist.Add(selectItemsOption);
                InvoBJ.itemslist.AddRange(_iinvoicerepository.GetAllitemsList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                return PartialView(InvoBJ);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [ActionName("_BindPostedItemslist")]
        public ActionResult _BindPostedItemslist(string InvoiceID)
        {

            try
            {
                int Invoiceid = Convert.ToInt32(InvoiceID);
                InvoiceModel InvoBJ = new InvoiceModel();
                InvoBJ.InvoiceitemObj = new tbl_InvoiceItem();
                InvoBJ.itemsobj = new tbl_Items();


                tbl_InvoiceItem AddInvoiceitemsRow = new tbl_InvoiceItem();
                AddInvoiceitemsRow.ItemTotal = null;
                AddInvoiceitemsRow.ItemName = "";
                AddInvoiceitemsRow.ItemCode = "";
                AddInvoiceitemsRow.ItemId = 0;
                AddInvoiceitemsRow.InvoiceItemId = 0;
                AddInvoiceitemsRow.Invoiceno = "";
                InvoBJ.listinvoiceitems = new List<tbl_InvoiceItem>();
                InvoBJ.listinvoiceitems.Add(AddInvoiceitemsRow);
                InvoBJ.listinvoiceitems.AddRange(_iinvoicerepository.GetAllInvoiceitemsList(Invoiceid, GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                // InvoBJ.listinvoiceitems = _iinvoicerepository.GetAllInvoiceitemsList(Invoiceid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 1; i < InvoBJ.listinvoiceitems.Count; i++)
                {
                    tbl_Items itemsobj = _iinvoicerepository.GetItemdetailsbyId(Convert.ToInt32(InvoBJ.listinvoiceitems[i].ItemId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    InvoBJ.listinvoiceitems[i].ItemCode = itemsobj.ItemCode;
                    InvoBJ.listinvoiceitems[i].ItemName = itemsobj.ItemName;
                    InvoBJ.listinvoiceitems[i].ItemId = itemsobj.ItemID;
                }

                tbl_Items selectItemsOption = new tbl_Items();
                selectItemsOption.ItemName = "Select";
                selectItemsOption.ItemID = 0;
                InvoBJ.itemslist = new List<tbl_Items>();
                InvoBJ.itemslist.Add(selectItemsOption);
                InvoBJ.itemslist.AddRange(_iinvoicerepository.GetAllitemsList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                return PartialView(InvoBJ);
            }
            catch (Exception)
            {
                throw;
            }
        }




        [HttpGet]
        public ActionResult GetCustomerDetails(string ContactId)
        {
            try
            {
                Company ObjCompany = _iinvoicerepository.GetCustomerAddressbyId(Convert.ToInt32(ContactId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                ObjCompany.MailingFullAddress = ObjCompany.MailingAddress;
                ObjCompany.BillingFullAddress = ObjCompany.BillingAddress;

               // tbl_Contact ObjContact = _iinvoicerepository.GetCustomerAddressbyId(Convert.ToInt32(ContactId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
               // ObjContact.MailingFullAddress = ObjContact.MailingAddress + ObjContact.Mailingstreet + ObjContact.Mailingcity + ObjContact.Mailingstate + ObjContact.Mailingcountry + ObjContact.Mailingzip;
               // ObjContact.BillingFullAddress = ObjContact.BillingAddress + ObjContact.Otherstreet + ObjContact.Othercity + ObjContact.Otherstate + ObjContact.Othercountry + ObjContact.Otherzip;
                return Json(ObjCompany, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public bool DeleteEachInvoice(string invoiceid)
        {
            try
            {
                bool Rval = false;

                if (!string.IsNullOrEmpty(invoiceid))
                {
                    foreach (var invid in invoiceid.Split(','))
                    {
                        bool res = _iinvoicerepository.DeleteInvoicebyID(Convert.ToInt32(invid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        if (res == true)
                        {
                            bool itemres = _iinvoicerepository.DeleteInvoiceitemsbyInvID(Convert.ToInt32(invid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            Rval = true;
                        }
                    }

                }

                return Rval;
            }
            catch (Exception)
            {
                throw;
            }
        }




        public ActionResult _GetEstimateInvoice()
        {
            try
            {
                //InvoiceModel InvoBJ = new InvoiceModel();
                //InvoBJ.listContact = new List<tbl_Contact>();
                //InvoBJ.itemslist = new List<tbl_Items>();
                //InvoBJ.itemsobj = new tbl_Items();
                //InvoBJ.invoice = new tbl_Invoice();
                //return PartialView(InvoBJ);
                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }

        }


        //getEstimateinvoicelistforInvoice
        public ActionResult getEstimateinvoicelistforInvoice(jQueryDataTableParamModel param)
        {
            try
            {
                //string ContactBranch = Request["ContactBranch"];
                //string ContactType = Request["ContactType"];
                //string ContactSalesRepresentative = Request["ContactSalesRepresentative"];
                //string ContactFunnelSuspects = Request["ContactFunnelSuspects"];
                //string ContactSkillSerachOption = Request["ContactSkillSerachOption"];
                //string ContactSkillId = Request["ContactSkillId"];
                //string ContactFilterString = Request["ContactFilterString"];
                //string Contactkeyword = Request["Contactkeyword"];
                //string iSortCol_0 = Request["iSortCol_0"];
                //string sSortDir_0 = Request["sSortDir_0"];
                //string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                //string orderByClause = string.Empty;
                //string specifiedZip = Request["specifiedZip"];
                //string Sector = Request["Sector"];
                //decimal dist = 5;
                //int totalPageCount = 0;
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "EstInvoiceID desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                var EstimateInvoicelst = _iinvoicerepository.GetAllEstimateInvoicerList(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //var EstimateInvoicelst = _iinvoicerepository.GetAllCustomerList(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = EstimateInvoicelst,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //GetCustomerAddressDetailsbyId

        # endregion

        #region Estimate Invoice

        public ActionResult EstimateInvoice()
        {
            if (TempData["CreateEstimate"] != null)
            {
                ViewBag.Estimate = "CreateEstimate";

            }
            if(TempData["UpdateEstimate"]!= null)
            {
                ViewBag.update = "UpdateEstimate";
            }

            if(TempData["UpdatePost"]!= null)
            {
                ViewBag.updatePost = "UpdatePost";
            }
            //TempData["UpdatePost"]
            return View();
        }




        public ActionResult getEstInvoicelist(jQueryDataTableParamModel param)
        {
            try
            {
                //string Branchkeyword = Request["b_keyword"];
                //string BranchName = Request["b_Name"];
                //string BranchZipcode = Request["b_Zip"];
                //string BranchCity = Request["b_City"];
                //string BranchState = Request["b_state"];
                //string BranchPhone = Request["b_Phone"];
                //string BranchEmail = Request["b_email"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "EstInvoiceID desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                var listEstinvoice = _iinvoicerepository.GetAllEstimateInvoiceList(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //EstInvStatus
                for (int i = 0; i < listEstinvoice.Count; i++)
                {
                    if (listEstinvoice[i].Posted == 0)
                    {
                        listEstinvoice[i].EstInvStatus = "Estimate";
                    }
                    else if (listEstinvoice[i].Posted == 1)
                    {
                        listEstinvoice[i].EstInvStatus = "Invoiced";
                    }
                    //else listEstinvoice[i].EstInvStatus = " -----";
                }
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listEstinvoice,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //
        public ActionResult CreateEstimateInvoice()
        {
            try
            {
                EstimateInvoiceModel EstINVModel = new EstimateInvoiceModel();
                EstINVModel.itemslist = new List<tbl_Items>();
                EstINVModel.itemsobj = new tbl_Items();
                // EstINVModel.estinvitemsobj = new tbl_EstimateInvoiceItem();
                EstINVModel.estinvoiceobj = new tbl_EstimateInvoice();


                //InvoiceModel InvoBJ = new InvoiceModel();
                //// Here get the List of Contacts from Contacts table where Contact Type is Customer
                //InvoBJ.itemslist = new List<tbl_Items>();
                //InvoBJ.itemsobj = new tbl_Items();
                //InvoBJ.invoice = new tbl_Invoice();

                int SlastEstInvoiceno = _iinvoicerepository.GetlastEstimateInvoicenumber(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                string LastEstinvoicenumber = "";
                SlastEstInvoiceno = SlastEstInvoiceno + 1;
                LastEstinvoicenumber = string.Format("{0:000000}", SlastEstInvoiceno);


                EstINVModel.estinvoiceobj.EstInvoiceNo = "EST-" + LastEstinvoicenumber;
                DateTime dtSpecifiedDate = DateTime.Now.Date;
                DateTime currDate = DateTime.Now;
                EstINVModel.estinvoiceobj.CreatedDate = currDate;


                return View(EstINVModel);
            }
            catch (Exception)
            {
                throw;
            }


        }



        // Inserting the Invoice Post Method
        [HttpPost]
        public ActionResult SaveEstimateInvoice(EstimateInvoiceModel EstINVModelObj, string ConvertInvoice, string SaveEI)
        {
            try
            {
                TempData["CreateEstimate"] = "CreateEstimate";
                if (ConvertInvoice != null)
                {                    
                    var EstimateList = EstINVModelObj.ListEstimateitems.ToList();                 
                }
                else if (SaveEI != null)
                {
                    if (EstINVModelObj.ListEstimateitems.Count != 0)
                    {
                        int lastEstInvoiceid = 0;
                        EstINVModelObj.estinvoiceobj.Posted = 0;
                        lastEstInvoiceid = _iinvoicerepository.InsertEstimateInvoiceDetails(EstINVModelObj.estinvoiceobj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


                        for (int i = 0; i < EstINVModelObj.ListEstimateitems.Count; i++)
                        {
                            if (EstINVModelObj.ListEstimateitems[i].ItemName != null && EstINVModelObj.ListEstimateitems[i].ItemName != "DeletedRow")
                            {
                                EstINVModelObj.ListEstimateitems[i].EstInvoiceID = lastEstInvoiceid;
                                EstINVModelObj.ListEstimateitems[i].EstInvoiceNo = EstINVModelObj.estinvoiceobj.EstInvoiceNo;
                                bool res = _iinvoicerepository.InsertEstimateInvoiceitems(EstINVModelObj.ListEstimateitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }
                        }
                    }

                    var EstimateList = EstINVModelObj.listinvoiceitems.ToList();

                }

                return RedirectToAction("EstimateInvoice");
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpGet]
        [ActionName("_BindEstimateItemslist")]
        public ActionResult _BindEstimateItemslist()
        {

            try
            {
                EstimateInvoiceModel InvoBJ = new EstimateInvoiceModel();
                tbl_Items selectItemsOption = new tbl_Items();
                selectItemsOption.ItemName = "Select";
                selectItemsOption.ItemID = 0;
                InvoBJ.itemslist = new List<tbl_Items>();
                InvoBJ.itemslist.Add(selectItemsOption);
                InvoBJ.itemslist.AddRange(_iinvoicerepository.GetAllitemsList(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                return PartialView(InvoBJ);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public JsonResult GetEstimateInvoiceData(string EstInvoiceId)
        {
            try
            {
                int Estinvid = Convert.ToInt32(EstInvoiceId);
                //string result = "";
                var result = _iinvoicerepository.GetEstimateInvoiceDetailsbyId(Estinvid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //return Objitemdetails;
                //var result = "";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }




        [HttpGet]
        public ActionResult EditEstimateInvoice(int EstInvoiceId)
        {
            try
            {

                EstimateInvoiceModel EstINVModel = new EstimateInvoiceModel();
                EstINVModel.estinvoiceobj = _iinvoicerepository.GetEstimateInvoiceDetailsbyId(EstInvoiceId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //INVmodelObj.invoice = _iinvoicerepository.GetInvoiceDetailsbyINVId(InvoiceId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                //return View(objBranch);
                return View(EstINVModel);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [ActionName("_BindEditEstimateItemslist")]
        public ActionResult _BindEditEstimateItemslist(string InvoiceID)
        {

            try
            {
                int EstInvoiceid = Convert.ToInt32(InvoiceID);
                EstimateInvoiceModel EstINVModel = new EstimateInvoiceModel();

                tbl_EstimateInvoiceItem AddEstimateitemRow = new tbl_EstimateInvoiceItem();
                AddEstimateitemRow.ItemTotal = null;
                AddEstimateitemRow.ItemName = "";
                AddEstimateitemRow.ItemCode = "";
                AddEstimateitemRow.ItemId = 0;
                AddEstimateitemRow.EstimateInvoiceItemId = 0;
                EstINVModel.ListEstimateitems = new List<tbl_EstimateInvoiceItem>();
                EstINVModel.ListEstimateitems.Add(AddEstimateitemRow);
                EstINVModel.ListEstimateitems.AddRange(_iinvoicerepository.GetAllEstimateInvoiceitemsList(EstInvoiceid, GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                for (int i = 1; i < EstINVModel.ListEstimateitems.Count; i++)
                {
                    tbl_Items itemsobj = _iinvoicerepository.GetItemdetailsbyId(Convert.ToInt32(EstINVModel.ListEstimateitems[i].ItemId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    EstINVModel.ListEstimateitems[i].ItemCode = itemsobj.ItemCode;
                    EstINVModel.ListEstimateitems[i].ItemName = itemsobj.ItemName;
                    EstINVModel.ListEstimateitems[i].ItemId = itemsobj.ItemID;
                }

                return PartialView(EstINVModel);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [ActionName("_BindConvertedEstimateItemslist")]
        public ActionResult _BindConvertedEstimateItemslist(string InvoiceID)
        {

            try
            {
                int EstInvoiceid = Convert.ToInt32(InvoiceID);
                EstimateInvoiceModel EstINVModel = new EstimateInvoiceModel();

                tbl_EstimateInvoiceItem AddEstimateitemRow = new tbl_EstimateInvoiceItem();
                AddEstimateitemRow.ItemTotal = null;
                AddEstimateitemRow.ItemName = "";
                AddEstimateitemRow.ItemCode = "";
                AddEstimateitemRow.ItemId = 0;
                AddEstimateitemRow.EstimateInvoiceItemId = 0;
                EstINVModel.ListEstimateitems = new List<tbl_EstimateInvoiceItem>();
                EstINVModel.ListEstimateitems.Add(AddEstimateitemRow);
                EstINVModel.ListEstimateitems.AddRange(_iinvoicerepository.GetAllEstimateInvoiceitemsList(EstInvoiceid, GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                for (int i = 1; i < EstINVModel.ListEstimateitems.Count; i++)
                {
                    tbl_Items itemsobj = _iinvoicerepository.GetItemdetailsbyId(Convert.ToInt32(EstINVModel.ListEstimateitems[i].ItemId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    EstINVModel.ListEstimateitems[i].ItemCode = itemsobj.ItemCode;
                    EstINVModel.ListEstimateitems[i].ItemName = itemsobj.ItemName;
                    EstINVModel.ListEstimateitems[i].ItemId = itemsobj.ItemID;
                }

                return PartialView(EstINVModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //UpdateEstimateInvoice
        // Updating the Invoice
        [HttpPost]
        public ActionResult UpdateEstimateInvoice(EstimateInvoiceModel EstINVModelObj, string ConvertInvoice, string SaveEI)
        {
            try
            {
               // TempData["UpdateEstimate"] = "UpdateEstimate";
                // Converting the Estimate invoice to Invoice.
                if (ConvertInvoice != null)
                {
                    TempData["UpdatePost"] = "UpdatePost";
                    int lastInvoiceid = 0;
                    var EstiInvObj = EstINVModelObj.estinvoiceobj;

                    EstINVModelObj.invoice = new tbl_Invoice
                    {
                        Active = EstiInvObj.Active,
                        CompanyId = EstiInvObj.CompanyId,
                        Companyname = EstiInvObj.Companyname,
                        CreateByUserID = EstiInvObj.CreateByUserID,
                        CreatedBy = EstiInvObj.CreatedBy,
                        CreatedDate = EstiInvObj.CreatedDate,
                        CustomerID = EstiInvObj.CustomerID,
                        Description = EstiInvObj.Description,
                        Discount = EstiInvObj.Discount,
                        DiscountPercent = EstiInvObj.DiscountPercent,
                        EquipmentAmount = EstiInvObj.EquipmentAmount,
                        EstInvoiceID = EstiInvObj.EstInvoiceID,
                        EstInvoiceNo = EstiInvObj.EstInvoiceNo,
                        Fname = EstiInvObj.Fname,
                        GrandTotal = EstiInvObj.GrandTotal,
                        invCretaedDate = EstiInvObj.invCretaedDate,
                        //InvoiceAmount = EstiInvObj.EstInvoiceAmount,
                        IsCreditMemo = EstiInvObj.IsCreditMemo,
                        IsDrafted = EstiInvObj.IsDrafted,
                        isPaid = EstiInvObj.isPaid,
                        JobLocation = EstiInvObj.JobLocation,
                        JobPhone = EstiInvObj.JobPhone,
                        LaborAmount = EstiInvObj.LaborAmount,
                        LateFee = EstiInvObj.LateFee,
                        MaterialAmount = EstiInvObj.MaterialAmount,
                        Memo = EstiInvObj.Memo,
                        MiscellaneousAmount = EstiInvObj.MiscellaneousAmount,
                        ModifiedBy = EstiInvObj.ModifiedBy,
                        ModifiedDate = EstiInvObj.ModifiedDate,
                        OrderNo = EstiInvObj.OrderNo,
                        Posted = EstiInvObj.Posted,
                        SoldTo = EstiInvObj.SoldTo,
                        TaxAmount = EstiInvObj.TaxAmount,
                        TaxPercent = EstiInvObj.TaxPercent,
                        Total = EstiInvObj.Total,
                        InvoiceAmount = EstiInvObj.Total,
                        BalanceAmount = EstiInvObj.GrandTotal

                    };
                    // Here assign Invoiceno and Due Date to default 30 days from the created Date.
                    int SlastInvoiceno = _iinvoicerepository.SetlastInvoicenumber(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    string Lastinvoicenumber = "";
                    SlastInvoiceno = SlastInvoiceno + 1;
                    Lastinvoicenumber = string.Format("{0:000000}", SlastInvoiceno);
                    EstINVModelObj.invoice.InvoiceNo = "INV-" + Lastinvoicenumber;


                    var dtSpecifiedDate = Convert.ToDateTime(EstINVModelObj.invoice.CreatedDate);
                    DateTime dt = Convert.ToDateTime(dtSpecifiedDate);

                    DateTime DueToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(30).ToString("MM/dd/yyyy"));
                    EstINVModelObj.invoice.DueDate = DueToDate;
                    //InvoBJ.invoice.DueDate = DueToDate;


                    var EstimateList = EstINVModelObj.ListEstimateitems.ToList();
                    var EstimateItemmodel = (from p in EstimateList.AsEnumerable()
                                             select new tbl_InvoiceItem
                                             {
                                                 ItemCode = p.ItemCode,
                                                 ItemId = p.ItemId,
                                                 ItemName = p.ItemName,
                                                 ItemTotal = p.ItemTotal,
                                                 Quantity = p.Quantity,
                                                 RatePerUnit = p.RatePerUnit

                                             }).ToList();

                    EstINVModelObj.listinvoiceitems = EstimateItemmodel;


                    if (EstINVModelObj.listinvoiceitems.Count != 0)
                    {
                        EstINVModelObj.invoice.Posted = 0; // 
                        EstINVModelObj.invoice.InvoiceType = "Estimate - Invoice";
                        lastInvoiceid = _iinvoicerepository.InsertInvoiceDetails(EstINVModelObj.invoice, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                        for (int i = 0; i < EstINVModelObj.listinvoiceitems.Count; i++)
                        {
                            if (EstINVModelObj.listinvoiceitems[i].ItemName != null && EstINVModelObj.listinvoiceitems[i].ItemName != " " && EstINVModelObj.listinvoiceitems[i].ItemName != "DeletedRow")
                            {
                                EstINVModelObj.listinvoiceitems[i].Invoiceno = EstINVModelObj.invoice.InvoiceNo;
                                EstINVModelObj.listinvoiceitems[i].Invoiceid = lastInvoiceid;
                                bool res = _iinvoicerepository.InsertInvoiceitems(EstINVModelObj.listinvoiceitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            }
                        }
                    }

                    // Update the Estimate invoice if the items are add or deleted
                    {
                        // Update the Main Estimate invoice table
                        EstINVModelObj.estinvoiceobj.Posted = 1;
                        bool res = _iinvoicerepository.UpdateEstimateInvoicebyEstInvoiceId(EstINVModelObj.estinvoiceobj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                       
                        for (int i = 0; i < EstINVModelObj.ListEstimateitems.Count; i++)
                        {
                            EstINVModelObj.ListEstimateitems[i].EstInvoiceID = EstINVModelObj.estinvoiceobj.EstInvoiceID;
                            EstINVModelObj.ListEstimateitems[i].EstInvoiceNo = EstINVModelObj.estinvoiceobj.EstInvoiceNo;

                            if (EstINVModelObj.ListEstimateitems[i].EstimateInvoiceItemId != 0)
                            {
                                if (EstINVModelObj.ListEstimateitems[i].ItemName != null && EstINVModelObj.ListEstimateitems[i].ItemName != "DeletedRow")
                                {
                                    // Update the Estimate invoiceitem to tbl_Estimateinvoiceitems with EstinvoiceitemId.
                                    bool ItemUpdate = _iinvoicerepository.UpdateEstiInvoiceItemsbyEstInvoiceItemId(EstINVModelObj.ListEstimateitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                                   
                                }
                                else
                                {
                                    // Delete the Item with EstInvoiceItemId
                                    bool itemdel = _iinvoicerepository.DeleteEstinvitemsbyestinvitemid(EstINVModelObj.ListEstimateitems[i].EstimateInvoiceItemId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);                                   
                                }

                            }

                            else
                            {
                                if (EstINVModelObj.ListEstimateitems[i].ItemName != null && EstINVModelObj.ListEstimateitems[i].ItemName != "DeletedRow" && EstINVModelObj.ListEstimateitems[i].ItemName != " ")
                                {
                                    // insert the new EstInvoiceitem to the tbl_Estimateinvoiceitems
                                    bool resins = _iinvoicerepository.InsertEstimateInvoiceitems(EstINVModelObj.ListEstimateitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                                   
                                }
                            }
                        }
                    }

                }
                // Updating the Estimate invoice.
                else if (SaveEI != null)
                {
                    TempData["UpdateEstimate"] = "UpdateEstimate";
                    if (EstINVModelObj.ListEstimateitems.Count != 0)
                    {
                        // Update the Main Estimate invoice table
                        EstINVModelObj.estinvoiceobj.Posted = 0;
                        EstINVModelObj.estinvoiceobj.Total = EstINVModelObj.estinvoiceobj.GrandTotal;
                        bool res = _iinvoicerepository.UpdateEstimateInvoicebyEstInvoiceId(EstINVModelObj.estinvoiceobj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                        for (int i = 0; i < EstINVModelObj.ListEstimateitems.Count; i++)
                        {
                            EstINVModelObj.ListEstimateitems[i].EstInvoiceID = EstINVModelObj.estinvoiceobj.EstInvoiceID;
                            EstINVModelObj.ListEstimateitems[i].EstInvoiceNo = EstINVModelObj.estinvoiceobj.EstInvoiceNo;

                            if (EstINVModelObj.ListEstimateitems[i].EstimateInvoiceItemId != 0)
                            {
                                if (EstINVModelObj.ListEstimateitems[i].ItemName != null && EstINVModelObj.ListEstimateitems[i].ItemName != "DeletedRow")
                                {
                                    // Update the Estimate invoiceitem to tbl_Estimateinvoiceitems with EstinvoiceitemId.
                                    bool ItemUpdate = _iinvoicerepository.UpdateEstiInvoiceItemsbyEstInvoiceItemId(EstINVModelObj.ListEstimateitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                                   
                                }
                                else
                                {
                                    // Delete the Item with EstInvoiceItemId
                                    bool itemdel = _iinvoicerepository.DeleteEstinvitemsbyestinvitemid(EstINVModelObj.ListEstimateitems[i].EstimateInvoiceItemId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                                    
                                }

                            }

                            else
                            {
                                if (EstINVModelObj.ListEstimateitems[i].ItemName != null && EstINVModelObj.ListEstimateitems[i].ItemName != "DeletedRow" && EstINVModelObj.ListEstimateitems[i].ItemName != " ")
                                {
                                    // insert the new EstInvoiceitem to the tbl_Estimateinvoiceitems
                                    bool resins = _iinvoicerepository.InsertEstimateInvoiceitems(EstINVModelObj.ListEstimateitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);                                   
                                }
                            }
                        }
                    }


                }

                # region Misc
                //if (INVModelObj.listinvoiceitems.Count != 0)
                //{
                //    // Update the Main invoice table
                //    bool res = _iinvoicerepository.UpdateInvoicebyInvoiceId(INVModelObj.invoice, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //    for (int i = 0; i < INVModelObj.listinvoiceitems.Count; i++)
                //    {
                //        INVModelObj.listinvoiceitems[i].Invoiceid = INVModelObj.invoice.InvoiceID;
                //        INVModelObj.listinvoiceitems[i].Invoiceno = INVModelObj.invoice.InvoiceNo;

                //        if (INVModelObj.listinvoiceitems[i].InvoiceItemId != 0)
                //        {
                //            if (INVModelObj.listinvoiceitems[i].ItemName != null && INVModelObj.listinvoiceitems[i].ItemName != "DeletedRow")
                //            {
                //                // Update the invoiceitem to tbl_invoiceitems with invoiceitemId.
                //                bool ItemUpdate = _iinvoicerepository.UpdateInvoiceItemsbyInvoiceItemId(INVModelObj.listinvoiceitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //            }
                //            else
                //            {
                //                // Delete the Item with InvoiceItemId
                //                bool itemdel = _iinvoicerepository.Deleteitemsbyitemid(INVModelObj.listinvoiceitems[i].InvoiceItemId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //            }

                //        }

                //        else
                //        {
                //            if (INVModelObj.listinvoiceitems[i].ItemName != null && INVModelObj.listinvoiceitems[i].ItemName != "DeletedRow" && INVModelObj.listinvoiceitems[i].ItemName != " ")
                //            {
                //                // insert the new Invoiceitem to the tbl_invoiceitems
                //                bool resIns = _iinvoicerepository.InsertInvoiceitems(INVModelObj.listinvoiceitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //            }
                //        }
                //    }
                //}
                #endregion
                return RedirectToAction("EstimateInvoice");
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpPost]
        public bool DeleteEachEstimateInvoice(string invoiceid)
        {
            try
            {
                bool Rval = false;
                if (!string.IsNullOrEmpty(invoiceid))
                {
                    foreach (var itemid in invoiceid.Split(','))
                    {
                        bool res = _iinvoicerepository.DeleteEstimateInvoicebyID(Convert.ToInt32(itemid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                        if (res == true)
                        {
                            bool itemres = _iinvoicerepository.DeleteEstimateInvoiceitemsbyInvID(Convert.ToInt32(itemid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                            Rval = true;
                        }
                        
                        
                    }
                }

                return Rval;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // ConvertEstimatetoInvoice

        [HttpGet]
        public bool ConvertEstimatetoInvoice(string Estinvoiceid)
        {
            try
            {
                int EstInvid = Convert.ToInt32(Estinvoiceid);
                # region getData
                EstimateInvoiceModel EstINVModel = new EstimateInvoiceModel();
                // get Data from tbl_EstimateInvoice table on Estimateinvoiceid.
                EstINVModel.estinvoiceobj = _iinvoicerepository.GetEstimateInvoiceDetailsbyId(EstInvid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                // get Data from tbl_EstimateInvoiceItemsTable  on Estimateinvoiceid.

                EstINVModel.ListEstimateitems = _iinvoicerepository.GetAllEstimateInvoiceitemsList(EstInvid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                for (int i = 0; i < EstINVModel.ListEstimateitems.Count; i++)
                {
                    tbl_Items itemsobj = _iinvoicerepository.GetItemdetailsbyId(Convert.ToInt32(EstINVModel.ListEstimateitems[i].ItemId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    EstINVModel.ListEstimateitems[i].ItemCode = itemsobj.ItemCode;
                    EstINVModel.ListEstimateitems[i].ItemName = itemsobj.ItemName;
                    EstINVModel.ListEstimateitems[i].ItemId = itemsobj.ItemID;
                }
                # endregion
                # region posting data
                int lastInvoiceid = 0;
                var EstiInvObj = EstINVModel.estinvoiceobj;

                EstINVModel.invoice = new tbl_Invoice
                {
                    Active = EstiInvObj.Active,
                    CompanyId = EstiInvObj.CompanyId,
                    Companyname = EstiInvObj.Companyname,
                    CreateByUserID = EstiInvObj.CreateByUserID,
                    CreatedBy = EstiInvObj.CreatedBy,
                    CreatedDate = EstiInvObj.CreatedDate,
                    CustomerID = EstiInvObj.CustomerID,
                    Description = EstiInvObj.Description,
                    Discount = EstiInvObj.Discount,
                    DiscountPercent = EstiInvObj.DiscountPercent,
                    EquipmentAmount = EstiInvObj.EquipmentAmount,
                    EstInvoiceID = EstiInvObj.EstInvoiceID,
                    EstInvoiceNo = EstiInvObj.EstInvoiceNo,
                    Fname = EstiInvObj.Fname,
                    GrandTotal = EstiInvObj.GrandTotal,
                    invCretaedDate = EstiInvObj.invCretaedDate,
                    //InvoiceAmount = EstiInvObj.EstInvoiceAmount,
                    IsCreditMemo = EstiInvObj.IsCreditMemo,
                    IsDrafted = EstiInvObj.IsDrafted,
                    isPaid = EstiInvObj.isPaid,
                    JobLocation = EstiInvObj.JobLocation,
                    JobPhone = EstiInvObj.JobPhone,
                    LaborAmount = EstiInvObj.LaborAmount,
                    LateFee = EstiInvObj.LateFee,
                    MaterialAmount = EstiInvObj.MaterialAmount,
                    Memo = EstiInvObj.Memo,
                    MiscellaneousAmount = EstiInvObj.MiscellaneousAmount,
                    ModifiedBy = EstiInvObj.ModifiedBy,
                    ModifiedDate = EstiInvObj.ModifiedDate,
                    OrderNo = EstiInvObj.OrderNo,
                    Posted = EstiInvObj.Posted,
                    SoldTo = EstiInvObj.SoldTo,
                    TaxAmount = EstiInvObj.TaxAmount,
                    TaxPercent = EstiInvObj.TaxPercent,
                    Total = EstiInvObj.Total,
                    InvoiceAmount = EstiInvObj.Total

                };
                // Here assign Invoiceno and Due Date to default 30 days from the created Date.
                int SlastInvoiceno = _iinvoicerepository.SetlastInvoicenumber(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                string Lastinvoicenumber = "";
                SlastInvoiceno = SlastInvoiceno + 1;
                Lastinvoicenumber = string.Format("{0:000000}", SlastInvoiceno);
                EstINVModel.invoice.InvoiceNo = "INV-" + Lastinvoicenumber;


                var dtSpecifiedDate = Convert.ToDateTime(EstINVModel.invoice.CreatedDate);
                DateTime dt = Convert.ToDateTime(dtSpecifiedDate);

                DateTime DueToDate = Convert.ToDateTime(dtSpecifiedDate.AddDays(30).ToString("MM/dd/yyyy"));
                EstINVModel.invoice.DueDate = DueToDate;
                //InvoBJ.invoice.DueDate = DueToDate;


                var EstimateList = EstINVModel.ListEstimateitems.ToList();
                var EstimateItemmodel = (from p in EstimateList.AsEnumerable()
                                         select new tbl_InvoiceItem
                                         {
                                             ItemCode = p.ItemCode,
                                             ItemId = p.ItemId,
                                             ItemName = p.ItemName,
                                             ItemTotal = p.ItemTotal,
                                             Quantity = p.Quantity,
                                             RatePerUnit = p.RatePerUnit

                                         }).ToList();

                EstINVModel.listinvoiceitems = EstimateItemmodel;


                if (EstINVModel.listinvoiceitems.Count != 0)
                {
                    EstINVModel.invoice.Posted = 0; // 
                    EstINVModel.invoice.InvoiceType = "Estimate - Invoice";
                    EstINVModel.invoice.BalanceAmount = EstINVModel.invoice.GrandTotal;
                    lastInvoiceid = _iinvoicerepository.InsertInvoiceDetails(EstINVModel.invoice, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                    for (int i = 0; i < EstINVModel.listinvoiceitems.Count; i++)
                    {
                        if (EstINVModel.listinvoiceitems[i].ItemName != null && EstINVModel.listinvoiceitems[i].ItemName != " " && EstINVModel.listinvoiceitems[i].ItemName != "DeletedRow")
                        {
                            EstINVModel.listinvoiceitems[i].Invoiceno = EstINVModel.invoice.InvoiceNo;
                            EstINVModel.listinvoiceitems[i].Invoiceid = lastInvoiceid;
                            bool res = _iinvoicerepository.InsertInvoiceitems(EstINVModel.listinvoiceitems[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                        }
                    }
                }

                // Update the Main Estimate invoice table
                EstINVModel.estinvoiceobj.Posted = 1;
                bool UpdateEstimateInvoice = _iinvoicerepository.UpdateEstimateInvoicebyEstInvoiceId(EstINVModel.estinvoiceobj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                # endregion


                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }







        #endregion



        #region Payments

        public ActionResult PaymentsIndex()
        {
            return View();
        }

        public ActionResult getAllInvoicelistforPayments(jQueryDataTableParamModel param)
        {
            try
            {
                string invoiceList = Request["b_invoice"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "InvoiceId desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                var listinvoice = _iinvoicerepository.GetAllInvoiceList(invoiceList, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int i = 0; i < listinvoice.Count; i++)
                {
                    int BalAmount = Convert.ToInt32(listinvoice[i].BalanceAmount);
                    int GrdTotal = Convert.ToInt32(listinvoice[i].GrandTotal);

                    if (BalAmount == GrdTotal)
                    {
                        listinvoice[i].Status = "Unpaid";
                    }
                    else if (BalAmount > 0 && BalAmount < GrdTotal)
                    {
                        listinvoice[i].Status = "Partially Paid";
                    }
                    else if (BalAmount == 0)
                    {
                        listinvoice[i].Status = "Paid";
                    }

                }


                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listinvoice,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }




        //
        public ActionResult Payments()
        {
            try
            {
                InvoiceModel InvoBJ = new InvoiceModel();

                return View();
            }
            catch (Exception)
            {
                throw;
            }


        }


        public ActionResult _GetCustomersListForPayments()
        {
            try
            {
                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }

        }


        //[HttpGet]
        //[ActionName("_BindInvoiceslistforCustomer")]
        //public ActionResult _BindInvoiceslistforCustomer(string CustomerId)
        //{

        //    try
        //    {
        //        InvoiceModel InvoBJ = new InvoiceModel();

        //        int customerID = Convert.ToInt32(CustomerId);
        //        //InvoBJ.invoice.CustomerID = customerID;

        //        // Get InvoiceList based on Customerid
        //        int totalPageCount = 0;
        //        InvoBJ.listinvoice = _iinvoicerepository.GetInvoiceListbyCustomerId(customerID, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        //for (int i = 0; i < InvoBJ.listinvoice.Count;i++)
        //        //{
        //        //    InvoBJ.listinvoice[i].AmountPaid = InvoBJ.listinvoice[i].BalanceAmount;
        //        //}

        //        return PartialView(InvoBJ);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        // MakePayment
        [HttpPost]
        public ActionResult MakePayment(InvoiceModel INVModelObj)
        {
            try
            {
                DateTime PaymentDate = DateTime.Now;
                var InvoiceList = INVModelObj.listinvoice.ToList();
                var Invoicemodel = (from p in InvoiceList.AsEnumerable()
                                    select new tbl_Payments
                                    {
                                        InvoiceID = p.InvoiceID,
                                        InvoiceAmount = p.GrandTotal,
                                        AmountPaid = p.AmountPaid,
                                        PaymentMode = p.Paymenttype,
                                        InvoiceBalance = p.BalanceAmount
                                    }).ToList();

                INVModelObj.Listpayments = Invoicemodel;
                for (int i = 0; i < INVModelObj.Listpayments.Count; i++)
                {
                    if (INVModelObj.Listpayments[i].AmountPaid != null)
                    {
                        INVModelObj.Listpayments[i].CustomerID = INVModelObj.invoice.CustomerID;
                        INVModelObj.Listpayments[i].CreatedDate = PaymentDate;
                        int BalAmount = 0;
                        int InvAmount = Convert.ToInt32(INVModelObj.Listpayments[i].InvoiceBalance);
                        int PaidAmount = Convert.ToInt32(INVModelObj.Listpayments[i].AmountPaid);
                        BalAmount = (Convert.ToInt32(INVModelObj.Listpayments[i].InvoiceBalance) - Convert.ToInt32(INVModelObj.Listpayments[i].AmountPaid));
                        INVModelObj.Listpayments[i].InvoiceBalance = BalAmount;
                        // Insert the Values Into the payment Table.
                       bool InsPaymentDetails = _iinvoicerepository.InsertPaymentDetails(INVModelObj.Listpayments[i], GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                        // Update the Invoice table
                        INVModelObj.listinvoice[i].BalanceAmount = BalAmount;
                        bool InvBalUpdate = _iinvoicerepository.UpdateInvoiceBalanceAmount(INVModelObj.listinvoice[i].InvoiceID, Convert.ToString(INVModelObj.listinvoice[i].BalanceAmount), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }

                }
                return RedirectToAction("PaymentsIndex");
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult _GetListofPaymentsByInvoiceId(string InvoiceId, string location)
        {
            try
            {
                int InvoiceID = Convert.ToInt32(InvoiceId);
                ViewBag.location = location;
                InvoiceModel INVModelObj = new InvoiceModel();
                INVModelObj.PaymentObj = new tbl_Payments();
                INVModelObj.PaymentObj.InvoiceID = InvoiceID;
                tbl_Invoice invObj = _iinvoicerepository.GetInvoiceDetailsbyINVId(Convert.ToInt32(InvoiceID), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                TempData["InvoiceNo"] = invObj.InvoiceNo;
                TempData["Customer"] = invObj.Fname;

                return PartialView(INVModelObj);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult getPaymentslistforInvoiceId(jQueryDataTableParamModel param)
        {
            string invoiceId = Request["InvoiceId"];
            string iSortCol_0 = Request["iSortCol_0"];
            string sSortDir_0 = Request["sSortDir_0"];
            string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
            string orderByClause = string.Empty;
            int totalPageCount = 0;
            if (iSortCol_0 == "0")
                orderByClause = "InvoiceId desc";
            else
                orderByClause = SortFieldName + " " + sSortDir_0;

            // tbl_Invoice invObj = _iinvoicerepository.GetInvoiceDetailsbyINVId(Convert.ToInt32(invoiceId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
            // TempData["InvoiceNo"] = invObj.InvoiceNo;
            // TempData["Customer"] = invObj.Fname;
            //// 
            //List<tbl_Payments> LstPayments = _iinvoicerepository.GetAllPaymentsbyinvoiceId(Convert.ToInt32(invoiceId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
            List<tbl_Payments> LstPayments = _iinvoicerepository.GetAllPaymentsbyinvoiceId(Convert.ToInt32(invoiceId), param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

            for (int i = 0; i < LstPayments.Count; i++)
            {
                if (LstPayments[i].PaymentMode == "1")
                {
                    LstPayments[i].PaymentType = "Cash";
                }
                else
                {
                    LstPayments[i].PaymentType = "Cheque";
                }
                //string payDate = Convert.ToDateTime(LstPayments[i].CreatedDate).ToShortDateString();
                ////LstPayments[i].PaidDate = payDate;
                //LstPayments[i].PaidDate = LstPayments[i].CreatedDate;
            }
            return Json(new
            {
                sEcho = param.sEcho,
                aaData = LstPayments,
                iTotalRecords = totalPageCount,
                iTotalDisplayRecords = totalPageCount
            }, JsonRequestBehavior.AllowGet);

        }
        //
        //
        public ActionResult _MakePaymentByInvoiceId(string InvoiceId)
        {
            try
            {
                int InvoiceID = Convert.ToInt32(InvoiceId);
                // ViewBag.location = location;
                InvoiceModel INVModelObj = new InvoiceModel();

                INVModelObj.PaymentObj = new tbl_Payments();
                INVModelObj.PaymentObj.InvoiceID = InvoiceID;
                INVModelObj.invoice = _iinvoicerepository.GetInvoiceDetailsbyINVId(Convert.ToInt32(InvoiceID), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //tbl_Invoice invObj = _iinvoicerepository.GetInvoiceDetailsbyINVId(Convert.ToInt32(InvoiceID), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                TempData["InvoiceNo"] = INVModelObj.invoice.InvoiceNo;
                TempData["Customer"] = INVModelObj.invoice.Fname;
                // INVModelObj.invoice.AmountPaid = INVModelObj.invoice.BalanceAmount;
                return PartialView(INVModelObj);
            }
            catch (Exception)
            {
                throw;
            }

        }



        // MakePayment from PayMentIndex Grid Button
        [HttpPost]
        public ActionResult MakeInvoicePayment(InvoiceModel INVModelObj)
        {
            try
            {
                TempData["MayPayment"] = "PayAmount";
                string editcnturlpath = "";
                if (Session["urcont"] != null)
                {
                    string Paymentulpth = this.Request.UrlReferrer.AbsoluteUri;
                    var Paycnturi = new Uri(Paymentulpth);
                    editcnturlpath = Paycnturi.ToString();
                }
                else
                {
                    string Paymentulpth = this.Request.UrlReferrer.AbsoluteUri;
                    var Paycnturi = new Uri(Paymentulpth);
                    editcnturlpath = Paycnturi.ToString();
                }


                DateTime PaymentDate = DateTime.Now;

                var InvObj = INVModelObj.invoice;

                INVModelObj.PaymentObj = new tbl_Payments
                       {
                           InvoiceID = InvObj.InvoiceID,
                           InvoiceAmount = InvObj.GrandTotal,
                           AmountPaid = InvObj.AmountPaid,
                           PaymentMode = InvObj.Paymenttype,
                           InvoiceBalance = InvObj.BalanceAmount,
                           CustomerID = InvObj.CustomerID

                       };

                INVModelObj.PaymentObj.CreatedDate = PaymentDate;

                int BalAmount = 0;
                int InvAmount = Convert.ToInt32(INVModelObj.PaymentObj.InvoiceBalance);
                int PaidAmount = Convert.ToInt32(INVModelObj.PaymentObj.AmountPaid);
                BalAmount = (Convert.ToInt32(INVModelObj.PaymentObj.InvoiceBalance) - Convert.ToInt32(INVModelObj.PaymentObj.AmountPaid));
                INVModelObj.PaymentObj.InvoiceBalance = BalAmount;
                // Insert the Values Into the payment Table.
                bool InsPaymentDetails = _iinvoicerepository.InsertPaymentDetails(INVModelObj.PaymentObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                // Update the Invoice table
                INVModelObj.invoice.BalanceAmount = BalAmount;
                bool InvBalUpdate = _iinvoicerepository.UpdateInvoiceBalanceAmount(INVModelObj.invoice.InvoiceID, Convert.ToString(INVModelObj.invoice.BalanceAmount), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Redirect(editcnturlpath);
                // return RedirectToAction("PaymentsIndex");



            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion
    
    
    // Code Converted for Zoho Model 
        /// To get the customer list from Company Table
        /// 
        public ActionResult _GetCustomersList()
        {
            try
            {

                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult getCustomerListforinvoice(jQueryDataTableParamModel param)
        {
            try
            {


                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "ID desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                int userId = -1;
                string Companykeyword = "";
                //var listcompany = _icommonrepository.GetListOfAllCompanies(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                var listcompany = _icompanyrepository.GetCompaniesIndex(Companykeyword, userId, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);




                //string iSortCol_0 = Request["iSortCol_0"];
                //string sSortDir_0 = Request["sSortDir_0"];
                //string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                //string orderByClause = string.Empty;
                //int totalPageCount = 0;
                //if (iSortCol_0 == "0")
                //    orderByClause = "ContactId desc";
                //else
                //    orderByClause = SortFieldName + " " + sSortDir_0;
                //var Customerlst = _iinvoicerepository.GetAllCustomerList(param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                // var listContacttype = _icontactrepository.GetAllContactTypesList(Contacttype == "" ? "" : Contacttype, Status == "0" ? null : Status, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listcompany,
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
        [ActionName("_BindInvoiceslistforCustomer")]
        public ActionResult _BindInvoiceslistforCustomer(string CustomerId)
        {

            try
            {
                InvoiceModel InvoBJ = new InvoiceModel();

                int customerID = Convert.ToInt32(CustomerId);
                //InvoBJ.invoice.CustomerID = customerID;

                // Get InvoiceList based on Customerid
                int totalPageCount = 0;
                InvoBJ.listinvoice = _iinvoicerepository.GetInvoiceListbyCustomerId(customerID, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                //for (int i = 0; i < InvoBJ.listinvoice.Count;i++)
                //{
                //    InvoBJ.listinvoice[i].AmountPaid = InvoBJ.listinvoice[i].BalanceAmount;
                //}

                return PartialView(InvoBJ);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}