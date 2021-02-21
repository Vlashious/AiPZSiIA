using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public record Game : MongoCollection
    {
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }

        [Required]
        [BsonElement("Year")]
        public int Year { get; set; }

        [Required]
        [BsonElement("Genre")]
        public string Genre { get; set; }

        [Required]
        [BsonElement("Publisher")]
        public string Publisher { get; set; }

        [Required]
        [BsonElement("Country")]
        public string Country { get; set; }
    }
}