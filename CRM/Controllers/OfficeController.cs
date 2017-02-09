using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using CRMHub.ViewModel;

namespace CRM.Controllers
{
    public class OfficeController : ControllerBase
    {
        //
        // GET: /Office/
        private readonly IOfficeRepository _iofficerepository;
        private readonly ICommonRepository _istateRepository;
        public OfficeController(IOfficeRepository iofficerepository, ICommonRepository istateRepository)
        {
            _iofficerepository = iofficerepository;
            _istateRepository = istateRepository;
        }

        public ActionResult Index(BranchModel objBranch)
        {
            try
            {
                objBranch.getListOfStates = _istateRepository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return View(objBranch);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public ActionResult getBranch(jQueryDataTableParamModel param)
        {
            try
            {
                string Branchkeyword = Request["b_keyword"];
                string BranchName = Request["b_Name"];
                string BranchZipcode = Request["b_Zip"];
                string BranchCity = Request["b_City"];
                string BranchState = Request["b_state"];
                string BranchPhone = Request["b_Phone"];
                string BranchEmail = Request["b_email"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "BranchId desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                //orderByClause = SortFieldName + " " + sSortDir_0;
                List<usState> listOfState = _istateRepository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                List<SelectListItem> listOfStates = listOfState.Select(objstate => new SelectListItem
                {
                    Text = objstate.StateName,
                    Value = objstate.StateCode
                }).ToList();
                ViewBag.State = listOfStates;
                List<Branch> listofOffice = _iofficerepository.getListOfOffice(BranchName == "" ? "" : BranchName, BranchEmail == "" ? "" : BranchEmail, BranchCity == "" ? "" : BranchCity,
                    BranchPhone == "" ? "" : BranchPhone, Branchkeyword == "" ? "" : Branchkeyword, BranchState == "AA" ? "" : BranchState, BranchZipcode == "" ? "" : BranchZipcode,
                    param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listofOffice,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult CreateOffice()
        {
            try
            {
                List<usState> listOfState = _istateRepository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                List<SelectListItem> listOfStates = listOfState.Select(state => new SelectListItem
                {
                    Text = state.StateName,
                    Value = state.StateCode
                }).ToList();
                ViewBag.StateList = listOfStates;
                var listOfBranchType = new List<SelectListItem>();
                listOfBranchType.Add(new SelectListItem { Text = "Staffing", Value = "Staffing" });
                listOfBranchType.Add(new SelectListItem { Text = "HealthCare", Value = "HealthCare" });
                ViewBag.BranchType = listOfBranchType;
                return View();
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult CreateOffice(Branch objBranch)
        {
            try
            {
                int result = _iofficerepository.createBranch(objBranch, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult EditOffice(int branchId)
        {
            try
            {
                List<usState> listOfState = _istateRepository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                List<SelectListItem> listOfStates = listOfState.Select(state => new SelectListItem
                {
                    Text = state.StateName,
                    Value = state.StateCode
                }).ToList();
                ViewBag.StateList = listOfStates;
                var listOfBranchType = new List<SelectListItem>();
                listOfBranchType.Add(new SelectListItem { Text = "Staffing", Value = "Staffing" });
                listOfBranchType.Add(new SelectListItem { Text = "HealthCare", Value = "HealthCare" });
                ViewBag.BranchType = listOfBranchType;
                Branch objBranch = _iofficerepository.editBranchByBranchID(branchId, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return View(objBranch);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult UpdateOffice(Branch objBranch)
        {
            try
            {
                int result = _iofficerepository.updateBranchByBranchId(objBranch, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
               // return RedirectToAction("EditOffice", new { branchId = objBranch.BranchId });
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public bool DeleteBranchByBranchId(string branchId)
        {
            try
            {
                if (!string.IsNullOrEmpty(branchId))
                {
                    foreach (var DelBranch in branchId.Split(','))
                    {
                        var deleteBranch =_iofficerepository.DeleteBranchByBranchID(Int32.Parse(DelBranch), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
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
