using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public record Game
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string StringId { get; set; }

        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }
        [Required]
        [BsonElement("Year")]
        public int Year { get; set; }
    }
}