using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public abstract record MongoCollection
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string StringId
        {
            get => Id.ToString();
            set => _innderId = value;
        }

        public string _innderId;
    }
}