using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM
{
    public class Facebook
    {
        public string Facebook_GraphAPI_Token = "https://graph.facebook.com/oauth/access_token?";
        public string Facebook_GraphAPI_Me = "https://facebook.com/me?";
        public string AppID = "1567086776902604";
        public string AppSecret = "8b5a2f45afc003a04aed5974279127d3";


        public IDictionary<string,string> GetUserData(string accessCode,string redirectURI)
        {
            string token = Web.GetHtml(Facebook_GraphAPI_Token + "client_id=" + AppID + "&redirct_uri=" + HttpUtility.HtmlEncode(redirectURI) + "%3F_provider_%3DFacebook" + "&client_secret=" + AppSecret + "&code=" + accessCode);
            if(token==null || token=="")
            {
                return null;
            }
            
                //string data = Web.GetHtml(Facebook_GraphAPI_Me + "fields=id,name,email,username,gender,link&access_token=" + token.Substring("access_token=", "&"));

                var userData = new Dictionary<string, string>();
                //userData.Add("id","\"id\":\"");
                //string x = "khj";
                //x.SubString("")
                return userData;
            
           
        }

    }
}