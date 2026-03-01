using Signet.Api.Domain.Entities;
using Signet.Api.Features.Schemas.Infrastructure.Persistence.Models;

namespace Signet.Api.Features.Schemas.Infrastructure.Persistence.Mappers;

public static class MongoMappers
{
    public static MongoSchemaContainer MapToMongo(SchemaContainer schemaContainer)
    {
        return new MongoSchemaContainer
        {
            Id = schemaContainer.Id.ToString(),
            Name = schemaContainer.Name,
            Description = schemaContainer.Description,
            NamedContainerId = schemaContainer.NamedId,
            CompatibilityLevel = schemaContainer.CompatibilityLevel.Level,
            Schemas = schemaContainer.Schemas.Select(scs => new MongoSchema
            {
                Id = scs.Id.ToString(),
                ChangeLog = scs.ChangeLog,
                Version = scs.Version.ToString(),
                SchemaDetails = new SchemaDetails
                {
                    SchemaType = scs.Definition.DefinitionType.ToString(),
                    SchemaDefinition = scs.Definition.Content
                }
            })
        };
    }

    public static async Task<SchemaContainer> MapToDomainAsync(MongoSchemaContainer mongoSchemaContainer)
    {
        SchemaContainer schemaContainer = await SchemaContainer.CreateSchemaContainerAsync(
            Guid.Parse(mongoSchemaContainer.Id),
            mongoSchemaContainer.Name,
            mongoSchemaContainer.NamedContainerId,
            mongoSchemaContainer.Description,
            mongoSchemaContainer.CompatibilityLevel
        );

        foreach(MongoSchema schema in mongoSchemaContainer.Schemas)
        {
            var newSchema = await Schema.CreateSchemaAsync(schema.Version, schema.ChangeLog, schema.SchemaDetails.SchemaDefinition);
            await schemaContainer.AddSchemaToContainerAsync(newSchema.Version.ToString(), newSchema.ChangeLog, newSchema.Definition.Content);
        }

        return schemaContainer;
    }
}
