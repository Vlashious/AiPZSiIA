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
        private IMongoCollection<BsonDocument> _gameCollection;
        private IMongoCollection<BsonDocument> _countryCollection;
        private IMongoCollection<BsonDocument> _genreCollection;
        private IMongoCollection<BsonDocument> _publisherCollection;
        private string _connectionString = "mongodb://localhost:27017";

        public Data()
        {
            _games = new List<Game>();

            var client = new MongoClient(_connectionString);
            _database = client.GetDatabase("aip");
            _gameCollection = _database.GetCollection<BsonDocument>("games");
            _countryCollection = _database.GetCollection<BsonDocument>("countries");
            _genreCollection = _database.GetCollection<BsonDocument>("genres");
            _publisherCollection = _database.GetCollection<BsonDocument>("publishers");
        }

        public void InsertData(Game game)
        {
            _gameCollection.InsertOne(game.ToBsonDocument());
        }

        public void InsertData(Country country)
        {
            _countryCollection.InsertOne(country.ToBsonDocument());
        }

        public void InsertData(Genre genre)
        {
            _genreCollection.InsertOne(genre.ToBsonDocument());
        }

        public void InsertData(Publisher publisher)
        {
            _publisherCollection.InsertOne(publisher.ToBsonDocument());
        }

        public IEnumerable<Game> GetGames()
        {
            var json = _gameCollection.Find(new BsonDocument()).ToList().Select(doc => BsonSerializer.Deserialize<Game>(doc));
            return json;
        }

        public Game GetGame(ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var game = _gameCollection.Find(filter).FirstOrDefault();
            return BsonSerializer.Deserialize<Game>(game);
        }

        public void UpdateGame(ObjectId id, Game game)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            _gameCollection.FindOneAndReplace(filter, game.ToBsonDocument());
        }

        public void RemoveGame(ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            _gameCollection.FindOneAndDelete(filter);
        }

        public IEnumerable<Country> GetCountries()
        {
            var json = _countryCollection.Find(new BsonDocument()).ToList().Select(doc => BsonSerializer.Deserialize<Country>(doc));
            return json;
        }

        public Country GetCountry(ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var country = _countryCollection.Find(filter).FirstOrDefault();
            return BsonSerializer.Deserialize<Country>(country);
        }

        public void UpdateCountry(ObjectId id, Country country)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            _countryCollection.FindOneAndReplace(filter, country.ToBsonDocument());
        }

        public void RemoveCountry(ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            _countryCollection.FindOneAndDelete(filter);
        }

        public IEnumerable<Country> GetGenres()
        {
            var json = _genreCollection.Find(new BsonDocument()).ToList().Select(doc => BsonSerializer.Deserialize<Country>(doc));
            return json;
        }

        public Country GetGenre(ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var genre = _genreCollection.Find(filter).FirstOrDefault();
            return BsonSerializer.Deserialize<Country>(genre);
        }

        public void UpdateGenre(ObjectId id, Genre genre)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            _genreCollection.FindOneAndReplace(filter, genre.ToBsonDocument());
        }

        public void RemoveGenre(ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            _genreCollection.FindOneAndDelete(filter);
        }

        public IEnumerable<Country> GetPublishers()
        {
            var json = _publisherCollection.Find(new BsonDocument()).ToList().Select(doc => BsonSerializer.Deserialize<Country>(doc));
            return json;
        }

        public Country GetPublisher(ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var publisher = _publisherCollection.Find(filter).FirstOrDefault();
            return BsonSerializer.Deserialize<Country>(publisher);
        }

        public void UpdatePublisher(ObjectId id, Publisher publisher)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            _publisherCollection.FindOneAndReplace(filter, publisher.ToBsonDocument());
        }

        public void RemovePublisher(ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            _publisherCollection.FindOneAndDelete(filter);
        }
    }
}