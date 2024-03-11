using System.Net;

namespace amorphie.contract.application
{
    public abstract class SoapRequester
    {
        protected string SoapEnvelope { get; set; }
        protected string Url { get; set; }

        protected SoapRequester(string soapEnvelope, string url)
        {
            SoapEnvelope = soapEnvelope;
            Url = url;
        }

        public string SendRequest()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                using (Stream stream = request.GetRequestStream())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(SoapEnvelope);
                }

                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return $"SOAP request failed: {ex.Message}";
            }
        }
    }
}