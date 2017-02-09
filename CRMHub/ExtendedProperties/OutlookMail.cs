using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(OutlookMailMetaData))]
    public partial class OutlookMail
    {
        public DateTime? testdate { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        
        public List<user> UserList { get; set; }
        public string SalesRepresentative { get; set; }
    }

    public partial class OutlookMailMetaData
    {
        public long ID { get; set; }
        public string Folder { get; set; }
        public string CreatedBy { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        public string SentDatetime { get; set; }
        public string RecievedDateTime { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Flag { get; set; }

    }
}
