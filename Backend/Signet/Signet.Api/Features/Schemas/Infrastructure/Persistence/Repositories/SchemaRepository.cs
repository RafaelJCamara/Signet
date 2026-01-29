using MongoDB.Driver;
using Signet.Api.Features.Schemas.Domain.Entities;
using Signet.Api.Features.Schemas.Domain.Repositories;
using Signet.Api.Features.Schemas.Infrastructure.Persistence.Mappers;
using Signet.Api.Features.Schemas.Infrastructure.Persistence.Models;
using System.Linq.Expressions;

namespace Signet.Api.Features.Schemas.Infrastructure.Persistence.Repositories
{
    public class SchemaRepository(IMongoDatabase schemaRegistryDatabase) : ISchemaRepository
    {
        private const string SchemaCollection = "schemas";
        private readonly IMongoCollection<MongoSchema> _collection = schemaRegistryDatabase.GetCollection<MongoSchema>(SchemaCollection);

        public async Task CreateAsync(Schema aggregateRoot, CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(SchemaToMongoSchemaMapper.MapFrom(aggregateRoot));
        }

        public async Task<Schema> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var schema = await _collection.FindAsync(schema => schema.Id == id.ToString());

            return await MongoSchemaToSchemaMapper.MapToDomainAsync(schema.FirstOrDefault());
        }

        public async Task<IEnumerable<Schema>> GetBySchemaIdAsync(string schemaId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<MongoSchema>.Filter.Eq(ms => ms.SchemaId, schemaId);

            using var cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken).ConfigureAwait(false);
            var mongoSchemas = await cursor.ToListAsync(cancellationToken).ConfigureAwait(false);

            if (mongoSchemas == null || mongoSchemas.Count == 0)
            {
                return Enumerable.Empty<Schema>();
            }

            var tasks = mongoSchemas.Select(ms => MongoSchemaToSchemaMapper.MapToDomainAsync(ms));
            var schemas = await Task.WhenAll(tasks).ConfigureAwait(false);

            return schemas;
        }
    }
}
