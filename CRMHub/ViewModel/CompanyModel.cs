using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Repository;

namespace CRMHub.ViewModel
{
    public class CompanyModel
    {
        public CompanyModel()
        {            
            listCustomfields = new List<Custom_Manage_Master>();
            lstcustomVM = new List<Custom_Value_Master>();
            lstCustomOptions = new List<Custom_DrpChkValues>();
            TypeList = new List<activity_types>();

        }
        public List<Custom_Manage_Master> listCustomfields { get; set; }

        public tbl_account compnay { get; set; }        
        public List<tbl_account> companyList { get; set; }
        public List<tbl_Contact> contactList{get;set;}
        public List<tbl_crm_industry> industryList { get; set; }
        public List<usState> stateList { get; set; }
        public List<user> salesList { get; set; }
        public List<Branch> branchList { get; set; }
        public List<tbl_account> lstParentComp { get; set; }        
        public List<tbl_account> lstGetAllCompany { get; set; }
        public List<tbl_Contact> lstGetAllCompanyContacts { get; set; }
        public AccountDocument acntDocument { get; set; }
        public List<AccountDocument> lstAcntDocumnt { get; set; }
        public List<tbl_history> lstCompanyActivity { get; set; }
        public tbl_history history { get; set; }
       // public List<Custom_Manage_Master> listCustomfields { get; set; }
        public Custom_Value_Master customVM { get; set; }
        public List<Custom_Value_Master> lstcustomVM { get; set; }
        public List<Custom_DrpChkValues> lstCustomDrp { get; set; }
        public Custom_DrpChkValues CustomDrpObj { get; set; }

        public List<Custom_DrpChkValues> lstCustomOptions { get; set; }
        public List<activity_types> TypeList { get; set; }
    }
}
