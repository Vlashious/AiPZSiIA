using System;
using System.Collections.Generic;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Database
{
    public class Data
    {
        private IEnumerable<Game> _games;
        private IMongoDatabase _database;
        private string _connectionString = "mongodb://localhost:27017";

        public Data()
        {
            _games = new List<Game>();

            var client = new MongoClient(_connectionString);
            _database = client.GetDatabase("aip");

            var kek = _database.ListCollectionNames().ToList();
        }

        public void InsertData()
        {
            //var k
        }
    }
}