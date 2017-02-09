using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Mentoring.Api.Controllers
{
    public class VentureDetailsController : ApiController
    {
        private readonly IVenture _iVenture = null;
        private readonly IErrorLogRepository _ierrorlogrepository;
        private readonly IClientRepository _iclientrepository;
      
        public VentureDetailsController()
        {
            _iVenture = new VentureRepository();
            _ierrorlogrepository = new ErrorLogRepository();
            _iclientrepository = new ClientRepository();
            
        }

        public string Post(JObject objData)
        {
            try
            {
                dynamic jsonData = objData;
                string collabp1 = "", collabs1 = "", advertise1 = "", currentstatus1 = "", VMSHelp1 = "";
                JObject venturedetailJson = jsonData.venturedetail;
                JObject ventureJson = jsonData.objVenture;
                var collabP = venturedetailJson["PrimaryColaborationAffiliation"];
                var collabS = venturedetailJson["ScondaryColaborationAffiliation"];
                var advertise = venturedetailJson["Advertisement"];
                var currentstatus = venturedetailJson["WhatIsYourCurrentStatus"];
                var VMSHelp = venturedetailJson["WhatTypeOfVMSHelpStartUpNeeds"];
                var enrollment = ventureJson["LegalEntityAtEnrollment"];
                // DataTable dtcoll = InsertPoDetailsDatatable(collab.ToString());

                var venture = ventureJson.ToObject<Venture>();
                venture.LegalEntityAtEnrollment = Convert.ToBoolean(enrollment);

                var venturedetails = venturedetailJson.ToObject<VentureDetail>();

                dynamic Pcollabobj = JsonConvert.DeserializeObject<dynamic>(collabP.ToString());
                dynamic Scollabobj = JsonConvert.DeserializeObject<dynamic>(collabS.ToString());
                dynamic advertiseobj = JsonConvert.DeserializeObject<dynamic>(advertise.ToString());
                dynamic currentstatusobj = JsonConvert.DeserializeObject<dynamic>(currentstatus.ToString());
                dynamic VMSHelpobj = JsonConvert.DeserializeObject<dynamic>(VMSHelp.ToString());
                if (Pcollabobj != null)
                {
                    foreach (var item in Pcollabobj)
                    {

                        collabp1 = collabp1 + "," + item.Value;
                        collabp1 = collabp1.Remove(0, 1);
                    }
                }
                else
                {
                    collabp1 = "";
                }

                venturedetails.PrimaryColaborationAffiliation = collabp1.ToString();
                if (Scollabobj != null)
                {
                    foreach (var item in Scollabobj)
                    {
                        collabs1 = collabs1 + "," + item.Value;
                        collabs1 = collabs1.Remove(0, 1);
                    }
                }
                else
                {
                    collabs1 = "";
                }

                venturedetails.ScondaryColaborationAffiliation = collabs1.ToString();

                if (advertiseobj != null)
                {
                    foreach (var item in advertiseobj)
                    {
                        advertise1 = advertise1 + "," + item.Value;
                        advertise1 = advertise1.Remove(0, 1);
                    }
                }
                else
                {
                    advertise1 = "";
                }

                venturedetails.Advertisement = advertise1.ToString();

                if (currentstatusobj != null)
                {
                    foreach (var item in currentstatusobj)
                    {
                        currentstatus1 = currentstatus1 + "," + item.Value;
                        currentstatus1 = currentstatus1.Remove(0, 1);
                    }
                }
                else
                {
                    currentstatus1 = "";
                }

                venturedetails.WhatIsYourCurrentStatus = currentstatus1.ToString();
                if (VMSHelpobj != null)
                {
                    foreach (var item in VMSHelpobj)
                    {
                        VMSHelp1 = VMSHelp1 + "," + item.Value;
                        VMSHelp1 = VMSHelp1.Remove(0, 1);
                    }
                }
                else
                {
                    VMSHelp1 = "";
                }

                venturedetails.WhatTypeOfVMSHelpStartUpNeeds = VMSHelp1.ToString();

                string Client = "mentor";
                Client objclienttype = _iclientrepository.GetSiteTypeByClientID(Client);
                string ConnectionString = objclienttype.ConnectionString.ToString();
                var details = _iVenture.InsertVentureDetails(venture, venturedetails, ConnectionString, Client);
                //var response = Request.CreateResponse(System.Net.HttpStatusCode.Created, objData);
                //var msg = Request.CreateResponse(HttpStatusCode.Created, details);
                //msg.Headers.Location = new Uri(Request.RequestUri + details.ToString());
                return "success";
            }
            catch (Exception ex)
            {
                return "Fail";
            }

        }

        public string getname(string name)
        {
            return name;
        }
    }
}
