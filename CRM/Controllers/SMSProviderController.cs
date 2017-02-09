using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Repository;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using CRMHub.ViewModel;

namespace CRM.Controllers
{
    public class SMSProviderController : ControllerBase
    {
        #region
        private readonly ISMSProviderRepository _smsProviderRepo;
        string loginId = GlobalVariables.UserID;
        string roleId = GlobalVariables.RoleID;
        public SMSProviderController()
        {
            this._smsProviderRepo = new SMSProviderRepository();
        }
        #endregion

        /// <summary>
        ///Method for Viewing All Existing Service Providers
        /// </summary>
        /// <returns></returns>
        public ActionResult SMSServiceProviders()
        {
            try
            {
                return View();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public ActionResult getSMSProviders(jQueryDataTableParamModel param)
        {
            try
            {
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                orderByClause = SortFieldName + " " + sSortDir_0;
                var listProviders = _smsProviderRepo.LoadSMSProviders(loginId, roleId, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listProviders,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                throw;
            }
        }

       /// <summary>
       /// Initial Method to Load AddSMSProvider Screen
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        public ActionResult AddSMSProvider()
        {
            try
            {
                return View();
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Call Add Functionality to Add a new SMS Provider
        /// </summary>
        /// <param name="smsProviderObj"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddSMSProvider(SMSProviderList smsProviderObj)
        {
            try
            {
                var result = _smsProviderRepo.CreateSMSProvider(smsProviderObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return RedirectToAction("SMSServiceProviders");
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Retrieve the Details of a SMS Provider with help of Unique ID
        /// </summary>
        /// <param name="Providerid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditSMSProvider(int Providerid)
        {
            try
            {
                var result = _smsProviderRepo.GetSelectedSMSProvider(Providerid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return View(result);
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to call Update Functionality to Update the SMS Provider Details 
        /// </summary>
        /// <param name="smsProviderObj"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditSMSProvider(SMSProviderList smsProviderObj)
        {
            try
            {
                var result = _smsProviderRepo.UpdateSMSProvider(smsProviderObj, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return RedirectToAction("SMSServiceProviders");
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Call Delete Functionality to Delete an Existing record of SMS Provider
        /// </summary>
        /// <param name="smsproviderIds"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteSMSProvider(string smsproviderIds)
        {
            try
            {
                if (!string.IsNullOrEmpty(smsproviderIds))
                {
                    foreach (var DelSMSProviders in smsproviderIds.Split(','))
                    {
                        var deleteSMSProv = _smsProviderRepo.DeleteSelectedSMSProvider(DelSMSProviders, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
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