using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;
using CRMHub.EntityFramework;

namespace CRMHub.Repository
{
    public class SMSProviderRepository : ISMSProviderRepository
    {
        /// <summary>
        /// Method to Bind all Existing SMS Providers and Display in a Grid
        /// </summary>
        /// <returns></returns>
        public List<SMSProviderList> GetSMSProviders()
        {
            try
            {
                SMSProviderList smsprov = new SMSProviderList();
                using (var ClientEntity = new DevEntities())
                {
                    List<CRM_GetSMSProvider_Result>smsproviders = ClientEntity.CRM_GetSMSProvider().ToList();
                    return smsproviders.Select(serviceproviders => new SMSProviderList {
                        ID = serviceproviders.ID,
                        Carrier = serviceproviders.Carrier,
                        Email = serviceproviders.emailused                      
                    }).ToList();
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }


        public List<SMSProviderList> LoadSMSProviders(string logInId, string roleId, int? startIndex, int? pagesize, string sorting, out int TotalRecordsCount, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));
                    List<CRM_LoadSMSProvidersList_Result> listofproviders = ClientEntity.CRM_LoadSMSProvidersList(Convert.ToInt64(logInId), Convert.ToInt16(roleId), startIndex, pagesize, sorting, output).ToList();
                    var result = listofproviders.Select(selprovider => new SMSProviderList
                    {
                        ID = Convert.ToInt16(selprovider.ID),
                        Carrier = selprovider.Carrier,
                        Email = selprovider.emailused    
                    }).ToList();
                    TotalRecordsCount = Convert.ToInt32(output.Value);
                    return result;
                }                 
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        /// <summary>
        /// Method to Add a New SMS Service Provider 
        /// </summary>
        /// <param name="smsProviderObj"></param>
        /// <returns></returns>
        public int CreateSMSProvider(SMSProviderList smsProviderObj, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var result = ClientEntity.CRM_InsertSMSProvider(smsProviderObj.Carrier, smsProviderObj.Email);
                    return result;
                }
            }
            catch (Exception)
            {                
                throw;
            }           
        }

        /// <summary>
        /// Method to Retrieve SMS Service Provider Details by Id
        /// </summary>
        /// <param name="ProviderId"></param>
        /// <returns></returns>
        public SMSProviderList GetSelectedSMSProvider(int ProviderId, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var ProvidersList = ClientEntity.CRM_GetSMSProviderbyID(Convert.ToString(ProviderId)).ToList();
                    var SMSPList = ProvidersList.Select(smsproviders => new SMSProviderList
                    {
                        ID = smsproviders.ID,
                        Carrier = smsproviders.Carrier,
                        Email = smsproviders.Emailused
                    }).ToList();
                    return SMSPList.SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }           
        }

        /// <summary>
        /// Method to Update the Service Provider Details
        /// </summary>
        /// <param name="smsProviderObj"></param>
        /// <returns></returns>
        public int UpdateSMSProvider(SMSProviderList smsProviderObj, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                    ClientEntity.Database.Connection.Open();
                    ClientEntity.Database.Connection.ChangeDatabase(dbName);
                    var result = ClientEntity.CRM_UpdateSMSProvider(Convert.ToString(smsProviderObj.ID), smsProviderObj.Carrier, smsProviderObj.Email);
                    return result;
                }
            }
            catch (Exception)
            {                
                throw;
            }           
        }

        /// <summary>
        /// Method to Delete an Existing SMS Provider
        /// </summary>
        /// <param name="DelProviders"></param>
        /// <returns></returns>
        public int DeleteSelectedSMSProvider(string DelProviders, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities ClientEntity = new DevEntities(Connectionstring))
                {
                   ClientEntity.Database.Connection.Open();
                   ClientEntity.Database.Connection.ChangeDatabase(dbName);
                   var result = ClientEntity.CRM_DeleteSMSProvider(DelProviders);
                   return result;
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }       
    }
}
