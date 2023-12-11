using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class CandidateSignUp : System.Web.UI.Page
    {
        string redirection_url = "https://localhost:44332/CandidateSignUp.aspx";

        public static GoogleTokenclass GoogleToken = new GoogleTokenclass();
        public static FacebookTokenclass FacebookToken = new FacebookTokenclass();
        public static GoogleUserclass GoogleProfile = new GoogleUserclass();
        public static FacebookUserclass FacebookProfile = new FacebookUserclass();

        public static string CETClientID;
        public static int UEClientStatus;
        public static string ReferralCode;
        public static bool IsReferralLink = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                redirection_url = ConfigurationManager.AppSettings["CandSignUpRedirectURL"].ToString();
                if (!IsPostBack)
                {
                    if (Request.QueryString["ReferralCode"] != null)
                    {
                        ReferralCode = Request.QueryString["ReferralCode"];
                        IsReferralLink = true;
                    }
                    if (Request.QueryString["code"] != null)
                    {
                        if (Request.Params[1].ToString().Contains("https://www.googleapis.com/auth/userinfo.profile"))
                        {
                            if (Request.QueryString["code"] != null)
                            {
                                if (GetGoogleToken(Request.QueryString["code"]))
                                {
                                    if (GetuserGoogleProfile(GoogleToken.access_token))
                                    {
                                        CandidateManagement.CandidateGoogleSignIn(GoogleProfile, ref CETClientID, ref UEClientStatus);
                                        if (IsReferralLink)
                                        {
                                            ReferralManagement.InsertReferralDetails(CETClientID, ReferralCode);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Request.QueryString["code"] != null)
                            {
                                if (GetFacebookOauthTokens(Request.QueryString["code"])) if (GetFacebookuserProfile(FacebookToken.access_token)) CandidateManagement.CandidatefacebookSignIn(FacebookProfile, ref CETClientID, ref UEClientStatus);
                            }
                        }
                    }
                    else if (Request.QueryString["Name"] != null)
                    {
                        lblname.Text = Request.QueryString["Name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }
        public bool GetGoogleToken(string code)
        {
            bool bRetValue = false;
            try
            {
                string poststring = "grant_type=authorization_code&code=" + code
                    + "&client_id=" + ConfigurationManager.AppSettings["Googleclientid"]
                    + "&client_secret=" + ConfigurationManager.AppSettings["Googleclientsecret"]
                    + "&redirect_uri=" + redirection_url + "";
                var request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                UTF8Encoding utfenc = new UTF8Encoding();
                byte[] bytes = utfenc.GetBytes(poststring);
                Stream outputstream = null;
                try
                {
                    request.ContentLength = bytes.Length;
                    outputstream = request.GetRequestStream();
                    outputstream.Write(bytes, 0, bytes.Length);
                }
                catch { }
                var response = (HttpWebResponse)request.GetResponse();
                var streamReader = new StreamReader(response.GetResponseStream());
                string responseFromServer = streamReader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                GoogleToken = js.Deserialize<GoogleTokenclass>(responseFromServer);
                bRetValue = true;
            }
            catch (Exception ex)
            {
                bRetValue = false;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {

            }
            return bRetValue;
        }
        public bool GetuserGoogleProfile(string accesstoken)
        {
            bool bRetValue = false;
            try
            {
                string url = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + accesstoken + "";
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                response.Close();
                JavaScriptSerializer js = new JavaScriptSerializer();
                GoogleProfile = js.Deserialize<GoogleUserclass>(responseFromServer);
                imgprofile.ImageUrl = GoogleProfile.picture;
                lblname.Text = GoogleProfile.name;
                bRetValue = true;
            }
            catch (Exception ex)
            {
                bRetValue = false;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {

            }
            return bRetValue;
        }
        private bool GetFacebookOauthTokens(string code)
        {
            bool bRetValue = false;
            try
            {

                string url = string.Format("https://graph.facebook.com/v16.0/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                                ConfigurationManager.AppSettings["FacebookclientId"],
                                redirection_url,
                                ConfigurationManager.AppSettings["FacebookclientSecret"],
                                code);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string retVal = reader.ReadToEnd();

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    FacebookToken = js.Deserialize<FacebookTokenclass>(retVal);
                }
                bRetValue = true;
            }
            catch (Exception ex)
            {
                bRetValue = false;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {

            }
            return bRetValue;
        }
        public bool GetFacebookuserProfile(string accesstoken)
        {
            bool bRetValue = false;
            try
            {
                string url = "https://graph.facebook.com/v16.0/me?fields=id,name,gender,email,picture{url,height,width,cache_key,is_silhouette}&access_token=" + accesstoken;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                response.Close();
                dynamic UserProfile = JsonConvert.DeserializeObject(responseFromServer);
                FacebookProfile.id = UserProfile.id;
                FacebookProfile.name = UserProfile.name;
                FacebookProfile.email = UserProfile.email;
                //FacebookProfile.gender = UserProfile.gender;
                FacebookProfile.imageurl = UserProfile.picture.data.url;
                lblname.Text = FacebookProfile.name;
                imgprofile.ImageUrl = FacebookProfile.imageurl;
                bRetValue = true;
            }
            catch (Exception ex)
            {
                bRetValue = false;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {

            }
            return bRetValue;
        }

    }
}