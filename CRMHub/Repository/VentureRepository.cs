using CRMHub.EntityFramework;
using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Repository
{
    public class VentureRepository : IVenture
    {
        public Int64 InserNewVenture(Venture objVenture, string Connectionstring, string dbName)
        {
            try
            {
                int result = 0;
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    //dbContext.Ventures.Add(objVenture);
                    if (objVenture.VmsStatusId == 2)
                    {
                        objVenture.ModifiedDate = (objVenture.CreatedDate == (DateTime?)null) ? (DateTime?)null : objVenture.CreatedDate;
                    }
                    else
                    {
                        objVenture.ModifiedDate = (DateTime?)null;
                    }
                    result = dbContext.Venture_CreateVenture(objVenture.PrimaryEmail, objVenture.PrimaryEmail, objVenture.password, objVenture.VentureName, objVenture.LeadMentor,
                       objVenture.VmsStatusId, objVenture.PrimaryContact, objVenture.PrimaryEmail, objVenture.SecondaryContact, objVenture.SecondaryEmail,
                       objVenture.DropBox, objVenture.DropBoxFolder, objVenture.Description, objVenture.PublicDesc, objVenture.ContactInfo,
                       objVenture.VmsMailList, objVenture.SignUpReturned, objVenture.Address, objVenture.Address1, objVenture.Phone,
                       objVenture.Fax, objVenture.Cellular, objVenture.WebSite, objVenture.MITLink, objVenture.StudentIdeaBasedOnResearch, objVenture.ResearchIdeaDisclosedToTLO,
                      objVenture.LegalEntityAtEnrollment, objVenture.RefferedBy, objVenture.FirstMentorMeeting, objVenture.Comments, objVenture.IntakeNotes, objVenture.IntakeNoteSummary,
                       objVenture.SecondContact, objVenture.Introduced, objVenture.SeachIndex, objVenture.CreatedDate, objVenture.CreatedBY, objVenture.ModifiedDate);
                    dbContext.SaveChanges();
                    return objVenture.VentureId;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public int updateVentureDetails(Venture objVenture, string Connectionstring, string dbName, string operation)
        {
            int isInserted = 0;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    VentureDetail detailss = new VentureDetail();

                    Venture getVentureDetails = dbContext.Ventures.Where(c => c.VentureId == objVenture.VentureId).SingleOrDefault();

                    VentureDetail details = dbContext.VentureDetails.Where(c => c.VentureId == objVenture.VentureId).SingleOrDefault();
                   
                    if (operation == "Update")
                    {
                        if (objVenture.LeadMentor == null)
                        {
                            objVenture.LeadMentor = "0";
                        }
                        if (objVenture.PrimaryEmail == null)
                        {
                            objVenture.PrimaryEmail = getVentureDetails.PrimaryEmail;
                        }
                        if (getVentureDetails != null)
                        {
                            getVentureDetails.LeadMentor = objVenture.LeadMentor;
                            getVentureDetails.VmsStatusId = objVenture.VmsStatusId;
                            getVentureDetails.PrimaryContact = objVenture.PrimaryContact;
                            getVentureDetails.PrimaryEmail = objVenture.PrimaryEmail;
                            getVentureDetails.SecondaryContact = objVenture.SecondaryContact;
                            getVentureDetails.SecondaryEmail = objVenture.SecondaryEmail;
                            getVentureDetails.DropBox = objVenture.DropBox;
                            getVentureDetails.Description = objVenture.Description;
                            getVentureDetails.DropBoxFolder = objVenture.DropBoxFolder;
                            getVentureDetails.PublicDesc = objVenture.PublicDesc;
                            getVentureDetails.ContactInfo = objVenture.ContactInfo;
                            getVentureDetails.VmsMailList = objVenture.VmsMailList;
                            getVentureDetails.SignUpReturned = objVenture.SignUpReturned;

                            getVentureDetails.Address = objVenture.Address;
                            getVentureDetails.Address1 = objVenture.Address1;
                            getVentureDetails.Phone = objVenture.Phone;
                            getVentureDetails.Fax = objVenture.Fax;
                            getVentureDetails.Cellular = objVenture.Cellular;
                            getVentureDetails.WebSite = objVenture.WebSite;
                            getVentureDetails.MITLink = objVenture.MITLink;
                            getVentureDetails.StudentIdeaBasedOnResearch = objVenture.StudentIdeaBasedOnResearch;
                            getVentureDetails.ResearchIdeaDisclosedToTLO = objVenture.ResearchIdeaDisclosedToTLO;
                            getVentureDetails.LegalEntityAtEnrollment = objVenture.LegalEntityAtEnrollment;
                            getVentureDetails.RefferedBy = objVenture.RefferedBy;
                            getVentureDetails.FirstMentorMeeting = objVenture.FirstMentorMeeting;
                            getVentureDetails.Comments = objVenture.Comments;

                            getVentureDetails.IntakeNotes = objVenture.IntakeNotes;
                            getVentureDetails.IntakeNoteSummary = objVenture.IntakeNoteSummary;
                            getVentureDetails.Introduced = objVenture.Introduced;
                            getVentureDetails.ModifiedDate = objVenture.ModifiedDate;
                            getVentureDetails.ModifiedBY = objVenture.ModifiedBY;
                            getVentureDetails.Status = objVenture.Status;
                            //dbContext.SaveChanges();
                            details.Advertisement = objVenture.Advertisement;
                            details.CategoryOthers = objVenture.CategoryOthers;
                            details.City = objVenture.City;
                            details.HowWillUMakeMoney = objVenture.HowWillUMakeMoney;
                            details.HowWillUSolveTheProblem = objVenture.HowWillUSolveTheProblem;
                            details.IndustryCategoryName = objVenture.IndustryCategoryName;
                            details.IsThereIP = objVenture.IsThereIP;
                            details.PrimaryColaborationAffiliation = objVenture.PrimaryColaborationAffiliation;
                            details.PrimaryPhoneNo = objVenture.PrimaryPhoneNo;
                            details.PrimaryRoleInVenture = objVenture.PrimaryRoleInVenture;
                            details.ScondaryColaborationAffiliation = objVenture.ScondaryColaborationAffiliation;
                            details.SecondaryPhoneNo = objVenture.SecondaryPhoneNo;
                            details.SecondaryRoleInVenture = objVenture.SecondaryRoleInVenture;
                            details.State = objVenture.State;
                            details.Street = objVenture.Street;
                            details.WhatDoYouDo = objVenture.WhatDoYouDo;
                            details.WhatIsTheProblem = objVenture.WhatIsTheProblem;
                            details.WhatIsYourCurrentStatus = objVenture.WhatIsYourCurrentStatus;
                            details.WhatTypeOfVMSHelpStartUpNeeds = objVenture.WhatTypeOfVMSHelpStartUpNeeds;
                            details.WhoHasIt = objVenture.WhoHasIt;
                            details.WhtIsTheSecretSauce = objVenture.WhtIsTheSecretSauce;
                            details.WouldLikeToElaborateSpecifics = objVenture.WouldLikeToElaborateSpecifics;
                            details.ZIP = objVenture.ZIP;
                            dbContext.SaveChanges();
                            isInserted = 1;
                        }
                    }

                    if (operation == "Delete")
                    {
                        if (objVenture.LeadMentor == null)
                        {
                            getVentureDetails.LeadMentor = "0";
                        }
                        objVenture.VentureName = getVentureDetails.VentureName;
                        objVenture.PrimaryContact = getVentureDetails.PrimaryContact;
                        objVenture.PrimaryEmail = getVentureDetails.PrimaryEmail;
                        objVenture.LeadMentor = getVentureDetails.LeadMentor;
                        //objVenture.Status = getVentureDetails.Status;
                        objVenture.VmsStatusId = getVentureDetails.VmsStatusId;


                        if (getVentureDetails != null)
                        {
                            getVentureDetails.LeadMentor = objVenture.LeadMentor;
                            getVentureDetails.VmsStatusId = objVenture.VmsStatusId;
                            getVentureDetails.PrimaryContact = objVenture.PrimaryContact;
                            getVentureDetails.PrimaryEmail = objVenture.PrimaryEmail;
                            getVentureDetails.VentureName = objVenture.VentureName;
                            getVentureDetails.Status = objVenture.Status;
                            getVentureDetails.ModifiedDate = objVenture.ModifiedDate;
                            getVentureDetails.ModifiedBY = objVenture.ModifiedBY;
                            dbContext.SaveChanges();

                            isInserted = Convert.ToInt32(getVentureDetails.UserId);
                        }
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return isInserted;
        }



        public List<Venture> GetVentureDetails(int ventureid, string Keyword,string alphanumericsort, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName)
        {

            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    var output = new System.Data.Entity.Core.Objects.ObjectParameter("TotalRecordsCount", typeof(int));

                    //List<CRM_GetAllAccountContacts_Result> ContactsList = dbContext.CRM_GetAllAccountContacts(jtStartIndex, jtPageSize, jtSorting, output).ToList();
                    List<Venture_GetAllVentures_Result> lstVentures = dbContext.Venture_GetAllVentures(ventureid, Keyword, jtStartIndex, jtPageSize, jtSorting,alphanumericsort, output).ToList();

                    var LstVentures = lstVentures.Select(venture => new Venture
                    {
                        VentureId = venture.VentureId.GetValueOrDefault(),
                        VentureName = venture.VentureName,
                        Phone = venture.Phone,
                        WebSite = venture.Website,
                        PrimaryContact = venture.PrimaryContact,
                        PrimaryEmail = venture.PrimaryEmail,
                        SecondaryContact = venture.SecondaryContact,
                        SecondaryEmail = venture.SecondaryEmail,
                        DropBox = venture.DropBox,
                        SignUpReturned = venture.SignUpReturned,
                        Address = venture.Address,
                        MITLink = venture.MITLink,
                        Status = venture.Status,
                        strVenCreatedDate = Convert.ToDateTime(venture.CreatedDate).ToString("MM/dd/yyyy"),
                        ModifiedDate = venture.ModifiedDate,
                        totcount = Convert.ToInt32(output.Value),
                        strActiveDate = (venture.Status != null && venture.Status == "Active") ? Convert.ToDateTime(venture.ModifiedDate).ToString("MM/dd/yyyy") : "",
                        strInactiveDate = (venture.Status != null && venture.Status == "InActive") ? Convert.ToDateTime(venture.ModifiedDate).ToString("MM/dd/yyyy") : "",
                    }).ToList();
                    RecordCount = Convert.ToInt32(output.Value);

                    // RecordCount = Convert.ToInt32(LstCompanies.Count);
                    //List<Company> CompaniesList1 = dbContext.Companies.ToList();
                    //RecordCount = CompaniesList.Count;

                    return LstVentures;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<VMSStatu> GetAllVMSStatus(string Connectionstring, string dbName)
        {

            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    List<VMSStatu> lstStatuses = dbContext.VMSStatus.ToList<VMSStatu>();
                    return lstStatuses;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        public bool IsVentureExists(string VentureName, string Connectionstring, string dbName)
        {
            var flag = false;

            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    var venture = dbContext.Ventures.Where(v => v.VentureName == VentureName).FirstOrDefault();
                    if (venture != null) // update
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                    return flag;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Venture GetVentureDetailsByID(long id, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);

                    // Venture objVenture = dbContext.Ventures.Where(venture => venture.VentureId == id).FirstOrDefault();
                    var LstVentures = dbContext.Venture_GetVentureDetails(id).ToList();
                    var venturedetails = LstVentures.Select(venture => new Venture
                    {
                        VentureId = venture.VentureId.GetValueOrDefault(),
                        VentureName = venture.VentureName,
                        Phone = venture.Phone,
                        WebSite = venture.Website,
                        PrimaryContact = venture.PrimaryContact,
                        PrimaryEmail = venture.PrimaryEmail,
                        SecondaryContact = venture.SecondaryContact,
                        SecondaryEmail = venture.SecondaryEmail,
                        DropBox = venture.DropBox,
                        SignUpReturned = venture.SignUpReturned,
                        Address = venture.Address,
                        MITLink = venture.MITLink,
                        strVenCreatedDate = Convert.ToDateTime(venture.CreatedDate).ToString("MM/dd/yyyy"),
                        ModifiedDate = venture.ModifiedDate,
                        LeadMentor = venture.LeadMentor,
                        VmsStatusId = venture.VmsStatusId,
                        Description = venture.Description,
                        DropBoxFolder = venture.DropBoxFolder,
                        PublicDesc = venture.PublicDesc,
                        ContactInfo = venture.ContactInfo,
                        VmsMailList = venture.VmsMailList,
                        Address1 = venture.Address1,
                        Fax = venture.Fax,
                        Cellular = venture.Cellular,
                        StudentIdeaBasedOnResearch = venture.StudentIdeaBasedOnResearch,
                        ResearchIdeaDisclosedToTLO = venture.ResearchIdeaDisclosedToTLO,
                        LegalEntityAtEnrollment = venture.LegalEntityAtEnrollment,
                        RefferedBy = venture.RefferedBy,
                        FirstMentorMeeting = venture.FirstMentorMeeting,
                        Comments = venture.Comments,

                        IntakeNotes = venture.IntakeNotes,
                        IntakeNoteSummary = venture.IntakeNoteSummary,
                        Introduced = venture.Introduced,
                        UserId=venture.UserId,
                        Advertisement = venture.Advertisement,
                        CategoryOthers = venture.CategoryOthers,
                        City = venture.City,
                        HowWillUMakeMoney = venture.HowWillUMakeMoney,
                        HowWillUSolveTheProblem = venture.HowWillUSolveTheProblem,
                        IndustryCategoryName = venture.IndustryCategoryName,
                        IsThereIP = venture.IsThereIP,
                        PrimaryColaborationAffiliation = venture.PrimaryColaborationAffiliation,
                        PrimaryPhoneNo = venture.PrimaryPhoneNo,
                        PrimaryRoleInVenture = venture.PrimaryRoleInVenture,
                        ScondaryColaborationAffiliation = venture.ScondaryColaborationAffiliation,
                        SecondaryPhoneNo = venture.SecondaryPhoneNo,
                        SecondaryRoleInVenture = venture.SecondaryRoleInVenture,
                        State = venture.State,
                        Street = venture.Street,
                        WhatDoYouDo = venture.WhatDoYouDo,
                        WhatIsTheProblem = venture.WhatIsTheProblem,
                        WhatIsYourCurrentStatus = venture.WhatIsYourCurrentStatus,
                        WhatTypeOfVMSHelpStartUpNeeds = venture.WhatTypeOfVMSHelpStartUpNeeds,
                        WhoHasIt = venture.WhoHasIt,
                        WhtIsTheSecretSauce = venture.WhtIsTheSecretSauce,
                        WouldLikeToElaborateSpecifics = venture.WouldLikeToElaborateSpecifics,
                        ZIP = venture.ZIP
                    }).SingleOrDefault();
                    return venturedetails;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }


        public bool DeleteVentureByVentureId(long VentureId, string Connectionstring, string dbName)
        {
            bool flag = false;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    Venture venture = dbContext.Ventures.Where(ven => ven.VentureId == VentureId).SingleOrDefault();
                    if (venture != null)
                    {
                        dbContext.Ventures.Remove(venture);
                        dbContext.SaveChanges();
                        flag = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        public VMSStatu GetVmsStatusNameByID(long id, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);

                    VMSStatu objVms = dbContext.VMSStatus.Where(vms => vms.StatusId == id).FirstOrDefault();

                    return objVms;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }


        public Int64 InsertVentureDetails(Venture venture, VentureDetail venturedetails, string Connectionstring, string dbName)
        {
            int result = 0;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    venture.VmsStatusId = 1;
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    result = dbContext.Venture_CreateVentureDetails(venture.VentureName, venture.VmsStatusId, venture.PrimaryContact, venture.PrimaryEmail,
                        venture.SecondaryContact, venture.SecondaryEmail, venture.LegalEntityAtEnrollment, venture.CreatedDate, venture.Comments,
                        venturedetails.Street, venturedetails.City, venturedetails.State, venturedetails.ZIP,
                        venturedetails.IndustryCategoryName, venturedetails.CategoryOthers,
                        venturedetails.PrimaryPhoneNo, venturedetails.PrimaryRoleInVenture, venturedetails.PrimaryColaborationAffiliation,
                        venturedetails.SecondaryPhoneNo, venturedetails.SecondaryRoleInVenture, venturedetails.ScondaryColaborationAffiliation,
                        venturedetails.Advertisement, venturedetails.WouldLikeToElaborateSpecifics,
                        venturedetails.WhatDoYouDo, venturedetails.WhatIsTheProblem, venturedetails.WhoHasIt,
                        venturedetails.HowWillUSolveTheProblem, venturedetails.HowWillUMakeMoney,
                        venturedetails.WhtIsTheSecretSauce, venturedetails.IsThereIP, venturedetails.WhatIsYourCurrentStatus, venturedetails.WhatTypeOfVMSHelpStartUpNeeds,
                        venture.PrimaryEmail, venture.password);
                    dbContext.SaveChanges();
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public Venture GetPendingVenturedetailsbyId(long id, string Connectionstring, string dbName)
        {
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);

                    var LstVenturesdetails = dbContext.Venture_GetVentureDetails(id).ToList();
                    var venturedetails = LstVenturesdetails.Select(venture => new Venture
                    {
                        VentureId = venture.VentureId.GetValueOrDefault(),
                        VentureName = venture.VentureName,
                        CreatedDate = venture.CreatedDate,
                        PrimaryContact = venture.PrimaryContact,
                        PrimaryEmail = venture.PrimaryEmail,
                        SecondaryContact = venture.SecondaryContact,
                        SecondaryEmail = venture.SecondaryEmail,
                        VmsStatusId = venture.VmsStatusId,
                        LegalEntityAtEnrollment = venture.LegalEntityAtEnrollment,
                        Comments = venture.Comments,
                        Advertisement = venture.Advertisement,
                        CategoryOthers = venture.CategoryOthers,
                        City = venture.City,
                        HowWillUMakeMoney = venture.HowWillUMakeMoney,
                        HowWillUSolveTheProblem = venture.HowWillUSolveTheProblem,
                        IndustryCategoryName = venture.IndustryCategoryName,
                        IsThereIP = venture.IsThereIP,
                        PrimaryColaborationAffiliation = venture.PrimaryColaborationAffiliation,
                        PrimaryPhoneNo = venture.PrimaryPhoneNo,
                        PrimaryRoleInVenture = venture.PrimaryRoleInVenture,
                        ScondaryColaborationAffiliation = venture.ScondaryColaborationAffiliation,
                        SecondaryPhoneNo = venture.SecondaryPhoneNo,
                        SecondaryRoleInVenture = venture.SecondaryRoleInVenture,
                        State = venture.State,
                        Street = venture.Street,
                        WhatDoYouDo = venture.WhatDoYouDo,
                        WhatIsTheProblem = venture.WhatIsTheProblem,
                        WhatIsYourCurrentStatus = venture.WhatIsYourCurrentStatus,
                        WhatTypeOfVMSHelpStartUpNeeds = venture.WhatTypeOfVMSHelpStartUpNeeds,
                        WhoHasIt = venture.WhoHasIt,
                        WhtIsTheSecretSauce = venture.WhtIsTheSecretSauce,
                        WouldLikeToElaborateSpecifics = venture.WouldLikeToElaborateSpecifics,
                        ZIP = venture.ZIP
                    }).SingleOrDefault();
                    return venturedetails;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public int updatePendingVentureDetails(Venture objVenturedetail, string Connectionstring, string dbName)
        {
            int isInserted = 0;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    VentureDetail getVentureDetails = dbContext.VentureDetails.Where(c => c.VentureId == objVenturedetail.VentureId).SingleOrDefault();
                    Venture updateVMSstatus = dbContext.Ventures.Where(c => c.VentureId == objVenturedetail.VentureId).SingleOrDefault();
                    if (updateVMSstatus.LeadMentor == null)
                    {
                        updateVMSstatus.LeadMentor = "0";
                    }
                    if (getVentureDetails != null)
                    {
                        updateVMSstatus.LeadMentor = updateVMSstatus.LeadMentor;
                        updateVMSstatus.Status = "Active";
                        updateVMSstatus.VmsStatusId = 2;
                        updateVMSstatus.ModifiedDate = DateTime.Now;
                        updateVMSstatus.ModifiedBY = objVenturedetail.ModifiedBY;
                        updateVMSstatus.PrimaryContact = objVenturedetail.PrimaryContact;
                        getVentureDetails.Advertisement = objVenturedetail.Advertisement;
                        getVentureDetails.CategoryOthers = objVenturedetail.CategoryOthers;
                        getVentureDetails.City = objVenturedetail.City;
                        getVentureDetails.HowWillUMakeMoney = objVenturedetail.HowWillUMakeMoney;
                        getVentureDetails.HowWillUSolveTheProblem = objVenturedetail.HowWillUSolveTheProblem;
                        getVentureDetails.IndustryCategoryName = objVenturedetail.IndustryCategoryName;
                        getVentureDetails.IsThereIP = objVenturedetail.IsThereIP;
                        getVentureDetails.PrimaryColaborationAffiliation = objVenturedetail.PrimaryColaborationAffiliation;
                        getVentureDetails.PrimaryPhoneNo = objVenturedetail.PrimaryPhoneNo;
                        getVentureDetails.PrimaryRoleInVenture = objVenturedetail.PrimaryRoleInVenture;
                        getVentureDetails.ScondaryColaborationAffiliation = objVenturedetail.ScondaryColaborationAffiliation;
                        getVentureDetails.SecondaryPhoneNo = objVenturedetail.SecondaryPhoneNo;
                        getVentureDetails.SecondaryRoleInVenture = objVenturedetail.SecondaryRoleInVenture;
                        getVentureDetails.State = objVenturedetail.State;
                        getVentureDetails.Street = objVenturedetail.Street;
                        getVentureDetails.WhatDoYouDo = objVenturedetail.WhatDoYouDo;
                        getVentureDetails.WhatIsTheProblem = objVenturedetail.WhatIsTheProblem;
                        getVentureDetails.WhatIsYourCurrentStatus = objVenturedetail.WhatIsYourCurrentStatus;
                        getVentureDetails.WhatTypeOfVMSHelpStartUpNeeds = objVenturedetail.WhatTypeOfVMSHelpStartUpNeeds;
                        getVentureDetails.WhoHasIt = objVenturedetail.WhoHasIt;
                        getVentureDetails.WhtIsTheSecretSauce = objVenturedetail.WhtIsTheSecretSauce;
                        getVentureDetails.WouldLikeToElaborateSpecifics = objVenturedetail.WouldLikeToElaborateSpecifics;
                        getVentureDetails.ZIP = objVenturedetail.ZIP;
                        getVentureDetails.PrimaryEmail = objVenturedetail.PrimaryEmail;
                        getVentureDetails.PrimaryContact = objVenturedetail.PrimaryContact;
                        dbContext.SaveChanges();
                        return Convert.ToInt32(updateVMSstatus.UserId);
                    }

                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return isInserted;
        }
    }
}
