using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRMHub.EntityFramework
{
    

    [MetadataType(typeof(AccountDocumentMetaData))]
    public partial class AccountDocument
    {

    }


    public partial class AccountDocumentMetaData
    {
        public long DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public Nullable<int> AccountId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
