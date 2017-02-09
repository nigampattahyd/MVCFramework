using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(MentorMetaData))]
    public partial class Mentor
    {
        
        public string VentureName { get; set; }
        public string MentorType { get; set; }
        public string Status { get; set; }
        public string Keyword { get; set; }
        public bool isRecruited { get; set; }
        public bool isHaverelevantExp { get; set; }
        public bool isWillhelpifNeeded { get; set; }
        public bool isInterested { get; set; }
        public bool isConflictFinancialInterest { get; set; }
        public bool Active { get; set; }
        public string strRecruited { get; set; }
        public string strHaverelevantExp { get; set; }
        public string strWillhelpifNeeded { get; set; }
        public string strInterested { get; set; }
        public string strConflictFinancialInterest { get; set; }
        public string strStartDate { get; set; }
        public string strEndDate { get; set; }
        public string Password { get; set; }
       
       
    }

    public partial class MentorMetaData
    {

        public long MentorId { get; set; }
        public Nullable<long> VentureId { get; set; }
        [Remote("IsMentorExists", "Mentor", ErrorMessage = "Mentor already exists")]
        public string Name { get; set; }
        public Nullable<int> MentorTypeId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<bool> HaverelevantExp { get; set; }
        public Nullable<bool> WillhelpifNeeded { get; set; }
        public Nullable<bool> Recruited { get; set; }
        public Nullable<bool> Interested { get; set; }
        public Nullable<bool> Conflict_FinancialInterest { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> UserId { get; set; }
        [Required]
        [Remote("IsEmailExists", "Mentor", AdditionalFields = "Email", ErrorMessage = "EmailId already exists")]
        public string Email { get; set; }
        public Nullable<int> VmsStatus { get; set; }
    }

}