using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CRMHub.EntityFramework
{
    [MetadataType(typeof(StateMetaData))]
    public partial class State
    {
        public string FullStateName { get; set; }
    }

    public partial class StateMetaData
    {
        public int ID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string StateName { get; set; }
        public string Abbreviation { get; set; }
    }
}
