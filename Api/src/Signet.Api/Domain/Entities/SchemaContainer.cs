using System.Text.Json;
using Signet.Api.Common.Entities;
using Signet.Api.Domain.Utilities;
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

    public async Task AddSchemaToContainerAsync(Guid? id, string version, string? changeLog, string schemaDefinition)
    {
        var newSchema = await Schema.CreateSchemaAsync(id, version, changeLog, schemaDefinition);

        if (Schemas.Any(schema => schema.Version == newSchema.Version)) {
            //TODO: add business exception
            throw new InvalidOperationException("");
        }

        _schemas.Add(newSchema);

        CheckInvariants();
    }

    public async Task AddSchemaToContainerAsync(Schema schema)
    {
        //TODO: perform check based on schema compatibility level
        _schemas.Add(schema);
        CheckInvariants();
    }

    protected override void CheckInvariants()
    {
        //TODO
        //TODO: perform check based on schema compatibility level
        if (Schemas.Any() && Schemas.Count() > 1)
        {
            if(string.Equals(CompatibilityLevel.Level, "backward", StringComparison.InvariantCultureIgnoreCase))
            {
                var lastSchema = Schemas.Last();
                var secondToLastSchema = Schemas.ElementAt(Schemas.Count() - 2);

                var areSchemasBackwardCompatible = AreSchemasBackwardCompatible(lastSchema, secondToLastSchema);

                if (!areSchemasBackwardCompatible)
                {
                    //TODO: Add custom business exception
                    throw new InvalidOperationException("Schemas in the container don't follow the compatility level defined.");
                }
            }
            if (string.Equals(CompatibilityLevel.Level, "forward", StringComparison.InvariantCultureIgnoreCase))
            {
                var areSchemasForwardCompatible = AreSchemasForwardCompatible(
                    Schemas.Last(),
                    Schemas.ElementAt(Schemas.Count() - 2)
                );

                if (!areSchemasForwardCompatible)
                {
                    //TODO: Add custom business exception
                    throw new InvalidOperationException("Schemas in the container don't follow the compatility level defined.");
                }
            }
            if (string.Equals(CompatibilityLevel.Level, "backward_transitive", StringComparison.InvariantCultureIgnoreCase))
            {

            }
            if (string.Equals(CompatibilityLevel.Level, "forward_transitive", StringComparison.InvariantCultureIgnoreCase))
            {

            }
        }
    }

    private bool AreSchemasBackwardCompatible(Schema newSchema, Schema previousSchema)
    {
        var checker = new BackwardCompatibilityChecker();
        string oldSchemaJson = previousSchema.Definition.Content;
        string newSchemaJson = newSchema.Definition.Content;

        //TODO: get rid of this horrible thing
        var result = checker.CheckAsync(oldSchemaJson, newSchemaJson).GetAwaiter().GetResult();
        return result.Count() == 0;
    }

    private bool AreSchemasForwardCompatible(Schema s1, Schema s2)
    {
        var checker = new ForwardCompatibilityChecker();
        var issues = checker.CheckAsync(s1.Definition.Content, s2.Definition.Content).GetAwaiter().GetResult();
        return issues.Count == 0;
    }

    private static string GenerateIdFromName(string name) => JsonNamingPolicy.KebabCaseLower.ConvertName(name);
}
