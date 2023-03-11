using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using TodoASMX.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Runtime.InteropServices;

namespace TodoASMX
{
    public partial class ConversorPage : ContentPage
	{
		public ConversorPage()
		{
			InitializeComponent();
			
		}

      
        async void OnConvertirActivated(object sender, EventArgs e)
        {
            // Parser to integer de entry value
            int a = int.Parse(nameEntry.Text);
            // Call
            String b = await CaFProxy.CreateSoapEnvelope(nameEntry.Text);
            // Delete LR/RC
            b = b.Replace("\r", "");
            b = b.Replace("\n", "");
            // Deserialized
            XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
            // Declare an object variable of the type to be deserialized.
            Envelope i;
            using (StringReader reader = new StringReader(b))
            {
                i = (Envelope)serializer.Deserialize(reader);
            }
            // Show 
            nameEntry2.Text = i.Body.CaFResponse.CaFResult.ToString();

            String res = await Zeus.CreateSoapEnvelope("a");

            // Refresh
            await Navigation.PopAsync();

		}

        private void OnUrlActivated(object sender, EventArgs e)
        {
            Browser.OpenAsync("https://www.elguille.info/NET/WebServices/CelsiusFahrenheit.asmx", BrowserLaunchMode.SystemPreferred);
        }
    }
}
