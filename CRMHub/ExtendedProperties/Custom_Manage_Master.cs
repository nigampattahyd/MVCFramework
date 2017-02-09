using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(Custom_Manage_MasterMetadata))]
    public partial class Custom_Manage_Master
    {
        public Custom_Manage_Master()
        {
            lstCustomOptions = new List<Custom_DrpChkValues>();
        }
        public List<Custom_DrpChkValues> lstCustomOptions { get; set; }
    }
    public partial class Custom_Manage_MasterMetadata
    {
        public long FieldId { get; set; }
        public long UserID { get; set; }
        public string Module { get; set; }
        [Display(Name = "Column Id")]
        public int Column_Id { get; set; }
        [Display(Name = "Actual Column Name")]
        public string ActualColumnName { get; set; }
        [Display(Name = "Column Label")]
        public string Column_Label { get; set; }
        [Display(Name = "Column Type")]
        public string Column_Type { get; set; }
        public string Field_Type { get; set; }
        [Display(Name = "Description")]
        public string Column_Description { get; set; }
        public string HoverText { get; set; }
        public string InputDataType { get; set; }
        public string DefaultValue { get; set; }
        public Nullable<int> DisplayPosition { get; set; }
        public Nullable<int> MaxLength { get; set; }
        public Nullable<bool> RequiredField { get; set; }
        public Nullable<bool> MultiValued { get; set; }
        public string ColumnDrpChkValues { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
        public bool IsShowInJoinNow { get; set; }
    }
}




