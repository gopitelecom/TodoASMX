using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using TodoASMX.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace TodoASMX.Models
{
    public class CaFProxy
    {

        public static async Task<HttpResponseMessage> PostXmlRequest(string baseUrl, string xmlString)
        {
            using (var httpClient = new HttpClient())
            {
                var httpContent = new StringContent(xmlString, Encoding.UTF8, "text/xml");
                httpContent.Headers.Add("SOAPAction", "http://elGuille/WebServices/CaF");

                return await httpClient.PostAsync(baseUrl, httpContent);
            }
        }
        public static async Task<string> CreateSoapEnvelope(String pValor)
        {
            string soapString = @"<?xml version=""1.0"" encoding=""utf-8""?>
          <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
            <soap:Body>
                <CaF xmlns=""http://elGuille/WebServices"">
                    <valor>XXX</valor>
                </CaF>
            </soap:Body>
          </soap:Envelope>";

            soapString = soapString.Replace("XXX", pValor);
            HttpResponseMessage response = await PostXmlRequest("https://www.elguille.info/NET/WebServices/CelsiusFahrenheit.asmx?WSDL", soapString);
            string content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }

    public class CaFResponse
    {
        [XmlElement(ElementName = "CaFResult", Namespace = "http://elGuille/WebServices")]
        public string CaFResult { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Body
    {
        [XmlElement(ElementName = "CaFResponse", Namespace = "http://elGuille/WebServices")]
        public CaFResponse CaFResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Body Body { get; set; }
        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
    }


}
