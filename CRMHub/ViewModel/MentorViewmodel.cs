using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class MentorViewmodel
    {
        public MentorViewmodel()
        {
            listCustomfields = new List<Custom_Manage_Master>();
            lstcustomVM = new List<Custom_Value_Master>();
            lstCustomOptions = new List<Custom_DrpChkValues>();
            lstMentor = new List<Mentor>();
            lstVenture = new List<Venture>();
            lstMentorType = new List<MentorType>();
            lstMVSStatus = new List<VMSStatu>();
            lststate = new List<usState>();
            lstcountry = new List<Country>();
            listofMQStartUps = new List<MQStartUps>();
            listofMQBusinessModel = new List<MQBusinessModel>();
            listofMQFunding = new List<MQFunding>();
            listofMQSoftware = new List<MQSoftware>();
            listofMQLifeSciences = new List<MQLifeSciences>();
            listofMQIndustryOther = new List<MQIndustryOther>();
            listofMQFunctionalAreas = new List<MQFunctionalAreas>();
        }

        public List<Custom_Manage_Master> listCustomfields { get; set; }
        public List<Custom_Value_Master> lstcustomVM { get; set; }
        public List<Custom_DrpChkValues> lstCustomOptions { get; set; }
        public List<Venture> lstVenture { get; set; }
        public List<Mentor> lstMentor { get; set; }
        public Mentor objMentor { get; set; }
        public List<MentorType> lstMentorType { get; set; }
        public Custom_Value_Master customVM { get; set; }
        public Custom_DrpChkValues CustomDrpObj { get; set; }
        public List<VMSStatu> lstMVSStatus { get; set; }
        public MentorDetail objmentordetail { get; set; }
        public MentorQuestionary objmentorquestionnary { get; set; }
        public List<MentorDetail> lstMentordetail { get; set; }
        public List<MentorQuestionary> lstMentorQuestionary { get; set; }
        public List<usState> lststate { get; set; }
        public List<Country> lstcountry { get; set; }
        public List<MQStartUps> listofMQStartUps { get; set; }
        public List<MQBusinessModel> listofMQBusinessModel { get; set; }
        public List<MQFunding> listofMQFunding { get; set; }
        public List<MQSoftware> listofMQSoftware { get; set; }
        public List<MQLifeSciences> listofMQLifeSciences { get; set; }
        public List<MQIndustryOther> listofMQIndustryOther { get; set; }
        public List<MQFunctionalAreas> listofMQFunctionalAreas { get; set; }
       
    }
}
