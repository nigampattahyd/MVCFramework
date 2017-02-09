using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Repository;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using System.IO;
using System.Configuration;
using CRMHub.ViewModel;
namespace CRM.Controllers
{
    public class ImportantDocumentsController : ControllerBase
    {
        #region
        private readonly IImportantDocumentsRepository _impDocRepository;
        string loginId = GlobalVariables.UserID;
        string roleId = GlobalVariables.RoleID;
        public ImportantDocumentsController()
        {
            this._impDocRepository = new ImportantDocumentsRepository();
        }
        #endregion

        public ActionResult ViewImportantDocuments()
        {
            try
            {
                ImportantDocumentsModel ImpDocsModelObj = new ImportantDocumentsModel();
                Branch selectOption = new Branch();
                selectOption.BranchId = 0;
                selectOption.BranchName = "Select All";
                ImpDocsModelObj.branchList = new List<Branch>();
                ImpDocsModelObj.branchList.Add(selectOption);
                ImpDocsModelObj.branchList.AddRange(_impDocRepository.GetBranches(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
                return View(ImpDocsModelObj);
            }
            catch(Exception)
            {
                throw;
            }
        }

        //public ActionResult getImportantDocuments(jQueryDataTableParamModel param)
        //{
        //    try
        //    {
        //        string FilterExpression = Request["ImpDocFilterExp"];
        //        string Branch = Request["ImpDocBranch"];
        //        string FilterString = Request["ImpDocFilterString"];
        //        string iSortCol_0 = Request["iSortCol_0"];
        //        string sSortDir_0 = Request["sSortDir_0"];
        //        string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
        //        string orderByClause = string.Empty;
        //        int totalPageCount = 0;
        //        if (iSortCol_0 == "0")
        //            orderByClause = "DocId desc";
        //        else
        //            orderByClause = SortFieldName + " " + sSortDir_0;
        //        var listImportantDocs = _impDocRepository.SearchforImpDocs(FilterString, Branch == "0" ? "All" : Branch,
        //            loginId, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            aaData = listImportantDocs,
        //            iTotalRecords = totalPageCount,
        //            iTotalDisplayRecords = totalPageCount
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet]
        //[ActionName("CreateNewImportantDoc")]
        //public ActionResult CreateNewImportantDoc()
        //{
        //    try
        //    {
        //        ImportantDocumentsModel ImpDocsModelObj = new ImportantDocumentsModel();
        //        Branch selectOption = new Branch();
        //        selectOption.BranchId = 0;
        //        selectOption.BranchName = "Select All";
        //        role selectrole = new role();
        //        selectrole.RoleId = 0;
        //        selectrole.RoleName = "Select All";
        //        ImpDocsModelObj.branchList = new List<Branch>();
        //        ImpDocsModelObj.branchList.Add(selectOption);
        //        ImpDocsModelObj.branchList.AddRange(_impDocRepository.GetBranches(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        ImpDocsModelObj.rolesList = new List<role>();
        //        ImpDocsModelObj.rolesList.Add(selectrole);
        //        ImpDocsModelObj.rolesList.AddRange(_impDocRepository.GetRoles(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        return View(ImpDocsModelObj);
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ActionResult CreateNewImportantDoc(ImportantDocumentsModel ImpDocsModelObj, HttpPostedFileBase uploadFile)
        //{
        //    try
        //    {
        //        if (uploadFile.ContentLength > 0)
        //        {
        //            string filePath = Path.Combine(HttpContext.Server.MapPath("~/Content/Files"),
        //            Path.GetFileName(uploadFile.FileName));
        //            uploadFile.SaveAs(filePath);
        //            ImpDocsModelObj.impDocsEntity.CreatedBy = Convert.ToInt64(loginId);
        //            ImpDocsModelObj.impDocsEntity.DocFileNameOriginal = uploadFile.FileName;
        //            ImpDocsModelObj.impDocsEntity.DocFileName = uploadFile.FileName;
        //            var result = _impDocRepository.InsertImportantDocument(ImpDocsModelObj.impDocsEntity, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        }
        //        return RedirectToAction("ViewImportantDocuments", ImpDocsModelObj.impDocsEntity);
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpGet]
        //[ActionName("EditNewImportantDoc")]
        //public ActionResult EditNewImportantDoc(int Docid)
        //{
        //    try
        //    {
        //        ImportantDocumentsModel ImpDocsModelObj = new ImportantDocumentsModel();
        //        Branch selectOption = new Branch();
        //        selectOption.BranchId = 0;
        //        selectOption.BranchName = "Select All";
        //        role selectrole = new role();
        //        selectrole.RoleId = 0;
        //        selectrole.RoleName = "Select All";
        //        ImpDocsModelObj.branchList = new List<Branch>();
        //        ImpDocsModelObj.branchList.Add(selectOption);
        //        ImpDocsModelObj.branchList.AddRange(_impDocRepository.GetBranches(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        ImpDocsModelObj.rolesList = new List<role>();
        //        ImpDocsModelObj.rolesList.Add(selectrole);
        //        ImpDocsModelObj.rolesList.AddRange(_impDocRepository.GetRoles(GlobalVariables.ConnectionString, GlobalVariables.ClientName));
        //        ImpDocsModelObj.impDocsEntity = _impDocRepository.GetSelectedImpDocument(Docid, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        return View(ImpDocsModelObj);
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ActionResult EditNewImportantDoc(ImportantDocumentsModel ImpDocsModelObj, HttpPostedFileBase uploadFile)
        //{
        //    try
        //    {
        //        if (uploadFile != null && uploadFile.ContentLength > 0)
        //        {
        //            string filePath = Path.Combine(HttpContext.Server.MapPath("~/Content/Files"),
        //            Path.GetFileName(uploadFile.FileName));
        //            uploadFile.SaveAs(filePath);
        //            ImpDocsModelObj.impDocsEntity.CreatedBy = Convert.ToInt64(loginId);
        //            ImpDocsModelObj.impDocsEntity.DocFileNameOriginal = uploadFile.FileName;
        //            TempData["File"] = ImpDocsModelObj.impDocsEntity.DocFileNameOriginal;
        //            ImpDocsModelObj.impDocsEntity.DocFileName = uploadFile.FileName;
        //            if (ImpDocsModelObj.impDocsEntity.DocId != null)
        //            {
        //                var result = _impDocRepository.UpdateImportantDocument(ImpDocsModelObj.impDocsEntity, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //            }
        //        }
        //        else
        //        {
        //            if (ImpDocsModelObj.impDocsEntity.DocId != null)
        //            {
        //                ImpDocsModelObj.impDocsEntity.CreatedBy = Convert.ToInt64(loginId);
        //                var result = _impDocRepository.UpdateImportantDocument(ImpDocsModelObj.impDocsEntity, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //            }
        //        }
        //        return RedirectToAction("ViewImportantDocuments", ImpDocsModelObj.impDocsEntity);
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public bool DeleteImportantDocument(string docIds)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(docIds))
        //        {
        //            foreach (var DelImpDocs in docIds.Split(','))
        //            {
        //                var deleteDocs =_impDocRepository.DeleteSelectedDocument(Int32.Parse(DelImpDocs), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //            }

        //        }
        //        return true;
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

        public FileResult Download(string filename)
        {
            try
            {
                string filePath = Server.MapPath("~/Content/Files");
                string contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
                return File(Path.Combine(filePath, filename), contentType, filename);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}