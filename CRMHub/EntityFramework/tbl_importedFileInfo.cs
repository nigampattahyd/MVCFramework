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
    
    public partial class tbl_importedFileInfo
    {
        public int id { get; set; }
        public string FileType { get; set; }
        public Nullable<System.DateTime> DateImported { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Filename { get; set; }
        public string importedby { get; set; }
    }
}
