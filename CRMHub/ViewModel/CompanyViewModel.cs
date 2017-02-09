using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMHub.EntityFramework;
using CRMHub.Repository;

namespace CRMHub.ViewModel
{
    public class CompanyViewModel
    {

        public CompanyViewModel()
        {
            listCustomfields = new List<Custom_Manage_Master>();
            lstcustomVM = new List<Custom_Value_Master>();
            lstCustomOptions = new List<Custom_DrpChkValues>();
            LstCompany = new List<Company>();
        }
        public List<Custom_Manage_Master> listCustomfields { get; set; }
        public List<Custom_Value_Master> lstcustomVM { get; set; }
        //public List<Custom_DrpChkValues> lstCustomDrp { get; set; }
        public List<Custom_DrpChkValues> lstCustomOptions { get; set; }
        public List<Company> LstCompany { get; set; }
        public Company CompanyObj { get; set; }
        public Custom_Value_Master customVM { get; set; }
        public Custom_DrpChkValues CustomDrpObj { get; set; }

    }
}
