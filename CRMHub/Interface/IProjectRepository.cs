using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Aspects;
using System.Data;

namespace CRMHub.Interface
{

    public interface IProjectRepository
    {
        List<tbl_ProjectDetails> GetAllProjectsList(string ProjectName, string Status, int startIndex, int pageSize, string orderByClause, out int TotalCount, string Connectionstring, string dbName);
        int createProject(tbl_ProjectDetails objproject, string Connectionstring, string dbName);
        int DeleteProjectByProjectId(int ProjectId, string Connectionstring, string dbName);
        tbl_ProjectDetails editprojectdetailsbProjectId(int ProjectId, string Connectionstring, string dbName);
        int UpdateProjectDetailsByProjectId(tbl_ProjectDetails objproj, string Connectionstring, string dbName);

    }
}
