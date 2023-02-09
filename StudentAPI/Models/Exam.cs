using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StudentAPI.Models
{
    public class Exam
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        [BsonElement("StudId")]
        public int StudId { get; set; }

        [BsonElement("Subject")]
        public string Subject { get; set; }

        [BsonElement("Mark")]
        public int Mark { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("LectId")]
        public int LectId { get; set; }
    }
}
