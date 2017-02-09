

using CRMHub.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.ViewModel
{
    public class DBoardViewModel
    {
        public DBoardViewModel()
        {
            lstContent = new List<Content>();
        }
        public List<Content> lstContent { get; set; }
    }
}
