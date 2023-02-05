using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentAPI.Models
{
    public class Students
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        [BsonElement("Id")]
        public int Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Birthday")]
        public string Birthday { get; set; }

        [BsonElement("Gender")]
        public string Gender { get; set; }

        [BsonElement("GroupNum")]
        public string GroupNum { get; set; }

        [BsonElement("Grant")]
        public int Grant { get; set; }
    }
}
