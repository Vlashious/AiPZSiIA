using System.Collections.Generic;
using Models;
using MongoDB.Driver;

namespace Services
{
    public class GameService
    {
        private readonly IMongoCollection<Game> _games;

        public GameService()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("aip");

            _games = database.GetCollection<Game>("games");
        }

        public IEnumerable<Game> Get() => _games.Find(game => true).ToEnumerable();

        public Game Get(string id) => _games.Find(game => game.Id == id).FirstOrDefault();

        public Game Create(Game game)
        {
            _games.InsertOne(game);
            return game;
        }

        public void Update(string id, Game gameIn) => _games.ReplaceOne(game => game.Id == id, gameIn);

        public void Remove(string id) => _games.DeleteOne(game => game.Id == id);
    }
}