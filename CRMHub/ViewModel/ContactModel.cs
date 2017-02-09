using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class ContactModel
    {
        public ContactModel()
        {
            TypeList = new List<activity_types>();
            userlist = new List<user>();
            listCustomfields = new List<Custom_Manage_Master>();
            lstcustomVM = new List<Custom_Value_Master>();
            Lstcontacttype = new List<tbl_ContactType>();
        }
        public List<Custom_Value_Master> lstcustomVM { get; set; }
        public List<Custom_Manage_Master> listCustomfields { get; set; }
        public List<tbl_Contact> listContact { get; set; }

        public tbl_Contact Contact { get; set; }

        public List<Branch> BranchList { get; set; }

        public Branch branch { get; set; }

        public user user { get; set; }

        public List<user> listUsers { get; set; }

        public List<SMSProviderList> smsProviderList { get; set; }

        public List<usState> stateList { get; set; }

        public List<tbl_history> historylist { get; set; }

        public tbl_history historydetails { get; set; }

        public List<activity_types> TypeList { get; set; }

        public List<user> userlist { get; set; }

        public List<OutlookMail> mailList { get; set; }

        public OutlookMail maildetails { get; set; }

        public tbl_Contact Contact1 { get; set; }

        public tbl_Contact Contact2 { get; set; }

        public Custom_Value_Master customVM { get; set; }


        public tbl_ContactType contacttype { get; set; }
        public List<tbl_ContactType> Lstcontacttype { get; set; }
    }
}
