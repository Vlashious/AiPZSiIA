using System.Collections.Generic;
using Models;
using MongoDB.Driver;

namespace Services
{
    public class PublisherService : BaseService<Publisher>
    {
        private readonly IMongoCollection<Publisher> _publishers;

        public PublisherService()
        {
            _publishers = _database.GetCollection<Publisher>("publishers");
        }

        public override List<Publisher> Get() => _publishers.Find(publisher => true).ToList();

        public override Publisher Get(string id) => _publishers.Find(publisher => publisher.Id == id).FirstOrDefault();

        public override Publisher Create(Publisher publisher)
        {
            _publishers.InsertOne(publisher);
            return publisher;
        }

        public override void Update(string id, Publisher publisherIn) => _publishers.ReplaceOne(publisher => publisher.Id == id, publisherIn);

        public override void Remove(string id) => _publishers.DeleteOne(publisher => publisher.Id == id);
    }
}