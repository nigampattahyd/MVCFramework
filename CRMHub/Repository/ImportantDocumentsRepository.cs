using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;
using CRMHub.EntityFramework;


namespace CRMHub.Repository
{
   public class ImportantDocumentsRepository : IImportantDocumentsRepository
    {
       //public List<ImportantDocument> GetImportantDocuments(long userId, string SelectedOffice)
       //{
       //    try
       //    {
       //        using (var ClientEntity = new DevEntities())
       //        { 
       //            ImportantDocument impdocObj = new ImportantDocument();
       //            List<CRM_GetImportantDocuments_Result> Docslst = ClientEntity.CRM_GetImportantDocuments(userId, Convert.ToInt64(SelectedOffice)).ToList();
       //            return Docslst.Select(docs => new ImportantDocument
       //                {
       //                    DocId = docs.DocId,
       //                    DocName = docs.DocName,
       //                    DocDescription = docs.DocDescription,
       //                    DocFileNameOriginal = docs.DocFileNameOriginal,
       //                    DocFileName = docs.DocFileName,
       //                    DocShareToUser = docs.DocShareToUser,
       //                    DocShareToOffice = docs.DocShareToOffice,
       //                    IsActive = docs.IsActive,
       //                    CreatedBy = docs.CreatedBy,
       //                    CreatedDate = docs.CreatedDate,
       //                    ModifiedBy = docs.ModifiedBy,
       //                    ModifiedDate = docs.ModifiedDate
       //                }).ToList();                                                         
       //         }
       //    }
       //    catch (Exception)
       //    {
       //        throw;
       //    }
       //}

       public List<Branch> GetBranches(string Connectionstring, string dbName)
       {
           try
           {
               using (DevEntities ClientEntity = new DevEntities(Connectionstring))
               {
                   ClientEntity.Database.Connection.Open();
                   ClientEntity.Database.Connection.ChangeDatabase(dbName);
                   List<CRM_GetBranchListAll_Result> lstbranches = ClientEntity.CRM_GetBranchListAll().ToList();
                   return lstbranches.Select(objbranches => new Branch
                   {
                       BranchId = objbranches.branchId,
                       BranchName = objbranches.branchName                       
                   }).ToList();
               }
           }
           catch (Exception)
           {            
               throw;
           }
        
       }
       public List<role> GetRoles(string Connectionstring, string dbName)
       {
           try
           {
               using (DevEntities ClientEntity = new DevEntities(Connectionstring))
               {
                   ClientEntity.Database.Connection.Open();
                   ClientEntity.Database.Connection.ChangeDatabase(dbName);
                   List<CRM_GetLevels_Result> lstroles = ClientEntity.CRM_GetLevels().ToList();
                   return lstroles.Select(objroles => new role
                   {
                       RoleId = objroles.RoleId,
                       RoleName = objroles.RoleName
                   }).ToList();
               }
           }
           catch (Exception)
           {
               throw;
           }
       }

       //public List<ImportantDocument> SearchforImpDocs(string filterExpression, string Branch, string logInId, int? startIndex, int? pagesize, string sorting, out int TotalRecordsCount, string Connectionstring, string dbName)
       //{
       //    try
       //    {
       //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
       //        {
       //            ClientEntity.Database.Connection.Open();
       //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
       //            var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
       //            List<CRM_SearchImportantDocuments_Result> listofDocs = ClientEntity.CRM_SearchImportantDocuments(filterExpression, Branch, Convert.ToInt64(logInId), startIndex, pagesize, sorting, output).ToList();
       //            var result = listofDocs.Select(impDocs => new ImportantDocument
       //            {
       //                DocId = Convert.ToInt64(impDocs.DocId),
       //                DocName = impDocs.DocName,
       //                DocDescription = impDocs.DocDescription,
       //                DocFileNameOriginal = impDocs.DocFileNameOriginal,
       //                DocFileName = impDocs.DocFileName,
       //                IsActive = Convert.ToBoolean(impDocs.IsActive),
       //                CreatedDate=Convert.ToDateTime(impDocs.CreatedDate)
       //            }).ToList();
       //            TotalRecordsCount = Convert.ToInt32(output.Value);
       //            return result;
       //        }
       //    }
       //    catch (Exception)
       //    {
       //        throw;
       //    }
       //}

       //public int InsertImportantDocument(ImportantDocument impDocObj, string Connectionstring, string dbName)
       //{
       //    int result = 0;
       //    try
       //    {
       //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
       //        {
       //            ClientEntity.Database.Connection.Open();
       //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
       //             result = ClientEntity.CRM_InsertUpdateImportantDocuments(impDocObj.DocId, impDocObj.DocName, impDocObj.DocDescription, impDocObj.DocFileNameOriginal,
       //             impDocObj.DocFileName, impDocObj.DocShareToUser, impDocObj.DocShareToOffice, Convert.ToBoolean(impDocObj.IsActive), impDocObj.CreatedBy, 1);
       //        }
       //    }
       //    catch (Exception)
       //    {
       //        throw;
       //    }
       //    return result;           
       //}

       //public ImportantDocument GetSelectedImpDocument(int DocumentId, string Connectionstring, string dbName)
       //{
       //    try
       //    {
       //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
       //        {
       //            ClientEntity.Database.Connection.Open();
       //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
       //            var lstImpDocs = ClientEntity.CRM_GetSelectedImpDocument(DocumentId).ToList();
       //            var lstDocs = lstImpDocs.Select(ImpDocs => new ImportantDocument
       //                {
       //                    DocId = ImpDocs.DocId,
       //                    DocName = ImpDocs.DocName,
       //                    DocDescription = ImpDocs.DocDescription,
       //                    DocFileNameOriginal = ImpDocs.DocFileNameOriginal,
       //                    DocFileName = ImpDocs.DocFileName,
       //                    DocShareToUser = ImpDocs.DocShareToUser,
       //                    DocShareToOffice = ImpDocs.DocShareToOffice,
       //                    IsActive = ImpDocs.IsActive,
       //                    CreatedBy = ImpDocs.CreatedBy,
       //                    CreatedDate = ImpDocs.CreatedDate,
       //                    ModifiedBy = ImpDocs.ModifiedBy,
       //                    ModifiedDate = ImpDocs.ModifiedDate
       //                }).ToList();
       //            return lstDocs.SingleOrDefault();
       //        }
       //    }
       //    catch (Exception)
       //    {               
       //        throw;
       //    }           
       //}

       //public int UpdateImportantDocument(ImportantDocument impDocObj, string Connectionstring, string dbName)
       //{
       //    try
       //    {
       //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
       //        {
       //            ClientEntity.Database.Connection.Open();
       //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
       //            var result = ClientEntity.CRM_InsertUpdateImportantDocuments(impDocObj.DocId, impDocObj.DocName, impDocObj.DocDescription, impDocObj.DocFileNameOriginal,
       //            impDocObj.DocFileName, impDocObj.DocShareToUser, impDocObj.DocShareToOffice, Convert.ToBoolean(impDocObj.IsActive), impDocObj.CreatedBy, 2);
       //        }
       //    }
       //    catch (Exception)
       //    {
       //        throw;
       //    }
       //    return 1;
       //}

       //public int DeleteSelectedDocument(int DelDocs, string Connectionstring, string dbName)
       //{
       //   try
       //    {
       //        using (DevEntities ClientEntity = new DevEntities(Connectionstring))
       //        {
       //            ClientEntity.Database.Connection.Open();
       //            ClientEntity.Database.Connection.ChangeDatabase(dbName);
       //            var result = ClientEntity.CRM_DeleteSelectedDocuments(DelDocs);
       //            return result;
       //        }
       //    }
       //    catch (Exception)
       //    {              
       //        throw;
       //    }          
       //}
    }
}
