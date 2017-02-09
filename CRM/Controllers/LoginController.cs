using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMHub.Interface;
using CRMHub.EntityFramework;
using System.Configuration;

using System.Net.Mail;
using System.Net;
using CRM.Helper;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    public class LoginController : Controller
    {
        private readonly IClientRepository _iclientrepository;
        private readonly IUserRepository _iuserrepository;
        private readonly IErrorLogRepository _ierrorlogrepository;
        private readonly ICommonRepository _icommonrepository;
        private readonly IUserMasterRepository _iusermasterrepository;
        private readonly IRoleRepository _irolerepository;
        public LoginController(IClientRepository iclientrepository, ICommonRepository icommonrepository, IUserRepository iuserrepository, IErrorLogRepository ierrorlogrepository, IUserMasterRepository iusermasterrepository, IRoleRepository irolerepository)
        {
            _iclientrepository = iclientrepository;
            _icommonrepository = icommonrepository;
            _iuserrepository = iuserrepository;
            _ierrorlogrepository = ierrorlogrepository;
            _iusermasterrepository = iusermasterrepository;
            _irolerepository = irolerepository;
        }
        //
        // GET: /Login/
        public ActionResult Index(user userObj)
        {
            try
            {
                if (Session["Logeduser"] != null)
                {
                    Session["Logeduser"] = null;
                }

                userObj.lstRoles = new List<SelectListItem>();
                userObj.lstRoles.Add(new SelectListItem { Text = "Admin", Value = "1" });
                userObj.lstRoles.Add(new SelectListItem { Text = "Venture", Value = "2" });
                userObj.lstRoles.Add(new SelectListItem { Text = "Mentor", Value = "4" });
              
                return View(userObj);
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost]
        public ActionResult LoginUser(user objUser)
        {
            try
            {

                //if (objUser == null)
                //{
                //    objUser = new objUser();
                //}
                //user objUser = new user();
                UserMaster objusermstr = new UserMaster();
                var typecode = "";
                if (objUser.Type == "1")
                {
                    typecode = "A";
                }
                else if (objUser.Type == "2")
                {
                    typecode = "V";
                }
                else if (objUser.Type == "4")
                {
                    typecode = "M";
                }
                if (ModelState.IsValid)
                //if (1 == 1)
                {
                    // objUser.ClientId = "Mentor";
                    Client objclienttype = _iclientrepository.GetSiteTypeByClientID(objUser.ClientId);
                    GlobalVariables.ClientName = objUser.ClientId;

                    TempData["LogID"] = objUser.LoginId;
                    TempData["Password"] = objUser.Password;

                    if (objUser.ClientId == "CRMClients")
                    {

                        string conn = ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;
                        GlobalVariables.ConnectionString = conn;
                        //GlobalVariables.ClientName = objUser.ClientId;                  
                        //string  uname = objUser.LoginId;
                        //string upassword = objUser.Password;
                        string Connc = objUser.ClientId;
                        string umstrname = TempData["LogID"].ToString();
                        string umstrpassword = TempData["Password"].ToString();
                        // UserMaster  objumaster =new UserMaster();

                        if (umstrname == "sadmin" && umstrpassword == "sadmin")
                        {
                            string userloggedin = _iusermasterrepository.CheckUserExists(umstrname, umstrpassword, conn, GlobalVariables.ClientName).ToString();

                            //userloggedin = _iusermasterrepository.CheckUserExists(umstrname, umstrpassword, conn, GlobalVariables.ClientName);
                            if (userloggedin != null)
                            {
                                //    //GlobalVariables.UserID = userloggedin..ToString();
                                //    //GlobalVariables.UserName = loggedUser.LoginId.ToString();
                                //    //GlobalVariables.RoleID = loggedUser.RoleId.ToString();
                                //    //GlobalVariables.RoleName = loggedUser.roleName.ToString();
                                //    //GlobalVariables.BranchID = loggedUser.BranchId.ToString();

                                return RedirectToAction("ClientManagerIndex", "ClientManager");

                            }
                            else
                            {
                                TempData["LoginError"] = "User Not Exist";
                                return RedirectToAction("Index");
                            }

                        }

                        else
                        {
                            TempData["LoginError"] = "Access denied";
                            return RedirectToAction("Index");
                        }
                        //string userloggedin = _iusermasterrepository.CheckUserExists(umstrname, umstrpassword, conn, GlobalVariables.ClientName).ToString();

                        ////userloggedin = _iusermasterrepository.CheckUserExists(umstrname, umstrpassword, conn, GlobalVariables.ClientName);
                        //if (userloggedin != null)
                        //{
                        //    //    //GlobalVariables.UserID = userloggedin..ToString();
                        //    //    //GlobalVariables.UserName = loggedUser.LoginId.ToString();
                        //    //    //GlobalVariables.RoleID = loggedUser.RoleId.ToString();
                        //    //    //GlobalVariables.RoleName = loggedUser.roleName.ToString();
                        //    //    //GlobalVariables.BranchID = loggedUser.BranchId.ToString();

                        //    return RedirectToAction("ClientManagerIndex", "ClientManager");

                        //}
                        //else
                        //{
                        //    TempData["LoginError"] = "User Not Exist";
                        //    return RedirectToAction("Index");
                        //}


                    }



                    //}
                    //else { 
                    if (objclienttype != null)
                    {
                        Client clientEntity = _iclientrepository.ValidateClient(objUser.ClientId, objclienttype.site_type);
                        if (clientEntity.Result == "1")
                        {
                            GlobalVariables.ConnectionString = objclienttype.ConnectionString.ToString();
                            var pw = EncryptDecrypt.Encrypt("seA/Hr0AgyE=");
                            GlobalVariables.ClientName = clientEntity.ClientID; // ClientUniqueID;

                            // For Logo 
                            string LogoLoc = "";
                            if (objclienttype.companylogo != null)
                            {
                                LogoLoc = objclienttype.companylogo;
                                Session["ClientId"] = LogoLoc;

                            }
                            else
                            {
                                LogoLoc = "logo.png";
                                // LogoLoc = "Digital55-Logo.png";
                                Session["ClientId"] = LogoLoc;
                            }
                            Session["Companyname"] = objclienttype.CompanyName;



                            user loggedUser = _iuserrepository.getLoggedUser(objUser.LoginId, EncryptDecrypt.Encrypt(objUser.Password), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                           
                            
                            if (loggedUser != null)
                            {
                              
                                if (objUser.Type == Convert.ToString(loggedUser.RoleId))
                                {

                                    if (loggedUser.Status == false)
                                    {
                                        TempData["LoginError"] = "User Is InActive";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {


                                        GlobalVariables.UserID = loggedUser.UserId.ToString();
                                        GlobalVariables.UserName = loggedUser.LoginId.ToString();
                                        GlobalVariables.RoleID = loggedUser.RoleId.ToString();
                                        GlobalVariables.RoleName = loggedUser.roleName.ToString();
                                        // GlobalVariables.BranchID = loggedUser.BranchId.ToString();
                                        GlobalVariables.FirstName = loggedUser.FirstName.ToString();
                                        if (loggedUser.LastName != null && loggedUser.LastName != string.Empty)
                                        {
                                            GlobalVariables.LastName = loggedUser.LastName.ToString();
                                        }
                                        else
                                        {
                                            GlobalVariables.LastName = "";
                                        }


                                        GlobalVariables.LoggedUsrFirstName = loggedUser.FirstName.ToString();
                                        if (loggedUser.LastName != null && loggedUser.LastName != string.Empty)
                                        {
                                            GlobalVariables.LoggedUsrLastName = loggedUser.LastName.ToString();
                                        }
                                        else
                                        {
                                            GlobalVariables.LoggedUsrLastName = "";
                                        }
                                        if (loggedUser.VentureId != null && loggedUser.VentureId != 0)
                                        {
                                            GlobalVariables.VentureId = loggedUser.VentureId.ToString();
                                        }
                                        else
                                        {
                                            GlobalVariables.VentureId = "";
                                        }
                                        if (loggedUser.MentorId != null && loggedUser.MentorId != 0)
                                        {
                                            GlobalVariables.MentorId = loggedUser.MentorId.ToString();
                                        }
                                        else
                                        {
                                            GlobalVariables.MentorId = "";
                                        }

                                        if (loggedUser.RoleId.ToString() == "10")
                                        {
                                            GlobalVariables.FullName = "sadmin";//loggedUser.firstName.ToString();
                                        }

                                        else
                                        {
                                            if (loggedUser.LastName != null && loggedUser.LastName != "")
                                            {
                                                GlobalVariables.FullName = loggedUser.LastName.ToString() + ", " + loggedUser.FirstName.ToString();
                                            }
                                            else
                                            {
                                                GlobalVariables.FullName = loggedUser.FirstName.ToString();
                                            }

                                        }
                                        if (loggedUser.LastName != null && loggedUser.LastName != "")
                                        {
                                            Session["Logeduser"] = loggedUser.FirstName.ToString() + " " + loggedUser.LastName.ToString();
                                        }
                                        else
                                        {
                                            Session["Logeduser"] = loggedUser.FirstName.ToString();
                                        }

                                        Session["RoleName"] = GlobalVariables.RoleName;
                                        Session["UserId"] = GlobalVariables.UserID;

                                        GlobalVariables.EmailID = loggedUser.Email.ToString();
                                        GlobalVariables.IsAllowedToSMS = loggedUser.IsAllowsedingSMS.ToString();

                                        List<tbl_Menus> listDynamicRoleMenu = _icommonrepository.getDynamicMenuByRoleId(Convert.ToInt32(GlobalVariables.RoleID), GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                                        Session["ssnDynamicMenus"] = listDynamicRoleMenu;
                                      
                                        return RedirectToAction("Index", "DashboardHome");
                                        //return RedirectToAction("Index", "Dashboard");
                                    }
                                }
                                else
                                {
                                    TempData["LoginError"] = "Role Check";
                                    return RedirectToAction("Index", "Login", new { @Type = typecode });
                                }
                            }
                            else
                            {
                                TempData["LoginError"] = "User Not Exist";
                                return RedirectToAction("Index", "Login", new { @Type = typecode });
                            }
                        }
                        // }
                        else
                        {
                            TempData["LoginError"] = "ClientId Not Exist";
                            return RedirectToAction("Index", "Login", new { @Type = typecode });
                        }
                    }
                    else
                    {
                        TempData["LoginError"] = "ClientId Not Exist";
                        return RedirectToAction("Index", "Login", new { @Type = typecode });
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { @Type = typecode });
                }
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Login/LoginUser";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }



        [HttpGet]
        public JsonResult GetClientLogo(string ClientId)
        {
            try
            {
                string result = "";
                Client objclienttype = _iclientrepository.GetSiteTypeByClientID(ClientId);

                if (objclienttype != null)
                {
                    if (objclienttype.companylogo != null)
                    {

                        result = objclienttype.companylogo;
                    }
                    else
                    {
                        // result = "Digital55-Logo.png";
                        result = "logo.png";
                    }
                }
                else
                {
                    result = "1";
                }




                // result = objclienttype.companylogo;              
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult GetPassWord(string Email, string ClientId)
        {
            try
            {
                bool emailstatus = false;
                user users = new user();
                string conn = ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;
                GlobalVariables.ConnectionString = conn;
                SendEmail objEmail = new SendEmail();
                if (ClientId != null)
                {
                    users = _iuserrepository.GetPassWord(Email, conn, ClientId);
                    if (users.Email != null)
                    {
                        string Password = EncryptDecrypt.Decrypt(users.Password);
                        string email = users.Email;
                        string fromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                        string loginId = users.LoginId;
                        if (Password != null)
                        {
                            //string firstName = users.FirstName;
                            //string MsgBody;
                            //String host = "";
                            //int port;
                            //string Message = "<b>Dear " + firstName + ",</br><br/>" + "&nbsp;&nbsp;&nbsp;&nbsp; LoginID&nbsp;:&nbsp;" + loginId + "<br/>&nbsp;&nbsp;&nbsp;&nbsp; Password&nbsp;:&nbsp;" + Password;
                            //emailstatus = objEmail.Compose(fromEmail, email, "Password", Message);

                            SendEmailToDigital55UsersForForgotPassword(email, loginId, Password);
                            emailstatus = true;
                        }
                    }
                    else
                    {
                        emailstatus = false;
                    }
                }

                return Json(emailstatus, JsonRequestBehavior.AllowGet);

            }

            catch (Exception e)
            {
                throw;
                // return View("Error");
                //ModelState.AddModelError("", e.Message);
                //return View("Index");
            }
        }

        public List<role> ListofRoles()
        {
            try
            {
                List<role> objroleslist = new List<role>();
                objroleslist = _irolerepository.GetAllMentoringRoles(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                return objroleslist;
            }
            catch (Exception ex)
            {

                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/ListofRoles";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                return null;
            }
        }

        public async Task SendEmailToDigital55UsersForForgotPassword(string to, string username, string password)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                string subject = "Your Password";
                string MessageBody = "";
                MessageBody = "<html><body><table><tr><td>The following are login  credentials of Mentoring </td></tr> " +
                                  " <tr><td>Username            : " + username + "</td></tr> " +
                                  " <tr><td>Password            : " + password + " </td></tr> " +
                                  " </table> </body></html>";
                SendEmail objEmail = new SendEmail();
                await Task.Run(() => objEmail.SendEmailasync(fromEmail, to, subject, MessageBody));

                //objEmail.SendAsyncMail(fromEmail, to, subject, MessageBody);
                //return true;
            }
            catch (Exception ex)
            {
                Error_Log objErrorLog = new Error_Log();
                objErrorLog.CreatedDate = DateTime.Now;
                objErrorLog.ErrorMessage = ex.Message;
                objErrorLog.ErrorPage = "Venture/SendEmailToDigital55Users";//controllerName + "/" + actionName;
                _ierrorlogrepository.InsertErrorLogException(objErrorLog);
                //return false;
            }
        }
    }
}