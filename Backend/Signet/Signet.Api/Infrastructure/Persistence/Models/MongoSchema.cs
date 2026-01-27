using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Signet.Api.Infrastructure.Persistence.Models
{
    public sealed class MongoSchema
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? SchemaId { get; set; }
        public string? ChangeLog { get; set; }
        public string Version { get; set; }
        public SchemaDetails SchemaDetails { get; set; }
    }

    public sealed class SchemaDetails
    {
        public string SchemaType { get; set; }
        public string SchemaContent { get; set; }
    }
}
