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
    
    public partial class Elearning_SurveyResponse
    {
        public long SurveyResponseId { get; set; }
        public string SurveyId { get; set; }
        public string UserId { get; set; }
        public string QuesId { get; set; }
        public Nullable<int> QTypeId { get; set; }
        public Nullable<int> RNum { get; set; }
        public Nullable<int> CNum { get; set; }
        public string SubCItem { get; set; }
        public string CollectorId { get; set; }
        public string TextAns { get; set; }
        public string Other { get; set; }
        public string Comment { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public Nullable<long> ResponseId { get; set; }
    }
}