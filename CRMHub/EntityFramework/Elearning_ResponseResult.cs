//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRMHub.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Elearning_ResponseResult
    {
        public long ResponseResultId { get; set; }
        public Nullable<long> ResponseId { get; set; }
        public Nullable<long> ContactId { get; set; }
        public string email_address { get; set; }
        public string SurveyId { get; set; }
        public Nullable<long> TotalQNum { get; set; }
        public Nullable<long> TotalAnsQ { get; set; }
        public Nullable<long> TotalCAns { get; set; }
        public Nullable<long> MarksObtained { get; set; }
        public Nullable<long> TotalMarks { get; set; }
        public Nullable<long> Percentage { get; set; }
        public Nullable<long> Passing_Percentage { get; set; }
        public string Result { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsAttemptedQuestionnaire { get; set; }
    }
}
