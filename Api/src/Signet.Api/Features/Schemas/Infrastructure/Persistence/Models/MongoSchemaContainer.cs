using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Signet.Api.Features.Schemas.Infrastructure.Persistence.Models;

public sealed class MongoSchemaContainer
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }
    public string? NamedContainerId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string CompatibilityLevel { get; set; }
    public IEnumerable<MongoSchema> Schemas { get; set; }
}

public sealed class MongoSchema
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }
    public string Version { get; set; }
    public string? ChangeLog { get; set; }
    public SchemaDetails SchemaDetails { get; set; }
}

public sealed class SchemaDetails
{
    public string SchemaType { get; set; }
    public string SchemaDefinition { get; set; }
}
