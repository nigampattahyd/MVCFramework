using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Interface;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace CRMHub.Repository
{
    public class ProjectRepository : IProjectRepository
    {


        public List<tbl_ProjectDetails> GetAllProjectsList(string ProjectName, string Status, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                //var ProjectData  = new List<tbl_ProjectDetails>();
                List<CRM_GET_Projectslist_Result> lstProjectlist = obj.CRM_GET_Projectslist(ProjectName, Status, startIndex, pageSize, orderByClause, output).ToList(); 
                //List<CRM_GETALL_Projectslist_Result> lstProjectlist = obj.CRM_GETALL_Projectslist(ProjectName, Status, startIndex, pageSize, orderByClause, output).ToList();
                TotalCount = Convert.ToInt32(output.Value);

                List<tbl_ProjectDetails> lstProjectDetails = lstProjectlist.Select(project => new tbl_ProjectDetails
                {
                    ProjectId = project.ProjectId ?? 0,
                    Projectname = project.Projectname,
                    ContactName = project.ContactName,
                    Contact = project.Contact,
                    Status = project.Status,
                    URL = project.URL,
                    DateCreated = project.DateCreated
                }).ToList();



                return lstProjectDetails;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int createProject(tbl_ProjectDetails objproject, string Connectionstring, string dbName)
        {
            try
            {
                int result = 0;
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                objproject.DateCreated = DateTime.Now;
                result = obj.CRM_Addproject(objproject.Projectname, objproject.Contact, objproject.Status, objproject.URL, objproject.DateCreated, objproject.Description);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int DeleteProjectByProjectId(int ProjectId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                return obj.CRM_deleteProjectByProjectId(ProjectId);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public tbl_ProjectDetails editprojectdetailsbProjectId(int ProjectId, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
               
                var result = obj.CRM_GETProjectDetailsByProjectID(ProjectId).Select(tblprojct => new tbl_ProjectDetails
                {
                    ProjectId = tblprojct.ProjectId,
                    Projectname = tblprojct.Projectname,
                    ContactName = tblprojct.fname,
                    Contact = tblprojct.Contact,
                    Status = tblprojct.Status,
                    URL = tblprojct.URL,
                    DateCreated = tblprojct.DateCreated,
                    Description = tblprojct.Description
                }).SingleOrDefault();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateProjectDetailsByProjectId(tbl_ProjectDetails objproj, string Connectionstring, string dbName)
        {
            try
            {
                DevEntities obj = new DevEntities(Connectionstring);
                obj.Database.Connection.Open();
                obj.Database.Connection.ChangeDatabase(dbName);
                //objproj.DateCreated = DateTime.Now;
                var result = obj.CRM_UpdateProjectDetailsByProjectId(objproj.ProjectId, objproj.Projectname, objproj.Contact, objproj.Status, objproj.URL, objproj.DateCreated, objproj.Description);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


       
    }
}
