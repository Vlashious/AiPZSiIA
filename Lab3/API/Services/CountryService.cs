using System.Collections.Generic;
using Models;
using MongoDB.Driver;

namespace Services
{
    public class CoutnryService
    {
        private readonly IMongoCollection<Country> _countries;

        public CoutnryService()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("aip");

            _countries = database.GetCollection<Country>("countries");
        }

        public IEnumerable<Country> Get() => _countries.Find(country => true).ToEnumerable();

        public Country Get(string id) => _countries.Find(country => country.Id == id).FirstOrDefault();

        public Country Create(Country country)
        {
            _countries.InsertOne(country);
            return country;
        }

        public void Update(string id, Country countryIn) => _countries.ReplaceOne(country => country.Id == id, countryIn);

        public void Remove(string id) => _countries.DeleteOne(country => country.Id == id);
    }
}