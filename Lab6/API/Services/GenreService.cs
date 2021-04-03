using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Models;
using MongoDB.Driver;

namespace Services
{
    public class GenreService : BaseService<Genre>
    {
        private readonly IMongoCollection<Genre> _genres;

        public GenreService()
        {

            _genres = _database.GetCollection<Genre>("genres");
        }

        public override List<Genre> Get() => _genres.Find(genre => true).ToList();

        public override Genre Get(string id) => _genres.Find(genre => genre.Id == id).FirstOrDefault();

        public override Genre Create(Genre genre)
        {
            _genres.InsertOne(genre);
            return genre;
        }

        public override void Update(string id, Genre genreIn) => _genres.ReplaceOne(genre => genre.Id == id, genreIn);

        public override void Remove(string id) => _genres.DeleteOne(genre => genre.Id == id);
    }
}