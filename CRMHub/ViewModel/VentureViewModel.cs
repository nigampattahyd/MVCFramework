using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class VentureViewModel
    {
        public VentureViewModel()
        {
            listCustomfields = new List<Custom_Manage_Master>();
            lstcustomVM = new List<Custom_Value_Master>();
            lstCustomOptions = new List<Custom_DrpChkValues>();
            lstVenture = new List<Venture>();
            lstMVSStatus = new List<VMSStatu>();
            objlstmentor = new List<Mentor>();
            ListPrimaryColaborationAffiliation = new List<CollabP>();
            ListSecondaryColaborationAffiliation = new List<CollabS>();
            ListofAdvertisement = new List<Advertisement>();
            Listofstatus = new List<CurrentStatus>();
            ListofVMShelp = new List<VMShelp>();
            listofCategory = new List<Category>();
        }

        public List<Custom_Manage_Master> listCustomfields { get; set; }
        public List<Custom_Value_Master> lstcustomVM { get; set; }
        public List<Custom_DrpChkValues> lstCustomOptions { get; set; }
        public List<Venture> lstVenture { get; set; }
        public Venture objVenture { get; set; }
        public List<VMSStatu> lstMVSStatus { get; set; }
        public Custom_Value_Master customVM { get; set; }
        public Custom_DrpChkValues CustomDrpObj { get; set; }
        public List<Mentor> objlstmentor { get; set; }
         public List<CollabP> ListPrimaryColaborationAffiliation { get; set; }
        public List<CollabS> ListSecondaryColaborationAffiliation { get; set; }
        public List<Advertisement> ListofAdvertisement { get; set; }
        public List<CurrentStatus> Listofstatus { get; set; }
        public List<VMShelp> ListofVMShelp { get; set; }
        public VentureDetail objVenturedetail { get; set; }
        public List<Category> listofCategory { get; set; }

     
    }











    
}
