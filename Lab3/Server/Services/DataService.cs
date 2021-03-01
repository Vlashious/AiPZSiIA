using System.Net.Http;

namespace Services
{
    public class DataService
    {
        public HttpClient Client;
        public DataService()
        {
            HttpClientHandler handler = new();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            Client = new HttpClient(handler);
        }
        public string MakeRequest()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/api/main/say");
            var response = Client.Send(req);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}