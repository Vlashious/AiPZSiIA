using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Database
{
    public class Data
    {
        private IEnumerable<Game> _games;
        private IMongoDatabase _database;
        private IMongoCollection<BsonDocument> _collection;
        private string _connectionString = "mongodb://localhost:27017";

        public Data()
        {
            _games = new List<Game>();

            var client = new MongoClient(_connectionString);
            _database = client.GetDatabase("aip");
            _collection = _database.GetCollection<BsonDocument>("games");
        }

        public void InsertData(Game game)
        {
            _collection.InsertOne(game.ToBsonDocument());
        }

        public IEnumerable<Game> GetGames()
        {
            var json = _collection.Find(new BsonDocument()).ToList().Select(doc => BsonSerializer.Deserialize<Game>(doc));
            return json;
        }

        public Game GetGame(ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var game = _collection.Find(filter).FirstOrDefault();
            return BsonSerializer.Deserialize<Game>(game);
        }

        public void UpdateGame(ObjectId id, Game game)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            _collection.FindOneAndReplace(filter, game.ToBsonDocument());
        }

        public void RemoveGame(ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            _collection.FindOneAndDelete(filter);
        }
    }
}