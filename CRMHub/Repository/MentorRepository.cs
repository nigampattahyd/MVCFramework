using CRMHub.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using System.Data;
using System.Globalization;

namespace CRMHub.Repository
{
    public class MentorRepository : IMentor
    {
        public long InserMentor(Mentor mentor, string Connectionstring, string dbName)
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
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    result = dbContext.Mentor_CreateMentor(mentor.Email, mentor.Email, mentor.Password, mentor.Name, mentor.VentureId, mentor.MentorTypeId, mentor.StartDate, mentor.EndDate,
                      mentor.HaverelevantExp,mentor.WillhelpifNeeded,mentor.Recruited,mentor.isInterested,mentor.Conflict_FinancialInterest,
                        mentor.Comment,mentor.CreatedDate,mentor.CreatedBy,mentor.VmsStatus);
                    //dbContext.Mentors.Add(mentor);
                    dbContext.SaveChanges();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Mentor GetMentorDetailsByID(long id, string Connectionstring, string dbName)
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

                    Mentor objMentor = dbContext.Mentors.Where(mentor => mentor.MentorId == id).FirstOrDefault();
                    if (objMentor.VentureId != null && objMentor.VentureId!=0)
                    {
                        Venture objventure = dbContext.Ventures.Where(venture => venture.VentureId == objMentor.VentureId).FirstOrDefault();

                        objMentor.VentureName = objventure.VentureName;
                    }
                    else
                    {
                        

                        objMentor.VentureName = "";
                    }
                
                    //               Mentor objMen=dbContext.Mentors.Join(dbContext.Ventures, 
                    //s => s.VentureId,
                    //sa => sa.VentureId,(s, sa) => new { men = s, ven = sa }).Where(ssa => ssa.men.MentorId == id).Select(ssa => ssa.men).FirstOrDefault();
                    return objMentor;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int updateMentorDetails(Mentor objMentor, MentorDetail objmentordetail, MentorQuestionary objmentorquestionary, string Connectionstring, string dbName)
        {
            int isInserted = 0;
            try
            {
                Venture objventure = new Venture();
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    Mentor getMentorDetails = dbContext.Mentors.Where(c => c.MentorId == objMentor.MentorId).SingleOrDefault();
                    MentorDetail updatementorDetails = dbContext.MentorDetails.Where(c => c.MentorId == objmentordetail.MentorId).SingleOrDefault();
                    MentorQuestionary updatementorquestionarrie = dbContext.MentorQuestionaries.Where(c => c.MentorId == objmentordetail.MentorId).SingleOrDefault();
                    if (objMentor.VentureId != null && objMentor.VentureId != 0)
                    {
                        objventure = dbContext.Ventures.Where(v => v.VentureId == objMentor.VentureId).SingleOrDefault();
                    }
                    if (objmentordetail.FirstName == null)
                    {
                        objmentordetail.FirstName = updatementorDetails.FirstName;
                    }
                    if (objmentordetail.LastName == null)
                    {
                        objmentordetail.LastName = updatementorDetails.LastName;
                    }
                    if (objmentordetail.PrimaryEmail == null)
                    {
                        objmentordetail.PrimaryEmail = updatementorDetails.PrimaryEmail;
                    }
                    if (objmentordetail.Telephone1 == null)
                    {
                        objmentordetail.Telephone1 = updatementorDetails.Telephone1;
                    }
                    if (getMentorDetails != null)
                    {
                        getMentorDetails.VentureId = objMentor.VentureId;
                        getMentorDetails.MentorTypeId = objMentor.MentorTypeId;
                        getMentorDetails.StartDate = objMentor.StartDate;
                        getMentorDetails.EndDate = objMentor.EndDate;
                        getMentorDetails.VmsStatus = objMentor.VmsStatus;
                        getMentorDetails.HaverelevantExp = objMentor.HaverelevantExp;
                        getMentorDetails.WillhelpifNeeded = objMentor.WillhelpifNeeded;
                        getMentorDetails.Recruited = objMentor.Recruited;
                        getMentorDetails.Interested = objMentor.Interested;
                        getMentorDetails.Conflict_FinancialInterest = objMentor.Conflict_FinancialInterest;
                        getMentorDetails.Comment = objMentor.Comment;
                        getMentorDetails.ModifiedDate = objMentor.ModifiedDate;
                        getMentorDetails.ModifiedBy = objMentor.ModifiedBy;
                        getMentorDetails.VentureName = objventure.VentureName;


                        updatementorDetails.Prefix = objmentordetail.Prefix;
                        updatementorDetails.FirstName = objmentordetail.FirstName;
                        updatementorDetails.MiddleName = objmentordetail.MiddleName;
                        updatementorDetails.LastName = objmentordetail.LastName;
                        updatementorDetails.Suffix = objmentordetail.Suffix;
                        updatementorDetails.NickName = objmentordetail.NickName;
                        updatementorDetails.Organization = objmentordetail.Organization;
                        updatementorDetails.Title = objmentordetail.Title;
                        updatementorDetails.Street1 = objmentordetail.Street1;
                        updatementorDetails.AddressLine1 = objmentordetail.AddressLine1;
                        updatementorDetails.City1 = objmentordetail.City1;
                        updatementorDetails.StateProvinceRegion1 = objmentordetail.StateProvinceRegion1;
                        updatementorDetails.ZIP1 = objmentordetail.ZIP1;
                        updatementorDetails.Country1 = objmentordetail.Country1;
                        updatementorDetails.Street2 = objmentordetail.Street2;
                        updatementorDetails.AddressLine2 = objmentordetail.AddressLine2;
                        updatementorDetails.City2 = objmentordetail.City2;
                        updatementorDetails.StateProvinceRegion2 = objmentordetail.StateProvinceRegion2;
                        updatementorDetails.ZIP2 = objmentordetail.ZIP2;
                        updatementorDetails.Country2 = objmentordetail.Country2;
                        updatementorDetails.PrimaryEmail = objmentordetail.PrimaryEmail;
                        updatementorDetails.SecondaryEmail = objmentordetail.SecondaryEmail;
                        updatementorDetails.SkypeAddress = objmentordetail.SkypeAddress;
                        updatementorDetails.TwitterAddress = objmentordetail.TwitterAddress;
                        updatementorDetails.LinkedIn = objmentordetail.LinkedIn;
                        updatementorDetails.Telephone1 = objmentordetail.Telephone1;
                        updatementorDetails.Telephone2 = objmentordetail.Telephone2;
                        updatementorDetails.Telephone3 = objmentordetail.Telephone3;
                        updatementorDetails.PublicProfile = objmentordetail.PublicProfile;
                        updatementorDetails.Resume = objmentordetail.Resume;

                        if (updatementorquestionarrie != null)
                        {
                            updatementorquestionarrie.Startups = objmentorquestionary.Startups;
                            updatementorquestionarrie.BusinessModel = objmentorquestionary.BusinessModel;
                            updatementorquestionarrie.Funding = objmentorquestionary.Funding;
                            updatementorquestionarrie.Others1 = objmentorquestionary.Others1;
                            updatementorquestionarrie.Software = objmentorquestionary.Software;
                            updatementorquestionarrie.LifeSciences = objmentorquestionary.LifeSciences;
                            updatementorquestionarrie.IndustryOther = objmentorquestionary.IndustryOther;
                            updatementorquestionarrie.Others2 = objmentorquestionary.Others2;
                            updatementorquestionarrie.FunctionalAreas = objmentorquestionary.FunctionalAreas;
                            updatementorquestionarrie.Others3 = objmentorquestionary.Others3;


                        }
                        dbContext.SaveChanges();

                        isInserted = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return isInserted;
        }

        public List<Mentor> GetMentorDetails(string Keyword, Int32 Id, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName, string roleid,string isfrom,string alphanumericsort)
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
                    List<Mentor_GetAllMentors_Result> lstMentors = dbContext.Mentor_GetAllMentors(Keyword, jtStartIndex, jtPageSize, jtSorting, output, Id, roleid, isfrom, alphanumericsort).ToList();
                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-gb");
                    //foreach (var item in lstMentors)
                    //{
                    //    if (item.EndDate == null)
                    //    {
                    //        item.EndDate = Convert.ToDateTime("");
                    //    }
                    //}
                  
                    var LstMnetors = lstMentors.Select(mentor => new Mentor
                    {
                        MentorId = mentor.MentorId.GetValueOrDefault(),
                        VentureId = mentor.VentureId.GetValueOrDefault(),
                        VentureName = mentor.VentureName,
                        Name = mentor.Name,
                        MentorType = mentor.MentorType,
                        //strStartDate = Convert.ToDateTime(mentor.StartDate).ToString("MM/dd/yyyy"),
                        //strEndDate = Convert.ToDateTime(mentor.EndDate).ToString("MM/dd/yyyy"),
                        StartDate=mentor.StartDate,
                        EndDate=mentor.EndDate,
                        //IsActive = Convert.ToBoolean(mentor.St),
                        HaverelevantExp = Convert.ToBoolean(mentor.HaverelevantExp),
                        WillhelpifNeeded = Convert.ToBoolean(mentor.WillhelpifNeeded),
                        Recruited = Convert.ToBoolean(mentor.Recruited),
                        Status = mentor.Status,
                        Interested = Convert.ToBoolean(mentor.Interested),
                        Conflict_FinancialInterest = Convert.ToBoolean(mentor.ConflictFinancialInterest),
                        Comment = mentor.Comment,
                        CreatedDate = Convert.ToDateTime(mentor.CreatedDate),
                        ModifiedDate = Convert.ToDateTime(mentor.ModifiedDate),
                        strRecruited=(mentor.Recruited!=null && mentor.Recruited=="true")?"True":"False",
                    }).ToList();
                    RecordCount = Convert.ToInt32(output.Value);
                    // RecordCount = Convert.ToInt32(LstCompanies.Count);
                    //List<Company> CompaniesList1 = dbContext.Companies.ToList();
                    //RecordCount = CompaniesList.Count;
                    return LstMnetors;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Venture> getVentureDetails(string Connectionstring, string dbName)
        {
            throw new NotImplementedException();
        }

        public List<Venture> getVenturePrefix(string Prefix, string Connectionstring, string dbName)
        {
            List<Venture> lstVenture = new List<Venture>();

            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);
                    lstVenture = dbContext.Ventures.Where(v => v.VentureName.Contains(Prefix)).ToList();
                    return lstVenture;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MentorType> getMentorTypeDetails(string Connectionstring, string dbName)
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
                    List<MentorType> lstMentorTypes = new List<MentorType>();
                    var ment = from mentortype in dbContext.MentorTypes
                              
                               select mentortype;
                    lstMentorTypes = ment.ToList();
                   
                    return lstMentorTypes;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public bool IsMentorExists(string MentorName, string Connectionstring, string dbName)
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
                    var venture = dbContext.Mentors.Where(v => v.Name == MentorName).FirstOrDefault();
                    if (venture != null) // update
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool DeleteMentorByMentorId(long MentorId, string Connectionstring, string dbName)
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
                    Mentor mentor = dbContext.Mentors.Where(men => men.MentorId == MentorId).SingleOrDefault();
                    if (mentor != null)
                    {
                        dbContext.Mentors.Remove(mentor);
                        dbContext.SaveChanges();
                        flag = true;
                    }
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int DeleteMentorByMentorIdNew(Mentor mentor, string Connectionstring, string dbName)
        {
            int isupdated = 0;
            try
            {
                using (DevEntities dbContext = new DevEntities(Connectionstring))
                {
                    if (dbContext.Database.Connection.State == ConnectionState.Closed)
                    {
                        dbContext.Database.Connection.Open();
                    }
                    dbContext.Database.Connection.ChangeDatabase(dbName);

                    mentor = dbContext.Mentors.Where(men => men.MentorId == mentor.MentorId).SingleOrDefault();
                    if (mentor != null)
                    {
                        mentor.ModifiedBy = mentor.ModifiedBy;
                        mentor.ModifiedDate = mentor.ModifiedDate;
                        dbContext.SaveChanges();
                        isupdated = Convert.ToInt32(mentor.UserId);
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
            return isupdated;
        }

        public List<Mentor> GetAllLeadMentorsList(string Connectionstring, string dbName)
        {
            try
            {

                DevEntities objentities = new DevEntities();
                objentities.Database.Connection.Open();
                objentities.Database.Connection.ChangeDatabase(dbName);
                List<Mentor> objlistmentors = new List<Mentor>();
               
                    var ment = from mentors in objentities.Mentors
                               where mentors.MentorTypeId == 4
                               select mentors;
                    objlistmentors = ment.ToList();
              

                return objlistmentors;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Int64 getMentorIdbyMentorName(string Mentorname, string Connectionstring, string dbName)
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

                    Mentor objMentor = dbContext.Mentors.Where(mentor =>mentor.Name ==Mentorname).FirstOrDefault();
                   
                
                    // Mentor objMen=dbContext.Mentors.Join(dbContext.Ventures, 
                    //s => s.VentureId,
                    //sa => sa.VentureId,(s, sa) => new { men = s, ven = sa }).Where(ssa => ssa.men.MentorId == id).Select(ssa => ssa.men).FirstOrDefault();
                    return objMentor.MentorId;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public MentorDetail GetPendingMentorDetailsByID(long id, string Connectionstring, string dbName)
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

                    MentorDetail objMentordetail = dbContext.MentorDetails.Where(mentordet => mentordet.MentorId == id).FirstOrDefault();
                    Mentor objmentor = dbContext.Mentors.Where(m => m.MentorId == id).FirstOrDefault();
                   objMentordetail.strmentCreatedDate = Convert.ToDateTime(objmentor.CreatedDate).ToString("MM/dd/yyyy");
                    return objMentordetail;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public MentorQuestionary GetMentorQuestionaryByMID(long id, string Connectionstring, string dbName)
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

                    MentorQuestionary objMentorquestionar = dbContext.MentorQuestionaries.Where(mentorques => mentorques.MentorId == id).FirstOrDefault();
                    return objMentorquestionar;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int updatePendingMentorDetails(Mentor objmentor,MentorDetail objmentordetail, MentorQuestionary objmentorquestionary,string Connectionstring, string dbName)
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
                    MentorDetail updatementorDetails = dbContext.MentorDetails.Where(c => c.MentorId == objmentordetail.MentorId).SingleOrDefault();
                    Mentor updateVMSstatusofMentor = dbContext.Mentors.Where(c => c.MentorId == objmentordetail.MentorId).SingleOrDefault();
                    MentorQuestionary updatementorquestionarrie = dbContext.MentorQuestionaries.Where(c => c.MentorId == objmentordetail.MentorId).SingleOrDefault();
                    if (updateVMSstatusofMentor.VentureId == null)
                    {
                        updateVMSstatusofMentor.VentureId = 0;
                    }
                    if (objmentordetail.FirstName == null)
                    {
                        objmentordetail.FirstName = updatementorDetails.FirstName;
                    }
                    if (objmentordetail.LastName == null)
                    {
                        objmentordetail.LastName = updatementorDetails.LastName;
                    }
                    if (objmentordetail.PrimaryEmail == null)
                    {
                        objmentordetail.PrimaryEmail = updatementorDetails.PrimaryEmail;
                    }
                    if (objmentordetail.Telephone1 == null)
                    {
                        objmentordetail.Telephone1 = updatementorDetails.Telephone1;
                    }
                    if (updatementorDetails != null)
                    {
                        updateVMSstatusofMentor.VentureId = updateVMSstatusofMentor.VentureId;
                        updateVMSstatusofMentor.Status = "Active";
                        updateVMSstatusofMentor.VmsStatus = 2;
                        updateVMSstatusofMentor.ModifiedDate = DateTime.Now;
                        updateVMSstatusofMentor.ModifiedBy = objmentor.ModifiedBy;
                        updateVMSstatusofMentor.StartDate = objmentor.StartDate;
                        updatementorDetails.Prefix = objmentordetail.Prefix;
                        updatementorDetails.FirstName = objmentordetail.FirstName;
                        updatementorDetails.MiddleName = objmentordetail.MiddleName;
                        updatementorDetails.LastName = objmentordetail.LastName;
                        updatementorDetails.Suffix = objmentordetail.Suffix;
                        updatementorDetails.NickName = objmentordetail.NickName;
                        updatementorDetails.Organization = objmentordetail.Organization;
                        updatementorDetails.Title = objmentordetail.Title;
                        updatementorDetails.Street1 = objmentordetail.Street1;
                        updatementorDetails.AddressLine1 = objmentordetail.AddressLine1;
                        updatementorDetails.City1 = objmentordetail.City1;
                        updatementorDetails.StateProvinceRegion1 = objmentordetail.StateProvinceRegion1;
                        updatementorDetails.ZIP1 = objmentordetail.ZIP1;
                        updatementorDetails.Country1 = objmentordetail.Country1;
                        updatementorDetails.Street2 = objmentordetail.Street2;
                        updatementorDetails.AddressLine2 = objmentordetail.AddressLine2;
                        updatementorDetails.City2 = objmentordetail.City2;
                        updatementorDetails.StateProvinceRegion2 = objmentordetail.StateProvinceRegion2;
                        updatementorDetails.ZIP2 = objmentordetail.ZIP2;
                        updatementorDetails.Country2 = objmentordetail.Country2;
                        updatementorDetails.PrimaryEmail = objmentordetail.PrimaryEmail;
                        updatementorDetails.SecondaryEmail = objmentordetail.SecondaryEmail;
                        updatementorDetails.SkypeAddress = objmentordetail.SkypeAddress;
                        updatementorDetails.TwitterAddress = objmentordetail.TwitterAddress;
                        updatementorDetails.LinkedIn = objmentordetail.LinkedIn;
                        updatementorDetails.Telephone1 = objmentordetail.Telephone1;
                        updatementorDetails.Telephone2 = objmentordetail.Telephone2;
                        updatementorDetails.Telephone3 = objmentordetail.Telephone3;
                        updatementorDetails.PublicProfile = objmentordetail.PublicProfile;
                        updatementorDetails.Resume = objmentordetail.Resume;
                        
                        if (updatementorquestionarrie != null)
                        {
                            updatementorquestionarrie.Startups = objmentorquestionary.Startups;
                            updatementorquestionarrie.BusinessModel = objmentorquestionary.BusinessModel;
                            updatementorquestionarrie.Funding = objmentorquestionary.Funding;
                            updatementorquestionarrie.Others1 = objmentorquestionary.Others1;
                            updatementorquestionarrie.Software = objmentorquestionary.Software;
                            updatementorquestionarrie.LifeSciences = objmentorquestionary.LifeSciences;
                            updatementorquestionarrie.IndustryOther = objmentorquestionary.IndustryOther;
                            updatementorquestionarrie.Others2 = objmentorquestionary.Others2;
                            updatementorquestionarrie.FunctionalAreas = objmentorquestionary.FunctionalAreas;
                            updatementorquestionarrie.Others3 = objmentorquestionary.Others3;
                            

                        }
                        dbContext.SaveChanges();
                        return Convert.ToInt32(updateVMSstatusofMentor.UserId);
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
