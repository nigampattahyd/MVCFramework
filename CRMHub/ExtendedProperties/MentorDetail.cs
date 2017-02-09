using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
   [MetadataType(typeof(MentorDetailMetaData))]
   public partial class MentorDetail
    {
       public string ResumeName { get; set; }
       public string strmentCreatedDate { get; set; }
    }

   public class MQStartUps
   {
       public int ID { get; set; }
       public string Value { get; set; }
       public string Text { get; set; }
       public bool IsChecked { get; set; }
   }
   public class MQBusinessModel
   {
       public int ID { get; set; }
       public string Value { get; set; }
       public string Text { get; set; }
       public bool IsChecked { get; set; }
   }
   public class MQFunding
   {
       public int ID { get; set; }
       public string Value { get; set; }
       public string Text { get; set; }
       public bool IsChecked { get; set; }
   }

   
   public class MQSoftware
   {
       public int ID { get; set; }
       public string Value { get; set; }
       public string Text { get; set; }
       public bool IsChecked { get; set; }
   }
   public class MQLifeSciences
   {
       public int ID { get; set; }
       public string Value { get; set; }
       public string Text { get; set; }
       public bool IsChecked { get; set; }
   }
   public class MQIndustryOther
   {
       public int ID { get; set; }
       public string Value { get; set; }
       public string Text { get; set; }
       public bool IsChecked { get; set; }
   }
   public class MQFunctionalAreas
   {
       public int ID { get; set; }
       public string Value { get; set; }
       public string Text { get; set; }
       public bool IsChecked { get; set; }
   }
 
    public partial class MentorDetailMetaData
    {
        public long MentorDetailsId { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string NickName { get; set; }
        public string Organization { get; set; }
        public string Title { get; set; }
        public string Street1 { get; set; }
        public string AddressLine1 { get; set; }
        public string City1 { get; set; }
        public string StateProvinceRegion1 { get; set; }
        public string ZIP1 { get; set; }
        public string Country1 { get; set; }
        public string Street2 { get; set; }
        public string AddressLine2 { get; set; }
        public string City2 { get; set; }
        public string StateProvinceRegion2 { get; set; }
        public string ZIP2 { get; set; }
        public string Country2 { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string SkypeAddress { get; set; }
        public string TwitterAddress { get; set; }
        public string LinkedIn { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Telephone3 { get; set; }
        public string PublicProfile { get; set; }
        public string Resume { get; set; }
        public Nullable<bool> QuestionStat { get; set; }
        public Nullable<long> MentorId { get; set; }

    }
}
