using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Services
{
    public abstract class BaseService<T>
    {
        protected IMongoDatabase _database;

        public abstract List<T> Get();
        public abstract T Get(string id);
        public abstract T Create(T obj);
        public abstract void Update(string id, T obj);
        public abstract void Remove(string id);

        public BaseService()
        {
            var client = new MongoClient("mongodb://root:root@375be2ee6c86:27017");
            _database = client.GetDatabase("aip");
            Console.WriteLine($"Connected to database {_database}");
        }
    }
}