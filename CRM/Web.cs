using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
namespace CRM
{
    public class Web
    {

        public static string GetHtml(string url)
        {
            string connectionstring = url;
            try
            {

                System.Net.HttpWebRequest myrequest = (HttpWebRequest)WebRequest.Create(connectionstring);
                myrequest.Credentials = CredentialCache.DefaultCredentials;
                WebResponse webresponse = myrequest.GetResponse();
                Stream resstream = myrequest.GetRequestStream();
                StreamReader resstreamreader = new StreamReader(resstream);
                string pagaecount = resstreamreader.ReadToEnd();
                resstreamreader.Close();
                resstream.Close();
                return pagaecount;

            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}