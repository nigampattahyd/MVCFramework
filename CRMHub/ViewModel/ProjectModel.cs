using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class ProjectModel
    {
        //public tbl_ProjectDetails project { get; set; }    
        //public List<tbl_Contact> contactList { get; set; }
        //public tbl_Contact Contact { get; set; }
        public ProjectModel()
        {
            LstprojectDetails = new List<tbl_ProjectDetails>();
            ProjectDetails = new tbl_ProjectDetails();
        }

        public List<tbl_ProjectDetails> LstprojectDetails { get; set; }
        public tbl_ProjectDetails ProjectDetails { get; set; }
    }
}
