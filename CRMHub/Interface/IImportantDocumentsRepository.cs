using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Repository;
using CRMHub.EntityFramework;


namespace CRMHub.Interface
{
    public interface IImportantDocumentsRepository
    {
       // List<ImportantDocument> GetImportantDocuments(long userId, string SelectedOffice);
        List<Branch> GetBranches(string Connectionstring, string dbName);
        List<role> GetRoles(string Connectionstring, string dbName);
       // List<ImportantDocument> SearchforImpDocs(string filterExpression, string Branch, string logInId, int? startIndex, int? pagesize, string sorting, out int TotalRecordsCount, string Connectionstring, string dbName);
       // int InsertImportantDocument(ImportantDocument impDocObj, string Connectionstring, string dbName);
       // ImportantDocument GetSelectedImpDocument(int DocumentId, string Connectionstring, string dbName);
       // int UpdateImportantDocument(ImportantDocument impDocObj, string Connectionstring, string dbName);
       // int DeleteSelectedDocument(int DelDocs, string Connectionstring, string dbName);
    }
}
