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
    
    public partial class tbl_CustomFields
    {
        public long FieldID { get; set; }
        public int CompanyID { get; set; }
        public string ProgramType { get; set; }
        public string FormCaption { get; set; }
        public string GridCaption { get; set; }
        public string DataType { get; set; }
        public int DataFieldLength { get; set; }
        public int DataDecialPlace { get; set; }
        public bool Required { get; set; }
        public bool Visible { get; set; }
    }
}
