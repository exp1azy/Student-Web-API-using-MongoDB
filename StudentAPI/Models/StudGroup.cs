using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StudentAPI.Models
{
    public class StudGroup
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        [BsonElement("GroupNum")]
        public string GroupNum { get; set; }

        [BsonElement("Course")]
        public int Course { get; set; }

        [BsonElement("Dep")]
        public string Dep { get; set; }
    }
}
