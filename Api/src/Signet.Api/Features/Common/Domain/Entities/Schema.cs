using Signet.Api.Features.Common.Domain.ValueObjects;
using Signet.Api.Features.Common.Entities;

namespace Signet.Api.Features.Common.Domain.Entities
{
    public sealed class Schema : AggregateRoot
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? SchemaId { get; private set; }
        public string? ChangeLog { get; set; }
        public SchemaVersion Version { get; private set; }
        public SchemaDefinition Definition { get; private set; }

        
        public static async Task<Schema> CreateSchemaAsync(string name, string? description, string? id, string version, string? changeLog, string schemaContent)
        {
            var schema = new Schema
            {
                Name = name,
                Description = description,
                SchemaId = id ?? name.ToLowerInvariant(),
                ChangeLog = changeLog,
                Version = SchemaVersion.CreateVersion(version),
                Definition = await SchemaDefinition.CreateDefinitionAsync(SchemaDefinitionType.JsonSchema, schemaContent).ConfigureAwait(false)
            };

            schema.CheckInvariants();

            return schema;
        }

        protected override void CheckInvariants()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("Schema name must be provided and cannot be empty.", nameof(Name));
            }
        }
    }
}
