using Signet.Api.Features.Schemas.Domain.ValueObjects;

namespace Signet.Api.Features.Schemas.Domain
{
    public sealed class Schema
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? Id { get; private set; }
        public SchemaVersion Version { get; private set; }
        public SchemaDefinition Definition { get; private set; }

        
        public static async Task<Schema> CreateSchemaAsync(string name, string? description, string? id, string version, string schemaContent)
        {
            var schema = new Schema
            {
                Name = name,
                Description = description,
                Id = id ?? name,
                Version = SchemaVersion.CreateVersion(version),
                Definition = await SchemaDefinition.CreateDefinitionAsync(SchemaDefinitionType.JsonSchema, schemaContent).ConfigureAwait(false)
            };

            schema.CheckInvariants();

            return schema;
        }

        private void CheckInvariants()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("Schema name must be provided and cannot be empty.", nameof(Name));
            }
        }
    }
}
