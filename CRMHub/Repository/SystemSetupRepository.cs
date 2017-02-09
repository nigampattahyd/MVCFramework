using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using System.Data.Entity;

namespace CRMHub.Repository
{
    public class SystemSetupRepository : ISystemSetupRepository
    {
        //public companySetup getCompanySetup(string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //        DevEntities objEntity = new DevEntities(Connectionstring);
        //        objEntity.Database.Connection.Open();
        //        objEntity.Database.Connection.ChangeDatabase(dbName);
        //        companySetup companySetup = new companySetup();
        //        var compsetup = objEntity.CRM_GET_SystemSetup().ToList();
        //        companySetup.CompanyId = compsetup[0].CompanyId;
        //        companySetup.CompanyName = compsetup[0].CompanyName;
        //        companySetup.AdminEmail = compsetup[0].AdminEmail;
        //        companySetup.CompanyURL = compsetup[0].CompanyURL;
        //        companySetup.TimeZone = compsetup[0].TimeZone;
        //        companySetup.LogoImage = compsetup[0].LogoImage;
        //        companySetup.HomeContent = compsetup[0].HomeContent;
        //        return companySetup;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int UpdateCompanySetup(companySetup companySetup, string Connectionstring, string dbName)
        //{
        //    try
        //    {
        //    DevEntities obj = new DevEntities(Connectionstring);
        //    obj.Database.Connection.Open();
        //    obj.Database.Connection.ChangeDatabase(dbName);            
        //    var result = obj.CRM_addUpdateCompanySetup(companySetup.CompanyName, companySetup.AdminEmail, companySetup.CompanyLogo, companySetup.ModifiedBy, companySetup.CompanyURL,
        //                                             companySetup.TwitterId, companySetup.TwitterPassword, companySetup.FacebookId, companySetup.FacebookPassword,
        //                                             companySetup.LinkedInId, companySetup.LinkedInPassword, companySetup.LogoImage, companySetup.CompanyURL,
        //                                             companySetup.JoinNowLogo,companySetup.From, companySetup.AssociateBranch, companySetup.Position, companySetup.HomeContent,
        //                                             companySetup.TimeZone, companySetup.PTOCalculation);
        //    return result;
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}        
    }
}
