using System.Collections.Generic;
using Models;
using MongoDB.Driver;

namespace Services
{
    public class GameService : BaseService<Game>
    {
        private readonly IMongoCollection<Game> _games;

        public GameService()
        {
            _games = _database.GetCollection<Game>("games");
        }

        public override List<Game> Get() => _games.Find(game => true).ToList();

        public override Game Get(string id) => _games.Find(game => game.Id == id).FirstOrDefault();

        public override Game Create(Game game)
        {
            _games.InsertOne(game);
            return game;
        }

        public override void Update(string id, Game gameIn) => _games.ReplaceOne(game => game.Id == id, gameIn);

        public override void Remove(string id) => _games.DeleteOne(game => game.Id == id);
    }
}