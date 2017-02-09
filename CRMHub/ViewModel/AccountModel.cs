using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRMHub.ViewModel
{
    public class AccountModel
    {
        public AccountModel()
        {
            listCustomfields = new List<Custom_Manage_Master>();
            lstcustomVM = new List<Custom_Value_Master>();
            AccoutnList = new List<Account>();
        }
        public List<Custom_Value_Master> lstcustomVM { get; set; }
        public List<Custom_Manage_Master> listCustomfields { get; set; }
        public List<Account> AccoutnList { get; set; }

        public Account AccountObj { get; set; }

    }
}
