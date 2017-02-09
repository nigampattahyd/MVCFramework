using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using System.IO;

namespace CRM.Controllers
{
    public class SystemSetupController : ControllerBase
    {
        private readonly ISystemSetupRepository _isystemsetuprepository;
        public SystemSetupController(ISystemSetupRepository isystemsetuprepository)
        {
            _isystemsetuprepository = isystemsetuprepository;
        }
             
        /// <summary>
        /// This method gives the details of company.
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult SystemSetup()
        //{
        //    try
        //    {
        //        companySetup companysetup = _isystemsetuprepository.getCompanySetup(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        var timeZones = TimeZoneInfo.GetSystemTimeZones();
        //        List<SelectListItem> items = new List<SelectListItem>();
        //        foreach (var timeZone in timeZones)
        //        {
        //            items.Add(new SelectListItem() { Text = timeZone.Id });
        //        }
        //        ViewBag.TimeZones = items;
        //        return View(companysetup);
        //    }
        //    catch (Exception)
        //    {                
        //        throw;
        //    }
            
        //}

        /// <summary>
        /// This method saves the updated details of the company.
        /// </summary>
        /// <param name="companySetup"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult SystemSetUp(companySetup companySetup,HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var timeZones = TimeZoneInfo.GetSystemTimeZones();
        //            List<SelectListItem> items = new List<SelectListItem>();
        //            foreach (var timeZone in timeZones)
        //            {
        //                items.Add(new SelectListItem() { Text = timeZone.Id });
        //            }
        //            ViewBag.TimeZones = items;
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                var filename = Path.GetFileName(file.FileName);
        //                var path = Path.Combine(Server.MapPath("~/Content/CompanySetupImages/"), filename);
        //                if (System.IO.File.Exists(path))
        //                {
        //                    file.SaveAs(path);
        //                    companySetup.LogoImage = path;
        //                }
        //                ViewBag.FileValidationMessage = "Size of logo should be 250 x 90 pixels";
        //                int companysetup = _isystemsetuprepository.UpdateCompanySetup(companySetup, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                ViewBag.Message = "Company Setup Updated Successfully. Changes will be reflected on Re-Login to the system.";
        //            }
        //            else
        //            {
        //                int companysetup = _isystemsetuprepository.UpdateCompanySetup(companySetup, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //                ViewBag.FileValidationMessage = "Size of logo should be 250 x 90 pixels";
        //                ViewBag.Message = "Company Setup Updated Successfully. Changes will be reflected on Re-Login to the system.";
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Company Setup Not Updated Successfully.";
        //        }
        //        return View();
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}
	}
}