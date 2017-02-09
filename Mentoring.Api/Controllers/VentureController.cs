using CRM.Helper;
using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mentoring.Api.Controllers
{
    [RoutePrefix("Venture")]
    public class VentureController : ApiController
    {
        [HttpGet]
        [Route("x")]
        public string Name()
        {
            return "Hello";
        }

        [HttpPost]
        [Route("Insert")]
        public string Post(string x, string y)
        {
            return x + y;
        }

        [HttpPost]
        [Route("Submit")]
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
                if (collabP.ToString() != "[]" && collabP != null)
                {
                    foreach (var item in Pcollabobj)
                    {

                        collabp1 = collabp1 + "," + item.Value;

                    }
                }
                else
                {
                    collabp1 = ",";
                }
                collabp1 = collabp1.Remove(0, 1);
                venturedetails.PrimaryColaborationAffiliation = collabp1.ToString();
                if (collabS.ToString() != "[]" && collabS != null)
                {
                    foreach (var item in Scollabobj)
                    {
                        collabs1 = collabs1 + "," + item.Value;

                    }
                }
                else
                {
                    collabs1 = ",";
                }
                collabs1 = collabs1.Remove(0, 1);
                venturedetails.ScondaryColaborationAffiliation = collabs1.ToString();

                if (advertise.ToString() != "[]" && advertise != null)
                {
                    foreach (var item in advertiseobj)
                    {
                        advertise1 = advertise1 + "," + item.Value;

                    }
                }
                else
                {
                    advertise1 = ",";
                }
                advertise1 = advertise1.Remove(0, 1);
                venturedetails.Advertisement = advertise1.ToString();

                if (currentstatusobj.ToString() != "[]" && currentstatusobj != null)
                {
                    foreach (var item in currentstatusobj)
                    {
                        currentstatus1 = currentstatus1 + "," + item.Value;

                    }
                }
                else
                {
                    currentstatus1 = ",";
                }
                currentstatus1 = currentstatus1.Remove(0, 1);
                venturedetails.WhatIsYourCurrentStatus = currentstatus1.ToString();

                if (VMSHelp.ToString() != "[]" && VMSHelp != null)
                {
                    foreach (var item in VMSHelpobj)
                    {
                        VMSHelp1 = VMSHelp1 + "," + item.Value;

                    }
                }
                else
                {
                    VMSHelp1 = ",";
                }
                VMSHelp1 = VMSHelp1.Remove(0, 1);
                venturedetails.WhatTypeOfVMSHelpStartUpNeeds = VMSHelp1.ToString();
                //ClientRepository repoClient = new ClientRepository();
                //VentureRepository repoVenture = new VentureRepository();
                string Client = "mentor";
                //Client objclienttype = repoClient.GetSiteTypeByClientID(Client);
                //string ConnectionString = objclienttype.ConnectionString.ToString();
                Int64 details = InsertVentureDetails(venture, venturedetails, Client);
                //var response = Request.CreateResponse(System.Net.HttpStatusCode.Created, objData);
                //var msg = Request.CreateResponse(HttpStatusCode.Created, details);
                //msg.Headers.Location = new Uri(Request.RequestUri + details.ToString());

                SendEmailToDigital55UsersForVentureApplication(venture.PrimaryEmail, "", venture.VentureName, details, 2);
                DataTable dt = new DataTable();
                dt = GetAdminbasedonRoleId();
                user users = new user();
                if (dt != null && dt.Rows.Count > 0)
                {
                    //foreach (DataRow item in dt.Rows)
                    //{
                    //users.RoleId = Convert.ToInt64(item["RoleId"]);
                    //users.FirstName = item["FirstName"].ToString();
                    //users.LastName = item["LastName"].ToString();
                    //users.Email = item["Email"].ToString();

                    users.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                    users.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                    users.Email = Convert.ToString(dt.Rows[0]["Email"]);
                    SendEmailToDigital55UsersForVentureApplication(venture.PrimaryEmail, users.Email, venture.VentureName, users.UserId, 1);
                }
                if (details > 0)

                    return "success";
                else
                    return "Already Exists!";
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                return "Fail";
            }

        }

        public Int64 InsertVentureDetails(Venture venture, VentureDetail venturedetails, string dbName)
        {
            int isInserted = 0;
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["ApiConStr"].ConnectionString;

                string Password = string.Empty;
                if (!string.IsNullOrEmpty(venture.PrimaryEmail))
                {
                    string[] split = venture.PrimaryEmail.Split('@');
                    if (split != null && split.Length > 0)
                    {
                        Password = Convert.ToString(split[0]);
                    }
                }
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = new SqlCommand("Venture_CreateVentureDetails", con))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VentureName", venture.VentureName);
                        cmd.Parameters.AddWithValue("@VmsStatusId", 1);
                        cmd.Parameters.AddWithValue("@PrimaryContact", venture.PrimaryContact);
                        cmd.Parameters.AddWithValue("@PrimaryEmail", venture.PrimaryEmail);
                        cmd.Parameters.AddWithValue("@SecondaryContact", venture.SecondaryContact);
                        cmd.Parameters.AddWithValue("@SecondaryEmail", venture.SecondaryEmail);
                        cmd.Parameters.AddWithValue("@LegalEntityAtEnrollment", venture.LegalEntityAtEnrollment);
                        cmd.Parameters.AddWithValue("@CreatedDate", venture.CreatedDate);
                        cmd.Parameters.AddWithValue("@Comments", venture.Comments);
                        cmd.Parameters.AddWithValue("@Street", venturedetails.Street);
                        cmd.Parameters.AddWithValue("@City", venturedetails.City);
                        cmd.Parameters.AddWithValue("@State", venturedetails.State);
                        cmd.Parameters.AddWithValue("@ZIP", venturedetails.ZIP);
                        cmd.Parameters.AddWithValue("@IndustryCategoryName", venturedetails.IndustryCategoryName);
                        cmd.Parameters.AddWithValue("@CategoryOthers", venturedetails.CategoryOthers);
                        cmd.Parameters.AddWithValue("@PrimaryPhoneNo", venturedetails.PrimaryPhoneNo);
                        cmd.Parameters.AddWithValue("@PrimaryRoleInVenture", venturedetails.PrimaryRoleInVenture);
                        cmd.Parameters.AddWithValue("@PrimaryColaborationAffiliation", venturedetails.PrimaryColaborationAffiliation);
                        cmd.Parameters.AddWithValue("@SecondaryPhoneNo", venturedetails.SecondaryPhoneNo);
                        cmd.Parameters.AddWithValue("@SecondaryRoleInVenture", venturedetails.SecondaryRoleInVenture);
                        cmd.Parameters.AddWithValue("@ScondaryColaborationAffiliation", venturedetails.ScondaryColaborationAffiliation);
                        cmd.Parameters.AddWithValue("@Advertisement", venturedetails.Advertisement);
                        cmd.Parameters.AddWithValue("@WouldLikeToElaborateSpecifics", venturedetails.WouldLikeToElaborateSpecifics);
                        cmd.Parameters.AddWithValue("@WhatDoYouDo", venturedetails.WhatDoYouDo);
                        cmd.Parameters.AddWithValue("@WhatIsTheProblem", venturedetails.WhatIsTheProblem);
                        cmd.Parameters.AddWithValue("@WhoHasIt", venturedetails.WhoHasIt);
                        cmd.Parameters.AddWithValue("@HowWillUSolveTheProblem", venturedetails.HowWillUSolveTheProblem);
                        cmd.Parameters.AddWithValue("@HowWillUMakeMoney", venturedetails.HowWillUMakeMoney);
                        cmd.Parameters.AddWithValue("@WhtIsTheSecretSauce", venturedetails.WhtIsTheSecretSauce);
                        cmd.Parameters.AddWithValue("@IsThereIP", venturedetails.IsThereIP);
                        cmd.Parameters.AddWithValue("@WhatIsYourCurrentStatus", venturedetails.WhatIsYourCurrentStatus);
                        cmd.Parameters.AddWithValue("@WhatTypeOfVMSHelpStartUpNeeds", venturedetails.WhatTypeOfVMSHelpStartUpNeeds);
                        cmd.Parameters.AddWithValue("@UserName", venture.PrimaryEmail);
                        cmd.Parameters.AddWithValue("@Password", EncryptDecrypt.Encrypt(Password));

                        isInserted = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message);

            }
            return isInserted;
        }
        public DataTable GetAdminbasedonRoleId()
        {
            DataTable dt = new DataTable();
            string conStr = ConfigurationManager.ConnectionStrings["ApiConStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("Select FirstName,LastName,RoleId,Email FROM users where RoleId = 1", con))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void LogError(string msg)
        {
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["ApiConStr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = new SqlCommand("CRM_InsertErrorLog", con))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ErrorMessage", msg);
                        cmd.Parameters.AddWithValue("@ErrorPage", "PostNextTech");
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SendEmailToDigital55UsersForVentureApplication(string ventureEmailId, string adminEmailid, string FirstName, Int64 mentorid, int roleid)
        {
            try
            {
                string MessageBody = "", subject = "";
                string fromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                if (roleid == 2)
                {
                    string questionurl = ConfigurationManager.AppSettings["QuestionaryUrl"].ToString();
                    fromEmail = "support@priyanet.com";
                    subject = "Thank you for Applying!";
                    MessageBody = "<html><body><table><tr><td>Hello " + FirstName + "</td></tr>" +
                    "<tr><td></td></tr>" +
                    "<tr><td></td></tr>" +
                    "<tr><td>We have received your request.</td></tr>" +
                    "<tr><td>Thank you for applying. Someone will be in touch shortly.</td></tr> " +
                    "<tr><td></td></tr>" +
                     "<tr><td></td></tr>" +
                    "<tr><td>Thank you</td></tr>" +
                      "<tr><td></td></tr>" +
                     "<tr><td><b>*** This is an automatically generated email, please do not reply ***</b></td></tr>" +
                    "</table> </body></html>";
                    SendEmail objEmail = new SendEmail();
                    await Task.Run(() => objEmail.SendEmailasync(fromEmail, ventureEmailId, subject, MessageBody));
                }
                if (roleid == 1)
                {
                    fromEmail = "support@priyanet.com";
                    subject = "New Request for Venture";
                    MessageBody = "<html><body><table><tr><td>Hello</td></tr>" +
                "<tr><td></td></tr>" +
                "<tr><td></td></tr>" +
                "<tr><td>You have received a New Venture Application request from (" + ventureEmailId + ").</td></tr>" +
                "<tr><td></td></tr>" +
                 "<tr><td></td></tr>" +
                "<tr><td>Thank you</td></tr>" +
                  "<tr><td></td></tr>" +
                 "<tr><td></td></tr>" +
                 "<tr><td><b>*** This is an automatically generated email, please do not reply ***</b></td></tr>" +
                "</table> </body></html>";
                    SendEmail objEmail1 = new SendEmail();
                    await Task.Run(() => objEmail1.SendEmailasync(fromEmail, adminEmailid, subject, MessageBody));
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}