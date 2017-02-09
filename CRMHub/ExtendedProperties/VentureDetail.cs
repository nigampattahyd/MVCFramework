using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(VentureDetailMetaData))]
    public partial class VentureDetail
    {
       // public Nullable<System.DateTime> CreatedDate { get; set; }
        public string strVenCreatedDate { get; set; }
        public string PrimaryContact { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryContact { get; set; }
        public string SecondaryEmail { get; set; }
        public bool LegalEntity { get; set; }
        public string Comments { get; set; }
    }
    public class CollabP
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
    public class CollabS
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
    public class Advertisement
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }

    public class CurrentStatus
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
    public class VMShelp
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }

    public class postedcollabP
    {
        public string[] postedcollabpId { get; set; }
    }
    public class Category
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
    }
    public partial class VentureDetailMetaData
    {
        public long VentureDetailsId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public string IndustryCategoryName { get; set; }
        public string CategoryOthers { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string PrimaryRoleInVenture { get; set; }
        public string PrimaryColaborationAffiliation { get; set; }
        public string SecondaryPhoneNo { get; set; }
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
        public Nullable<long> VentureId { get; set; }
        
    }
}
