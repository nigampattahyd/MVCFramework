using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
   public class SalesModel
    {
       public SalesModel()
       {
          salesList = new List<tbl_Sales>();
          accountsList = new List<tbl_account>();
          userList = new List<user>();
          branchList = new List<Branch>();
          listCustomfields = new List<Custom_Manage_Master>();
          customVM = new Custom_Value_Master();
          lstcustomVM = new List<Custom_Value_Master>();
       }
       public List<Custom_Manage_Master> listCustomfields { get; set; }
       public tbl_Sales SalesEntity { get; set; }
       public List<tbl_Sales> salesList { get; set; }
       public user user { get; set; }
       public List<user> userList { get; set; }            
       public List<tbl_account> accountsList { get; set; }
       public Branch branch { get; set; }
       public List<Branch> branchList { get; set; }
       public Custom_Value_Master customVM { get; set; }
       public List<Custom_Value_Master> lstcustomVM { get; set; }
    }
}
