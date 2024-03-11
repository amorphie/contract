namespace amorphie.contract.application
{
    public class DmsTagElementRequest : SoapRequester
    {
        public DmsTagElementRequest(string url, string soapEnvelope) : base(soapEnvelope, url)
        {
        }
    }
}

