using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            MenusList = new List<tbl_Menus>();
            
        }
       public tbl_Menus MenusEntity { get; set; }
       public List<tbl_Menus> MenusList { get; set; } 
    }
}
