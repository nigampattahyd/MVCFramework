using CRMHub.EntityFramework;
using CRMHub.Interface;
using CRMHub.Repository;
using CRMHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using ClosedXML;
using ClosedXML.Excel;

using System.Runtime.InteropServices;


namespace CRM.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserRepo _iuserrepo;
        private readonly ICommonRepository _icommonpository;
        private readonly IUserRepository _iuserrepository;
        private readonly IRoleRepository _irolerepository;
      
        public ProfileController()
        {
            _iuserrepo = new UserRepo();
            _icommonpository = new CommonRepository();
            _iuserrepository = new UserRepository();
            _irolerepository = new RoleRepository();
        }

        // GET: Profile

        public ActionResult Index()
        {
            try
            {
               // ViewData["Isexcel"] = "false";
                string ClientID = GlobalVariables.ClientName;
                Int32 UserId = Convert.ToInt32(GlobalVariables.UserID);
                string conStr = GlobalVariables.ConnectionString;
                UsersModel usermodel = new UsersModel();
                usermodel.users = _iuserrepo.GetUserDetailsbyloginID(UserId, conStr, ClientID);
                List<usState> listOfState = _icommonpository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                List<SelectListItem> listOfStates = listOfState.Select(state => new SelectListItem
                {
                    Text = state.StateName,
                    Value = state.StateCode
                }).ToList();
                ViewBag.StateList = listOfStates;
               ViewBag.success = TempData["success"];
                return View(usermodel);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult CheckPwd(string password)
        {
            try
            {
                bool checkpwd = false;
                string conStr = GlobalVariables.ConnectionString;
                user usermodel = new user();
                int userid = Convert.ToInt32(GlobalVariables.UserID);
                string encryptpassword = EncryptDecrypt.Encrypt(password);
                usermodel = _iuserrepo.CheckpasswordbyUserID(userid, encryptpassword, conStr, GlobalVariables.ClientName);
                if (usermodel.Password != null)
                {
                    string pwd = EncryptDecrypt.Decrypt(usermodel.Password);
                    checkpwd = true;
                }
                else
                {
                    checkpwd = false;
                }
                return Json(checkpwd, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }

        public ActionResult ChangePassword(string password)
        {
            try
            {
                bool ispwdempty = false;
                string conStr = GlobalVariables.ConnectionString;
                int userid = Convert.ToInt32(GlobalVariables.UserID);
                if (password != null)
                {
                    string encryptpassw = EncryptDecrypt.Encrypt(password);
                    bool isUpdated = _iuserrepo.UpdateUserPassword(userid, encryptpassw, conStr, GlobalVariables.ClientName);
                    ispwdempty = true;
                }
                else
                {
                    ispwdempty = false;
                }
                return Json(ispwdempty, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult DeleteUser(string UserId)
        {
            try
            {
                bool isdeleted = false;
                string ClientID = GlobalVariables.ClientName;
               // Int32 UserId = Convert.ToInt32(GlobalVariables.UserID);
                string conStr = GlobalVariables.ConnectionString;
                user usermodel = new user();
                int userid = Convert.ToInt32(UserId);
                bool status = false;
                if (UserId != null)
                {
                     isdeleted = _iuserrepo.Deleteuser(userid, ClientID, status, conStr, GlobalVariables.ClientName);
                     isdeleted = true;
                }
                return Json(isdeleted, JsonRequestBehavior.AllowGet);
            }
           catch(Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult DeleteUsers(List<user> UserData)
        {
            bool flag = false;
            try
            {
                var ClientEntity = new DevEntities();
                string ClientID = GlobalVariables.ClientName;
                bool status = false;
                for (int i = 0; i < UserData.Count; i++)
                {
                    if (!string.IsNullOrEmpty(UserData[i].UserId.ToString()))
                    {

                        flag = _iuserrepo.Deleteuser(Convert.ToInt32(UserData[i].UserId), ClientID, status, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                    }
                }
                return Json(flag, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost]
        public ActionResult UpdateUserDetailsbyloginID(UsersModel userobj, FormCollection fc)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;
                GlobalVariables.ConnectionString = conn;
                int userid = Convert.ToInt32(GlobalVariables.UserID);
                userobj.users.UserId = userid;
                //userobj.users.Gender = fc["Gender"];
                bool isUpdated = _iuserrepo.UpdateUserDetailsbyloginID(userobj.users, conn, GlobalVariables.ClientName,"");
                TempData["success"] = "success";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult GetAllUsers()
        {
            ViewBag.Updated = TempData["Updated"];
            ViewBag.Created = TempData["Created"];
            ViewBag.DownloadExcel = TempData["excel"];
            return View();
        }


        public ActionResult getUser(jQueryDataTableParamModel param)
        {
            try
            {
                string loginid = GlobalVariables.UserID;
                string Keyword_UserName = Request["Keyword_UserName"].TrimStart();
                string iSortCol_0 = Request["iSortCol_0"];
                string sSortDir_0 = Request["sSortDir_0"];
                string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
                string orderByClause = string.Empty;
                int totalPageCount = 0;
                if (iSortCol_0 == "0")
                    orderByClause = "UserId desc";

                else
                    if (SortFieldName == "FullName")
                    {
                        SortFieldName = "FirstName";
                    }
                    else if (SortFieldName == "Email")
                    {
                        SortFieldName = "Email";
                    }
                    else if (SortFieldName == "Phone")
                    {
                        SortFieldName = "Phone";
                    }
                    else if (SortFieldName == "City")
                    {
                        SortFieldName = "City";
                    }
                    else if (SortFieldName == "stateName")
                    {
                        SortFieldName = "StateCode";
                    }
                    else if (SortFieldName == "roleName")
                    {
                        SortFieldName = "roleid";
                    }
                    else if (SortFieldName == "branchName")
                    {
                        SortFieldName = "branchId";
                    }
                orderByClause = SortFieldName + " " + sSortDir_0;
                var listusers = _iuserrepo.GetUserDetails(Keyword_UserName, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);

                if (TempData["Isexcel"] == "true")
                {
                    TempData["Isexcel"] = "false";
                    DataTable dtexcel = new DataTable();
                    dtexcel.TableName = "users";
                    dtexcel = ToDataTable(listusers);


                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dtexcel);
                        // Microsoft.Office.Interop.Excel.Worksheet ws2 =
                        //(Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets.Add();
                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename= UsersReport.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();

                        }
                    }
                }
                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = listusers,
                    iTotalRecords = totalPageCount,
                    iTotalDisplayRecords = totalPageCount
                }, JsonRequestBehavior.AllowGet);
            }   

            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult CreateNewUser(UsersModel usermodel)
        {
            try
            {
                string loginid = GlobalVariables.UserID;
                string roleid = GlobalVariables.RoleID;
                Branch selectOption = new Branch();
                selectOption.BranchId = -1;
                selectOption.BranchName = "Select Office";
                usermodel.listBranch = new List<Branch>();
                usermodel.listBranch.Add(selectOption);
                usermodel.listBranch.AddRange(_iuserrepository.getUserbranchlist(loginid, roleid, GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                role selectRole = new role();
                selectRole.RoleId = -1;
                selectRole.RoleName = "Select Role";
                usermodel.lstRoles = new List<role>();
                usermodel.lstRoles.Add(selectRole);
                usermodel.lstRoles.AddRange(_irolerepository.GetLevels(GlobalVariables.ConnectionString, GlobalVariables.ClientName));

                usState selectState = new usState();
                selectState.StateCode = "-1";
                selectState.StateName = "Select State";
                usermodel.getListOfStates = new List<usState>();
                usermodel.getListOfStates.Add(selectState);
                usermodel.getListOfStates.AddRange(_icommonpository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName));


                List<role> lstrole = new List<role>();
                //lstrole = usermodel.lstRoles.Where(x => x.RoleId != 10).ToList();
                //lstrole = usermodel.lstRoles.Where(x => x.RoleId != 10 && x.RoleId != 13 && x.RoleId != 18).ToList();
                lstrole = usermodel.lstRoles.Where(x => x.RoleId != 1).ToList();
                ViewBag.roleids = lstrole;
                return View(usermodel);

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult CreateNewUser(UsersModel usersmodel, FormCollection fc)
        {
            try
            {
                int i=1;
                TempData["CreateUser"] = "CreateUser";
                usersmodel.users.ClientId = GlobalVariables.ClientName;
                usersmodel.users.Password = EncryptDecrypt.Encrypt(usersmodel.users.Password);
                bool Iscreated = _iuserrepo.CreateNewUser(usersmodel.users, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                TempData["Created"] = "Created";
                return RedirectToAction("GetAllUsers");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult EditUser(int UserID)
        {
            try
            {
                string ClientID = GlobalVariables.ClientName;
                Int32 UserId = Convert.ToInt32(GlobalVariables.UserID);
                string conStr = GlobalVariables.ConnectionString;
                UsersModel usermodel = new UsersModel();
                usermodel.users = _iuserrepo.GetUserDetailsbyUserID(UserID, conStr, ClientID);
                usermodel.users.UserId = UserID;
                usermodel.users.Password = EncryptDecrypt.Decrypt(usermodel.users.Password);
                List<usState> listOfState = _icommonpository.getListOfStates(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                List<SelectListItem> listOfStates = listOfState.Select(state => new SelectListItem
                {
                    Text = state.StateName,
                    Value = state.StateCode
                }).ToList();
                ViewBag.StateList = listOfStates;
                List<role> listOfRole = _irolerepository.GetLevels(GlobalVariables.ConnectionString, GlobalVariables.ClientName);
                List<SelectListItem> listofroles = listOfRole.Select(role => new SelectListItem
                {
                    Text =role.RoleName,
                    Value = (role.RoleId).ToString()
                }).ToList();
                ViewBag.RoleList = listofroles;
                ViewBag.success = TempData["success"];
                return View(usermodel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult EditUser(UsersModel usermodel, FormCollection fc)
        {
            try
            {
                DateTime CreatedDate = DateTime.Now;
                usermodel.users.CreatedDate = CreatedDate;

                try
                {
                    string conn = ConfigurationManager.ConnectionStrings["CRMMasterClientsEntities"].ConnectionString;
                    GlobalVariables.ConnectionString = conn;
                   // usermodel.users.Password=EncryptDecrypt.Encrypt(usermodel.users.Password);
                    usermodel.users.ClientId = GlobalVariables.ClientName;
                    bool isUpdated = _iuserrepo.UpdateUserDetailsbyUserId(usermodel.users, conn, GlobalVariables.ClientName);
                    TempData["Updated"] = "Updated";
                    return RedirectToAction("GetAllUsers");
                }
                catch (Exception)
                {
                    throw;
                }
            
              
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public ActionResult DownloadExcel(jQueryDataTableParamModel param)
        {

            TempData["Isexcel"] = "true";
            return RedirectToAction("GetAllUsers");
           
        }

        private string ExportToExcelFile(DataTable dtTable)
        {
            try
            {
                StringBuilder sbldr = new StringBuilder();
                if (dtTable.Columns.Count != 0)
                {
                    foreach (DataColumn col in dtTable.Columns)
                    {
                        sbldr.Append(col.ColumnName + ',');
                    }
                    sbldr.Append("\r\n");
                    foreach (DataRow row in dtTable.Rows)
                    {
                        foreach (DataColumn column in dtTable.Columns)
                        {
                            sbldr.Append(string.Format("{0}", "\"" + row[column].ToString() + "\"") + ',');
                        }
                        sbldr.Append("\r\n");
                    }
                }
                return sbldr.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
        //public ActionResult getUser(jQueryDataTableParamModel param)
        //{
        //    try
        //    {
        //        string loginid = GlobalVariables.UserID;
        //        string Keyword_UserName = Request["Keyword_UserName"].TrimStart();
        //        string iSortCol_0 = Request["iSortCol_0"];
        //        string sSortDir_0 = Request["sSortDir_0"];
        //        string SortFieldName = Request["mDataProp_" + iSortCol_0 + ""];
        //        string orderByClause = string.Empty;
        //        int totalPageCount = 0;
        //        if (iSortCol_0 == "0")
        //            orderByClause = "UserId desc";

        //        else
        //            if (SortFieldName == "FullName")
        //            {
        //                SortFieldName = "FirstName";
        //            }
        //            else if (SortFieldName == "Email")
        //            {
        //                SortFieldName = "Email";
        //            }
        //            else if (SortFieldName == "Phone")
        //            {
        //                SortFieldName = "Phone";
        //            }
        //            else if (SortFieldName == "City")
        //            {
        //                SortFieldName = "City";
        //            }
        //            else if (SortFieldName == "stateName")
        //            {
        //                SortFieldName = "StateCode";
        //            }
        //            else if (SortFieldName == "roleName")
        //            {
        //                SortFieldName = "roleid";
        //            }
        //            else if (SortFieldName == "branchName")
        //            {
        //                SortFieldName = "branchId";
        //            }
        //        orderByClause = SortFieldName + " " + sSortDir_0;
        //        var listusers = _iuserrepo.GetUserDetails(Keyword_UserName, param.iDisplayStart, param.iDisplayLength, orderByClause, out totalPageCount, GlobalVariables.ConnectionString, GlobalVariables.ClientName);
        //        //    if (TempData["Isexcel"] == "true")
        //        //    {
        //        //        TempData["Isexcel"] = "false";
        //        //         DataTable dtexcel = new DataTable();
        //        //         dtexcel = ToDataTable(listusers);

        //        //if (dtexcel != null && dtexcel.Rows.Count>0)
        //        //{
        //        //    GridView gridexcel = new GridView();
        //        //    gridexcel.DataSource = dtexcel;
        //        //    gridexcel.DataBind();
        //        //    Response.ClearContent();
        //        //    Response.Buffer = true;
        //        //    Response.ContentType = "Application/x-msexcel";
        //        //    Response.AddHeader("content-disposition", "attachment;filename = POtest.xls");
        //        //    gridexcel.RenderControl(new HtmlTextWriter(Response.Output));
        //        //    Response.Charset = "";
        //        //    StringWriter sw = new StringWriter();
        //        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //        //    gridexcel.RenderControl(htw);
        //        //    Response.Output.Write(sw.ToString());
        //        //    //Response.Output.Write(ExportToExcelFile(dtexcel));
        //        //    Response.Flush();
        //        //    Response.End();
        //        //    //return RedirectToAction("GetAllUsers");
        //        //}
        //        //    }
        //        if (TempData["Isexcel"] == "true")
        //        {
        //            TempData["Isexcel"] = "false";
        //            DataTable dtexcel = new DataTable();
        //            dtexcel = ToDataTable(listusers);
        //            StringBuilder sb = new StringBuilder();

        //            IEnumerable<string> columnNames = dtexcel.Columns.Cast<DataColumn>().
        //                                              Select(column => column.ColumnName);
        //            sb.AppendLine(string.Join(",", columnNames));

        //            foreach (DataRow row in dtexcel.Rows)
        //            {
        //                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
        //                sb.AppendLine(string.Join(",", fields));
        //            }

        //            //string destinationPath = Server.MapPath("~/Documents/");
        //            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        //            string destinationPath = Path.Combine(pathUser, "Downloads");
        //            var testfile = " UsersReport" + ".xlsX";
        //            var dpath = destinationPath + "\\" + testfile;
        //            System.IO.File.WriteAllText(dpath, sb.ToString());
        //            TempData["excel"] = "Excel";
        //            // return RedirectToAction("GetAllUsers");
        //        }
        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            aaData = listusers,
        //            iTotalRecords = totalPageCount,
        //            iTotalDisplayRecords = totalPageCount
        //        }, JsonRequestBehavior.AllowGet);


        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}