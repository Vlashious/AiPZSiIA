using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public record Publisher : MongoCollection
    {
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}