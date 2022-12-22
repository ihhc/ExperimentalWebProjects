using MongoDB.Bson.Serialization.Attributes;

namespace DocumentsCore
{
    public class Document
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
    }
}
