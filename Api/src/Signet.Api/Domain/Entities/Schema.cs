using Signet.Api.Common.Entities;
using Signet.Api.Domain.ValueObjects;

namespace Signet.Api.Domain.Entities;

public sealed class Schema : Entity
{
    public string? ChangeLog { get; set; }
    public SchemaVersion Version { get; private set; }
    public SchemaDefinition Definition { get; private set; }

    
    public static async Task<Schema> CreateSchemaAsync( string version, string? changeLog, string schemaDefinition)
    {
        var schema = new Schema
        {
            ChangeLog = changeLog,
            Version = await SchemaVersion.CreateVersionAsync(version),
            Definition = await SchemaDefinition.CreateDefinitionAsync(SchemaDefinitionType.JsonSchema, schemaDefinition).ConfigureAwait(false)
        };

        schema.CheckInvariants();

        return schema;
    }

    public async Task<bool> IsContentValidAgainstSchemaAsync(string content, CancellationToken cancellationToken = default)
    {
        return await Definition.IsContentValidAgainstSchemaAsync(content, cancellationToken).ConfigureAwait(false);
    }

    protected override void CheckInvariants()
    {
        //TODO
    }
}
