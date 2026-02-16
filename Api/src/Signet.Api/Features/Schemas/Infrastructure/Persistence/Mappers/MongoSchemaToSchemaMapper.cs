using Signet.Api.Features.Schemas.Domain.Entities;
using Signet.Api.Features.Schemas.Infrastructure.Persistence.Models;

namespace Signet.Api.Features.Schemas.Infrastructure.Persistence.Mappers
{
    public static class MongoSchemaToSchemaMapper
    {
        public static async Task<Schema> MapToDomainAsync(MongoSchema mongoSchema)
        {
            if (mongoSchema is null)
                throw new ArgumentNullException(nameof(mongoSchema));

            if (mongoSchema.SchemaDetails is null)
                throw new ArgumentException("SchemaDetails cannot be null.", nameof(mongoSchema));

            return await Schema.CreateSchemaAsync(
                mongoSchema.Name,
                mongoSchema.Description,
                mongoSchema.SchemaId,
                mongoSchema.Version,
                mongoSchema.ChangeLog,
                mongoSchema.SchemaDetails.SchemaContent
            ).ConfigureAwait(false);
        }
    }
}
