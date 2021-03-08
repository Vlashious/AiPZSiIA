using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Genre { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Publisher { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Country { get; set; }

        [Required]
        public string Name { get; set; }
    }
}