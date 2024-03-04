using System.Xml;
using amorphie.contract.core;

namespace amorphie.contract.application
{
    public interface IDocumentDysAppService
    {
        List<Element> GetAllTagsDys(int ReferenceId, CancellationToken cancellationToken);
    }

    public class DocumentDysAppService : IDocumentDysAppService
    {

        public DocumentDysAppService()
        {
        }

        public List<Element> GetAllTagsDys(int ReferenceId, CancellationToken cancellationToken)
        {
            string xmlString = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                <soap:Body>
                                    <GetTagElementList xmlns=""http://tempuri.org/"">
                                    <TagID>{ReferenceId}</TagID>
                                    </GetTagElementList>
                                </soap:Body>
                                </soap:Envelope>";
            var dmsTagElementRequest = new DmsTagElementRequest(StaticValuesExtensions.DmsUrl,xmlString);
            string response = dmsTagElementRequest.SendRequest();
            var parsedElement = XLMLParser(response);
            return parsedElement;
        }

        private List<Element> XLMLParser(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsMgr.AddNamespace("tempuri", "http://tempuri.org/");
            nsMgr.AddNamespace("diffgr", "urn:schemas-microsoft-com:xml-diffgram-v1");
            nsMgr.AddNamespace("msdata", "urn:schemas-microsoft-com:xml-msdata");

            XmlNodeList tagListNodes = xmlDoc.SelectNodes("//tempuri:GetTagElementListResult/diffgr:diffgram/NewDataSet/TagList", nsMgr);

            List<Element> elements = new List<Element>();
            foreach (XmlNode node in tagListNodes)
            {
                string? elementName = node.SelectSingleNode("ElementName")?.InnerText;
                string? elementID = node.SelectSingleNode("ElementID")?.InnerText;
                var elem = new Element
                    {
                        ElementName = elementName,
                        ElementID = elementID
                    };
                elements.Add(elem);
            }
            return elements;
        }
    }
}