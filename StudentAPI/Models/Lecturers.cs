using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StudentAPI.Models
{
    public class Lecturers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        [BsonElement("Id")]
        public int Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Stage")]
        public int Stage { get; set; }

        [BsonElement("Dep")]
        public string Dep { get; set; }
    }
}
