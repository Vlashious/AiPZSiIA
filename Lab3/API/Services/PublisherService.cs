using System.Collections.Generic;
using Models;
using MongoDB.Driver;

namespace Services
{
    public class PublisherService : IService<Publisher>
    {
        private readonly IMongoCollection<Publisher> _publishers;

        public PublisherService()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("aip");

            _publishers = database.GetCollection<Publisher>("publishers");
        }

        public List<Publisher> Get() => _publishers.Find(publisher => true).ToList();

        public Publisher Get(string id) => _publishers.Find(publisher => publisher.Id == id).FirstOrDefault();

        public Publisher Create(Publisher publisher)
        {
            _publishers.InsertOne(publisher);
            return publisher;
        }

        public void Update(string id, Publisher publisherIn) => _publishers.ReplaceOne(publisher => publisher.Id == id, publisherIn);

        public void Remove(string id) => _publishers.DeleteOne(publisher => publisher.Id == id);
    }
}