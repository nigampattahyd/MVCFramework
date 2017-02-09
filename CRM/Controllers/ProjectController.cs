using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.EntityFramework;
using CRMHub.ViewModel;
using CRMHub.Interface;

namespace CRM.Controllers
{
    public class ProjectController : Controller
    {

        private readonly IProjectRepository _iprojectrepository;
        public ProjectController(IProjectRepository iprojectrepository)
        {


            _iprojectrepository = iprojectrepository;

        }

        // GET: Project
        public ActionResult Index(ProjectModel modelproject)
        {
            return View(modelproject);
        }

        public ActionResult getProjectsList(jQueryDataTableParamModel param)
        {
            try
            {
                string ProjectName = Request["Projectname"];
                string Status = Request["ProStatus"];
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "ProjectId desc";
                else
                    orderByClause = SortFieldName + " " + sSortDir_0;
                var listProject = _iprojectrepository.GetAllProjectsList(ProjectName == "" ? "" : ProjectName, Status == "0" ? null : Status, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                for (int a = 0; a < listProject.Count; a++)
                {
                    if (listProject[a].Status == "0")
                    {
                        listProject[a].Status = " ";
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

        public ActionResult CreateProject()
        {
            try
            {
                // tbl_ProjectDetails projectdetails = new tbl_ProjectDetails();


                return View();
            }
            catch (Exception)
            {
                throw;
            }


        }
        [HttpGet]
        public ActionResult Editprojectdetails(int ProjectId)
        {
            try
            {
                tbl_ProjectDetails objTblproject = new tbl_ProjectDetails();

                objTblproject = _iprojectrepository.editprojectdetailsbProjectId(Convert.ToInt32(ProjectId), GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                ProjectModel Prjdetails = new ProjectModel();
                Prjdetails.ProjectDetails = objTblproject;
                return View(Prjdetails);
            }

            catch (Exception)
            {
                throw;
            }


        }
        [HttpPost]
        public ActionResult UpdateProject(ProjectModel objproject)
        {
            try
            {
                //objproject.ProjectDetails.Contact = objproject.ProjectDetails.ContactId;
                int result = _iprojectrepository.UpdateProjectDetailsByProjectId(objproject.ProjectDetails, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost]
        public ActionResult AddProject(ProjectModel objproject)
        {
            try
            {
                // objproject.ProjectDetails.Contact = objproject.ProjectDetails.ContactId;
                int result = _iprojectrepository.createProject(objproject.ProjectDetails, GlobalVariables.ConnectionString, GlobalVariables.ClientName);


                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public bool DeleteProjects(string Projids)
        {
            try
            {
                if (!string.IsNullOrEmpty(Projids))
                {
                    foreach (var projid in Projids.Split(','))
                    {
                        var deleteProj = _iprojectrepository.DeleteProjectByProjectId(Convert.ToInt32(projid), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
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