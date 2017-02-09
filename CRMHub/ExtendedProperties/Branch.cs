using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(BranchMetaData))]
    public partial class Branch
    {
        public string StateName { get; set; }
        public string Keyword { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string branchIdString { get; set; }
        public List<Branch> BranchesList { get; set; }
    }

    public partial class BranchMetaData
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress1 { get; set; }
        public string BranchAddress2 { get; set; }
        public string BranchCity { get; set; }
        public string BranchStateCode { get; set; }
        public string BranchZip { get; set; }
        public string BranchPhone { get; set; }
        public string BranchPhoneExt { get; set; }
        public string BranchFax { get; set; }
        public string BranchEmail { get; set; }

        [AllowHtml]
        public string Comment { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string StateOther { get; set; }
        public string RoutingEmail { get; set; }
        public string BranchType { get; set; }
    }
}
