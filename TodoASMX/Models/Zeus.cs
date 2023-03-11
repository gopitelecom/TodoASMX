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
    public class Zeus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostXmlRequest(string baseUrl, string xmlString)
        {
            using (var httpClient = new HttpClient())
            {
                var httpContent = new StringContent(xmlString, Encoding.UTF8, "text/xml");
                httpContent.Headers.Add("SOAPAction", "http://www.gopitelecom.com/getMedidasSensores");

                return await httpClient.PostAsync(baseUrl, httpContent);
            }
        }
        public static async Task<string> CreateSoapEnvelope(String pValor)
        {
            string soapString = @"<? xml version = ""1.0"" encoding = ""utf-8""?>
            <soap:Envelope xmlns: soap = ""http://schemas.xmlsoap.org/soap/envelope/"" xmlns: xsi = ""http://www.w3.org/2001/XMLSchema-instance"" xmlns: xsd = ""http://www.w3.org/2001/XMLSchema"">
                <soap:Body>
                    <getMedidasSensores xmlns = ""http://www.gopitelecom.com"">
                         <idSensor>XXX</idSensor>
                    </getMedidasSensores>
                </soap:Body>
            </soap:Envelope>";

            soapString = soapString.Replace("XXX", pValor);
            HttpResponseMessage response = await PostXmlRequest("http://51.83.73.42/ZeusWebService/ZeusService.asmx?WSDL", soapString);
            string content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }

    public class GetMedidasSensoresResponse
    {
        public string GetMedidasSensoresResult { get; set; }
        public string Xmlns { get; set; }
        public string Text { get; set; }
    }

    public class Body_
    {
        public GetMedidasSensoresResponse GetMedidasSensoresResponse { get; set; }
    }

    public class Envelope_
    {
        public Body Body_ { get; set; }
        public string Soap { get; set; }
        public string Xsi { get; set; }
        public string Xsd { get; set; }
        public string Text { get; set; }
    }

}
