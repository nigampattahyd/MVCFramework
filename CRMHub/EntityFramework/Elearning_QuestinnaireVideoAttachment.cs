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
    
    public partial class Elearning_QuestinnaireVideoAttachment
    {
        public long AttachmentId { get; set; }
        public string QuestionnaireId { get; set; }
        public long VideoId { get; set; }
        public System.DateTime AttachedDate { get; set; }
        public string Extension { get; set; }
    }
}
