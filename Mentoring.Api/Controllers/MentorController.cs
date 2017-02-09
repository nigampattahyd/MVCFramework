using CRMHub;
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
using System.Web.Http;
using CRMHub.ExtendedProperties;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using CRM.Helper;

namespace Mentoring.Api.Controllers
{

    [RoutePrefix("Mentor")]
    public class MentorController : ApiController
    {
        public Microsoft.Office.Interop.Word.Document wordDocument { get; set; }
        public IEnumerable<usState> GetAllStates()
        {

            string conStr = ConfigurationManager.ConnectionStrings["ApiConStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("Select  StateCode,StateName FROM usStates", con))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        yield return new usState
                        {
                            StateCode = Convert.ToString(row["StateCode"]),
                            StateName = Convert.ToString(row["StateName"]),
                        };
                    }
                }
            }
        }

        [HttpGet]
        [Route("GetState")]
        public IEnumerable<usState> getState()
        {
            var States = GetAllStates();
            // IEnumerable<MentorType> mentortypes = GetAllmentortypes();
            if (States == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return States.ToList();
        }

        public IEnumerable<Country> GetAllCountrys()
        {
            string conStr = ConfigurationManager.ConnectionStrings["ApiConStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("Select CountryID, CountryName FROM Country", con))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        yield return new Country
                        {
                            CountryID = Convert.ToInt32(row["CountryID"]),
                            CountryName = Convert.ToString(row["CountryName"]),
                        };
                    }
                }
            }
        }

        [HttpGet]
        [Route("GetCountry")]
        public IEnumerable<Country> getCountry()
        {
            var countrys = GetAllCountrys();
            // IEnumerable<MentorType> mentortypes = GetAllmentortypes();
            if (countrys == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return countrys.ToList();
        }

        [HttpGet]
        [Route("EmailExists")]
        public Int64 get(string EmailId)
        {
            try
            {
                Int64 isexists = 0;
                string Client = "mentor";
                isexists = IsEmailExists(EmailId, Client);
                return isexists;
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                return 0;
            }

        }

        public Int64 IsEmailExists(string EmailId, string dbname)
        {
            Int64 isinserted = 0;
            string constr = ConfigurationManager.ConnectionStrings["apiconstr"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("EmailIDExists", con))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmailId", EmailId);
                        cmd.Parameters.Add(new SqlParameter("@Isexits", SqlDbType.Int));
                        cmd.Parameters["@Isexits"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        isinserted = Convert.ToInt32(cmd.Parameters["@Isexits"].Value);

                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message);

            }
            return isinserted;
        }

        //[HttpGet]
        //[Route("FirstNameExists")]
        //public Int64 getfirstname(string FirstName)
        //{
        //    Int64 isexists = 0;
        //    string Client = "mentor";
        //    isexists = IsFirstNameExists(FirstName, Client);
        //    return isexists;
        //}

        //public Int64 IsFirstNameExists(string FirstName, string dbname)
        //{
        //    Int64 isinserted = 0;
        //    string constr = ConfigurationManager.ConnectionStrings["apiconstr"].ConnectionString;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(constr))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("MentorFirstNameExists", con))
        //            {
        //                if (con.State == ConnectionState.Closed)
        //                {
        //                    con.Open();
        //                }

        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@FirstName", FirstName);
        //                cmd.Parameters.Add(new SqlParameter("@Isexists", SqlDbType.Int));
        //                cmd.Parameters["@Isexists"].Direction = ParameterDirection.Output;
        //                cmd.ExecuteNonQuery();
        //                isinserted = Convert.ToInt32(cmd.Parameters["@Isexists"].Value);

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return isinserted;
        //}

        [HttpPost]
        [Route("Submit")]
        public string Post()
        {
            try
            {
                MentorDetail mentor = new MentorDetail();
                mentor.FirstName = HttpContext.Current.Request.Form["FirstName"];
                mentor.LastName = HttpContext.Current.Request.Form["LastName"];
                mentor.Organization = HttpContext.Current.Request.Form["Organization"];
                mentor.Title = HttpContext.Current.Request.Form["Title"];
                mentor.Street1 = HttpContext.Current.Request.Form["Street1"];
                mentor.AddressLine1 = HttpContext.Current.Request.Form["AddressLine1"];
                mentor.City1 = HttpContext.Current.Request.Form["City1"];
                mentor.StateProvinceRegion1 = HttpContext.Current.Request.Form["StateProvinceRegion1"];
                mentor.ZIP1 = HttpContext.Current.Request.Form["ZIP1"];
                mentor.Telephone1 = HttpContext.Current.Request.Form["Telephone1"];
                mentor.Telephone2 = HttpContext.Current.Request.Form["Telephone2"];
                mentor.PrimaryEmail = HttpContext.Current.Request.Form["PrimaryEmail"];
                mentor.LinkedIn = HttpContext.Current.Request.Form["LinkedIn"];
                mentor.Resume = HttpContext.Current.Request.Form["Resume"];
                string filepath = "";
                int iUploadedCnt = 0;
                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                string sPath = "";

                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/ResumeDocs/");
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

                // CHECK THE FILE COUNT.
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                        //if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        //{

                        // SAVE THE FILES IN THE FOLDER.
                        filepath = sPath + Path.GetFileName(hpf.FileName);
                        mentor.Resume = filepath;
                        var getextension = Path.GetExtension(hpf.FileName);
                        if (getextension == ".doc" || getextension == ".docx")
                        {
                            var getnamewithoutextn = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName);
                            hpf.SaveAs(filepath);
                            Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
                            wordDocument = appWord.Documents.Open(filepath);
                            wordDocument.ExportAsFixedFormat(sPath + getnamewithoutextn + ".pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
                        }

                        iUploadedCnt = iUploadedCnt + 1;
                        //}
                    }
                }

                //// RETURN A MESSAGE.
                //if (iUploadedCnt > 0) {
                //    return iUploadedCnt + " Files Uploaded Successfully";
                //}
                //else {
                //    return "Upload Failed";
                //}
                mentor.Resume = filepath;
                string Client = "mentor";
                Int64 details = InsertMentorApplicationDetails(mentor, Client);

                SendEmailToDigital55UsersForMentorAplication(mentor.PrimaryEmail, "", mentor.FirstName, mentor.LastName, details, 4);
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

                    SendEmailToDigital55UsersForMentorAplication(mentor.PrimaryEmail, users.Email, mentor.FirstName, mentor.LastName, users.UserId, 1);
                    //}

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

        public Int64 InsertMentorApplicationDetails(MentorDetail mentor, string dbname)
        {
            Int64 isinserted = 0;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["apiconstr"].ConnectionString;
                string password = string.Empty;
                //string CreatedDate = System.DateTime.Now.ToString(); 
                if (!string.IsNullOrEmpty(mentor.PrimaryEmail))
                {
                    string[] split = mentor.PrimaryEmail.Split('@');
                    if (split != null && split.Length > 0)
                    {
                        password = Convert.ToString(split[0]);
                        password = EncryptDecrypt.Encrypt(password);
                    }
                }

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("Mentor_CreateMentorDetails", con))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", mentor.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", mentor.LastName);
                        cmd.Parameters.AddWithValue("@Organization", mentor.Organization);
                        cmd.Parameters.AddWithValue("@Title", mentor.Title);
                        cmd.Parameters.AddWithValue("@Street1", mentor.Street1);
                        cmd.Parameters.AddWithValue("@AddressLine1", mentor.AddressLine1);
                        cmd.Parameters.AddWithValue("@City1", mentor.City1);
                        cmd.Parameters.AddWithValue("@StateProvinceRegion1", mentor.StateProvinceRegion1);
                        cmd.Parameters.AddWithValue("@ZIP1", mentor.ZIP1);
                        cmd.Parameters.AddWithValue("@Telephone1", mentor.Telephone1);
                        cmd.Parameters.AddWithValue("@Telephone2", mentor.Telephone2);
                        cmd.Parameters.AddWithValue("@PrimaryEmail", mentor.PrimaryEmail);
                        cmd.Parameters.AddWithValue("@LinkedIn", mentor.LinkedIn);
                        cmd.Parameters.AddWithValue("@Resume", mentor.Resume);
                        cmd.Parameters.AddWithValue("@UserName", mentor.PrimaryEmail);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.Add(new SqlParameter("@outID", SqlDbType.Int));
                        cmd.Parameters["@outID"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        isinserted = Convert.ToInt32(cmd.Parameters["@outID"].Value);

                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
            return isinserted;
        }

        [HttpGet]
        [Route("GetMentorDetails")]
        public MentorDetail get(int MentorId)
        {
            try
            {

                DataTable dt = new DataTable();
                dt = getmentordetails(MentorId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new MentorDetail
                    {
                        FirstName = Convert.ToString(dt.Rows[0]["FirstName"]),
                        LastName = Convert.ToString(dt.Rows[0]["LastName"]),
                        Organization = Convert.ToString(dt.Rows[0]["Organization"]),
                        Title = Convert.ToString(dt.Rows[0]["Title"]),
                        Street1 = Convert.ToString(dt.Rows[0]["Street1"]),
                        AddressLine1 = Convert.ToString(dt.Rows[0]["AddressLine1"]),
                        City1 = Convert.ToString(dt.Rows[0]["City1"]),
                        StateProvinceRegion1 = Convert.ToString(dt.Rows[0]["StateProvinceRegion1"]),
                        ZIP1 = Convert.ToString(dt.Rows[0]["ZIP1"]),
                        Telephone1 = Convert.ToString(dt.Rows[0]["Telephone1"]),
                        Telephone2 = Convert.ToString(dt.Rows[0]["Telephone2"]),
                        PrimaryEmail = Convert.ToString(dt.Rows[0]["PrimaryEmail"]),
                        LinkedIn = Convert.ToString(dt.Rows[0]["LinkedIn"]),
                    };
                }
                else
                {

                    return new MentorDetail();
                }

            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                return new MentorDetail();
            }

        }

        public DataTable getmentordetails(int MentorId)
        {
            DataTable dt = new DataTable();
            try
            {

                string conStr = ConfigurationManager.ConnectionStrings["ApiConStr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = new SqlCommand("select FirstName,LastName,Organization,Title,Street1,AddressLine1,City1,StateProvinceRegion1,ZIP1,Telephone1,Telephone2,PrimaryEmail,LinkedIn from MentorDetails where MentorId = " + MentorId + "", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                return dt;
            }

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

        [HttpPost]
        [Route("MentorDetails")]
        public string Post(JObject objData)
        {
            try
            {
                dynamic jsonData = objData;
                string IndustryOther1 = "", Startups1 = "", BusinessModel1 = "", Funding1 = "", Software1 = "", LifeSciences1 = "", FunctionalAreas1 = "";
                JObject mentordetailJson = jsonData.objmentordetail;
                JObject mentorquestionaryJson = jsonData.objmentorquestionnary;
                var IndustryOther2 = mentorquestionaryJson["IndustryOther"];
                var Startups2 = mentorquestionaryJson["Startups"];
                var BusinessModel2 = mentorquestionaryJson["BusinessModel"];
                var Funding2 = mentorquestionaryJson["Funding"];
                var Software2 = mentorquestionaryJson["Software"];
                var LifeSciences2 = mentorquestionaryJson["LifeSciences"];
                var FunctionalAreas2 = mentorquestionaryJson["FunctionalAreas"];

                var mentordetail = mentordetailJson.ToObject<MentorDetail>();
                var mentorquestionary = mentorquestionaryJson.ToObject<MentorQuestionary>();

                dynamic IndustryOtherbobj = JsonConvert.DeserializeObject<dynamic>(IndustryOther2.ToString());
                dynamic Startupsobj = JsonConvert.DeserializeObject<dynamic>(Startups2.ToString());
                dynamic BusinessModelobj = JsonConvert.DeserializeObject<dynamic>(BusinessModel2.ToString());
                dynamic Fundingobj = JsonConvert.DeserializeObject<dynamic>(Funding2.ToString());
                dynamic Softwareobj = JsonConvert.DeserializeObject<dynamic>(Software2.ToString());
                dynamic LifeSciencesobj = JsonConvert.DeserializeObject<dynamic>(LifeSciences2.ToString());
                dynamic FunctionalAreasobj = JsonConvert.DeserializeObject<dynamic>(FunctionalAreas2.ToString());

                if (IndustryOther2.ToString() != "[]" && IndustryOther2 != null)
                {
                    foreach (var item in IndustryOtherbobj)
                    {

                        IndustryOther1 = IndustryOther1 + "," + item.Value;

                    }
                }
                else
                {
                    IndustryOther1 = ",";
                }
                IndustryOther1 = IndustryOther1.Remove(0, 1);
                mentorquestionary.IndustryOther = IndustryOther1.ToString();
                if (Startups2.ToString() != "[]" && Startups2 != null)
                {
                    foreach (var item in Startupsobj)
                    {
                        Startups1 = Startups1 + "," + item.Value;

                    }
                }
                else
                {
                    Startups1 = ",";
                }
                Startups1 = Startups1.Remove(0, 1);
                mentorquestionary.Startups = Startups1.ToString();

                if (BusinessModel2.ToString() != "[]" && BusinessModel2 != null)
                {
                    foreach (var item in BusinessModelobj)
                    {
                        BusinessModel1 = BusinessModel1 + "," + item.Value;

                    }
                }
                else
                {
                    BusinessModel1 = ",";
                }
                BusinessModel1 = BusinessModel1.Remove(0, 1);
                mentorquestionary.BusinessModel = BusinessModel1.ToString();

                if (Funding2.ToString() != "[]" && Funding2 != null)
                {
                    foreach (var item in Fundingobj)
                    {
                        Funding1 = Funding1 + "," + item.Value;

                    }
                }
                else
                {
                    Funding1 = ",";
                }
                Funding1 = Funding1.Remove(0, 1);
                mentorquestionary.Funding = Funding1.ToString();

                if (Software2.ToString() != "[]" && Software2 != null)
                {
                    foreach (var item in Softwareobj)
                    {
                        Software1 = Software1 + "," + item.Value;

                    }
                }
                else
                {
                    Software1 = ",";
                }
                Software1 = Software1.Remove(0, 1);
                mentorquestionary.Software = Software1.ToString();


                if (LifeSciences2.ToString() != "[]" && LifeSciences2 != null)
                {
                    foreach (var item in LifeSciencesobj)
                    {
                        LifeSciences1 = LifeSciences1 + "," + item.Value;

                    }
                }
                else
                {
                    LifeSciences1 = ",";
                }
                LifeSciences1 = LifeSciences1.Remove(0, 1);
                mentorquestionary.LifeSciences = LifeSciences1.ToString();

                if (FunctionalAreas2.ToString() != "[]" && FunctionalAreas2 != null)
                {
                    foreach (var item in FunctionalAreasobj)
                    {
                        FunctionalAreas1 = FunctionalAreas1 + "," + item.Value;

                    }
                }
                else
                {
                    FunctionalAreas1 = ",";
                }
                FunctionalAreas1 = FunctionalAreas1.Remove(0, 1);
                mentorquestionary.FunctionalAreas = FunctionalAreas1.ToString();

                string Client = "mentor";
                Int64 details = InsertMentorDetails(mentordetail, mentorquestionary, Client);

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

        public Int64 InsertMentorDetails(MentorDetail mentordetail, MentorQuestionary mentorquestionary, string dbName)
        {
            int isInserted = 0;
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["ApiConStr"].ConnectionString;

                string password = string.Empty;
                if (!string.IsNullOrEmpty(mentordetail.PrimaryEmail))
                {
                    string[] split = mentordetail.PrimaryEmail.Split('@');
                    if (split != null && split.Length > 0)
                    {
                        password = Convert.ToString(split[0]);
                        password = EncryptDecrypt.Encrypt(password);
                    }
                }
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = new SqlCommand("Mentor_Questionnaries", con))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Prefix", mentordetail.Prefix);
                        cmd.Parameters.AddWithValue("@FirstName", mentordetail.FirstName);
                        cmd.Parameters.AddWithValue("@MiddleName", mentordetail.MiddleName);
                        cmd.Parameters.AddWithValue("@LastName", mentordetail.LastName);
                        cmd.Parameters.AddWithValue("@Suffix", mentordetail.Suffix);
                        cmd.Parameters.AddWithValue("@NickName", mentordetail.NickName);
                        cmd.Parameters.AddWithValue("@Organization", mentordetail.Organization);
                        cmd.Parameters.AddWithValue("@Title", mentordetail.Title);
                        cmd.Parameters.AddWithValue("@Street1", mentordetail.Street1);
                        cmd.Parameters.AddWithValue("@AddressLine1", mentordetail.AddressLine1);
                        cmd.Parameters.AddWithValue("@City1", mentordetail.City1);
                        cmd.Parameters.AddWithValue("@StateProvinceRegion1", mentordetail.StateProvinceRegion1);
                        cmd.Parameters.AddWithValue("@ZIP1", mentordetail.ZIP1);
                        cmd.Parameters.AddWithValue("@Country1", mentordetail.Country1);
                        cmd.Parameters.AddWithValue("@Street2", mentordetail.Street2);
                        cmd.Parameters.AddWithValue("@AddressLine2", mentordetail.AddressLine2);
                        cmd.Parameters.AddWithValue("@City2", mentordetail.City2);
                        cmd.Parameters.AddWithValue("@StateProvinceRegion2", mentordetail.StateProvinceRegion2);
                        cmd.Parameters.AddWithValue("@ZIP2", mentordetail.ZIP2);
                        cmd.Parameters.AddWithValue("@Country2", mentordetail.Country2);
                        cmd.Parameters.AddWithValue("@PrimaryEmail", mentordetail.PrimaryEmail);
                        cmd.Parameters.AddWithValue("@SecondaryEmail", mentordetail.SecondaryEmail);
                        cmd.Parameters.AddWithValue("@SkypeAddress", mentordetail.SkypeAddress);
                        cmd.Parameters.AddWithValue("@TwitterAddress", mentordetail.TwitterAddress);
                        cmd.Parameters.AddWithValue("@LinkedIn", mentordetail.LinkedIn);
                        cmd.Parameters.AddWithValue("@Telephone1", mentordetail.Telephone1);
                        cmd.Parameters.AddWithValue("@Telephone2", mentordetail.Telephone2);
                        cmd.Parameters.AddWithValue("@Telephone3", mentordetail.Telephone3);
                        cmd.Parameters.AddWithValue("@PublicProfile", mentordetail.PublicProfile);
                        //cmd.Parameters.AddWithValue("@Resume", mentordetail.Resume);
                        cmd.Parameters.AddWithValue("@Startups", mentorquestionary.Startups);
                        cmd.Parameters.AddWithValue("@BusinessModel", mentorquestionary.BusinessModel);
                        cmd.Parameters.AddWithValue("@Funding", mentorquestionary.Funding);
                        cmd.Parameters.AddWithValue("@Others1", mentorquestionary.Others1);
                        cmd.Parameters.AddWithValue("@Software", mentorquestionary.Software);
                        cmd.Parameters.AddWithValue("@LifeSciences", mentorquestionary.LifeSciences);
                        cmd.Parameters.AddWithValue("@IndustryOther", mentorquestionary.IndustryOther);
                        cmd.Parameters.AddWithValue("@Others2", mentorquestionary.Others2);
                        cmd.Parameters.AddWithValue("@FunctionalAreas", mentorquestionary.FunctionalAreas);
                        cmd.Parameters.AddWithValue("@Others3", mentorquestionary.Others3);
                        cmd.Parameters.AddWithValue("@UserName", mentordetail.PrimaryEmail);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@MentorId", mentordetail.MentorId);
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

        public async Task SendEmailToDigital55UsersForMentorAplication(string mentorEmailId, string adminEmailid, string FirstName, string LastName, Int64 mentorid, int roleid)
        {
            try
            {
                string MessageBody = "", subject = "";

                string fromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                if (roleid == 4)
                {
                    string questionurl = ConfigurationManager.AppSettings["QuestionaryUrl"].ToString();
                    fromEmail = "support@priyanet.com";
                    subject = " Mentoring Questionnaire Form";

                    MessageBody = "<html><body><table><tr><td>Hello " + FirstName + " " + LastName + " </td></tr>" +
                    "<tr><td></td></tr>" +
                    "<tr><td></td></tr>" +
                    "<tr><td>Thanks for contacting us!</td></tr>" +
                    "<tr><td>Please provide us with more information so we could serve you better.</td></tr> " +
                    "<tr><td>Follow the link below:</td></tr>" +
                    "<tr><td><a href=" + questionurl + mentorid + ">Click here for the Mentoring Questionnaire Form</a></td></tr>" +
                    "<tr><td></td></tr>" +
                     "<tr><td></td></tr>" +
                    "<tr><td>Thank you</td></tr>" +
                    "</table> </body></html>";
                    SendEmail objEmail = new SendEmail();
                    await Task.Run(() => objEmail.SendEmailasync(fromEmail, mentorEmailId, subject, MessageBody));
                }
                if (roleid == 1)
                {
                    fromEmail = "support@priyanet.com";
                    subject = "New Request for Mentor";
                    MessageBody = "<html><body><table><tr><td>Hello</td></tr>" +
                "<tr><td></td></tr>" +
                "<tr><td></td></tr>" +
                "<tr><td>You have received a New Mentor Application request from (" + FirstName + " " + LastName + " ).</td></tr>" +
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
                //objEmail.SendAsyncMail(fromEmail, to, subject, MessageBody);
                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //Error_Log objErrorLog = new Error_Log();
                //objErrorLog.CreatedDate = DateTime.Now;
                //objErrorLog.ErrorMessage = ex.Message;
                //objErrorLog.ErrorPage = "Venture/SendEmailToDigital55Users";//controllerName + "/" + actionName;
                //_ierrorlogrepository.InsertErrorLogException(objErrorLog);
                //return false;
            }
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
    }
}
