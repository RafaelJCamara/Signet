using Signet.Api.Features.Schemas.Domain.Entities;
using Signet.Api.Infrastructure.Persistence.Models;

namespace Signet.Api.Infrastructure.Persistence.Mappers
{
    public static class SchemaToMongoSchemaMapper
    {
        public static MongoSchema MapFrom(Schema schema)
        {
            return new MongoSchema
            {
                Id = schema.Id.ToString(),
                Name = schema.Name,
                ChangeLog = schema.ChangeLog,
                Description = schema.Description,
                Version = schema.Version.ToString(),
                SchemaId = schema.SchemaId?.ToString(),
                SchemaDetails = new SchemaDetails
                {
                    SchemaType = schema.Definition.DefinitionType.ToString(),
                    SchemaContent = schema.Definition.Content
                }
            };
        }
    }
}
