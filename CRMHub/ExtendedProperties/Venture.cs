using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(VentureMetaData))]
    public partial class Venture
    {
        [Required]
        public string Status { get; set; }
        public string Keyword { get; set; }
        [DisplayName("IsEnrolled")]
        public bool Enrolled { get; set; }
        public bool StudentIdea { get; set; }
        public bool ResearchIdea { get; set; }
        public bool LegalEntity { get; set; }
        public bool IsIntroduced { get; set; }
        public bool Active { get; set; }
        public string password { get; set; }
        public string strVenCreatedDate { get; set; }
        public string strModifiedDate { get; set; }
        public string strActiveDate { get; set; }
        public string strInactiveDate { get; set; }
        public Int64 totcount { get; set; }



        public long VentureDetailsId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public string IndustryCategoryName { get; set; }
        public string CategoryOthers { get; set; }
        
        public string PrimaryRoleInVenture { get; set; }
        public string PrimaryColaborationAffiliation { get; set; }
       
        public string SecondaryRoleInVenture { get; set; }
        public string ScondaryColaborationAffiliation { get; set; }
        public string Advertisement { get; set; }
        public string WouldLikeToElaborateSpecifics { get; set; }
        public string WhatDoYouDo { get; set; }
        public string WhatIsTheProblem { get; set; }
        public string WhoHasIt { get; set; }
        public string HowWillUSolveTheProblem { get; set; }
        public string HowWillUMakeMoney { get; set; }
        public string WhtIsTheSecretSauce { get; set; }
        public string IsThereIP { get; set; }
        public string WhatIsYourCurrentStatus { get; set; }
        public string WhatTypeOfVMSHelpStartUpNeeds { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string SecondaryPhoneNo { get; set; }
      

    }

    public partial class VentureMetaData
    {
        public long VentureId { get; set; }
        [Remote("IsVentureExists", "Venture", AdditionalFields = "VentureName", ErrorMessage = "Venture already exists")]
        public string VentureName { get; set; }
        [Required]
        public string LeadMentor { get; set; }
        public Nullable<int> VmsStatusId { get; set; }
        [Required]
        public string PrimaryContact { get; set; }
        [Required]
        //[EmailAddress(ErrorMessage = "E-mail is not valid")]
        [Remote("IsEmailExists", "Venture", AdditionalFields = "PrimaryEmail", ErrorMessage = "EmailId already exists")]
        public string PrimaryEmail { get; set; }
        public string SecondaryContact { get; set; }
        //[EmailAddress(ErrorMessage = "E-mail is not valid")]
        public string SecondaryEmail { get; set; }
        public string DropBox { get; set; }
        public string Description { get; set; }
        public string DropBoxFolder { get; set; }
        public string PublicDesc { get; set; }
        public string ContactInfo { get; set; }
        public string VmsMailList { get; set; }
        public string SignUpReturned { get; set; }
    
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Cellular { get; set; }
        public string WebSite { get; set; }
        public string MITLink { get; set; }
        public Nullable<bool> StudentIdeaBasedOnResearch { get; set; }
        public Nullable<bool> ResearchIdeaDisclosedToTLO { get; set; }
        public Nullable<bool> LegalEntityAtEnrollment { get; set; }
        public string RefferedBy { get; set; }
        public string FirstMentorMeeting { get; set; }
        public string Comments { get; set; }
        
        public string IntakeNotes { get; set; }
        public string IntakeNoteSummary { get; set; }
        public string SecondContact { get; set; }
        public Nullable<bool> Introduced { get; set; }
        public string SeachIndex { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> CreatedBY { get; set; }
        public Nullable<int> ModifiedBY { get; set; }
        public Nullable<int> UserId { get; set; }
    }
}
