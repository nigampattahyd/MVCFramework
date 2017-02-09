using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CRM
{
    public class GlobalVariables
    {
        //*** Session String Values ***********************
        private static string _clientName = "_clientName";
        private static string _clientNameAccounting = "_clientNameAccounting";
        private static string _userID = "_userID";
        private static string _userName = "_userName";
        private static string _roleID = "_roleID";
        private static string _roleName = "_roleName";
        private static string _branchID = "_branchID";
        private static string _fname = "_fname";
        private static string _lname = "_lname";
        private static string _fullname = "_fullname";
        private static string _email = "_email";
        private static string _company_name = "_company_name";
        private static string _IsAllowedToSMS = "_IsAllowedToSMS";
        private static string _CompanyTimeZone = "_CompanyTimeZone";
        private static string _ELearning = "_ELearning";
        private static string _Connectionstring = "_Connectionstring";
        public static string _Ventureid = "_Ventureid";
        public static string _Mentorid = "_Mentorid";

        // Set ELearning
        public static string ELearning
        {
            get
            {
                return HttpContext.Current.Session[_ELearning].ToString();
            }
            set
            { 
                HttpContext.Current.Session[_ELearning] = value; 
            }
        }

        // Set Client Database Name
        public static string ClientName
        {
            get
            {
                if (HttpContext.Current.Session[_clientName] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return HttpContext.Current.Session[_clientName].ToString();
                }
            }
            set
            { HttpContext.Current.Session[_clientName] = value; }

        }
        // Set Client Connection String
        public static string ClientNameAccounting
        {
            get
            {
                if (HttpContext.Current.Session[_clientNameAccounting] == null)
                {
                    return (ClientName + "Accounting").ToString();
                }
                else
                {
                    return HttpContext.Current.Session[_clientNameAccounting].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session[_clientNameAccounting] = value;// (ClientName + "Accounting").ToString(); 
            }

        }
        // Set UserID
        public static string UserID
        {
            get
            {
                if (HttpContext.Current.Session[_userID] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return HttpContext.Current.Session[_userID].ToString();
                }
            }
            set
            { HttpContext.Current.Session[_userID] = value; }

        }
        // Set UserName
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session[_userName] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_userName].ToString(); }
            }
            set
            { HttpContext.Current.Session[_userName] = value; }
        }
        // Set RoleID
        public static string RoleID
        {
            get
            {
                if (HttpContext.Current.Session[_roleID] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_roleID].ToString(); }
            }
            set
            { HttpContext.Current.Session[_roleID] = value; }

        }
        // Set RoleName
        public static string RoleName
        {
            get
            {
                if (HttpContext.Current.Session[_roleName] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_roleName].ToString(); }
            }
            set
            { HttpContext.Current.Session[_roleName] = value; }
        }
        // Set BranchID
        public static string BranchID
        {
            get
            {
                if (HttpContext.Current.Session[_branchID] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_branchID].ToString(); }
            }
            set
            { HttpContext.Current.Session[_branchID] = value; }
        }
        // Set FirstName
        public static string FirstName
        {
            get
            {
                if (HttpContext.Current.Session[_fname] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_fname].ToString(); }
            }
            set
            { HttpContext.Current.Session[_fname] = value; }
        }


        
        // Set LastName
        public static string LastName
        {
            get
            {
                if (HttpContext.Current.Session[_lname] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_lname].ToString(); }
            }
            set
            { HttpContext.Current.Session[_fname] = value; }
        }
        // Set LastName


        public static string LoggedUsrFirstName
        {
            get
            {
                if (HttpContext.Current.Session[_fname] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_fname].ToString(); }
            }
            set
            { HttpContext.Current.Session[_fname] = value; }
        }

        public static string LoggedUsrLastName
        {
            get
            {
                if (HttpContext.Current.Session[_lname] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_lname].ToString(); }
            }
            set
            { HttpContext.Current.Session[_fname] = value; }
        }




        public static string FullName
        {
            get
            {
                if (HttpContext.Current.Session[_fullname] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_fullname].ToString(); }
            }
            set
            { HttpContext.Current.Session[_fullname] = value; }
        }
        // Set EmailID
        public static string EmailID
        {
            get
            {
                if (HttpContext.Current.Session[_email] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_email].ToString(); }
            }
            set
            { HttpContext.Current.Session[_email] = value; }
        }
        // Set CompanyName
        public static string CompanyName
        {
            get
            {
                if (HttpContext.Current.Session[_company_name] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_company_name].ToString(); }
            }
            set
            { HttpContext.Current.Session[_company_name] = value; }
        }
        // Set IsAllowedToSMS
        public static string IsAllowedToSMS
        {
            get
            {
                if (HttpContext.Current.Session[_IsAllowedToSMS] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_IsAllowedToSMS].ToString(); }
            }
            set
            { HttpContext.Current.Session[_IsAllowedToSMS] = value; }
        }

        // Function to clear All Session
        public static void ClearAllSessions()
        {
            UserID = string.Empty;
            UserName = string.Empty;
            RoleID = string.Empty;
            RoleName = string.Empty;
            BranchID = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            FullName = string.Empty;
            EmailID = string.Empty;
            CompanyName = string.Empty;
            IsAllowedToSMS = string.Empty;
            CompanyTimeZone = string.Empty;
        }

        // Function to Check If Session Is Valid Or Not
        public static Boolean IsValidSession()
        {
            if (string.IsNullOrEmpty(ClientName) || string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(RoleID) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(BranchID))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Set Company TimeZone
        public static string CompanyTimeZone
        {
            get
            {
                if (HttpContext.Current.Session[_CompanyTimeZone] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_CompanyTimeZone].ToString(); }
            }
            set
            { HttpContext.Current.Session[_CompanyTimeZone] = value; }
        }

        //Set Connectionstring 
        public static string ConnectionString
        {
            get
            {
                if (HttpContext.Current.Session[_Connectionstring] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_Connectionstring].ToString(); }
            }
            set
            { HttpContext.Current.Session[_Connectionstring] = value; }
        }

        //Loggedin user VentureId
        public static string VentureId
        {
            get
            {
                if (HttpContext.Current.Session[_Ventureid] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_Ventureid].ToString(); }
            }
            set
            { HttpContext.Current.Session[_Ventureid] = value; }
        }
        //Loggedin user MentorId
        public static string MentorId
        {
            get
            {
                if (HttpContext.Current.Session[_Mentorid] == null) { return string.Empty; }
                else { return HttpContext.Current.Session[_Mentorid].ToString(); }
            }
            set
            { HttpContext.Current.Session[_Mentorid] = value; }
        }
    }
}