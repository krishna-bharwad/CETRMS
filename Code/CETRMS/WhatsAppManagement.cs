using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace CETRMS
{
    public static class WhatsAppManagement
    {
        public static List<WhatsAppAcount> WhatsAppAcounts = new List<WhatsAppAcount>();
        public static int RegisterWhatsAppAccount(WhatsAppAcount appAcount)
        {
            int iRetValue = 0;
            WhatsAppAcounts.Add(appAcount);
            return iRetValue;
        }
        public static int SendMessage(WhatsAppAcount appAcount, ref WhatsAppMessage.cRequest warequest, ref WhatsAppMessage.cResponse waresponse)
        {
            int iRetValue = 0;
            if (ConfigurationManager.AppSettings["EnableWhatsAppNotification"] == "0")
                return RetValue.Success;

            string strPhoneID = ConfigurationManager.AppSettings["WhatsAppPhoneID"];
            string strWhatsappAuth = ConfigurationManager.AppSettings["WhatsAppAuthToken"];
            try
            {
                var client = new RestClient("https://graph.facebook.com/v16.0/" + strPhoneID + "/messages");
                var request = new RestRequest();
                request.Method = Method.Post;
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(warequest);
                request.AddHeader("authorization", String.Format("Bearer {0}", strWhatsappAuth));
                RestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                    iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", Message);
            }
            return iRetValue;
        }
        public static int SendMessage(string PhoneNo, string Message)
        {
            int iRetValue = RetValue.NoRecord;

            if (ConfigurationManager.AppSettings["EnableWhatsAppNotification"] == "0")
                return RetValue.Success;

            string strPhoneID = ConfigurationManager.AppSettings["WhatsAppPhoneID"];
            string strWhatsappAuth = ConfigurationManager.AppSettings["WhatsAppAuthToken"];
            try
            {
                var client = new RestClient("https://login.digitalsms.biz/api?apikey=deec027561328594464e128e8ffb135e" + strPhoneID + " /messages");
                var request = new RestRequest();
                request.Method = Method.Post;
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody("{ \"messaging_product\": \"whatsapp\", \"to\": \"" + PhoneNo + "\", \"type\": \"text\", \"text\": { \"body\": \"" + Message + "\" } }");
                request.AddHeader("authorization", String.Format("Bearer {0}", strWhatsappAuth));
                RestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                    iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string errMessage = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                errMessage = errMessage + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", errMessage);
            }
            return iRetValue;
        }
    }
}