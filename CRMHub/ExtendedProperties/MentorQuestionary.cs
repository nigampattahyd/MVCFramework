using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
     [MetadataType(typeof(MentorQuestionaryMetaData))]
   public partial class MentorQuestionary
    {
    }

    public partial class MentorQuestionaryMetaData
    {
        public long MentorQuestionaryId { get; set; }
        public string Startups { get; set; }
        public string BusinessModel { get; set; }
        public string Funding { get; set; }
        public string Others1 { get; set; }
        public string Software { get; set; }
        public string LifeSciences { get; set; }
        public string IndustryOther { get; set; }
        public string Others2 { get; set; }
        public string FunctionalAreas { get; set; }
        public string Others3 { get; set; }
        public Nullable<long> MentorId { get; set; }
    }
}
