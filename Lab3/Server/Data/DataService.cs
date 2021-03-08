using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
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

        public Genre CreateGenre(Genre genre)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, Constant.GenreControllerUri);
            req.Content = new StringContent(JsonConvert.SerializeObject(genre));
            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var resp = _client.Send(req).Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Genre>(resp);
        }

        public void UpdateGenre(Genre genreIn)
        {
            var req = new HttpRequestMessage(HttpMethod.Put, Constant.GenreControllerUri);
            var json = JsonConvert.SerializeObject(genreIn);
            req.Content = new StringContent(json);
            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var resp = _client.Send(req).Content.ReadAsStringAsync().Result;
        }

        public void RemoveGenre(string id)
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, Constant.GenreControllerUri + $"/{id}");
            var resp = _client.Send(req).Content.ReadAsStringAsync().Result;
        }
        public List<Country> GetCountries()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, Constant.CountryControllerUri);
            var resp = _client.Send(req).Content.ReadAsStringAsync().Result;
            var countries = JsonConvert.DeserializeObject<List<Country>>(resp);

            return countries;
        }

        public Country GetCountry(string id)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, Constant.CountryControllerUri + $"/{id}");
            var resp = _client.Send(req).Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Country>(resp);
        }

        public Country CreateCountry(Country country)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, Constant.CountryControllerUri);
            req.Content = new StringContent(JsonConvert.SerializeObject(country));
            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var resp = _client.Send(req).Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Country>(resp);
        }

        public void UpdateCountry(Country countryIn)
        {
            var req = new HttpRequestMessage(HttpMethod.Put, Constant.CountryControllerUri);
            var json = JsonConvert.SerializeObject(countryIn);
            req.Content = new StringContent(json);
            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var resp = _client.Send(req).Content.ReadAsStringAsync().Result;
        }

        public void RemoveCountry(string id)
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, Constant.CountryControllerUri + $"/{id}");
            var resp = _client.Send(req).Content.ReadAsStringAsync().Result;
        }
    }
}