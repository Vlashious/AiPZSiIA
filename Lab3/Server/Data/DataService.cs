using System.Collections.Generic;
using System.Net.Http;
using Models;
using Newtonsoft.Json;

namespace Data
{
    public class DataService
    {
        private static HttpClient _client;
        public DataService()
        {
            var settings = new HttpClientHandler();
            settings.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;

            _client = new HttpClient(settings);
        }
        public List<Genre> GetGenres()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, Constant.GenreControllerUri);
            var resp = _client.Send(req).Content.ReadAsStringAsync().Result;
            var genres = JsonConvert.DeserializeObject<List<Genre>>(resp);

            return genres;
        }

        public Genre GetGenre(string id)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, Constant.GenreControllerUri + $"/{id}");
            var resp = _client.Send(req).Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Genre>(resp);
        }

        public void UpdateGenre(Genre genreIn)
        {
            var req = new HttpRequestMessage(HttpMethod.Put, Constant.GenreControllerUri + $"/{genreIn.Id}");
            var json = JsonConvert.SerializeObject(genreIn);
            req.Content = new StringContent(json);
            var resp = _client.Send(req).Content.ReadAsStringAsync().Result;
        }
    }
}