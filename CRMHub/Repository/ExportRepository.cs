using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Repository
{
    public class ExportRepository : IExportRepository
    {

        public DataSet GetExportDocuments(string office, string roleId, string logInId, string Connectionstring)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(Connectionstring))
                {
                    SqlCommand sqlComm = new SqlCommand("CRM_ExportCompaniesDatawithCustomField", conn);
                    //sqlComm.Parameters.AddWithValue("@keyword", keyword);
                    sqlComm.Parameters.AddWithValue("@office", office);
                    //sqlComm.Parameters.AddWithValue("@Status", Status);
                    sqlComm.Parameters.AddWithValue("@RoleID", roleId);
                    sqlComm.Parameters.AddWithValue("@logInId", logInId);
                    //sqlComm.Parameters.AddWithValue("@salesRep", salesRep);
                    //sqlComm.Parameters.AddWithValue("@FilterExpression", filterExpression);
                    //sqlComm.Parameters.AddWithValue("@StartIndex", startIndex);
                    //sqlComm.Parameters.AddWithValue("@PageSize", pageSize);
                    //sqlComm.Parameters.AddWithValue("@Sorting", sorting);
                    foreach (SqlParameter Parameter in sqlComm.Parameters)
                    {
                        if (Parameter.Value == null)
                        {
                            Parameter.Value = DBNull.Value;
                        }
                    }

                    sqlComm.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    da.Fill(ds);
                }

                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public List<ExportCompanyData> GetExportDocuments(string Account_office, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities obj = new DevEntities(Connectionstring);
        //        obj.Database.Connection.Open();
        //        obj.Database.Connection.ChangeDatabase(dbName);
        //        //List<CRM_ExportCompaniesDate_Result> lstExportDocumentsResult = obj.CRM_ExportCompaniesDate(Account_office).ToList();

        //        List<CRM_ExportCompaniesDatawithCustomField_Result> lstExportDocumentsResult = obj.CRM_ExportCompaniesDatawithCustomField(Account_office).ToList();

        //        List<ExportCompanyData> listExportDocuments = lstExportDocumentsResult.Select(ExpoDocumnt => new ExportCompanyData
        //        {
        //            AccountId=ExpoDocumnt.AccountId,
        //            Account_Name=ExpoDocumnt.Account_Name,
        //            AccountSite=ExpoDocumnt.AccountSite,
        //            Phone=ExpoDocumnt.Phone,
        //            Shippingcity=ExpoDocumnt.Shippingcity,
        //            Shippingzip=ExpoDocumnt.Shippingzip,
        //            CreatedDate=Convert.ToDateTime(ExpoDocumnt.CreatedDate),
        //            Account_type=ExpoDocumnt.Account_type,
        //            CustomLabel = ExpoDocumnt.CustomLabel,
        //            Customvalue = ExpoDocumnt.CustomValue
        //        }).ToList();
        //        return listExportDocuments;
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
