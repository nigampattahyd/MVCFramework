using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class MenuModel
    {
        public string text { get; set; }
        public int id { get; set; }
        public MenuModelAttribute state { get; set; }
        public List<MenuModel> children { get; set; }
    }

    public class MenuModelAttribute
    {
        public Boolean opened { get; set; }
        public Boolean disabled { get; set; }
        public Boolean selected { get; set; }
    }
}
