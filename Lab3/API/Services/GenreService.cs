using System.Collections.Generic;
using Models;
using MongoDB.Driver;

namespace Services
{
    public class GenreService
    {
        private readonly IMongoCollection<Genre> _genres;

        public GenreService()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("aip");

            _genres = database.GetCollection<Genre>("genres");
        }

        public IEnumerable<Genre> Get() => _genres.Find(genre => true).ToEnumerable();

        public Genre Get(string id) => _genres.Find(genre => genre.Id == id).FirstOrDefault();

        public Genre Create(Genre genre)
        {
            _genres.InsertOne(genre);
            return genre;
        }

        public void Update(string id, Genre genreIn) => _genres.ReplaceOne(genre => genre.Id == id, genreIn);

        public void Remove(string id) => _genres.DeleteOne(genre => genre.Id == id);
    }
}