using System.Text.Json;
using Signet.Api.Common.Entities;
using Signet.Api.Domain.ValueObjects;

namespace Signet.Api.Domain.Entities;

public sealed class SchemaContainer : AggregateRoot
{
    public string Name { get; private set; }
    public string NamedId { get; private set; }
    public string? Description { get; private set; }
    public CompatibilityLevel CompatibilityLevel { get; private set; }

    private List<Schema> _schemas = [];
    public IReadOnlyCollection<Schema> Schemas => _schemas.AsReadOnly(); 

    private SchemaContainer() {}

    public static async ValueTask<SchemaContainer> CreateSchemaContainerAsync(Guid? id, string name, string? namedId, string? description, string compatibilityLevel)
    {
        var container = new SchemaContainer
        {
            Name = name,
            Description = description,
            NamedId = namedId ?? GenerateIdFromName(name),
            CompatibilityLevel = await CompatibilityLevel.CreateCompatibilityLevelAsync(compatibilityLevel)
        };

        container.SetId(id);

        container.CheckInvariants();

        return container;
    }   

    public async Task AddSchemaToContainerAsync(string version, string? changeLog, string schemaDefinition)
    {
        var newSchema = await Schema.CreateSchemaAsync(version, changeLog, schemaDefinition);

        if (Schemas.Any(schema => schema.Version == newSchema.Version)) {
            //TODO: add business exception
            throw new InvalidOperationException("");
        }

        //TODO: perform check based on schema compatibility level
        _schemas.Add(newSchema);
    }

    protected override void CheckInvariants()
    {
        //TODO
    }

    private static string GenerateIdFromName(string name) => JsonNamingPolicy.KebabCaseLower.ConvertName(name);
}
