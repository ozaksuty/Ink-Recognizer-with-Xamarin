using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace InkRecognizerSample
{
    public partial class MainPage : ContentPage
    {
        static readonly HttpClient client = new HttpClient();
        public MainPage()
        {
            InitializeComponent();

            client.DefaultRequestHeaders.Add("x-ms-client-request-id", "");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                "key");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        async void Save(object sender, EventArgs e)
        {
            var signatureStrokes = signatureView.Strokes;
            var id = 100;

            var requestModel = new InkServiceRequestModel
            {
                language = "tr-TR"
            };

            foreach (var strokes in signatureStrokes)
            {
                var stroke = new Stroke
                {
                    id = id
                };
                foreach (var s in strokes)
                {
                    var x = s.X.ToString("##.####").Replace(',', '.');
                    var y = s.Y.ToString("##.####").Replace(',', '.');
                    stroke.points += $"{x},{y},";
                }

                stroke.points = stroke.points.Remove(stroke.points.Length - 1);
                requestModel.strokes.Add(stroke);

                id++;
            }
            var body = JsonConvert.SerializeObject(requestModel);
            var result = await client.PutAsync("https://api.cognitive.microsoft.com/inkrecognizer/v1.0-preview/recognize",
                new StringContent(body, Encoding.UTF8, "application/json"));

            var response = await result.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<InkServiceResponseModel>(response);
        }
    }
}