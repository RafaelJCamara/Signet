using MongoDB.Driver;
using Signet.Api.Features.Schemas.Domain.Entities;
using Signet.Api.Features.Schemas.Domain.Repositories;
using Signet.Api.Features.Schemas.Infrastructure.Persistence.Mappers;
using Signet.Api.Features.Schemas.Infrastructure.Persistence.Models;

namespace Signet.Api.Features.Schemas.Infrastructure.Persistence.Repositories
{
    public class SchemaRepository(IMongoDatabase schemaRegistryDatabase) : ISchemaRepository
    {
        private const string SchemaCollection = "schemas";

        public async Task CreateAsync(Schema aggregateRoot, CancellationToken cancellationToken = default)
        {
            var collection = schemaRegistryDatabase.GetCollection<MongoSchema>(SchemaCollection);

            await collection.InsertOneAsync(SchemaToMongoSchemaMapper.MapFrom(aggregateRoot));
        }
    }
}
