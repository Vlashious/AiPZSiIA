using System.Collections.Generic;
using Models;
using MongoDB.Driver;

namespace Services
{
    public class CountryService : BaseService<Country>
    {
        private readonly IMongoCollection<Country> _countries;

        public CountryService()
        {
            _countries = _database.GetCollection<Country>("countries");
        }

        public override List<Country> Get() => _countries.Find(country => true).ToList();

        public override Country Get(string id) => _countries.Find(country => country.Id == id).FirstOrDefault();

        public override Country Create(Country country)
        {
            _countries.InsertOne(country);
            return country;
        }

        public override void Update(string id, Country countryIn) => _countries.ReplaceOne(country => country.Id == id, countryIn);

        public override void Remove(string id) => _countries.DeleteOne(country => country.Id == id);
    }
}